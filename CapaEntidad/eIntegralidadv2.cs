using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class eIntegralidadv2
    {
        public int Id_Cd { get; set; }
        public int Id_Zona { get; set; }
        public int Id_Rik { get; set; }
        public int Id_Op { get; set; }
        public int Id_Cte { get; set; }
        public int Id_Ter { get; set; }
        public int Id_Usu { get; set; }
        public int Id_Seg { get; set; }
        public int Id_Area { get; set; }
        public int Id_Apl { get; set; }
        public int Id_Sol { get; set; }

        public string Seg_Descripcion { get; set; }
        public string Area_Desc { get; set; }
        public string Apl_Descripcion { get; set; }
        public string Sol_Descripcion { get; set; }


        public string Cliente { get; set; }

        public string Estatus { get; set; }

        public string Sucursal { get; set; }
        public string Rik { get; set; }
        public string Zona { get; set; }
        public int Tipo { get; set; }
        public int AnioFin { get; set; }
        public int MesFin { get; set; }
        public int AnioIni { get; set; }
        public int MesIni { get; set; }

        public double VPO { get; set; }
        public double VPT_Total { get; set; }
        public double VPT { get; set; }
        public double Venta { get; set; }
        public double GAP { get; set; }
        public double VPOMeta { get; set; }
        public int Anio { get; set; }
        public int Mes { get; set; }
        public string TipoProducto { get; set; }
        public string Bracket { get; set; }
        public string Uen_Desc { get; set; }

        public double Porc_CoberturaVPT { get; set; }
        public double Porc_CoberturaVPO { get; set; }

        public int Id_Uen { get; set; }
        public string Seg_Unidades { get; set; }
        public double Seg_ValUniDim { get; set; }
        public double Cantidad { get; set; }

        public double PorcentajeAplicacion { get; set; }
        public double PorcentajeAplicacionPotencial { get; set; }
        public double AplPotencialGeneralVende { get; set; }
        public double AplPotencialGeneralNoVende { get; set; }

    }
}