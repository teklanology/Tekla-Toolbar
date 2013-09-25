using Tekla.Structures.Model;
using System.Collections;

namespace Tekla.Technology.Akit.UserScript
{
    public class Script
    {
        public static void Run(Tekla.Technology.Akit.IScript akit)
        {
            Model model = new Model();
            ArrayList array = new ArrayList();
            ModelObjectEnumerator modelObjectEnum = model.GetModelObjectSelector().GetSelectedObjects();
            while (modelObjectEnum.MoveNext())
            {
                if (modelObjectEnum.Current is Tekla.Structures.Model.Part)
                {
                    Tekla.Structures.Model.Part part = modelObjectEnum.Current as Tekla.Structures.Model.Part;
                    //array.Add(model.SelectModelObject(new Tekla.Structures.Identifier(part.Identifier.ID)));
                    ModelObjectEnumerator CutEnum = part.GetBooleans();
                    while (CutEnum.MoveNext())
                    {
                        Tekla.Structures.Model.Boolean cut = CutEnum.Current as Tekla.Structures.Model.Boolean;
                        array.Add(model.SelectModelObject(new Tekla.Structures.Identifier(cut.Identifier.ID)));
                    }
                }
            }
            Tekla.Structures.Model.UI.ModelObjectSelector modelObjectSelector = new Tekla.Structures.Model.UI.ModelObjectSelector();
            modelObjectSelector.Select(array);
        }
    }
}
