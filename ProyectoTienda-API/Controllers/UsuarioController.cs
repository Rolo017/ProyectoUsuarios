using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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


        [Authorize]
        [HttpPost]
        [Route("RegistrarUsuario")]
        public ActionResult RegistrarUsuario(UsuarioObj usuario)
        {
           if (model.RegistrarUsuario(usuario, _configuration)>0)
            {
                return Ok();
            }
            return BadRequest();
           
        }
        
    }


    public class ProductoController : ControllerBase
    {
        private readonly IConfiguration2 _configurationn;
        ProductoModel pmodel=new ProductoModel();

        public ProductoController(IConfiguration2 configuration2) {

            _configurationn=configuration2;

         }

        [Authorize]
        [HttpGet]




     }

}
