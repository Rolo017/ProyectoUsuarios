using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProyectoTienda_API.Entities;
using ProyectoTienda_API.Models;

namespace ProyectoTienda_API.Controllers
{
    public class VentasController : Controller
    {
        private readonly IConfiguration _configuration;
        VentasModel model = new VentasModel();

        [HttpGet]
        [Route("GetVentas")]
        public List<VentasObj> GetVentas()
        {
            var ventas = new List<VentasObj>();
            VentasObj v = new VentasObj();
            return model.Get_Ventas(_configuration);
        }

        public VentasController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("ValidarVentas")]
        public ActionResult<VentasObj> ValidarVentas(VentasObj ventas)
        {
            return Ok(model.ValidarVentas(ventas, _configuration));
           
        }


        [HttpPost]
        [Route("RegistrarVenta")]
        public ActionResult RegistrarVentas(VentasObj venta)
        {
            if (model.RegistrarVenta(venta, _configuration) > 0)
            {
                return Ok();
            }
            return BadRequest();

        }

        //[Authorize]
        [HttpPut("{id}")]
        public ActionResult EditarUsuario(VentasObj venta)
        {
            if (model.ActualizarVenta(venta, _configuration) > 0)
            {
                return Ok();
            }
            return BadRequest();

        }

        [HttpDelete("{id}")]
        public ActionResult EliminarUsuario(int ID_Venta)
        {
            try
            {
                if (ID_Venta == null)
                {
                    NotFound();
                }
                else
                {
                    var persona = model.EliminarVenta(ID_Venta, _configuration);
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
