using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProyectoTienda_API.Entities;
using ProyectoTienda_API.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProyectoTienda_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {

        private readonly IConfiguration _configuration;
        ProductoModel model = new ProductoModel();

        public ProductosController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        [AllowAnonymous]
        [HttpPost]
        [Route("ValidarProducto")]
        public ActionResult<ProductoObj> ValidarProducto(ProductoObj producto)
        {
            return Ok(model.ValidarProducto(producto, _configuration));
            
        }

        // GET: api/<ProductosController>
        [HttpPost]
        [Route("Registrar_Producto")]
        public ActionResult RegistrarProducto(ProductoObj producto)
        {
            if (model.Registrar_Producto(producto, _configuration) > 0)
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpPut]
        public ActionResult ActualizarProductol(int Idproducto, ProductoObj producto)
        {
            try
            {
                if (producto == null)
                {
                    return NoContent();

                }
                else
                {
                    var persona = model.Actualizar_Producto(Idproducto, producto, _configuration);
                    return Ok(producto);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete]
        public ActionResult EliminarProducto(int IdProducto)
        {
            try
            {
                if (IdProducto == null)
                {
                    NotFound();
                }
                else
                {
                    var producto = model.Borrar_Producto(IdProducto, _configuration);
                }
                return Ok(IdProducto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        //// GET api/<ProductosController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/<ProductosController>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/<ProductosController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<ProductosController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
