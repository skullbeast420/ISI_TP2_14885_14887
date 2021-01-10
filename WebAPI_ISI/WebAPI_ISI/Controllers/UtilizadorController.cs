using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI_ISI.Models;

namespace WebAPI_ISI.Controllers
{
    [ApiController]
    [Route("api/Utilizador")]
    public class UtilizadorController : Controller
    {

        Utilizadores newUtilizadores = new Utilizadores();

        /// <summary>
        /// Método POST que insere um novo Utilizador na Base de Dados
        /// </summary>
        /// <param name="jsonString">String em formato JSON com a informação do Utilizador a ser introduzido</param>
        /// <returns>
        /// 200 OK - Sucesso no registo
        /// 400 Bad Request - Já existe um utilizador com o email introduzido
        /// </returns>
        [HttpPost("Registo/{jsonString}")]
        public ActionResult<bool> Registo(string jsonString)
        {
            if (newUtilizadores.Registo(jsonString) == true) return Ok();

            else { return BadRequest(); }
        }

        /// <summary>
        /// Método GET que irá executar a função de Login
        /// </summary>
        /// <param name="email">E-Mail introduzido na aplicação</param>
        /// <param name="password">Password introduzida na aplicação</param>
        /// <returns>
        /// Objeto do tipo "Aux" cuja propriedade "json" irá conter uma string em formato JSON com os dados do Utilizador
        /// </returns>
        [HttpGet("Login/{email}/{password}")]
        public Aux Login(string email, string password)
        {
            return newUtilizadores.Login(email, password);
        }

    }
}
