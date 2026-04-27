using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaModelo;
using CapaDatos;
using CapaEntidad;

namespace CapaNegocios
{
    public class CN_CapAcysDet
    {
        // 28 Nov 2018 RFh OverLoad e la funcion.
        public CN_CapAcysDet()
        {
        }
        public CN_CapAcysDet(IBusinessTransaction ibt)
        {
            _ibt = ibt;
        }

        public IEnumerable<CapAcysDet> AgregarProductosAACYS(IEnumerable<CapAcysDet> productos)
        {
            //Este método solo llama a la operación de datos para agregar las entidades al repositorio de datos.
            //Debiera de correrse una validación para determinar el estado del ACYS, y determinar si es posible
            //este movimiento.
            //Es posible que esta operación deba de disparar de nuevo el flujo de autorización del ACYS.
            CD_CapAcysDet cdCapAcysDet = new CD_CapAcysDet(_ibt.DataContext);
            return cdCapAcysDet.Insertar(productos);
        }

        /// <summary>
        /// Devuelve el conjunto de productos asociados a un ACYS. Se utiliza la última versión del ACYS.
        /// </summary>
        /// <param name="s">Sesión del usuario en operación</param>
        /// <param name="idAcys">Identificador del ACYS</param>
        /// <param name="idCte">Identificador del cliente</param>
        /// <param name="idTerritorio">Identificador del territorio</param>
        /// <param name="ibt">Transacción de la capa de negocio</param>
        /// <returns>IEnumerable[CapAcysDet]</returns>
        public IEnumerable<CapAcysDet> ObtenerProductosDeACYS(Sesion s, int idAcys, int idCte, int idTerritorio, IBusinessTransaction ibt)
        {
            CD_CapAcysDet cdCapAcysDet = new CD_CapAcysDet(_ibt.DataContext);
            return cdCapAcysDet.ConsultarPorAcys(s.Id_Emp, s.Id_Cd, idAcys, idCte, idTerritorio, ibt.DataContext);
        }

        //
        // RFH 06 04 2018 
        // Reemplaza la consulta por EF
        //
        public List<CapaEntidad.eCapAcysDet> Consulta_ProductosDeACYS(int idEmp, int idCd, int idCte, int idAcs, int idTer, Sesion sesion)
        {
            CD_CapAcysDet clsCD = new CD_CapAcysDet(sesion.Emp_Cnx);
            return clsCD.Consulta_ProductosDeACYS(idEmp, idCd, idCte, idAcs, idTer);
        }

        // 18 Oct 2018 RFH
        public int InsertUpdate(int Id_Acs, int Id_Acs_Version, eAcysDet2 Obj, Sesion sesion)
        {
            CD_CapAcysDet cCD = new CD_CapAcysDet(sesion.Emp_Cnx);
            return cCD.InsertUpdate(Id_Acs, Id_Acs_Version, Obj, sesion);
        }

        public int Update_RptVti(int Id_Acs, int Id_Acs_Version, eAcysDet2 Obj, Sesion sesion)
        {
            CD_CapAcysDet cCD = new CD_CapAcysDet(sesion.Emp_Cnx);
            return cCD.Update_RptVti(Id_Acs, Id_Acs_Version, Obj, sesion);
        }

        public int SP_CapAcysDet_Exist(Sesion sesion, int idEmp, int idCd, long idPrd, int idacys)
        {
            CD_CapAcysDet cCD = new CD_CapAcysDet(sesion.Emp_Cnx);
            return cCD.SP_CapAcysDet_Exist(idEmp, idCd, idPrd, idacys, sesion.Emp_Cnx);
        }

        // 11Jun2021 RFH
        public int InsertUpdate_CN(int Id_Acs, int Id_Acs_Version, eAcysDet2 Obj, Sesion sesion)
        {
            CD_CapAcysDet cCD = new CD_CapAcysDet(sesion.Emp_Cnx);
            return cCD.InsertUpdate_CN(Id_Acs, Id_Acs_Version, Obj, sesion);
        }

        public int InsertUpdate_MarcarBorrarTodo(
            int Id_Emp, int Id_Cd, int Id_Acs, int Id_AcsVersion, int Tipo, Sesion sesion, int TipoCuenta)
        {
            CD_CapAcysDet cCD = new CD_CapAcysDet(sesion.Emp_Cnx);
            return cCD.InsertUpdate_MarcarBorrarTodo(
                Id_Emp, Id_Cd, Id_Acs, Id_AcsVersion, Tipo, sesion, TipoCuenta
            );
        }
        private IBusinessTransaction _ibt = null;
    }
}