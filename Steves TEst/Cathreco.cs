using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
// ADU needs to interoperate with the legacy DLL
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;


namespace CathRecorderMain
{
    public partial class MainForm : Form
    {

        public static Bitmap bitmap_LastSavedRed;
        public static Rectangle rectRed;
        public static PixelFormat pixelformat = PixelFormat.Format32bppArgb;

        // This is the structure of data to be saved in the settings file.
        public struct RecParam
        {
            public string strSaveDirectory;        //  Directory where images are stored
            public string strFileBaseName;         //  Base name for image files
            public int nDelay1;                  //  Minimum time delay between recordings
            public int nDelay2;                  //  Minimum time delay between recordings
            public double fTimeBetweenFrames;       //  Time in seconds between taking (but not necc recording) shots
        }
        public RecParam recparamSet;         // structure holding recording parameters.
        public string strCurrentRecParamFile;     // string holding path/name of last parameter file

        private uEye.Camera cam;
        private uEye.Defines.Status statusRet = 0;
        private bool fRecordingImageToDisk = false;  // flag that recording is taking place - hold video;
        private bool fReadingImage = false;          // flag true if pulling down image
        private bool fManualCathodeSense = false;    // flag true if cathode sense button pressed;
        private int nImagesRecorded = 0;             //  number of images recorded this session;

        private Bitmap bitmap_Current, bitmap_CurrentRed;

        private Statemachine statemach;  // Main sequencer statemachine.

        // Import ADU DLL functions

        private IntPtr pAduIO;   // pointer to Adu
        public ulong ulWritten;

        [DllImport("aduhid.dll")]
        public static extern IntPtr OpenAduDevice(UInt32 iTimeout);

        [DllImport("aduhid.dll")]
        public static extern bool WriteAduDevice(IntPtr hFile,
            [MarshalAs(UnmanagedType.LPStr)]string lpBuffer,
            UInt32 nNumberOfBytesToWrite,
            out UInt32 lpNumberOfBytesWritten,
            UInt32 iTimeout);

        [DllImport("aduhid.dll")]
        public static extern bool ReadAduDevice(IntPtr hFile,
            StringBuilder lpBuffer,
            UInt32 nNumberOfBytesToRead,
            out UInt32 lpNumberOfBytesRead,
            UInt32 iTimeout);

        [DllImport("aduhid.dll")]
        public static extern void CloseAduDevice(IntPtr hFile);


        // FUNCTION:  Initialize the Main Program Form
        //
        //
        public MainForm()
        {
            // You know... 
            InitializeComponent();

            statemach = new Statemachine();
            //tc = new testclass();

            // Preload all bitmaps
            System.Reflection.Assembly assyThis = System.Reflection.Assembly.GetExecutingAssembly();
            Stream fileImage = assyThis.GetManifestResourceStream("Cathreco.IonicLogo.bmp");

            // set the starting bitmaps
            bitmap_CurrentRed = new Bitmap(fileImage);
            bitmap_LastSavedRed = new Bitmap(fileImage);

            // set the starting images into the pictboxes
            pictBox_Current.Image = bitmap_CurrentRed;
            pictBox_LastSaved.Image = bitmap_LastSavedRed;

            // Initialize bitmaps and pictboxes
            pictBox_Current.SizeMode = PictureBoxSizeMode.StretchImage;
            pictBox_LastSaved.SizeMode = PictureBoxSizeMode.StretchImage;

            rectRed.Width = pictBox_Current.Image.Width;
            rectRed.Height = pictBox_Current.Image.Height;

            // Get the name of the current record parameters file from the registry
            //strCurrentRecParamFile = (string)Registry.GetValue(@"HKEY_CURRENT_USER\AppEvents\Schemes\Apps\CathrecoSettings", @"PathRecParamFile", "Undefined");

            // Loadup the current set of recording parameters
            LoadRecParamCurrent();

            // Initialize the camera obviously.
            InitCamera();

            // Open the ADU Device
            AduOpen();

        }

        // FUNCTION:  onFrameEvent:   currently not used
        //
        //
        private void onFrameEvent(object sender, EventArgs e)
        {
            // We literally do nothing here right now
            // using a timer seems to resolve a lot of issues.
        }

