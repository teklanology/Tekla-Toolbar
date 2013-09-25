using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Tekla.Structures;
using Tekla.Structures.Model;
using Tekla.Structures.Drawing;

namespace TeklaToolbar
{
    public partial class TeklaToolbarForm : Form
    {
        public TeklaToolbarForm()
        {
            InitializeComponent();
        }

        Properties.Settings settings = new global::TeklaToolbar.Properties.Settings();

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Location = settings.location;
            TreeViewSerializer serializer = new TreeViewSerializer();
            serializer.PopulateMenu(this.menuStrip1);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            TreeViewSerializer serializer = new TreeViewSerializer();
            if (e.KeyCode == Keys.R) serializer.PopulateMenu(menuStrip1); // refresh toolbar
            if (e.KeyCode == Keys.C) ShowOptions(); // customise
        }

        private void ShowOptions()
        {
            CustomiseForm optionsForm = new CustomiseForm();
            optionsForm.ShowDialog();
        }

        private void TeklaToolbar_FormClosing(object sender, FormClosingEventArgs e)
        {
            settings.location = Form.ActiveForm.Location;
            settings.Save();
        }
    }
}