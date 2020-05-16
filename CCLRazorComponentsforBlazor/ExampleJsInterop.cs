using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace CCLRazorComponentsForBlazor
{
    public class ExampleJsInterop
    {
        public class Coords
        {
            public double X;
            public double Y;
        }

        public static ValueTask<string> GetElementOffsetTop(IJSRuntime jsRuntime, ElementReference el)
        {
            return jsRuntime.InvokeAsync<string>("cclHelperFunctions.getElementTopLeftCornerPageCoords", el);
        }
    }
}
