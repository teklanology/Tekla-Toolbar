using Tekla.Structures;
using Tekla.Structures.Model;
using Tekla.Structures.Drawing;
using System.Windows.Forms;

namespace Tekla.Technology.Akit.UserScript
{
    public class Script
    {
        public static void Run(Tekla.Technology.Akit.IScript akit)
        {
            Model model = new Model();
            ModelObjectEnumerator modelObjectEnum = model.GetModelObjectSelector().GetAllObjectsWithType(Tekla.Structures.Model.ModelObject.ModelObjectEnum.BOLT_ARRAY);
            while (modelObjectEnum.MoveNext())
            {
                if (modelObjectEnum.Current is BoltGroup)
                {
                    BoltGroup bolt = (BoltGroup)modelObjectEnum.Current;
                    if (bolt.BoltStandard == "7990")
                    {
                        bolt.BoltStandard = "8.8XOX";
                        bolt.Modify();
                    }
                }
            }
            model.CommitChanges();
            MessageBox.Show("Operation Finished");
        }
    }
}
