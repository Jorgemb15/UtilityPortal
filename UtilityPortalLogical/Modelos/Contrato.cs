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
    
    public partial class Contrato
    {
        public Contrato()
        {
            this.ContratoDetalle = new HashSet<ContratoDetalle>();
            this.Notificacion_Contrato = new HashSet<Notificacion_Contrato>();
        }
    
        public int Codigo { get; set; }
        public string Descripcion { get; set; }
        public System.DateTime Fecha { get; set; }
        public string Detalle { get; set; }
        public int Duracion { get; set; }
        public System.DateTime FechaInicio { get; set; }
        public System.DateTime FechaFin { get; set; }
        public System.DateTime FechaPago { get; set; }
        public string Frecuencia { get; set; }
        public int Tipo { get; set; }
        public string GeneraPagoProveedor { get; set; }
        public string NotificaFinContratoMes { get; set; }
        public string NotificaFinContratoSemana { get; set; }
        public string NotificaPagoProveedor { get; set; }
        public int CodCliente { get; set; }
        public Nullable<int> CodProveedor { get; set; }
        public Nullable<System.DateTime> FechaPagoProveedor { get; set; }
        public string FrecuenciaPagoProveedor { get; set; }
        public string Moneda { get; set; }
        public int PorcentajeDescuento { get; set; }
        public double SubTotal { get; set; }
        public double Descuento { get; set; }
        public double Iva { get; set; }
        public double Total { get; set; }
        public string UsuarioCrea { get; set; }
        public System.DateTime FechaCrea { get; set; }
        public string UsuarioModifica { get; set; }
        public System.DateTime FechaModifica { get; set; }
    
        public virtual Cliente Cliente { get; set; }
        public virtual ICollection<ContratoDetalle> ContratoDetalle { get; set; }
        public virtual Proveedor Proveedor { get; set; }
        public virtual Tipo_Contrato Tipo_Contrato { get; set; }
        public virtual ICollection<Notificacion_Contrato> Notificacion_Contrato { get; set; }
    }
}