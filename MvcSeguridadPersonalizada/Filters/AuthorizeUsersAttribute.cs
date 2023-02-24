using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MvcSeguridadPersonalizada.Filters
{
    public class AuthorizeUsersAttribute : AuthorizeAttribute
        , IAuthorizationFilter
    {
        //EN ESTE METODO ENTRARA CUANDO UN CONTROLADOR ESTE DECORADO
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            //LOS USUARIOS SON ALMACENADOS DENTRO DE HttpContext
            //Y DENTRO DE User
            //UN USUARIO ESTA COMPUESTO POR UNA IDENTIDAD Y UN PRINCIPAL
            //PODEMOS SABER EL NOMBRE DEL USUARIO O SI ESTA AUTENTICADO
            var user = context.HttpContext.User;
            if (user.Identity.IsAuthenticated == false)
            {
                //SI EL USUARIO NO SE HA VALIDADO, PUES NO LE 
                //DEJAMOS ENTRAR Y LE LLEVAMOS A LOGIN
                RouteValueDictionary rutalogin =
                    new RouteValueDictionary(new
                    {
                        controller = "Manager", action = "Login"
                    });
                context.Result = new RedirectToRouteResult(rutalogin);
            }
        }
    }
}
