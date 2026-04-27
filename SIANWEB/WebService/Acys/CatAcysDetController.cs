using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using CapaEntidad;
using CapaNegocios;

namespace SIANWEB.WebService
{
    public class CatAcysDetController : ApiController
    {

        [HttpGet]
        public List<eAcysDet2> Get(int IdAcys, int AcsVersion)
        {
            List<eAcysDet2> Lst = new List<eAcysDet2>();
            try
            {
                string MensajeError = "";
                CN_CapAcys cn_capacys = new CN_CapAcys();
                int TipoCuenta = 0;
                Lst = cn_capacys.ConsultarDet2(Sesion.Id_Emp, Sesion.Id_Cd, IdAcys, AcsVersion, Sesion.Emp_Cnx, TipoCuenta, ref MensajeError);
            }
            catch (Exception ex)
            {
                Lst = null;
            }
            return Lst;
        }

        // 4 Abr 2019 RFH
        // Obtiene el Listado de Productos de ACYS

        [HttpGet]
        public eResponse<List<eAcysDet2>> GetList(int IdAcys, int AcsVersion, int Param1, int TipoCuenta)
        {
            eResponse<List<eAcysDet2>> result = new eResponse<List<eAcysDet2>>();
            result.Mensaje = "Consulta Detalle Productos";
            string ErrorMensaje = "";
            int iResult = 0;
            int iStatus = 0;
            CN_CapAcysDet cn_CAD = new CN_CapAcysDet();

            List<eAcysDet2> Lst = new List<eAcysDet2>();
            try
            {
                CN_CapAcys cn_capacys = new CN_CapAcys();
                Lst = cn_capacys.ConsultarDet2(Sesion.Id_Emp, Sesion.Id_Cd, IdAcys, AcsVersion, Sesion.Emp_Cnx, TipoCuenta, ref ErrorMensaje);

                result.Datos = Lst;
                if (Lst == null)
                {
                    result.Estado = -1;
                    result.Mensaje = ErrorMensaje;
                }
                else
                {
                    result.Estado = 1;
                    result.Mensaje = ErrorMensaje;
                }
            }
            catch (Exception ex)
            {
                result.Estado = -1;
                result.Datos = null;
                //result.Mensaje = ex.InnerException.Message.ToString();
            }
            return result;
        }

        //
        // 17 Oct 2018 RFH 
        // Recibe el listado de producto y los GUARDA o Actualiza        

        // TipoAcys : 1 Local, 2 CC, 3 Cuenta Nacional

        [HttpPut]
        public eResponse<int> InsertUpdate(int Id_Acs, int Id_AcsVersion, int TipoAcys, List<eAcysDet2> lstProductos)
        {
            eResponse<int> result = new eResponse<int>();
            int iResult = 0;
            int iStatus = 0;
            CN_CapAcysDet cn_CAD = new CN_CapAcysDet();

            int TipoCuenta;
            TipoCuenta = TipoAcys;

            try
            {
                //Se modifico para que aplique cualquier tipo de acys por que cuando en el caso de CN y COORDINADA no se grababan en el detalle del acys y no se programaba en el calendario
                //RAGY 08 Febrero 2023
                /*if (TipoAcys == 1) // LOCAL
                {*/
                // Marca todos parra borrar
                iStatus = cn_CAD.InsertUpdate_MarcarBorrarTodo(
                Sesion.Id_Emp, Sesion.Id_Cd, lstProductos[0].Id_Acs, lstProductos[0].Id_AcsVersion, 1, Sesion, TipoCuenta);
                foreach (eAcysDet2 Prd in lstProductos)
                {
                    iStatus = cn_CAD.InsertUpdate(Id_Acs, Id_AcsVersion, Prd, Sesion);
                    if (iStatus <= 0)
                    {
                        // Si hay un error
                        iResult = iStatus;
                    }
                }
                // Si no hubo errores 
                if (iResult == 0)
                {
                    iResult = 1;
                }
                if (iResult == 1)
                {
                    // Elimina los que no se actualizaron 
                    iStatus = cn_CAD.InsertUpdate_MarcarBorrarTodo(
                    Sesion.Id_Emp, Sesion.Id_Cd, lstProductos[0].Id_Acs, lstProductos[0].Id_AcsVersion, 2, Sesion, TipoCuenta);

                    // Reodena el listado
                    iStatus = cn_CAD.InsertUpdate_MarcarBorrarTodo(
                    Sesion.Id_Emp, Sesion.Id_Cd, lstProductos[0].Id_Acs, lstProductos[0].Id_AcsVersion, 3, Sesion, TipoCuenta);
                }
                /* }*/

                int ErrorCount = 0;



                //Se elimino esta seccion por que cuando en el caso de CN y COORDINADA no se grababan en el detalle del acys y no se programaba en el calendario
                //RAGY 08 Febrero 2023
                /* if (TipoAcys == 2 || TipoAcys == 3) // Coordinada - CN
                 {
                     foreach (eAcysDet2 Prd in lstProductos)
                     {
                         iStatus = cn_CAD.InsertUpdate_CN(Id_Acs, Id_AcsVersion, Prd, Sesion);
                         if (iStatus <= 0)
                         {
                             // Si hay un error
                             ErrorCount++;
                         }
                         //Prd.Id_Matriz


                     }
                 }*/

                result.Datos = 0;
                result.Estado = 1;
            }
            catch (Exception ex)
            {
                result.Estado = -1;
                result.Datos = 0;
            }

            return result;
        }


        [HttpPost]
        public eResponse<int> Update_RptVti(int Id_Acs, int Id_AcsVersion, int TipoAcys, List<eAcysDet2> lstProductos)
        {
            eResponse<int> result = new eResponse<int>();
            int iResult = 0;
            int iStatus = 0;
            CN_CapAcysDet cn_CAD = new CN_CapAcysDet();

            int TipoCuenta;
            TipoCuenta = TipoAcys;

            try
            {
                //Se modifico para que aplique cualquier tipo de acys por que cuando en el caso de CN y COORDINADA no se grababan en el detalle del acys y no se programaba en el calendario
                //RAGY 08 Febrero 2023
                /*if (TipoAcys == 1) // LOCAL
                {*/
                // Marca todos parra borrar

                foreach (eAcysDet2 Prd in lstProductos)
                {
                    iStatus = cn_CAD.Update_RptVti(Id_Acs, Id_AcsVersion, Prd, Sesion);
                    if (iStatus <= 0)
                    {
                        // Si hay un error
                        iResult = iStatus;
                    }
                }
                // Si no hubo errores 
                if (iResult == 0)
                {
                    iResult = 1;
                }



                result.Datos = 0;
                result.Estado = 1;
            }
            catch (Exception ex)
            {
                result.Estado = -1;
                result.Datos = 0;
            }

            return result;
        }

        [HttpGet]
        public int SP_CapAcysDet_Exist(int IdAcys, int Id_Prod)
        {
            int respuesta = 0;
            try
            {
                CN_CapAcysDet cn_capacys = new CN_CapAcysDet();

                respuesta = cn_capacys.SP_CapAcysDet_Exist(Sesion, Sesion.Id_Emp, Sesion.Id_Cd, Id_Prod, IdAcys);
            }
            catch (Exception ex)
            {
                respuesta = 0;
            }
            return respuesta;
        }

        protected Sesion Sesion
        {
            get
            {
                if (HttpContext.Current.Session != null)
                {
                    return (Sesion)HttpContext.Current.Session["Sesion" + HttpContext.Current.Session.SessionID];
                }
                return null;
            }
        }
        //
    }
}