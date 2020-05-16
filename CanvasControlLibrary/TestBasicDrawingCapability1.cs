using System;
using System.Threading.Tasks;
using Blazor.Extensions;
using Microsoft.AspNetCore.Components;

namespace CanvasControlLibrary
{
    public class TestBasicDrawingCapability1 : CCLBaseControl
    {
        public async Task TestDrawLine(BECanvasComponent c)
        {
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

        public async Task OnClick(double clientX, double clientY)
        {
            await currentCanvasContext.StrokeTextAsync("ClientX: " + clientX + "   Client Y: " + clientY, 10, 10);
            await currentCanvasContext.FillRectAsync(clientX, clientY, 10, 10);
        }
    }
}
