using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UtilityPortalLogical.Logica;
using UtilityPortalLogical.Modelos;

namespace UtilityPortal.Controllers
{
    [Autorizacion]
    public class ContratoController : Controller
    {
        private const string strIdPantallaLista = "21";
        private const string strIdPantallaMantenimiento = "22";

        // GET: Contrato
        public ActionResult Index()
        {
            return View();
        }

        [Autorizacion(Roles = ContratoController.strIdPantallaLista)]
        public ActionResult Lista()
        {
            UtilityPortalEntities ModeloBD = new UtilityPortalEntities();
            List<SP_Contrato_Consulta_Result> ModeloVista = null;
            string strResultado = "";

            try
            {
                ModeloVista = ModeloBD.SP_Contrato_Consulta(null, null, null).ToList();
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
                    ModeloVista = new List<SP_Contrato_Consulta_Result>();
                }
                ViewBag.strResultadoOperacion = strResultado.Replace("'", "");
            }

            return View(ModeloVista);
        }

        [HttpPost]
        public ActionResult Lista(SP_Contrato_Consulta_Result objModeloVista)
        {
            UtilityPortalEntities ModeloBD = new UtilityPortalEntities();
            List<SP_Contrato_Consulta_Result> ModeloVista = null;
            string strResultado = "";

            try
            {
                if (objModeloVista.Codigo > 0)
                {
                    if (objModeloVista.CodCliente > 0)
                    {
                        ModeloVista = ModeloBD.SP_Contrato_Consulta(objModeloVista.Codigo, objModeloVista.CodCliente, objModeloVista.Nombre_Completo).ToList();
                    }
                    else
                    {
                        ModeloVista = ModeloBD.SP_Contrato_Consulta(objModeloVista.Codigo, null, objModeloVista.Nombre_Completo).ToList();
                    }
                }
                else
                {
                    if (objModeloVista.CodCliente > 0)
                    {
                        ModeloVista = ModeloBD.SP_Contrato_Consulta(null, objModeloVista.CodCliente, objModeloVista.Nombre_Completo).ToList();
                    }
                    else
                    {
                        ModeloVista = ModeloBD.SP_Contrato_Consulta(null, null, objModeloVista.Nombre_Completo).ToList();
                    }
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
                    ModeloVista = new List<SP_Contrato_Consulta_Result>();
                }
                ViewBag.strResultadoOperacion = strResultado.Replace("'", "");
            }

            return View(ModeloVista);
        }

        [Autorizacion(Roles = ContratoController.strIdPantallaMantenimiento)]
        public ActionResult Mantenimiento(string strCodProceso = ClsConstantes.strCodigoInsertar, int nCodigo = 0)
        {
            UtilityPortalEntities ModeloBD = new UtilityPortalEntities();
            ContratoModel ModeloVista = new ContratoModel();

            if (Session["Moneda"] == null)
            {
                Session["Moneda"] = "C";
            }

            string strResultado = "";

            try
            {
                switch (strCodProceso)
                {
                    case ClsConstantes.strCodigoInsertar:
                        ViewBag.strCodProceso = ClsConstantes.strCodigoInsertar;
                        ModeloVista.ContratoCabeceraModel.Moneda = Session["Moneda"].ToString();
                        if (ModeloVista.ContratoCabeceraModel.Moneda.Equals("D"))
                        {
                            Session["Moneda"] = "D";
                        }
                        break;
                    case ClsConstantes.strCodigoModificar:
                        ViewBag.strCodProceso = ClsConstantes.strCodigoModificar;
                        ModeloVista.ContratoCabeceraModel = ModeloBD.SP_Contrato_Obtener(nCodigo).FirstOrDefault();
                        ModeloVista.ContratoDetalleModel = ModeloBD.SP_ContratoDetalle_Obtener(nCodigo).ToList();
                        if (ModeloVista.ContratoCabeceraModel.Moneda.Equals("D"))
                        {
                            Session["Moneda"] = "D";
                        }
                        break;
                    case ClsConstantes.strCodigoVisualizar:
                        ViewBag.strCodProceso = ClsConstantes.strCodigoVisualizar;
                        ModeloVista.ContratoCabeceraModel = ModeloBD.SP_Contrato_Obtener(nCodigo).FirstOrDefault();
                        ModeloVista.ContratoDetalleModel = ModeloBD.SP_ContratoDetalle_Obtener(nCodigo).ToList();
                        if (ModeloVista.ContratoCabeceraModel.Moneda.Equals("D"))
                        {
                            Session["Moneda"] = "D";
                        }
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
                    ModeloVista = new ContratoModel();
                }
                ViewBag.strResultadoOperacion = strResultado.Replace("'", "");

            }
            CargarClientesViewBag();

            CargarProveedoresViewBag();

            CargarTipoContratosViewBag();

            return View(ModeloVista);
        }

