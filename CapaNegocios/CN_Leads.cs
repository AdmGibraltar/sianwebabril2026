using System;
using System.Collections.Generic;
using System.Data;
using CapaDatos;
using CapaEntidad;

namespace CapaNegocios
{
    public class CN_Leads
    {

        public void ConsultaLeadsRik(int PageNumber, int PageSize,
           Leads territorio, string TextoBuscar, int TipoRepresentante, int Ter_Activo,
           string Conexion, ref List<Leads> List)
        {
            CD_Leads claseCapaDatos = new CD_Leads();
            claseCapaDatos.ConsultaLeadsRik(PageNumber, PageSize,
                territorio, TextoBuscar, TipoRepresentante, Ter_Activo,
                Conexion, ref List);
        }

        public void ReporteLeads_TiempoRespuesta(ReporteCRM RegistroReporteCRM, ref List<ReporteCRM> listaReporteCRM, string Conexion)
        {
            CD_Leads cd = new CD_Leads();
            cd.ReporteLeads_TiempoRespuesta(RegistroReporteCRM, ref listaReporteCRM, Conexion);

        }

        public void ReporteLeads_TiempoRespuestaBarra(ReporteCRM RegistroReporteCRM, ref List<ReporteCRM> listaReporteTiempoEstandar, ref List<ReporteCRM> listaReporteTiempoLimite,
          ref List<ReporteCRM> listaReporteFuera, string Conexion)
        {
            CD_Leads cd = new CD_Leads();
            cd.ReporteLeads_TiempoRespuestaBarra(RegistroReporteCRM, ref listaReporteTiempoEstandar, ref listaReporteTiempoLimite,
           ref listaReporteFuera, Conexion);
        }


