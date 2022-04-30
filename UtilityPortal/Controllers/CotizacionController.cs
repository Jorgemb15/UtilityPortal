using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using UtilityPortalLogical.Logica;
using UtilityPortalLogical.Modelos;

namespace UtilityPortal.Controllers
{
    [Autorizacion]
    public class CotizacionController : Controller
    {
        private const string strIdPantallaLista = "21";
        private const string strIdPantallaMantenimiento = "22";

        // GET: Cotizacion
        public ActionResult Index()
        {
            return View();
        }

        [Autorizacion(Roles = CotizacionController.strIdPantallaLista)]
        public ActionResult Lista()
        {
            UtilityPortalEntities ModeloBD = new UtilityPortalEntities();
            List<SP_Cotizacion_Consulta_Result> ModeloVista = null;
            string strResultado = "";

            try
            {
                ModeloVista = ModeloBD.SP_Cotizacion_Consulta(null, null, null).ToList();
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
                    ModeloVista = new List<SP_Cotizacion_Consulta_Result>();
                }
                ViewBag.strResultadoOperacion = strResultado.Replace("'", "");
            }

            return View(ModeloVista);
        }

        [HttpPost]
        public ActionResult Lista(SP_Cotizacion_Consulta_Result objModeloVista)
        {
            UtilityPortalEntities ModeloBD = new UtilityPortalEntities();
            List<SP_Cotizacion_Consulta_Result> ModeloVista = null;
            string strResultado = "";

            try
            {
                if (objModeloVista.Codigo > 0)
                {
                    if (objModeloVista.CodCliente > 0)
                    {
                        ModeloVista = ModeloBD.SP_Cotizacion_Consulta(objModeloVista.Codigo, objModeloVista.CodCliente, objModeloVista.Nombre_Completo).ToList();
                    }
                    else
                    {
                        ModeloVista = ModeloBD.SP_Cotizacion_Consulta(objModeloVista.Codigo, null, objModeloVista.Nombre_Completo).ToList();
                    }
                }
                else
                {
                    if (objModeloVista.CodCliente > 0)
                    {
                        ModeloVista = ModeloBD.SP_Cotizacion_Consulta(null, objModeloVista.CodCliente, objModeloVista.Nombre_Completo).ToList();
                    }
                    else
                    {
                        ModeloVista = ModeloBD.SP_Cotizacion_Consulta(null, null, objModeloVista.Nombre_Completo).ToList();
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
                    ModeloVista = new List<SP_Cotizacion_Consulta_Result>();
                }
                ViewBag.strResultadoOperacion = strResultado.Replace("'", "");
            }

            return View(ModeloVista);
        }
        [Autorizacion(Roles = CotizacionController.strIdPantallaMantenimiento)]
        public ActionResult Mantenimiento(string strCodProceso = ClsConstantes.strCodigoInsertar, int nCodigo = 0)
        {
            UtilityPortalEntities ModeloBD = new UtilityPortalEntities();
            CotizacionModel ModeloVista = new CotizacionModel();

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
                        ModeloVista.CotizacionCabeceraModel.Moneda = Session["Moneda"].ToString();

                        if (ModeloVista.CotizacionCabeceraModel.Moneda.Equals("D"))
                        {
                            Session["Moneda"] = "D";
                        }
                        break;
                    case ClsConstantes.strCodigoModificar:
                        ViewBag.strCodProceso = ClsConstantes.strCodigoModificar;
                        ModeloVista.CotizacionCabeceraModel = ModeloBD.SP_Cotizacion_Obtener(nCodigo).FirstOrDefault();
                        ModeloVista.CotizacionDetalleModel = ModeloBD.SP_CotizacionDetalle_Obtener(nCodigo).ToList();

                        if (ModeloVista.CotizacionCabeceraModel.Moneda.Equals("D"))
                        {
                            Session["Moneda"] = "D";
                        }
                        break;
                    case ClsConstantes.strCodigoVisualizar:
                        ViewBag.strCodProceso = ClsConstantes.strCodigoVisualizar;
                        ModeloVista.CotizacionCabeceraModel = ModeloBD.SP_Cotizacion_Obtener(nCodigo).FirstOrDefault();
                        ModeloVista.CotizacionDetalleModel = ModeloBD.SP_CotizacionDetalle_Obtener(nCodigo).ToList();

                        if (ModeloVista.CotizacionCabeceraModel.Moneda.Equals("D"))
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
                    ModeloVista = new CotizacionModel();
                }
                ViewBag.strResultadoOperacion = strResultado.Replace("'", "");

            }
            CargarClientesViewBag();