        // FUNCTION:  LoadRecParamCurren:  Load recording parameters file:  TODO
        //
        //
        public void LoadRecParamCurrent()
        {
            string sDelay;
            // TODO:  Need to do program to load from file.

            // Set defaults
            recparamSet.strFileBaseName = "image";

            recparamSet.strSaveDirectory = Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Cathreco", "ImageDirectory", @"C:\Temp").ToString();
            //recparamSet.strSaveDirectory = @"C:\Temp";

            recparamSet.fTimeBetweenFrames = 0.5;

            sDelay = Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Cathreco", "Delay1", "4").ToString();
            recparamSet.nDelay1 = Convert.ToInt32(sDelay);

            sDelay = Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Cathreco", "Delay2", "4").ToString();
            recparamSet.nDelay2 = Convert.ToInt32(sDelay);
             
            textBox_ImageDirectory.Text = recparamSet.strSaveDirectory;
            textBox_Delay1.Text = recparamSet.nDelay1.ToString();
            textBox_Delay2.Text = recparamSet.nDelay2.ToString();

        }

        // FUNCTION:  InitCamera:  Initialize the Camera
        //
        //
        private void InitCamera()
        {
            cam = new uEye.Camera();

            uEye.Defines.Status statusRet = 0;

            cam.Size.ImageFormat.Set(7);

            // Open Camera
            statusRet = cam.Init();
            if (statusRet != uEye.Defines.Status.Success)
            {
                MessageBox.Show("Camera initializing failed");
                Environment.Exit(-1);
            }

            // Set Colour Mode
            uEye.Types.SensorInfo SensorInfo;
            statusRet = cam.Information.GetSensorInfo(out SensorInfo);

            if (SensorInfo.SensorColorMode == uEye.Defines.SensorColorMode.Bayer)
            {
                statusRet = cam.PixelFormat.Set(uEye.Defines.ColorMode.BGR8Packed);
            }
            else
            {
                statusRet = cam.PixelFormat.Set(uEye.Defines.ColorMode.Mono8);
                MessageBox.Show("Black and white?!");
                Environment.Exit(-1);
            }

            // Allocate Memory
            statusRet = cam.Memory.Allocate();
            if (statusRet != uEye.Defines.Status.Success)
            {
                MessageBox.Show("Allocate Memory failed");
                Environment.Exit(-1);
            }

            SetCameraParameters();

            // Start Live Video
            statusRet = cam.Acquisition.Capture();
            if (statusRet != uEye.Defines.Status.Success)
            {
                MessageBox.Show("Start Live Video failed");
                Environment.Exit(-1);
            }


            // Connect video to window
            cam.EventFrame += onFrameEvent;

        }

        // FUNCTION:  SetCameraParameter:  Set the camera parameters
        //
        //
        private void SetCameraParameters()
        {
            double f64Min, f64Max, f64Inc;
            //            double f64ExposureSet = 60;
            double f64FrameRateSet = 3;


            // Set Color Format
            uEye.Types.SensorInfo SensorInfo;

            statusRet = cam.Information.GetSensorInfo(out SensorInfo);

            if (SensorInfo.SensorColorMode == uEye.Defines.SensorColorMode.Bayer)
            {
                statusRet = cam.PixelFormat.Set(uEye.Defines.ColorMode.BGR8Packed);
            }
            else
            {
                statusRet = cam.PixelFormat.Set(uEye.Defines.ColorMode.Mono8);
            }


            // Set Frame Rate
            cam.Timing.Framerate.GetFrameRateRange(out f64Min, out f64Max, out f64Inc);

            if (f64Min <= f64FrameRateSet && f64Max >= f64FrameRateSet)
            {
                cam.Timing.Framerate.Set(f64FrameRateSet);
            }
            else
            {
                MessageBox.Show("Can't set frame rate");
                Environment.Exit(-1);
            }


            if (cam.AutoFeatures.Software.Gain.Supported)
            {
                cam.AutoFeatures.Software.Gain.SetEnable(true);
            }
            else
            {
                MessageBox.Show("Auto gain not supported?");
                Environment.Exit(-1);
            }

            // Set Auto White Balance
            if (cam.AutoFeatures.Software.WhiteBalance.Supported)
            {
                cam.AutoFeatures.Software.WhiteBalance.SetEnable(true);
            }
            else
            {
                MessageBox.Show("Auto white balance not supported?");
                Environment.Exit(-1);
            }

            /*   - exposure setting not enabled right now.
            // Set Exposure
            cam.Timing.Exposure.GetRange(out f64Min, out f64Max, out f64Inc);

            if(f64Min <= f64ExposureSet && f64Max >= f64ExposureSet)
            {
                cam.Timing.Exposure.Set(f64ExposureSet);
            }
            else
            {
                MessageBox.Show("Can't set exposure");
                Environment.Exit(-1);
            }
            */
        }

