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

        /// <summary>
        /// Método POST que adiciona um novo Evento à Base de Dados
        /// </summary>
        /// <param name="jsonString">String em formato JSON com a informação do novo Evento a ser adicionado</param>
        /// <returns>
        /// 200 OK - Sucesso na criação do Evento
        /// 400 Bad Request - O utilizador já criou um evento com os mesmos dados
        /// </returns>
        [HttpPost("AddEvento/{jsonString}")]
        public ActionResult<bool> AddEvento(string jsonString)
        {
            if (newEventos.AddEvento(jsonString) == true) return Ok();

            else { return BadRequest(); }
        }

        /// <summary>
        /// Método GET que retorna todos os eventos do utilizador
        /// </summary>
        /// <param name="id_utilizador">Id do utilizador</param>
        /// <returns>
        /// Objeto do tipo "Aux" cuja propriedade "json" irá conter uma string em formato JSON com os eventos do utilizador
        /// </returns>
        [HttpGet("GetEventos/{id_utilizador}")]
        public Aux GetEventos(string id_utilizador)
        {
            return newEventos.GetEventos(id_utilizador);
        }

        /// <summary>
        /// Método DELETE que irá eliminar um Evento da Base de Dados
        /// </summary>
        /// <param name="jsonString">String em formato JSON que irá conter a informação do Evento a ser eliminado</param>
        /// <returns></returns>
        [HttpDelete("DeleteEvento/{jsonString}")]
        public bool DeleteEvento(string jsonString)
        {
            return newEventos.DeleteEvento(jsonString);
        }

        /// <summary>
        /// Método PUT que irá alterar a informação de um Evento
        /// </summary>
        /// <param name="jsonString">String em formato JSON que irá conter a informação do Evento a ser alterado</param>
        /// <returns></returns>
        [HttpPut("UpdateEvento/{jsonString}")]
        public bool UpdateEvento(string jsonString)
        {
            return newEventos.UpdateEvento(jsonString);
        }

    }
}
