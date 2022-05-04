﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Objects;
    using System.Data.Objects.DataClasses;
    using System.Linq;
    
    public partial class UtilityPortalEntities : DbContext
    {
        public UtilityPortalEntities()
            : base("name=UtilityPortalEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<Canton> Canton { get; set; }
        public DbSet<Cliente> Cliente { get; set; }
        public DbSet<Configuracion> Configuracion { get; set; }
        public DbSet<Contrato> Contrato { get; set; }
        public DbSet<ContratoDetalle> ContratoDetalle { get; set; }
        public DbSet<Cotizacion> Cotizacion { get; set; }
        public DbSet<CotizacionDetalle> CotizacionDetalle { get; set; }
        public DbSet<Distrito> Distrito { get; set; }
        public DbSet<Extension> Extension { get; set; }
        public DbSet<Notificacion_Contrato> Notificacion_Contrato { get; set; }
        public DbSet<Perfil> Perfil { get; set; }
        public DbSet<Permiso> Permiso { get; set; }
        public DbSet<Permiso_Accion> Permiso_Accion { get; set; }
        public DbSet<Producto> Producto { get; set; }
        public DbSet<Proveedor> Proveedor { get; set; }
        public DbSet<Provincia> Provincia { get; set; }
        public DbSet<Puesto> Puesto { get; set; }
        public DbSet<Tipo_Contrato> Tipo_Contrato { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<SampleTable> SampleTable { get; set; }
    
        public virtual ObjectResult<SP_Canton_Consulta_Result> SP_Canton_Consulta(Nullable<int> pCodigo, string pNombre, Nullable<int> pCodProvincia)
        {
            var pCodigoParameter = pCodigo.HasValue ?
                new ObjectParameter("pCodigo", pCodigo) :
                new ObjectParameter("pCodigo", typeof(int));
    
            var pNombreParameter = pNombre != null ?
                new ObjectParameter("pNombre", pNombre) :
                new ObjectParameter("pNombre", typeof(string));
    
            var pCodProvinciaParameter = pCodProvincia.HasValue ?
                new ObjectParameter("pCodProvincia", pCodProvincia) :
                new ObjectParameter("pCodProvincia", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_Canton_Consulta_Result>("SP_Canton_Consulta", pCodigoParameter, pNombreParameter, pCodProvinciaParameter);
        }
    
        public virtual ObjectResult<SP_Cliente_Consulta_Result> SP_Cliente_Consulta(Nullable<int> pCodigo, string pNombre, string pCedula)
        {
            var pCodigoParameter = pCodigo.HasValue ?
                new ObjectParameter("pCodigo", pCodigo) :
                new ObjectParameter("pCodigo", typeof(int));
    
            var pNombreParameter = pNombre != null ?
                new ObjectParameter("pNombre", pNombre) :
                new ObjectParameter("pNombre", typeof(string));
    
            var pCedulaParameter = pCedula != null ?
                new ObjectParameter("pCedula", pCedula) :
                new ObjectParameter("pCedula", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_Cliente_Consulta_Result>("SP_Cliente_Consulta", pCodigoParameter, pNombreParameter, pCedulaParameter);
        }
    
        public virtual ObjectResult<Nullable<int>> SP_Cliente_Mantenimiento(string pProceso, string pParametros)
        {
            var pProcesoParameter = pProceso != null ?
                new ObjectParameter("pProceso", pProceso) :
                new ObjectParameter("pProceso", typeof(string));
    
            var pParametrosParameter = pParametros != null ?
                new ObjectParameter("pParametros", pParametros) :
                new ObjectParameter("pParametros", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("SP_Cliente_Mantenimiento", pProcesoParameter, pParametrosParameter);
        }
    
        public virtual ObjectResult<SP_Cliente_Obtener_Result> SP_Cliente_Obtener(Nullable<int> pCodigo)
        {
            var pCodigoParameter = pCodigo.HasValue ?
                new ObjectParameter("pCodigo", pCodigo) :
                new ObjectParameter("pCodigo", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_Cliente_Obtener_Result>("SP_Cliente_Obtener", pCodigoParameter);
        }
    
        public virtual ObjectResult<SP_Configuracion_Consulta_Result> SP_Configuracion_Consulta(string pGrupo, string pCodigo)
        {
            var pGrupoParameter = pGrupo != null ?
                new ObjectParameter("pGrupo", pGrupo) :
                new ObjectParameter("pGrupo", typeof(string));
    
            var pCodigoParameter = pCodigo != null ?
                new ObjectParameter("pCodigo", pCodigo) :
                new ObjectParameter("pCodigo", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_Configuracion_Consulta_Result>("SP_Configuracion_Consulta", pGrupoParameter, pCodigoParameter);
        }
    
        public virtual ObjectResult<SP_Contrato_Consulta_Result> SP_Contrato_Consulta(Nullable<int> pCodigo, Nullable<int> pCodCliente, string pNomCliente)
        {
            var pCodigoParameter = pCodigo.HasValue ?
                new ObjectParameter("pCodigo", pCodigo) :
                new ObjectParameter("pCodigo", typeof(int));
    
            var pCodClienteParameter = pCodCliente.HasValue ?
                new ObjectParameter("pCodCliente", pCodCliente) :
                new ObjectParameter("pCodCliente", typeof(int));
    
            var pNomClienteParameter = pNomCliente != null ?
                new ObjectParameter("pNomCliente", pNomCliente) :
                new ObjectParameter("pNomCliente", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_Contrato_Consulta_Result>("SP_Contrato_Consulta", pCodigoParameter, pCodClienteParameter, pNomClienteParameter);
        }
    
        public virtual ObjectResult<Nullable<int>> SP_Contrato_Mantenimiento(string pProceso, string pParametros)
        {
            var pProcesoParameter = pProceso != null ?
                new ObjectParameter("pProceso", pProceso) :
                new ObjectParameter("pProceso", typeof(string));
    
            var pParametrosParameter = pParametros != null ?
                new ObjectParameter("pParametros", pParametros) :
                new ObjectParameter("pParametros", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("SP_Contrato_Mantenimiento", pProcesoParameter, pParametrosParameter);
        }
    
        public virtual ObjectResult<SP_Contrato_Obtener_Result> SP_Contrato_Obtener(Nullable<int> pCodigo)
        {
            var pCodigoParameter = pCodigo.HasValue ?
                new ObjectParameter("pCodigo", pCodigo) :
                new ObjectParameter("pCodigo", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_Contrato_Obtener_Result>("SP_Contrato_Obtener", pCodigoParameter);
        }
    
        public virtual ObjectResult<SP_ContratoDetalle_Obtener_Result> SP_ContratoDetalle_Obtener(Nullable<int> pCodigo)
        {
            var pCodigoParameter = pCodigo.HasValue ?
                new ObjectParameter("pCodigo", pCodigo) :
                new ObjectParameter("pCodigo", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_ContratoDetalle_Obtener_Result>("SP_ContratoDetalle_Obtener", pCodigoParameter);
        }
    
        public virtual ObjectResult<SP_Cotizacion_Consulta_Result> SP_Cotizacion_Consulta(Nullable<int> pCodigo, Nullable<int> pCodCliente, string pNomCliente)
        {
            var pCodigoParameter = pCodigo.HasValue ?
                new ObjectParameter("pCodigo", pCodigo) :
                new ObjectParameter("pCodigo", typeof(int));
    
            var pCodClienteParameter = pCodCliente.HasValue ?
                new ObjectParameter("pCodCliente", pCodCliente) :
                new ObjectParameter("pCodCliente", typeof(int));
    
            var pNomClienteParameter = pNomCliente != null ?
                new ObjectParameter("pNomCliente", pNomCliente) :
                new ObjectParameter("pNomCliente", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_Cotizacion_Consulta_Result>("SP_Cotizacion_Consulta", pCodigoParameter, pCodClienteParameter, pNomClienteParameter);
        }
    
        public virtual ObjectResult<Nullable<int>> SP_Cotizacion_Mantenimiento(string pProceso, string pParametros)
        {
            var pProcesoParameter = pProceso != null ?
                new ObjectParameter("pProceso", pProceso) :
                new ObjectParameter("pProceso", typeof(string));
    
            var pParametrosParameter = pParametros != null ?
                new ObjectParameter("pParametros", pParametros) :
                new ObjectParameter("pParametros", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("SP_Cotizacion_Mantenimiento", pProcesoParameter, pParametrosParameter);
        }
    
        public virtual ObjectResult<SP_Cotizacion_Obtener_Result> SP_Cotizacion_Obtener(Nullable<int> pCodigo)
        {
            var pCodigoParameter = pCodigo.HasValue ?
                new ObjectParameter("pCodigo", pCodigo) :
                new ObjectParameter("pCodigo", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_Cotizacion_Obtener_Result>("SP_Cotizacion_Obtener", pCodigoParameter);
        }
    
        public virtual ObjectResult<SP_CotizacionDetalle_Obtener_Result> SP_CotizacionDetalle_Obtener(Nullable<int> pCodigo)
        {
            var pCodigoParameter = pCodigo.HasValue ?
                new ObjectParameter("pCodigo", pCodigo) :
                new ObjectParameter("pCodigo", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_CotizacionDetalle_Obtener_Result>("SP_CotizacionDetalle_Obtener", pCodigoParameter);
        }
    
        public virtual ObjectResult<SP_Distrito_Consulta_Result> SP_Distrito_Consulta(Nullable<int> pCodigo, string pNombre, Nullable<int> pCodCanton)
        {
            var pCodigoParameter = pCodigo.HasValue ?
                new ObjectParameter("pCodigo", pCodigo) :
                new ObjectParameter("pCodigo", typeof(int));
    
            var pNombreParameter = pNombre != null ?
                new ObjectParameter("pNombre", pNombre) :
                new ObjectParameter("pNombre", typeof(string));
    
            var pCodCantonParameter = pCodCanton.HasValue ?
                new ObjectParameter("pCodCanton", pCodCanton) :
                new ObjectParameter("pCodCanton", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_Distrito_Consulta_Result>("SP_Distrito_Consulta", pCodigoParameter, pNombreParameter, pCodCantonParameter);
        }
    
        public virtual ObjectResult<SP_Extension_Consulta_Result> SP_Extension_Consulta(Nullable<int> pCodigo, string pNombre, string pNumero)
        {
            var pCodigoParameter = pCodigo.HasValue ?
                new ObjectParameter("pCodigo", pCodigo) :
                new ObjectParameter("pCodigo", typeof(int));
    
            var pNombreParameter = pNombre != null ?
                new ObjectParameter("pNombre", pNombre) :
                new ObjectParameter("pNombre", typeof(string));
    
            var pNumeroParameter = pNumero != null ?
                new ObjectParameter("pNumero", pNumero) :
                new ObjectParameter("pNumero", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_Extension_Consulta_Result>("SP_Extension_Consulta", pCodigoParameter, pNombreParameter, pNumeroParameter);
        }
    
        public virtual ObjectResult<SP_Notificacion_Contrato_Correo_Consulta_Result> SP_Notificacion_Contrato_Correo_Consulta()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_Notificacion_Contrato_Correo_Consulta_Result>("SP_Notificacion_Contrato_Correo_Consulta");
        }
    
        public virtual ObjectResult<SP_Notificacion_Contrato_Dashboard_Consulta_Result> SP_Notificacion_Contrato_Dashboard_Consulta()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_Notificacion_Contrato_Dashboard_Consulta_Result>("SP_Notificacion_Contrato_Dashboard_Consulta");
        }
    
        public virtual ObjectResult<Nullable<int>> SP_Notificacion_Contrato_Mantenimiento(string pProceso, string pParametros)
        {
            var pProcesoParameter = pProceso != null ?
                new ObjectParameter("pProceso", pProceso) :
                new ObjectParameter("pProceso", typeof(string));
    
            var pParametrosParameter = pParametros != null ?
                new ObjectParameter("pParametros", pParametros) :
                new ObjectParameter("pParametros", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("SP_Notificacion_Contrato_Mantenimiento", pProcesoParameter, pParametrosParameter);
        }
    
        public virtual ObjectResult<SP_Obtener_Permisos_Usuario_Result> SP_Obtener_Permisos_Usuario(string pUsuario)
        {
            var pUsuarioParameter = pUsuario != null ?
                new ObjectParameter("pUsuario", pUsuario) :
                new ObjectParameter("pUsuario", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_Obtener_Permisos_Usuario_Result>("SP_Obtener_Permisos_Usuario", pUsuarioParameter);
        }
    
        public virtual ObjectResult<SP_Producto_Consulta_Result> SP_Producto_Consulta(Nullable<int> pCodigo, string pNombre)
        {
            var pCodigoParameter = pCodigo.HasValue ?
                new ObjectParameter("pCodigo", pCodigo) :
                new ObjectParameter("pCodigo", typeof(int));
    
            var pNombreParameter = pNombre != null ?
                new ObjectParameter("pNombre", pNombre) :
                new ObjectParameter("pNombre", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_Producto_Consulta_Result>("SP_Producto_Consulta", pCodigoParameter, pNombreParameter);
        }
    
        public virtual ObjectResult<Nullable<int>> SP_Producto_Mantenimiento(string pProceso, string pParametros)
        {
            var pProcesoParameter = pProceso != null ?
                new ObjectParameter("pProceso", pProceso) :
                new ObjectParameter("pProceso", typeof(string));
    
            var pParametrosParameter = pParametros != null ?
                new ObjectParameter("pParametros", pParametros) :
                new ObjectParameter("pParametros", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("SP_Producto_Mantenimiento", pProcesoParameter, pParametrosParameter);
        }
    
        public virtual ObjectResult<SP_Producto_Obtener_Result> SP_Producto_Obtener(Nullable<int> pCodigo)
        {
            var pCodigoParameter = pCodigo.HasValue ?
                new ObjectParameter("pCodigo", pCodigo) :
                new ObjectParameter("pCodigo", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_Producto_Obtener_Result>("SP_Producto_Obtener", pCodigoParameter);
        }
    
        public virtual ObjectResult<SP_Proveedor_Consulta_Result> SP_Proveedor_Consulta(Nullable<int> pCodigo, string pNombre, string pCedula)
        {
            var pCodigoParameter = pCodigo.HasValue ?
                new ObjectParameter("pCodigo", pCodigo) :
                new ObjectParameter("pCodigo", typeof(int));
    
            var pNombreParameter = pNombre != null ?
                new ObjectParameter("pNombre", pNombre) :
                new ObjectParameter("pNombre", typeof(string));
    
            var pCedulaParameter = pCedula != null ?
                new ObjectParameter("pCedula", pCedula) :
                new ObjectParameter("pCedula", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_Proveedor_Consulta_Result>("SP_Proveedor_Consulta", pCodigoParameter, pNombreParameter, pCedulaParameter);
        }
    
        public virtual ObjectResult<Nullable<int>> SP_Proveedor_Mantenimiento(string pProceso, string pParametros)
        {
            var pProcesoParameter = pProceso != null ?
                new ObjectParameter("pProceso", pProceso) :
                new ObjectParameter("pProceso", typeof(string));
    
            var pParametrosParameter = pParametros != null ?
                new ObjectParameter("pParametros", pParametros) :
                new ObjectParameter("pParametros", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("SP_Proveedor_Mantenimiento", pProcesoParameter, pParametrosParameter);
        }
    
        public virtual ObjectResult<SP_Proveedor_Obtener_Result> SP_Proveedor_Obtener(Nullable<int> pCodigo)
        {
            var pCodigoParameter = pCodigo.HasValue ?
                new ObjectParameter("pCodigo", pCodigo) :
                new ObjectParameter("pCodigo", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_Proveedor_Obtener_Result>("SP_Proveedor_Obtener", pCodigoParameter);
        }
    
        public virtual ObjectResult<SP_Provincia_Consulta_Result> SP_Provincia_Consulta(Nullable<int> pCodigo, string pNombre)
        {
            var pCodigoParameter = pCodigo.HasValue ?
                new ObjectParameter("pCodigo", pCodigo) :
                new ObjectParameter("pCodigo", typeof(int));
    
            var pNombreParameter = pNombre != null ?
                new ObjectParameter("pNombre", pNombre) :
                new ObjectParameter("pNombre", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_Provincia_Consulta_Result>("SP_Provincia_Consulta", pCodigoParameter, pNombreParameter);
        }
    
        public virtual ObjectResult<SP_Puesto_Consulta_Result> SP_Puesto_Consulta(Nullable<int> pCodigo, string pNombre)
        {
            var pCodigoParameter = pCodigo.HasValue ?
                new ObjectParameter("pCodigo", pCodigo) :
                new ObjectParameter("pCodigo", typeof(int));
    
            var pNombreParameter = pNombre != null ?
                new ObjectParameter("pNombre", pNombre) :
                new ObjectParameter("pNombre", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_Puesto_Consulta_Result>("SP_Puesto_Consulta", pCodigoParameter, pNombreParameter);
        }
    
        public virtual ObjectResult<SP_Tipo_Contrato_Consulta_Result> SP_Tipo_Contrato_Consulta(Nullable<int> pCodigo, string pDescripcion)
        {
            var pCodigoParameter = pCodigo.HasValue ?
                new ObjectParameter("pCodigo", pCodigo) :
                new ObjectParameter("pCodigo", typeof(int));
    
            var pDescripcionParameter = pDescripcion != null ?
                new ObjectParameter("pDescripcion", pDescripcion) :
                new ObjectParameter("pDescripcion", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_Tipo_Contrato_Consulta_Result>("SP_Tipo_Contrato_Consulta", pCodigoParameter, pDescripcionParameter);
        }
    
        public virtual ObjectResult<SP_Ubicacion_Obtener_Result> SP_Ubicacion_Obtener(Nullable<int> pCodDistrito)
        {
            var pCodDistritoParameter = pCodDistrito.HasValue ?
                new ObjectParameter("pCodDistrito", pCodDistrito) :
                new ObjectParameter("pCodDistrito", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_Ubicacion_Obtener_Result>("SP_Ubicacion_Obtener", pCodDistritoParameter);
        }
    
        public virtual ObjectResult<SP_Validar_Usuario_Result> SP_Validar_Usuario(string pTipoAutenticacion, string pUsuario, string pClave, string pIdPrograma)
        {
            var pTipoAutenticacionParameter = pTipoAutenticacion != null ?
                new ObjectParameter("pTipoAutenticacion", pTipoAutenticacion) :
                new ObjectParameter("pTipoAutenticacion", typeof(string));
    
            var pUsuarioParameter = pUsuario != null ?
                new ObjectParameter("pUsuario", pUsuario) :
                new ObjectParameter("pUsuario", typeof(string));
    
            var pClaveParameter = pClave != null ?
                new ObjectParameter("pClave", pClave) :
                new ObjectParameter("pClave", typeof(string));
    
            var pIdProgramaParameter = pIdPrograma != null ?
                new ObjectParameter("pIdPrograma", pIdPrograma) :
                new ObjectParameter("pIdPrograma", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_Validar_Usuario_Result>("SP_Validar_Usuario", pTipoAutenticacionParameter, pUsuarioParameter, pClaveParameter, pIdProgramaParameter);
        }
    
        public virtual ObjectResult<SP_Extension_Obtener_Result> SP_Extension_Obtener(Nullable<int> pCodigo)
        {
            var pCodigoParameter = pCodigo.HasValue ?
                new ObjectParameter("pCodigo", pCodigo) :
                new ObjectParameter("pCodigo", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_Extension_Obtener_Result>("SP_Extension_Obtener", pCodigoParameter);
        }
    
        public virtual ObjectResult<SP_Puesto_Obtener_Result> SP_Puesto_Obtener(Nullable<int> pCodigo)
        {
            var pCodigoParameter = pCodigo.HasValue ?
                new ObjectParameter("pCodigo", pCodigo) :
                new ObjectParameter("pCodigo", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_Puesto_Obtener_Result>("SP_Puesto_Obtener", pCodigoParameter);
        }
    
        public virtual ObjectResult<Nullable<int>> SP_Puesto_Mantenimiento(string pProceso, string pParametros)
        {
            var pProcesoParameter = pProceso != null ?
                new ObjectParameter("pProceso", pProceso) :
                new ObjectParameter("pProceso", typeof(string));
    
            var pParametrosParameter = pParametros != null ?
                new ObjectParameter("pParametros", pParametros) :
                new ObjectParameter("pParametros", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("SP_Puesto_Mantenimiento", pProcesoParameter, pParametrosParameter);
        }
    
        public virtual ObjectResult<Nullable<int>> SP_Extension_Mantenimiento(string pProceso, string pParametros)
        {
            var pProcesoParameter = pProceso != null ?
                new ObjectParameter("pProceso", pProceso) :
                new ObjectParameter("pProceso", typeof(string));
    
            var pParametrosParameter = pParametros != null ?
                new ObjectParameter("pParametros", pParametros) :
                new ObjectParameter("pParametros", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("SP_Extension_Mantenimiento", pProcesoParameter, pParametrosParameter);
        }
    }
}