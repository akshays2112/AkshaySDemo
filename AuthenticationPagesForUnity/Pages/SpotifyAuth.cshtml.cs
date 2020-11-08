using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AuthenticationPagesForUnity.Pages
{
    public class SpotifyAuthModel : PageModel
    {
        [FromQuery]
        public string unityToken { get; set; }

        [FromQuery]
        public string accessToken { get; set; }
    }
}
