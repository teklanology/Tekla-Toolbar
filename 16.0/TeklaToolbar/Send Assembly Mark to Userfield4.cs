using System;
using System.Windows.Forms;
using Tekla.Structures;
using Tekla.Structures.Model;
using System.Collections;

namespace Tekla.Technology.Akit.UserScript
{
    public class Script
    {
        public static void Run(Tekla.Technology.Akit.IScript akit)
        {
            try
            {
                Model model = new Model();
                ModelObjectEnumerator modelObjectEnum = model.GetModelObjectSelector().GetSelectedObjects();
                if (modelObjectEnum.GetSize() > 0)
                {
                    while (modelObjectEnum.MoveNext())
                    {
                        if (modelObjectEnum.Current is Tekla.Structures.Model.Part)
                        {
                            Tekla.Structures.Model.Part part = (Tekla.Structures.Model.Part)modelObjectEnum.Current;
                            string mark = "";
                            Hashtable strProps = new Hashtable();
                            ArrayList PartStrRepPropNames = new ArrayList();
                            PartStrRepPropNames.Add("ASSEMBLY_POS");
                            part.GetStringReportProperties(PartStrRepPropNames, ref strProps);
                            if ((mark = (string)strProps["ASSEMBLY_POS"]) == null)
                                mark = "";
							
							mark = mark.Replace("(?)", "");
							
                            part.SetUserProperty("USER_FIELD_4", mark);
                            part.Modify();
                        }
                    }
					model.CommitChanges();
					MessageBox.Show("Process Complete");
                }
            }
            catch { }
        }
    }
}
