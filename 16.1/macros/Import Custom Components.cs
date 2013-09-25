using System;
using System.IO;
using System.Diagnostics;
using Tekla.Structures.Model;

namespace Tekla.Technology.Akit.UserScript
{
    public class Script
    {
        public static void Run(Tekla.Technology.Akit.IScript akit)
        {
			Model model = new Model();
            ModelInfo modelinfo = model.GetInfo();
            string[] split; split = model.GetCurrentProgramVersion().Split(new char[] { ' ' });
            bool boolResult; double dblVersion; boolResult = double.TryParse(split[0], out dblVersion);
            dblVersion = dblVersion * 10;
			string strVersion = dblVersion.ToString();
			string uelFilePath = @"X:\data2\TeklaStructures\KWP-settings" + strVersion + @"\CustomComponents\" + strVersion + @"-CC-Package.uel";
			akit.FileSelection(uelFilePath);
			akit.ModalDialog(1);
			akit.Callback("acmdDisplayImportCustomComponentDialog", "", "main_frame");
        }
    }
}
