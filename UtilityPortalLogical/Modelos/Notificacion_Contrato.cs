//------------------------------------------------------------------------------
// <auto-generated>
//    Este código se generó a partir de una plantilla.
//
//    Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//    Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace UtilityPortalLogical.Modelos
{
    using System;
    using System.Collections.Generic;
    
    public partial class Notificacion_Contrato
    {
        public int Codigo { get; set; }
        public int CodContrato { get; set; }
        public string Titulo { get; set; }
        public string Detalle { get; set; }
        public System.DateTime Fecha { get; set; }
        public string CorreoEnviado { get; set; }
    
        public virtual Contrato Contrato { get; set; }
    }
}