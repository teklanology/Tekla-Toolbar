using Tekla.Structures.Model;
using Tekla.Structures.Model.UI;

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
                System.Collections.ArrayList array = new System.Collections.ArrayList();
                if (modelObjectEnum.GetSize() == 0)
                {
                    Picker picker = new Picker();
                    Part part = (Part)picker.PickObject(Tekla.Structures.Model.UI.Picker.PickObjectEnum.PICK_ONE_PART);
                    part.Insert();
                    array.Add(part);
                }

                else
                {
                    while (modelObjectEnum.MoveNext())
                    {
                        if (modelObjectEnum.Current is Part)
                        {
                            Part part = (Part)modelObjectEnum.Current;
                            part.Insert();
                            array.Add(part);
                        }
                    }
                }
                model.CommitChanges();
                Tekla.Structures.Model.UI.ModelObjectSelector modelobjsel = new Tekla.Structures.Model.UI.ModelObjectSelector();
                modelobjsel.Select(array);
            }
            catch { }

        }
    }
}
