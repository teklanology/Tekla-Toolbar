// Generated by Tekla.Technology.Akit.ScriptBuilder

using Tekla.Structures.Model;


namespace Tekla.Technology.Akit.UserScript 
{
    public class Script 
    {
        public static void Run(Tekla.Technology.Akit.IScript akit) 
        {
			System.Diagnostics.Process Process = new System.Diagnostics.Process();
			Process.EnableRaisingEvents=false;
			Process.StartInfo.FileName="iexplore";
			Process.StartInfo.Arguments="X:/data2/TeklaStructures/KWP/KWP-help.html";
			Process.Start();
        }
    }
}