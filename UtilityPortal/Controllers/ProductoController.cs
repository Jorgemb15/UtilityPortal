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
    public class ProductoController : Controller
    {
        private const string strIdPantallaLista = "18";
        private const string strIdPantallaMantenimiento = "19";

        // GET: Producto
        public ActionResult Index()
        {
            return View();
        }

        [Autorizacion(Roles = ProductoController.strIdPantallaLista)]
        public ActionResult Lista()
        {
            UtilityPortalEntities ModeloBD = new UtilityPortalEntities();
            List<SP_Producto_Consulta_Result> ModeloVista = null;
            string strResultado = "";

            try
            {
                ModeloVista = ModeloBD.SP_Producto_Consulta(null, null).ToList();
            }
            catch (Exception error)
            {
                strResultado = "Ocurrió un Error al Listar : " + error.Message;

                if (error.InnerException != null)
                {
                    strResultado = "Ocurrió un Error al Listar : " + error.InnerException.Message;
                }

            }
            finally
            {
                if (ModeloVista == null)
                {
                    ModeloVista = new List<SP_Producto_Consulta_Result>();
                }
                ViewBag.strResultadoOperacion = strResultado.Replace("'", "");
            }

            return View(ModeloVista);
        }

        [HttpPost]
        public ActionResult Lista(SP_Producto_Consulta_Result objModeloVista)
        {
            UtilityPortalEntities ModeloBD = new UtilityPortalEntities();
            List<SP_Producto_Consulta_Result> ModeloVista = null;
            string strResultado = "";

            try
            {
                if (objModeloVista.Codigo > 0)
                {
                    ModeloVista = ModeloBD.SP_Producto_Consulta(objModeloVista.Codigo, objModeloVista.Nombre).ToList();
                }
                else
                {
                    ModeloVista = ModeloBD.SP_Producto_Consulta(null, objModeloVista.Nombre).ToList();
                }
            }
            catch (Exception error)
            {
                strResultado = "Ocurrió un Error al Listar : " + error.Message;

                if (error.InnerException != null)
                {
                    strResultado = "Ocurrió un Error al Listar : " + error.InnerException.Message;
                }
            }
            finally
            {
                if (ModeloVista == null)
                {
                    ModeloVista = new List<SP_Producto_Consulta_Result>();
                }
                ViewBag.strResultadoOperacion = strResultado.Replace("'", "");
            }

            return View(ModeloVista);
        }

        [Autorizacion(Roles = ProductoController.strIdPantallaMantenimiento)]
        public ActionResult Mantenimiento(string strCodProceso = ClsConstantes.strCodigoInsertar, int nCodigo = 0)
        {
            UtilityPortalEntities ModeloBD = new UtilityPortalEntities();

            SP_Producto_Obtener_Result ModeloVista = new SP_Producto_Obtener_Result();
            string strResultado = "";

            try
            {
                switch (strCodProceso)
                {
                    case ClsConstantes.strCodigoInsertar:
                        ViewBag.strCodProceso = ClsConstantes.strCodigoInsertar;
                        break;
                    case ClsConstantes.strCodigoModificar:
                        ViewBag.strCodProceso = ClsConstantes.strCodigoModificar;
                        ModeloVista = ModeloBD.SP_Producto_Obtener(nCodigo).FirstOrDefault();
                        break;
                }
            }
            catch (Exception error)
            {
                strResultado = "Ocurrió un Error al Obtener : " + error.Message;

                if (error.InnerException != null)
                {
                    strResultado = "Ocurrió un Error al Obtener : " + error.InnerException.Message;
                }
            }
            finally
            {
                if (ModeloVista == null)
                {
                    ModeloVista = new SP_Producto_Obtener_Result();
                }
                ViewBag.strResultadoOperacion = strResultado.Replace("'", "");

            }

            return View(ModeloVista);
        }

        [HttpPost]
        public ActionResult Mantenimiento(SP_Producto_Obtener_Result objModeloVista)
        {
            UtilityPortalEntities ModeloBD = new UtilityPortalEntities();

            string strResultado = "";

            try
            {
                ClsConstantes clsConstantes = new ClsConstantes();

                string ObjetoProceso = clsConstantes.RetornaXML<SP_Producto_Obtener_Result>(objModeloVista);

                if (!string.IsNullOrEmpty(Request["btnGuardar"]))
                {
                    switch (Request["CodProceso"].ToString())
                    {
                        case ClsConstantes.strCodigoInsertar:
                            objModeloVista.Codigo = Convert.ToInt32(ModeloBD.SP_Producto_Mantenimiento(ClsConstantes.strCodigoInsertar, ObjetoProceso).FirstOrDefault());
                            break;
                        case ClsConstantes.strCodigoModificar:
                            objModeloVista.Codigo = Convert.ToInt32(ModeloBD.SP_Producto_Mantenimiento(ClsConstantes.strCodigoModificar, ObjetoProceso).FirstOrDefault());
                            break;
                    }
                    ViewBag.strCodProceso = ClsConstantes.strCodigoModificar;
                    strResultado = "Actualización de Registro Exitosa";
                }
                if (!string.IsNullOrEmpty(Request["btnEliminar"]))
                {
                    objModeloVista.Codigo = Convert.ToInt32(ModeloBD.SP_Producto_Mantenimiento(ClsConstantes.strCodigoEliminar, ObjetoProceso).FirstOrDefault());
                    objModeloVista = new SP_Producto_Obtener_Result();
                    strResultado = "Actualización de Registro Exitosa";
                    ViewBag.strCodProceso = ClsConstantes.strCodigoInsertar;
                }
            }
            catch (Exception error)
            {
                strResultado = "Ocurrió un Error  : " + error.Message;
                if (error.InnerException != null)
                {
                    strResultado = error.InnerException.Message;
                }
                ViewBag.strCodProceso = Request["CodProceso"].ToString();
            }
            finally
            {
                if (objModeloVista == null)
                {
                    objModeloVista = new SP_Producto_Obtener_Result();
                }

                ViewBag.strResultadoOperacion = strResultado.Replace("'", "");
            }

            return View(objModeloVista);
        }
    }
}