            return View(ModeloVista);
        }

        [HttpPost]
        public ActionResult Mantenimiento(CotizacionModel objModeloVista)
        {
            UtilityPortalEntities ModeloBD = new UtilityPortalEntities();
            SP_Validar_Usuario_Result DatosUsuario = (SP_Validar_Usuario_Result)this.Session["DatosUsuario"];


            string strResultado = "";

            try
            {
                ClsConstantes clsConstantes = new ClsConstantes();
                string ObjetoProceso = "";

                int nLinea = 1;

                foreach (SP_CotizacionDetalle_Obtener_Result Linea in objModeloVista.CotizacionDetalleModel)
                {
                    Linea.NumLinea = nLinea;
                    nLinea += 1;
                }

                if (!string.IsNullOrEmpty(Request["btnGuardar"]) || (!string.IsNullOrEmpty(Request["hddGuardar"]) && Request["hddGuardar"] == "Guardar"))
                {
                    switch (Request["CodProceso"].ToString())
                    {
                        case ClsConstantes.strCodigoInsertar:
                            objModeloVista.CotizacionCabeceraModel.UsuarioCrea = DatosUsuario.Codigo;
                            objModeloVista.CotizacionCabeceraModel.FechaCrea = DateTime.Now;
                            objModeloVista.CotizacionCabeceraModel.UsuarioModifica = DatosUsuario.Codigo;
                            objModeloVista.CotizacionCabeceraModel.FechaModifica = DateTime.Now;
                            objModeloVista.CotizacionCabeceraModel.Fecha = DateTime.Now;
                            ObjetoProceso = clsConstantes.RetornaXML<CotizacionModel>(objModeloVista);
                            objModeloVista.CotizacionCabeceraModel.Codigo = Convert.ToInt32(ModeloBD.SP_Cotizacion_Mantenimiento(ClsConstantes.strCodigoInsertar, ObjetoProceso).FirstOrDefault());
                            break;
                        case ClsConstantes.strCodigoModificar:
                            objModeloVista.CotizacionCabeceraModel.UsuarioModifica = DatosUsuario.Codigo;
                            objModeloVista.CotizacionCabeceraModel.FechaModifica = DateTime.Now;
                            ObjetoProceso = clsConstantes.RetornaXML<CotizacionModel>(objModeloVista);
                            objModeloVista.CotizacionCabeceraModel.Codigo = Convert.ToInt32(ModeloBD.SP_Cotizacion_Mantenimiento(ClsConstantes.strCodigoModificar, ObjetoProceso).FirstOrDefault());
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
                    objModeloVista = new CotizacionModel();
                }

                ViewBag.strResultadoOperacion = strResultado.Replace("'", "");
            }

            CargarClientesViewBag();

            return View(objModeloVista);
        }

        void CargarClientesViewBag()
        {
            UtilityPortalEntities ModeloBD = new UtilityPortalEntities();

            ViewBag.lstCliente = ModeloBD.SP_Cliente_Consulta(null, null, null).ToList();
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

        public ActionResult Calcular(List<List<string>> DetalleCotizacion, float nPorcentajeExoneracion, float nPorcentajeDescuento)
        {
            ClsConstantes clsConstantes = new ClsConstantes();
            float SubTotal = 0;
            float Descuento;
            float Iva;
            float Total;

            if (DetalleCotizacion != null)
            {
                foreach (List<string> LineaCotizacion in DetalleCotizacion)
                {
                    float Precio = 0;
                    float Cantidad = 0;
                    float.TryParse(LineaCotizacion[2], out Precio);
                    float.TryParse(LineaCotizacion[3], out Cantidad);
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
                    DetalleCotizacion = DetalleCotizacion
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

        public ActionResult CambiarMoneda(string strCodMoneda, List<List<string>> DetalleCotizacion)
        {
            UtilityPortalEntities ModeloBD = new UtilityPortalEntities();
            Session["Moneda"] = strCodMoneda;
            float Total = 0;

            if (DetalleCotizacion != null)
            {
                foreach (List<string> LineaCotizacion in DetalleCotizacion)
                {
                    SP_Producto_Obtener_Result objProducto = ModeloBD.SP_Producto_Obtener(int.Parse(LineaCotizacion[0])).FirstOrDefault();
                    if (objProducto != null)
                    {
                        if (Session["Moneda"].ToString().Equals("D"))
                        {
                            LineaCotizacion[2] = objProducto.PrecioDolar.ToString();
                        }
                        else
                        {
                            LineaCotizacion[2] = objProducto.Precio.ToString();
                        }
                    }
                    float Precio = 0;
                    float Cantidad = 0;
                    float.TryParse(LineaCotizacion[2], out Precio);
                    float.TryParse(LineaCotizacion[3], out Cantidad);
                    Total += (Precio * Cantidad);
                }
            }

            return new JsonResult()
            {
                Data = new
                {
                    Total = Total,
                    DetalleCotizacion = DetalleCotizacion
                }
            };

        }

        public async Task<JsonResult> EnviarCorreo(int nCodCotizacion)
        {
            Boolean lblEnvioCorreo;
            ClsConstantes clsConstantes = new ClsConstantes();
            ReporteModel clsReporte = new ReporteModel();



            ReportParameter ParametroReporte = new ReportParameter("Codigo", nCodCotizacion.ToString());
            DbParameter[] Parametrosds1 = new DbParameter[1];
            Parametrosds1[0] = new SqlParameter();
            Parametrosds1[0].ParameterName = "@Codigo";
            Parametrosds1[0].Value = nCodCotizacion;
            DbParameter[] Parametrosds2 = new DbParameter[1];
            Parametrosds2[0] = new SqlParameter();
            Parametrosds2[0].ParameterName = "@Codigo";
            Parametrosds2[0].Value = nCodCotizacion;

            try
            {

                ReportViewer viewer = new ReportViewer();
                viewer.LocalReport.Refresh();
                viewer.LocalReport.ReportPath = Server.MapPath("~/Reportes/Cotizador.rdl");
                viewer.LocalReport.SetParameters(ParametroReporte);

                string strScript = "SELECT COT.*, DET.*, CLI.Nombre + ' ' + CLI.APELLIDO1 + ' ' + CLI.APELLIDO2 AS NOMBRE_COMPLETO, CLI.Correo, CLI.Telefono1, PRO.Nombre AS NOMBRE_Producto, DET.CantidadProducto * DET.PrecioProducto AS Total_Linea FROM Cotizacion AS COT LEFT JOIN CotizacionDetalle AS DET ON COT.Codigo = DET.CodCotizacion LEFT JOIN Cliente AS CLI ON COT.CodCliente = CLI.Codigo LEFT JOIN Producto AS PRO ON DET.CodProducto = PRO.Codigo WHERE COT.CODIGO = @Codigo";
                DataTable Encabezado = clsReporte.ObtenerDatosReporte(strScript, Parametrosds1);
                ReportDataSource datasource1 = new ReportDataSource("DataSet1", Encabezado);

                strScript = "SELECT Moneda, SubTotal, Descuento, Iva, Total, UsuarioCrea FROM Cotizacion WHERE CODIGO = @Codigo";
                DataTable Detalle = clsReporte.ObtenerDatosReporte(strScript, Parametrosds2);
                ReportDataSource datasource2 = new ReportDataSource("DataSetTotal", Detalle);

                viewer.LocalReport.DataSources.Clear();
                viewer.LocalReport.DataSources.Add(datasource1);
                viewer.LocalReport.DataSources.Add(datasource2);
                byte[] Bytes = viewer.LocalReport.Render(format: "PDF", deviceInfo: "");

                string strNombreArchivo = Server.MapPath("~/Reportes") + "\\Cotizacion_" + nCodCotizacion + ".pdf";

                using (FileStream stream = new FileStream(strNombreArchivo, FileMode.Create))
                {
                    stream.Write(Bytes, 0, Bytes.Length);
                    stream.Close();
                }


                lblEnvioCorreo = await clsConstantes.EnviarCorreo("", Encabezado.Rows[0]["Correo"].ToString(), "Grupo Computacional Desarrollador GORO del este S.A. Cotizacion " + "#" + nCodCotizacion, "Un gusto en saludarle, adjunto encontrara la cotizacion solicitada", strNombreArchivo);
            }
            catch (Exception ex)
            {
                string aa = ex.Message;
                lblEnvioCorreo = false;
            }

            return Json(lblEnvioCorreo);
        }

        [HttpGet]
        public ActionResult DescargarCotizacion(int nCodCotizacion)
        {
            ClsConstantes clsConstantes = new ClsConstantes();
            ReporteModel clsReporte = new ReporteModel();



            ReportParameter ParametroReporte = new ReportParameter("Codigo", nCodCotizacion.ToString());
            DbParameter[] Parametrosds1 = new DbParameter[1];
            Parametrosds1[0] = new SqlParameter();
            Parametrosds1[0].ParameterName = "@Codigo";
            Parametrosds1[0].Value = nCodCotizacion;
            DbParameter[] Parametrosds2 = new DbParameter[1];
            Parametrosds2[0] = new SqlParameter();
            Parametrosds2[0].ParameterName = "@Codigo";
            Parametrosds2[0].Value = nCodCotizacion;


            ReportViewer viewer = new ReportViewer();
            viewer.LocalReport.Refresh();
            viewer.LocalReport.ReportPath = Server.MapPath("~/Reportes/Cotizador.rdl");
            viewer.LocalReport.SetParameters(ParametroReporte);

            string strScript = "SELECT COT.*, DET.*, CLI.Nombre + ' ' + CLI.APELLIDO1 + ' ' + CLI.APELLIDO2 AS NOMBRE_COMPLETO, CLI.Correo, CLI.Telefono1, PRO.Nombre AS NOMBRE_Producto, DET.CantidadProducto * DET.PrecioProducto AS Total_Linea FROM Cotizacion AS COT LEFT JOIN CotizacionDetalle AS DET ON COT.Codigo = DET.CodCotizacion LEFT JOIN Cliente AS CLI ON COT.CodCliente = CLI.Codigo LEFT JOIN Producto AS PRO ON DET.CodProducto = PRO.Codigo WHERE COT.CODIGO = @Codigo";
            DataTable Encabezado = clsReporte.ObtenerDatosReporte(strScript, Parametrosds1);
            ReportDataSource datasource1 = new ReportDataSource("DataSet1", Encabezado);

            strScript = "SELECT Moneda, SubTotal, Descuento, Iva, Total, UsuarioCrea FROM Cotizacion WHERE CODIGO = @Codigo";
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


            return File(bytes, "application/pdf", "Cotizacion_" + nCodCotizacion + ".pdf");
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