        [HttpPost]
        public ActionResult Mantenimiento(ContratoModel objModeloVista)
        {
            UtilityPortalEntities ModeloBD = new UtilityPortalEntities();
            SP_Validar_Usuario_Result DatosUsuario = (SP_Validar_Usuario_Result)this.Session["DatosUsuario"];


            string strResultado = "";

            try
            {
                ClsConstantes clsConstantes = new ClsConstantes();
                string ObjetoProceso = "";

                int nLinea = 1;

                foreach (SP_ContratoDetalle_Obtener_Result Linea in objModeloVista.ContratoDetalleModel)
                {
                    Linea.NumLinea = nLinea;
                    nLinea += 1;
                }

                if (!string.IsNullOrEmpty(Request["btnGuardar"]) || (!string.IsNullOrEmpty(Request["hddGuardar"]) && Request["hddGuardar"] == "Guardar"))
                {
                    switch (Request["CodProceso"].ToString())
                    {
                        case ClsConstantes.strCodigoInsertar:
                            objModeloVista.ContratoCabeceraModel.UsuarioCrea = DatosUsuario.Codigo;
                            objModeloVista.ContratoCabeceraModel.FechaCrea = DateTime.Now;
                            objModeloVista.ContratoCabeceraModel.UsuarioModifica = DatosUsuario.Codigo;
                            objModeloVista.ContratoCabeceraModel.FechaModifica = DateTime.Now;
                            objModeloVista.ContratoCabeceraModel.Fecha = DateTime.Now;
                            objModeloVista.ContratoCabeceraModel.FechaFin = objModeloVista.ContratoCabeceraModel.FechaInicio.AddMonths(objModeloVista.ContratoCabeceraModel.Duracion);

                            if (objModeloVista.ContratoCabeceraModel.NotificaFinContratoMes is null)
                            {
                                objModeloVista.ContratoCabeceraModel.NotificaFinContratoMes = "N";
                            }
                            if (objModeloVista.ContratoCabeceraModel.NotificaFinContratoSemana is null)
                            {
                                objModeloVista.ContratoCabeceraModel.NotificaFinContratoSemana = "N";
                            }
                            if (objModeloVista.ContratoCabeceraModel.GeneraPagoProveedor.Equals("S"))
                            {
                                objModeloVista.ContratoCabeceraModel.NotificaPagoProveedor = "S";
                            }
                            else
                            {
                                objModeloVista.ContratoCabeceraModel.NotificaPagoProveedor = "N";
                            }
                        
                            ObjetoProceso = clsConstantes.RetornaXML<ContratoModel>(objModeloVista);
                            objModeloVista.ContratoCabeceraModel.Codigo = Convert.ToInt32(ModeloBD.SP_Contrato_Mantenimiento(ClsConstantes.strCodigoInsertar, ObjetoProceso).FirstOrDefault());
                            break;
                        case ClsConstantes.strCodigoModificar:
                            objModeloVista.ContratoCabeceraModel.UsuarioModifica = DatosUsuario.Codigo;
                            objModeloVista.ContratoCabeceraModel.FechaModifica = DateTime.Now;

                            objModeloVista.ContratoCabeceraModel.FechaFin = objModeloVista.ContratoCabeceraModel.FechaInicio.AddMonths(objModeloVista.ContratoCabeceraModel.Duracion);

                            if (objModeloVista.ContratoCabeceraModel.NotificaFinContratoMes is null)
                            {
                                objModeloVista.ContratoCabeceraModel.NotificaFinContratoMes = "N";
                            }
                            if (objModeloVista.ContratoCabeceraModel.NotificaFinContratoSemana is null)
                            {
                                objModeloVista.ContratoCabeceraModel.NotificaFinContratoSemana = "N";
                            }
                            if (objModeloVista.ContratoCabeceraModel.GeneraPagoProveedor.Equals("S"))
                            {
                                objModeloVista.ContratoCabeceraModel.NotificaPagoProveedor = "S";
                            }
                            else
                            {
                                objModeloVista.ContratoCabeceraModel.NotificaPagoProveedor = "N";
                            }

                            ObjetoProceso = clsConstantes.RetornaXML<ContratoModel>(objModeloVista);
                            objModeloVista.ContratoCabeceraModel.Codigo = Convert.ToInt32(ModeloBD.SP_Contrato_Mantenimiento(ClsConstantes.strCodigoModificar, ObjetoProceso).FirstOrDefault());
                            break;
                    }
                    ViewBag.strCodProceso = ClsConstantes.strCodigoModificar;
                    strResultado = "Actualización de Registro Exitosa";
                }
                if (!string.IsNullOrEmpty(Request["btnEliminar"]))
                {

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
                    objModeloVista = new ContratoModel();
                }

                ViewBag.strResultadoOperacion = strResultado.Replace("'", "");
            }

            CargarClientesViewBag();

            CargarProveedoresViewBag();

            CargarTipoContratosViewBag();

            return View(objModeloVista);
        }

