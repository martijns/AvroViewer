namespace AvroViewerGui
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
            this.ctxMenuPopup = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mainPanel = new System.Windows.Forms.Panel();
            this.resultPanel = new System.Windows.Forms.Panel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.filterTextBox = new System.Windows.Forms.TextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportToCsvToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyAllToClipboardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportAllTojsonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportSelectionToCsvToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copySelectionToClipboardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportSelectionTojsonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.changelogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.submitFeedbackToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripSplitButton1 = new System.Windows.Forms.ToolStripSplitButton();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.ctxMenuPopup.SuspendLayout();
            this.mainPanel.SuspendLayout();
            this.resultPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // ctxMenuPopup
            // 
            this.ctxMenuPopup.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cutToolStripMenuItem,
            this.copyToolStripMenuItem,
            this.pasteToolStripMenuItem});
            this.ctxMenuPopup.Name = "ctxMenuPopup";
            this.ctxMenuPopup.Size = new System.Drawing.Size(103, 70);
            // 
            // cutToolStripMenuItem
            // 
            this.cutToolStripMenuItem.Name = "cutToolStripMenuItem";
            this.cutToolStripMenuItem.Size = new System.Drawing.Size(102, 22);
            this.cutToolStripMenuItem.Text = "Cut";
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(102, 22);
            this.copyToolStripMenuItem.Text = "Copy";
            // 
            // pasteToolStripMenuItem
            // 
            this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            this.pasteToolStripMenuItem.Size = new System.Drawing.Size(102, 22);
            this.pasteToolStripMenuItem.Text = "Paste";
            // 
            // mainPanel
            // 
            this.mainPanel.Controls.Add(this.resultPanel);
            this.mainPanel.Controls.Add(this.flowLayoutPanel1);
            this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPanel.Location = new System.Drawing.Point(0, 24);
            this.mainPanel.Margin = new System.Windows.Forms.Padding(2);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(1143, 371);
            this.mainPanel.TabIndex = 1;
            // 
            // resultPanel
            // 
            this.resultPanel.Controls.Add(this.dataGridView1);
            this.resultPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.resultPanel.Location = new System.Drawing.Point(0, 42);
            this.resultPanel.Name = "resultPanel";
            this.resultPanel.Size = new System.Drawing.Size(1143, 329);
            this.resultPanel.TabIndex = 2;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.ShowCellErrors = false;
            this.dataGridView1.ShowCellToolTips = false;
            this.dataGridView1.ShowEditingIcon = false;
            this.dataGridView1.ShowRowErrors = false;
            this.dataGridView1.Size = new System.Drawing.Size(1143, 329);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnKeyDown);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel1.Controls.Add(this.filterTextBox);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(2);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Padding = new System.Windows.Forms.Padding(8);
            this.flowLayoutPanel1.Size = new System.Drawing.Size(1143, 42);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // filterTextBox
            // 
            this.filterTextBox.Location = new System.Drawing.Point(11, 11);
            this.filterTextBox.Name = "filterTextBox";
            this.filterTextBox.Size = new System.Drawing.Size(100, 20);
            this.filterTextBox.TabIndex = 8;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.exportToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(1143, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.openToolStripMenuItem.Text = "Open...";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.HandleOpenClicked);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(109, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.HandleExitClick);
            // 
            // exportToolStripMenuItem
            // 
            this.exportToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exportToCsvToolStripMenuItem,
            this.copyAllToClipboardToolStripMenuItem,
            this.exportAllTojsonToolStripMenuItem,
            this.toolStripSeparator2,
            this.exportSelectionToCsvToolStripMenuItem,
            this.copySelectionToClipboardToolStripMenuItem,
            this.exportSelectionTojsonToolStripMenuItem});
            this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
            this.exportToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.exportToolStripMenuItem.Text = "Export";
            // 
            // exportToCsvToolStripMenuItem
            // 
            this.exportToCsvToolStripMenuItem.Name = "exportToCsvToolStripMenuItem";
            this.exportToCsvToolStripMenuItem.Size = new System.Drawing.Size(287, 22);
            this.exportToCsvToolStripMenuItem.Text = "Export all to .csv";
            this.exportToCsvToolStripMenuItem.Click += new System.EventHandler(this.exportToCsvToolStripMenuItem_Click);
            // 
            // copyAllToClipboardToolStripMenuItem
            // 
            this.copyAllToClipboardToolStripMenuItem.Name = "copyAllToClipboardToolStripMenuItem";
            this.copyAllToClipboardToolStripMenuItem.Size = new System.Drawing.Size(287, 22);
            this.copyAllToClipboardToolStripMenuItem.Text = "Export all to clipboard";
            this.copyAllToClipboardToolStripMenuItem.Click += new System.EventHandler(this.copyAllToClipboardToolStripMenuItem_Click);
            // 
            // exportAllTojsonToolStripMenuItem
            // 
            this.exportAllTojsonToolStripMenuItem.Name = "exportAllTojsonToolStripMenuItem";
            this.exportAllTojsonToolStripMenuItem.Size = new System.Drawing.Size(310, 22);
            this.exportAllTojsonToolStripMenuItem.Text = "Export all to .json (body column only!)";
            this.exportAllTojsonToolStripMenuItem.Click += new System.EventHandler(this.HandleExportAllToJson);
            // 
            // exportSelectionToCsvToolStripMenuItem
            // 
            this.exportSelectionToCsvToolStripMenuItem.Name = "exportSelectionToCsvToolStripMenuItem";
            this.exportSelectionToCsvToolStripMenuItem.Size = new System.Drawing.Size(287, 22);
            this.exportSelectionToCsvToolStripMenuItem.Text = "Export selection to .csv";
            this.exportSelectionToCsvToolStripMenuItem.Click += new System.EventHandler(this.exportSelectionToCsvToolStripMenuItem_Click);
            // 
            // copySelectionToClipboardToolStripMenuItem
            // 
            this.copySelectionToClipboardToolStripMenuItem.Name = "copySelectionToClipboardToolStripMenuItem";
            this.copySelectionToClipboardToolStripMenuItem.Size = new System.Drawing.Size(287, 22);
            this.copySelectionToClipboardToolStripMenuItem.Text = "Export selection to clipboard";
            this.copySelectionToClipboardToolStripMenuItem.Click += new System.EventHandler(this.copySelectionToClipboardToolStripMenuItem_Click);
            // 
            // exportSelectionTojsonToolStripMenuItem
            // 
            this.exportSelectionTojsonToolStripMenuItem.Name = "exportSelectionTojsonToolStripMenuItem";
            this.exportSelectionTojsonToolStripMenuItem.Size = new System.Drawing.Size(310, 22);
            this.exportSelectionTojsonToolStripMenuItem.Text = "Export selection to .json (body column only!)";
            this.exportSelectionTojsonToolStripMenuItem.Click += new System.EventHandler(this.HandleExportSelectionToJson);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.changelogToolStripMenuItem,
            this.submitFeedbackToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // changelogToolStripMenuItem
            // 
            this.changelogToolStripMenuItem.Name = "changelogToolStripMenuItem";
            this.changelogToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.changelogToolStripMenuItem.Text = "Changelog...";
            this.changelogToolStripMenuItem.Click += new System.EventHandler(this.HandleChangelogClicked);
            // 
            // submitFeedbackToolStripMenuItem
            // 
            this.submitFeedbackToolStripMenuItem.Name = "submitFeedbackToolStripMenuItem";
            this.submitFeedbackToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.submitFeedbackToolStripMenuItem.Text = "Submit feedback...";
            this.submitFeedbackToolStripMenuItem.Click += new System.EventHandler(this.HandleSubmitFeedbackClicked);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.aboutToolStripMenuItem.Text = "About...";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.HandleAboutClick);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatus,
            this.toolStripSplitButton1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 373);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1143, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Tag = "KEEP_ENABLED";
            this.statusStrip1.Text = "statusStrip1";
            this.statusStrip1.Visible = false;
            // 
            // lblStatus
            // 
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(48, 17);
            this.lblStatus.Text = "Status...";
            // 
            // toolStripSplitButton1
            // 
            this.toolStripSplitButton1.DropDownButtonWidth = 0;
            this.toolStripSplitButton1.Image = global::AvroViewerGui.Properties.Resources.corssbtn;
            this.toolStripSplitButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripSplitButton1.Name = "toolStripSplitButton1";
            this.toolStripSplitButton1.Size = new System.Drawing.Size(95, 20);
            this.toolStripSplitButton1.Tag = "KEEP_ENABLED";
            this.toolStripSplitButton1.Text = "Stop loading";
            this.toolStripSplitButton1.ToolTipText = "Stop loading";
            this.toolStripSplitButton1.ButtonClick += new System.EventHandler(this.HandleAbortClicked);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(307, 6);
            // 
            // MainForm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1143, 395);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.mainPanel);
            this.Controls.Add(this.menuStrip1);
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.WindowsDefaultBounds;
            this.Text = "AvroViewer";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.HandleFormClosed);
            this.Load += new System.EventHandler(this.HandleLoad);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.HandleDragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.HandleDragEnter);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnKeyDown);
            this.ctxMenuPopup.ResumeLayout(false);
            this.mainPanel.ResumeLayout(false);
            this.mainPanel.PerformLayout();
            this.resultPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.Panel mainPanel;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.TextBox filterTextBox;
        private System.Windows.Forms.ToolStripMenuItem exportToCsvToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportSelectionToCsvToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip ctxMenuPopup;
        private System.Windows.Forms.ToolStripMenuItem cutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copySelectionToClipboardToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyAllToClipboardToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem changelogToolStripMenuItem;
        private System.Windows.Forms.Panel resultPanel;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblStatus;
        private System.Windows.Forms.ToolStripSplitButton toolStripSplitButton1;
        private System.Windows.Forms.ToolStripMenuItem submitFeedbackToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.BindingSource bindingSource1;
        private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportAllTojsonToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportSelectionTojsonToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    }
}

