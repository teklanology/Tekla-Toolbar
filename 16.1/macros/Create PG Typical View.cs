// Generated by Tekla.Technology.Akit.ScriptBuilder
using System;
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
			Model model = new Model();
			ModelInfo modelInfo = model.GetInfo();
			string XS_MACRO_DIRECTORY = "";
            model.GetAdvancedOption("XS_MACRO_DIRECTORY", ref XS_MACRO_DIRECTORY);
			Process StartApp = new Process();
			StartApp.EnableRaisingEvents = false;
			StartApp.StartInfo.FileName = XS_MACRO_DIRECTORY + @"\applications\CreatePGTypicalView.exe";
			StartApp.Start();
        }
    }
}
