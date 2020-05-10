using System;
using System.Collections.Generic;
using System.Text;
using Blazor.Extensions;
using Blazor.Extensions.Canvas.Canvas2D;

namespace CanvasControlLibrary
{
    class CCLWindowManager
    {
        private int currentWindowIDIndex = 0;
        private int highestDepth = 0;
        private Canvas2DContext currentCanvasContext;
        private BECanvasComponent currentCanvas;

        protected List<CCLWindow> CCLWindows = new List<CCLWindow>();

        protected int CurrentWindowIDIndex { get; }

        protected void RegisterWindow(CCLWindow window)
        {
            window.ID = ++currentWindowIDIndex;
            window.Depth = ++highestDepth;
            CCLWindows.Add(window);
        }
    }
}
