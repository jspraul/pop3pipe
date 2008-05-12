namespace POP3Pipe
{
    partial class ThunderbirdImport
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
            this.listViewPOP3 = new System.Windows.Forms.ListView();
            this.colPOP3Import = new System.Windows.Forms.ColumnHeader();
            this.colPOP3Description = new System.Windows.Forms.ColumnHeader();
            this.colPOP3Host = new System.Windows.Forms.ColumnHeader();
            this.colPOP3User = new System.Windows.Forms.ColumnHeader();
            this.groupBoxPop3 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBoxSmtp = new System.Windows.Forms.GroupBox();
            this.listViewSMTP = new System.Windows.Forms.ListView();
            this.colSmtpImport = new System.Windows.Forms.ColumnHeader();
            this.colSmtpDescription = new System.Windows.Forms.ColumnHeader();
            this.colSmtpHost = new System.Windows.Forms.ColumnHeader();
            this.colSmtpUser = new System.Windows.Forms.ColumnHeader();
            this.groupBoxAddresses = new System.Windows.Forms.GroupBox();
            this.listViewAddresses = new System.Windows.Forms.ListView();
            this.colAddressesImport = new System.Windows.Forms.ColumnHeader();
            this.colAddressesDescription = new System.Windows.Forms.ColumnHeader();
            this.colAddressesEMail = new System.Windows.Forms.ColumnHeader();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnImport = new System.Windows.Forms.Button();
            this.groupBoxPop3.SuspendLayout();
            this.groupBoxSmtp.SuspendLayout();
            this.groupBoxAddresses.SuspendLayout();
            this.SuspendLayout();
            // 
            // listViewPOP3
            // 
            this.listViewPOP3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewPOP3.CheckBoxes = true;
            this.listViewPOP3.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colPOP3Import,
            this.colPOP3Description,
            this.colPOP3Host,
            this.colPOP3User});
            this.listViewPOP3.FullRowSelect = true;
            this.listViewPOP3.GridLines = true;
            this.listViewPOP3.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listViewPOP3.HideSelection = false;
            this.listViewPOP3.LabelWrap = false;
            this.listViewPOP3.Location = new System.Drawing.Point(27, 20);
            this.listViewPOP3.MultiSelect = false;
            this.listViewPOP3.Name = "listViewPOP3";
            this.listViewPOP3.ShowGroups = false;
            this.listViewPOP3.Size = new System.Drawing.Size(354, 121);
            this.listViewPOP3.TabIndex = 11;
            this.listViewPOP3.TabStop = false;
            this.listViewPOP3.TileSize = new System.Drawing.Size(1, 1);
            this.listViewPOP3.UseCompatibleStateImageBehavior = false;
            this.listViewPOP3.View = System.Windows.Forms.View.Details;
            // 
            // colPOP3Import
            // 
            this.colPOP3Import.Text = "Import";
            this.colPOP3Import.Width = 42;
            // 
            // colPOP3Description
            // 
            this.colPOP3Description.Text = "Description";
            this.colPOP3Description.Width = 122;
            // 
            // colPOP3Host
            // 
            this.colPOP3Host.Text = "Host";
            this.colPOP3Host.Width = 93;
            // 
            // colPOP3User
            // 
            this.colPOP3User.Text = "User";
            this.colPOP3User.Width = 93;
            // 
            // groupBoxPop3
            // 
            this.groupBoxPop3.Controls.Add(this.listViewPOP3);
            this.groupBoxPop3.Location = new System.Drawing.Point(15, 49);
            this.groupBoxPop3.Name = "groupBoxPop3";
            this.groupBoxPop3.Size = new System.Drawing.Size(405, 155);
            this.groupBoxPop3.TabIndex = 12;
            this.groupBoxPop3.TabStop = false;
            this.groupBoxPop3.Text = "POP3 Hosts";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(412, 16);
            this.label1.TabIndex = 13;
            this.label1.Text = "Select the settings you want to import from Thunderbird configuration.";
            // 
            // groupBoxSmtp
            // 
            this.groupBoxSmtp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBoxSmtp.Controls.Add(this.listViewSMTP);
            this.groupBoxSmtp.Location = new System.Drawing.Point(15, 219);
            this.groupBoxSmtp.Name = "groupBoxSmtp";
            this.groupBoxSmtp.Size = new System.Drawing.Size(405, 155);
            this.groupBoxSmtp.TabIndex = 14;
            this.groupBoxSmtp.TabStop = false;
            this.groupBoxSmtp.Text = "SMTP Hosts";
            // 
            // listViewSMTP
            // 
            this.listViewSMTP.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewSMTP.CheckBoxes = true;
            this.listViewSMTP.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colSmtpImport,
            this.colSmtpDescription,
            this.colSmtpHost,
            this.colSmtpUser});
            this.listViewSMTP.FullRowSelect = true;
            this.listViewSMTP.GridLines = true;
            this.listViewSMTP.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listViewSMTP.HideSelection = false;
            this.listViewSMTP.LabelWrap = false;
            this.listViewSMTP.Location = new System.Drawing.Point(27, 20);
            this.listViewSMTP.MultiSelect = false;
            this.listViewSMTP.Name = "listViewSMTP";
            this.listViewSMTP.ShowGroups = false;
            this.listViewSMTP.Size = new System.Drawing.Size(354, 121);
            this.listViewSMTP.TabIndex = 11;
            this.listViewSMTP.TabStop = false;
            this.listViewSMTP.TileSize = new System.Drawing.Size(1, 1);
            this.listViewSMTP.UseCompatibleStateImageBehavior = false;
            this.listViewSMTP.View = System.Windows.Forms.View.Details;
            // 
            // colSmtpImport
            // 
            this.colSmtpImport.Text = "Import";
            this.colSmtpImport.Width = 42;
            // 
            // colSmtpDescription
            // 
            this.colSmtpDescription.Text = "Description";
            this.colSmtpDescription.Width = 122;
            // 
            // colSmtpHost
            // 
            this.colSmtpHost.Text = "Host";
            this.colSmtpHost.Width = 93;
            // 
            // colSmtpUser
            // 
            this.colSmtpUser.Text = "User";
            this.colSmtpUser.Width = 93;
            // 
            // groupBoxAddresses
            // 
            this.groupBoxAddresses.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxAddresses.Controls.Add(this.listViewAddresses);
            this.groupBoxAddresses.Location = new System.Drawing.Point(436, 49);
            this.groupBoxAddresses.Name = "groupBoxAddresses";
            this.groupBoxAddresses.Size = new System.Drawing.Size(403, 325);
            this.groupBoxAddresses.TabIndex = 15;
            this.groupBoxAddresses.TabStop = false;
            this.groupBoxAddresses.Text = "Addresses";
            // 
            // listViewAddresses
            // 
            this.listViewAddresses.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewAddresses.CheckBoxes = true;
            this.listViewAddresses.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colAddressesImport,
            this.colAddressesDescription,
            this.colAddressesEMail});
            this.listViewAddresses.FullRowSelect = true;
            this.listViewAddresses.GridLines = true;
            this.listViewAddresses.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listViewAddresses.HideSelection = false;
            this.listViewAddresses.LabelWrap = false;
            this.listViewAddresses.Location = new System.Drawing.Point(27, 19);
            this.listViewAddresses.MultiSelect = false;
            this.listViewAddresses.Name = "listViewAddresses";
            this.listViewAddresses.ShowGroups = false;
            this.listViewAddresses.Size = new System.Drawing.Size(354, 292);
            this.listViewAddresses.TabIndex = 16;
            this.listViewAddresses.TabStop = false;
            this.listViewAddresses.UseCompatibleStateImageBehavior = false;
            this.listViewAddresses.View = System.Windows.Forms.View.Details;
            // 
            // colAddressesImport
            // 
            this.colAddressesImport.Text = "Import";
            this.colAddressesImport.Width = 42;
            // 
            // colAddressesDescription
            // 
            this.colAddressesDescription.Text = "Description";
            this.colAddressesDescription.Width = 152;
            // 
            // colAddressesEMail
            // 
            this.colAddressesEMail.Text = "E-Mail Address";
            this.colAddressesEMail.Width = 156;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(42, 389);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(139, 23);
            this.btnCancel.TabIndex = 16;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnImport
            // 
            this.btnImport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnImport.Location = new System.Drawing.Point(678, 389);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(139, 23);
            this.btnImport.TabIndex = 17;
            this.btnImport.Text = "Import";
            this.btnImport.UseVisualStyleBackColor = true;
            // 
            // ThunderbirdImport
            // 
            this.AcceptButton = this.btnImport;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(858, 423);
            this.ControlBox = false;
            this.Controls.Add(this.btnImport);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.groupBoxAddresses);
            this.Controls.Add(this.groupBoxSmtp);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBoxPop3);
            this.MinimumSize = new System.Drawing.Size(866, 450);
            this.Name = "ThunderbirdImport";
            this.Text = "Import Thunderbird Configuration";
            this.groupBoxPop3.ResumeLayout(false);
            this.groupBoxSmtp.ResumeLayout(false);
            this.groupBoxAddresses.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView listViewPOP3;
        private System.Windows.Forms.ColumnHeader colPOP3Import;
        private System.Windows.Forms.ColumnHeader colPOP3Description;
        private System.Windows.Forms.ColumnHeader colPOP3Host;
        private System.Windows.Forms.ColumnHeader colPOP3User;
        private System.Windows.Forms.GroupBox groupBoxPop3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBoxSmtp;
        private System.Windows.Forms.ListView listViewSMTP;
        private System.Windows.Forms.ColumnHeader colSmtpImport;
        private System.Windows.Forms.ColumnHeader colSmtpDescription;
        private System.Windows.Forms.ColumnHeader colSmtpHost;
        private System.Windows.Forms.ColumnHeader colSmtpUser;
        private System.Windows.Forms.GroupBox groupBoxAddresses;
        private System.Windows.Forms.ListView listViewAddresses;
        private System.Windows.Forms.ColumnHeader colAddressesImport;
        private System.Windows.Forms.ColumnHeader colAddressesDescription;
        private System.Windows.Forms.ColumnHeader colAddressesEMail;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnImport;

    }
}