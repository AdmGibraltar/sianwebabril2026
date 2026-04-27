using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using CapaEntidad;

namespace CapaNegocios
{
    public class CNRSCAgenda
    {
        public void ConsultaPedidoRastreo(AgendaRsc agenda, string Conexion, ref List<AgendaRsc> List)
        {
            CDRSCAgenda cd = new CDRSCAgenda();
            cd.ConsultaPedidoRastreo(agenda, Conexion, ref List);
        }


        public void ConsultaMotivo(int id_emp, int id_usu, int id_Cte, ref List<RSCMotivo> List, string Conexion)
        {
            CDRSCAgenda cd = new CDRSCAgenda();
            cd.ConsultarMotivo(id_emp, id_usu, id_Cte, ref List, Conexion);
        }

        public void ConsultarSubMotivo(int id_emp, int idMotivo, int id_Cte, int id_usu, ref List<RSCMotivo> Lista, string Conexion)
        {
            try
            {
                CDRSCAgenda CD = new CDRSCAgenda();
                CD.ConsultarSubMotivo(id_emp, idMotivo, id_Cte, id_usu, ref Lista, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultarAplicacion(int idSubMotivo, ref List<RSCMotivo> Lista, string Conexion)
        {
            try
            {
                CDRSCAgenda CD = new CDRSCAgenda();
                CD.ConsultarAplicacion(idSubMotivo, ref Lista, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultarAgendaDetallada(AgendaRsc Datos, ref List<Agenda> Lista, string Conexion)
        {
            try
            {
                CDRSCAgenda CD = new CDRSCAgenda();
                CD.ConsultarAgendaDetallada(Datos, ref Lista, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultarClienteTipoCriterio(AgendaRsc Datos, ref List<Agenda> Lista, string Conexion)
        {
            try
            {
                CDRSCAgenda CD = new CDRSCAgenda();
                CD.ConsultarClienteTipoCriterio(Datos, ref Lista, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultarPreAgendaDetallada(AgendaRsc Datos, ref List<Agenda> Lista, string Conexion)
        {
            try
            {
                CDRSCAgenda CD = new CDRSCAgenda();
                CD.ConsultarPreAgendaDetallada(Datos, ref Lista, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultarAgendaDetalladaLog(AgendaRsc Datos, ref List<AgendaRsc> Lista, string Conexion)
        {
            try
            {
                CDRSCAgenda CD = new CDRSCAgenda();
                CD.ConsultarAgendaDetalladaLog(Datos, ref Lista, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Consultarusuario(Agenda RegistroRik, ref List<Agenda> list_Riks, string Conexion)
        {
            CDRSCAgenda CD = new CDRSCAgenda();
            CD.Consultarusuario(RegistroRik, ref list_Riks, Conexion);
        }

        public void ConsultarTipoUsuario(ref List<Agenda> list_Riks, string Conexion)
        {
            CDRSCAgenda CD = new CDRSCAgenda();
            CD.ConsultarTipoUsuario(ref list_Riks, Conexion);
        }

        public void TipoUsuarioConsultar(Agenda lista, ref List<Agenda> list_Riks, string Conexion)
        {
            CDRSCAgenda CD = new CDRSCAgenda();
            CD.TipoUsuarioConsultar(lista, ref list_Riks, Conexion);
        }



        public void AltaAgendaDetallada(AgendaRsc Registro, ref int verificador, string Conexion)
        {
            CDRSCAgenda CD = new CDRSCAgenda();
            CD.AltaAgendaDetallada(Registro, ref verificador, Conexion);
        }

        public void ActualizarAgendaDetallada(AgendaRsc Registro, ref int verificador, string Conexion)
        {
            CDRSCAgenda CD = new CDRSCAgenda();
            CD.ActualizarAgendaDetallada(Registro, ref verificador, Conexion);
        }

        public void BajaAgendaDetallada(AgendaRsc Registro, ref List<Agenda> Lista, string Conexion)
        {
            CDRSCAgenda CD = new CDRSCAgenda();
            CD.BajaAgendaDetallada(Registro, ref Lista, Conexion);
        }

        public void AgendaInicioActividades(AgendaRsc Registro, ref List<Agenda> Lista, string Conexion)
        {
            CDRSCAgenda CD = new CDRSCAgenda();
            CD.AgendaInicioActividades(Registro, ref Lista, Conexion);
        }

        public void ConsultaIniciarAgendaDetallada(AgendaRsc Registro, ref int verificador, string Conexion)
        {
            CDRSCAgenda CD = new CDRSCAgenda();
            CD.ConsultaIniciarAgendaDetallada(Registro, ref verificador, Conexion);
        }

        public void AgendaFinalizarActividades(AgendaRsc Registro, ref List<Agenda> Lista, string Conexion)
        {
            CDRSCAgenda CD = new CDRSCAgenda();
            CD.AgendaFinalizarActividades(Registro, ref Lista, Conexion);
        }

        public void ConsultarACtividadInicioFin(AgendaRsc Registro, ref List<Agenda> Lista, string Conexion)
        {
            CDRSCAgenda CD = new CDRSCAgenda();
            CD.ConsultarACtividadInicioFin(Registro, ref Lista, Conexion);
        }

        public void ConsultarACtividadActivas(AgendaRsc Registro, ref List<Agenda> Lista, string Conexion)
        {
            CDRSCAgenda CD = new CDRSCAgenda();
            CD.ConsultarACtividadActivas(Registro, ref Lista, Conexion);
        }

        public void ConsultarMonitorActividadesGral(AgendaRsc Registro, ref List<AgendaRsc> Lista, string Conexion)
        {
            CDRSCAgenda CD = new CDRSCAgenda();
            CD.ConsultarMonitorActividadesGral(Registro, ref Lista, Conexion);
        }

        public void ConsultarMonitorActividades(AgendaRsc Registro, ref List<AgendaRsc> Lista, string Conexion)
        {
            CDRSCAgenda CD = new CDRSCAgenda();
            CD.ConsultarMonitorActividades(Registro, ref Lista, Conexion);
        }

        public void ConsultarActividades(AgendaRsc Registro, ref List<AgendaRsc> Lista, string Conexion)
        {
            CDRSCAgenda CD = new CDRSCAgenda();
            CD.ConsultarActividades(Registro, ref Lista, Conexion);
        }

        public void ConsultarMonitorAgenda(AgendaRsc Registro, ref List<AgendaRsc> Lista, string Conexion)
        {
            CDRSCAgenda CD = new CDRSCAgenda();
            CD.ConsultarMonitorAgenda(Registro, ref Lista, Conexion);
        }

        public void ConsultarMonitorAgendaDetalle(AgendaRsc Registro, ref List<AgendaRsc> Lista, string Conexion)
        {
            CDRSCAgenda CD = new CDRSCAgenda();
            CD.ConsultarMonitorAgendaDetalle(Registro, ref Lista, Conexion);
        }

        public void monitorActServicioValor(AgendaRsc Registro, ref List<AgendaRsc> Lista, string Conexion)
        {
            CDRSCAgenda CD = new CDRSCAgenda();
            CD.monitorActServicioValor(Registro, ref Lista, Conexion);
        }

        public void monitorActServicioValordet(AgendaRsc Registro, ref List<AgendaRsc> Lista, string Conexion)
        {
            CDRSCAgenda CD = new CDRSCAgenda();
            CD.monitorActServicioValordet(Registro, ref Lista, Conexion);
        }

        public void ConsultaragendaGeolocalizacion(Geolocalizacion geo, ref List<Geolocalizacion> List, string Conexion)
        {
            CDRSCAgenda CD = new CDRSCAgenda();
            CD.ConsultaragendaGeolocalizacion(geo, ref List, Conexion);
        }

        public void CargarPreagenda(AgendaRsc agenda, ref List<AgendaRsc> List, string Conexion)
        {
            CDRSCAgenda CD = new CDRSCAgenda();
            CD.CargarPreagenda(agenda, ref List, Conexion);
        }

        public void PreagendaBaja(AgendaRsc agenda, ref int verificador, string Conexion)
        {
            CDRSCAgenda CD = new CDRSCAgenda();
            CD.PreagendaBaja(agenda, ref verificador, Conexion);
        }

        public void ActualizarPrecarga(AgendaRsc Registro, ref int verificador, string Conexion)
        {
            CDRSCAgenda CD = new CDRSCAgenda();
            CD.ActualizarPrecarga(Registro, ref verificador, Conexion);
        }

        public void AltaAgendaCargaARchivo(AgendaRsc Registro, ref int verificador, string Conexion)
        {
            CDRSCAgenda CD = new CDRSCAgenda();
            CD.AltaAgendaCargaARchivo(Registro, ref verificador, Conexion);
        }

        public void ConsultaTrackingActividad(AgendaRsc agenda, string Conexion, ref List<AgendaRsc> List)
        {
            CDRSCAgenda CD = new CDRSCAgenda();
            CD.ConsultaTrackingActividad(agenda, Conexion, ref List);
        }

        public void spRepCumplimientoVI_Dinamico(eClenteBuscar_Params Pms, string Conexion, ref List<eClienteBuscar> lst)
        {
            CDRSCAgenda CD = new CDRSCAgenda();
            CD.spRepCumplimientoVI_Dinamico(Pms, Conexion, ref lst);
        }

        public void ConsultaActHibridas(AgendaRsc agenda, ref List<AgendaRsc> List, string Conexion)
        {
            CDRSCAgenda CD = new CDRSCAgenda();
            CD.ConsultaActHibridas(agenda, ref List, Conexion);
        }

        public void ActHibridasInsertar(AgendaRsc Registro, ref int verificador, string Conexion)
        {
            CDRSCAgenda CD = new CDRSCAgenda();
            CD.ActHibridasInsertar(Registro, ref verificador, Conexion);
        }

        public void ActHibridasBaja(AgendaRsc Registro, ref int verificador, string Conexion)
        {
            CDRSCAgenda CD = new CDRSCAgenda();
            CD.ActHibridasBaja(Registro, ref verificador, Conexion);
        }


        public void VentaServicioAnual(VenEstadisticaVentas Ventas, ref List<VenEstadisticaVentas> List, string Conexion)
        {
            CDRSCAgenda CD = new CDRSCAgenda();
            CD.VentaServicioAnual(Ventas, ref List, Conexion);
        }

    }
}