using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI_ISI.Models;

namespace WebAPI_ISI.Controllers
{
    [ApiController]
    [Route("api/Evento")]
    public class EventoController : Controller
    {

        Eventos newEventos = new Eventos();

        [HttpPost("AddEvento/{jsonString}")]
        public ActionResult<bool> AddEvento(string jsonString)
        {
            if (newEventos.AddEvento(jsonString) == true) return Ok();

            else { return BadRequest(); }
        }

        [HttpGet("GetEventos/{id_utilizador}")]
        public Aux GetEventos(string id_utilizador)
        {
            return newEventos.GetEventos(id_utilizador);
        }

        [HttpDelete("DeleteEvento/{jsonString}")]
        public bool DeleteEvento(string jsonString)
        {
            return newEventos.DeleteEvento(jsonString);
        }

        [HttpPut("UpdateEvento/{jsonString}")]
        public bool UpdateEvento(string jsonString)
        {
            return newEventos.UpdateEvento(jsonString);
        }

    }
}
