using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProyectoTienda_API.Entities;
using ProyectoTienda_API.Models;

namespace ProyectoTienda_API.Controllers
{
    public class ErrorController : Controller
    {
        // GET: ErrorController
        private readonly IConfiguration _configuration;
        ErrorModel model = new ErrorModel();
        public ErrorController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        // GET: RolesController
        [HttpGet]
        [Route("GetErrores")]
        public List<ErrorObj> GetUsuarios()
        {
            var errores = new List<ErrorObj>();
            ErrorObj E = new ErrorObj();
            return model.Get_Errores(_configuration);

        }
    }
}
