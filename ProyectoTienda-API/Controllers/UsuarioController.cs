using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProyectoTienda_API.Entities;
using ProyectoTienda_API.Models;
using System.Security.Claims;

namespace ProyectoTienda_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class UsuarioController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        UsuarioModel model = new UsuarioModel();

        // GET: RolesController
        [HttpGet]
        [Route("GetUsuarios")]
        public List<UsuarioObj> GetUsuarios()
        {
            var usuarios = new List<UsuarioObj>();
            UsuarioObj u = new UsuarioObj();
            return model.Get_Usuario(_configuration);

        }


        //-------Login--------
        public UsuarioController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("ValidarUsuario")]
        public ActionResult<UsuarioObj> ValidarUsuario(UsuarioObj usuario)
        {
            return Ok(model.ValidarUsuario(usuario, _configuration));
            
        }

        [Authorize]
        [HttpGet]
        [Route("Prueba")]
        public ActionResult<string> Prueba(string usuario)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            return Ok(identity?.Name + "   -   " + usuario);
        }

        //-------C-R-U-D-------
        //[Authorize]
        [HttpPost]
        [Route("RegistrarUsuario")]
        public ActionResult RegistrarUsuario(UsuarioObj usuario)
        {
            if (model.RegistrarUsuario(usuario, _configuration) > 0)
            {
                return Ok();
            }
            return BadRequest();

        }

        //[Authorize]
        [HttpPut]
        public ActionResult EditarUsuario(UsuarioObj usuario)
        {
            if (model.ActualizarUsuario(usuario, _configuration) > 0)
            {
                return Ok();
            }
            return BadRequest();

        }

        [HttpDelete]
        public ActionResult EliminarUsuario(int? Cedula)
        {
            try
            {
                if (Cedula == null)
                {
                    NotFound();
                }
                else
                {
                    var persona = model.EliminarUsuario(Cedula, _configuration);
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
