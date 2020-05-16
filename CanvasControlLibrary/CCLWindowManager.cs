using System;
using System.Collections.Generic;
using System.Text;

namespace CanvasControlLibrary
{
    internal class CCLWindowManager
    {
        private class CCLWindowEvent
        {
            protected CCLWindow window;
            protected string typeOfEvent;
            protected List<object> parameters;

            public CCLWindowEvent(string typeOfEvent, List<object> parameters)
            {
                this.typeOfEvent = typeOfEvent;
                this.parameters = parameters;
            }
        }

        private int currentWindowIDIndex = 0;
        private int highestDepth = 0;

        internal List<CCLWindow> CCLWindows = new List<CCLWindow>();

        private List<CCLWindowEvent> registeredWindowsEvents;

        internal int CurrentWindowIDIndex { get; }

        internal void RegisterWindow(CCLWindow window)
        {
            window.ID = ++currentWindowIDIndex;
            window.Depth = ++highestDepth;
            CCLWindows.Add(window);
        }

        internal void RegisterEvent(string typeOfEvent, List<object> parameters)
        {
            CCLWindowEvent cclWindowEvent = new CCLWindowEvent(typeOfEvent, parameters);
            registeredWindowsEvents.Add(cclWindowEvent);
        }
    }
}
