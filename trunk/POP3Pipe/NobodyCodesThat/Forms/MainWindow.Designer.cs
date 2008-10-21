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
            //MainWindow.FadeForm(this, 50, false);
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.btnStart = new System.Windows.Forms.Button();
            this.listViewPOP3 = new System.Windows.Forms.ListView();
            this.colPOP3Active = new System.Windows.Forms.ColumnHeader();
            this.colPOP3Description = new System.Windows.Forms.ColumnHeader();
            this.colPOP3Host = new System.Windows.Forms.ColumnHeader();
            this.colPOP3User = new System.Windows.Forms.ColumnHeader();
            this.contextMenuPOP3 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.pop3HostToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuPop3Add = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuPop3Edit = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuPop3Delete = new System.Windows.Forms.ToolStripMenuItem();
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
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuSmtpAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuSmtpEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuSmtpDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.tabPageAddresses = new System.Windows.Forms.TabPage();
            this.listViewAddresses = new System.Windows.Forms.ListView();
            this.colAddressesActive = new System.Windows.Forms.ColumnHeader();
            this.colAddressesDescription = new System.Windows.Forms.ColumnHeader();
            this.colAddressesEMail = new System.Windows.Forms.ColumnHeader();
            this.contextMenuAddresses = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuAddressesAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuAddressesEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuAddressesDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.tabPageConnections = new System.Windows.Forms.TabPage();
            this.listViewConnections = new System.Windows.Forms.ListView();
            this.colConnectionsActive = new System.Windows.Forms.ColumnHeader();
            this.colConnectionsSource = new System.Windows.Forms.ColumnHeader();
            this.colConnectionsSMTP = new System.Windows.Forms.ColumnHeader();
            this.colConnectionsDestination = new System.Windows.Forms.ColumnHeader();
            this.contextMenuConnections = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuConnectionsAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuConnectionsEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuConnectionsDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.tabLogs = new System.Windows.Forms.TabPage();
            this.textLog = new System.Windows.Forms.RichTextBox();
            this.contextMenuLogs = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.clearConsoleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.configurationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pop3ServerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addPOP3ServerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deletePOP3ServerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.smtpServerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addSMTPServerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteSMTPServerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addressesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addAddressToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteAddressToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.connectionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addConnectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteConnectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.checkForUpdateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.revisionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutThisProgramToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpProvider = new System.Windows.Forms.HelpProvider();
            this.threadComManager = new System.ComponentModel.BackgroundWorker();
            this.threadPOP3 = new System.ComponentModel.BackgroundWorker();
            this.threadSMTP = new System.ComponentModel.BackgroundWorker();
            this.button3 = new System.Windows.Forms.Button();
            this.loadingCircle = new MRG.Controls.UI.LoadingCircle();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.contextMenuPOP3.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabPagePOP3.SuspendLayout();
            this.tabPageSMTP.SuspendLayout();
            this.contextMenuSMTP.SuspendLayout();
            this.tabPageAddresses.SuspendLayout();
            this.contextMenuAddresses.SuspendLayout();
            this.tabPageConnections.SuspendLayout();
            this.contextMenuConnections.SuspendLayout();
            this.tabLogs.SuspendLayout();
            this.contextMenuLogs.SuspendLayout();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnStart
            // 
            this.btnStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStart.Location = new System.Drawing.Point(397, 201);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(159, 37);
            this.btnStart.TabIndex = 8;
            this.btnStart.Text = "START";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // listViewPOP3
            // 
            this.listViewPOP3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
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
            this.helpProvider.SetShowHelp(this.listViewPOP3, true);
            this.listViewPOP3.Size = new System.Drawing.Size(354, 172);
            this.listViewPOP3.TabIndex = 10;
            this.listViewPOP3.TabStop = false;
            this.listViewPOP3.TileSize = new System.Drawing.Size(1, 1);
            this.listViewPOP3.UseCompatibleStateImageBehavior = false;
            this.listViewPOP3.View = System.Windows.Forms.View.Details;
            this.listViewPOP3.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.listViewPOP3_ItemChecked);
            this.listViewPOP3.DoubleClick += new System.EventHandler(this.listView_DoubleClick);
            this.listViewPOP3.MouseUp += new System.Windows.Forms.MouseEventHandler(this.listViewPOP3_MouseUp);
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
            this.pop3HostToolStripMenuItem,
            this.toolStripSeparator1,
            this.toolStripMenuPop3Add,
            this.toolStripMenuPop3Edit,
            this.toolStripMenuPop3Delete});
            this.contextMenuPOP3.Name = "contextMenuList";
            this.contextMenuPOP3.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.contextMenuPOP3.ShowImageMargin = false;
            this.contextMenuPOP3.ShowItemToolTips = false;
            this.contextMenuPOP3.Size = new System.Drawing.Size(119, 98);
            this.contextMenuPOP3.Text = "POP3 Hosts";
            // 
            // pop3HostToolStripMenuItem
            // 
            this.pop3HostToolStripMenuItem.BackColor = System.Drawing.SystemColors.Control;
            this.pop3HostToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.pop3HostToolStripMenuItem.Enabled = false;
            this.pop3HostToolStripMenuItem.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pop3HostToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ControlText;
            this.pop3HostToolStripMenuItem.Name = "pop3HostToolStripMenuItem";
            this.pop3HostToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
            this.pop3HostToolStripMenuItem.Text = "POP3 Host";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(115, 6);
            // 
            // toolStripMenuPop3Add
            // 
            this.toolStripMenuPop3Add.Name = "toolStripMenuPop3Add";
            this.toolStripMenuPop3Add.Size = new System.Drawing.Size(118, 22);
            this.toolStripMenuPop3Add.Text = "Add Item";
            this.toolStripMenuPop3Add.Click += new System.EventHandler(this.toolMenuPop3Add_Click);
            // 
            // toolStripMenuPop3Edit
            // 
            this.toolStripMenuPop3Edit.Name = "toolStripMenuPop3Edit";
            this.toolStripMenuPop3Edit.Size = new System.Drawing.Size(118, 22);
            this.toolStripMenuPop3Edit.Text = "Edit Item";
            this.toolStripMenuPop3Edit.Click += new System.EventHandler(this.toolMenuPop3Edit_Click);
            // 
            // toolStripMenuPop3Delete
            // 
            this.toolStripMenuPop3Delete.Name = "toolStripMenuPop3Delete";
            this.toolStripMenuPop3Delete.Size = new System.Drawing.Size(118, 22);
            this.toolStripMenuPop3Delete.Text = "Delete Item";
            this.toolStripMenuPop3Delete.Click += new System.EventHandler(this.toolMenuPop3Delete_Click);
            // 
            // tabControl
            // 
            this.tabControl.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl.Controls.Add(this.tabPagePOP3);
            this.tabControl.Controls.Add(this.tabPageSMTP);
            this.tabControl.Controls.Add(this.tabPageAddresses);
            this.tabControl.Controls.Add(this.tabPageConnections);
            this.tabControl.Controls.Add(this.tabLogs);
            this.tabControl.Location = new System.Drawing.Point(12, 38);
            this.tabControl.Multiline = true;
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(362, 200);
            this.tabControl.TabIndex = 16;
            this.tabControl.SelectedIndexChanged += new System.EventHandler(this.tabControl_SelectedIndexChanged);
            // 
            // tabPagePOP3
            // 
            this.tabPagePOP3.Controls.Add(this.listViewPOP3);
            this.tabPagePOP3.Location = new System.Drawing.Point(4, 4);
            this.tabPagePOP3.Name = "tabPagePOP3";
            this.tabPagePOP3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPagePOP3.Size = new System.Drawing.Size(354, 174);
            this.tabPagePOP3.TabIndex = 0;
            this.tabPagePOP3.Text = "POP3 Server";
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
            this.tabPageSMTP.Text = "SMTP Server ";
            this.tabPageSMTP.UseVisualStyleBackColor = true;
            // 
            // listViewSMTP
            // 
            this.listViewSMTP.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
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
            this.helpProvider.SetShowHelp(this.listViewSMTP, true);
            this.listViewSMTP.Size = new System.Drawing.Size(354, 172);
            this.listViewSMTP.TabIndex = 11;
            this.listViewSMTP.TabStop = false;
            this.listViewSMTP.UseCompatibleStateImageBehavior = false;
            this.listViewSMTP.View = System.Windows.Forms.View.Details;
            this.listViewSMTP.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.listViewSMTP_ItemChecked);
            this.listViewSMTP.DoubleClick += new System.EventHandler(this.listView_DoubleClick);
            this.listViewSMTP.MouseUp += new System.Windows.Forms.MouseEventHandler(this.listViewSMTP_MouseUp);
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
            this.toolStripSeparator2,
            this.toolStripMenuSmtpAdd,
            this.toolStripMenuSmtpEdit,
            this.toolStripMenuSmtpDelete});
            this.contextMenuSMTP.Name = "contextMenuList";
            this.contextMenuSMTP.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.contextMenuSMTP.ShowImageMargin = false;
            this.contextMenuSMTP.Size = new System.Drawing.Size(121, 98);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Enabled = false;
            this.toolStripMenuItem1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(120, 22);
            this.toolStripMenuItem1.Text = "SMTP Host";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(117, 6);
            // 
            // toolStripMenuSmtpAdd
            // 
            this.toolStripMenuSmtpAdd.Name = "toolStripMenuSmtpAdd";
            this.toolStripMenuSmtpAdd.Size = new System.Drawing.Size(120, 22);
            this.toolStripMenuSmtpAdd.Text = "Add Item";
            this.toolStripMenuSmtpAdd.Click += new System.EventHandler(this.toolMenuSmtpAdd_Click);
            // 
            // toolStripMenuSmtpEdit
            // 
            this.toolStripMenuSmtpEdit.Name = "toolStripMenuSmtpEdit";
            this.toolStripMenuSmtpEdit.Size = new System.Drawing.Size(120, 22);
            this.toolStripMenuSmtpEdit.Text = "Edit Item";
            this.toolStripMenuSmtpEdit.Click += new System.EventHandler(this.toolMenuSmtpEdit_Click);
            // 
            // toolStripMenuSmtpDelete
            // 
            this.toolStripMenuSmtpDelete.Name = "toolStripMenuSmtpDelete";
            this.toolStripMenuSmtpDelete.Size = new System.Drawing.Size(120, 22);
            this.toolStripMenuSmtpDelete.Text = "Delete Item";
            this.toolStripMenuSmtpDelete.Click += new System.EventHandler(this.toolMenuSmtpDelete_Click);
            // 
            // tabPageAddresses
            // 
            this.tabPageAddresses.Controls.Add(this.listViewAddresses);
            this.tabPageAddresses.Location = new System.Drawing.Point(4, 4);
            this.tabPageAddresses.Name = "tabPageAddresses";
            this.tabPageAddresses.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageAddresses.Size = new System.Drawing.Size(354, 174);
            this.tabPageAddresses.TabIndex = 4;
            this.tabPageAddresses.Text = "  Addresses  ";
            this.tabPageAddresses.UseVisualStyleBackColor = true;
            // 
            // listViewAddresses
            // 
            this.listViewAddresses.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
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
            this.helpProvider.SetShowHelp(this.listViewAddresses, true);
            this.listViewAddresses.Size = new System.Drawing.Size(354, 172);
            this.listViewAddresses.TabIndex = 12;
            this.listViewAddresses.TabStop = false;
            this.listViewAddresses.UseCompatibleStateImageBehavior = false;
            this.listViewAddresses.View = System.Windows.Forms.View.Details;
            this.listViewAddresses.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.listViewAddresses_ItemChecked);
            this.listViewAddresses.DoubleClick += new System.EventHandler(this.listView_DoubleClick);
            this.listViewAddresses.MouseUp += new System.Windows.Forms.MouseEventHandler(this.listViewAddresses_MouseUp);
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
            this.toolStripMenuItem2,
            this.toolStripSeparator3,
            this.toolStripMenuAddressesAdd,
            this.toolStripMenuAddressesEdit,
            this.toolStripMenuAddressesDelete});
            this.contextMenuAddresses.Name = "contextMenuList";
            this.contextMenuAddresses.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.contextMenuAddresses.ShowImageMargin = false;
            this.contextMenuAddresses.Size = new System.Drawing.Size(144, 98);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripMenuItem2.Enabled = false;
            this.toolStripMenuItem2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(143, 22);
            this.toolStripMenuItem2.Text = "E-Mail Address";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(140, 6);
            // 
            // toolStripMenuAddressesAdd
            // 
            this.toolStripMenuAddressesAdd.Name = "toolStripMenuAddressesAdd";
            this.toolStripMenuAddressesAdd.Size = new System.Drawing.Size(143, 22);
            this.toolStripMenuAddressesAdd.Text = "Add Item";
            this.toolStripMenuAddressesAdd.Click += new System.EventHandler(this.toolMenuAddressAdd_Click);
            // 
            // toolStripMenuAddressesEdit
            // 
            this.toolStripMenuAddressesEdit.Name = "toolStripMenuAddressesEdit";
            this.toolStripMenuAddressesEdit.Size = new System.Drawing.Size(143, 22);
            this.toolStripMenuAddressesEdit.Text = "Edit Item";
            this.toolStripMenuAddressesEdit.Click += new System.EventHandler(this.toolMenuAdressesEdit_Click);
            // 
            // toolStripMenuAddressesDelete
            // 
            this.toolStripMenuAddressesDelete.Name = "toolStripMenuAddressesDelete";
            this.toolStripMenuAddressesDelete.Size = new System.Drawing.Size(143, 22);
            this.toolStripMenuAddressesDelete.Text = "Delete Item";
            this.toolStripMenuAddressesDelete.Click += new System.EventHandler(this.toolMenuAddressDelete_Click);
            // 
            // tabPageConnections
            // 
            this.tabPageConnections.Controls.Add(this.listViewConnections);
            this.tabPageConnections.Location = new System.Drawing.Point(4, 4);
            this.tabPageConnections.Name = "tabPageConnections";
            this.tabPageConnections.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageConnections.Size = new System.Drawing.Size(354, 174);
            this.tabPageConnections.TabIndex = 3;
            this.tabPageConnections.Text = "  Connections  ";
            this.tabPageConnections.UseVisualStyleBackColor = true;
            // 
            // listViewConnections
            // 
            this.listViewConnections.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
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
            this.helpProvider.SetShowHelp(this.listViewConnections, true);
            this.listViewConnections.Size = new System.Drawing.Size(354, 172);
            this.listViewConnections.TabIndex = 11;
            this.listViewConnections.TabStop = false;
            this.listViewConnections.UseCompatibleStateImageBehavior = false;
            this.listViewConnections.View = System.Windows.Forms.View.Details;
            this.listViewConnections.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.listViewConnections_ItemChecked);
            this.listViewConnections.DoubleClick += new System.EventHandler(this.listView_DoubleClick);
            this.listViewConnections.MouseUp += new System.Windows.Forms.MouseEventHandler(this.listViewConnections_MouseUp);
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
            this.toolStripMenuItem3,
            this.toolStripSeparator4,
            this.toolStripMenuConnectionsAdd,
            this.toolStripMenuConnectionsEdit,
            this.toolStripMenuConnectionsDelete});
            this.contextMenuConnections.Name = "contextMenuList";
            this.contextMenuConnections.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.contextMenuConnections.ShowImageMargin = false;
            this.contextMenuConnections.Size = new System.Drawing.Size(124, 98);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripMenuItem3.Enabled = false;
            this.toolStripMenuItem3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(123, 22);
            this.toolStripMenuItem3.Text = "Connection";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(120, 6);
            // 
            // toolStripMenuConnectionsAdd
            // 
            this.toolStripMenuConnectionsAdd.Name = "toolStripMenuConnectionsAdd";
            this.toolStripMenuConnectionsAdd.Size = new System.Drawing.Size(123, 22);
            this.toolStripMenuConnectionsAdd.Text = "Add Item";
            this.toolStripMenuConnectionsAdd.Click += new System.EventHandler(this.toolMenuConnectionsAdd_Click);
            // 
            // toolStripMenuConnectionsEdit
            // 
            this.toolStripMenuConnectionsEdit.Name = "toolStripMenuConnectionsEdit";
            this.toolStripMenuConnectionsEdit.Size = new System.Drawing.Size(123, 22);
            this.toolStripMenuConnectionsEdit.Text = "Edit Item";
            this.toolStripMenuConnectionsEdit.Click += new System.EventHandler(this.toolMenuConnectionsEdit_Click);
            // 
            // toolStripMenuConnectionsDelete
            // 
            this.toolStripMenuConnectionsDelete.Name = "toolStripMenuConnectionsDelete";
            this.toolStripMenuConnectionsDelete.Size = new System.Drawing.Size(123, 22);
            this.toolStripMenuConnectionsDelete.Text = "Delete Item";
            this.toolStripMenuConnectionsDelete.Click += new System.EventHandler(this.toolMenuConnectionsDelete_Click);
            // 
            // tabLogs
            // 
            this.tabLogs.Controls.Add(this.textLog);
            this.tabLogs.Location = new System.Drawing.Point(4, 4);
            this.tabLogs.Name = "tabLogs";
            this.tabLogs.Padding = new System.Windows.Forms.Padding(3);
            this.tabLogs.Size = new System.Drawing.Size(354, 174);
            this.tabLogs.TabIndex = 1;
            this.tabLogs.Text = "  Logs  ";
            this.tabLogs.UseVisualStyleBackColor = true;
            // 
            // textLog
            // 
            this.textLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textLog.BackColor = System.Drawing.Color.White;
            this.textLog.ContextMenuStrip = this.contextMenuLogs;
            this.textLog.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textLog.Location = new System.Drawing.Point(0, 0);
            this.textLog.Name = "textLog";
            this.textLog.ReadOnly = true;
            this.textLog.Size = new System.Drawing.Size(354, 172);
            this.textLog.TabIndex = 0;
            this.textLog.TabStop = false;
            this.textLog.Text = "";
            this.textLog.WordWrap = false;
            this.textLog.TextChanged += new System.EventHandler(this.textLog_TextChanged);
            // 
            // contextMenuLogs
            // 
            this.contextMenuLogs.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clearConsoleToolStripMenuItem});
            this.contextMenuLogs.Name = "contextMenuLogs";
            this.contextMenuLogs.Size = new System.Drawing.Size(152, 26);
            // 
            // clearConsoleToolStripMenuItem
            // 
            this.clearConsoleToolStripMenuItem.Name = "clearConsoleToolStripMenuItem";
            this.clearConsoleToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.clearConsoleToolStripMenuItem.Text = "Clear Console";
            this.clearConsoleToolStripMenuItem.Click += new System.EventHandler(this.clearConsoleToolStripMenuItem_Click);
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
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(397, 49);
            this.button1.Name = "button1";
            this.helpProvider.SetShowHelp(this.button1, false);
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 18;
            this.button1.Text = "test";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Location = new System.Drawing.Point(492, 49);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 19;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Visible = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // menuStrip
            // 
            this.menuStrip.BackColor = System.Drawing.Color.Transparent;
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.configurationToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.menuStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(595, 24);
            this.menuStrip.TabIndex = 20;
            this.menuStrip.Text = "Main Menu";
            // 
            // configurationToolStripMenuItem
            // 
            this.configurationToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pop3ServerToolStripMenuItem,
            this.smtpServerToolStripMenuItem,
            this.addressesToolStripMenuItem,
            this.connectionsToolStripMenuItem});
            this.configurationToolStripMenuItem.Name = "configurationToolStripMenuItem";
            this.configurationToolStripMenuItem.Size = new System.Drawing.Size(84, 20);
            this.configurationToolStripMenuItem.Text = "Configuration";
            // 
            // pop3ServerToolStripMenuItem
            // 
            this.pop3ServerToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addPOP3ServerToolStripMenuItem,
            this.deletePOP3ServerToolStripMenuItem});
            this.pop3ServerToolStripMenuItem.Name = "pop3ServerToolStripMenuItem";
            this.pop3ServerToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.pop3ServerToolStripMenuItem.Text = "POP3 Server";
            // 
            // addPOP3ServerToolStripMenuItem
            // 
            this.addPOP3ServerToolStripMenuItem.Name = "addPOP3ServerToolStripMenuItem";
            this.addPOP3ServerToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.addPOP3ServerToolStripMenuItem.Text = "Add Server";
            // 
            // deletePOP3ServerToolStripMenuItem
            // 
            this.deletePOP3ServerToolStripMenuItem.Name = "deletePOP3ServerToolStripMenuItem";
            this.deletePOP3ServerToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.deletePOP3ServerToolStripMenuItem.Text = "Delete Server";
            // 
            // smtpServerToolStripMenuItem
            // 
            this.smtpServerToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addSMTPServerToolStripMenuItem,
            this.deleteSMTPServerToolStripMenuItem});
            this.smtpServerToolStripMenuItem.Name = "smtpServerToolStripMenuItem";
            this.smtpServerToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.smtpServerToolStripMenuItem.Text = "SMTP Server";
            // 
            // addSMTPServerToolStripMenuItem
            // 
            this.addSMTPServerToolStripMenuItem.Name = "addSMTPServerToolStripMenuItem";
            this.addSMTPServerToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.addSMTPServerToolStripMenuItem.Text = "Add Server";
            // 
            // deleteSMTPServerToolStripMenuItem
            // 
            this.deleteSMTPServerToolStripMenuItem.Name = "deleteSMTPServerToolStripMenuItem";
            this.deleteSMTPServerToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.deleteSMTPServerToolStripMenuItem.Text = "Delete Server";
            // 
            // addressesToolStripMenuItem
            // 
            this.addressesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addAddressToolStripMenuItem,
            this.deleteAddressToolStripMenuItem});
            this.addressesToolStripMenuItem.Name = "addressesToolStripMenuItem";
            this.addressesToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.addressesToolStripMenuItem.Text = "Addresses";
            // 
            // addAddressToolStripMenuItem
            // 
            this.addAddressToolStripMenuItem.Name = "addAddressToolStripMenuItem";
            this.addAddressToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.addAddressToolStripMenuItem.Text = "Add Address";
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
            this.connectionsToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.connectionsToolStripMenuItem.Text = "Connections";
            // 
            // addConnectionToolStripMenuItem
            // 
            this.addConnectionToolStripMenuItem.Name = "addConnectionToolStripMenuItem";
            this.addConnectionToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.addConnectionToolStripMenuItem.Text = "Add Connection";
            // 
            // deleteConnectionToolStripMenuItem
            // 
            this.deleteConnectionToolStripMenuItem.Name = "deleteConnectionToolStripMenuItem";
            this.deleteConnectionToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.deleteConnectionToolStripMenuItem.Text = "Delete Connection";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.checkForUpdateToolStripMenuItem,
            this.revisionsToolStripMenuItem,
            this.aboutThisProgramToolStripMenuItem});
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.aboutToolStripMenuItem.Text = "About";
            // 
            // checkForUpdateToolStripMenuItem
            // 
            this.checkForUpdateToolStripMenuItem.Name = "checkForUpdateToolStripMenuItem";
            this.checkForUpdateToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.checkForUpdateToolStripMenuItem.Text = "Check For Update";
            this.checkForUpdateToolStripMenuItem.Click += new System.EventHandler(this.checkForUpdateToolStripMenuItem_Click);
            // 
            // revisionsToolStripMenuItem
            // 
            this.revisionsToolStripMenuItem.Name = "revisionsToolStripMenuItem";
            this.revisionsToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.revisionsToolStripMenuItem.Text = "Revisions";
            this.revisionsToolStripMenuItem.Click += new System.EventHandler(this.revisionsToolStripMenuItem_Click);
            // 
            // aboutThisProgramToolStripMenuItem
            // 
            this.aboutThisProgramToolStripMenuItem.Name = "aboutThisProgramToolStripMenuItem";
            this.aboutThisProgramToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.aboutThisProgramToolStripMenuItem.Text = "About This Program";
            this.aboutThisProgramToolStripMenuItem.Click += new System.EventHandler(this.aboutThisProgramToolStripMenuItem_Click);
            // 
            // threadComManager
            // 
            this.threadComManager.WorkerReportsProgress = true;
            this.threadComManager.WorkerSupportsCancellation = true;
            this.threadComManager.DoWork += new System.ComponentModel.DoWorkEventHandler(this.threadComManager_DoWork);
            this.threadComManager.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.threadComManager_RunWorkerCompleted);
            this.threadComManager.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.threadComManager_ProgressChanged);
            // 
            // threadPOP3
            // 
            this.threadPOP3.WorkerReportsProgress = true;
            this.threadPOP3.WorkerSupportsCancellation = true;
            this.threadPOP3.DoWork += new System.ComponentModel.DoWorkEventHandler(this.threadPOP3_DoWork);
            this.threadPOP3.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.threadPOP3_RunWorkerCompleted);
            this.threadPOP3.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.threadPOP3_ProgressChanged);
            // 
            // threadSMTP
            // 
            this.threadSMTP.WorkerReportsProgress = true;
            this.threadSMTP.WorkerSupportsCancellation = true;
            this.threadSMTP.DoWork += new System.ComponentModel.DoWorkEventHandler(this.threadSMTP_DoWork);
            this.threadSMTP.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.threadSMTP_RunWorkerCompleted);
            this.threadSMTP.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.threadSMTP_ProgressChanged);
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button3.Location = new System.Drawing.Point(397, 117);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(170, 23);
            this.button3.TabIndex = 21;
            this.button3.Text = "Logger messages";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Visible = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // loadingCircle
            // 
            this.loadingCircle.Active = false;
            this.loadingCircle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.loadingCircle.BackColor = System.Drawing.Color.Transparent;
            this.loadingCircle.Color = System.Drawing.Color.MintCream;
            this.loadingCircle.FadeInSpeed = MRG.Controls.UI.LoadingCircle.FadeSpeeds.Slower;
            this.loadingCircle.FadeOutSpeed = MRG.Controls.UI.LoadingCircle.FadeSpeeds.Slower;
            this.loadingCircle.InnerCircleRadius = 8;
            this.loadingCircle.Location = new System.Drawing.Point(553, 4);
            this.loadingCircle.Name = "loadingCircle";
            this.loadingCircle.NumberSpoke = 24;
            this.loadingCircle.OuterCircleRadius = 9;
            this.loadingCircle.RotationSpeed = 50;
            this.loadingCircle.Size = new System.Drawing.Size(38, 39);
            this.loadingCircle.SpokeThickness = 4;
            this.loadingCircle.StylePreset = MRG.Controls.UI.LoadingCircle.StylePresets.IE7;
            this.loadingCircle.TabIndex = 22;
            this.loadingCircle.Text = "loadingCircle";
            this.loadingCircle.Visible = false;
            // 
            // button4
            // 
            this.button4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button4.Location = new System.Drawing.Point(397, 88);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 23;
            this.button4.Text = "Show Circle";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button5.ForeColor = System.Drawing.Color.Black;
            this.button5.Location = new System.Drawing.Point(492, 88);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(75, 23);
            this.button5.TabIndex = 24;
            this.button5.Text = "Hide Circle";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button6
            // 
            this.button6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button6.Location = new System.Drawing.Point(397, 172);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(159, 23);
            this.button6.TabIndex = 25;
            this.button6.Text = "Test SMTP Automatic";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button7
            // 
            this.button7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button7.Location = new System.Drawing.Point(397, 143);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(159, 23);
            this.button7.TabIndex = 26;
            this.button7.Text = "Test Read/Write UID";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::POP3Pipe.Properties.Resources.background;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(595, 266);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.loadingCircle);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.menuStrip);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.button6);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip;
            this.Name = "MainWindow";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "POP3 === Pipe";
            this.Deactivate += new System.EventHandler(this.MainWindow_Deactivate);
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainWindow_FormClosed);
            this.contextMenuPOP3.ResumeLayout(false);
            this.tabControl.ResumeLayout(false);
            this.tabPagePOP3.ResumeLayout(false);
            this.tabPageSMTP.ResumeLayout(false);
            this.contextMenuSMTP.ResumeLayout(false);
            this.tabPageAddresses.ResumeLayout(false);
            this.contextMenuAddresses.ResumeLayout(false);
            this.tabPageConnections.ResumeLayout(false);
            this.contextMenuConnections.ResumeLayout(false);
            this.tabLogs.ResumeLayout(false);
            this.contextMenuLogs.ResumeLayout(false);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.ListView listViewPOP3;
        private System.Windows.Forms.ColumnHeader colPOP3User;
        private System.Windows.Forms.ContextMenuStrip contextMenuPOP3;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuPop3Edit;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuPop3Delete;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPagePOP3;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader colPOP3Host;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuPop3Add;
        private System.Windows.Forms.TabPage tabLogs;
        private System.Windows.Forms.RichTextBox textLog;
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
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ColumnHeader colPOP3Active;
        private System.Windows.Forms.ColumnHeader colSMTPActive;
        private System.Windows.Forms.ColumnHeader colAddressesActive;
        private System.Windows.Forms.ColumnHeader colConnectionsActive;
        private System.Windows.Forms.ContextMenuStrip contextMenuSMTP;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuSmtpAdd;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuSmtpEdit;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuSmtpDelete;
        private System.Windows.Forms.ContextMenuStrip contextMenuAddresses;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuAddressesAdd;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuAddressesEdit;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuAddressesDelete;
        private System.Windows.Forms.ContextMenuStrip contextMenuConnections;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuConnectionsAdd;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuConnectionsEdit;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuConnectionsDelete;
        private System.Windows.Forms.ToolStripMenuItem pop3HostToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.HelpProvider helpProvider;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem checkForUpdateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem revisionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutThisProgramToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem configurationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem connectionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addConnectionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteConnectionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addressesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addAddressToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteAddressToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem smtpServerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addSMTPServerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteSMTPServerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pop3ServerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addPOP3ServerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deletePOP3ServerToolStripMenuItem;
        private System.ComponentModel.BackgroundWorker threadComManager;
        private System.ComponentModel.BackgroundWorker threadPOP3;
        private System.ComponentModel.BackgroundWorker threadSMTP;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.ContextMenuStrip contextMenuLogs;
        private System.Windows.Forms.ToolStripMenuItem clearConsoleToolStripMenuItem;
        private MRG.Controls.UI.LoadingCircle loadingCircle;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button7;
    }
}

