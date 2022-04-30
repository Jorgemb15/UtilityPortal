using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UtilityPortalLogical.Modelos
{
    public class CotizacionModel
    {
        public CotizacionModel()
        {
            CotizacionCabeceraModel = new SP_Cotizacion_Obtener_Result();
            CotizacionDetalleModel = new List<SP_CotizacionDetalle_Obtener_Result>();
        }

        public SP_Cotizacion_Obtener_Result CotizacionCabeceraModel { get; set; }
        public List<SP_CotizacionDetalle_Obtener_Result> CotizacionDetalleModel { get; set; }
    }
}