using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using UtilityPortalLogical.Modelos;

namespace UtilityPortalLogical.Logica
{
    public class ClsConstantes
    {
        public const string strCodigoInsertar = "I";
        public const string strCodigoModificar = "M";
        public const string strCodigoEliminar = "E";
        public const string strCodigoVisualizar = "V";
        public const string strCodigoConsulta = "C";
        public const string strCodigoLimpiar = "L";
        public const string strEstadoActivo = "A";
        public const string strUsuarioCliente = "C";
        public const string strUsuarioEmpleado = "E";
        public const string strUsuarioAmbos = "A";
        public const string strTransaccionRetiro = "R";
        public const string strTransaccionDeposito = "D";
        public const string strTransaccionTransferencia = "T";
        public float nPorcentajeIVA = float.Parse(ConfigurationManager.AppSettings["PorcentajeIVA"]);
        public static readonly string strIdPrograma = ConfigurationManager.AppSettings["IdPrograma"];

        public string RetornaXML<T>(object pObjeto)
        {
            XmlSerializer ser = new XmlSerializer(typeof(T));
            MemoryStream m = new MemoryStream();
            ser.Serialize(m, pObjeto);
            m.Position = 0;
            string xml = new StreamReader(m).ReadToEnd();
            return xml;
        }


        public async Task<Boolean> EnviarCorreo(string strNombreReceptor, string strCorreoReceptor, string strTitulo, string strMensaje, string strRutaArchivo = null)
        {
            await Task.Delay(1);
            UtilityPortalEntities ModeloBD = new UtilityPortalEntities();

            Boolean lblEnviado = false;
            string strCorreoOrigen = "";
            string strClaveCorreoOrigen = "";
            string strServidor = "";
            string strPuerto = "";
            string strSeguridad = "";

            try
            {
                List<SP_Configuracion_Consulta_Result> lstConfiguracion = ModeloBD.SP_Configuracion_Consulta("Correo", null).ToList();

                foreach (SP_Configuracion_Consulta_Result PosicionLista in lstConfiguracion)
                {
                    switch (PosicionLista.Codigo)
                    {
                        case "Correo":
                            strCorreoOrigen = PosicionLista.Valor;
                            break;
                        case "Clave":
                            strClaveCorreoOrigen = PosicionLista.Valor;
                            break;
                        case "Host":
                            strServidor = PosicionLista.Valor;
                            break;
                        case "Puerto":
                            strPuerto = PosicionLista.Valor;
                            break;
                        case "EnableSsl":
                            strSeguridad = PosicionLista.Valor;
                            break;
                    }
                }

                var body = "<p>Este es un correo automático de {0} ({1})</p><p>{2}</p><p>{3}</p>";
                var message = new MailMessage();
                message.To.Add(new MailAddress(strCorreoReceptor));
                message.From = new MailAddress(strCorreoOrigen);
                message.Subject = strTitulo;
                message.Body = string.Format(body, "Grupo Computacional Desarrollador GORO del este S.A.", strCorreoOrigen, strMensaje, "Gracias por confiar en Grupo Computacional Desarrollador GORO del este S.A.");
                message.IsBodyHtml = true;

                if (!string.IsNullOrEmpty(strRutaArchivo))
                {
                    message.Attachments.Add(new Attachment(strRutaArchivo));
                }

                using (var smtp = new SmtpClient())
                {
                    var credential = new NetworkCredential
                    {
                        UserName = strCorreoOrigen,
                        Password = strClaveCorreoOrigen
                    };
                    smtp.EnableSsl = Convert.ToBoolean(strSeguridad);
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = credential;
                    smtp.Host = strServidor;
                    smtp.Port = Convert.ToInt32(strPuerto);
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.Send(message);

                }
                lblEnviado = true;
            }
            catch (Exception error)
            {
                lblEnviado = false;
            }
            return lblEnviado;
        }

        //public async Task<Boolean> EnviarCorreo(string strNombreReceptor, string strCorreoReceptor, string strTitulo, string strMensaje, string strRutaArchivo = null)
        //{
        //    await Task.Delay(1);
        //    PortalReportsEntities ModeloBD = new PortalReportsEntities();

        //    Boolean lblEnviado = false;
        //    string strCorreoOrigen = "";
        //    string strClaveCorreoOrigen = "";
        //    string strServidor = "";
        //    string strPuerto = "";
        //    string strSeguridad = "";

        //    try
        //    {
        //        List<SP_Configuracion_Consulta_Result> lstConfiguracion = ModeloBD.SP_Configuracion_Consulta("Correo", null).ToList();

        //        foreach (SP_Configuracion_Consulta_Result PosicionLista in lstConfiguracion)
        //        {
        //            switch (PosicionLista.Codigo)
        //            {
        //                case "Correo":
        //                    strCorreoOrigen = PosicionLista.Valor;
        //                    break;
        //                case "Clave":
        //                    strClaveCorreoOrigen = PosicionLista.Valor;
        //                    break;
        //                case "Host":
        //                    strServidor = PosicionLista.Valor;
        //                    break;
        //                case "Puerto":
        //                    strPuerto = PosicionLista.Valor;
        //                    break;
        //                case "EnableSsl":
        //                    strSeguridad = PosicionLista.Valor;
        //                    break;
        //            }
        //        }

        //        var body = "<p>Este es un correo automático de {0} ({1})</p><p>{2}</p><p>{3}</p>";
        //        var message = new MailMessage();
        //        message.To.Add(new MailAddress(strCorreoReceptor));
        //        message.From = new MailAddress(strCorreoOrigen);
        //        message.Subject = strTitulo;
        //        message.Body = string.Format(body, "Grupo Computacional Desarrollador GORO del este S.A.", strCorreoOrigen, strMensaje, "Gracias por confiar en Grupo Computacional Desarrollador GORO del este S.A.");
        //        message.IsBodyHtml = true;

        //        if (!string.IsNullOrEmpty(strRutaArchivo))
        //        {
        //            message.Attachments.Add(new Attachment(strRutaArchivo));
        //        }

        //        using (var smtp = new SmtpClient())
        //        {
        //            var credential = new NetworkCredential
        //            {
        //                UserName = strCorreoOrigen,
        //                Password = strClaveCorreoOrigen
        //            };
        //            smtp.EnableSsl = Convert.ToBoolean(strSeguridad);
        //            smtp.UseDefaultCredentials = false;
        //            smtp.Credentials = credential;
        //            smtp.Host = strServidor;
        //            smtp.Port = Convert.ToInt32(strPuerto);
        //            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
        //            smtp.Send(message);

        //        }
        //        lblEnviado = true;
        //    }
        //    catch (Exception error)
        //    {
        //        lblEnviado = false;
        //    }
        //    return lblEnviado;
        //}

    }
}
