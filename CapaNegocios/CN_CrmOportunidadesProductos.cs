using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaModelo;
using CapaDatos;
using CapaEntidad;
using System.Data;

namespace CapaNegocios
{
    public class CN_CrmOportunidadesProductos
    {

        public int Update_CrmOportunidadesProductos(
            int Id_Emp, int Id_Cd,
            int Id_Op, int Id_Val, int Id_Cte, long Id_Prd, decimal Cantidad,
            int AplDilucion, decimal DilucionA, decimal DilucionC,
            string CPT_ProductoActual, string CPT_SituacionActual, string CPT_VentajasKey,
            string CPT_RecursoImagenProductoActual, string CPT_RecursoImagensolucionKey,
            decimal COP_CostoEnUso,
            string Conexion)
        {

            CD_CrmOportunidadesProductos cdOP = new CD_CrmOportunidadesProductos();

            return cdOP.Update_CrmOportunidadesProductos(
                Id_Emp, Id_Cd, Id_Op, Id_Val, Id_Cte, Id_Prd, Cantidad,
                AplDilucion, DilucionA, DilucionC,
                CPT_ProductoActual, CPT_SituacionActual, CPT_VentajasKey,
                CPT_RecursoImagenProductoActual, CPT_RecursoImagensolucionKey,
                COP_CostoEnUso,
            Conexion);
        }

        // JUN04-2020 RFH 
        // Se actializa la version Arriba 

        public int CrmOportunidadesProductos_InsertUpdate(eCrmOportunidadesProducto P, int Accion, string Conexion)
        {
            CD_CrmOportunidadesProductos CD = new CD_CrmOportunidadesProductos();
            return CD.spCrmOportunidadesProductos_InsertUpdate(P, Accion, Conexion);
        }

        /// <summary>
        /// Obtiene los productos asociados a un proyecto
        /// </summary>
        /// <param name="sesion">Llave de inicio de sesión</param>
        /// <param name="Id_CrmOportunidad">Identificador del proyecto</param>
        /// <param name="Id_Cte">Identificador del cliente</param>
        /// <returns>Conjunto de productos asociados al proyecto especificado</returns>
        public IEnumerable<CrmOportunidadesProducto> ObtenerProductosPorOportunidad(
            Sesion sesion, int Id_CrmOportunidad, int Id_Cte)
        {
            CD_CrmOportunidadesProductos cdCrmOportunidadesProductos = new CD_CrmOportunidadesProductos();
            var result = cdCrmOportunidadesProductos.ConsultarPorOportunidadYCliente(sesion.Id_Emp, sesion.Id_Cd, Id_CrmOportunidad, Id_Cte, sesion.Emp_Cnx_EF);
            return result;
        }
        //
        // 10JUL-2019 RFH  
        // Version 2 de la funcion arriba
        /*
        public List<eCrmOportunidadesProducto> ObtenerProductosPorOportunidad_ver2(
            int Id_Emp, int Id_Cd, int Id_CrmOportunidad, int Id_Cte, string Conexion)
        {
            CD_CrmOportunidadesProductos cdCrmOportunidadesProductos = new CD_CrmOportunidadesProductos();
            var result = cdCrmOportunidadesProductos.ConsultarPorOportunidadYCliente_ver2(
                Id_Emp, Id_Cd, Id_CrmOportunidad, Id_Cte, Conexion);
            return result;
        }
        */

        /// <summary>
        /// Obtiene los productos asociados a un proyecto
        /// </summary>
        /// <param name="sesion">Llave de inicio de sesión</param>
        /// <param name="Id_CrmOportunidad">Identificador del proyecto</param>
        /// <param name="Id_Cte">Identificador del cliente</param>
        /// <param name="ibt">Transacción de la capa de negocio</param>
        /// <returns>IEnumerable[CrmOportunidadesProducto]</returns>
        public IEnumerable<CrmOportunidadesProducto> ObtenerProductosPorOportunidad(
            Sesion sesion, int Id_CrmOportunidad, int Id_Cte, IBusinessTransaction ibt)
        {
            CD_CrmOportunidadesProductos cdCrmOportunidadesProductos = new CD_CrmOportunidadesProductos();
            var result = cdCrmOportunidadesProductos.ConsultarPorOportunidadYCliente(
                sesion.Id_Emp, sesion.Id_Cd, Id_CrmOportunidad, Id_Cte, ibt.DataContext);
            return result;
        }

