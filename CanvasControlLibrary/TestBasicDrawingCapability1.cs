using System;
using System.Threading.Tasks;
using Blazor.Extensions.Canvas;
using Blazor.Extensions;
using Blazor.Extensions.Canvas.Canvas2D;


namespace CanvasControlLibrary
{
    public class TestBasicDrawingCapability1
    {
        private Canvas2DContext currentCanvasContext;
        private BECanvasComponent currentCanvas;
        public BECanvasComponent CurrentCanvas { 
            get { 
                return currentCanvas;
            } 
            set {
                if (value != currentCanvas) { 
                    currentCanvas = value;
                }
            } 
        }

        public async Task SetCurrentCanvasContext(BECanvasComponent c)
        {
            if(c is null)
            {
                throw new ArgumentNullException();
            }
            if(currentCanvas != c)
            {
                currentCanvas = c;
                currentCanvasContext = await c.CreateCanvas2DAsync();
            }
        }

        public async Task TestDrawLine(BECanvasComponent c)
        {
            await SetCurrentCanvasContext(c);
            await SetCurrentCanvasContext(c);
            int chartHeight = Convert.ToInt32(currentCanvas.Height) - 40;
            // Draw the axises  
            await currentCanvasContext.BeginPathAsync();
            await currentCanvasContext.MoveToAsync(10, 10);
            // first Draw Y Axis  
            await currentCanvasContext.LineToAsync(10, chartHeight);
            // Next draw the X-Axis
            await currentCanvasContext.StrokeAsync();
        }
    }
}