        // FUNCTION:  timer1_Tick: Main tick handler
        //
        //
        private void timer1_Tick(object sender, EventArgs e)
        {
            // we need avoid re-entry when busy.
            if (fRecordingImageToDisk || fReadingImage)
                return;

            // update the current image display
            fReadingImage = true;
            DownloadImage();  // this sets bitmap_Current & bitmap_CurrentRed
            fReadingImage = false;

            // Now execute the state machine
            if (statemach.Step(recparamSet, FCathodeDetected()))
            {
                RecordImage();
                bitmap_LastSavedRed = bitmap_CurrentRed.Clone(rectRed, pixelformat);  // copy the current image to last saved;
                pictBox_LastSaved.Image = bitmap_LastSavedRed;  // and display them both

            }


            // And update the display
            UpdateDisplay();
        }

        // FUNCTION:  DownloadImage: Pull image down from camera
        //
        // In this routine we create two bitmaps: 
        //      bitmap_Current (full size)
        //      bitmap_CurrentRed (reduced for display)
        private void DownloadImage()
        {
            uEye.Defines.Status statusRet = 0;

            // Get last image memory
            Int32 s32LastMemId, s32Width, s32Height;

            // figure out how big the image is
            statusRet = cam.Memory.GetLast(out s32LastMemId);
            statusRet = cam.Memory.Lock(s32LastMemId);
            statusRet = cam.Memory.GetSize(s32LastMemId, out s32Width, out s32Height);

            // copy it to a bitmap
            statusRet = cam.Memory.ToBitmap(s32LastMemId, out bitmap_Current);

            // Scale down the bitmap
            Bitmap bitmap_Temp = new Bitmap(bitmap_Current, pictBox_Current.Width, pictBox_Current.Height);

            // clone it
            bitmap_CurrentRed = bitmap_Temp.Clone(rectRed, pixelformat);

            // Display it
            pictBox_Current.Image = bitmap_CurrentRed;

            // unlock image buffer
            statusRet = cam.Memory.Unlock(s32LastMemId);

            bitmap_Temp.Dispose();
        }

