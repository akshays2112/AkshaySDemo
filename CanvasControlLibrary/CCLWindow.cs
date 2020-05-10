using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace CanvasControlLibrary
{
    class CCLWindow
    {
        protected internal int ID { get; set; }

        protected internal int ParentWindowID { get; set; }

        protected internal bool HasAnimation { get; set; }

        protected internal bool HasFocus { get; set; }

        protected internal bool IsModalWindow { get; set; }

        protected internal bool IsHidden { get; set; }

        protected internal int X { get; set; }

        protected internal int Y { get; set; }

        protected internal int Width { get; set; }

        protected internal int Height { get; set; }

        protected internal int Depth { get; set; }
    }
}
