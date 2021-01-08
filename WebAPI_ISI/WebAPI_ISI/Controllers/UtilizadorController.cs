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

        [HttpPost("Registo/{jsonString}")]
        public bool Registo(string jsonString)
        {
            return newUtilizadores.Registo(jsonString);
        }

        [HttpGet("Login/{email}/{password}")]
        public Aux Login(string email, string password)
        {
            return newUtilizadores.Login(email, password);
        }

    }
}