        //JUN06-2020 RFH

        public List<eCrmOportunidadesProducto> ObtenerProductosPorOportunidad_Ver2(
            int Id_Emp, int Id_Cd, int Id_Op, int Id_Cte, string Conexion)
        {
            CD_CrmOportunidadesProductos CD = new CD_CrmOportunidadesProductos();
            return CD.ConsultarPorOportunidadYCliente_ver2(
                Id_Emp, Id_Cd, Id_Op, Id_Cte, Conexion);
        }

        /// <summary>
        /// Registra una nueva instancia de un producto asociado a un proyecto en la fuente de persistencia.
        /// </summary>
        /// <param name="sesion">Objecto de llave de inicio de sesión</param>
        /// <param name="crmOportunidadesProducto">Información del registro</param>
        /// <returns>CrmOportunidadesProducto. Información del registro</returns>
        public CrmOportunidadesProducto Crear(Sesion sesion, CrmOportunidadesProducto crmOportunidadesProducto, ref int iResult)
        {
            CrmOportunidadesProducto result = null;

            using (IBusinessTransaction ibt = CN_FabricaTransaccionNegocios.Default(sesion))
            {
                ibt.Begin();
                //Se invoca la llamada a la operación para crear la asociación del producto con el proyecto
                CD_CrmOportunidadesProductos cdCrmOportunidadesProductos = new CD_CrmOportunidadesProductos();
                var producto = cdCrmOportunidadesProductos.Insertar(crmOportunidadesProducto, ibt.DataContext, ref iResult);

                // 19 sep 2018 RFH
                // Si se inserta un producto se debe ACTUALIZAR  el estatus de 
                // CapValProyecto 
                // 

                eCapValProyecto ecVP = new eCapValProyecto();
                ecVP.Id_Emp = sesion.Id_Emp;
                ecVP.Id_Cd = sesion.Id_Cd;
                ecVP.Id_Op = crmOportunidadesProducto.Id_Op;
                ecVP.Id_Vap = 0;
                ecVP.Id_Cte = 0;
                ecVP.Estatus = 2;
                ecVP.Vap_Estatus = "A"; // Cambia estatus a incio de captura 
                ecVP.Vap_Estatus2 = 3;
                ecVP.Id_Rik = 0;
                ecVP.Id_Ter = 0;
                ecVP.Vap_Fecha = "";
                ecVP.Id_U = 0;
                ecVP.Vap_Nota = "";
                ecVP.Vap_UtilidadRemanente = 0;
                ecVP.Vap_ValorPresenteNeto = 0;
                ecVP.MotivoParaAutorizacion = "";
                ecVP.Tipo = 2; // Actualiza Solo Estatus

                CN_CapValProyecto cVP = new CN_CapValProyecto();
                int iRes = cVP.CRM2_InsertUpdate_Tipo(ecVP, sesion.Emp_Cnx);

                //Se valida el estado del proyecto
                CN_CrmOportunidad cnCrmOportunidad = new CN_CrmOportunidad();
                var proyecto = cnCrmOportunidad.ObtenerPorId(sesion, crmOportunidadesProducto.Id_Op, ibt);
                CapaNegocios.FlujosDeEstado.CRM.ProyectoStateMachine psm = new FlujosDeEstado.CRM.ProyectoStateMachine(proyecto, sesion);
                psm.Transaction = ibt;

                var productos = cdCrmOportunidadesProductos.ConsultarPorOportunidadYCliente(sesion.Id_Emp, sesion.Id_Cd, crmOportunidadesProducto.Id_Op, crmOportunidadesProducto.Id_Cte, ibt.DataContext);
                proyecto.CrmOportunidadesProducto = productos;
                psm.Update();

                result = producto;

                ibt.Commit();
            }

            return result;
        }

