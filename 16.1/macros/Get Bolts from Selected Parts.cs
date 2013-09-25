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
                    ModelObjectEnumerator BoltEnum = part.GetBolts();
                    while (BoltEnum.MoveNext())
                    {
                        Tekla.Structures.Model.BoltGroup bolt = BoltEnum.Current as Tekla.Structures.Model.BoltGroup;
                        array.Add(model.SelectModelObject(new Tekla.Structures.Identifier(bolt.Identifier.ID)));
                    }
                }
            }
            Tekla.Structures.Model.UI.ModelObjectSelector modelObjectSelector = new Tekla.Structures.Model.UI.ModelObjectSelector();
            modelObjectSelector.Select(array);
        }
    }
}
