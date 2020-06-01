using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Components.Web;
using System.Reflection;
using Newtonsoft.Json.Linq;
using CCLRazorComponentsForBlazor.CCLControls;
using System.Net.NetworkInformation;

namespace CCLRazorComponentsForBlazor.Globals
{
    internal class CCLJsInterops
    {
        internal static async Task<string> OnClick(IJSRuntime jsRuntime, ElementReference el, string[] props)
        {
            return await jsRuntime.InvokeAsync<string>("cclHelperFunctions.getElementProps", new object[] { el, props });
        }

        internal static async Task<string> GetJSFunctionResultsByName(IJSRuntime jsRuntime, string jsFunctionToCall)
        {
            return await jsRuntime.InvokeAsync<string>(jsFunctionToCall);
        }
    }
}