        /// <summary>
        /// Elimina el registro persistente en la fuente de datos.
        /// </summary>
        /// <param name="sesion">Objeto de llave de sesión.</param>
        /// <param name="idEmp">Identificador de la empresa</param>
        /// <param name="idCd">Identificador del centro de distribución</param>
        /// <param name="idCte">Identificador del cliente</param>
        /// <param name="idRik">Identificador del representante</param>
        /// <param name="idOp">Identificador del proyecto</param>
        public void Eliminar(Sesion sesion, int idCte, int idOp, long idPrd)
        {
            CD_CrmOportunidadesProductos cdCrmOportunidadesProductos = new CD_CrmOportunidadesProductos();
            cdCrmOportunidadesProductos.Eliminar(sesion.Id_Emp, sesion.Id_Cd, idCte, sesion.Id_Rik, idOp, idPrd, sesion.Emp_Cnx_EF);

        }

        //
        // 19 Sep 2018 RFH
        // Elimina con SP.
        public int SP_Eliminar(Sesion sesion, int idCte, int idOp, long idPrd)
        {
            CD_CrmOportunidadesProductos cdCrmOportunidadesProductos = new CD_CrmOportunidadesProductos();
            return cdCrmOportunidadesProductos.Sp_Eliminar(sesion.Id_Emp, sesion.Id_Cd, idOp, idCte, sesion.Id_Rik, idPrd, sesion.Emp_Cnx);

        }

        public void Actualizar(Sesion sesion, CrmOportunidadesProducto crmOportunidadesProducto)
        {
            CD_CrmOportunidadesProductos cdCrmOportunidadesProductos = new CD_CrmOportunidadesProductos();
            cdCrmOportunidadesProductos.Actualizar(crmOportunidadesProducto, sesion.Emp_Cnx_EF);
        }

        public void Actualizar(Sesion sesion, IEnumerable<CrmOportunidadesProducto> crmOportunidadesProductos)
        {
            CD_CrmOportunidadesProductos cdCrmOportunidadesProductos = new CD_CrmOportunidadesProductos();
            foreach (var cop in crmOportunidadesProductos)
            {
                cop.Id_Emp = sesion.Id_Emp;
                cop.Id_Cd = sesion.Id_Cd;
            }
            cdCrmOportunidadesProductos.Actualizar(crmOportunidadesProductos, sesion.Emp_Cnx_EF);
        }

        /// <summary>
        /// Actualiza los campos de interés en la edición de la propuesta tecno/económica
        /// </summary>
        /// <param name="sesion">Sesión de usuario en la llamada</param>
        /// <param name="crmOportunidadesProductos">Conjunto de productos asociados a la propuesta a modificar</param>
        public void ActualizarEdicionPropuesta(Sesion sesion, IEnumerable<CrmOportunidadesProducto> crmOportunidadesProductos)
        {
            CD_CrmOportunidadesProductos cdCrmOportunidadesProductos = new CD_CrmOportunidadesProductos();
            foreach (var cop in crmOportunidadesProductos)
            {
                cop.Id_Emp = sesion.Id_Emp;
                cop.Id_Cd = sesion.Id_Cd;
            }
            cdCrmOportunidadesProductos.ActualizarDesdePropuesta(crmOportunidadesProductos, sesion.Emp_Cnx_EF);
        }

        public IEnumerable<CrmOportunidadesProducto> ObtenerPropuestaEconomica(Sesion s, int idVal, int idCte)
        {
            CD_CrmOportunidadesProductos cdCrmOportunidadesProductos = new CD_CrmOportunidadesProductos();
            var productos = cdCrmOportunidadesProductos.ConsultarProductosDePropuesta(s.Id_Emp, s.Id_Cd, s.Id_Rik, idCte, idVal, s.Emp_Cnx_EF);

            return productos;
        }

        public IEnumerable<CrmOportunidadesProducto> ObtenerPropuestaEconomica(Sesion s, int idVal, int idCte, IBusinessTransaction ibt)
        {
            CD_CrmOportunidadesProductos cdCrmOportunidadesProductos = new CD_CrmOportunidadesProductos();
            var productos = cdCrmOportunidadesProductos.ConsultarProductosDePropuesta(s.Id_Emp, s.Id_Cd, s.Id_Rik, idCte, idVal, ibt.DataContext);

            return productos;
        }

    }
}