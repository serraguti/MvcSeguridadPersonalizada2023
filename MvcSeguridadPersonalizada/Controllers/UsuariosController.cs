using Microsoft.AspNetCore.Mvc;
using MvcSeguridadPersonalizada.Filters;

namespace MvcSeguridadPersonalizada.Controllers
{
    public class UsuariosController : Controller
    {
        [AuthorizeUsers]
        public IActionResult Perfil()
        {
            return View();
        }
    }
}
