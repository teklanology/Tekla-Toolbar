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
			Process StartApp = new Process();
			StartApp.EnableRaisingEvents = false;
			StartApp.StartInfo.FileName = @"X:\data2\TeklaStructures\16.1\environments\KWP-GET-UPDATES.bat";
			StartApp.Start();
        }
    }
}
