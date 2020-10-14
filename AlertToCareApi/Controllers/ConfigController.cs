using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AlertToCareApi.Models;

namespace AlertToCareApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfigController : ControllerBase
    {
        Beds[] beds = new Beds[]
        {
            new Beds{ id = 1,occupancyStatus= true },
            new Beds{ id = 2,occupancyStatus= false },
        };

        [HttpGet]
        public IEnumerable<Beds> Get()
        {
            return beds;
        }
    }
}
