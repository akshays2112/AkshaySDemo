using System.Collections.Generic;

namespace CCLRazorComponentsForBlazor.CCLWindowManager
{
    internal class CCLWindow
    {
        internal int ID { get; set; }

        internal int ParentCCLWindowID { get; set; }

        internal bool HasAnimation { get; set; }

        internal bool HasFocus { get; set; }

        internal bool IsModalWindow { get; set; }

        internal bool IsHidden { get; set; }

        internal int X { get; set; }

        internal int Y { get; set; }

        internal int Width { get; set; }

        internal int Height { get; set; }

        internal int Depth { get; set; }
    }
}
