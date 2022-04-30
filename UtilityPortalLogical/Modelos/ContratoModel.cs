using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace UtilityPortalLogical.Modelos
{
    public class ContratoModel
    {
        public ContratoModel()
        {
            ContratoCabeceraModel = new SP_Contrato_Obtener_Result();
            ContratoDetalleModel = new List<SP_ContratoDetalle_Obtener_Result>();
        }

        public SP_Contrato_Obtener_Result ContratoCabeceraModel { get; set; }
        public List<SP_ContratoDetalle_Obtener_Result> ContratoDetalleModel { get; set; }
    }
}