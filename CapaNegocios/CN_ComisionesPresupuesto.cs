using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using CapaDatos;

namespace CapaNegocios
{
    public class CN_ComisionesPresupuesto
    {
        //public void Lista(PedidoVtaInst pedido, string Conexion, ref List<PedidoVtaInst> List)
        //{
        //    try
        //    {
        //        CD_CapPedidoVtaInst claseCapaDatos = new CD_CapPedidoVtaInst();
        //        claseCapaDatos.Lista(pedido, Conexion, ref List);

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public void Consultar(ref PedidoVtaInst pedido, string Conexion, ref int verificador, ref Clientes cc)
        //{
        //    try
        //    {
        //        CD_CapPedidoVtaInst claseCapaDatos = new CD_CapPedidoVtaInst();
        //        claseCapaDatos.Consultar(ref pedido, Conexion, ref verificador, ref cc);

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public void ConsultarDet(PedidoVtaInst pedido, ref System.Data.DataTable dt, string Conexion)
        //{
        //    try
        //    {
        //        CD_CapPedidoVtaInst claseCapaDatos = new CD_CapPedidoVtaInst();
        //        claseCapaDatos.ConsultarDet(pedido, Conexion, ref dt);

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public void ConsultarPedidoExistente(PedidoVtaInst pvi, Int64 Id_Prd, string Conexion, ref int verificador)
        //{
        //    try
        //    {
        //        CD_CapPedidoVtaInst claseCapaDatos = new CD_CapPedidoVtaInst();
        //        claseCapaDatos.ConsultarPedidoExistente(pvi, Id_Prd, Conexion, ref verificador);

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}


        public int CapComisionesPresupuesto_InsertOrUpdate(string Conexion, ComisionesPresupuesto comisionpresupuesto)
        {
            try
            {
                int verificador = 0;
                CD_ComisionesPresupuesto claseCapaDatos = new CD_ComisionesPresupuesto();
                claseCapaDatos.CapComisionesPresupuesto_Insertar(Conexion, comisionpresupuesto, ref verificador);
                return verificador;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void CapComisionesPresupuesto_List(ComisionesPresupuesto presupuesto, string Conexion, ref List<ComisionesPresupuesto> list)
        {
            try
            {
                new CD_ComisionesPresupuesto().CapComisionesPresupuesto_List(presupuesto, Conexion, ref list);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CapComisionesPresupuestoVertical_List(ComisionesPresupuesto presupuesto, string Conexion, ref List<ComisionesPresupuesto> list)
        {
            try
            {
                new CD_ComisionesPresupuesto().CapComisionesPresupuestoVertical_List(presupuesto, Conexion, ref list);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void CapComisionesPresupuestoPorRik(ComisionesPresupuesto presupuesto, string Conexion, ref List<ComisionesPresupuesto> list)
        {
            try
            {
                new CD_ComisionesPresupuesto().CapComisionesPresupuestoPorRik(presupuesto, Conexion, ref list);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CapComisionesPresupuesto_UPReal(ComisionesPresupuesto presupuesto, string Conexion, ref List<ComisionesPresupuesto> list)
        {
            try
            {
                new CD_ComisionesPresupuesto().CapComisionesPresupuesto_UPReal(presupuesto, Conexion, ref list);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}