using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.Design;

namespace ControlEx.Designer
{
    public class HideUnnecessaryPropertyDesign : ControlDesigner
    {
        protected override void PreFilterProperties(System.Collections.IDictionary properties)
        {
            base.PreFilterProperties(properties);

        }

    }
}
