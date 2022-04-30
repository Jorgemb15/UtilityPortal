using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using UtilityPortalLogical.Logica;
using UtilityPortalLogical.Modelos;

namespace UtilityPortal.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index(string strMensaje = null)
        {
            if (!string.IsNullOrEmpty(strMensaje))
            {
                this.ViewBag.strResultadoOperacion = strMensaje;
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(LoginModel Modelo)
        {
            if (Modelo is null || string.IsNullOrEmpty(Modelo.StrUsuario) || string.IsNullOrEmpty(Modelo.StrClave))
            {
                this.ViewBag.strResultadoOperacion = "Por favor ingrese usuario y Clave";
            }
            else
            {

                SP_Validar_Usuario_Result objUsuario = new SP_Validar_Usuario_Result();
                string strResultado = "";
                Boolean lblBVerificado = false;

                try
                {
                    UtilityPortalEntities ModeloBD = new UtilityPortalEntities();
                    objUsuario = ModeloBD.SP_Validar_Usuario("S", Modelo.StrUsuario.ToUpper(), Modelo.StrClave, ClsConstantes.strIdPrograma).FirstOrDefault();

                    if (objUsuario != null)
                    {
                        lblBVerificado = true;
                        strResultado = "Bien";
                    }
                    else
                    {
                        strResultado = "Usuario o Clave Incorrectas";
                    }
                }
                catch (Exception Error)
                {
                    lblBVerificado = false;
                    strResultado = "Error : " + Error.Message;
                    if (Error.InnerException != null)
                    {
                        strResultado = "Alerta : " + Error.InnerException.Message;
                    }
                }
                finally
                {
                    if (lblBVerificado == true)
                    {
                        this.Session.Add("SesionIniciada", true);
                        this.Session.Add("DatosUsuario", objUsuario);
                        this.Session.Add("PrimerIngreso", true);
                        FormsAuthentication.Initialize();
                        FormsAuthentication.SetAuthCookie(objUsuario.Codigo, true);
                    }
                    else
                    {
                        ViewBag.strResultadoOperacion = strResultado.Replace("'", "");
                    }
                }

                if (lblBVerificado == true)
                {
                    return RedirectToAction("Index", "Menu");
                }
            }
            return View();
        }

        public ActionResult CerrarSesion()
        {
            FormsAuthentication.SignOut();
            Session.Clear();
            return RedirectToAction("Index");
        }

        public ActionResult SesionExpirada()
        {
            FormsAuthentication.SignOut();
            Session.Clear();
            return RedirectToAction("Index", new { strMensaje = "Sesion Expirada por favor ingrese nuevamente" });
        }

        public ActionResult AccesoDenegado()
        {
            return View();
        }


        public ActionResult Error()
        {
            return View();
        }
    }
}