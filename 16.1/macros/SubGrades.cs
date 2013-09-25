// Created by Dale Nicholls
// Calculates Subgrades for plates and copies them to the clipboard ready to paste.
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

namespace Tekla.Technology.Akit.UserScript
{
    public class TeklaForm : System.Windows.Forms.Form
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private ComboBox comboBox1;
        private ComboBox comboBox2;
        private TextBox textBox1;

        static Tekla.Technology.Akit.IScript akit;

        public TeklaForm(Tekla.Technology.Akit.IScript RunMe)
        {
            InitializeComponent();

            akit = RunMe;
        }

        private void InitializeComponent()
        {
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "S355",
            "S275"});
            this.comboBox1.Location = new System.Drawing.Point(12, 12);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(128, 21);
            this.comboBox1.TabIndex = 0;
            // 
            // comboBox2
            // 
            this.comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Items.AddRange(new object[] {
            "External",
            "Internal"});
            this.comboBox2.Location = new System.Drawing.Point(146, 12);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(128, 21);
            this.comboBox2.TabIndex = 1;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 39);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(262, 20);
            this.textBox1.TabIndex = 2;
            this.textBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyDown);
            // 
            // TeklaForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 69);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(this.comboBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "TeklaForm";
            this.Text = "SubGrades";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.TeklaForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (comboBox1.SelectedItem.ToString() != "" && comboBox2.SelectedItem.ToString() != "" && textBox1.Text != "")
                    {
                        double thk; bool result = double.TryParse(textBox1.Text, out thk);
                        if (thk > 0)
                        {
                            string strGrade = "";
                            if (comboBox1.SelectedItem.ToString() == "S275")
                            {
                                /*
                                 * 275 - EXTERNAL
                                 * >=79 
                                 * >= 55 & <79
                                 * < 55

                                 * 275 - INTERNAL
                                 * >= 95
                                 * >= 66 & < 95
                                 * >= 31 & < 66
                                 * < 31
                                 */

                                //if (comboBox2.SelectedItem.ToString() == "Internal")
                                //{
                                //    if (thk <= 25.9)
                                //        strGrade = "";
                                //    else if (thk <= 46.9)
                                //        strGrade = "";
                                //    else if (thk <= 66.9)
                                //        strGrade = "";
                                //    else if (thk <= 79.9)
                                //        strGrade = "";
                                //    else if (thk <= 114.9)
                                //        strGrade = "";
                                //}

                                //if (comboBox2.SelectedItem.ToString() == "External")
                                //{
                                //    if (thk <= 14.9)
                                //        strGrade = "";
                                //    else if (thk <= 38.9)
                                //        strGrade = "";
                                //    else if (thk <= 55.9)
                                //        strGrade = "";
                                //    else if (thk <= 66.9)
                                //        strGrade = "";
                                //    else if (thk <= 95.9)
                                //        strGrade = "";
                                //}
                            }

                            if (comboBox1.SelectedItem.ToString() == "S355")
                            {
                                if (comboBox2.SelectedItem.ToString() == "Internal")
                                {
                                    if (thk <= 25.9) // JR <= 25.9
                                        strGrade = "S355JR";
                                    else if (thk <= 46.9) // JO <= 46.9
                                        strGrade = "S355JO";
                                    else if (thk <= 66.9) // J2G4 <= 66.9
                                        strGrade = "S355J2G4";
                                    else if (thk <= 79.9) // K2G3 <= 79.9
                                        strGrade = "S355K2G3";
                                    else if (thk <= 114.9) // NL <= 114.9
                                        strGrade = "S355NL";
                                    else
                                        strGrade = "special";
                                }

                                if (comboBox2.SelectedItem.ToString() == "External")
                                {
                                    if (thk <= 14.9) // JR <= 14.9
                                        strGrade = "S355JR";
                                    else if (thk <= 38.9) // JO <= 38.9
                                        strGrade = "S355JO";
                                    else if (thk <= 55.9) // J2G4 <= 55.9
                                        strGrade = "S355J2G4";
                                    else if (thk <= 66.9) // K2G3 <= 66.9
                                        strGrade = "S355K2G3";
                                    else if (thk <= 95.9) // NL <= 95.9
                                        strGrade = "S355NL";
                                }
                            }

                            Clipboard.SetDataObject(strGrade);
                        }
                    }
                }
            }
            catch { }
        }

        private void TeklaForm_Load(object sender, EventArgs e)
        {

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