using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MvcSeguridadPersonalizada.Controllers
{
    public class ManagerController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult>
            Login(string username, string password)
        {
            if (username.ToLower() == "admin" 
                && password.ToLower() == "admin")
            {
                //DEBEMOS CREAR UNA IDENTIDAD (NAME Y ROLE)
                ClaimsIdentity identity =
                    new ClaimsIdentity
                    (CookieAuthenticationDefaults.AuthenticationScheme
                    , ClaimTypes.Name, ClaimTypes.Role);
                //CREAMOS LAS CARACTERISTICAS PARA EL USUARIO (CLAIMS)
                Claim userName = new Claim(ClaimTypes.Name, username);
                Claim role = new Claim(ClaimTypes.Role, "USUARIO");
                identity.AddClaim(userName);
                identity.AddClaim(role);
                ClaimsPrincipal userPrincipal =
                    new ClaimsPrincipal(identity);
                //POR ULTIMO, ALMACENAMOS AL USUARIO EN LA SESSION
                //Y EN SU COOKIE DE EXPLORADOS
                await HttpContext.SignInAsync
                    (CookieAuthenticationDefaults.AuthenticationScheme
                    , userPrincipal,
                    new AuthenticationProperties
                    {
                        ExpiresUtc = DateTime.Now.AddMinutes(15)
                    });
                return RedirectToAction("Perfil", "Usuarios");
            }
            else
            {
                ViewData["MENAJE"] = "Usuario/password incorrectos";
            }
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync
                (CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
    }
}
