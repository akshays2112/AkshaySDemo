using System;
using System.Threading.Tasks;
using Blazor.Extensions;
using CCLRazorComponentsForBlazor.CCLWindowManager;

namespace CCLRazorComponentsForBlazor.CCLControls
{
    internal class TestDrawingControl : CCLBaseControl
    {
        internal bool IsNewClientXYSet;
        internal double ClientX;
        internal double ClientY;
        internal async Task TestDrawLine(BECanvasComponent c)
        {
            await SetCurrentCanvasContext(c);
            int canvasHeight = Convert.ToInt32(currentCanvas.Height);
            int canvasWidth = Convert.ToInt32(currentCanvas.Width);
            await currentCanvasContext.BeginBatchAsync();
            await currentCanvasContext.SaveAsync();
            await currentCanvasContext.ClearRectAsync(0, 0, c.Width, c.Height);
            await currentCanvasContext.RectAsync(0, 0, canvasWidth, canvasHeight);
            await currentCanvasContext.StrokeAsync();
            if (IsNewClientXYSet)
            {
                await currentCanvasContext.ClearRectAsync(10, 10, 300, 11);
                await currentCanvasContext.SetStrokeStyleAsync("Red");
                await currentCanvasContext.StrokeTextAsync("ClientX: " + ClientX + "   Client Y: " + ClientY, 20, 20);
                await currentCanvasContext.SetFillStyleAsync("Green");
                await currentCanvasContext.FillRectAsync(ClientX, ClientY, 3, 3);
            }
            await currentCanvasContext.RestoreAsync();
            await currentCanvasContext.EndBatchAsync();
        }
    }
}
