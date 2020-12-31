using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace PrivateApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize("ApiScope")]
    public class IdentityController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<Claim> Get()
        {
            return User.Claims.Select(x => new Claim { Type = x.Type, Value = x.Value });
        }
    }

    public class Claim
    {
        public string Type { get; set; }
        public string Value { get; set; }
    }
}
