
using System;
using System.Threading.Tasks;
using knewinAPI.Models;
using knewinAPI.Repositories;
using knewinAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Knewin.Controllers
{

    [Route("api")]
    [ApiController]
    [Authorize]
    public class LoginController : Controller
    {
        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> Authenticate([FromBody] User model)
        {


            var user = UserRepository.Get(model.Username, model.Password);

            if (user == null )
                return NotFound(new { message = "Usuário ou senha inválidos" });

            var token = TokenService.GenerateToken(user);
            user.Password = "";
            return new
            {
                user = user,
                token = token
            };
        }

    }
}