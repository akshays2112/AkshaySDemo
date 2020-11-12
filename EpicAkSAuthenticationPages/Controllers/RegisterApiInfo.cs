using Microsoft.AspNetCore.Mvc;
using EpicAkSAuthenticationPages.Models;

namespace EpicAkSAuthenticationPages.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RegisterApiInfo : ControllerBase
    {
        [HttpPost]
        public string Post(ApiInfoValues apiInfoValues)
        {
            return Globals.GenerateClientAppToken(apiInfoValues.clientId, apiInfoValues.clientSecret).CATToken;
        }
    }
}
