namespace POP3Pipe
{
    partial class ConnectionWindow
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
            this.btnOk = new System.Windows.Forms.Button();
            this.comboPOP3 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblTimesPerHour = new System.Windows.Forms.Label();
            this.numericTimesPerHour = new System.Windows.Forms.NumericUpDown();
            this.lblMinutes = new System.Windows.Forms.Label();
            this.numericMinutes = new System.Windows.Forms.NumericUpDown();
            this.lblSeconds = new System.Windows.Forms.Label();
            this.lblHours = new System.Windows.Forms.Label();
            this.numericHours = new System.Windows.Forms.NumericUpDown();
            this.numericSeconds = new System.Windows.Forms.NumericUpDown();
            this.checkBoxCycling = new System.Windows.Forms.CheckBox();
            this.groupPOP3 = new System.Windows.Forms.GroupBox();
            this.groupSMTP = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.comboSMTP = new System.Windows.Forms.ComboBox();
            this.groupEMailAddress = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.comboAddress = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericTimesPerHour)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericMinutes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericHours)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericSeconds)).BeginInit();
            this.groupPOP3.SuspendLayout();
            this.groupSMTP.SuspendLayout();
            this.groupEMailAddress.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(454, 39);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(21, 135);
            this.btnOk.TabIndex = 14;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // comboPOP3
            // 
            this.comboPOP3.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboPOP3.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboPOP3.FormattingEnabled = true;
            this.comboPOP3.Location = new System.Drawing.Point(102, 17);
            this.comboPOP3.Name = "comboPOP3";
            this.comboPOP3.Size = new System.Drawing.Size(152, 21);
            this.comboPOP3.TabIndex = 16;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 17;
            this.label1.Text = "POP3 Host:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblTimesPerHour);
            this.groupBox1.Controls.Add(this.numericTimesPerHour);
            this.groupBox1.Controls.Add(this.lblMinutes);
            this.groupBox1.Controls.Add(this.numericMinutes);
            this.groupBox1.Controls.Add(this.lblSeconds);
            this.groupBox1.Controls.Add(this.lblHours);
            this.groupBox1.Controls.Add(this.numericHours);
            this.groupBox1.Controls.Add(this.numericSeconds);
            this.groupBox1.Enabled = false;
            this.groupBox1.Location = new System.Drawing.Point(285, 35);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(161, 139);
            this.groupBox1.TabIndex = 22;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Automatic Process";
            // 
            // lblTimesPerHour
            // 
            this.lblTimesPerHour.AutoSize = true;
            this.lblTimesPerHour.Location = new System.Drawing.Point(75, 105);
            this.lblTimesPerHour.Name = "lblTimesPerHour";
            this.lblTimesPerHour.Size = new System.Drawing.Size(79, 13);
            this.lblTimesPerHour.TabIndex = 20;
            this.lblTimesPerHour.Text = "Times per Hour";
            // 
            // numericTimesPerHour
            // 
            this.numericTimesPerHour.Location = new System.Drawing.Point(13, 102);
            this.numericTimesPerHour.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.numericTimesPerHour.Name = "numericTimesPerHour";
            this.numericTimesPerHour.Size = new System.Drawing.Size(62, 20);
            this.numericTimesPerHour.TabIndex = 19;
            this.numericTimesPerHour.ValueChanged += new System.EventHandler(this.numericField_ValueChanged);
            // 
            // lblMinutes
            // 
            this.lblMinutes.AutoSize = true;
            this.lblMinutes.Location = new System.Drawing.Point(83, 49);
            this.lblMinutes.Name = "lblMinutes";
            this.lblMinutes.Size = new System.Drawing.Size(44, 13);
            this.lblMinutes.TabIndex = 15;
            this.lblMinutes.Text = "Minutes";
            // 
            // numericMinutes
            // 
            this.numericMinutes.Location = new System.Drawing.Point(13, 47);
            this.numericMinutes.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.numericMinutes.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.numericMinutes.Name = "numericMinutes";
            this.numericMinutes.Size = new System.Drawing.Size(62, 20);
            this.numericMinutes.TabIndex = 14;
            this.numericMinutes.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericMinutes.ValueChanged += new System.EventHandler(this.numericField_ValueChanged);
            // 
            // lblSeconds
            // 
            this.lblSeconds.AutoSize = true;
            this.lblSeconds.Location = new System.Drawing.Point(83, 71);
            this.lblSeconds.Name = "lblSeconds";
            this.lblSeconds.Size = new System.Drawing.Size(49, 13);
            this.lblSeconds.TabIndex = 17;
            this.lblSeconds.Text = "Seconds";
            // 
            // lblHours
            // 
            this.lblHours.AutoSize = true;
            this.lblHours.Location = new System.Drawing.Point(83, 27);
            this.lblHours.Name = "lblHours";
            this.lblHours.Size = new System.Drawing.Size(35, 13);
            this.lblHours.TabIndex = 13;
            this.lblHours.Text = "Hours";
            // 
            // numericHours
            // 
            this.numericHours.Location = new System.Drawing.Point(13, 25);
            this.numericHours.Name = "numericHours";
            this.numericHours.Size = new System.Drawing.Size(62, 20);
            this.numericHours.TabIndex = 12;
            this.numericHours.ValueChanged += new System.EventHandler(this.numericField_ValueChanged);
            // 
            // numericSeconds
            // 
            this.numericSeconds.Location = new System.Drawing.Point(13, 69);
            this.numericSeconds.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.numericSeconds.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.numericSeconds.Name = "numericSeconds";
            this.numericSeconds.Size = new System.Drawing.Size(62, 20);
            this.numericSeconds.TabIndex = 16;
            this.numericSeconds.ValueChanged += new System.EventHandler(this.numericField_ValueChanged);
            // 
            // checkBoxCycling
            // 
            this.checkBoxCycling.AutoSize = true;
            this.checkBoxCycling.Location = new System.Drawing.Point(298, 12);
            this.checkBoxCycling.Name = "checkBoxCycling";
            this.checkBoxCycling.Size = new System.Drawing.Size(103, 17);
            this.checkBoxCycling.TabIndex = 18;
            this.checkBoxCycling.Text = "Continous Mode";
            this.checkBoxCycling.UseVisualStyleBackColor = true;
            this.checkBoxCycling.CheckedChanged += new System.EventHandler(this.checkBoxCycling_CheckedChanged);
            // 
            // groupPOP3
            // 
            this.groupPOP3.Controls.Add(this.label1);
            this.groupPOP3.Controls.Add(this.comboPOP3);
            this.groupPOP3.Location = new System.Drawing.Point(12, 12);
            this.groupPOP3.Name = "groupPOP3";
            this.groupPOP3.Size = new System.Drawing.Size(267, 50);
            this.groupPOP3.TabIndex = 23;
            this.groupPOP3.TabStop = false;
            this.groupPOP3.Text = "Fetch Mails From";
            // 
            // groupSMTP
            // 
            this.groupSMTP.Controls.Add(this.label2);
            this.groupSMTP.Controls.Add(this.comboSMTP);
            this.groupSMTP.Location = new System.Drawing.Point(12, 68);
            this.groupSMTP.Name = "groupSMTP";
            this.groupSMTP.Size = new System.Drawing.Size(267, 50);
            this.groupSMTP.TabIndex = 24;
            this.groupSMTP.TabStop = false;
            this.groupSMTP.Text = "Send Mails With";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 13);
            this.label2.TabIndex = 17;
            this.label2.Text = "SMTP Host:";
            // 
            // comboSMTP
            // 
            this.comboSMTP.FormattingEnabled = true;
            this.comboSMTP.Location = new System.Drawing.Point(102, 17);
            this.comboSMTP.Name = "comboSMTP";
            this.comboSMTP.Size = new System.Drawing.Size(152, 21);
            this.comboSMTP.TabIndex = 16;
            // 
            // groupEMailAddress
            // 
            this.groupEMailAddress.Controls.Add(this.label3);
            this.groupEMailAddress.Controls.Add(this.comboAddress);
            this.groupEMailAddress.Location = new System.Drawing.Point(12, 124);
            this.groupEMailAddress.Name = "groupEMailAddress";
            this.groupEMailAddress.Size = new System.Drawing.Size(267, 50);
            this.groupEMailAddress.TabIndex = 25;
            this.groupEMailAddress.TabStop = false;
            this.groupEMailAddress.Text = "To This";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 13);
            this.label3.TabIndex = 17;
            this.label3.Text = "E-Mail-Address:";
            // 
            // comboAddress
            // 
            this.comboAddress.FormattingEnabled = true;
            this.comboAddress.Location = new System.Drawing.Point(102, 17);
            this.comboAddress.Name = "comboAddress";
            this.comboAddress.Size = new System.Drawing.Size(152, 21);
            this.comboAddress.TabIndex = 16;
            // 
            // ConnectionWindow
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(487, 186);
            this.Controls.Add(this.groupEMailAddress);
            this.Controls.Add(this.groupSMTP);
            this.Controls.Add(this.checkBoxCycling);
            this.Controls.Add(this.groupPOP3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnOk);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ConnectionWindow";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Connection Window";
            this.TopMost = true;
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericTimesPerHour)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericMinutes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericHours)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericSeconds)).EndInit();
            this.groupPOP3.ResumeLayout(false);
            this.groupPOP3.PerformLayout();
            this.groupSMTP.ResumeLayout(false);
            this.groupSMTP.PerformLayout();
            this.groupEMailAddress.ResumeLayout(false);
            this.groupEMailAddress.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.ComboBox comboPOP3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblTimesPerHour;
        private System.Windows.Forms.NumericUpDown numericTimesPerHour;
        private System.Windows.Forms.CheckBox checkBoxCycling;
        private System.Windows.Forms.Label lblMinutes;
        private System.Windows.Forms.NumericUpDown numericMinutes;
        private System.Windows.Forms.Label lblSeconds;
        private System.Windows.Forms.Label lblHours;
        private System.Windows.Forms.NumericUpDown numericHours;
        private System.Windows.Forms.NumericUpDown numericSeconds;
        private System.Windows.Forms.GroupBox groupPOP3;
        private System.Windows.Forms.GroupBox groupSMTP;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboSMTP;
        private System.Windows.Forms.GroupBox groupEMailAddress;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboAddress;
    }
}