using System;
using System.Threading.Tasks;
using Blazor.Extensions;
using Microsoft.AspNetCore.Components;
using CCLRazorComponentsForBlazor.CCLWindowManager;

namespace CCLRazorComponentsForBlazor.CCLControls
{
    internal class TestBasicDrawingCapability1 : CCLBaseControl
    {
        internal bool IsNewClientXYSet;
        internal double ClientX;
        internal double ClientY;
        internal async Task TestDrawLine(BECanvasComponent c)
        {
            await SetCurrentCanvasContext(c);
            int chartHeight = Convert.ToInt32(currentCanvas.Height) - 40;
            // Draw the axises
            await currentCanvasContext.BeginBatchAsync();
            await currentCanvasContext.SaveAsync();
            await currentCanvasContext.ClearRectAsync(0, 0, c.Width, c.Height);
            await currentCanvasContext.BeginPathAsync();
            await currentCanvasContext.MoveToAsync(10, 10);
            // first Draw Y Axis  
            await currentCanvasContext.LineToAsync(10, chartHeight);
            // Next draw the X-Axis
            await currentCanvasContext.StrokeAsync();
            if (IsNewClientXYSet)
            {
                await currentCanvasContext.ClearRectAsync(0, 0, 300, 11);
                await currentCanvasContext.SetStrokeStyleAsync("Red");
                await currentCanvasContext.StrokeTextAsync("ClientX: " + ClientX + "   Client Y: " + ClientY, 10, 10);
                await currentCanvasContext.SetFillStyleAsync("Green");
                await currentCanvasContext.FillRectAsync(ClientX, ClientY, 3, 3);
            }
            await currentCanvasContext.RestoreAsync();
            await currentCanvasContext.EndBatchAsync();
        }
    }
}
