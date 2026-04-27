
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using CapaDatos;
using System.Data;
using System.Collections;

namespace CapaNegocios
{
    public class CN_CapPedidoVtaInst
    {
        public void Lista(PedidoVtaInst pedido, string Conexion, ref List<PedidoVtaInst> List)
        {
            try
            {
                CD_CapPedidoVtaInst claseCapaDatos = new CD_CapPedidoVtaInst();
                claseCapaDatos.Lista(pedido, Conexion, ref List);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void Cliente_credito(ref PedidoVtaInst pedido, string Conexion)
        {
            try
            {
                CD_CapPedidoVtaInst claseCapaDatos = new CD_CapPedidoVtaInst();
                claseCapaDatos.Cliente_credito(ref pedido, Conexion);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Lista(PedidoVtaInst pedido, string Conexion, ref List<PedidoVtaInst> List, string modalidadVenta)
        {
            try
            {
                CD_CapPedidoVtaInst claseCapaDatos = new CD_CapPedidoVtaInst();
                claseCapaDatos.Lista(pedido, Conexion, ref List, modalidadVenta);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ListaFacturacion(PedidoVtaInst pedido, string Conexion, ref List<PedidoVtaInst> List)
        {
            try
            {
                CD_CapPedidoVtaInst claseCapaDatos = new CD_CapPedidoVtaInst();
                claseCapaDatos.ListaFacturacion(pedido, Conexion, ref List);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void Consultar(ref PedidoVtaInst pedido, string Conexion, ref int verificador, ref Clientes cc)
        {
            try
            {
                CD_CapPedidoVtaInst claseCapaDatos = new CD_CapPedidoVtaInst();
                claseCapaDatos.Consultar(ref pedido, Conexion, ref verificador, ref cc);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public void ConsultarDet(PedidoVtaInst pedido, ref List<PedidoVtaInst> List, ref System.Data.DataTable dt, string Conexion, int? idTG)
        {
            try
            {
                CD_CapPedidoVtaInst claseCapaDatos = new CD_CapPedidoVtaInst();
                claseCapaDatos.ConsultarDet(pedido, ref List, Conexion, ref dt, idTG);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void ConsultarDet_Resto(PedidoVtaInst pedido, ref List<PedidoVtaInst> List, ref System.Data.DataTable dt, string Conexion)
        {
            try
            {
                CD_CapPedidoVtaInst claseCapaDatos = new CD_CapPedidoVtaInst();
                claseCapaDatos.ConsultarDet_Resto(pedido, ref List, Conexion, ref dt);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void ConsultarDetadmin(PedidoVtaInst pedido, ref List<PedidoVtaInst> List, ref System.Data.DataTable dt, string Conexion, int? idTG)
        {
            try
            {
                CD_CapPedidoVtaInst claseCapaDatos = new CD_CapPedidoVtaInst();
                claseCapaDatos.ConsultarDetadmin(pedido, ref List, Conexion, ref dt, idTG);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void ConsultarDet_Restoadmin(PedidoVtaInst pedido, ref List<PedidoVtaInst> List, ref System.Data.DataTable dt, string Conexion)
        {
            try
            {
                CD_CapPedidoVtaInst claseCapaDatos = new CD_CapPedidoVtaInst();
                claseCapaDatos.ConsultarDet_Restoadmin(pedido, ref List, Conexion, ref dt);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        /// <summary>
        /// Consulta el detalle del resto de productos de un pedido en captura de garantía.
        /// </summary>
        /// <param name="pedido"></param>
        /// <param name="List"></param>
        /// <param name="Conexion"></param>
        /// <param name="dt"></param>
        /// <param name="Id_TG">Identificador del tipo de garantía</param>
        public void ConsultarDet_Resto(PedidoVtaInst pedido, ref List<PedidoVtaInst> List, ref System.Data.DataTable dt, string Conexion, int Id_TG)
        {
            try
            {
                CD_CapPedidoVtaInst claseCapaDatos = new CD_CapPedidoVtaInst();
                claseCapaDatos.ConsultarDet_Resto(pedido, ref List, Conexion, ref dt, Id_TG);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultarDet_RestoDetalle(PedidoVtaInst pedido, ref List<PedidoVtaInst> List, ref System.Data.DataTable dt, string Conexion, int Id_TG)
        {
            try
            {
                CD_CapPedidoVtaInst claseCapaDatos = new CD_CapPedidoVtaInst();
                claseCapaDatos.ConsultarDet_RestoDetalle(pedido, ref List, Conexion, ref dt, Id_TG);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }




        public void ConsultarDetAcys(PedidoVtaInst pedido, ref List<PedidoVtaInst> List, string Conexion)
        {
            try
            {
                CD_CapPedidoVtaInst cd_pv = new CD_CapPedidoVtaInst();
                cd_pv.ConsultarDetAcys(pedido, ref List, Conexion);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void ConsultarPedidoExistente(PedidoVtaInst pvi, long Id_Prd, string Conexion, ref int verificador)
        {
            try
            {
                CD_CapPedidoVtaInst claseCapaDatos = new CD_CapPedidoVtaInst();
                claseCapaDatos.ConsultarPedidoExistente(pvi, Id_Prd, Conexion, ref verificador);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaProductoSustituto(PedidoVtaInst Registro, ref List<PedidoVtaInst> Lista, string conexion)
        {
            try
            {
                CD_CapPedidoVtaInst claseCapaDatos = new CD_CapPedidoVtaInst();
                claseCapaDatos.ConsultaProductoSustituto(Registro, ref Lista, conexion);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Insertar(PedidoVtaInst pedido, int tipoPedido, DataTable dt, string Conexion, ref int verificador, int? idTG, int? Id_AcsVersion)
        {
            try
            {
                CD_CapPedidoVtaInst claseCapaDatos = new CD_CapPedidoVtaInst();
                claseCapaDatos.Insertar(pedido, tipoPedido, dt, Conexion, ref verificador, idTG, Id_AcsVersion);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Insertar(PedidoVtaInst pedido, DataTable dt, string Conexion, ref int verificador, int? idTG, int? Id_AcsVersion)
        {
            try
            {
                CD_CapPedidoVtaInst claseCapaDatos = new CD_CapPedidoVtaInst();
                claseCapaDatos.Insertar(pedido, dt, Conexion, ref verificador, idTG, Id_AcsVersion);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public void InsertarOrdenCompraCentral(int Id_U, int Id_Cd, PedidoVtaInst pedido, DataTable dt, string Conexion, ref int verificador, int? idTG, int? Id_AcsVersion)
        {
            try
            {
                CD_CapPedidoVtaInst claseCapaDatos = new CD_CapPedidoVtaInst();
                claseCapaDatos.InsertarOrdenCompraCentral(Id_U, Id_Cd, pedido, dt, Conexion, ref verificador, idTG, Id_AcsVersion);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void InsertarOrdenCompraCentralSolicitud(int Id_U, int Id_Cd, PedidoVtaInst pedido, DataTable dt, string Conexion, ref int verificador, int? idTG, int? Id_AcsVersion)
        {
            try
            {
                CD_CapPedidoVtaInst claseCapaDatos = new CD_CapPedidoVtaInst();
                claseCapaDatos.InsertarOrdenCompraCentralSolicitud(Id_U, Id_Cd, pedido, dt, Conexion, ref verificador, idTG, Id_AcsVersion);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void ConsultaSolicitud(PedidoVtaInst pedido, string Conexion, ref List<PedidoVtaInst> Solicitud)
        {
            try
            {
                CD_CapPedidoVtaInst claseCapaDatos = new CD_CapPedidoVtaInst();
                claseCapaDatos.ConsultaSolicitud(pedido, Conexion, ref Solicitud);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ActualizarSolicitud(PedidoVtaInst pedido, string Conexion, ref int verificador)
        {
            try
            {
                CD_CapPedidoVtaInst claseCapaDatos = new CD_CapPedidoVtaInst();
                claseCapaDatos.ActualizarSolicitud(pedido, Conexion, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void ConsultaSolicitudDet(PedidoDet pedido, string Conexion, ref List<PedidoDet> ListaPed)
        {
            try
            {
                CD_CapPedidoVtaInst claseCapaDatos = new CD_CapPedidoVtaInst();
                claseCapaDatos.ConsultaSolicitudDet(pedido, Conexion, ref ListaPed);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void Cancelar(PedidoVtaInst pedido, string Conexion, ref int verificador)
        {
            try
            {
                CD_CapPedidoVtaInst claseCapaDatos = new CD_CapPedidoVtaInst();
                claseCapaDatos.Cancelar(pedido, Conexion, ref verificador);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void RechazarPedidoVI(PedidoVtaInst pedido, string Conexion, ref int verificador)
        {
            try
            {
                CD_CapPedidoVtaInst claseCapaDatos = new CD_CapPedidoVtaInst();
                claseCapaDatos.RechazarPedidoVI(pedido, Conexion, ref verificador);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Modificar(PedidoVtaInst pedido, DataTable dt, DataTable dtTemp, string Conexion, int captado, ref int verificador, ArrayList eliminados, int? Id_TG)
        {
            try
            {
                CD_CapPedidoVtaInst claseCapaDatos = new CD_CapPedidoVtaInst();
                claseCapaDatos.Modificar(pedido, dt, dtTemp, Conexion, captado, ref verificador, eliminados, Id_TG);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ModificarCN(int id_u, int ID_cd, PedidoVtaInst pedido, DataTable dt, string Conexion, int captado, ref int verificador, ArrayList eliminados, int? Id_TG)
        {
            try
            {
                CD_CapPedidoVtaInst claseCapaDatos = new CD_CapPedidoVtaInst();
                claseCapaDatos.ModificarCN(id_u, ID_cd, pedido, dt, Conexion, captado, ref verificador, eliminados, Id_TG);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Modificar(PedidoVtaInst pedido, DataTable dt, string Conexion, int captado, ref int verificador, ArrayList eliminados)
        {
            try
            {
                CD_CapPedidoVtaInst claseCapaDatos = new CD_CapPedidoVtaInst();
                claseCapaDatos.Modificar(pedido, dt, Conexion, captado, ref verificador, eliminados);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public void ConsultarAAAEspecial(int Id_Emp, int Id_Cd, double Id_Cte, string Id_prd, ref int verificador, string Conexion)
        {
            try
            {
                CD_CapPedidoVtaInst claseCapaDatos = new CD_CapPedidoVtaInst();
                claseCapaDatos.ConsultarAAAEspecial(Id_Emp, Id_Cd, Id_Cte, Id_prd, Conexion, ref verificador);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultarDet_Resto2(PedidoVtaInst pedido, ref List<PedidoVtaInst> List, ref System.Data.DataTable dt, string Conexion)
        {
            try
            {
                CD_CapPedidoVtaInst claseCapaDatos = new CD_CapPedidoVtaInst();
                claseCapaDatos.ConsultarDet_Resto2(pedido, ref List, Conexion, ref dt);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaClienteAcysCombo(PedidoVtaInst pedido, ref List<PedidoVtaInst> List, string Conexion)
        {
            try
            {
                CD_CapPedidoVtaInst claseCapaDatos = new CD_CapPedidoVtaInst();
                claseCapaDatos.ConsultaClienteAcysCombo(pedido, ref List, Conexion);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultarAgendaCliente(AgendaRsc Agenda, ref List<Cliente> Lista, string Conexion)
        {
            try
            {
                CD_CapPedidoVtaInst claseCapaDatos = new CD_CapPedidoVtaInst();
                claseCapaDatos.ConsultarAgendaCliente(Agenda, ref Lista, Conexion);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaClienteAcys(PedidoVtaInst pedido, ref List<PedidoVtaInst> List, string Conexion)
        {
            try
            {
                CD_CapPedidoVtaInst claseCapaDatos = new CD_CapPedidoVtaInst();
                claseCapaDatos.ConsultaClienteAcys(pedido, ref List, Conexion);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Consultar2(ref PedidoVtaInst pedido, string Conexion, ref int verificador, ref Clientes cc)
        {
            try
            {
                CD_CapPedidoVtaInst claseCapaDatos = new CD_CapPedidoVtaInst();
                claseCapaDatos.Consultar2(ref pedido, Conexion, ref verificador, ref cc);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public void ConsultaPedidoRastreo(PedidoVtaInst pedido, string Conexion, ref List<PedidoVtaInst> List)
        {
            try
            {
                CD_CapPedidoVtaInst claseCapaDatos = new CD_CapPedidoVtaInst();
                claseCapaDatos.ConsultaPedidoRastreo(pedido, Conexion, ref List);

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void RechazarPedidoVI2(PedidoVtaInst pedido, string Conexion, ref int verificador)
        {
            try
            {
                CD_CapPedidoVtaInst claseCapaDatos = new CD_CapPedidoVtaInst();
                claseCapaDatos.RechazarPedidoVI2(pedido, Conexion, ref verificador);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Cancelar2(PedidoVtaInst pedido, string Conexion, ref int verificador)
        {
            try
            {
                CD_CapPedidoVtaInst claseCapaDatos = new CD_CapPedidoVtaInst();
                claseCapaDatos.Cancelar2(pedido, Conexion, ref verificador);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AgregarProductoAcys(PedidoVtaInst pedido, DataTable dt, string Conexion, ref string verificador)
        {
            try
            {
                CD_CapPedidoVtaInst claseCapaDatos = new CD_CapPedidoVtaInst();
                claseCapaDatos.AgregarProductoAcys(pedido, dt, Conexion, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultarDet2(PedidoVtaInst pedido, ref List<PedidoVtaInst> List, ref System.Data.DataTable dt, string Conexion, int? idTG)
        {
            try
            {
                CD_CapPedidoVtaInst claseCapaDatos = new CD_CapPedidoVtaInst();
                claseCapaDatos.ConsultarDet2(pedido, ref List, Conexion, ref dt, idTG);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void guardarInformacionPedidosSinAcys(PedidoVtaInst pedido, string Conexion, ref int Cliente, ref int Usuario)
        {
            try
            {
                CD_CapPedidoVtaInst claseCapaDatos = new CD_CapPedidoVtaInst();
                claseCapaDatos.guardarInformacionPedidosSinAcys(pedido, Conexion, ref Cliente, ref Usuario);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Lista2(PedidoVtaInst pedido, string Conexion, ref List<PedidoVtaInst> List, string modalidadVenta)
        {
            try
            {
                CD_CapPedidoVtaInst claseCapaDatos = new CD_CapPedidoVtaInst();
                claseCapaDatos.Lista2(pedido, Conexion, ref List, modalidadVenta);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void ListaOrderCompra(PedidoVtaInst pedido, string Conexion, ref List<PedidoVtaInst> List, string modalidadVenta)
        {
            try
            {
                CD_CapPedidoVtaInst claseCapaDatos = new CD_CapPedidoVtaInst();
                claseCapaDatos.ListaOrderCompra(pedido, Conexion, ref List, modalidadVenta);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public void ConsultarPedido(PedidoVtaInst pedido, string Conexion, ref List<PedidoVtaInst> List, string modalidadVenta)
        {
            try
            {
                CD_CapPedidoVtaInst claseCapaDatos = new CD_CapPedidoVtaInst();
                claseCapaDatos.ConsultarPedido(pedido, Conexion, ref List, modalidadVenta);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void Lista_acysPendiente(PedidoVtaInst pedido, string Conexion, ref List<PedidoVtaInst> List)
        {
            try
            {
                CD_CapPedidoVtaInst claseCapaDatos = new CD_CapPedidoVtaInst();
                claseCapaDatos.Lista_acysPendiente(pedido, Conexion, ref List);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Insertar(PedidoVtaInst pedido, DataTable dt, DataTable dtRestos, string Conexion, ref int verificador, int? idTG, int? Id_AcsVersion)
        {
            try
            {
                CD_CapPedidoVtaInst claseCapaDatos = new CD_CapPedidoVtaInst();
                claseCapaDatos.Insertar(pedido, dt, dtRestos, Conexion, ref verificador, idTG, Id_AcsVersion);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertarOrderCompra(PedidoVtaInst pedido, string Conexion, ref int verificador, int? idTG, int? idAcsVersion)
        {
            try
            {
                CD_CapPedidoVtaInst claseCapaDatos = new CD_CapPedidoVtaInst();
                claseCapaDatos.InsertarOrderCompra(pedido, Conexion, ref verificador, idTG, idAcsVersion);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CoonsultarOrdenCompra(PedidoVtaInst pedido, ref List<PedidoVtaInst> pedidoDescarga, string Conexion)
        {
            try
            {
                CD_CapPedidoVtaInst claseCapaDatos = new CD_CapPedidoVtaInst();
                claseCapaDatos.CoonsultarOrdenCompra(pedido, ref pedidoDescarga, Conexion);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void Modificar(PedidoVtaInst pedido, DataTable dt, DataTable dtRestos, DataTable dtNuevaLista, string Conexion, int captado, ref int verificador, ArrayList eliminados, int? idTG)
        {
            try
            {
                CD_CapPedidoVtaInst claseCapaDatos = new CD_CapPedidoVtaInst();
                claseCapaDatos.Modificar(pedido, dt, dtRestos, Conexion, captado, ref verificador, eliminados, idTG);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public void Actualizartokenportalcliente(ref string token, int tipo, string Conexion)
        {
            try
            {
                CD_CapPedidoVtaInst claseCapaDatos = new CD_CapPedidoVtaInst();
                claseCapaDatos.Actualizartokenportalcliente(ref token, tipo, Conexion);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public void logPortalCliente(int id_Cd, int PedidoExterno, string TipoAPi, int Estatus, string mensaje, ref int verificador, string Conexion)
        {
            try
            {
                CD_CapPedidoVtaInst claseCapaDatos = new CD_CapPedidoVtaInst();
                claseCapaDatos.logPortalCliente(id_Cd, PedidoExterno, TipoAPi, Estatus, mensaje, ref verificador, Conexion);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void ConsutarlogPortalClienteRemision(int id_Cd, int PedidoExterno, string TipoAPi, int estatus, ref int verificador, string Conexion)
        {
            try
            {
                CD_CapPedidoVtaInst claseCapaDatos = new CD_CapPedidoVtaInst();
                claseCapaDatos.ConsutarlogPortalClienteRemision(id_Cd, PedidoExterno, TipoAPi, estatus, ref verificador, Conexion);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void ReporteCaptacionPedido(PedidoVtaInst pedido, string Conexion, ref List<PedidoVtaInst> List)
        {
            try
            {
                CD_CapPedidoVtaInst claseCapaDatos = new CD_CapPedidoVtaInst();
                claseCapaDatos.ReporteCaptacionPedido(pedido, Conexion, ref List);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaSolicitudPedidos(PedidoVtaInst pedido, string Conexion, ref List<PedidoVtaInst> Solicitud)
        {
            try
            {
                CD_CapPedidoVtaInst claseCapaDatos = new CD_CapPedidoVtaInst();
                claseCapaDatos.ConsultaSolicitudPedidos(pedido, Conexion, ref Solicitud);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ActualizarSolicitudPedido(PedidoVtaInst pedido, string Conexion, ref int verificador)
        {
            try
            {
                CD_CapPedidoVtaInst claseCapaDatos = new CD_CapPedidoVtaInst();
                claseCapaDatos.ActualizarSolicitudPedido(pedido, Conexion, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultarLogHistPedido(PedidoVtaInst pedido, ref List<PedidoVtaInst> List, string Conexion)
        {
            try
            {
                CD_CapPedidoVtaInst claseCapaDatos = new CD_CapPedidoVtaInst();
                claseCapaDatos.ConsultarLogHistPedido(pedido, ref List, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void guardarInformacionSolicitudPedidosSinAcys(PedidoVtaInst pedido, string Conexion, ref int Cliente, ref int Usuario)
        {
            try
            {
                CD_CapPedidoVtaInst claseCapaDatos = new CD_CapPedidoVtaInst();
                claseCapaDatos.guardarInformacionPedidosSinAcys(pedido, Conexion, ref Cliente, ref Usuario);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void InsertarAutorizacion(PedidoVtaInst pedido, DataTable dt, DataTable dtRestos, string Conexion, ref int verificador, int? idTG, int? Id_AcsVersion)
        {
            try
            {
                CD_CapPedidoVtaInst claseCapaDatos = new CD_CapPedidoVtaInst();
                claseCapaDatos.InsertarAutorizacion(pedido, dt, dtRestos, Conexion, ref verificador, idTG, Id_AcsVersion);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertarSolicitudOrderCompra(PedidoVtaInst pedido, string Conexion, ref int verificador, int? idTG, int? idAcsVersion)
        {
            try
            {
                CD_CapPedidoVtaInst claseCapaDatos = new CD_CapPedidoVtaInst();
                claseCapaDatos.InsertarSolicitudOrderCompra(pedido, Conexion, ref verificador, idTG, idAcsVersion);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void ConsultaPedidosAutorizados(PedidoVtaInst pedido, string Conexion, ref int Verificador)
        {
            try
            {
                CD_CapPedidoVtaInst claseCapaDatos = new CD_CapPedidoVtaInst();
                claseCapaDatos.ConsultaPedidosAutorizados(pedido, Conexion, ref Verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}