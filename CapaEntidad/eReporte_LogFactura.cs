using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class eReporte_LogFactura
    {
        public int Id_Emp { get; set; }
        public int Id_Cd { get; set; }
        public int Id_Fac { get; set; }

        public string Fac_FolioFiscal { get; set; }
        public int Id_Cte { get; set; }
        public string FechaIni { get; set; }
        public string FechaFin { get; set; }

        public string Usuario { get; set; }
        public string Actividad { get; set; }
        public string Fecha { get; set; }
        public int Id_Relacion { get; set; }

        public string Factura { get; set; }
        public string Cancelacion { get; set; }
        public string RelacionAlmacen { get; set; }
        public string ConfirmadoAlmacen { get; set; }
        public string FacturaEmbarcada { get; set; }
        public string FacturaRegresoAlmacen { get; set; }
        public string RelacionadaCobranza { get; set; }
        public string ConfirmadaCobranza { get; set; }
        public string EnviadaRevision { get; set; }
        public string ConfirmadaRevision { get; set; }
        public string EnviadaCobro { get; set; }
        public string FEmbarcadaV2 { get; set; }
        public string FacturaEntregada { get; set; }
        public string Cliente { get; set; }

        public string UsuarioFactura { get; set; }
        public string FechaFactura { get; set; }

        public string ActFactura { get; set; }
        public string DocFactura { get; set; }
        public string UsuarioCancela { get; set; }
        public string FechaCancela { get; set; }
        public string ActCancela { get; set; }
        public string DocCancela { get; set; }

        public string UsuarioRelacionAlmacen { get; set; }
        public string FechaRelacionAlmacen { get; set; }
        public string ActRelacionAlmacen { get; set; }
        public string DocRelacionAlmacen { get; set; }

        public string UsuarioConfirmadaAlmacen { get; set; }
        public string FechaConfirmadaAlmacen { get; set; }
        public string ActConfirmadaAlmacen { get; set; }
        public string DocConfirmadaAlmacen { get; set; }

        public string UsuarioEmbarque { get; set; }
        public string FechaEmbarque { get; set; }
        public string ActEmbarque { get; set; }
        public string DocEmbarque { get; set; }

        public string UsuarioRegresoAlmacen { get; set; }
        public string FechaRegresoAlmacen { get; set; }
        public string ActRegresoAlmacen { get; set; }
        public string DocRegresoAlmacen { get; set; }

        public string UsuarioRelacionCobranza { get; set; }
        public string FechaRelacionCobranza { get; set; }
        public string ActRelacionCobranza { get; set; }
        public string DocRelacionCobranza { get; set; }
        public string UsuarioEnviadaRevision { get; set; }
        public string FechaEnviadaRevision { get; set; }
        public string ActEnviadaRevison { get; set; }
        public string DocEnviadaRevision { get; set; }

        public string UsuarioConfirmadaRevision { get; set; }
        public string FechaConfirmadaRevision { get; set; }
        public string ActConfirmadaRevision { get; set; }
        public string DocConfirmadaRevision { get; set; }

        public string UsuarioEnviadaCobro { get; set; }
        public string FechaEnviadaCobro { get; set; }
        public string ActEnviadaCobro { get; set; }
        public string DocEnviadaCobro { get; set; }

        public string UsuarioEmbarqueV2 { get; set; }
        public string FechaEmbarqueV2 { get; set; }
        public string ActEmbarqueV2 { get; set; }
        public string DocEmbarqueV2 { get; set; }

        public string UsuarioEntregada { get; set; }
        public string FechaEntregada { get; set; }
        public string ActEntregada { get; set; }
        public string DocEntregada { get; set; }

        public string UsuarioConfirmadaCobranza { get; set; }
        public string FechaConfirmadaCobranza { get; set; }
        public string ActConfirmadaCobranza { get; set; }
        public string DocConfirmadaCobranza { get; set; }



    }
}