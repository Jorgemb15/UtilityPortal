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
    public class ClienteController : Controller
    {
        // GET: Cliente
        public ActionResult Index()
        {
            return View();
        }

        private const string strIdPantallaLista = "12";
        private const string strIdPantallaMantenimiento = "13";

        [Autorizacion(Roles = ClienteController.strIdPantallaLista)]
        public ActionResult Lista()
        {
            UtilityPortalEntities ModeloBD = new UtilityPortalEntities();
            List<SP_Cliente_Consulta_Result> ModeloVista = null;
            string strResultado = "";

            try
            {
                ModeloVista = ModeloBD.SP_Cliente_Consulta(null, null, null).ToList();
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
                    ModeloVista = new List<SP_Cliente_Consulta_Result>();
                }
                ViewBag.strResultadoOperacion = strResultado.Replace("'", "");
            }

            return View(ModeloVista);
        }

        [HttpPost]
        public ActionResult Lista(SP_Cliente_Consulta_Result objModeloVista)
        {
            UtilityPortalEntities ModeloBD = new UtilityPortalEntities();
            List<SP_Cliente_Consulta_Result> ModeloVista = null;
            string strResultado = "";

            try
            {
                if (objModeloVista.Codigo > 0)
                {
                    ModeloVista = ModeloBD.SP_Cliente_Consulta(objModeloVista.Codigo, objModeloVista.Nombre, objModeloVista.Cedula).ToList();
                }
                else
                {
                    ModeloVista = ModeloBD.SP_Cliente_Consulta(null, objModeloVista.Nombre, objModeloVista.Cedula).ToList();
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
                    ModeloVista = new List<SP_Cliente_Consulta_Result>();
                }
                ViewBag.strResultadoOperacion = strResultado.Replace("'", "");
            }

            return View(ModeloVista);
        }

        [Autorizacion(Roles = ClienteController.strIdPantallaMantenimiento)]
        public ActionResult Mantenimiento(string strCodProceso = ClsConstantes.strCodigoInsertar, int nCodigo = 0)
        {
            UtilityPortalEntities ModeloBD = new UtilityPortalEntities();

            SP_Cliente_Obtener_Result ModeloVista = new SP_Cliente_Obtener_Result();
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
                        ModeloVista = ModeloBD.SP_Cliente_Obtener(nCodigo).FirstOrDefault();
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
                    ModeloVista = new SP_Cliente_Obtener_Result();
                }
                ViewBag.strResultadoOperacion = strResultado.Replace("'", "");

            }

            CargarUbicacionesViewBag();

            return View(ModeloVista);
        }

        [HttpPost]
        public ActionResult Mantenimiento(SP_Cliente_Obtener_Result objModeloVista)
        {
            UtilityPortalEntities ModeloBD = new UtilityPortalEntities();
            SP_Validar_Usuario_Result DatosUsuario = (SP_Validar_Usuario_Result)this.Session["DatosUsuario"];

            string strResultado = "";

            try
            {
                ClsConstantes clsConstantes = new ClsConstantes();
                string ObjetoProceso = "";

                if (!string.IsNullOrEmpty(Request["btnGuardar"]))
                {
                    switch (Request["CodProceso"].ToString())
                    {
                        case ClsConstantes.strCodigoInsertar:
                            objModeloVista.UsuarioCrea = DatosUsuario.Codigo;
                            objModeloVista.FechaCrea = DateTime.Now;
                            objModeloVista.UsuarioModifica = DatosUsuario.Codigo;
                            objModeloVista.FechaModifica = DateTime.Now;
                            ObjetoProceso = clsConstantes.RetornaXML<SP_Cliente_Obtener_Result>(objModeloVista);
                            objModeloVista.Codigo = Convert.ToInt32(ModeloBD.SP_Cliente_Mantenimiento(ClsConstantes.strCodigoInsertar, ObjetoProceso).FirstOrDefault());
                            break;
                        case ClsConstantes.strCodigoModificar:
                            objModeloVista.UsuarioModifica = DatosUsuario.Codigo;
                            objModeloVista.FechaModifica = DateTime.Now;
                            ObjetoProceso = clsConstantes.RetornaXML<SP_Cliente_Obtener_Result>(objModeloVista);
                            objModeloVista.Codigo = Convert.ToInt32(ModeloBD.SP_Cliente_Mantenimiento(ClsConstantes.strCodigoModificar, ObjetoProceso).FirstOrDefault());
                            break;
                    }
                    ViewBag.strCodProceso = ClsConstantes.strCodigoModificar;
                    strResultado = "Actualización de Registro Exitosa";
                }
                if (!string.IsNullOrEmpty(Request["btnEliminar"]))
                {
                    ObjetoProceso = clsConstantes.RetornaXML<SP_Cliente_Obtener_Result>(objModeloVista);
                    objModeloVista.Codigo = Convert.ToInt32(ModeloBD.SP_Cliente_Mantenimiento(ClsConstantes.strCodigoEliminar, ObjetoProceso).FirstOrDefault());
                    objModeloVista = new SP_Cliente_Obtener_Result();
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
                    objModeloVista = new SP_Cliente_Obtener_Result();
                }

                ViewBag.strResultadoOperacion = strResultado.Replace("'", "");
            }

            CargarUbicacionesViewBag();

            return View(objModeloVista);
        }

        void CargarUbicacionesViewBag()
        {
            UtilityPortalEntities ModeloBD = new UtilityPortalEntities();

            ViewBag.lstProvincia = ModeloBD.SP_Provincia_Consulta(null, null).ToList();
            ViewBag.lstDistrito = ModeloBD.SP_Distrito_Consulta(null, null, null).ToList();
        }

    }
}