using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Tekla.Structures.Model;
using Tekla.Structures.Drawing;
using System.IO;

namespace Tekla.Technology.Akit.UserScript
{
    public class Script
    {
        public static void Run(Tekla.Technology.Akit.IScript akit)
        {
            Model model = new Model();
            ModelInfo modelinfo = model.GetInfo();
            
            FileInfo fileInfo = new FileInfo(modelinfo.ModelPath + @"\attributes\CurrentContracts");
            if (fileInfo.Exists)
            {
                using (StreamReader sr = new StreamReader(fileInfo.FullName))
                {
                    string line = sr.ReadLine();
                    DirectoryInfo CurrentContracts = new DirectoryInfo(line);
                    if (CurrentContracts.Exists)
                        akit.Callback("acmd_shellexecute_open", CurrentContracts.FullName, "main_frame");
                }
            }
            else
            {
                FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog();
                DialogResult result = folderBrowserDialog1.ShowDialog();
                if (result == DialogResult.OK)
                {
                    string folderName = folderBrowserDialog1.SelectedPath;
                    using (StreamWriter sw = new StreamWriter(fileInfo.FullName))
                    {
                        sw.WriteLine(folderName);
                    }
                    akit.Callback("acmd_shellexecute_open", folderName, "main_frame");
                }
            }          
        }
    }
}