        // FUNCTION: UpdateDisplay:  Update the State Machine Display
        //
        //
        private void UpdateDisplay()
        {
            button_DetectNoCathodeState.BackColor = Color.White;
            button_DetectNoCathodeState.ForeColor = Color.Black;

            button_Delay1State.BackColor = Color.White;
            button_Delay1State.ForeColor = Color.Black;

            button_Delay2State.BackColor = Color.White;
            button_Delay2State.ForeColor = Color.Black;

            button_DetectCathodeState.BackColor = Color.White;
            button_DetectCathodeState.ForeColor = Color.Black;

            button_RecordState.BackColor = Color.White;
            button_RecordState.ForeColor = Color.Black;

            button_Delay2State.BackColor = Color.White;
            button_Delay2State.ForeColor = Color.Black;

            label_Flash.Text = "";

            if (statemach.Current() == Statemachine.rstate.Off)
            {
                button_RecordingFlag.Text = "Recording Off";
                button_RecordingFlag.BackColor = Color.Red;
                button_RecordingFlag.ForeColor = Color.Black;
            }
            else
            {
                button_RecordingFlag.Text = "Recording On";

                if (button_RecordingFlag.BackColor == Color.Yellow)
                    button_RecordingFlag.BackColor = Color.LightGreen;
                else
                    button_RecordingFlag.BackColor = Color.Yellow;

                button_RecordingFlag.ForeColor = Color.Black;
            }

            if (FCathodeDetected())
            {
                button_CathodeDetectedFlag.BackColor = Color.LightGreen;
                button_CathodeDetectedFlag.Text = "Cathode";
            }
            else
            {
                button_CathodeDetectedFlag.BackColor = Color.Red;
                button_CathodeDetectedFlag.Text = "No Cathode";
            }

            // Implement a state progress bar
            switch (statemach.Current())
            {
                case Statemachine.rstate.SeqStart:
                    break;

                case Statemachine.rstate.NoCathodeDetect:
                    button_DetectNoCathodeState.BackColor = Color.Yellow;
                    break;

                case Statemachine.rstate.Delay1:
                    button_DetectNoCathodeState.BackColor = Color.LightGreen;
                    button_Delay1State.BackColor = Color.Yellow;
                    break;

                case Statemachine.rstate.CathodeDetect:
                    button_Delay1State.BackColor = Color.LightGreen;
                    button_DetectNoCathodeState.BackColor = Color.LightGreen;
                    button_DetectCathodeState.BackColor = Color.Yellow;
                    break;

                case Statemachine.rstate.Delay2:
                    button_Delay1State.BackColor = Color.LightGreen;
                    button_DetectNoCathodeState.BackColor = Color.LightGreen;
                    button_DetectCathodeState.BackColor = Color.LightGreen;
                    button_Delay2State.BackColor = Color.Yellow;
                    break;

                case Statemachine.rstate.Record:
                    button_Delay1State.BackColor = Color.LightGreen;
                    button_DetectNoCathodeState.BackColor = Color.LightGreen;
                    button_Delay2State.BackColor = Color.LightGreen;
                    button_DetectCathodeState.BackColor = Color.LightGreen;
                    button_RecordState.BackColor = Color.Yellow;
                    label_Flash.Text = "*";
                    break;

                case Statemachine.rstate.Off:
                    break;

                default:
                    break;
            }

        }

        // FUNCTION:  RecordImage:  Record a Single Photo to Disk
        //
        //
        private void RecordImage()
        {
            string str_FileName, str_TimeStamp;

            ImageCodecInfo imagecodecinfo;
            System.Drawing.Imaging.Encoder encoder;
            EncoderParameter encoderparam;
            EncoderParameters rgencoderparams;

            fRecordingImageToDisk = true;

            // Save it using current time stamp as part of file name
            str_TimeStamp = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");
            str_FileName = recparamSet.strSaveDirectory + @"\" +
                            recparamSet.strFileBaseName + " " +
                            str_TimeStamp + ".jpg";

            // Get an ImageCodecInfo object that represents the JPEG codec.
            imagecodecinfo = GetEncoderInfo("image/jpeg");

            // Create an Encoder object based on the GUID
            // for the Quality parameter category.
            encoder = System.Drawing.Imaging.Encoder.Quality;

            // Create an EncoderParameters object.
            // An EncoderParameters object has an array of EncoderParameter
            // objects. In this case, there is only one EncoderParameter object in the array.
            rgencoderparams = new EncoderParameters(1);

            // Save the bitmap as a JPEG file with quality level 75.
            encoderparam = new EncoderParameter(encoder, 75L);
            rgencoderparams.Param[0] = encoderparam;

            bitmap_Current.Save(str_FileName, imagecodecinfo, rgencoderparams);

            // Update dialog box
            label_LastFileName.Text = str_FileName;
            nImagesRecorded++;
            label_ImagesRecorded.Text = nImagesRecorded.ToString();

            fRecordingImageToDisk = false;

        }

        // FUNCTION:  GetEncoderInfo:  Pull down the set of image encoders
        //
        // Needed to save jpeg
        //
        //
        private ImageCodecInfo GetEncoderInfo(String mimeType)
        {
            int j;
            ImageCodecInfo[] encoders;
            encoders = ImageCodecInfo.GetImageEncoders();
            for (j = 0; j < encoders.Length; ++j)
            {
                if (encoders[j].MimeType == mimeType)
                    return encoders[j];
            }
            return null;
        }

