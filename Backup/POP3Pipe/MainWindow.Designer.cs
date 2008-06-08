namespace POP3Pipe
{
    partial class MainWindow
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
            this.btnStart = new System.Windows.Forms.Button();
            this.listViewPOP3 = new System.Windows.Forms.ListView();
            this.colPOP3Active = new System.Windows.Forms.ColumnHeader();
            this.colPOP3Description = new System.Windows.Forms.ColumnHeader();
            this.colPOP3Host = new System.Windows.Forms.ColumnHeader();
            this.colPOP3User = new System.Windows.Forms.ColumnHeader();
            this.contextMenuPOP3 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.pOP3HostToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.addItemToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editItemToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteItemToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.numericHours = new System.Windows.Forms.NumericUpDown();
            this.lblHours = new System.Windows.Forms.Label();
            this.lblMinutes = new System.Windows.Forms.Label();
            this.numericMinutes = new System.Windows.Forms.NumericUpDown();
            this.lblSeconds = new System.Windows.Forms.Label();
            this.numericSeconds = new System.Windows.Forms.NumericUpDown();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkBoxCyclingActive = new System.Windows.Forms.CheckBox();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPagePOP3 = new System.Windows.Forms.TabPage();
            this.tabPageSMTP = new System.Windows.Forms.TabPage();
            this.listViewSMTP = new System.Windows.Forms.ListView();
            this.colSMTPActive = new System.Windows.Forms.ColumnHeader();
            this.colSMTPDescription = new System.Windows.Forms.ColumnHeader();
            this.colSMTPHost = new System.Windows.Forms.ColumnHeader();
            this.colSMTPUser = new System.Windows.Forms.ColumnHeader();
            this.contextMenuSMTP = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.tabPageAddresses = new System.Windows.Forms.TabPage();
            this.listViewAddresses = new System.Windows.Forms.ListView();
            this.colAddressesActive = new System.Windows.Forms.ColumnHeader();
            this.colAddressesDescription = new System.Windows.Forms.ColumnHeader();
            this.colAddressesEMail = new System.Windows.Forms.ColumnHeader();
            this.contextMenuAddresses = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem7 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem8 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem9 = new System.Windows.Forms.ToolStripMenuItem();
            this.tabPageConnections = new System.Windows.Forms.TabPage();
            this.listViewConnections = new System.Windows.Forms.ListView();
            this.colConnectionsActive = new System.Windows.Forms.ColumnHeader();
            this.colConnectionsSource = new System.Windows.Forms.ColumnHeader();
            this.colConnectionsSMTP = new System.Windows.Forms.ColumnHeader();
            this.colConnectionsDestination = new System.Windows.Forms.ColumnHeader();
            this.contextMenuConnections = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripMenuItem();
            this.tabLogs = new System.Windows.Forms.TabPage();
            this.btnExpand = new System.Windows.Forms.Button();
            this.textLog = new System.Windows.Forms.RichTextBox();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.lblCountdown = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.pOP3ServerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addPOP3ServerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteServerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sMTPServerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addSMTPServerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteServerToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.addressesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addAddressToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteAddressToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.connectionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addConnectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteConnectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuPOP3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericHours)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericMinutes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericSeconds)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabPagePOP3.SuspendLayout();
            this.tabPageSMTP.SuspendLayout();
            this.contextMenuSMTP.SuspendLayout();
            this.tabPageAddresses.SuspendLayout();
            this.contextMenuAddresses.SuspendLayout();
            this.tabPageConnections.SuspendLayout();
            this.contextMenuConnections.SuspendLayout();
            this.tabLogs.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(371, 211);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(196, 30);
            this.btnStart.TabIndex = 8;
            this.btnStart.Text = "START";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // listViewPOP3
            // 
            this.listViewPOP3.CheckBoxes = true;
            this.listViewPOP3.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colPOP3Active,
            this.colPOP3Description,
            this.colPOP3Host,
            this.colPOP3User});
            this.listViewPOP3.ContextMenuStrip = this.contextMenuPOP3;
            this.listViewPOP3.FullRowSelect = true;
            this.listViewPOP3.GridLines = true;
            this.listViewPOP3.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listViewPOP3.HideSelection = false;
            this.listViewPOP3.LabelWrap = false;
            this.listViewPOP3.Location = new System.Drawing.Point(0, 0);
            this.listViewPOP3.MultiSelect = false;
            this.listViewPOP3.Name = "listViewPOP3";
            this.listViewPOP3.ShowGroups = false;
            this.listViewPOP3.Size = new System.Drawing.Size(354, 172);
            this.listViewPOP3.TabIndex = 10;
            this.listViewPOP3.TabStop = false;
            this.listViewPOP3.UseCompatibleStateImageBehavior = false;
            this.listViewPOP3.View = System.Windows.Forms.View.Details;
            this.listViewPOP3.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.listPOP3Server_ItemCheck);
            // 
            // colPOP3Active
            // 
            this.colPOP3Active.Text = "Active";
            this.colPOP3Active.Width = 42;
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
            // contextMenuPOP3
            // 
            this.contextMenuPOP3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pOP3HostToolStripMenuItem,
            this.toolStripSeparator1,
            this.addItemToolStripMenuItem,
            this.editItemToolStripMenuItem,
            this.deleteItemToolStripMenuItem});
            this.contextMenuPOP3.Name = "contextMenuList";
            this.contextMenuPOP3.ShowImageMargin = false;
            this.contextMenuPOP3.ShowItemToolTips = false;
            this.contextMenuPOP3.Size = new System.Drawing.Size(117, 98);
            this.contextMenuPOP3.Text = "POP3 Hosts";
            // 
            // pOP3HostToolStripMenuItem
            // 
            this.pOP3HostToolStripMenuItem.Enabled = false;
            this.pOP3HostToolStripMenuItem.Name = "pOP3HostToolStripMenuItem";
            this.pOP3HostToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.pOP3HostToolStripMenuItem.Text = "POP3 Host";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(113, 6);
            // 
            // addItemToolStripMenuItem
            // 
            this.addItemToolStripMenuItem.Name = "addItemToolStripMenuItem";
            this.addItemToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.addItemToolStripMenuItem.Text = "Add Item";
            this.addItemToolStripMenuItem.Click += new System.EventHandler(this.toolMenuPop3Add_Click);
            // 
            // editItemToolStripMenuItem
            // 
            this.editItemToolStripMenuItem.Name = "editItemToolStripMenuItem";
            this.editItemToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.editItemToolStripMenuItem.Text = "Edit Item";
            // 
            // deleteItemToolStripMenuItem
            // 
            this.deleteItemToolStripMenuItem.Name = "deleteItemToolStripMenuItem";
            this.deleteItemToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.deleteItemToolStripMenuItem.Text = "Delete Item";
            this.deleteItemToolStripMenuItem.Click += new System.EventHandler(this.toolMenuPop3Delete_Click);
            // 
            // numericHours
            // 
            this.numericHours.Location = new System.Drawing.Point(43, 22);
            this.numericHours.Name = "numericHours";
            this.numericHours.Size = new System.Drawing.Size(81, 20);
            this.numericHours.TabIndex = 12;
            // 
            // lblHours
            // 
            this.lblHours.AutoSize = true;
            this.lblHours.Location = new System.Drawing.Point(132, 24);
            this.lblHours.Name = "lblHours";
            this.lblHours.Size = new System.Drawing.Size(35, 13);
            this.lblHours.TabIndex = 13;
            this.lblHours.Text = "Hours";
            // 
            // lblMinutes
            // 
            this.lblMinutes.AutoSize = true;
            this.lblMinutes.Location = new System.Drawing.Point(132, 46);
            this.lblMinutes.Name = "lblMinutes";
            this.lblMinutes.Size = new System.Drawing.Size(44, 13);
            this.lblMinutes.TabIndex = 15;
            this.lblMinutes.Text = "Minutes";
            // 
            // numericMinutes
            // 
            this.numericMinutes.Location = new System.Drawing.Point(43, 44);
            this.numericMinutes.Name = "numericMinutes";
            this.numericMinutes.Size = new System.Drawing.Size(81, 20);
            this.numericMinutes.TabIndex = 14;
            // 
            // lblSeconds
            // 
            this.lblSeconds.AutoSize = true;
            this.lblSeconds.Location = new System.Drawing.Point(132, 68);
            this.lblSeconds.Name = "lblSeconds";
            this.lblSeconds.Size = new System.Drawing.Size(49, 13);
            this.lblSeconds.TabIndex = 17;
            this.lblSeconds.Text = "Seconds";
            // 
            // numericSeconds
            // 
            this.numericSeconds.Location = new System.Drawing.Point(43, 66);
            this.numericSeconds.Name = "numericSeconds";
            this.numericSeconds.Size = new System.Drawing.Size(81, 20);
            this.numericSeconds.TabIndex = 16;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checkBoxCyclingActive);
            this.groupBox1.Controls.Add(this.lblMinutes);
            this.groupBox1.Controls.Add(this.numericMinutes);
            this.groupBox1.Controls.Add(this.lblSeconds);
            this.groupBox1.Controls.Add(this.lblHours);
            this.groupBox1.Controls.Add(this.numericHours);
            this.groupBox1.Controls.Add(this.numericSeconds);
            this.groupBox1.Location = new System.Drawing.Point(371, 89);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(195, 96);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Run process every:";
            // 
            // checkBoxCyclingActive
            // 
            this.checkBoxCyclingActive.AutoSize = true;
            this.checkBoxCyclingActive.Enabled = false;
            this.checkBoxCyclingActive.Location = new System.Drawing.Point(15, 44);
            this.checkBoxCyclingActive.Name = "checkBoxCyclingActive";
            this.checkBoxCyclingActive.Size = new System.Drawing.Size(15, 14);
            this.checkBoxCyclingActive.TabIndex = 18;
            this.checkBoxCyclingActive.UseVisualStyleBackColor = true;
            // 
            // tabControl
            // 
            this.tabControl.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.tabControl.Controls.Add(this.tabPagePOP3);
            this.tabControl.Controls.Add(this.tabPageSMTP);
            this.tabControl.Controls.Add(this.tabPageAddresses);
            this.tabControl.Controls.Add(this.tabPageConnections);
            this.tabControl.Controls.Add(this.tabLogs);
            this.tabControl.Location = new System.Drawing.Point(3, 29);
            this.tabControl.Multiline = true;
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(362, 200);
            this.tabControl.TabIndex = 16;
            // 
            // tabPagePOP3
            // 
            this.tabPagePOP3.Controls.Add(this.listViewPOP3);
            this.tabPagePOP3.Location = new System.Drawing.Point(4, 4);
            this.tabPagePOP3.Name = "tabPagePOP3";
            this.tabPagePOP3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPagePOP3.Size = new System.Drawing.Size(354, 174);
            this.tabPagePOP3.TabIndex = 0;
            this.tabPagePOP3.Text = "POP3 Hosts";
            this.tabPagePOP3.UseVisualStyleBackColor = true;
            // 
            // tabPageSMTP
            // 
            this.tabPageSMTP.Controls.Add(this.listViewSMTP);
            this.tabPageSMTP.Location = new System.Drawing.Point(4, 4);
            this.tabPageSMTP.Name = "tabPageSMTP";
            this.tabPageSMTP.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageSMTP.Size = new System.Drawing.Size(354, 174);
            this.tabPageSMTP.TabIndex = 2;
            this.tabPageSMTP.Text = "SMTP Hosts";
            this.tabPageSMTP.UseVisualStyleBackColor = true;
            // 
            // listViewSMTP
            // 
            this.listViewSMTP.CheckBoxes = true;
            this.listViewSMTP.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colSMTPActive,
            this.colSMTPDescription,
            this.colSMTPHost,
            this.colSMTPUser});
            this.listViewSMTP.ContextMenuStrip = this.contextMenuSMTP;
            this.listViewSMTP.FullRowSelect = true;
            this.listViewSMTP.GridLines = true;
            this.listViewSMTP.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listViewSMTP.HideSelection = false;
            this.listViewSMTP.LabelWrap = false;
            this.listViewSMTP.Location = new System.Drawing.Point(0, 0);
            this.listViewSMTP.MultiSelect = false;
            this.listViewSMTP.Name = "listViewSMTP";
            this.listViewSMTP.ShowGroups = false;
            this.listViewSMTP.Size = new System.Drawing.Size(354, 172);
            this.listViewSMTP.TabIndex = 11;
            this.listViewSMTP.TabStop = false;
            this.listViewSMTP.UseCompatibleStateImageBehavior = false;
            this.listViewSMTP.View = System.Windows.Forms.View.Details;
            // 
            // colSMTPActive
            // 
            this.colSMTPActive.Text = "Active";
            this.colSMTPActive.Width = 42;
            // 
            // colSMTPDescription
            // 
            this.colSMTPDescription.Text = "Description";
            this.colSMTPDescription.Width = 122;
            // 
            // colSMTPHost
            // 
            this.colSMTPHost.Text = "Host";
            this.colSMTPHost.Width = 93;
            // 
            // colSMTPUser
            // 
            this.colSMTPUser.Text = "User";
            this.colSMTPUser.Width = 93;
            // 
            // contextMenuSMTP
            // 
            this.contextMenuSMTP.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.toolStripMenuItem2,
            this.toolStripMenuItem3});
            this.contextMenuSMTP.Name = "contextMenuList";
            this.contextMenuSMTP.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.contextMenuSMTP.Size = new System.Drawing.Size(142, 70);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(141, 22);
            this.toolStripMenuItem1.Text = "Add Item";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolMenuSmtpAdd_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(141, 22);
            this.toolStripMenuItem2.Text = "Edit Item";
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(141, 22);
            this.toolStripMenuItem3.Text = "Delete Item";
            this.toolStripMenuItem3.Click += new System.EventHandler(this.toolMenuSmtpDelete_Click);
            // 
            // tabPageAddresses
            // 
            this.tabPageAddresses.Controls.Add(this.listViewAddresses);
            this.tabPageAddresses.Location = new System.Drawing.Point(4, 4);
            this.tabPageAddresses.Name = "tabPageAddresses";
            this.tabPageAddresses.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageAddresses.Size = new System.Drawing.Size(354, 174);
            this.tabPageAddresses.TabIndex = 4;
            this.tabPageAddresses.Text = "Addresses";
            this.tabPageAddresses.UseVisualStyleBackColor = true;
            // 
            // listViewAddresses
            // 
            this.listViewAddresses.CheckBoxes = true;
            this.listViewAddresses.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colAddressesActive,
            this.colAddressesDescription,
            this.colAddressesEMail});
            this.listViewAddresses.ContextMenuStrip = this.contextMenuAddresses;
            this.listViewAddresses.FullRowSelect = true;
            this.listViewAddresses.GridLines = true;
            this.listViewAddresses.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listViewAddresses.HideSelection = false;
            this.listViewAddresses.LabelWrap = false;
            this.listViewAddresses.Location = new System.Drawing.Point(0, 0);
            this.listViewAddresses.MultiSelect = false;
            this.listViewAddresses.Name = "listViewAddresses";
            this.listViewAddresses.ShowGroups = false;
            this.listViewAddresses.Size = new System.Drawing.Size(354, 172);
            this.listViewAddresses.TabIndex = 12;
            this.listViewAddresses.TabStop = false;
            this.listViewAddresses.UseCompatibleStateImageBehavior = false;
            this.listViewAddresses.View = System.Windows.Forms.View.Details;
            // 
            // colAddressesActive
            // 
            this.colAddressesActive.Text = "Active";
            this.colAddressesActive.Width = 42;
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
            // contextMenuAddresses
            // 
            this.contextMenuAddresses.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem7,
            this.toolStripMenuItem8,
            this.toolStripMenuItem9});
            this.contextMenuAddresses.Name = "contextMenuList";
            this.contextMenuAddresses.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.contextMenuAddresses.Size = new System.Drawing.Size(142, 70);
            // 
            // toolStripMenuItem7
            // 
            this.toolStripMenuItem7.Name = "toolStripMenuItem7";
            this.toolStripMenuItem7.Size = new System.Drawing.Size(141, 22);
            this.toolStripMenuItem7.Text = "Add Item";
            this.toolStripMenuItem7.Click += new System.EventHandler(this.toolMenuAddressAdd_Click);
            // 
            // toolStripMenuItem8
            // 
            this.toolStripMenuItem8.Name = "toolStripMenuItem8";
            this.toolStripMenuItem8.Size = new System.Drawing.Size(141, 22);
            this.toolStripMenuItem8.Text = "Edit Item";
            // 
            // toolStripMenuItem9
            // 
            this.toolStripMenuItem9.Name = "toolStripMenuItem9";
            this.toolStripMenuItem9.Size = new System.Drawing.Size(141, 22);
            this.toolStripMenuItem9.Text = "Delete Item";
            this.toolStripMenuItem9.Click += new System.EventHandler(this.toolMenuAddressDelete_Click);
            // 
            // tabPageConnections
            // 
            this.tabPageConnections.Controls.Add(this.listViewConnections);
            this.tabPageConnections.Location = new System.Drawing.Point(4, 4);
            this.tabPageConnections.Name = "tabPageConnections";
            this.tabPageConnections.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageConnections.Size = new System.Drawing.Size(354, 174);
            this.tabPageConnections.TabIndex = 3;
            this.tabPageConnections.Text = "Connections";
            this.tabPageConnections.UseVisualStyleBackColor = true;
            // 
            // listViewConnections
            // 
            this.listViewConnections.CheckBoxes = true;
            this.listViewConnections.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colConnectionsActive,
            this.colConnectionsSource,
            this.colConnectionsSMTP,
            this.colConnectionsDestination});
            this.listViewConnections.ContextMenuStrip = this.contextMenuConnections;
            this.listViewConnections.FullRowSelect = true;
            this.listViewConnections.GridLines = true;
            this.listViewConnections.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listViewConnections.HideSelection = false;
            this.listViewConnections.LabelWrap = false;
            this.listViewConnections.Location = new System.Drawing.Point(0, 0);
            this.listViewConnections.MultiSelect = false;
            this.listViewConnections.Name = "listViewConnections";
            this.listViewConnections.ShowGroups = false;
            this.listViewConnections.Size = new System.Drawing.Size(354, 172);
            this.listViewConnections.TabIndex = 11;
            this.listViewConnections.TabStop = false;
            this.listViewConnections.UseCompatibleStateImageBehavior = false;
            this.listViewConnections.View = System.Windows.Forms.View.Details;
            // 
            // colConnectionsActive
            // 
            this.colConnectionsActive.Text = "Active";
            this.colConnectionsActive.Width = 42;
            // 
            // colConnectionsSource
            // 
            this.colConnectionsSource.Text = "Source (POP3)";
            this.colConnectionsSource.Width = 91;
            // 
            // colConnectionsSMTP
            // 
            this.colConnectionsSMTP.Text = "Over (SMTP)";
            this.colConnectionsSMTP.Width = 101;
            // 
            // colConnectionsDestination
            // 
            this.colConnectionsDestination.Text = "Destination";
            this.colConnectionsDestination.Width = 116;
            // 
            // contextMenuConnections
            // 
            this.contextMenuConnections.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem4,
            this.toolStripMenuItem5,
            this.toolStripMenuItem6});
            this.contextMenuConnections.Name = "contextMenuList";
            this.contextMenuConnections.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.contextMenuConnections.Size = new System.Drawing.Size(153, 92);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(152, 22);
            this.toolStripMenuItem4.Text = "Add Item";
            this.toolStripMenuItem4.Click += new System.EventHandler(this.toolMenuConnectionsAdd_Click);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(152, 22);
            this.toolStripMenuItem5.Text = "Edit Item";
            this.toolStripMenuItem5.Click += new System.EventHandler(this.toolMenuConnectionsEdit_Click);
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            this.toolStripMenuItem6.Size = new System.Drawing.Size(152, 22);
            this.toolStripMenuItem6.Text = "Delete Item";
            this.toolStripMenuItem6.Click += new System.EventHandler(this.toolMenuConnectionsDelete_Click);
            // 
            // tabLogs
            // 
            this.tabLogs.Controls.Add(this.btnExpand);
            this.tabLogs.Controls.Add(this.textLog);
            this.tabLogs.Location = new System.Drawing.Point(4, 4);
            this.tabLogs.Name = "tabLogs";
            this.tabLogs.Padding = new System.Windows.Forms.Padding(3);
            this.tabLogs.Size = new System.Drawing.Size(354, 174);
            this.tabLogs.TabIndex = 1;
            this.tabLogs.Text = "Logs";
            this.tabLogs.UseVisualStyleBackColor = true;
            // 
            // btnExpand
            // 
            this.btnExpand.Location = new System.Drawing.Point(342, -4);
            this.btnExpand.Name = "btnExpand";
            this.btnExpand.Size = new System.Drawing.Size(15, 180);
            this.btnExpand.TabIndex = 1;
            this.btnExpand.Text = ">>>";
            this.btnExpand.UseVisualStyleBackColor = true;
            this.btnExpand.Click += new System.EventHandler(this.btnExpand_Click);
            // 
            // textLog
            // 
            this.textLog.BackColor = System.Drawing.Color.White;
            this.textLog.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textLog.Location = new System.Drawing.Point(0, 0);
            this.textLog.Name = "textLog";
            this.textLog.ReadOnly = true;
            this.textLog.Size = new System.Drawing.Size(343, 172);
            this.textLog.TabIndex = 0;
            this.textLog.TabStop = false;
            this.textLog.Text = "";
            this.textLog.WordWrap = false;
            this.textLog.TextChanged += new System.EventHandler(this.textLog_TextChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "POP3 Server";
            this.columnHeader1.Width = 160;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "User Name";
            this.columnHeader2.Width = 119;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "";
            this.columnHeader3.Width = 0;
            // 
            // lblCountdown
            // 
            this.lblCountdown.AutoSize = true;
            this.lblCountdown.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCountdown.Location = new System.Drawing.Point(505, 189);
            this.lblCountdown.Name = "lblCountdown";
            this.lblCountdown.Padding = new System.Windows.Forms.Padding(5, 1, 5, 1);
            this.lblCountdown.Size = new System.Drawing.Size(61, 17);
            this.lblCountdown.TabIndex = 17;
            this.lblCountdown.Text = "00:00:00";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(397, 49);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 18;
            this.button1.Text = "test";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(492, 49);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 19;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pOP3ServerToolStripMenuItem,
            this.sMTPServerToolStripMenuItem,
            this.addressesToolStripMenuItem,
            this.connectionsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(581, 24);
            this.menuStrip1.TabIndex = 20;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // pOP3ServerToolStripMenuItem
            // 
            this.pOP3ServerToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addPOP3ServerToolStripMenuItem,
            this.deleteServerToolStripMenuItem});
            this.pOP3ServerToolStripMenuItem.Name = "pOP3ServerToolStripMenuItem";
            this.pOP3ServerToolStripMenuItem.Size = new System.Drawing.Size(80, 20);
            this.pOP3ServerToolStripMenuItem.Text = "POP3 Server";
            // 
            // addPOP3ServerToolStripMenuItem
            // 
            this.addPOP3ServerToolStripMenuItem.Name = "addPOP3ServerToolStripMenuItem";
            this.addPOP3ServerToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.addPOP3ServerToolStripMenuItem.Text = "Add Server";
            this.addPOP3ServerToolStripMenuItem.Click += new System.EventHandler(this.toolMenuPop3Add_Click);
            // 
            // deleteServerToolStripMenuItem
            // 
            this.deleteServerToolStripMenuItem.Name = "deleteServerToolStripMenuItem";
            this.deleteServerToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.deleteServerToolStripMenuItem.Text = "Delete Server";
            // 
            // sMTPServerToolStripMenuItem
            // 
            this.sMTPServerToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addSMTPServerToolStripMenuItem,
            this.deleteServerToolStripMenuItem1});
            this.sMTPServerToolStripMenuItem.Name = "sMTPServerToolStripMenuItem";
            this.sMTPServerToolStripMenuItem.Size = new System.Drawing.Size(80, 20);
            this.sMTPServerToolStripMenuItem.Text = "SMTP Server";
            // 
            // addSMTPServerToolStripMenuItem
            // 
            this.addSMTPServerToolStripMenuItem.Name = "addSMTPServerToolStripMenuItem";
            this.addSMTPServerToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.addSMTPServerToolStripMenuItem.Text = "Add Server";
            // 
            // deleteServerToolStripMenuItem1
            // 
            this.deleteServerToolStripMenuItem1.Name = "deleteServerToolStripMenuItem1";
            this.deleteServerToolStripMenuItem1.Size = new System.Drawing.Size(151, 22);
            this.deleteServerToolStripMenuItem1.Text = "Delete Server";
            // 
            // addressesToolStripMenuItem
            // 
            this.addressesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addAddressToolStripMenuItem,
            this.deleteAddressToolStripMenuItem});
            this.addressesToolStripMenuItem.Name = "addressesToolStripMenuItem";
            this.addressesToolStripMenuItem.Size = new System.Drawing.Size(69, 20);
            this.addressesToolStripMenuItem.Text = "Addresses";
            // 
            // addAddressToolStripMenuItem
            // 
            this.addAddressToolStripMenuItem.Name = "addAddressToolStripMenuItem";
            this.addAddressToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.addAddressToolStripMenuItem.Text = "Add Address";
            this.addAddressToolStripMenuItem.Click += new System.EventHandler(this.addAddressToolStripMenuItem_Click);
            // 
            // deleteAddressToolStripMenuItem
            // 
            this.deleteAddressToolStripMenuItem.Name = "deleteAddressToolStripMenuItem";
            this.deleteAddressToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.deleteAddressToolStripMenuItem.Text = "Delete Address";
            // 
            // connectionsToolStripMenuItem
            // 
            this.connectionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addConnectionToolStripMenuItem,
            this.deleteConnectionToolStripMenuItem});
            this.connectionsToolStripMenuItem.Name = "connectionsToolStripMenuItem";
            this.connectionsToolStripMenuItem.Size = new System.Drawing.Size(78, 20);
            this.connectionsToolStripMenuItem.Text = "Connections";
            // 
            // addConnectionToolStripMenuItem
            // 
            this.addConnectionToolStripMenuItem.Name = "addConnectionToolStripMenuItem";
            this.addConnectionToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.addConnectionToolStripMenuItem.Text = "Add Connection";
            this.addConnectionToolStripMenuItem.Click += new System.EventHandler(this.addConnectionToolStripMenuItem_Click);
            // 
            // deleteConnectionToolStripMenuItem
            // 
            this.deleteConnectionToolStripMenuItem.Name = "deleteConnectionToolStripMenuItem";
            this.deleteConnectionToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.deleteConnectionToolStripMenuItem.Text = "Delete Connection";
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(581, 247);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.lblCountdown);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "MainWindow";
            this.Text = "POP3 === Pipe";
            this.Deactivate += new System.EventHandler(this.MainWindow_Deactivate);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainWindow_FormClosed);
            this.contextMenuPOP3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericHours)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericMinutes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericSeconds)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabControl.ResumeLayout(false);
            this.tabPagePOP3.ResumeLayout(false);
            this.tabPageSMTP.ResumeLayout(false);
            this.contextMenuSMTP.ResumeLayout(false);
            this.tabPageAddresses.ResumeLayout(false);
            this.contextMenuAddresses.ResumeLayout(false);
            this.tabPageConnections.ResumeLayout(false);
            this.contextMenuConnections.ResumeLayout(false);
            this.tabLogs.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.ListView listViewPOP3;
        private System.Windows.Forms.ColumnHeader colPOP3User;
        private System.Windows.Forms.NumericUpDown numericHours;
        private System.Windows.Forms.Label lblHours;
        private System.Windows.Forms.Label lblMinutes;
        private System.Windows.Forms.NumericUpDown numericMinutes;
        private System.Windows.Forms.Label lblSeconds;
        private System.Windows.Forms.NumericUpDown numericSeconds;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox checkBoxCyclingActive;
        private System.Windows.Forms.ContextMenuStrip contextMenuPOP3;
        private System.Windows.Forms.ToolStripMenuItem editItemToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteItemToolStripMenuItem;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPagePOP3;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader colPOP3Host;
        private System.Windows.Forms.ToolStripMenuItem addItemToolStripMenuItem;
        private System.Windows.Forms.TabPage tabLogs;
        private System.Windows.Forms.RichTextBox textLog;
        private System.Windows.Forms.Button btnExpand;
        private System.Windows.Forms.Label lblCountdown;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TabPage tabPageSMTP;
        private System.Windows.Forms.ListView listViewSMTP;
        private System.Windows.Forms.ColumnHeader colSMTPDescription;
        private System.Windows.Forms.ColumnHeader colSMTPUser;
        private System.Windows.Forms.TabPage tabPageConnections;
        private System.Windows.Forms.ColumnHeader colPOP3Description;
        private System.Windows.Forms.TabPage tabPageAddresses;
        private System.Windows.Forms.ColumnHeader colSMTPHost;
        private System.Windows.Forms.ListView listViewAddresses;
        private System.Windows.Forms.ColumnHeader colAddressesDescription;
        private System.Windows.Forms.ColumnHeader colAddressesEMail;
        private System.Windows.Forms.ListView listViewConnections;
        private System.Windows.Forms.ColumnHeader colConnectionsSource;
        private System.Windows.Forms.ColumnHeader colConnectionsSMTP;
        private System.Windows.Forms.ColumnHeader colConnectionsDestination;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem connectionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addConnectionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteConnectionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pOP3ServerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addPOP3ServerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteServerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sMTPServerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addSMTPServerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteServerToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem addressesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addAddressToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteAddressToolStripMenuItem;
        private System.Windows.Forms.ColumnHeader colPOP3Active;
        private System.Windows.Forms.ColumnHeader colSMTPActive;
        private System.Windows.Forms.ColumnHeader colAddressesActive;
        private System.Windows.Forms.ColumnHeader colConnectionsActive;
        private System.Windows.Forms.ContextMenuStrip contextMenuSMTP;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ContextMenuStrip contextMenuAddresses;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem7;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem8;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem9;
        private System.Windows.Forms.ContextMenuStrip contextMenuConnections;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem6;
        private System.Windows.Forms.ToolStripMenuItem pOP3HostToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    }
}

