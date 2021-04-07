using MapForms.DataAccess.Data.Repositories;
using MapForms.Models.DTOS;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MapForms.WebAPI.Controllers
{
    public class AccontController : Controller
    {
        private readonly IWorkContainer container;

        public AccontController(IWorkContainer container)
        {
            this.container = container;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UsuarioToken>> RegisterUser([FromBody] UsuarioCreateDTO userInfo)
        {
            var result = await container.Usuarios.RegisterUser(userInfo);
            if (result.StatusResponse == StatusResponse.Ok)
            {
                return result.Result;
            }else
            {
                return BadRequest(result.MessageError);
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult<UsuarioToken>> Login([FromBody] UserLogin userInfo)
        {
            var result = await container.Usuarios.LoginUser(userInfo);
            if (result.StatusResponse == StatusResponse.Ok)
            {
                return result.Result;
            }
            else
            {
                ModelState.AddModelError(string.Empty, result.MessageError);
                return BadRequest(result.MessageError);
            }
        }


    }
}
