namespace CathRecorderMain
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.buttonRecordOne = new System.Windows.Forms.Button();
            this.pictBox_Current = new System.Windows.Forms.PictureBox();
            this.buttonStartRecord = new System.Windows.Forms.Button();
            this.buttonStopRecord = new System.Windows.Forms.Button();
            this.pictBox_LastSaved = new System.Windows.Forms.PictureBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.button_DetectNoCathodeState = new System.Windows.Forms.Button();
            this.button_Delay1State = new System.Windows.Forms.Button();
            this.button_RecordState = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.button_RecordingFlag = new System.Windows.Forms.Button();
            this.button_DetectCathodeState = new System.Windows.Forms.Button();
            this.button_Delay2State = new System.Windows.Forms.Button();
            this.button_CathodeDetectedFlag = new System.Windows.Forms.Button();
            this.checkBox_Bypass = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button_ManualCathodeSense = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label_Flash = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label_LastFileName = new System.Windows.Forms.Label();
            this.label_ImagesRecorded = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.textBox_ImageDirectory = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.textBox_Delay1 = new System.Windows.Forms.TextBox();
            this.textBox_Delay2 = new System.Windows.Forms.TextBox();
            this.button_SaveSettings = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictBox_Current)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictBox_LastSaved)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonRecordOne
            // 
            this.buttonRecordOne.Location = new System.Drawing.Point(445, 102);
            this.buttonRecordOne.Name = "buttonRecordOne";
            this.buttonRecordOne.Size = new System.Drawing.Size(73, 34);
            this.buttonRecordOne.TabIndex = 0;
            this.buttonRecordOne.Text = "Record One";
            this.buttonRecordOne.UseVisualStyleBackColor = true;
            this.buttonRecordOne.Click += new System.EventHandler(this.buttonTakePic_Click);
            // 
            // pictBox_Current
            // 
            this.pictBox_Current.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictBox_Current.Location = new System.Drawing.Point(181, 20);
            this.pictBox_Current.Name = "pictBox_Current";
            this.pictBox_Current.Size = new System.Drawing.Size(160, 120);
            this.pictBox_Current.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictBox_Current.TabIndex = 2;
            this.pictBox_Current.TabStop = false;
            // 
            // buttonStartRecord
            // 
            this.buttonStartRecord.Location = new System.Drawing.Point(445, 20);
            this.buttonStartRecord.Name = "buttonStartRecord";
            this.buttonStartRecord.Size = new System.Drawing.Size(73, 35);
            this.buttonStartRecord.TabIndex = 3;
            this.buttonStartRecord.Text = "Start Recording";
            this.buttonStartRecord.UseVisualStyleBackColor = true;
            this.buttonStartRecord.Click += new System.EventHandler(this.buttonStartRecord_Click);
            // 
            // buttonStopRecord
            // 
            this.buttonStopRecord.Location = new System.Drawing.Point(445, 61);
            this.buttonStopRecord.Name = "buttonStopRecord";
            this.buttonStopRecord.Size = new System.Drawing.Size(73, 35);
            this.buttonStopRecord.TabIndex = 4;
            this.buttonStopRecord.Text = "Stop Recording";
            this.buttonStopRecord.UseVisualStyleBackColor = true;
            this.buttonStopRecord.Click += new System.EventHandler(this.buttonStopRecord_Click);
            // 
            // pictBox_LastSaved
            // 
            this.pictBox_LastSaved.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictBox_LastSaved.Location = new System.Drawing.Point(9, 20);
            this.pictBox_LastSaved.Name = "pictBox_LastSaved";
            this.pictBox_LastSaved.Size = new System.Drawing.Size(160, 120);
            this.pictBox_LastSaved.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictBox_LastSaved.TabIndex = 15;
            this.pictBox_LastSaved.TabStop = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(224, 5);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(73, 13);
            this.label5.TabIndex = 17;
            this.label5.Text = "Current Image";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(33, 4);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(113, 13);
            this.label6.TabIndex = 18;
            this.label6.Text = "Last Recorded to Disk";
            // 
            // button_DetectNoCathodeState
            // 
            this.button_DetectNoCathodeState.Location = new System.Drawing.Point(9, 187);
            this.button_DetectNoCathodeState.Name = "button_DetectNoCathodeState";
            this.button_DetectNoCathodeState.Size = new System.Drawing.Size(65, 35);
            this.button_DetectNoCathodeState.TabIndex = 29;
            this.button_DetectNoCathodeState.Text = "Wait No Cathode";
            this.button_DetectNoCathodeState.UseVisualStyleBackColor = true;
            // 
            // button_Delay1State
            // 
            this.button_Delay1State.Location = new System.Drawing.Point(75, 187);
            this.button_Delay1State.Name = "button_Delay1State";
            this.button_Delay1State.Size = new System.Drawing.Size(65, 35);
            this.button_Delay1State.TabIndex = 30;
            this.button_Delay1State.Text = "Delay 1";
            this.button_Delay1State.UseVisualStyleBackColor = true;
            // 
            // button_RecordState
            // 
            this.button_RecordState.Location = new System.Drawing.Point(276, 187);
            this.button_RecordState.Name = "button_RecordState";
            this.button_RecordState.Size = new System.Drawing.Size(65, 35);
            this.button_RecordState.TabIndex = 33;
            this.button_RecordState.Text = "Record!";
            this.button_RecordState.UseVisualStyleBackColor = true;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 500;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // button_RecordingFlag
            // 
            this.button_RecordingFlag.Location = new System.Drawing.Point(445, 187);
            this.button_RecordingFlag.Name = "button_RecordingFlag";
            this.button_RecordingFlag.Size = new System.Drawing.Size(73, 35);
            this.button_RecordingFlag.TabIndex = 37;
            this.button_RecordingFlag.Text = "Recording Off";
            this.button_RecordingFlag.UseVisualStyleBackColor = true;
            // 
            // button_DetectCathodeState
            // 
            this.button_DetectCathodeState.Location = new System.Drawing.Point(142, 187);
            this.button_DetectCathodeState.Name = "button_DetectCathodeState";
            this.button_DetectCathodeState.Size = new System.Drawing.Size(65, 35);
            this.button_DetectCathodeState.TabIndex = 32;
            this.button_DetectCathodeState.Text = "Wait Cathode";
            this.button_DetectCathodeState.UseVisualStyleBackColor = true;
            // 
            // button_Delay2State
            // 
            this.button_Delay2State.Location = new System.Drawing.Point(209, 187);
            this.button_Delay2State.Name = "button_Delay2State";
            this.button_Delay2State.Size = new System.Drawing.Size(65, 35);
            this.button_Delay2State.TabIndex = 31;
            this.button_Delay2State.Text = "Delay 2";
            this.button_Delay2State.UseVisualStyleBackColor = true;
            // 
            // button_CathodeDetectedFlag
            // 
            this.button_CathodeDetectedFlag.Location = new System.Drawing.Point(355, 21);
            this.button_CathodeDetectedFlag.Name = "button_CathodeDetectedFlag";
            this.button_CathodeDetectedFlag.Size = new System.Drawing.Size(65, 34);
            this.button_CathodeDetectedFlag.TabIndex = 38;
            this.button_CathodeDetectedFlag.UseVisualStyleBackColor = true;
            // 
            // checkBox_Bypass
            // 
            this.checkBox_Bypass.AutoSize = true;
            this.checkBox_Bypass.Location = new System.Drawing.Point(360, 68);
            this.checkBox_Bypass.Name = "checkBox_Bypass";
            this.checkBox_Bypass.Size = new System.Drawing.Size(60, 17);
            this.checkBox_Bypass.TabIndex = 39;
            this.checkBox_Bypass.Text = "Bypass";
            this.checkBox_Bypass.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(349, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 13);
            this.label1.TabIndex = 40;
            this.label1.Text = "Cathode Sense";
            // 
            // button_ManualCathodeSense
            // 
            this.button_ManualCathodeSense.Location = new System.Drawing.Point(355, 102);
            this.button_ManualCathodeSense.Name = "button_ManualCathodeSense";
            this.button_ManualCathodeSense.Size = new System.Drawing.Size(65, 34);
            this.button_ManualCathodeSense.TabIndex = 41;
            this.button_ManualCathodeSense.Text = "Manual Sense";
            this.button_ManualCathodeSense.UseVisualStyleBackColor = true;
            this.button_ManualCathodeSense.MouseDown += new System.Windows.Forms.MouseEventHandler(this.button_ManualCathodeSense_MouseDown);
            this.button_ManualCathodeSense.MouseUp += new System.Windows.Forms.MouseEventHandler(this.button_ManualCathodeSense_MouseUp);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 171);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 13);
            this.label2.TabIndex = 42;
            this.label2.Text = "Recording Cycle";
            // 
            // label_Flash
            // 
            this.label_Flash.AutoSize = true;
            this.label_Flash.Font = new System.Drawing.Font("Arial", 40F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_Flash.ForeColor = System.Drawing.Color.Red;
            this.label_Flash.Location = new System.Drawing.Point(331, 184);
            this.label_Flash.Name = "label_Flash";
            this.label_Flash.Size = new System.Drawing.Size(48, 63);
            this.label_Flash.TabIndex = 43;
            this.label_Flash.Text = "*";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(145, 148);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 13);
            this.label3.TabIndex = 44;
            this.label3.Text = "Last File:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 148);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(94, 13);
            this.label4.TabIndex = 45;
            this.label4.Text = "Images Recorded:";
            // 
            // label_LastFileName
            // 
            this.label_LastFileName.AutoSize = true;
            this.label_LastFileName.Location = new System.Drawing.Point(191, 148);
            this.label_LastFileName.Name = "label_LastFileName";
            this.label_LastFileName.Size = new System.Drawing.Size(83, 13);
            this.label_LastFileName.TabIndex = 46;
            this.label_LastFileName.Text = "None Recorded";
            // 
            // label_ImagesRecorded
            // 
            this.label_ImagesRecorded.AutoSize = true;
            this.label_ImagesRecorded.Location = new System.Drawing.Point(98, 148);
            this.label_ImagesRecorded.Name = "label_ImagesRecorded";
            this.label_ImagesRecorded.Size = new System.Drawing.Size(13, 13);
            this.label_ImagesRecorded.TabIndex = 47;
            this.label_ImagesRecorded.Text = "0";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 232);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(48, 13);
            this.label7.TabIndex = 48;
            this.label7.Text = "Settings:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 254);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(84, 13);
            this.label8.TabIndex = 49;
            this.label8.Text = "Image Directory:";
            // 
            // textBox_ImageDirectory
            // 
            this.textBox_ImageDirectory.Location = new System.Drawing.Point(95, 251);
            this.textBox_ImageDirectory.Name = "textBox_ImageDirectory";
            this.textBox_ImageDirectory.Size = new System.Drawing.Size(177, 20);
            this.textBox_ImageDirectory.TabIndex = 50;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(276, 254);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(46, 13);
            this.label9.TabIndex = 51;
            this.label9.Text = "Delay 1:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(352, 254);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(46, 13);
            this.label10.TabIndex = 52;
            this.label10.Text = "Delay 2:";
            // 
            // textBox_Delay1
            // 
            this.textBox_Delay1.Location = new System.Drawing.Point(320, 251);
            this.textBox_Delay1.Name = "textBox_Delay1";
            this.textBox_Delay1.Size = new System.Drawing.Size(30, 20);
            this.textBox_Delay1.TabIndex = 53;
            // 
            // textBox_Delay2
            // 
            this.textBox_Delay2.Location = new System.Drawing.Point(398, 250);
            this.textBox_Delay2.Name = "textBox_Delay2";
            this.textBox_Delay2.Size = new System.Drawing.Size(30, 20);
            this.textBox_Delay2.TabIndex = 54;
            // 
            // button_SaveSettings
            // 
            this.button_SaveSettings.Location = new System.Drawing.Point(445, 242);
            this.button_SaveSettings.Name = "button_SaveSettings";
            this.button_SaveSettings.Size = new System.Drawing.Size(73, 34);
            this.button_SaveSettings.TabIndex = 55;
            this.button_SaveSettings.Text = "Save Settings";
            this.button_SaveSettings.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(530, 282);
            this.Controls.Add(this.button_SaveSettings);
            this.Controls.Add(this.textBox_Delay2);
            this.Controls.Add(this.textBox_Delay1);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.textBox_ImageDirectory);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label_ImagesRecorded);
            this.Controls.Add(this.label_LastFileName);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button_ManualCathodeSense);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.checkBox_Bypass);
            this.Controls.Add(this.button_CathodeDetectedFlag);
            this.Controls.Add(this.button_RecordingFlag);
            this.Controls.Add(this.button_RecordState);
            this.Controls.Add(this.button_DetectCathodeState);
            this.Controls.Add(this.button_Delay2State);
            this.Controls.Add(this.button_Delay1State);
            this.Controls.Add(this.button_DetectNoCathodeState);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.pictBox_LastSaved);
            this.Controls.Add(this.buttonStopRecord);
            this.Controls.Add(this.buttonStartRecord);
            this.Controls.Add(this.pictBox_Current);
            this.Controls.Add(this.buttonRecordOne);
            this.Controls.Add(this.label_Flash);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cathode Image Recorder";
            ((System.ComponentModel.ISupportInitialize)(this.pictBox_Current)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictBox_LastSaved)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonRecordOne;
        private System.Windows.Forms.PictureBox pictBox_Current;
        private System.Windows.Forms.Button buttonStartRecord;
        private System.Windows.Forms.Button buttonStopRecord;
        private System.Windows.Forms.PictureBox pictBox_LastSaved;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button button_DetectNoCathodeState;
        private System.Windows.Forms.Button button_Delay1State;
        private System.Windows.Forms.Button button_RecordState;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button button_RecordingFlag;
        private System.Windows.Forms.Button button_DetectCathodeState;
        private System.Windows.Forms.Button button_Delay2State;
        private System.Windows.Forms.Button button_CathodeDetectedFlag;
        private System.Windows.Forms.CheckBox checkBox_Bypass;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button_ManualCathodeSense;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label_Flash;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label_LastFileName;
        private System.Windows.Forms.Label label_ImagesRecorded;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBox_ImageDirectory;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox textBox_Delay1;
        private System.Windows.Forms.TextBox textBox_Delay2;
        private System.Windows.Forms.Button button_SaveSettings;
    }
}

