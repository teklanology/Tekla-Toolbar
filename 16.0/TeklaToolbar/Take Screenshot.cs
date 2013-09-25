using System;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using Tekla.Structures;
using Tekla.Structures.Model;

namespace Tekla.Technology.Akit.UserScript
{
    public class Script
    {
        public static void Run(Tekla.Technology.Akit.IScript akit)
        {
            akit.Callback("diaDisplaySnapshotDialog", "", "main_frame");
            akit.PushButton("options", "snapshot_dialog");
            akit.ValueChange("snapshot_option_dialog", "width", "387.000000000000");
            akit.ValueChange("snapshot_option_dialog", "white_bg_enabled", "1");
            akit.PushButton("option_apply", "snapshot_option_dialog");
            akit.PushButton("option_ok", "snapshot_option_dialog");
            
			Model model = new Model();
            ModelInfo modelinfo = model.GetInfo();
			string ScreenshotsFolderPath = modelinfo.ModelPath + @"\screenshots\";
			string now = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString("0#") + "-" + DateTime.Now.Day.ToString("0#") + "-" + 
                DateTime.Now.Hour.ToString("0#") + DateTime.Now.Minute.ToString("0#") + DateTime.Now.Second.ToString("0#");
			string ScreenshotFilePath = ScreenshotsFolderPath + Environment.UserName + "_" + now + ".png";
			akit.ValueChange("snapshot_dialog", "filename",  ScreenshotFilePath);
            
			akit.ValueChange("snapshot_dialog", "target_selection", "1");
            akit.ValueChange("snapshot_dialog", "show_with_viewer_enabled", "1");
            akit.PushButton("take_snapshot", "snapshot_dialog");
            akit.PushButton("cancel", "snapshot_dialog");
			
			
			if (!File.Exists(ScreenshotFilePath))
				return;
			else
			{
				string argument = @"/select, " + ScreenshotFilePath;
				System.Diagnostics.Process.Start("explorer.exe", argument);
			}
        }
    }
}
