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
    public class UbicacionController : Controller
    {
        // GET: Ubicacion
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CargarCantones(int nCodProvincia)
        {
            UtilityPortalEntities ModeloBD = new UtilityPortalEntities();

            List<SP_Canton_Consulta_Result> lstCantones = ModeloBD.SP_Canton_Consulta(null, null, nCodProvincia).ToList();

            return Json(lstCantones);
        }

        public ActionResult CargarDistritos(int nCodCanton)
        {
            UtilityPortalEntities ModeloBD = new UtilityPortalEntities();

            List<SP_Distrito_Consulta_Result> lstDistritos = ModeloBD.SP_Distrito_Consulta(null, null, nCodCanton).ToList();

            return Json(lstDistritos);
        }

        public ActionResult CargarUbicacionCliente(int nCodDistrito)
        {
            UtilityPortalEntities ModeloBD = new UtilityPortalEntities();

            SP_Ubicacion_Obtener_Result objUbicacion = ModeloBD.SP_Ubicacion_Obtener(nCodDistrito).FirstOrDefault();

            return Json(objUbicacion);
        }

    }
}