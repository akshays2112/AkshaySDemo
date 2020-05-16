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
        internal static async Task OnClick(IJSRuntime jsRuntime, ElementReference el, MouseEventArgs eventArgs, TestBasicDrawingCapability1 t)
        {
            string data = await jsRuntime.InvokeAsync<string>("cclHelperFunctions.getElementTopLeftCornerPageCoords", new object[] { el, new string[] { "offsetLeft", "offsetTop" } });
            JObject coords = (JObject) JsonConvert.DeserializeObject(data);
            t.ClientX = eventArgs.ClientX - coords.Value<double>("offsetLeft");
            t.ClientY = eventArgs.ClientY - coords.Value<double>("offsetTop");
            t.IsNewClientXYSet = true;
        }
    }
}