        public void ConsultaGerente(Usuario usuario, string conexion, ref List<Usuario> List)
        {
            try
            {
                CapaDatos.CD_Leads claseCapaDatos = new CapaDatos.CD_Leads();
                claseCapaDatos.ConsultaGerente(usuario, conexion, ref List);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void ConsultaLeadsGerente(int PageNumber, int PageSize,
          Leads territorio, string TextoBuscar, int TipoRepresentante, int Ter_Activo,
          string Conexion, ref List<Leads> List, int estatusBuscar, int filtrarFecha, DateTime fechaInicial, DateTime fechaFinal)
        {
            CD_Leads claseCapaDatos = new CD_Leads();
            claseCapaDatos.ConsultaLeadsGerente(PageNumber, PageSize,
                territorio, TextoBuscar, TipoRepresentante, Ter_Activo,
                Conexion, ref List, estatusBuscar, filtrarFecha, fechaInicial, fechaFinal);
        }



        public void ConsultaListaLeads(Leads leads, ref List<Leads> listLeads, string Conexion)
        {
            try
            {
                CD_Leads cd_leads = new CD_Leads();
                cd_leads.ConsultaListaLeads(leads, ref listLeads, Conexion);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void InsertarLeads(Leads conv, ref int Verificador, ref int Id_PC, string Conexion)
        {
            try
            {
                CD_Leads cd_conv = new CD_Leads();
                cd_conv.InsertarLeads(conv, ref Verificador, ref Id_PC, Conexion);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public int CancelaLeads(int idLeads, int tipocancelacion, string motivoCancelacion, Sesion sesion, string conexion)
        {
            CD_Leads cdLeads = new CD_Leads();
            int iEstatus = 0;
            iEstatus = cdLeads.CancelaLeads(idLeads, sesion.Id_Emp, sesion.Id_U, tipocancelacion, motivoCancelacion, conexion, ref iEstatus);
            return iEstatus;
        }

        public int asociarLeadAProspecto(Sesion s, int idCte, int idRik, int idLead, string conexion)
        {
            CD_Leads cdLeads = new CD_Leads();
            int iEstatus = 0;
            iEstatus = cdLeads.asociarLeadAProspecto(s.Id_Emp, s.Id_Cd, idCte, idRik, idLead, s.Id_U, conexion, ref iEstatus);
            return iEstatus;
        }

        public int ActualizarAgente(Sesion s, int idCD, int idRik, int idLead, string conexion)
        {
            CD_Leads cdLeads = new CD_Leads();
            int iEstatus = 0;
            //si no recibio Id_cd entonces le mando el de el sistema 
            // y no habría cambios pero si le mando uno diferente entonces lo paso tal como viene.

            if (idCD == -1)
            {

                return iEstatus;
            }

            //si la sucursal es diferente a la local entonces continuo y Rik = -1

            if (idCD == s.Id_Cd) // s.Id_Cd es igual al id_Cd de la sucursal
            {
                if (idRik == -1)
                {
                    //si no cambio la sucursal y no cambio el rik entonces regreso 0 
                    return iEstatus;
                }
            }
            else
            {
                //Si la sucursal es diferente a la local entonces rik es cero y solo cambia de sucursal
                if (idRik != -1)
                {
                    return iEstatus;
                }

                //idRik = -1;
            }

            iEstatus = cdLeads.ActualizarAgente(s.Id_Emp, idCD, idRik, idLead, s.Id_U, conexion, ref iEstatus);
            return iEstatus;
        }

        public void ConsultaLeadsObservarTotales(Leads RegistroPresupuesto, ref List<Leads> list_Presupuesto, string Conexion)
        {
            CD_Leads cd = new CD_Leads();
            cd.ConsultaLeadsObservarTotales(RegistroPresupuesto, ref list_Presupuesto, Conexion);
        }


        public void ConsultaLeadsIntegrarResultados(Leads RegistroPresupuesto, ref List<Leads> list_Presupuesto, string Conexion)
        {
            CD_Leads cd = new CD_Leads();
            cd.ConsultaLeadsIntegrarResultados(RegistroPresupuesto, ref list_Presupuesto, Conexion);
        }

        public void ConsultarProyectosAsig(CatPresupuesto RegistroPresupuesto, ref List<CatPresupuesto> list_Presupuesto, string Conexion)
        {
            CD_Leads cd = new CD_Leads();
            cd.ConsultarProyectosAsig(RegistroPresupuesto, ref list_Presupuesto, Conexion);
        }

        public void ConsultaIntegrarLeads(CatPresupuesto RegistroPresupuesto, int seleccion, ref List<CatPresupuesto> list_Presupuesto, string Conexion)
        {
            CD_Leads cd = new CD_Leads();
            cd.ConsultaIntegrarLeads(RegistroPresupuesto, seleccion, ref list_Presupuesto, Conexion);
        }

        public void LlenaCombo(string Conexion, string sp, ref List<Comun> Lista)
        {
            try
            {
                CD_Leads CD = new CD_Leads();
                CD.LlenaCombo(Conexion, sp, ref Lista);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaReporteGraficaCantidad(ReporteCRM RegistroReporteCRM, ref List<ReporteCRM> listaReporteCRM, string Conexion)
        {
            CD_Leads cd = new CD_Leads();
            cd.ConsultaReporteGraficaCantidad(RegistroReporteCRM, ref listaReporteCRM, Conexion);

        }

        public void ConsultaGraficaEfectividadCantidad(ReporteCRM RegistroReporteCRM, ref List<ReporteCRM> listaReporteCRMAnalisis, ref List<ReporteCRM> listaReporteCRMPresentacion,
           ref List<ReporteCRM> listaReporteCRMNegociacion, string Conexion)
        {
            CD_Leads cd = new CD_Leads();
            cd.ConsultaGraficaEfectividadCantidad(RegistroReporteCRM, ref listaReporteCRMAnalisis, ref listaReporteCRMPresentacion,
           ref listaReporteCRMNegociacion, Conexion);
        }

        public void ConsultaReporteGraficaEfectividadMonto(ReporteCRM RegistroReporteCRM, ref List<ReporteCRM> listaReporteCRMAnalisis, ref List<ReporteCRM> listaReporteCRMPresentacion,
           ref List<ReporteCRM> listaReporteCRMNegociacion, ref List<ReporteCRM> listaReporteCRMCierre, ref List<ReporteCRM> listaReporteCRMCancelacion, string Conexion)
        {
            CD_Leads cd = new CD_Leads();
            cd.ConsultaReporteGraficaEfectividadMonto(RegistroReporteCRM, ref listaReporteCRMAnalisis, ref listaReporteCRMPresentacion,
           ref listaReporteCRMNegociacion, ref listaReporteCRMCierre, ref listaReporteCRMCancelacion, Conexion);

        }

        public void ConsultaReporteEfectividadGrafBarrasCantidad(ReporteCRM RegistroReporteCRM, ref List<ReporteCRM> listaReporteCRMAnalisis, ref List<ReporteCRM> listaReporteCRMPresentacion,
        ref List<ReporteCRM> listaReporteCRMNegociacion, ref List<ReporteCRM> listaReporteCRMCierre, ref List<ReporteCRM> listaReporteCRMCancelacion, string Conexion)
        {
            CD_Leads cd = new CD_Leads();
            cd.ConsultaReporteEfectividadGrafBarrasCantidad(RegistroReporteCRM, ref listaReporteCRMAnalisis, ref listaReporteCRMPresentacion,
           ref listaReporteCRMNegociacion, ref listaReporteCRMCierre, ref listaReporteCRMCancelacion, Conexion);
        }

    }
}