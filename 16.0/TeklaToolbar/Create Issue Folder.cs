using System;
using System.Windows.Forms;
using System.IO;
using Tekla.Structures.Model;

namespace Tekla.Technology.Akit.UserScript
{
	public class Form1 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;

		private System.ComponentModel.Container components = null;

		public Form1()
		{
		 InitializeComponent();
		}

		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region
		private void InitializeComponent()
		{		
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.label1 = new System.Windows.Forms.Label();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.textBox1);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Location = new System.Drawing.Point(8, 8);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(208, 54);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = " Create issue folders for Phase ";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 24);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(128, 16);
			this.label1.TabIndex = 0;
			this.label1.Text = "Phase Number";
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(128, 24);
			this.textBox1.MaxLength = 5;
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(64, 20);
			this.textBox1.TabIndex = 2;
			this.textBox1.Text = "";
			this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;			
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(8, 66);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(96, 32);
			this.button1.TabIndex = 1;
			this.button1.Text = "Ok";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(120, 66);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(96, 32);
			this.button2.TabIndex = 2;
			this.button2.Text = "Cancel";
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(224, 104);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.groupBox1);
			this.Name = "Form1";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Kennedy Watts";
			this.TopMost = true;
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		}
		#endregion
		
        private void button1_Click(object sender, System.EventArgs e)
		{
			try
			{
				string PhaseNumber = this.textBox1.Text;
				Script.CreatePhaseIssueFolders(PhaseNumber);
			}
			catch (FormatException)
			{
				System.Windows.Forms.MessageBox.Show("A", "Tekla Structures");
				this.textBox1.Text = "";
			}
		}		
		
        private void button2_Click(object sender, System.EventArgs e)
		{
		 Application.Exit();
		} 					
	}
	
	public class Script
    {    
		static Tekla.Technology.Akit.IScript akit;   
		
        public static void Run(Tekla.Technology.Akit.IScript RunMe)
        {
			akit=RunMe;
			Application.Run(new Form1());         
        }
		
        public static void CreatePhaseIssueFolders(string PhaseNumber)
        {
			Model model = new Model();
            ModelInfo modelinfo = model.GetInfo();
			ProjectInfo projectinfo = model.GetProjectInfo();
			
			string IssueFolder = projectinfo.ProjectNumber + " Phase " + PhaseNumber;
			string IssueFolderPath = modelinfo.ModelPath + @"\Issues\" + IssueFolder + @"\";

			/** - Check for existence of a file - **/
			if(Directory.Exists(IssueFolderPath))
			{
				System.Windows.Forms.MessageBox.Show("Directory exists");
				akit.Callback("acmd_shellexecute_open", IssueFolderPath, "main_frame");
			}
			else
			{
				Directory.CreateDirectory(IssueFolderPath);
				string DrawingsFolderPath = IssueFolderPath + IssueFolder +@"\";
				
				Directory.CreateDirectory(DrawingsFolderPath + @"ASS\A0");
				Directory.CreateDirectory(DrawingsFolderPath + @"ASS\A1");
				Directory.CreateDirectory(DrawingsFolderPath + @"ASS\A2");
				Directory.CreateDirectory(DrawingsFolderPath + @"ASS\A3");
				Directory.CreateDirectory(DrawingsFolderPath + "FIT");
				Directory.CreateDirectory(DrawingsFolderPath + "GAS");
				
				string ListsFolderPath = IssueFolderPath + IssueFolder + " LISTS";
				Directory.CreateDirectory(ListsFolderPath);
				string ncFittsFolderPath = IssueFolderPath + IssueFolder + " NCFITTS";
				Directory.CreateDirectory(ncFittsFolderPath);
				string ncShaftsFolderPath = IssueFolderPath + IssueFolder + " NCSHAFTS";
				Directory.CreateDirectory(ncShaftsFolderPath);
				string CopyofModel = IssueFolderPath + projectinfo.ProjectNumber + " Copy of Model " + 
					DateTime.Now.Day.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Year.ToString();
				Directory.CreateDirectory(CopyofModel);
				
				akit.Callback("acmd_shellexecute_open", IssueFolderPath, "main_frame");
			}
            Application.Exit();
        }        
    }
}
