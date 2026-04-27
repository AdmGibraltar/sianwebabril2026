using CapaDatos;
using CapaModelo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocios
{
    public class CN_CatCRMInt_Integralidad
    {
        public List<CatCRM_ListadoRIKS> ListadoRiks(int id_emp, int id_cd, string Riks, string Ctes, string Segs, string Conexion)
        {
            CD_CatCRMInt_Integralidad CM = new CD_CatCRMInt_Integralidad();
            return CM.ListadoRiks(id_emp, id_cd, Riks, Ctes, Segs, Conexion);
        }

        public List<CatCRM_ListadoRIKS> ListadoRiks_Ver2(int id_emp, int id_cd, string Riks, string Ctes, string Segs, string Conexion)
        {
            CD_CatCRMInt_Integralidad CM = new CD_CatCRMInt_Integralidad();
            return CM.ListadoRiks_Ver2(id_emp, id_cd, Riks, Ctes, Segs, Conexion);
        }

        // 22SEP-2021 RFH
        public DataTable ListadoRiksToDt(int id_emp, int id_cd, string Riks, string Ctes, string Segs, string Conexion)
        {
            CD_CatCRMInt_Integralidad CM = new CD_CatCRMInt_Integralidad();
            return CM.ListadoRiksToDt(id_emp, id_cd, Riks, Ctes, Segs, Conexion);
        }

        // 
        public List<CatCRM_IntegralidadMes> ListadoAplicaciones(int Id_Cte, int Id_Rik, int Id_Ter, int Id_Seg, string Conexion)
        {
            CD_CatCRMInt_Integralidad CM = new CD_CatCRMInt_Integralidad();
            return CM.ListadoAplicaciones(Id_Cte, Id_Rik, Id_Ter, Id_Seg, Conexion);
        }

        // 4Oct-2021 RFH
        public List<CatCRM_IntegralidadMes> ListadoAplicaciones_Ver2(int Id_Cte, int Id_Rik, int Id_Ter, int Id_Seg, string Conexion)
        {
            CD_CatCRMInt_Integralidad CM = new CD_CatCRMInt_Integralidad();
            return CM.ListadoAplicaciones_Ver2(Id_Cte, Id_Rik, Id_Ter, Id_Seg, Conexion);
        }

        public List<CatCRM_IntegralidadMes> CNActualizaVPOMeta(int Id_Cte, int Id_Ter,  string Conexion, float VPOMeta)
        {
            CD_CatCRMInt_Integralidad CM = new CD_CatCRMInt_Integralidad();
            return CM.CDActualizaVPOMeta(Id_Cte,  Id_Ter , Conexion, VPOMeta);
        }

        // 5Oct-2021 RFH
        public List<eCrmInt_MapaAplicaciones_Detalle> spCrmInt_MapaAplicaciones_Detalle_Ver2(
            int Id_Cte, int Id_Ter, int Id_Rik, int Id_Seg, int Cal_Anio, int Cal_Mes, string Conexion)
        {
            CD_CatCRMInt_Integralidad CM = new CD_CatCRMInt_Integralidad();
            return CM.spCrmInt_MapaAplicaciones_Detalle_Ver2(Id_Cte, Id_Ter, Id_Rik, Id_Seg, Cal_Anio, Cal_Mes, Conexion);
        }

        public List<CatCRM_ReporteIntegralidadGral> ListadoIntegralidadRIK(int Id_Emp, int Id_Cd, string riksSeleccionados, string Conexion)
        {
            CD_CatCRMInt_Integralidad CM = new CD_CatCRMInt_Integralidad();
            return CM.ListadoIntegralidadRIK(Id_Emp, Id_Cd, riksSeleccionados, Conexion);
        }

        // 21OCT-2021 RFH
        public List<CatCRM_ReporteIntegralidadGral> spCrmIntegralidad_PorRik(int Id_Emp, int Id_Cd, string riksSeleccionados, string Conexion)
        {
            CD_CatCRMInt_Integralidad CM = new CD_CatCRMInt_Integralidad();
            return CM.spCrmIntegralidad_PorRik(Id_Emp, Id_Cd, riksSeleccionados, 0, Conexion);
        }

        public DataTable ListadoIntegralidadRIK_Excel(int Id_Emp, int Id_Cd, string Conexion)
        {
            CD_CatCRMInt_Integralidad CM = new CD_CatCRMInt_Integralidad();
            return CM.ListadoIntegralidadRIK_Excel(Id_Emp, Id_Cd, Conexion);
        }


    }
}