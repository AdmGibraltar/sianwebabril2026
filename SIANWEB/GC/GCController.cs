using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using CapaEntidad;
using CapaNegocios;
using System.Text;
using System.Net.Mail;
using System.Net;
using System.Net.Mime;

// 8MAR2021 RFH 
// 11Mar2022 RFH Actualizacion

namespace SIANWEB.WebService
{
    public class GCController : ApiController
    {
        // ABR28-2021
        [HttpGet]
        public eResponse<List<eTerritorio>> spCatTerritorio_PorRik_Ver2(
           int Anio, int Mes, int Id_Rik, int Par1, int Par2, int Par3)
        {
            eResponse<List<eTerritorio>> result = new eResponse<List<eTerritorio>>();
            List<eTerritorio> lst = new List<eTerritorio>();
            result.Estado = 0;
            try
            {
                CN_CatTerritorios cnCatTerritorios = new CN_CatTerritorios();
                lst = cnCatTerritorios.spCatTerritorio_PorRik_Ver2(Anio, Mes, Sesion.Id_Emp, Sesion.Id_Cd, Id_Rik, Sesion);
                result.Datos = lst;
                result.Estado = 1;
            }
            catch (Exception ex)
            {
                result.Datos = null;
                result.Estado = -1;
            }
            return result;
        }

        // Representantes Activos en El Periodo        
        [HttpGet]
        public eResponse<List<eUsuarioRik>> spCatRik_ComboTodos_ver3(int Anio, int Mes, int rol, int Id_Param5)
        {
            eResponse<List<eUsuarioRik>> result = new eResponse<List<eUsuarioRik>>();
            List<eUsuarioRik> Lst = new List<eUsuarioRik>();
            try
            {
                CN_CatRepresentantes CR = new CN_CatRepresentantes();
                // Segun el tipo de Usuario 
                Lst = CR.spCatRik_ComboTodos_ver3(
                    Sesion.Id_Emp, Sesion.Id_Cd, Sesion.Id_U, Anio, Mes, rol, Sesion.Emp_Cnx);

                result.Datos = Lst;
                result.Estado = 1;
            }
            catch (Exception ex)
            {
                result.Estado = -1;
                result.Mensaje = "Error";
            }
            return result;
        }

        // 04MAR-2021
        /*
        [HttpGet]
        public eResponse<List<eClienteBuscar>> spGC_CatCiente_Listar(
            int PageNumber, int PageSize, 
            int Anio, int Mes, int Id_Emp, int Id_Cd, int Id_Rik, string TextoBuscar)
        {
            eResponse<List<eClienteBuscar>> result = new eResponse<List<eClienteBuscar>>();
            result.Estado = 0;
            List<eClienteBuscar> lst = new List<eClienteBuscar>();
            try
            {
                CN_CatCliente CC = new CN_CatCliente();
                if (String.IsNullOrEmpty(TextoBuscar))
                {
                    TextoBuscar = "";
                }
                lst = CC.spGC_CatCiente_Listar(PageNumber, PageSize, 
                    Anio, Mes, Id_Emp, Id_Cd, Sesion.Id_U, Id_Rik, TextoBuscar, Sesion.Emp_Cnx);
                result.Estado = 1;
                result.Datos = lst;
            }
            catch (Exception ex)
            {
                result.Estado = 0;
                result.Datos = null;
            }
            return result;
        }
        */

        [HttpGet]
        public eResponse<List<eClienteBuscar>> spRepCumplimientoVI_Dinamico(
           //int PageNumber, int PageSize,
           int Anio, int Mes,
           int Id_Uen, int Id_Seg, int Id_Rik, int Id_Ter, int Id_Cte, int Tipo, string TextoBuscar,
           int CampoOrden, int OrdenDir)
        {
            eResponse<List<eClienteBuscar>> result = new eResponse<List<eClienteBuscar>>();
            result.Estado = 0;

            List<eClienteBuscar> lst = new List<eClienteBuscar>();
            try
            {
                CN_CatCliente CC = new CN_CatCliente();
                eClenteBuscar_Params Pms = new eClenteBuscar_Params();

                if (String.IsNullOrEmpty(TextoBuscar))
                {
                    TextoBuscar = "";
                }
                Pms.TextoBuscar = TextoBuscar;
                Pms.PageNumber = 0;
                Pms.PageSize = 0;
                Pms.Id_Emp = Sesion.Id_Emp;
                Pms.Id_Cd = Sesion.Id_Cd;
                Pms.Anio = Anio;
                Pms.Mes = Mes;
                Pms.Id_Uen = Id_Uen;
                Pms.Id_Seg = Id_Seg;
                Pms.Id_Rik = Id_Rik;
                Pms.Id_Ter = Id_Ter;
                Pms.Tipo = Tipo;
                Pms.Conexion = Sesion.Emp_Cnx;
                Pms.Id_Cte = Id_Cte;
                Pms.CampoOrden = CampoOrden;
                Pms.OrdenDir = OrdenDir;

                lst = CC.spRepCumplimientoVI_Dinamico(Pms);

                result.Estado = 1;
                result.Datos = lst;
            }
            catch (Exception ex)
            {
                result.Estado = 0;
                result.Datos = null;
            }
            return result;
        }

        [HttpGet]
        public eResponse<List<eClienteBuscar>> spRepCumplimientoVI_ver3(
           //int PageNumber, int PageSize,
           int Anio, int Mes,
           int Id_Uen, int Id_Seg, int Id_Rik, int Id_Ter, int Id_Cte, string NombreCliente, int Tipo, string TextoBuscar,
           int CampoOrden, int OrdenDir, int TipoCliente, int rol,
           int Param12)
        {
            eResponse<List<eClienteBuscar>> result = new eResponse<List<eClienteBuscar>>();
            result.Estado = 0;
            List<eClienteBuscar> lst = new List<eClienteBuscar>();
            try
            {
                CN_CatCliente CC = new CN_CatCliente();
                eClenteBuscar_Params Pms = new eClenteBuscar_Params();
                if (String.IsNullOrEmpty(TextoBuscar))
                {
                    TextoBuscar = "";
                }
                Pms.TextoBuscar = TextoBuscar;
                Pms.PageNumber = 0;
                Pms.PageSize = 0;
                Pms.Id_Emp = Sesion.Id_Emp;
                Pms.Id_Cd = Sesion.Id_Cd;
                Pms.Anio = Anio;
                Pms.Mes = Mes;
                Pms.Id_Uen = Id_Uen;
                Pms.Id_Seg = Id_Seg;
                Pms.Id_Rik = Id_Rik;
                Pms.Id_Ter = Id_Ter;
                Pms.Tipo = Tipo;
                Pms.Conexion = Sesion.Emp_Cnx;
                Pms.Id_Cte = Id_Cte;
                Pms.NombreCliente = NombreCliente;
                Pms.CampoOrden = CampoOrden;
                Pms.OrdenDir = OrdenDir;
                Pms.tipoCliente = TipoCliente;
                Pms.TipoRep = rol;
                lst = CC.spRepCumplimientoVI_ver3(Pms);
                result.Estado = 1;
                result.Datos = lst;
            }
            catch (Exception ex)
            {
                result.Estado = 0;
                result.Datos = null;
            }
            return result;
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


    }
}