        void CargarClientesViewBag()
        {
            UtilityPortalEntities ModeloBD = new UtilityPortalEntities();

            ViewBag.lstCliente = ModeloBD.SP_Cliente_Consulta(null, null, null).ToList();
        }

        void CargarProveedoresViewBag()
        {
            UtilityPortalEntities ModeloBD = new UtilityPortalEntities();

            ViewBag.lstProveedor = ModeloBD.SP_Proveedor_Consulta(null, null, null).ToList();
        }

        void CargarTipoContratosViewBag() {
            UtilityPortalEntities ModeloBD = new UtilityPortalEntities();

            ViewBag.lstTipoContrato = ModeloBD.SP_Tipo_Contrato_Consulta(null, null).ToList();
        }


        [AcceptVerbs(HttpVerbs.Post | HttpVerbs.Get)]
        public ActionResult ListaProductos(int nCodigo, string strNombre)
        {
            UtilityPortalEntities ModeloBD = new UtilityPortalEntities();
            List<SP_Producto_Consulta_Result> ModeloVista = null;
            string strResultado = "";

            try
            {
                if (nCodigo > 0)
                {
                    ModeloVista = ModeloBD.SP_Producto_Consulta(nCodigo, strNombre).ToList();
                }
                else
                {
                    ModeloVista = ModeloBD.SP_Producto_Consulta(null, strNombre).ToList();
                }

                if (Session["Moneda"] != null)
                {
                    if (Session["Moneda"].ToString().Equals("D"))
                    {
                        foreach (SP_Producto_Consulta_Result Producto in ModeloVista)
                        {
                            Producto.Precio = Producto.PrecioDolar;
                        }
                    }

                }
                else
                {
                    Session["Moneda"] = "C";
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

            return this.Request.IsAjaxRequest()
                ? this.PartialView("ListaProductos", ModeloVista)
                : this.View(ModeloVista) as ActionResult;
        }

        public ActionResult Calcular(List<List<string>> DetalleContrato, float nPorcentajeExoneracion, float nPorcentajeDescuento)
        {
            ClsConstantes clsConstantes = new ClsConstantes();
            float SubTotal = 0;
            float Descuento;
            float Iva;
            float Total;

            if (DetalleContrato != null)
            {
                foreach (List<string> LineaContrato in DetalleContrato)
                {
                    float Precio = 0;
                    float Cantidad = 0;
                    float.TryParse(LineaContrato[2], out Precio);
                    float.TryParse(LineaContrato[3], out Cantidad);
                    SubTotal += (Precio * Cantidad);
                }
            }
            Descuento = SubTotal * (nPorcentajeDescuento / 100);

            Iva = clsConstantes.nPorcentajeIVA - nPorcentajeExoneracion;
            Iva = (SubTotal - Descuento) * (Iva / 100);

            Total = SubTotal - Descuento + Iva;

            return new JsonResult()
            {
                Data = new
                {
                    SubTotal = SubTotal,
                    Descuento = Descuento,
                    Iva = Iva,
                    Total = Total,
                    DetalleContrato = DetalleContrato
                }
            };
        }

        public ActionResult ObtenerProducto(int nCodProducto)
        {
            UtilityPortalEntities ModeloBD = new UtilityPortalEntities();

            SP_Producto_Obtener_Result objProducto = ModeloBD.SP_Producto_Obtener(nCodProducto).FirstOrDefault();

            if (Session["Moneda"] != null)
            {
                if (Session["Moneda"].ToString().Equals("D"))
                {
                    objProducto.Precio = objProducto.PrecioDolar;
                }

            }
            else
            {
                Session["Moneda"] = "C";
            }
            return Json(objProducto);
        }

        public ActionResult CambiarMoneda(string strCodMoneda, List<List<string>> DetalleContrato)
        {
            UtilityPortalEntities ModeloBD = new UtilityPortalEntities();
            Session["Moneda"] = strCodMoneda;
            float Total = 0;

            if (DetalleContrato != null)
            {
                foreach (List<string> LineaContrato in DetalleContrato)
                {
                    SP_Producto_Obtener_Result objProducto = ModeloBD.SP_Producto_Obtener(int.Parse(LineaContrato[0])).FirstOrDefault();
                    if (objProducto != null)
                    {
                        if (Session["Moneda"].ToString().Equals("D"))
                        {
                            LineaContrato[2] = objProducto.PrecioDolar.ToString();
                        }
                        else
                        {
                            LineaContrato[2] = objProducto.Precio.ToString();
                        }
                    }
                    float Precio = 0;
                    float Cantidad = 0;
                    float.TryParse(LineaContrato[2], out Precio);
                    float.TryParse(LineaContrato[3], out Cantidad);
                    Total += (Precio * Cantidad);
                }
            }

            return new JsonResult()
            {
                Data = new
                {
                    Total = Total,
                    DetalleContrato = DetalleContrato
                }
            };

        }

        [HttpGet]
        public ActionResult DescargarContrato(int nCodContrato)
        {
            ClsConstantes clsConstantes = new ClsConstantes();
            ReporteModel clsReporte = new ReporteModel();



            ReportParameter ParametroReporte = new ReportParameter("Codigo", nCodContrato.ToString());
            DbParameter[] Parametrosds1 = new DbParameter[1];
            Parametrosds1[0] = new SqlParameter();
            Parametrosds1[0].ParameterName = "@Codigo";
            Parametrosds1[0].Value = nCodContrato;
            DbParameter[] Parametrosds2 = new DbParameter[1];
            Parametrosds2[0] = new SqlParameter();
            Parametrosds2[0].ParameterName = "@Codigo";
            Parametrosds2[0].Value = nCodContrato;


            ReportViewer viewer = new ReportViewer();
            viewer.LocalReport.Refresh();
            viewer.LocalReport.ReportPath = Server.MapPath("~/Reportes/Cotizador.rdl");
            viewer.LocalReport.SetParameters(ParametroReporte);

            string strScript = "SELECT COT.*, DET.*, CLI.Nombre + ' ' + CLI.APELLIDO1 + ' ' + CLI.APELLIDO2 AS NOMBRE_COMPLETO, CLI.Correo, CLI.Telefono1, PRO.Nombre AS NOMBRE_Producto, DET.CantidadProducto * DET.PrecioProducto AS Total_Linea FROM Contrato AS COT LEFT JOIN ContratoDetalle AS DET ON COT.Codigo = DET.CodContrato LEFT JOIN Cliente AS CLI ON COT.CodCliente = CLI.Codigo LEFT JOIN Producto AS PRO ON DET.CodProducto = PRO.Codigo WHERE COT.CODIGO = @Codigo";
            DataTable Encabezado = clsReporte.ObtenerDatosReporte(strScript, Parametrosds1);
            ReportDataSource datasource1 = new ReportDataSource("DataSet1", Encabezado);

            strScript = "SELECT Moneda, SubTotal, Descuento, Iva, Total, UsuarioCrea FROM Contrato WHERE CODIGO = @Codigo";
            DataTable Detalle = clsReporte.ObtenerDatosReporte(strScript, Parametrosds2);
            ReportDataSource datasource2 = new ReportDataSource("DataSetTotal", Detalle);

            viewer.LocalReport.DataSources.Clear();
            viewer.LocalReport.DataSources.Add(datasource1);
            viewer.LocalReport.DataSources.Add(datasource2);

            Warning[] warnings;
            string[] streamIds;
            string mimeType = string.Empty;
            string encoding = string.Empty;
            string extension = string.Empty;

            //EventLog eventLog = new EventLog();
            //eventLog.Source = "webcotizador";
            //eventLog.WriteEntry("pruebaaaa.", EventLogEntryType.Information);

            //EventLog.CreateEventSource("dsafdas", "webcotizador");
            //Console.WriteLine("Write from second source ");
            //EventLog.WriteEntry("webcotizador",
            //                    "asdfdsf",
            //                    EventLogEntryType.Warning);

            byte[] bytes = viewer.LocalReport.Render(format: "PDF", deviceInfo: "");

            //byte[] bytes = viewer.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);


            return File(bytes, "application/pdf", "Contrato_" + nCodContrato + ".pdf");
        }

        public ActionResult ObtenerExoneracion(int nCodCliente)
        {
            UtilityPortalEntities ModeloBD = new UtilityPortalEntities();
            double nPorcentajeExoneracion = 0;

            SP_Cliente_Obtener_Result Cliente = ModeloBD.SP_Cliente_Obtener(nCodCliente).FirstOrDefault();

            if (Cliente != null)
            {
                nPorcentajeExoneracion = Cliente.PorcentajeExoneracion;
            }

            return Json(nPorcentajeExoneracion);
        }

    }
}