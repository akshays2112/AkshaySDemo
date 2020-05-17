using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Components.Web;
using System.Reflection;
using Newtonsoft.Json.Linq;
using CCLRazorComponentsForBlazor.CCLControls;

namespace CCLRazorComponentsForBlazor.Globals
{
    internal class CCLJsInterops
    {
        internal static async Task OnClick(IJSRuntime jsRuntime, ElementReference el, MouseEventArgs eventArgs, TestDrawingControl t)
        {
            string data = await jsRuntime.InvokeAsync<string>("cclHelperFunctions.getElementProps", new object[] { el, new string[] { "offsetLeft", "offsetTop" } });
            JObject coords = (JObject) JsonConvert.DeserializeObject(data);
            t.ClientX = eventArgs.ClientX - coords.Value<double>("offsetLeft");
            t.ClientY = eventArgs.ClientY - coords.Value<double>("offsetTop");
            t.IsNewClientXYSet = true;
        }

        internal static async Task SetViewportDimensions(IJSRuntime jsRuntime, TestCanvasDrawingComponent t)
        {
            string data = await jsRuntime.InvokeAsync<string>("cclHelperFunctions.getViewportDimensions");
            JObject dim = (JObject)JsonConvert.DeserializeObject(data);
            if (t.canvasWidth == 0)
            {
                t.canvasWidth = dim.Value<int>("clientWidth");
            }
            if (t.canvasHeight == 0)
            {
                t.canvasHeight = dim.Value<int>("clientHeight");
            }
        }
    }
}
