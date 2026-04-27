using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaModelo
{

    public class CatCRM_IntegralidadMes
    {
        public int Anio { get; set; }
        public string Mes { get; set; }

        public int MesNum { get; set; }

        public double Venta { get; set; }

        public List<CatCRM_IntMapaApps> Segmentos;
    }



    public class CatCRM_IntMapaApps
    {
        public int Id_Seg { get; set; }

        public string Seg_Descripcion { get; set; }

        public int Id_Area { get; set; }

        public string Area_Descripcion { get; set; }

        public int Id_Sol { get; set; }

        public string Sol_Descripcion { get; set; }

        public List<CatCRM_IntAplicaciones> Aplicaciones { get; set; }

    }



    public class CatCRM_IntAplicaciones
    {

        public int Id_Op { get; set; }

        public int Id_Apl { get; set; }


        public int Id_Seg { get; set; }
        public int Id_Area { get; set; }
        public int Id_Sol { get; set; }


        public string Apl_Descripcion { get; set; }

        public double Venta { get; set; }
        public double Integralidad { get; set; }
        public double Area_Op { get; set; }

        public double Apl_Potencial { get; set; }

        public double VPO { get; set; }

        public double VPOMeta { get; set; }
        public double VPT { get; set; }

        public double GAP_Teorico { get; set; }

        public double GAP_Observado { get; set; }



        public double Est_CEne { get; set; }
        public double Est_CFeb { get; set; }
        public double Est_CMar { get; set; }
        public double Est_CAbr { get; set; }
        public double Est_CMay { get; set; }
        public double Est_CJun { get; set; }
        public double Est_CJul { get; set; }
        public double Est_CAgo { get; set; }
        public double Est_CSep { get; set; }
        public double Est_COct { get; set; }
        public double Est_CNov { get; set; }
        public double Est_CDic { get; set; }


    }

    public class CatCRM_IntArea
    {

        public int Id_Area { get; set; }

        public string Area_Descripcion { get; set; }

    }




    public class CatCRM_ReporteIntegralidadGral
    {
        public int Id_Cte { get; set; }

        public int Id_Ter { get; set; }

        public int Id_Usu { get; set; }

        public int Id_Seg { get; set; }

        public Double VPT { get; set; }

        public Double VPO { get; set; }

        public Double Venta_Real { get; set; }

        public Double GAP { get; set; }

        public Double Integralidad { get; set; }

        public Double Integralidad_Obs { get; set; }

        public string Rik_Nombre { get; set; }

    }




    public class CatCRM_ReporteIntegralidadGral_sExcel
    {
        public int Id_Op { get; set; }
        public int Id_Cte { get; set; }
        public int Id_Ter { get; set; }
        public String Rik_Nombre { get; set; }

        public int Id_Seg { get; set; }
        public string Seg_Descripcion { get; set; }
        public int Id_Area { get; set; }
        public string Area_Descripcion { get; set; }
        public int Id_Sol { get; set; }
        public string Sol_Descripcion { get; set; }
        public int Id_Apl { get; set; }
        public string Apl_Descripcion { get; set; }

        public double Apl_Potencial { get; set; }

        public double VPO { get; set; }

        public int SegVal_UniDim { get; set; }

        public Double VPT_Total { get; set; }

        public Double VPT { get; set; }

        public Double Venta_Real { get; set; }

        public Double GAP { get; set; }

        public int Anio { get; set; }

        public int Mes { get; set; }
    }


    public class eCrmInt_MapaAplicaciones_Detalle
    {
        public int Id_Op { get; set; }
        public int Id_Cte { get; set; }
        public int Id_Ter { get; set; }

        public int Id_Usu { get; set; }

        public int Estatus { get; set; }

        public String Rik_Nombre { get; set; }

        public int Id_Seg { get; set; }
        public string Seg_Descripcion { get; set; }
        public int Id_Area { get; set; }
        public string Area_Descripcion { get; set; }
        public int Id_Sol { get; set; }
        public string Sol_Descripcion { get; set; }
        public int Id_Apl { get; set; }
        public string Apl_Descripcion { get; set; }

        public double Apl_Potencial { get; set; }

        public double VPO { get; set; }

        public int SegVal_UniDim { get; set; }

        public Double VPT_Total { get; set; }

        public Double VPT { get; set; }

        public Double Venta_Real { get; set; }

        public Double GAP { get; set; }

        public int Anio { get; set; }

        public int Mes { get; set; }

        public double GAP_Teorico { get; set; }

        public double GAP_Observado { get; set; }
        public double VPOMeta { get; set; }

    }






}
