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

        /// <summary>
        /// Método GET que irá retornar um objeto do tipo "Locais" que contém a informação das diferentes cidades
        /// </summary>
        /// <returns></returns>
        [HttpGet("RetornaCidades")]
        public Locais RetornaCidades()
        {
            return newIpma.RetornaCidades();
        }

        /// <summary>
        /// Método GET que irá retornar um objeto do tipo "Previsao5dias" que contém a previsão metereológica dos próximos 5 dias de uma determinada cidade
        /// </summary>
        /// <param name="idCidade">Id da cidade para a qual se quer saber a previsão metereológica dos próximos 5 dias</param>
        /// <returns></returns>
        [HttpGet("Get5DayWeather/{idCidade}")]
        public Previsao5dias Get5DayWeather(string idCidade)
        {
            return newIpma.Get5DayWeather(idCidade);
        }

        /// <summary>
        /// Método GET que irá retornar um objeto do tipo "Previsao5dias" que contém informação sobre as descrições sobre os diferentes tipos de tempo
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetWeatherTypes")]
        public TiposTempo GetWeatherTypes()
        {
            return newIpma.GetWeatherTypes();
        }

    }

}