        // FUNCTION:  FCathodeDetected:  Detect if copper is visible in the current image window:  TODO
        //
        //
        private bool FCathodeDetected()
        {
            if (checkBox_Bypass.Checked)
            {
                if (fManualCathodeSense)
                    return (true);
            }

            //AduSetOutState(0);

            return (AduGetInputState(0, 0)); // Return input state
        }

        // FUNCTION:  buttonTakePic_Click:  Record a Single Photo Button
        //
        //
        private void buttonTakePic_Click(object sender, EventArgs e)
        {
            RecordImage();  // save to file

            bitmap_LastSavedRed = bitmap_CurrentRed.Clone(rectRed, pixelformat);  // copy the current image to last saved;

            // and display them both
            pictBox_LastSaved.Image = bitmap_LastSavedRed;

        }

        // FUNCTION:  buttonStartRecord_Click:  Start Recording
        //
        //
        // Recording Timer Control
        //
        //  This sequences through a set of delays and detection events
        //  that determine when to take a photo.
        //
        //
        private void buttonStartRecord_Click(object sender, EventArgs e)
        {
            statemach.Start();
        }

        // FUNCTION:  buttonStopRecord_Click:  Stop Recording
        //
        //
        private void buttonStopRecord_Click(object sender, EventArgs e)
        {
            statemach.Reset();   // Reset state machine
        }

        // FUNCTION:  button_ManualCathodeSense_MouseDown:  Set manual cathode sense flag
        //
        //
        private void button_ManualCathodeSense_MouseDown(object sender, MouseEventArgs e)
        {
            fManualCathodeSense = true;
            if (checkBox_Bypass.Checked)
            {
                button_CathodeDetectedFlag.BackColor = Color.LightGreen;
                button_CathodeDetectedFlag.Text = "Cathode";
            }
        }

        // FUNCTION:  button_ManualCathodeSense_MouseUp:  Set manual cathode sense flag
        //
        //
        private void button_ManualCathodeSense_MouseUp(object sender, MouseEventArgs e)
        {
            fManualCathodeSense = false;

            if (checkBox_Bypass.Checked)
            {
                button_CathodeDetectedFlag.BackColor = Color.Red;
                button_CathodeDetectedFlag.Text = "No Cathode";
            }

        }

        //  CLASS: Statemachine: Recording Sequence State Machine
        //
        //  This sequences through a set of delays and detection events
        //  that determine when to take a photo.
        //
        //  Sequence is as follows
        //
        //  Seqstart - entry point
        //  NoCathodeDetect - awaiting time with no cathode visible
        //  Delay1 - opening delay
        //  CathodeDetect - awaiting a cathode to be visible
        //  Delay2 - wait for cathode to move into position
        //  Record - record a photo
        //
        //  returns true if calling routine is to record an image.  
        //  returns false otherwise
        private class Statemachine
        {
            // Setup state definitions 
            public enum rstate { SeqStart, NoCathodeDetect, Delay1, CathodeDetect, Delay2, Record, Off };
            public rstate rstateCur;   //Current recording state

            // Define timing delay counters.  One associated with each delay
            private int cDelay1, cDelay2;

            // FUNCTION: Statemachine:  Constructor.  Resets state machine
            //
            //
            //
            public Statemachine()
            {
                Reset();
            }

            // FUNCTION: Reset:  Reset the State Machine
            //
            // Reset state and state timing counters.
            //
            //
            public void Reset()
            {
                rstateCur = rstate.Off;
                cDelay1 = 0;
                cDelay2 = 0;
            }

            // FUNCTION: Current:  Returns the Current State
            //
            //
            public rstate Current()
            {
                return (rstateCur);
            }

            // FUNCTION: Start:  Starts the Sequence
            //
            //
            public void Start()
            {
                Reset();
                rstateCur = rstate.SeqStart;
            }

