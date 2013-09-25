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
                            string USER_FIELD_3 = "", USER_FIELD_4 = "", PRELIM_MARK = "";
                            part.GetUserProperty("USER_FIELD_3", ref USER_FIELD_3);
                            part.GetUserProperty("USER_FIELD_4", ref USER_FIELD_4);
                            part.GetUserProperty("PRELIM_MARK", ref PRELIM_MARK);

                            USER_FIELD_4 = USER_FIELD_4.Replace("(?)", "");

                            if (USER_FIELD_3 == "Z")
                            {
                                part.SetUserProperty("USER_FIELD_2", PRELIM_MARK);
                                part.SetUserProperty("PRELIM_MARK", USER_FIELD_3 + USER_FIELD_4);
                                part.Modify();
                            }
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
