using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using CapaEntidad;
using CapaDatos;

namespace CapaNegocios
{
    public class CN_ServiciosValor
    {

//-------------------------------------------------------------------------------------------

        public void GrabaSegmento(ServiciosValor Ma, string conexion, ref int ver)
        {
            try
            {
                CapaDatos.CD_ServiciosValor claseCapaDatos = new CapaDatos.CD_ServiciosValor();
                claseCapaDatos.GrabaSegmento(Ma, conexion, ref ver);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaSegmento(ServiciosValor SV, string conexion, ref List<ServiciosValor> Servs)
        {
            try
            {
                CapaDatos.CD_ServiciosValor claseCapaDatos = new CapaDatos.CD_ServiciosValor();
                claseCapaDatos.ConsultaSegmento(SV, conexion, ref Servs);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

//-------------------------------------------------------------------------------------------

        public void GrabaServicio(ServiciosValor Ma, string conexion, ref int ver)
        {
            try
            {
                CapaDatos.CD_ServiciosValor claseCapaDatos = new CapaDatos.CD_ServiciosValor();
                claseCapaDatos.GrabaServicio(Ma, conexion, ref ver);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaServicio(ServiciosValor SV, string conexion, ref List<ServiciosValor> Servs)
        {
            try
            {
                CapaDatos.CD_ServiciosValor claseCapaDatos = new CapaDatos.CD_ServiciosValor();
                claseCapaDatos.ConsultaServicio(SV, conexion, ref Servs);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CapServicioConsultaSW(ServiciosValor SV,   ref List<ServiciosValor> Servs, string conexion)
        {
            try
            {
                CapaDatos.CD_ServiciosValor claseCapaDatos = new CapaDatos.CD_ServiciosValor();
                claseCapaDatos.CapServicioConsultaSW(SV, conexion, ref Servs);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CapServicioConsultaSWticket(ServiciosValor SV, ref ServiciosValor Servs, string conexion)
        {
            try
            {
                CapaDatos.CD_ServiciosValor claseCapaDatos = new CapaDatos.CD_ServiciosValor();
                claseCapaDatos.CapServicioConsultaSWticket(SV, conexion, ref Servs);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CapServicioActualizarSW(ServiciosValor SV, ref int verificador, string conexion)
        {
            try
            {
                CapaDatos.CD_ServiciosValor claseCapaDatos = new CapaDatos.CD_ServiciosValor();
                claseCapaDatos.CapServicioActualizarSW(SV, conexion, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void CapFotoServicioConsultaSWticket(ServiciosValor SV, ref List<ServiciosValor> Servs, string conexion)
        {
            try
            {
                CapaDatos.CD_ServiciosValor claseCapaDatos = new CapaDatos.CD_ServiciosValor();
                claseCapaDatos.CapFotoServicioConsultaSWticket(SV, conexion, ref Servs);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


            //-------------------------------------------------------------------------------------------

            public void GrabaRol(ServiciosValor Ma, string conexion, ref int ver)
        {
            try
            {
                CapaDatos.CD_ServiciosValor claseCapaDatos = new CapaDatos.CD_ServiciosValor();
                claseCapaDatos.GrabaRol(Ma, conexion, ref ver);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaRol(ServiciosValor SV, string conexion, ref List<ServiciosValor> Servs)
        {
            try
            {
                CapaDatos.CD_ServiciosValor claseCapaDatos = new CapaDatos.CD_ServiciosValor();
                claseCapaDatos.ConsultaRol(SV, conexion, ref Servs);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

//-------------------------------------------------------------------------------------------

        public void GrabaAplicacion(ServiciosValor Ma, string conexion, ref int ver)
        {
            try
            {
                CapaDatos.CD_ServiciosValor claseCapaDatos = new CapaDatos.CD_ServiciosValor();
                claseCapaDatos.GrabaAplicacion(Ma, conexion, ref ver);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaAplicacion(ServiciosValor SV, string conexion, ref List<ServiciosValor> Servs)
        {
            try
            {
                CapaDatos.CD_ServiciosValor claseCapaDatos = new CapaDatos.CD_ServiciosValor();
                claseCapaDatos.ConsultaAplicacion(SV, conexion, ref Servs);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

//-------------------------------------------------------------------------------------------

        public void ListadoSegmentos_Todos(string Conexion, string sp, ref System.Web.UI.WebControls.CheckBoxList Listado)
        {
            try
            {
                CD_ServiciosValor claseCapaDatos = new CD_ServiciosValor();
                System.Collections.Generic.List<Comun> Lista = new System.Collections.Generic.List<Comun>();
                claseCapaDatos.ListadoSegmentos_Todos(Conexion, sp, ref Lista);
                if (Lista.Count > 0)
                {
                    Listado.DataSource = Lista;
                    Listado.DataValueField = "Id";
                    Listado.DataTextField = "Descripcion";
                    Listado.DataBind();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ListadoSegmentosDeServicios(ServiciosValor SV, string Conexion, ref List<ServiciosValor> Listado)
        {
            try
            {
                CapaDatos.CD_ServiciosValor claseCapaDatos = new CapaDatos.CD_ServiciosValor();
                claseCapaDatos.ListadoSegmentosDeServicios(SV, Conexion, ref Listado);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

//-------------------------------------------------------------------------------------------

        public void GrabaSegmentoDeServicio(ServiciosValor Ma, string conexion, ref int ver)
        {
            try
            {
                CapaDatos.CD_ServiciosValor claseCapaDatos = new CapaDatos.CD_ServiciosValor();
                claseCapaDatos.GrabaSegmentoDeServicio(Ma, conexion, ref ver);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void ListadoAplicacionDeSegmentosDeServicios(ServiciosValor SV, string Conexion, ref List<ServiciosValor> Listado)
        {
            try
            {
                CapaDatos.CD_ServiciosValor claseCapaDatos = new CapaDatos.CD_ServiciosValor();
                claseCapaDatos.ListadoAplicacionDeSegmentosDeServicios(SV, Conexion, ref Listado);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void GrabaAppSegmentoDeServicio(ServiciosValor Ma, string conexion, ref int ver)
        {
            try
            {
                CapaDatos.CD_ServiciosValor claseCapaDatos = new CapaDatos.CD_ServiciosValor();
                claseCapaDatos.GrabaAppSegmentoDeServicio(Ma, conexion, ref ver);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ListadoCostoAplicacionDeSegmentosDeServicios(ServiciosValor SV, string Conexion, ref List<ServiciosValor> Listado)
        {
            try
            {
                CapaDatos.CD_ServiciosValor claseCapaDatos = new CapaDatos.CD_ServiciosValor();
                claseCapaDatos.ListadoCostoAplicacionDeSegmentosDeServicios(SV, Conexion, ref Listado);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void LlenaComboDeAplicaciones(Int32 Id1, Int32 Id2, Int32 Id3, Int32 Id4, string conexion, string SP, ref  Telerik.Web.UI.RadComboBox RadComboBox)
        {
            try
            {
                System.Collections.Generic.List<Comun> lista = new System.Collections.Generic.List<Comun>();
                CapaDatos.CD_ServiciosValor claseCapaDatos = new CapaDatos.CD_ServiciosValor();
                //  claseCapaDatos.LlenaComboDeAplicaciones(Id1, Id2, Id3, Id4, SP, conexion, ref lista);
                claseCapaDatos.LlenaComboDeAplicaciones(Id1, Id2, Id3, Id4, conexion, ref lista);
                if (lista.Count > 0)
                {
                    RadComboBox.DataSource = lista;
                    RadComboBox.DataValueField = "Id";
                    RadComboBox.DataTextField = "Descripcion";
                    RadComboBox.DataBind();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaCostoAplicaciones(Int32 Id1, Int32 Id2, Int32 Id3, Int32 Id4, Int32 Id5, string conexion, ref string cozto)
        {
            try
            {
                CapaDatos.CD_ServiciosValor claseCapaDatos = new CapaDatos.CD_ServiciosValor();
                claseCapaDatos.ConsultaCostoAplicaciones(Id1, Id2, Id3, Id4, Id5, conexion, ref cozto);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void GrabaCostoAppSegmentoDeServicio(ServiciosValor Ma, string conexion, ref int ver)
        {
            try
            {
                CapaDatos.CD_ServiciosValor claseCapaDatos = new CapaDatos.CD_ServiciosValor();
                claseCapaDatos.GrabaCostoAppSegmentoDeServicio(Ma, conexion, ref ver);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ListadoMaterialesFiltro(ServiciosValor SV, string Conexion, ref List<ServiciosValor> Listado)
        {
            try
            {
                CapaDatos.CD_ServiciosValor claseCapaDatos = new CapaDatos.CD_ServiciosValor();
                claseCapaDatos.ListadoMaterialesFiltro(SV, Conexion, ref Listado);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AgregaMaterial(ServiciosValor Ma, string conexion, long idPrd)
        {
            try
            {
                CapaDatos.CD_ServiciosValor claseCapaDatos = new CapaDatos.CD_ServiciosValor();
                claseCapaDatos.AgregaMaterial(Ma, conexion, idPrd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void ValidaAgregarMaterial(ServiciosValor Ma, string conexion, long idPrd, ref bool Vlida)
        {
            try
            {
                CapaDatos.CD_ServiciosValor claseCapaDatos = new CapaDatos.CD_ServiciosValor();
                claseCapaDatos.ValidaAgregarMaterial(Ma, conexion, idPrd, ref Vlida);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ELiminarMaterial(ServiciosValor Ma, string conexion, ref bool Vlida)
        {
            try
            {
                CapaDatos.CD_ServiciosValor claseCapaDatos = new CapaDatos.CD_ServiciosValor();
                claseCapaDatos.ELiminarMaterial(Ma, conexion, ref Vlida);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //-------------------------------------------------------------------------------------------

        public void GrabaTipoApp(ServiciosValor Ma, string conexion, ref int ver)
        {
            try
            {
                CapaDatos.CD_ServiciosValor claseCapaDatos = new CapaDatos.CD_ServiciosValor();
                claseCapaDatos.GrabaTipoApp(Ma, conexion, ref ver);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaTipoApp(ServiciosValor SV, string conexion, ref List<ServiciosValor> Servs)
        {
            try
            {
                CapaDatos.CD_ServiciosValor claseCapaDatos = new CapaDatos.CD_ServiciosValor();
                claseCapaDatos.ConsultaTipoApp(SV, conexion, ref Servs);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //-------------------------------------------------------------------------------------------

        public void ListadoTipoApp_Todos(string Conexion, string sp, ref System.Web.UI.WebControls.CheckBoxList Listado)
        {
            try
            {
                CD_ServiciosValor claseCapaDatos = new CD_ServiciosValor();
                System.Collections.Generic.List<Comun> Lista = new System.Collections.Generic.List<Comun>();
                claseCapaDatos.ListadoSegmentos_Todos(Conexion, sp, ref Lista);
                if (Lista.Count > 0)
                {
                    Listado.DataSource = Lista;
                    Listado.DataValueField = "Id";
                    Listado.DataTextField = "Descripcion";
                    Listado.DataBind();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //-------------------------------------------------------------------------------------------


        public void LlenaComboDeTipoApp(Int32 Id1, Int32 Id2, Int32 Id3, Int32 Id4, Int32 Id5, string conexion, string SP, ref  Telerik.Web.UI.RadComboBox RadComboBox)
        {
            try
            {
                System.Collections.Generic.List<Comun> lista = new System.Collections.Generic.List<Comun>();
                CapaDatos.CD_ServiciosValor claseCapaDatos = new CapaDatos.CD_ServiciosValor();
                claseCapaDatos.LlenaComboDeTipoApp(Id1, Id2, Id3, Id4, Id5, conexion, ref lista);
                if (lista.Count > 0)
                {
                    RadComboBox.DataSource = lista;
                    RadComboBox.DataValueField = "Id";
                    RadComboBox.DataTextField = "Descripcion";
                    RadComboBox.DataBind();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //-------------------------------------------------------------------------------------------

        public void BuscaServicio(ref Producto prd, string conexion, int iEmp, int iCd, int iRol, int iServ, int iSegm, string sDescripcion)
        {
            try
            {
                CapaDatos.CD_ServiciosValor claseCapaDatos = new CapaDatos.CD_ServiciosValor();
                claseCapaDatos.BuscaServicio(ref prd, conexion, iEmp, iCd, iRol, iServ, iSegm, sDescripcion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //-------------------------------------------------------------------------------------------

        public void InsertarOrdenServicio(CapaEntidad.Pedido pedido, int iCi, DataTable dt, string Conexion, ref int verificador)
        {
            try
            {
                CD_ServiciosValor claseCapaDatos = new CD_ServiciosValor();
                claseCapaDatos.InsertarOrdenServicio(pedido, iCi, dt, Conexion, ref verificador);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void InsertarOrdenServiciodocumento(CapaEntidad.Pedido pedido, string Conexion, ref int verificador)
        {
            try
            {
                CD_ServiciosValor claseCapaDatos = new CD_ServiciosValor();
                claseCapaDatos.InsertarOrdenServiciodocumento(pedido,  Conexion, ref verificador);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

         


        public void ActualizarOrdenServicio(CapaEntidad.Pedido pedido, int iCi, DataTable dt, string Conexion, ref int verificador)
        {
            try
            {
                CD_ServiciosValor claseCapaDatos = new CD_ServiciosValor();
                claseCapaDatos.ActualizarOrdenServicio(pedido, iCi, dt, Conexion, ref verificador);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


      

        public void ConsultaOrdenServicio(Pedido pedido, string Conexion, ref List<Pedido> List)
        {
            try
            {
                CD_ServiciosValor claseCapaDatos = new CD_ServiciosValor();
                claseCapaDatos.ConsultaOrdenServicio(pedido, Conexion, ref List);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //public void ConsultaOrdenServicioCita(CalendarioCitas CCitas, string Conexion, ref List<CalendarioCitas> List)
        //{

        //    try
        //    {
        //        CD_ServiciosValor claseCapaDatos = new CD_ServiciosValor();
        //        claseCapaDatos.ConsultaOrdenServicioCita(CCitas, Conexion, ref List);

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

 
        //public void ConsultaOrdenServicioCitaDetalle(CalendarioCitas CCitas, string Conexion, ref List<CalendarioCitas> List)
        //{

        //    try
        //    {
        //        CD_ServiciosValor claseCapaDatos = new CD_ServiciosValor();
        //        claseCapaDatos.ConsultaOrdenServicioCitaDetalle(CCitas, Conexion, ref List);

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

         
        //-------------------------------------------------------------------------------------------

        public void ObtieneCita(Clientes ctee, string Conexion, ref int iCit, ref DateTime fCit)
        {
            try
            {
                CD_ServiciosValor claseCapaDatos = new CD_ServiciosValor();
                claseCapaDatos.ObtieneCita(ctee, Conexion, ref iCit, ref fCit);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void ConsultaUnaOrdenServicio(ref Pedido pedido, string Conexion)
        {
            try
            {
                CD_ServiciosValor claseCapaDatos = new CD_ServiciosValor();
                claseCapaDatos.ConsultaUnaOrdenServicio(ref pedido, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void ConsultaUnaOrdenServicioCita(int iCt, ref Pedido pedido, string Conexion)
        {
            try
            {
                CD_ServiciosValor claseCapaDatos = new CD_ServiciosValor();
                claseCapaDatos.ConsultaUnaOrdenServicioCita(iCt, ref pedido, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaDetalleOrden(int iOrden, ref DataTable dt, string Conexion)
        {
            try
            {
                CD_ServiciosValor claseCapaDatos = new CD_ServiciosValor();
                claseCapaDatos.ConsultaDetalleOrden(iOrden, Conexion, ref dt);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void ConsultaDetalleOrdenCorrectivo(int iOrden, ref DataTable dt, string Conexion)
        {
            try
            {
                CD_ServiciosValor claseCapaDatos = new CD_ServiciosValor();
                claseCapaDatos.ConsultaDetalleOrdenCorrectivo(iOrden, Conexion, ref dt);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ObtieneInsumosAplicacionServicio(string servcicio, string segmento, string aplica, string tipoapp, ref DataTable dt4, string Conexion)
        {
            try
            {
                CD_ServiciosValor claseCapaDatos = new CD_ServiciosValor();
                claseCapaDatos.ObtieneInsumosAplicacionServicio(servcicio, segmento, aplica, tipoapp, Conexion, ref dt4);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //-------------------------------------------------------------------------------------------

        public void GeneraRemisionComodato(CapaEntidad.Pedido pedido, string Conexion)
        {
            try
            {
                CD_ServiciosValor claseCapaDatos = new CD_ServiciosValor();
                claseCapaDatos.GeneraRemisionComodato(pedido, Conexion);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //-------------------------------------------------------------------------------------------

        public void ListaSucursales(string Conexion, ref List<Embudo> List)
        {
            try
            {
                CD_ServiciosValor claseCapaDatos = new CD_ServiciosValor();
                claseCapaDatos.ListaSucursales(Conexion, ref List);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
