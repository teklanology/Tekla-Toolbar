namespace TeklaToolbar
{
    partial class CustomiseForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CustomiseForm));
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.newToolStripButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.newMainFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newSubFolderToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.helpToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.moveUpToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.moveDownToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.addMacroToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.addSeparatorToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStrip1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.LabelEdit = true;
            this.treeView1.Location = new System.Drawing.Point(0, 0);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(209, 308);
            this.treeView1.TabIndex = 0;
            this.treeView1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.treeView1_KeyDown);
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.listView1.Location = new System.Drawing.Point(0, 0);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(185, 308);
            this.listView1.TabIndex = 1;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Width = 200;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripButton,
            this.saveToolStripButton,
            this.toolStripSeparator,
            this.addMacroToolStripButton,
            this.addSeparatorToolStripButton,
            this.toolStripSeparator3,
            this.moveUpToolStripButton,
            this.moveDownToolStripButton,
            this.toolStripSeparator1,
            this.helpToolStripButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(398, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // newToolStripButton
            // 
            this.newToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.newToolStripButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newMainFolderToolStripMenuItem,
            this.newSubFolderToolStripMenuItem1});
            this.newToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("newToolStripButton.Image")));
            this.newToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.newToolStripButton.Name = "newToolStripButton";
            this.newToolStripButton.Size = new System.Drawing.Size(29, 22);
            this.newToolStripButton.Text = "&New";
            // 
            // newMainFolderToolStripMenuItem
            // 
            this.newMainFolderToolStripMenuItem.Name = "newMainFolderToolStripMenuItem";
            this.newMainFolderToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.newMainFolderToolStripMenuItem.Text = "New Main Folder";
            this.newMainFolderToolStripMenuItem.Click += new System.EventHandler(this.newMainFolderToolStripMenuItem_Click);
            // 
            // newSubFolderToolStripMenuItem1
            // 
            this.newSubFolderToolStripMenuItem1.Name = "newSubFolderToolStripMenuItem1";
            this.newSubFolderToolStripMenuItem1.Size = new System.Drawing.Size(164, 22);
            this.newSubFolderToolStripMenuItem1.Text = "New Sub Folder";
            this.newSubFolderToolStripMenuItem1.Click += new System.EventHandler(this.newSubFolderToolStripMenuItem1_Click);
            // 
            // saveToolStripButton
            // 
            this.saveToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.saveToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("saveToolStripButton.Image")));
            this.saveToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveToolStripButton.Name = "saveToolStripButton";
            this.saveToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.saveToolStripButton.Text = "&Save";
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(6, 25);
            // 
            // helpToolStripButton
            // 
            this.helpToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.helpToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("helpToolStripButton.Image")));
            this.helpToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.helpToolStripButton.Name = "helpToolStripButton";
            this.helpToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.helpToolStripButton.Text = "He&lp";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 25);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.listView1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.treeView1);
            this.splitContainer1.Size = new System.Drawing.Size(398, 308);
            this.splitContainer1.SplitterDistance = 185;
            this.splitContainer1.TabIndex = 3;
            // 
            // moveUpToolStripButton
            // 
            this.moveUpToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.moveUpToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("moveUpToolStripButton.Image")));
            this.moveUpToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.moveUpToolStripButton.Name = "moveUpToolStripButton";
            this.moveUpToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.moveUpToolStripButton.Text = "Move Up";
            this.moveUpToolStripButton.Click += new System.EventHandler(this.moveUpToolStripButton_Click);
            // 
            // moveDownToolStripButton
            // 
            this.moveDownToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.moveDownToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("moveDownToolStripButton.Image")));
            this.moveDownToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.moveDownToolStripButton.Name = "moveDownToolStripButton";
            this.moveDownToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.moveDownToolStripButton.Text = "Move Down";
            this.moveDownToolStripButton.Click += new System.EventHandler(this.moveDownToolStripButton_Click);
            // 
            // addMacroToolStripButton
            // 
            this.addMacroToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.addMacroToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("addMacroToolStripButton.Image")));
            this.addMacroToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.addMacroToolStripButton.Name = "addMacroToolStripButton";
            this.addMacroToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.addMacroToolStripButton.Text = "Add Macro";
            this.addMacroToolStripButton.Click += new System.EventHandler(this.addMacroToolStripButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // addSeparatorToolStripButton
            // 
            this.addSeparatorToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.addSeparatorToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("addSeparatorToolStripButton.Image")));
            this.addSeparatorToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.addSeparatorToolStripButton.Name = "addSeparatorToolStripButton";
            this.addSeparatorToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.addSeparatorToolStripButton.Text = "Add Separator";
            this.addSeparatorToolStripButton.Click += new System.EventHandler(this.addSeparatorToolStripButton_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // CustomiseForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(398, 333);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "CustomiseForm";
            this.Text = "Customise";
            this.Load += new System.EventHandler(this.Options_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OptionsForm_FormClosing);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton saveToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton helpToolStripButton;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ToolStripDropDownButton newToolStripButton;
        private System.Windows.Forms.ToolStripMenuItem newMainFolderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newSubFolderToolStripMenuItem1;
        private System.Windows.Forms.ToolStripButton moveUpToolStripButton;
        private System.Windows.Forms.ToolStripButton moveDownToolStripButton;
        private System.Windows.Forms.ToolStripButton addMacroToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton addSeparatorToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
    }
}