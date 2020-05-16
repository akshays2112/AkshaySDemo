using System;
using System.Collections.Generic;
using System.Text;

namespace CCLRazorComponentsForBlazor.CCLWindowManager
{
    internal class CCLWindowManager
    {
        private int currentWindowIDIndex = 0;
        private int highestDepth = 0;

        internal List<CCLWindow> CCLWindows = new List<CCLWindow>();

        internal int CurrentWindowIDIndex { get; }

        internal void RegisterWindow(CCLWindow window)
        {
            window.ID = ++currentWindowIDIndex;
            window.Depth = ++highestDepth;
            CCLWindows.Add(window);
        }
    }
}
