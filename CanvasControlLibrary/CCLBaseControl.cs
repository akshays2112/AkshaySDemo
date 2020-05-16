﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Blazor.Extensions;
using Blazor.Extensions.Canvas.Canvas2D;

namespace CanvasControlLibrary
{
    public class CCLBaseControl : CCLWindow
    {
        protected Canvas2DContext currentCanvasContext;

        protected BECanvasComponent currentCanvas;
        internal BECanvasComponent CurrentCanvas
        {
            get
            {
                return currentCanvas;
            }
            set
            {
                if (value != currentCanvas)
                {
                    currentCanvas = value;
                }
            }
        }

        internal async Task SetCurrentCanvasContext(BECanvasComponent c)
        {
            if (c is null)
            {
                throw new ArgumentNullException();
            }
            if (currentCanvas != c)
            {
                currentCanvas = c;
                currentCanvasContext = await c.CreateCanvas2DAsync();
                if(currentCanvasContext is null)
                {
                    throw new ArgumentNullException();
                }
            }
        }
    }
}
