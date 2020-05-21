using System;
using System.Threading.Tasks;
using Blazor.Extensions;
using Blazor.Extensions.Canvas.Canvas2D;

namespace CCLRazorComponentsForBlazor.CCLWindowManager
{
    internal class CCLBaseControl : CCLWindow
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
            }
            if (currentCanvasContext == null)
            {
                currentCanvasContext = await c.CreateCanvas2DAsync();
                if (currentCanvasContext is null)
                {
                    throw new ArgumentNullException();
                }
            }
        }
    }
}
