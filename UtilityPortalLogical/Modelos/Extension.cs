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
    
    public partial class Extension
    {
        public int Codigo { get; set; }
        public string Nombre { get; set; }
        public string Numero { get; set; }
        public string Estado { get; set; }
        public int CodPuesto { get; set; }
        public string UsuarioCrea { get; set; }
        public System.DateTime FechaCrea { get; set; }
        public string UsuarioModifica { get; set; }
        public System.DateTime FechaModifica { get; set; }
    
        public virtual Puesto Puesto { get; set; }
    }
}
