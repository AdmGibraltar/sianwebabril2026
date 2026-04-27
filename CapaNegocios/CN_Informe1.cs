using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using CapaDatos;

namespace CapaNegocios
{
    public class CN_Informe1
    {
        // CRM 1                 
        public void spCRM_ControlPromocion_LimpiezaAplicacion(int Id_Emp, int Id_Cd, int Id_U, int Id_Rik, string periodo, int IntConsulta, string monto1, string monto2, bool PNuevo, ref List<CapaEntidad.Informe1> Lista, string Conexion)
        {
            try
            {
                CD_CrmInforme1 Inf = new CD_CrmInforme1();
                Inf.spCRM_ControlPromocion_LimpiezaAplicacion(Id_Emp, Id_Cd, Id_U, Id_Rik, periodo, IntConsulta, monto1, monto2, PNuevo, ref Lista, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // CRM 2 
        public void spCRM_ControlPromocion_LimpiezaAplicacion2(
            int Id_Emp, int Id_Cd, int Id_U, int Id_Rik, string periodo, int IntConsulta, string monto1, string monto2, bool PNuevo,
            ref List<CapaEntidad.Informe1> Lista, string Conexion)
        {
            CD_CrmInforme1 Inf = new CD_CrmInforme1();
            Inf.spCRM_ControlPromocion_LimpiezaAplicacion2(Id_Emp, Id_Cd, Id_U, Id_Rik, periodo, IntConsulta, monto1, monto2, PNuevo, ref Lista, Conexion);
        }

        // 
        // 13Jun2019 - RFH 
        // CRM 2 - Reporte pro UEN Aplicacion Solucion  
        //
        public void spCRM_ControlPromocion_LimpiezaAplicacion2_BySegmento(
            int Id_Emp, int Id_Cd, int Id_U, int Id_Rik, string periodo, int IntConsulta, string monto1, string monto2, bool PNuevo,
            int Id_Uen, int Id_Seg, int Id_Area, int Id_Sol, int Id_Apl,
            ref List<CapaEntidad.Informe1> Lista, string Conexion)
        {
            CD_CrmInforme1 Inf = new CD_CrmInforme1();
            Inf.spCRM_ControlPromocion_LimpiezaAplicacion2_BySegmento(
                Id_Emp, Id_Cd, Id_U, Id_Rik, periodo, IntConsulta, monto1, monto2, PNuevo,
                Id_Uen, Id_Seg, Id_Area, Id_Sol, Id_Apl,
                ref Lista, Conexion);
        }

        public void spCRM_ControlEntrada(int Id_Emp, int Id_Cd, int Id_Rik, string periodo, ref List<CapaEntidad.Informe2> Lista, string Conexion)
        {
            try
            {
                CD_CrmInforme1 Inf = new CD_CrmInforme1();
                Inf.spCRM_ControlEntrada(Id_Emp, Id_Cd, Id_Rik, periodo, ref Lista, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void spCRM_ControlPromocion_DIINumero(int Id_Emp, int Id_Cd, int Id_Rik, string Periodo, ref List<CapaEntidad.Informe3> Lista, string Conexion)
        {
            try
            {
                CD_CrmInforme1 Inf = new CD_CrmInforme1();
                Inf.spCRM_ControlPromocion_DIINumero(Id_Emp, Id_Cd, Id_Rik, Periodo, ref Lista, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        //
    }
}