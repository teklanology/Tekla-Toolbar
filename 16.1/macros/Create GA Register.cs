using Tekla.Structures.Model;
using System.IO;
using System.Windows.Forms;

namespace Tekla.Technology.Akit.UserScript 
{
    public class Script 
    {
    	/**** ****/
		private static string file = "GA-Drawing-Register.xls";
		/*****************************************************************************/
		public static void Run(Tekla.Technology.Akit.IScript akit) 
       	{
			Model model = new Model();
            ModelInfo modelinfo = model.GetInfo();
            string[] split; split = model.GetCurrentProgramVersion().Split(new char[] { ' ' });
            bool boolResult; double dblVersion; boolResult = double.TryParse(split[0], out dblVersion);
            dblVersion = dblVersion * 10;
			string strVersion = dblVersion.ToString();
		
			string modelDir;
			string spreadsheet;

			/** Get model directory **/
			modelDir = new System.IO.DirectoryInfo("./").FullName;
			//test for the file path
			//System.Windows.Forms.MessageBox.Show(modelDir);

			akit.Callback("acmd_display_report_dialog", "", "main_frame");
			akit.ListSelect("xs_report_dialog", "xs_report_list", "ga_register");
			akit.TabChange("xs_report_dialog", "Container_516", "Container_519");
			akit.ValueChange("xs_report_dialog", "display_created_report", "0");
			akit.TabChange("xs_report_dialog", "Container_516", "Container_517");
			akit.ModalDialog(1);
			akit.PushButton("xs_report_selected", "xs_report_dialog");
			akit.ListSelect("xs_report_dialog", "xs_report_list", "rev-iss-dates");
			akit.ModalDialog(1);
			akit.PushButton("xs_report_selected", "xs_report_dialog");
                
			akit.PushButton("xs_report_cancel", "xs_report_dialog");

			/** Check for existence of a file -  **/
			if(System.IO.File.Exists(@modelDir + "Reports/" +file))
			System.Windows.Forms.MessageBox.Show("file exists, opening the one in the model folder", "Kennedy Watts");
				
			else 
			/** Copy a file to the model folder **/
			new System.IO.FileInfo("X:/data2/TeklaStructures/KWP-settings" + strVersion + "/Spreadsheets/GA-Drawing-Register.xls").CopyTo(@modelDir+@"Reports\"+file,true);

			spreadsheet = @modelDir+@"Reports\"+file;
				                
			System.Diagnostics.Process Process2 = new System.Diagnostics.Process();
			Process2.EnableRaisingEvents=false;
			Process2.StartInfo.FileName="EXCEL";
			Process2.StartInfo.Arguments="\""+@spreadsheet+"\"";
			Process2.Start();
        }
    }
}
