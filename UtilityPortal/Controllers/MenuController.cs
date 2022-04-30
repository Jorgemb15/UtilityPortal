using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UtilityPortalLogical.Logica;
using UtilityPortalLogical.Modelos;

namespace UtilityPortal.Controllers
{
    [Autorizacion]
    public class MenuController : Controller
    {
        private const string strIdPantalla = "1";

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CargarNotificaciones()
        {
            UtilityPortalEntities ModeloBD = new UtilityPortalEntities();

            List<SP_Notificacion_Contrato_Dashboard_Consulta_Result> objNotificaciones = ModeloBD.SP_Notificacion_Contrato_Dashboard_Consulta().ToList();


            return Json(objNotificaciones);
        }

       

    }
}