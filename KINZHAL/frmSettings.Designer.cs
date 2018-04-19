namespace KINZHAL
{
    partial class frmSettings
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnSaveClose = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.numCoefSensVert = new System.Windows.Forms.NumericUpDown();
            this.numCoefKrutVert = new System.Windows.Forms.NumericUpDown();
            this.numCoefSensHor = new System.Windows.Forms.NumericUpDown();
            this.numCoefKrutHor = new System.Windows.Forms.NumericUpDown();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chkFullMode = new System.Windows.Forms.CheckBox();
            this.lblFPSTotal = new System.Windows.Forms.Label();
            this.lblFPS_Received = new System.Windows.Forms.Label();
            this.lstBoxSizeVideo = new System.Windows.Forms.ListBox();
            this.lstBoxVideoInputs = new System.Windows.Forms.ListBox();
            this.btnVideoDeviceDisconnect = new System.Windows.Forms.Button();
            this.btnVideoDeviceConnect = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbDevices = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numCoefSensVert)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCoefKrutVert)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCoefSensHor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCoefKrutHor)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnSaveClose);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.numCoefSensVert);
            this.groupBox1.Controls.Add(this.numCoefKrutVert);
            this.groupBox1.Controls.Add(this.numCoefSensHor);
            this.groupBox1.Controls.Add(this.numCoefKrutHor);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(613, 121);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Параметры джойстика";
            // 
            // btnSaveClose
            // 
            this.btnSaveClose.Location = new System.Drawing.Point(482, 86);
            this.btnSaveClose.Name = "btnSaveClose";
            this.btnSaveClose.Size = new System.Drawing.Size(114, 23);
            this.btnSaveClose.TabIndex = 2;
            this.btnSaveClose.Text = "Закрыть";
            this.btnSaveClose.UseVisualStyleBackColor = true;
            this.btnSaveClose.Click += new System.EventHandler(this.btnSaveClose_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(336, 47);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(191, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Порог чувствительности, вертикаль";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(187, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Коэффициент крутизны, вертикаль";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(343, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(184, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Порог чувствительности, горизонт";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(180, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Коэффициент крутизны, горизонт";
            // 
            // numCoefSensVert
            // 
            this.numCoefSensVert.Location = new System.Drawing.Point(533, 45);
            this.numCoefSensVert.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numCoefSensVert.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numCoefSensVert.Name = "numCoefSensVert";
            this.numCoefSensVert.Size = new System.Drawing.Size(63, 20);
            this.numCoefSensVert.TabIndex = 0;
            this.numCoefSensVert.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numCoefSensVert.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            // 
            // numCoefKrutVert
            // 
            this.numCoefKrutVert.DecimalPlaces = 2;
            this.numCoefKrutVert.Increment = new decimal(new int[] {
            5,
            0,
            0,
            131072});
            this.numCoefKrutVert.Location = new System.Drawing.Point(212, 45);
            this.numCoefKrutVert.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numCoefKrutVert.Name = "numCoefKrutVert";
            this.numCoefKrutVert.Size = new System.Drawing.Size(63, 20);
            this.numCoefKrutVert.TabIndex = 0;
            this.numCoefKrutVert.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numCoefKrutVert.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // numCoefSensHor
            // 
            this.numCoefSensHor.Location = new System.Drawing.Point(533, 19);
            this.numCoefSensHor.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numCoefSensHor.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numCoefSensHor.Name = "numCoefSensHor";
            this.numCoefSensHor.Size = new System.Drawing.Size(63, 20);
            this.numCoefSensHor.TabIndex = 0;
            this.numCoefSensHor.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numCoefSensHor.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            // 
            // numCoefKrutHor
            // 
            this.numCoefKrutHor.DecimalPlaces = 2;
            this.numCoefKrutHor.Increment = new decimal(new int[] {
            5,
            0,
            0,
            131072});
            this.numCoefKrutHor.Location = new System.Drawing.Point(212, 19);
            this.numCoefKrutHor.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numCoefKrutHor.Name = "numCoefKrutHor";
            this.numCoefKrutHor.Size = new System.Drawing.Size(63, 20);
            this.numCoefKrutHor.TabIndex = 0;
            this.numCoefKrutHor.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numCoefKrutHor.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.chkFullMode);
            this.groupBox2.Controls.Add(this.lblFPSTotal);
            this.groupBox2.Controls.Add(this.lblFPS_Received);
            this.groupBox2.Controls.Add(this.lstBoxSizeVideo);
            this.groupBox2.Controls.Add(this.lstBoxVideoInputs);
            this.groupBox2.Controls.Add(this.btnVideoDeviceDisconnect);
            this.groupBox2.Controls.Add(this.btnVideoDeviceConnect);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.cmbDevices);
            this.groupBox2.Location = new System.Drawing.Point(12, 139);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(613, 148);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Захват видео";
            // 
            // chkFullMode
            // 
            this.chkFullMode.AutoSize = true;
            this.chkFullMode.Location = new System.Drawing.Point(356, 79);
            this.chkFullMode.Name = "chkFullMode";
            this.chkFullMode.Size = new System.Drawing.Size(100, 17);
            this.chkFullMode.TabIndex = 34;
            this.chkFullMode.Text = "На весь экран";
            this.chkFullMode.UseVisualStyleBackColor = true;
            this.chkFullMode.CheckedChanged += new System.EventHandler(this.chkFullMode_CheckedChanged);
            // 
            // lblFPSTotal
            // 
            this.lblFPSTotal.AutoSize = true;
            this.lblFPSTotal.BackColor = System.Drawing.SystemColors.Control;
            this.lblFPSTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblFPSTotal.ForeColor = System.Drawing.Color.Black;
            this.lblFPSTotal.Location = new System.Drawing.Point(18, 115);
            this.lblFPSTotal.Name = "lblFPSTotal";
            this.lblFPSTotal.Size = new System.Drawing.Size(44, 20);
            this.lblFPSTotal.TabIndex = 32;
            this.lblFPSTotal.Text = "FPS:";
            // 
            // lblFPS_Received
            // 
            this.lblFPS_Received.AutoSize = true;
            this.lblFPS_Received.BackColor = System.Drawing.SystemColors.Control;
            this.lblFPS_Received.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblFPS_Received.ForeColor = System.Drawing.Color.Black;
            this.lblFPS_Received.Location = new System.Drawing.Point(18, 90);
            this.lblFPS_Received.Name = "lblFPS_Received";
            this.lblFPS_Received.Size = new System.Drawing.Size(44, 20);
            this.lblFPS_Received.TabIndex = 33;
            this.lblFPS_Received.Text = "FPS:";
            // 
            // lstBoxSizeVideo
            // 
            this.lstBoxSizeVideo.FormattingEnabled = true;
            this.lstBoxSizeVideo.Location = new System.Drawing.Point(482, 30);
            this.lstBoxSizeVideo.Name = "lstBoxSizeVideo";
            this.lstBoxSizeVideo.Size = new System.Drawing.Size(120, 56);
            this.lstBoxSizeVideo.TabIndex = 4;
            // 
            // lstBoxVideoInputs
            // 
            this.lstBoxVideoInputs.FormattingEnabled = true;
            this.lstBoxVideoInputs.Location = new System.Drawing.Point(105, 57);
            this.lstBoxVideoInputs.Name = "lstBoxVideoInputs";
            this.lstBoxVideoInputs.Size = new System.Drawing.Size(170, 30);
            this.lstBoxVideoInputs.TabIndex = 3;
            // 
            // btnVideoDeviceDisconnect
            // 
            this.btnVideoDeviceDisconnect.Enabled = false;
            this.btnVideoDeviceDisconnect.Location = new System.Drawing.Point(482, 102);
            this.btnVideoDeviceDisconnect.Name = "btnVideoDeviceDisconnect";
            this.btnVideoDeviceDisconnect.Size = new System.Drawing.Size(120, 23);
            this.btnVideoDeviceDisconnect.TabIndex = 2;
            this.btnVideoDeviceDisconnect.Text = "Отключиться";
            this.btnVideoDeviceDisconnect.UseVisualStyleBackColor = true;
            this.btnVideoDeviceDisconnect.Click += new System.EventHandler(this.btnVideoDeviceDisconnect_Click);
            // 
            // btnVideoDeviceConnect
            // 
            this.btnVideoDeviceConnect.Location = new System.Drawing.Point(356, 102);
            this.btnVideoDeviceConnect.Name = "btnVideoDeviceConnect";
            this.btnVideoDeviceConnect.Size = new System.Drawing.Size(120, 23);
            this.btnVideoDeviceConnect.TabIndex = 2;
            this.btnVideoDeviceConnect.Text = "Подключиться";
            this.btnVideoDeviceConnect.UseVisualStyleBackColor = true;
            this.btnVideoDeviceConnect.Click += new System.EventHandler(this.btnVideoDeviceConnect_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(52, 57);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(47, 13);
            this.label6.TabIndex = 1;
            this.label6.Text = "Выходы";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(312, 33);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(164, 13);
            this.label7.TabIndex = 1;
            this.label7.Text = "Поддерживаемые разрешения";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(32, 33);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(67, 13);
            this.label5.TabIndex = 1;
            this.label5.Text = "Устройство";
            // 
            // cmbDevices
            // 
            this.cmbDevices.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDevices.FormattingEnabled = true;
            this.cmbDevices.Location = new System.Drawing.Point(105, 30);
            this.cmbDevices.Name = "cmbDevices";
            this.cmbDevices.Size = new System.Drawing.Size(170, 21);
            this.cmbDevices.TabIndex = 0;
            this.cmbDevices.SelectedIndexChanged += new System.EventHandler(this.cmbDevices_SelectedIndexChanged);
            // 
            // frmSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(637, 297);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "frmSettings";
            this.Text = "Настройки";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmSettings_FormClosing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmSettings_KeyDown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numCoefSensVert)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCoefKrutVert)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCoefSensHor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCoefKrutHor)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnSaveClose;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.NumericUpDown numCoefSensVert;
        public System.Windows.Forms.NumericUpDown numCoefKrutVert;
        public System.Windows.Forms.NumericUpDown numCoefSensHor;
        public System.Windows.Forms.NumericUpDown numCoefKrutHor;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbDevices;
        private System.Windows.Forms.Button btnVideoDeviceConnect;
        private System.Windows.Forms.ListBox lstBoxSizeVideo;
        private System.Windows.Forms.ListBox lstBoxVideoInputs;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnVideoDeviceDisconnect;
        public System.Windows.Forms.Label lblFPSTotal;
        public System.Windows.Forms.Label lblFPS_Received;
        private System.Windows.Forms.CheckBox chkFullMode;
    }
}