using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using UtilityPortalLogical.Modelos;


namespace UtilityPortalLogical.Logica
{
    public class Autorizacion : AuthorizeAttribute
    {

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);

            if (filterContext.Result is HttpUnauthorizedResult)
            {
                filterContext.Result = new RedirectResult("~/Login/AccesoDenegado");
            }
            else if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                filterContext.Result = new RedirectToRouteResult(new
                RouteValueDictionary(new { controller = "Login", action = "SesionExpirada" }));
            }
            else if (filterContext.HttpContext.Session["SesionIniciada"] == null || filterContext.HttpContext.Session["DatosUsuario"] == null)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Login", action = "SesionExpirada" }));
            }
            else
            {
                UtilityPortalEntities ModeloBD = new UtilityPortalEntities();
                SP_Validar_Usuario_Result DatosUsuario = (SP_Validar_Usuario_Result)filterContext.HttpContext.Session["DatosUsuario"];

                filterContext.Controller.ViewBag.ListaPermisosUsuario = ModeloBD.SP_Obtener_Permisos_Usuario(DatosUsuario.Codigo).ToList();
            }
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Login", action = "SesionExpirada" }));
            }
            else
            {
                filterContext.Result = new RedirectResult("~/Login/AccesoDenegado");
            }
        }
    }
}
