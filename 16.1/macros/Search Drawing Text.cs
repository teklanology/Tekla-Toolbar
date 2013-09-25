// Created by Dale Nicholls
// 
// http://dalenicholls.webs.com
// http://twitter.com/dalenicholls

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Tekla.Technology.Akit;
using Tekla.Structures;
using Tekla.Structures.Drawing;
using Tekla.Structures.Geometry3d;
using Tekla.Structures.Model;
using System.IO;

namespace Tekla.Technology.Akit.UserScript
{
    public class TeklaForm : System.Windows.Forms.Form
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnLeader;
        private System.Windows.Forms.Button btnText;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFileName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colText;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label label1;

        Model model = new Model();
        public string XS_SYSTEM = "";
        public string XS_MODEL_DIRECTORY_ATTRIBUTES = "";
        public string filename = "";

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        static Tekla.Technology.Akit.IScript akit;

        public TeklaForm(Tekla.Technology.Akit.IScript RunMe)
        {
            InitializeComponent();

            akit = RunMe;
        }

        private void InitializeComponent()
        {
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.colFileName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colText = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnLeader = new System.Windows.Forms.Button();
            this.btnText = new System.Windows.Forms.Button();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colFileName,
            this.colText});
            this.dataGridView1.Location = new System.Drawing.Point(12, 38);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.Size = new System.Drawing.Size(546, 167);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // colFileName
            // 
            this.colFileName.HeaderText = "Filename";
            this.colFileName.Name = "colFileName";
            this.colFileName.ReadOnly = true;
            this.colFileName.Width = 150;
            // 
            // colText
            // 
            this.colText.HeaderText = "Text";
            this.colText.Name = "colText";
            this.colText.ReadOnly = true;
            this.colText.Width = 350;
            // 
            // btnLeader
            // 
            this.btnLeader.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLeader.Location = new System.Drawing.Point(250, 211);
            this.btnLeader.Name = "btnLeader";
            this.btnLeader.Size = new System.Drawing.Size(146, 23);
            this.btnLeader.TabIndex = 1;
            this.btnLeader.Text = "Create text with leader line";
            this.btnLeader.UseVisualStyleBackColor = true;
            this.btnLeader.Click += new System.EventHandler(this.btnLeader_Click);
            // 
            // btnText
            // 
            this.btnText.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnText.Location = new System.Drawing.Point(402, 211);
            this.btnText.Name = "btnText";
            this.btnText.Size = new System.Drawing.Size(75, 23);
            this.btnText.TabIndex = 2;
            this.btnText.Text = "Create text";
            this.btnText.UseVisualStyleBackColor = true;
            this.btnText.Click += new System.EventHandler(this.btnText_Click);
            // 
            // btnBrowse
            // 
            this.btnBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowse.Location = new System.Drawing.Point(483, 211);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(75, 23);
            this.btnBrowse.TabIndex = 5;
            this.btnBrowse.Text = "Browse";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(200, 100);
            this.panel1.TabIndex = 0;
            // 
            // txtSearch
            // 
            this.txtSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearch.Location = new System.Drawing.Point(59, 12);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(499, 20);
            this.txtSearch.TabIndex = 7;
            this.txtSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSearch_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Search";
            // 
            // TeklaForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(570, 246);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btnLeader);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.btnText);
            this.Name = "TeklaForm";
            this.Text = "Search Drawing Text";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.TeklaForm_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TeklaForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void TeklaForm_Load(object sender, EventArgs e)
        {
            ModelInfo modelInfo = model.GetInfo();
            model.GetAdvancedOption("XS_SYSTEM", ref XS_SYSTEM);
            XS_MODEL_DIRECTORY_ATTRIBUTES = modelInfo.ModelPath + @"\attributes\";
            PopulateForm(XS_SYSTEM);
            PopulateForm(XS_MODEL_DIRECTORY_ATTRIBUTES);
            akit.Callback("acmd_display_attr_dialog", "text_dial", "main_frame");
        }

        private void PopulateForm(string folderPath)
        {
            try
            {
                DirectoryInfo di = new DirectoryInfo(folderPath);
                FileInfo[] fi = di.GetFiles("*.drtxt");
                foreach (FileInfo fiTemp in fi)
                {
                    string strFileName = fiTemp.Name;
                    int intFileNameLength = strFileName.Length - 6;
                    strFileName = fiTemp.Name.Substring(0, intFileNameLength);

                    using (StreamReader sr = new StreamReader(folderPath + fiTemp.Name))
                    {
                        sr.ReadLine();
                        sr.ReadLine();
                        string line = sr.ReadLine();
                        line = line.Substring(19);
                        int strLineLength = line.Length - 1;
                        line = line.Substring(0, strLineLength);
                        line = line.Replace("", " \n");
                        dataGridView1.Rows.Add(new string[] { strFileName, line });
                    }
                }
                int i = dataGridView1.SelectedCells[0].RowIndex;
                dataGridView1.Rows[i].Selected = true;
                dataGridView1.Sort(colFileName, ListSortDirection.Ascending);
            }
            catch { }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int i = dataGridView1.SelectedCells[0].RowIndex;
                dataGridView1.Rows[i].Selected = true;
                akit.Callback("acmd_display_attr_dialog", "text_dial", "main_frame");
                akit.ValueChange("text_dial", "gr_text_get_menu", dataGridView1.Rows[i].Cells[0].Value.ToString());
                akit.PushButton("gr_text_get", "text_dial");
                akit.PushButton("text_apply", "text_dial");
                //richTextBox1.Text = dataGridView1.Rows[i].Cells[1].Value.ToString();
                //filename = dataGridView1.Rows[i].Cells[0].Value.ToString();
            }
            catch { }
        }

        private void btnText_Click(object sender, EventArgs e)
        {
            akit.CommandStart("ail_create_text", "GR_TEXT0", "main_frame");
        }

        private void btnLeader_Click(object sender, EventArgs e)
        {
            akit.CommandStart("ail_create_text", "GR_TEXT1", "main_frame");
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            try
            {
                openFileDialog1.InitialDirectory = XS_MODEL_DIRECTORY_ATTRIBUTES;
                openFileDialog1.FileName = "";
                openFileDialog1.Filter = "text files (*.drtxt)|*.drtxt";
                openFileDialog1.FilterIndex = 2;
                openFileDialog1.RestoreDirectory = true;

                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    FileInfo fileToCopy = new FileInfo(openFileDialog1.FileName);
                    string strSourceFile = openFileDialog1.FileName;
                    string strDestFile = XS_MODEL_DIRECTORY_ATTRIBUTES + fileToCopy.Name;
                    File.Copy(strSourceFile, strDestFile);

                    using (StreamReader sr = new StreamReader(strDestFile))
                    {
                        sr.ReadLine();
                        sr.ReadLine();
                        string line = sr.ReadLine();
                        line = line.Substring(19);
                        int strLineLength = line.Length - 1;
                        line = line.Substring(0, strLineLength);
                        line = line.Replace("", " \n");
                        dataGridView1.Rows.Add(new string[] { fileToCopy.Name.Replace(".drtxt", ""), line });
                        dataGridView1.Sort(colFileName, ListSortDirection.Ascending);
                    }
                }
            }
            catch { }
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (txtSearch.Text != "")
                    {
                        foreach (DataGridViewRow temp in dataGridView1.Rows)
                        {
                            temp.Visible = true;
                            string tempstring = "";
                            tempstring = temp.Cells[1].Value as string;
                            if (tempstring.Contains(txtSearch.Text) || tempstring.Contains(txtSearch.Text.ToUpper()))
                                Console.WriteLine(temp.Index + " " + tempstring);
                            else
                                temp.Visible = false;
                        }
                    }
                    else
                    {
                        foreach (DataGridViewRow temp in dataGridView1.Rows)
                        {
                            temp.Visible = true;
                        }
                    }
                }
            }
            catch { }
        }

        private void TeklaForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            akit.PushButton("text_ok", "text_dial");
        }
    }

    public class Script
    {
        static Tekla.Technology.Akit.IScript akit;

        public static void Run(Tekla.Technology.Akit.IScript RunMe)
        {
            akit = RunMe;
            Application.Run(new TeklaForm(akit));
        }
    }
}