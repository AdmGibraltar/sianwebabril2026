using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaModelo
{
    public class CatCRM_ListadoRIKS
    {
        public int Id_Rik { get; set; }

        public string Rik_Nombre { get; set; }

        public string Cd_nombre { get; set; }

        public int Id_Cte { get; set; }

        public string Cte_NomComercial { get; set; }

        public int Id_Ter { get; set; }

        public int Id_Seg { get; set; }

        public string Seg_Descripcion { get; set; }

        public DateTime UltFechafac { get; set; }

        public double Venta { get; set; }

        public double Integralidad { get; set; }
        public double Integralidad_Obs { get; set; }

        public double Seg_ValUniDim { get; set; }


        public string Seg_Unidades { get; set; }

        public double Cantidad { get; set; }

        public double VPT { get; set; }

        public double VPO { get; set; }
        public double VPOMeta { get; set; }
        public double PromedioTrimestral { get; set; }

    }
}