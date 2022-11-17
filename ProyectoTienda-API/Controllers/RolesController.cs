using Microsoft.AspNetCore.Mvc;
using ProyectoTienda_API.Entities;
using ProyectoTienda_API.Models;

namespace ProyectoTienda_API.Controllers
{
    public class RolesController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        RolesModel model = new RolesModel();

        public RolesController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // GET: RolesController
        [HttpGet]
        [Route("Get")]
        public List<RolObj> Get()
        {
            var roles = new List<RolObj>();
            RolObj r = new RolObj();
            return model.Get_Rol(_configuration);

        }

        // GET: RolesController/Create
        [HttpPost]
        [Route("Registrarrol")]
        public ActionResult RegistrarRoles(RolObj rol)
        {
            //VAlIdACION DE DATOS 
            try
            {
                if (rol == null)
                {
                    return NoContent();

                }

                var persona = model.Registrar_Rol(rol, _configuration);
                return Ok(persona);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // POST: RolesController/Edit/5
        [HttpPut]
        [Route("ActualizarRol")]
        public ActionResult Actualizarrol(RolObj rol)
        {
            try
            {
                if (rol == null)
                {
                    return NoContent();

                }
                else
                {
                    var persona = model.Actualizar_Rol(rol, _configuration);
                    return Ok(persona);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        // POST: RolesController/Delete/5
        [HttpDelete]
        [Route("Delete")]
        public ActionResult Delete(int Rol)
        {
            try
            {
                if (Rol == 0)
                {
                    NotFound();
                }
                else
                {
                    var persona = model.Delete_Rol(Rol, _configuration);
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
