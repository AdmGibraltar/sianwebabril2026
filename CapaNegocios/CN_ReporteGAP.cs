using System;
using System.Collections.Generic;
using CapaDatos;
using CapaEntidad;
using System.Data;


namespace CapaNegocios
{
    public class CN_ReporteGAP
    {
        public static void ConsultaListaReporteGAP_Producto(ReporteGAP reporteGAP, ref List<ReporteGAP> listReporteGAP, string Conexion)
        {
            try
            {
                CD_ReporteGAP cd_reporteGAP = new CD_ReporteGAP();
                cd_reporteGAP.ConsultaListaReporteGAP(reporteGAP, ref listReporteGAP, Conexion);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static void ConsultaListaReporteGAPRiks(ReporteGAP reporteGAP, ref List<ReporteGAP> listReporteGAP, string Conexion)
        {
            try
            {
                CD_ReporteGAP cd_reporteGAP = new CD_ReporteGAP();
                cd_reporteGAP.ConsultaListaReporteGAPRiks(reporteGAP, ref listReporteGAP, Conexion);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static void ConsultaListaReporteGAPRik(ReporteGAP reporteGAP, ref List<ReporteGAP> listReporteGAP, string Conexion)
        {
            try
            {
                CD_ReporteGAP cd_reporteGAP = new CD_ReporteGAP();
                cd_reporteGAP.ConsultaListaReporteGAPRik(reporteGAP, ref listReporteGAP, Conexion);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static List<GestionIncrementoPrecios> ConsultaListaReporteGAP_Producto2(GestionIncrementoPrecios reporteGAP, ref List<GestionIncrementoPrecios> listReporteGAP, string Conexion)
        {
            try
            {
                CD_ReporteGAP cd_reporteGAP = new CD_ReporteGAP();
                if (reporteGAP.Id_TipoReporte == 0)
                {
                    cd_reporteGAP.ConsultaListaGPMa_Consultadet(reporteGAP, ref listReporteGAP, Conexion);
                }
                else
                {
                    cd_reporteGAP.ConsultaListaGestionIncrementoClientes(reporteGAP, ref listReporteGAP, Conexion);
                }

                return listReporteGAP;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static int InsertarGestionIncremento(GestionIncrementoPrecios reporteGAP, List<GestionIncrementoPrecios> listReporteGAP, string Conexion, ref int verificador)
        {
            try
            {
                CD_ReporteGAP cd_reporteGAP = new CD_ReporteGAP();
                cd_reporteGAP.InsertarGestionIncremento(reporteGAP, listReporteGAP, Conexion, ref verificador);
                return verificador;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static int CerrarGestionIncremento(GestionIncrementoPrecios reporteGAP, string Conexion, ref int verificador)
        {
            try
            {
                CD_ReporteGAP cd_reporteGAP = new CD_ReporteGAP();
                cd_reporteGAP.CerrarGestionIncremento(reporteGAP, Conexion, ref verificador);
                return verificador;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static object ConsultaPropuesta(int id_cte, int id_rik, int id_cd, string Conexion, ref int Id_ReporteGP, ref object datos)
        {
            try
            {
                CD_ReporteGAP cd_reporteGAP = new CD_ReporteGAP();
                datos = cd_reporteGAP.ConsultaPropuesta(id_cte, id_rik, id_cd, Conexion, ref Id_ReporteGP);
                return datos;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public void ReporteRentabilidad_ConsultarTotales(int Id_Emp, int Id_Cd_Ver, int Id_Cte, int? Id_Ter, string periodo, string ventas, ref DataTable dt, string id_reportegp, string Conexion)
        {
            try
            {
                new CD_ReporteGAP().ReporteRentabilidad_ConsultarTotales(Id_Emp, Id_Cd_Ver, Id_Cte, Id_Ter, periodo, ventas, ref dt, id_reportegp, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}