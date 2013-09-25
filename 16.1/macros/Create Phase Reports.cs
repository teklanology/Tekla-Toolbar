using System;
using System.Windows.Forms;
using System.IO;
using Tekla.Structures;
using Tekla.Structures.Model;
using Tekla.Structures.Model.Operations;

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
			this.groupBox1.Size = new System.Drawing.Size(208, 56);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = " Create Reports for Phase ";
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
			this.textBox1.TabIndex = 1;
			this.textBox1.Text = "";
			this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			//
			// button1
			//
			this.button1.Location = new System.Drawing.Point(8, 72);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(96, 32);
			this.button1.TabIndex = 1;
			this.button1.Text = "Ok";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			//
			// button2
			//
			this.button2.Location = new System.Drawing.Point(120, 72);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(96, 32);
			this.button2.TabIndex = 2;
			this.button2.Text = "Cancel";
			this.button2.Click += new System.EventHandler(this.button2_Click);
			//
			// Form1
			//
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(224, 110);
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
				Script.RefRefresh(PhaseNumber);
			}
			catch (FormatException)
			{
				System.Windows.Forms.MessageBox.Show("Sorry, wrong input! Only numerical values are allowed!", "Tekla Structures");
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
        public static void RefRefresh(string PhaseNumber)
        {
			try
			{
				Model model = new Model();
				ModelInfo modelinfo = model.GetInfo();
				ProjectInfo projectInfo = model.GetProjectInfo();
				string PhaseReportsFolder = projectInfo.ProjectNumber + " Phase " + PhaseNumber + " LISTS";
				string PhaseReportsFolderPath = modelinfo.ModelPath + @"\Reports\" + PhaseReportsFolder + @"\";
				Directory.CreateDirectory(PhaseReportsFolderPath);
				akit.Callback("acmd_shellexecute_open", PhaseReportsFolderPath, "main_frame");
				
				/* creates the select filter so its visible in the list */
				akit.Callback("diaDisplayObjectGroupDialogCB", "diaSelectObjectGroup", "main_frame");	//opens select filter dialog box
				akit.ValueChange("diaSelectObjectGroup", "saveas_file", "phase-macro"); 				//filename to save select filter
				akit.PushButton("attrib_saveas", "diaSelectObjectGroup"); 								//saves the select filter
				akit.PushButton("dia_pa_cancel", "diaSelectObjectGroup"); 								//closes select filter dialog box		
			
				/* writes the new select filter to the model folder */
				System.IO.StreamWriter sw = new System.IO.StreamWriter("phase-macro.SObjGrp",false,System.Text.Encoding.Default);
				sw.WriteLine("TITLE_OBJECT_GROUP");
				sw.WriteLine("{");
				sw.WriteLine("Version= 1.04");
				sw.WriteLine("Count= 2");
				sw.WriteLine("SECTION_OBJECT_GROUP");
				sw.WriteLine("{");
				sw.WriteLine("0");
				sw.WriteLine("1");
				sw.WriteLine("co_part");
				sw.WriteLine("proPHASE");
				sw.WriteLine("albl_Phase");
				sw.WriteLine("==");
				sw.WriteLine("albl_Equals");
				sw.WriteLine(PhaseNumber);
				sw.WriteLine("0");
				sw.WriteLine("Empty");
				sw.WriteLine("}");
				sw.WriteLine("}");
				sw.Flush();
				sw.Close();

				/* moves new select filter to attributes folder */
				File.Delete("./attributes/phase-macro.SObjGrp");
				File.Move("phase-macro.SObjGrp", "./attributes/phase-macro.SObjGrp");
			      		
				/* set select switches and load phase and main part filter */
				akit.ValueChange("main_frame", "sel_objects_in_joints", "1");        //choose parts in componets only
				akit.ValueChange("main_frame", "sel_parts", "1");                    //pick parts only
				akit.ValueChange("main_frame", "sel_filter", "phase-macro");         //pick the filter to use
				akit.Callback("acmdSelectAll", "", "main_frame");					 //selects parts
			
				System.Runtime.Remoting.Lifetime.ClientSponsor sponsor = null;

				sponsor = new System.Runtime.Remoting.Lifetime.ClientSponsor();
				sponsor.Register((System.MarshalByRefObject)akit);
				
				Operation.CreateReportFromSelected("KWP-ass_issue_list_SUB", PhaseReportsFolderPath + "ass_issue_list_PH" + PhaseNumber + ".xsr", PhaseNumber, "", "");
				Operation.CreateReportFromSelected("KWP-part_list_SUB", PhaseReportsFolderPath + "part_list_PH" + PhaseNumber +".xsr", PhaseNumber, "", "");
				Operation.CreateReportFromSelected("g2_assy6_SUB", PhaseReportsFolderPath + "g2_assy6_PH" + PhaseNumber + ".xsr", PhaseNumber, "", "");			
				Operation.CreateReportFromSelected("KWP-assembly_bolt_list1_comment", PhaseReportsFolderPath + "assembly_bolt_list1_PH" + PhaseNumber + ".xsr", PhaseNumber, "", "");
				Operation.CreateReportFromSelected("KWP-bolt_list_shop_only_comment", PhaseReportsFolderPath + "bolt_list_shop_only_PH" + PhaseNumber + ".xsr", PhaseNumber, "", "");			
				Operation.CreateReportFromSelected("KWP-bolt_list_site_only_comment", PhaseReportsFolderPath + "bolt_list_site_only_PH" + PhaseNumber + ".xsr", PhaseNumber, "", "");
				
				/* closes report dialog and resets switches/filter */
				akit.ValueChange("main_frame", "sel_filter", "standard");			//pick the filter to use
				akit.ValueChange("main_frame", "sel_views", "1");					//selects views
				akit.Callback("acmdSelectAll", "", "main_frame");					//deselects objects
				akit.ValueChange("main_frame", "sel_all", "1");						//reselects all switches
				System.Windows.Forms.MessageBox.Show("Reports created for phase " + PhaseNumber, "Tekla Structures");
				Application.Exit();
			}
			catch
			{
				MessageBox.Show("Operation timed out");
			}
        }
    }
}