            // FUNCTION: Step:  Sequencer the State Machine
            //
            //
            public bool Step(RecParam recparam, bool fCathodeDetected)
            {
                bool fRecord = false;

                switch (rstateCur)
                {
                    // Start of sequence
                    case rstate.SeqStart:
                        cDelay1 = 2;
                        cDelay2 = 2;
                        rstateCur = rstate.NoCathodeDetect;
                        break;

                    // Waiot for cathode
                    case rstate.NoCathodeDetect:

                        if (!fCathodeDetected)  // Does sensor see a cathode?
                            rstateCur = rstate.Delay1;  // no - go to next state
                        break;


                    // First delay
                    case rstate.Delay1:
                        // Check delay counter
                        if (cDelay1 >= recparam.nDelay1)  // Counter passed require time?
                            rstateCur = rstate.CathodeDetect;  // yes - go to next state
                        else
                            cDelay1++;  // nope - increment
                        break;

                    // Wait for motion
                    case rstate.CathodeDetect:

                        if (fCathodeDetected)  // Does sensor see a cathode?
                            rstateCur = rstate.Delay2;  // no - go to next state
                        break;

                    case rstate.Delay2:

                        // Check delay counter
                        if (cDelay2 >= recparam.nDelay2)  // Counter passed require time?
                            rstateCur = rstate.Record;  // yes - go to next state
                        else
                            cDelay2++;  // nope - increment
                        break;


                    case rstate.Record:

                        // Record the image
                        rstateCur = rstate.SeqStart;
                        fRecord = true;

                        break;

                    default:
                        //MessageBox.Show("State Machine Failure", "Goddabug");
                        //System.Diagnostics.Debugger.Break();
                        break;
                }

                return (fRecord);

            }

        }

        // FUNCTION:  OpenADU:  Open the ADU208 I/O Device
        //
        //
        private void AduOpen()
        {
            pAduIO = OpenAduDevice((UInt32)500);
            if ((uint)pAduIO == 0)
            {
                MessageBox.Show("Could not open ADU");
            }

        }

        // FUNCTION:  CloseADU:  Close the ADU208 I/O Device
        //
        //
        private void AduClose()
        {
            CloseAduDevice(pAduIO);
        }

        // FUNCTION:  AduGetInputState:  Get state of an input
        //            returns true or false
        //
        //            Port 0 = Port A
        //            Port 1 = Port B
        //            Port 2 = Pork K
        //
        private bool AduGetInputState(int nPortNum, int nInputNum)
        {
            bool bRC = false;
            uint uiWritten = 0xdead;
            string sAduCmd = "XXXX";
            uint nAduCmdLength;
            StringBuilder sBuffer = new StringBuilder(8);
            uint nBufferLength = 7;
            uint uiRead = 0;
            bool fInputState;


            if (nPortNum == 0)
                sAduCmd = "RPA" + nInputNum.ToString();
            if (nPortNum == 1)
                sAduCmd = "RPB" + nInputNum.ToString();
            if (nPortNum == 2)
                sAduCmd = "RPK" + nInputNum.ToString();

            nAduCmdLength = (uint)sAduCmd.Length;

            bRC = WriteAduDevice(pAduIO, sAduCmd, nAduCmdLength, out uiWritten, 500);  // request input status
            if (!bRC)
            {
                MessageBox.Show("Could not write to ADU");
                return (false);
            }

            bRC = ReadAduDevice(pAduIO, sBuffer, nBufferLength, out uiRead, 500);  // read input status

            if (!bRC)
            {
                MessageBox.Show("Could not read from ADU");
                return (false);
            }


            fInputState = (sBuffer.ToString()).StartsWith("1");

            return (fInputState);

        }

        // FUNCTION:  AduSetOutState:  Set state of an input
        //
        //            Only functions on Port K
        //
        private void AduSetOutState(int nOutputNum)
        {
            bool bRC = false;
            uint uiWritten = 0xdead;
            string sAduCmd;
            uint nAduCmdLength;
            StringBuilder sBuffer = new StringBuilder(8);

            sAduCmd = "SK" + nOutputNum.ToString();

            nAduCmdLength = (uint)sAduCmd.Length;

            bRC = WriteAduDevice(pAduIO, sAduCmd, nAduCmdLength, out uiWritten, 500);  // request input status
            if (!bRC)
            {
                MessageBox.Show("Could not write to ADU");
            }
        }
    }
}


