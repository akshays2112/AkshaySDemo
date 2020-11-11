using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EpicAkSAuthenticationPages.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RegisterApiInfo : ControllerBase
    {
        [HttpGet]
        public string Get(string clientId, string clientSecret)
        {
            return Globals.GenerateClientAppToken(clientId, clientSecret).CATToken;
        }
    }
}
