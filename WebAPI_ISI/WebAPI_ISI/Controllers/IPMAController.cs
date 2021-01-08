using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI_ISI.Models;

namespace WebAPI_ISI.Controllers
{
    [ApiController]
    [Route("api/IPMA")]
    public class IPMAController : ControllerBase
    {
        IpmaMetodos newIpma = new IpmaMetodos();

        [HttpGet("RetornaCidades")]
        public Dictionary<int, string> RetornaCidades()
        {
            return newIpma.RetornaCidades();
        }

        [HttpGet("Get5DayWeather/{idCidade}")]
        public Previsao5dias Get5DayWeather(string idCidade)
        {
            return newIpma.Get5DayWeather(idCidade);
        }

        [HttpGet("GetWeatherTypes")]
        public TiposTempo GetWeatherTypes()
        {
            return newIpma.GetWeatherTypes();
        }

    }

}

