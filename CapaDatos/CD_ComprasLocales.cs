using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using CapaEntidad;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class CD_ComprasLocales
    {
        public void ConsultaComprasCombo(string txtsql, string Conexion, ref List<string> List)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand(txtsql, ref dr);

                while (dr.Read())
                {
                    List.Add(string.Format("{0}-{1}", (string)dr.GetValue(dr.GetOrdinal("Id_Prd")), (string)dr.GetValue(dr.GetOrdinal("Producto"))));
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // Compras Locales
        // ENE21-2020 RFH 

        public int CompraLocalPedidosProducto_ChecaDuplicado(long CodigoUsadoProd, int Param2, string Conexion)
        {
            int ok = 0;
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Producto" };
                object[] Valores = { CodigoUsadoProd };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCompraLocalPedidosProducto_ChecaDuplicado", ref dr, Parametros, Valores);
                while (dr.Read())
                {
                    ok = Convert.ToInt32(dr["Duplicado"].ToString());
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                //throw ex;
                ok = -1;
            }
            return ok;
        }

        // Compras Locales
        // ENE21-2020 RFH 

        public string CL_LlenarProdcutosHermanos(long Producto, string Conexion)
        {
            string lst = "";
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Producto" };
                object[] Valores = { Producto };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCompraLocalProductosHermanos_Lista", ref dr, Parametros, Valores);
                while (dr.Read())
                {
                    lst = lst + dr.GetValue(dr.GetOrdinal("Id_Producto")).ToString() + ";";
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                //throw ex;
                lst = null;
            }
            return lst;
        }

        //
        // FEB14-2020
        //

        //public List<CapaEntidad.ComprasLocales> Get_ComprasLocales_Index(
        //    int Id_Emp, int Id_Cd, int PageNo, int PageSize, int Id_Com, int Id_Estatus, string Conexion)
        //{
        //    List<ComprasLocales> lst = new List<ComprasLocales>();

        //    try
        //    {
        //        SqlDataReader dr = null;
        //        CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

        //        string[] Parametros = { "@Id_Emp", "@Id_Cd", "@PageNo", "@PageSize", "@Id_Com", "@Id_Estatus" };
        //        object[] Valores = { Id_Emp, Id_Cd, PageNo, PageSize, Id_Com, Id_Estatus };

        //        SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spProCompraLocal_Consulta_ver2", ref dr, Parametros, Valores);

        //        while (dr.Read())
        //        {
        //            ComprasLocales Obj = new ComprasLocales();
        //            Obj.Id_Cd = Convert.ToInt32(dr[dr.GetOrdinal("Id_Cd")].ToString());
        //            Obj.Cd_Nombre = dr.GetValue(dr.GetOrdinal("Cd_Nombre")).ToString();
        //            Obj.Comp_Solicito = Convert.ToInt32(dr[dr.GetOrdinal("Comp_Solicito")].ToString());
        //            Obj.Id_Comp = Convert.ToInt32(dr[dr.GetOrdinal("Id_Comp")].ToString());
        //            Obj.Comp_FechaSol = dr.GetValue(dr.GetOrdinal("Comp_FechaSol")).ToString();
        //            Obj.Det_FecAut = dr.GetValue(dr.GetOrdinal("Det_FecAut")).ToString();
        //            Obj.U_Nombre = dr.GetValue(dr.GetOrdinal("U_Nombre")).ToString();
        //            Obj.U_Correo = dr.GetValue(dr.GetOrdinal("U_Correo")).ToString();
        //            Obj.Estatus = dr.GetValue(dr.GetOrdinal("Estatus")).ToString();
        //            Obj.Comentarios = dr.GetValue(dr.GetOrdinal("Comentarios")).ToString();
        //            Obj.Vigencia = dr.GetValue(dr.GetOrdinal("Vigencia")).ToString();
        //            Obj.IdTipoSolicitud = Convert.ToInt32(dr[dr.GetOrdinal("IdTipoSolicitud")].ToString());
        //            Obj.TipoSolicitud = dr.GetValue(dr.GetOrdinal("TipoSolicitud")).ToString();

        //            Obj.Autorizados = Convert.ToInt32(dr[dr.GetOrdinal("Autorizados")].ToString());
        //            Obj.Totales = Convert.ToInt32(dr[dr.GetOrdinal("Totales")].ToString());

        //            Obj.Det_Estatus = dr.GetValue(dr.GetOrdinal("Det_Estatus")).ToString();

        //            lst.Add(Obj);
        //        }
        //        CapaDatos.LimpiarSqlcommand(ref sqlcmd);
        //    }
        //    catch (Exception ex)
        //    {
        //        lst = null;
        //    }
        //    return lst;
        //}


        public void ConsultarSolicitudes(CompraLocal cl, string Conexion, ref List<CompraLocal> List)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_Com" };
                object[] Valores = { cl.Id_Emp, cl.Id_Cd, cl.Id_Comp };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spProCompraLocal_Consulta", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    CompraLocal cl1 = new CompraLocal();
                    cl1.Id_Comp = dr.IsDBNull(dr.GetOrdinal("Id_Comp")) ? 0 : dr.GetInt32(dr.GetOrdinal("Id_Comp"));
                    cl1.Cd_Nombre = dr.IsDBNull(dr.GetOrdinal("Cd_Nombre")) ? "" : dr.GetString(dr.GetOrdinal("Cd_Nombre"));
                    cl1.IdTipoSolicitud = dr.IsDBNull(dr.GetOrdinal("IdTipoSolicitud")) ? 0 : dr.GetInt32(dr.GetOrdinal("IdTipoSolicitud")); ;
                    cl1.TipoSolicitud = dr.IsDBNull(dr.GetOrdinal("TipoSolicitud")) ? "" : dr.GetString(dr.GetOrdinal("TipoSolicitud"));
                    cl1.FechaSol = dr.GetDateTime(dr.GetOrdinal("Comp_FechaSol"));
                    cl1.Id_Prd = dr.IsDBNull(dr.GetOrdinal("Id_Prd")) ? 0 : dr.GetInt64(dr.GetOrdinal("Id_Prd"));
                    cl1.Prd_Descripcion = dr.IsDBNull(dr.GetOrdinal("Prd_descripcion")) ? "" : dr.GetString(dr.GetOrdinal("Prd_descripcion"));
                    cl1.Vigencia = dr.GetDateTime(dr.GetOrdinal("Vigencia"));
                    cl1.Solicito_Nombre = dr.IsDBNull(dr.GetOrdinal("U_Nombre")) ? "" : dr.GetString(dr.GetOrdinal("U_Nombre"));
                    cl1.IdAutorizador = dr.IsDBNull(dr.GetOrdinal("IdUsuarioAutorizador")) ? 0 : dr.GetInt32(dr.GetOrdinal("IdUsuarioAutorizador"));
                    cl1.EstatusAut = dr.IsDBNull(dr.GetOrdinal("Estatus")) ? "" : dr.GetString(dr.GetOrdinal("Estatus"));

                    List.Add(cl1);
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaSolicitudCombo(int SolComId, long IdProd, int IdProv, string Conexion, ref List<CompraLocal> List)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@IdSolCom", "@IdProducto", "@IdProveedor" };
                object[] Valores = { SolComId, IdProd, IdProv };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spProCompraLocal_ConsultaMixta", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    CompraLocal cl1 = new CompraLocal();
                    cl1.Id_Comp = dr.IsDBNull(dr.GetOrdinal("Id_Comp")) ? 0 : dr.GetInt32(dr.GetOrdinal("Id_Comp"));
                    cl1.Cd_Nombre = dr.IsDBNull(dr.GetOrdinal("Cd_Nombre")) ? "" : dr.GetString(dr.GetOrdinal("Cd_Nombre"));
                    cl1.Comp_Solicito = dr.IsDBNull(dr.GetOrdinal("Comp_Solicito")) ? 0 : dr.GetInt32(dr.GetOrdinal("Comp_Solicito"));
                    cl1.Solicito_Nombre = dr.IsDBNull(dr.GetOrdinal("U_Nombre")) ? "" : dr.GetString(dr.GetOrdinal("U_Nombre"));
                    cl1.FechaAut = dr.IsDBNull(dr.GetOrdinal("Det_FecAut")) ? "" : dr.GetDateTime(dr.GetOrdinal("Det_FecAut")).ToString("dd/MM/yyyy hh:mm:ss tt").ToUpper();
                    cl1.FechaSol = dr.GetDateTime(dr.GetOrdinal("Comp_FechaSol"));
                    cl1.EstatusAut = dr.IsDBNull(dr.GetOrdinal("Estatus")) ? "" : dr.GetString(dr.GetOrdinal("Estatus"));
                    List.Add(cl1);
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaSolicitudXProveedor(int IdProv, string Conexion, ref List<CompraLocal> List)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@IdProveedor" };
                object[] Valores = { IdProv };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spProCompraLocal_ConsultaxProveedor", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    CompraLocal cl1 = new CompraLocal();
                    cl1.Id_Comp = dr.IsDBNull(dr.GetOrdinal("Id_Comp")) ? 0 : dr.GetInt32(dr.GetOrdinal("Id_Comp"));
                    cl1.Cd_Nombre = dr.IsDBNull(dr.GetOrdinal("Cd_Nombre")) ? "" : dr.GetString(dr.GetOrdinal("Cd_Nombre"));
                    cl1.Comp_Solicito = dr.IsDBNull(dr.GetOrdinal("Comp_Solicito")) ? 0 : dr.GetInt32(dr.GetOrdinal("Comp_Solicito"));
                    cl1.Solicito_Nombre = dr.IsDBNull(dr.GetOrdinal("U_Nombre")) ? "" : dr.GetString(dr.GetOrdinal("U_Nombre"));
                    cl1.FechaAut = dr.IsDBNull(dr.GetOrdinal("Det_FecAut")) ? "" : dr.GetDateTime(dr.GetOrdinal("Det_FecAut")).ToString("dd/MM/yyyy hh:mm:ss tt").ToUpper();
                    cl1.FechaSol = dr.GetDateTime(dr.GetOrdinal("Comp_FechaSol"));
                    cl1.EstatusAut = dr.IsDBNull(dr.GetOrdinal("Estatus")) ? "" : dr.GetString(dr.GetOrdinal("Estatus"));
                    List.Add(cl1);
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaSolicitudXProducto(int IdProd, string Conexion, ref List<CompraLocal> List)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@IdProducto" };
                object[] Valores = { IdProd };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spProCompraLocal_ConsultaxProducto", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    CompraLocal cl1 = new CompraLocal();
                    cl1.Id_Comp = dr.IsDBNull(dr.GetOrdinal("Id_Comp")) ? 0 : dr.GetInt32(dr.GetOrdinal("Id_Comp"));
                    cl1.Cd_Nombre = dr.IsDBNull(dr.GetOrdinal("Cd_Nombre")) ? "" : dr.GetString(dr.GetOrdinal("Cd_Nombre"));
                    cl1.Comp_Solicito = dr.IsDBNull(dr.GetOrdinal("Comp_Solicito")) ? 0 : dr.GetInt32(dr.GetOrdinal("Comp_Solicito"));
                    cl1.Solicito_Nombre = dr.IsDBNull(dr.GetOrdinal("U_Nombre")) ? "" : dr.GetString(dr.GetOrdinal("U_Nombre"));
                    cl1.FechaAut = dr.IsDBNull(dr.GetOrdinal("Det_FecAut")) ? "" : dr.GetDateTime(dr.GetOrdinal("Det_FecAut")).ToString("dd/MM/yyyy hh:mm:ss tt").ToUpper();
                    cl1.FechaSol = dr.GetDateTime(dr.GetOrdinal("Comp_FechaSol"));
                    cl1.EstatusAut = dr.IsDBNull(dr.GetOrdinal("Estatus")) ? "" : dr.GetString(dr.GetOrdinal("Estatus"));
                    List.Add(cl1);
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaSolicitudUnica(int SolComId, string Conexion, ref List<CompraLocal> List)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@IdSolCom" };
                object[] Valores = { SolComId };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spProCompraLocal_ConsultaUnica", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    CompraLocal cl1 = new CompraLocal();
                    cl1.Id_Comp = dr.IsDBNull(dr.GetOrdinal("Id_Comp")) ? 0 : dr.GetInt32(dr.GetOrdinal("Id_Comp"));
                    cl1.Cd_Nombre = dr.IsDBNull(dr.GetOrdinal("Cd_Nombre")) ? "" : dr.GetString(dr.GetOrdinal("Cd_Nombre"));
                    cl1.Comp_Solicito = dr.IsDBNull(dr.GetOrdinal("Comp_Solicito")) ? 0 : dr.GetInt32(dr.GetOrdinal("Comp_Solicito"));
                    cl1.Solicito_Nombre = dr.IsDBNull(dr.GetOrdinal("U_Nombre")) ? "" : dr.GetString(dr.GetOrdinal("U_Nombre"));
                    cl1.FechaAut = dr.IsDBNull(dr.GetOrdinal("Det_FecAut")) ? "" : dr.GetDateTime(dr.GetOrdinal("Det_FecAut")).ToString("dd/MM/yyyy hh:mm:ss tt").ToUpper();
                    cl1.FechaSol = dr.GetDateTime(dr.GetOrdinal("Comp_FechaSol"));
                    cl1.EstatusAut = dr.IsDBNull(dr.GetOrdinal("Estatus")) ? "" : dr.GetString(dr.GetOrdinal("Estatus"));
                    /// se agrega el idTipo de Solicitud, para la consulta que pueda hacer el selectValue de Categorias
                    // cl1.IdTipoSolicitud = dr.IsDBNull(dr.GetOrdinal("IdTipoSolicitud")) ? 0 : dr.GetInt32(dr.GetOrdinal("IdTipoSolicitud"));
                    List.Add(cl1);
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void GrabaSoloComentariosCliente(string comenta, int solicitud, string Conexion, ref int verifica)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Solicitud", "@Comentarios" };
                object[] Valores = { solicitud, comenta };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spSoloComentariosCompraLocal_Grabar", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    verifica = dr.IsDBNull(dr.GetOrdinal("Grabo")) ? 0 : dr.GetInt32(dr.GetOrdinal("Grabo"));

                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // Compras Locales - Clinetes Exclusivos
        //RBM Marzo 2024 Se agrega tipo cliente para clientes exclusivos
        public int CL_InsertClienteExclusivo(int IdCte, string Nombre, long Id_Sol, string TipoCliente, string Conexion)
        {
            var iRes = 0;

            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@IdCte", "@Nombre", "@Id_Sol", "@TipoCliente" };
                object[] Valores = { IdCte, Nombre, Id_Sol, TipoCliente };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spComprasLocales_InsertClientesExclusivo", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    //iRes = dr.IsDBNull(dr.GetOrdinal("Grabo")) ? 0 : dr.GetInt32(dr.GetOrdinal("Grabo"));
                    iRes = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Grabo")));

                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                //throw ex;
                iRes = -1;
            }
            return iRes;
        }

        // Compras Locales - Clinetes Exclusivos

        public int CL_InsertClienteExclusivo_UpdateSol(
            string KeyArray_ClienteExclusivos, long Id_Solicitud, string Conexion)
        {
            var iRes = 0;

            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@KeyArray_ClienteExclusivos", "@Id_Solicitud" };
                object[] Valores = { KeyArray_ClienteExclusivos, Id_Solicitud };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spComprasLocales_InsertClientesExclusivo_UpdateSol", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    //iRes = dr.IsDBNull(dr.GetOrdinal("Grabo")) ? 0 : dr.GetInt32(dr.GetOrdinal("Grabo"));
                    iRes = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Grabo")));

                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                //throw ex;
                iRes = -1;
            }
            return iRes;
        }


        // Compras Locales - Clinetes Exclusivos
        // FEB13-2020 RFH

        public List<eListaGenerica> CL_SelClienteExclusivo(long Id_Sol, string Conexion)
        {
            List<eListaGenerica> lst = new List<eListaGenerica>();

            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = { "@Id_Sol" };
                object[] Valores = { Id_Sol };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spComprasLocales_SelClientesExclusivo", ref dr, Parametros, Valores);
                while (dr.Read())
                {
                    eListaGenerica Obj = new eListaGenerica();
                    Obj.Id = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("IdCte")));
                    Obj.Descripcion = dr.GetValue(dr.GetOrdinal("Nombre")).ToString();
                    Obj.TipoCliente = dr.GetValue(dr.GetOrdinal("TipoCliente")).ToString();
                    lst.Add(Obj);
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                //throw ex;
                lst = null;
            }
            return lst;
        }


        public void DesAutorizaSolicitud(int solicitud, string Conexion, ref int verifica)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Solicitud" };
                object[] Valores = { solicitud };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spModificaCompraLocal_Grabar", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    verifica = dr.IsDBNull(dr.GetOrdinal("Grabo")) ? 0 : dr.GetInt32(dr.GetOrdinal("Grabo"));

                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void GrabaComentariosCliente(string comenta, string fVige, int TipoSol, int solicitud, string Conexion, ref int verifica)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Solicitud", "@Comentarios", "@FechaVige", "@TipoCompra" };
                object[] Valores = { solicitud, comenta, fVige, TipoSol };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spComentariosCompraLocal_Grabar", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    verifica = dr.IsDBNull(dr.GetOrdinal("Grabo")) ? 0 : dr.GetInt32(dr.GetOrdinal("Grabo"));

                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void EliminaClientesExclusivos(int solicitud, string Conexion, ref int verifica)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_SolComp" };
                object[] Valores = { solicitud };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("EliminaClienteExclusivosSolicitud", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    verifica = dr.IsDBNull(dr.GetOrdinal("Grabo")) ? 0 : dr.GetInt32(dr.GetOrdinal("Grabo"));

                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void GrabaClientesExclusivos(long Prod, int Cliente, int solicitud, string Conexion, ref int verifica)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Prd", "@Id_Cte", "@Id_SolComp" };
                object[] Valores = { Prod, Cliente, solicitud };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("InsertaClienteProdExc", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    verifica = dr.IsDBNull(dr.GetOrdinal("Grabo")) ? 0 : dr.GetInt32(dr.GetOrdinal("Grabo"));

                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void GrabaTipoCompraLocal(
            int solicitud, int tiposolicitud, string Conexion, ref int verifica)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Solicitud", "@TipoCompra" };
                object[] Valores = { solicitud, tiposolicitud };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spTipoCompraLocal_Grabar", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    verifica = dr.IsDBNull(dr.GetOrdinal("Grabo")) ? 0 : dr.GetInt32(dr.GetOrdinal("Grabo"));

                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /*
         * COMPRAS LOCALES 
         * 19FEB-2020
         */
        public int GrabaTipoCompraLocal_ver2(
            int Id_Solicitud, int IdCausaDesabasto, string Comentarios, string Vigencia, int TipoSolicitud,
            string PedidoReferencia, string Conexion)
        {
            int verifica = 0;

            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = {
                    "@Id_Solicitud",
                    "@Comentarios",
                    "@Vigencia",
                    "@TipoSolicitud",
                    "@PedidoReferencia" };

                SqlCommand sqlcmd = null;
                /*
                if (Vigencia == "")
                {
                    object[] Valores = { Id_Solicitud, Comentarios, null, TipoSolicitud, PedidoReferencia };
                    sqlcmd = CapaDatos.GenerarSqlCommand("spTipoCompraLocal_Grabar_ver2", ref dr, Parametros, Valores);
                }
                else
                {
                    object[] Valores = { Id_Solicitud, Comentarios, Vigencia, TipoSolicitud, PedidoReferencia };
                    sqlcmd = CapaDatos.GenerarSqlCommand("spTipoCompraLocal_Grabar_ver2", ref dr, Parametros, Valores);
                }
                */

                object[] Valores = {
                    Id_Solicitud,
                    Comentarios,
                    Vigencia == "" ? null : Vigencia,
                    TipoSolicitud,
                    PedidoReferencia
                };
                sqlcmd = CapaDatos.GenerarSqlCommand("spTipoCompraLocal_Grabar_ver2", ref dr, Parametros, Valores);


                while (dr.Read())
                {
                    verifica = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Grabo")));
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                //throw ex;
                verifica = -1;
            }
            return verifica;
        }


        public void GrabaVigencia(string fVige, int solicitud, string Conexion, ref int verifica)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Solicitud", "@FechaVige" };
                object[] Valores = { solicitud, fVige };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spVigenciaCompraLocal_Grabar", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    verifica = dr.IsDBNull(dr.GetOrdinal("Grabo")) ? 0 : dr.GetInt32(dr.GetOrdinal("Grabo"));

                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /*
         * ENE30-2020 RFH Compras Locales 
         */

        public void GrabaDatosProductoSAT_ver2(int solicitud, long producto, string CUnidad, string CProducto, string Conexion)
        {
            try
            {
                int verifica;
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Solicitud", "@Id_Prd", "@CveUnidad", "@CveProdServ" };
                object[] Valores = { solicitud, producto, CUnidad, CProducto };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spSATAdicionalesProd_Grabar_ver2", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    verifica = dr.IsDBNull(dr.GetOrdinal("Grabo")) ? 0 : dr.GetInt32(dr.GetOrdinal("Grabo"));

                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void GrabaDatosProductoSAT(int solicitud, long producto, string CUnidad, string CProducto, string Conexion)
        {
            try
            {
                int verifica;
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Solicitud", "@Id_Prd", "@CveUnidad", "@CveProdServ" };
                object[] Valores = { solicitud, producto, CUnidad, CProducto };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spSATAdicionalesProd_Grabar", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    verifica = dr.IsDBNull(dr.GetOrdinal("Grabo")) ? 0 : dr.GetInt32(dr.GetOrdinal("Grabo"));

                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ProductosClonados(int solicitud, long productooriginal, long productoclon, string Conexion)
        {
            try
            {
                int verifica;
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Solicitud", "@IdProducto1", "@IdProducto2" };
                object[] Valores = { solicitud, productooriginal, productoclon };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCompraLocalProductosClon_Grabar", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    verifica = dr.IsDBNull(dr.GetOrdinal("Grabo")) ? 0 : dr.GetInt32(dr.GetOrdinal("Grabo"));

                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void NuevoCodigoProducto(int idEmp, int idCd, string Categoria, string Proveed, int Prod, string Conexion, ref string maximo)
        {
            try
            {

                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Cd", "@IdCategoria", "@IdProducto", "@Id_Emp", "@Id_Prov" };
                object[] Valores = { idCd, Categoria, Prod, idEmp.ToString(), Proveed };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("CodigoHomologado_Maximo", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    maximo = dr.IsDBNull(dr.GetOrdinal("Codigo")) ? "" : dr.GetString(dr.GetOrdinal("Codigo"));

                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // Compras Locales
        // ENE21-2020 RFH 

        public void NuevoCodigoProducto_ver2(int idEmp, int idCd, string Categoria, string Proveed, Int64 Prod, string Conexion, ref string maximo)
        {
            try
            {

                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Cd", "@IdCategoria", "@IdProducto", "@Id_Emp", "@Id_Prov" };
                object[] Valores = { idCd, Categoria, Prod, idEmp.ToString(), Proveed };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("CodigoHomologado_Maximo", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    maximo = dr.IsDBNull(dr.GetOrdinal("Codigo")) ? "" : dr.GetString(dr.GetOrdinal("Codigo"));

                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CorreosAutorizadorxMotivoxApp(ref string Correo, int motivo, int aplicacion, int Emp, int Cd, string Conexion)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                SqlDataReader dr = null;
                string[] Parametros = { "@Empresa", "@CDI", "@Motivo", "@Aplicacion" };
                object[] Valores = { Emp, Cd, motivo, aplicacion };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatCorreosAutoCompraLocal_CorreoxApp", ref dr, Parametros, Valores);
                while (dr.Read())
                {
                    Correo = Correo + dr.GetValue(dr.GetOrdinal("Correo")) + ", ";
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        // 5Abr2022 RFH Correo de Autorzador de Compra Local
        public List<eCL_ResponsableAutorizador> spCatCorreosAutoCompraLocal_CorreoxApp(
            ref int Verificador, int motivo, int IdAplicacion, int Emp, int Cd, string Conexion)
        {
            Verificador = 0;
            List<eCL_ResponsableAutorizador> Lst = new List<eCL_ResponsableAutorizador>();
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                SqlDataReader dr = null;
                string[] Parametros = { "@Empresa", "@CDI", "@Motivo", "@Aplicacion" };
                object[] Valores = { Emp, Cd, motivo, IdAplicacion };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatCorreosAutoCompraLocal_CorreoxApp", ref dr, Parametros, Valores);
                while (dr.Read())
                {
                    eCL_ResponsableAutorizador obj = new eCL_ResponsableAutorizador();
                    obj.Id_MotivoCL = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_MotivoCL")));
                    obj.Concepto = dr.GetValue(dr.GetOrdinal("Concepto")).ToString();
                    obj.Correo = dr.GetValue(dr.GetOrdinal("Correo")).ToString();
                    obj.Responsable = dr.GetValue(dr.GetOrdinal("Responsable")).ToString();
                    obj.ResponsableIdUsuario = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ResponsableIdUsuario")));
                    Lst.Add(obj);
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
                Verificador = 1;
            }
            catch (Exception ex)
            {
                Lst = null;
                Verificador = -1;
            }
            return Lst;
        }

        public void RespaldoDeCorreo(string Solicitud, string BodyMail, string Remitente, int Emp, int Cd, string Conexion)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                int verifica;
                SqlDataReader dr = null;
                string[] Parametros = { "@Empresa", "@CDI", "@Solicitud", "@Correo", "@Para" };
                object[] Valores = { Emp, Cd, Solicitud, BodyMail, Remitente };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatCorreoCompraLocal_BckpGuardar", ref dr, Parametros, Valores);
                while (dr.Read())
                {
                    verifica = dr.IsDBNull(dr.GetOrdinal("Grabo")) ? 0 : dr.GetInt32(dr.GetOrdinal("Grabo"));

                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ValidaSolicitud(string Solicitu, int CDII, string Conexion)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                int verifica;
                SqlDataReader dr = null;
                string[] Parametros = { "@IdSolicitud", "@IdCD" };
                object[] Valores = { Solicitu, CDII };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spVerificaErrorSolicitudCompraLocal", ref dr, Parametros, Valores);
                while (dr.Read())
                {
                    verifica = dr.IsDBNull(dr.GetOrdinal("OK")) ? 0 : dr.GetInt32(dr.GetOrdinal("OK"));

                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AutorizaSolicitudEditar(string Solicitud, int Emp, int CDI, int Usr, string Vigencia, string Conexion)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                int verifica;
                SqlDataReader dr = null;
                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_Comp", "@Det_Autorizo", "@FechaVige" };
                object[] Valores = { Emp, CDI, Solicitud, Usr, Vigencia };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCompraLocal_Autorizar_CL_Edicion", ref dr, Parametros, Valores);
                while (dr.Read())
                {
                    verifica = dr.IsDBNull(dr.GetOrdinal("OK")) ? 0 : dr.GetInt32(dr.GetOrdinal("OK"));

                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AutorizaSolicitud(string Solicitud, int Emp, int CDI, int Usr, string Vigencia, string Conexion)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                int verifica;
                SqlDataReader dr = null;
                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_Comp", "@Det_Autorizo", "@FechaVige" };
                object[] Valores = { Emp, CDI, Solicitud, Usr, Convert.ToDateTime(Vigencia) };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCompraLocal_Autorizar_CL", ref dr, Parametros, Valores);
                while (dr.Read())
                {
                    verifica = dr.IsDBNull(dr.GetOrdinal("OK")) ? 0 : dr.GetInt32(dr.GetOrdinal("OK"));

                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CorreosAutorizadorxMotivo(ref string Correo, int motivo, int Emp, int Cd, string Conexion)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                SqlDataReader dr = null;
                string[] Parametros = { "@Empresa", "@CDI", "@Motivo" };
                object[] Valores = { Emp, Cd, motivo };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatCorreosAutoCompraLocal_Correo", ref dr, Parametros, Valores);
                while (dr.Read())
                {
                    Correo = Correo + dr.GetValue(dr.GetOrdinal("Correo")) + ", ";
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CatCorreosCL(ref DataTable dt, int Emp, int Cd, string Conexion)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                SqlDataReader dr = null;
                string[] Parametros = { "@Id_Emp", "@Id_Cd" };
                object[] Valores = { Emp, Cd };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatCorreosAutoCompraLocal_Listado", ref dr, Parametros, Valores);
                while (dr.Read())
                {
                    dt.Rows.Add(new object[] {
                                               dr.GetValue(dr.GetOrdinal("Id_Emp")),
                                               dr.GetValue(dr.GetOrdinal("Id_Cd")),
                                               dr.GetValue(dr.GetOrdinal("Id_Conf")),
                                               dr.GetValue(dr.GetOrdinal("Id_MotivoCL")),
                                               dr.GetValue(dr.GetOrdinal("Secuencia")),
                                               dr.GetValue(dr.GetOrdinal("Desc_MotivoCL")),
                                               dr.GetValue(dr.GetOrdinal("Id_Aplicacion")),
                                               dr.GetValue(dr.GetOrdinal("Aplicacion")),
                                               dr.GetValue(dr.GetOrdinal("Concepto")),
                                               dr.GetValue(dr.GetOrdinal("Correo"))
                    });
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CatMotivoCL(ref DataTable dt, string Conexion)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                SqlDataReader dr = null;
                string[] Parametros = { "@Id_MotivoCL" };
                object[] Valores = { null };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatMotivoCompraLocal_Listado", ref dr, Parametros, Valores);
                while (dr.Read())
                {
                    dt.Rows.Add(new object[] {
                                               dr.GetValue(dr.GetOrdinal("Id_MotivoCL")),
                                               dr.GetValue(dr.GetOrdinal("Desc_MotivoCL")),
                                               dr.GetValue(dr.GetOrdinal("PorcentajeAAA")),
                                               dr.GetValue(dr.GetOrdinal("Aplica"))
                    });
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CatDesConsulta(ref DataTable dt, string Conexion)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                SqlDataReader dr = null;
                string[] Parametros = { "@Id_Causa" };
                object[] Valores = { null };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCompraLocalCausaDesabasto_Consultar", ref dr, Parametros, Valores);
                while (dr.Read())
                {
                    dt.Rows.Add(new object[] {
                                               dr.GetValue(dr.GetOrdinal("Id_Causa")),
                                               dr.GetValue(dr.GetOrdinal("Desc_CausaDesAbasto")),
                                               dr.GetValue(dr.GetOrdinal("Activo"))
                    });
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaClienteExclusivos(ref DataTable dt, string Conexion, long Prod)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                SqlDataReader dr = null;
                string[] Parametros = { "@IdProducto" };
                object[] Valores = { Prod };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("ConsultaProductoExXProducto", ref dr, Parametros, Valores);
                while (dr.Read())
                {
                    dt.Rows.Add(new object[] {
                                               dr.GetValue(dr.GetOrdinal("Id_Cliente")),
                                               dr.GetValue(dr.GetOrdinal("Cliente"))
                    });
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CatCorreoGraba(string Empre, int CDI, int idConfigu, int Motivo, string correo, int aplicacion, int secuancia, string Conexion)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                int verifica;

                string[] Parametros = { "@Empresa", "@CDI", "@COnfiguracion", "@Motivo", "@Correo", "@Aplicacion", "@Secuencia" };
                object[] Valores = { Empre, CDI, idConfigu, Motivo, correo, aplicacion, secuancia };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatCorreosAutoCompraLocal_Grabar", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    verifica = dr.IsDBNull(dr.GetOrdinal("Grabo")) ? 0 : dr.GetInt32(dr.GetOrdinal("Grabo"));

                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CatMotivoGraba(string Id, string descip, string AAA, bool aplica, string Conexion)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                int verifica;
                int Id2 = Convert.ToInt32(Id);
                int aplica2 = aplica ? 1 : 0;
                double AAA2 = Convert.ToDouble(AAA);
                string[] Parametros = { "@Id_MotivoCL", "@Desc_MotivoCL", "@PorcentajeAAA", "@Aplica" };
                object[] Valores = { Id2, descip, AAA2, aplica2 };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatMotivoCompraLocal_Modificar", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    verifica = dr.IsDBNull(dr.GetOrdinal("Grabo")) ? 0 : dr.GetInt32(dr.GetOrdinal("Grabo"));

                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CatDesAgrega(string causa, string Conexion)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                int verifica;
                string[] Parametros = { "@Desc_Causa" };
                object[] Valores = { causa };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCompraLocalCausaDesabasto_Grabar", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    verifica = dr.IsDBNull(dr.GetOrdinal("Grabo")) ? 0 : dr.GetInt32(dr.GetOrdinal("Grabo"));

                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CatDesDesactiva(int IdCausa, string Conexion)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                int verifica;
                string[] Parametros = { "@Id_Causa" };
                object[] Valores = { IdCausa };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCompraLocalCausaDesabasto_Desactiva", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    verifica = dr.IsDBNull(dr.GetOrdinal("Grabo")) ? 0 : dr.GetInt32(dr.GetOrdinal("Grabo"));

                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CatEliminaCorreoNotifica(int Empresa, int CDI, int Motivo, int Aplica, int secuencia, string Conexion)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                int verifica;
                string[] Parametros = { "@Empresa", "@CDI", "@Motivo", "@Aplicacion", "@Secuencia" };
                object[] Valores = { Empresa, CDI, Motivo, Aplica, secuencia };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatCorreosAutoCompraLocal_Elimina", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    verifica = dr.IsDBNull(dr.GetOrdinal("Grabo")) ? 0 : dr.GetInt32(dr.GetOrdinal("Grabo"));

                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CatDesElimina(int IdCausa, string Conexion)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                int verifica;
                string[] Parametros = { "@Id_Causa" };
                object[] Valores = { IdCausa };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCompraLocalCausaDesabasto_Elimina", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    verifica = dr.IsDBNull(dr.GetOrdinal("Grabo")) ? 0 : dr.GetInt32(dr.GetOrdinal("Grabo"));

                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultarSolicitud(int Solicitud, string Conexion, ref long Producto)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                int verifica;
                string[] Parametros = { "@Id_SolComp" };
                object[] Valores = { Solicitud };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("ConsultaSolicitudYProducto", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    Producto = dr.IsDBNull(dr.GetOrdinal("Producto")) ? 0 : dr.GetInt64(dr.GetOrdinal("Producto"));

                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaExistenciaProductoCompraLocal(int Empres, int CDI, long Producto, string Conexion, ref int verifica)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_Prd" };
                object[] Valores = { Empres, CDI, Producto };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spConsultaExistenciaProductoCompraLocal", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    verifica = dr.IsDBNull(dr.GetOrdinal("Existe")) ? 0 : dr.GetInt32(dr.GetOrdinal("Existe"));
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        // Cambios para Ajustes de Costos EMC Sept-2019
        public void EvaluaCambioDeCosto(int Id_Cd, long Id_Prd, double Es_Costo, string Conexion, ref int iOkk)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = { "@Id_Prd", "@Costo" };       //  "@Id_Cd",
                object[] Valores = { Id_Prd, Es_Costo };               // Id_Cd
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spEvaluaCambioCosto_CCL", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    iOkk = dr.IsDBNull(dr.GetOrdinal("iMsg")) ? 0 : dr.GetInt32(dr.GetOrdinal("iMsg"));
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AplicaCambioDeCostoCompraLocal(int Id_Cd, long Id_Prd, double Es_Costo, string Conexion, ref int iOkk)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = { "@Id_Prd", "@Costo" };       //  "@Id_Cd",
                object[] Valores = { Id_Prd, Es_Costo };               // Id_Cd
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spGeneraCambioCosto_CCL", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    iOkk = 1;
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        //  -----------------------------------------------------------------------------------------------------------------------

        public void ListaSucursales(string Conexion, ref List<Embudo> List)
        {
            try
            {
                Embudo VI = new Embudo();
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spSucursalesReporteNacional_Listar", ref dr);

                while (dr.Read())
                {
                    VI = new Embudo();
                    VI.Ordern = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id")));
                    VI.RowDesc = dr.GetValue(dr.GetOrdinal("Sucursal")).ToString();
                    List.Add(VI);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<eComprasLocales> Get_ComprasLocales_Index(int Id_U, int Id_Emp, int Id_Cd, int PageNo, int PageSize, string Vencido, Int64 Id_Prd, int IdProveedorLocal, int Id_Motivo, int Id_Comp, int Id_Estatus, ref int Verificador, string Conexion)
        {
            Verificador = 0;
            List<eComprasLocales> lst = new List<eComprasLocales>();
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@UsuarioQueConsulta", "@Id_Emp", "@Id_Cd", "@PageNo", "@PageSize", "@Vencido", "@Id_Prd", "IdProveedorLocal", "Id_Motivo", "Id_Comp", "Id_Estatus" };

                object[] Valores = { Id_U, Id_Emp, Id_Cd, PageNo, PageSize, Vencido, Id_Prd, IdProveedorLocal, Id_Motivo, Id_Comp, Id_Estatus };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spProCompraLocal_Consulta_ver4", ref dr, Parametros, Valores);
                //SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spProCompraLocal_Consulta_ver3", ref dr, Parametros, Valores);
                //SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spProCompraLocal_Consulta_ver2", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    eComprasLocales Obj = new eComprasLocales();
                    // #Sucursal
                    // Nombre Sucursal
                    Obj.Id_Cd = Convert.ToInt32(dr[dr.GetOrdinal("Id_Cd")].ToString());
                    Obj.Cd_Nombre = dr.GetValue(dr.GetOrdinal("Cd_Nombre")).ToString();
                    Obj.Comp_Solicitud = Convert.ToInt32(dr[dr.GetOrdinal("Comp_Solicito")].ToString());
                    Obj.Id_Comp = Convert.ToInt32(dr[dr.GetOrdinal("Id_Comp")].ToString());
                    Obj.Id_CompDet = Convert.ToInt32(dr[dr.GetOrdinal("Id_CompDet")].ToString());
                    // Fecha Solicitud
                    Obj.Comp_FechaSol = dr.GetValue(dr.GetOrdinal("Comp_FechaSol")).ToString();
                    Obj.FechaSolicitud_Anio = Convert.ToInt32(dr[dr.GetOrdinal("FechaSolicitud_Anio")].ToString());
                    Obj.FechaSolicitud_Mes = Convert.ToInt32(dr[dr.GetOrdinal("FechaSolicitud_Mes")].ToString());
                    Obj.Det_FecAut = dr.GetValue(dr.GetOrdinal("Det_FecAut")).ToString();
                    // Usuario
                    Obj.U_Nombre = dr.GetValue(dr.GetOrdinal("U_Nombre")).ToString();
                    Obj.U_Correo = dr.GetValue(dr.GetOrdinal("U_Correo")).ToString();
                    Obj.Estatus = dr.GetValue(dr.GetOrdinal("Estatus")).ToString();
                    // Comentarios
                    Obj.Comentarios = dr.GetValue(dr.GetOrdinal("Comentarios")).ToString();
                    Obj.VigenciaFecha = dr.GetValue(dr.GetOrdinal("Vigencia")).ToString();
                    // Motivos Compra Local 
                    Obj.IdTipoSolicitud = Convert.ToInt32(dr[dr.GetOrdinal("IdTipoSolicitud")].ToString());
                    Obj.TipoSolicitudNombre = dr.GetValue(dr.GetOrdinal("TipoSolicitud")).ToString();
                    Obj.Autorizados = Convert.ToInt32(dr[dr.GetOrdinal("Autorizados")].ToString());
                    Obj.Totales = Convert.ToInt32(dr[dr.GetOrdinal("Totales")].ToString());
                    Obj.Det_Estatus = dr.GetValue(dr.GetOrdinal("Det_Estatus")).ToString();
                    Obj.Id_Prd = Convert.ToInt64(dr[dr.GetOrdinal("Id_Prd")].ToString());
                    // Fecha Venc Codigio Local
                    Obj.Vigencia = dr.GetValue(dr.GetOrdinal("Vigencia")).ToString();
                    // Falta de Entrega CEDIS
                    // Falta de Entrega DIRECTA
                    // Incremento Intesperado Demanda
                    // Error Planeacion 
                    // Motivo de Solicitud
                    // #Proveedor Padre
                    Obj.ProveedorPadreClave = dr.GetValue(dr.GetOrdinal("ProveedorPadreClave")).ToString();
                    Obj.ProveedorPadreNombre = dr.GetValue(dr.GetOrdinal("ProveedorPadreNombre")).ToString();
                    // #Proveedor Local                    
                    // Nombre Proveedor Padre
                    // Nombre Proveedor Local
                    // Proveedor ok
                    Obj.IdProveedor = Convert.ToInt32(dr[dr.GetOrdinal("IdProveedor")].ToString());
                    Obj.ProveedorNombre = dr.GetValue(dr.GetOrdinal("ProveedorNombre")).ToString();
                    // Descripion
                    Obj.DescripcionProducto = dr.GetValue(dr.GetOrdinal("ProductoNombre")).ToString();
                    // Tipo de Producto
                    Obj.TipoProducto = dr.GetValue(dr.GetOrdinal("TipoProducto")).ToString();
                    // Aplicacion Familia
                    Obj.ProductoAplicacion = dr.GetValue(dr.GetOrdinal("ProductoAplicacion")).ToString();
                    // Subfamilia
                    Obj.ProductoFamilia = dr.GetValue(dr.GetOrdinal("ProductoFamilia")).ToString();
                    // Preentacion 
                    // Unidad
                    // Año Solicitud
                    Obj.FechaSolicitud_Anio = Convert.ToInt32(dr[dr.GetOrdinal("FechaSolicitud_Anio")].ToString());
                    // Mes Solicitud
                    Obj.FechaSolicitud_Mes = Convert.ToInt32(dr[dr.GetOrdinal("FechaSolicitud_Mes")].ToString());
                    // OC Original (Aplica Motivo 1)
                    // OC Local 
                    // Cantidad Ordenada
                    // Costo
                    // PAAA
                    Obj.PrecioAAA = Convert.ToDouble(dr[dr.GetOrdinal("PrecioAAAKey")].ToString());
                    // Precio Public 
                    Obj.PrecioPublico = Convert.ToDouble(dr[dr.GetOrdinal("PrecioPublico")].ToString());
                    // Monto Compra Local 
                    Obj.PrecioCostoCompralocal = Convert.ToDouble(dr[dr.GetOrdinal("PrecioAAA")].ToString()); ;
                    Obj.PrecioCosto = Convert.ToDouble(dr[dr.GetOrdinal("PrecioCosto")].ToString());
                    // SKUPadre
                    // SKULocal
                    Obj.Id_PrdOriginal = Convert.ToInt64(dr[dr.GetOrdinal("Id_PrdOriginal")].ToString());
                    // ok
                    Obj.ProductoNombre = dr.GetValue(dr.GetOrdinal("ProductoNombre")).ToString();
                    // Causa de desabasto ok
                    Obj.IdCausaDesabasto = Convert.ToInt32(dr[dr.GetOrdinal("IdCausaDesabasto")].ToString());
                    Obj.CausaDesabasto = dr.GetValue(dr.GetOrdinal("CausaDesabasto")).ToString();
                    // Tipo Poroducto
                    Obj.IdTipoProducto = Convert.ToInt32(dr[dr.GetOrdinal("IdTipoProducto")].ToString());
                    Obj.TipoProducto = dr.GetValue(dr.GetOrdinal("TipoProducto")).ToString();
                    Obj.AutorizadorId = Convert.ToInt32(dr[dr.GetOrdinal("AutorizadorId")].ToString());
                    Obj.AutorizadorNombre = dr.GetValue(dr.GetOrdinal("AutorizadorNombre")).ToString();
                    // Entradas y Salidas 
                    Obj.Entradas_Cant = Convert.ToInt32(dr[dr.GetOrdinal("Entradas_Cant")].ToString());
                    Obj.Entradas_Costo = Convert.ToDecimal(dr[dr.GetOrdinal("Entradas_Costo")].ToString());
                    Obj.Salidas_Cant = Convert.ToInt32(dr[dr.GetOrdinal("Salidas_Cant")].ToString());
                    Obj.Salidas_Costo = Convert.ToDecimal(dr[dr.GetOrdinal("Salidas_Costo")].ToString());

                    Obj.Prd_Presentacion = dr.GetValue(dr.GetOrdinal("Prd_Presentacion")).ToString();
                    Obj.Prd_UniEmp = Convert.ToInt32(dr[dr.GetOrdinal("Prd_UniEmp")].ToString());

                    lst.Add(Obj);
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
                Verificador = 1;
            }
            catch (Exception ex)
            {
                Verificador = -1;
                lst = null;
            }
            return lst;
        }

        //RBM MARZO 2024 DETALLE PARA LISTADO INICIAL
        public void Get_DetalleCL(ref CompraLocal CL, string Conexion)
        {
            List<CompraLocal> lst = new List<CompraLocal>();
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = {
                    "@Id_Emp",
                    "@Id_Cd",
                    "@Id_Comp"
                };

                object[] Valores = {
                    CL.Id_Emp,
                    CL.Id_Cd,
                    CL.Id_Comp
                };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("sp_consultaCompaLocal", ref dr, Parametros, Valores);

                if (dr.HasRows)
                {
                    dr.Read();
                    //General
                    CL.FechaSol = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("FechaSol")));
                    CL.Vigencia = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("FechaVig")));
                    CL.EstatusAut = dr.GetValue(dr.GetOrdinal("Estatus")).ToString();
                    CL.Cd_Nombre = dr.GetValue(dr.GetOrdinal("Sucursal")).ToString();
                    CL.Solicito_Nombre = dr.GetValue(dr.GetOrdinal("Solicitante")).ToString();
                    CL.IdTipoSolicitud = dr.GetInt32(dr.GetOrdinal("IdTipoSolicitud"));
                    CL.TipoSolicitud = dr.GetValue(dr.GetOrdinal("MotivoCL")).ToString();
                    CL.Id_Prd = dr.GetInt64(dr.GetOrdinal("Id_Prd"));
                    CL.Prd_Descripcion = dr.GetValue(dr.GetOrdinal("Prd_Descripcion")).ToString();
                    CL.Prd_Presentacion = dr.GetValue(dr.GetOrdinal("Prd_Presentacion")).ToString();
                    CL.CodigoPadre = dr.GetValue(dr.GetOrdinal("CodigoPadre")).ToString();
                    CL.ProveedorLocal = dr.GetValue(dr.GetOrdinal("ProveedorLocal")).ToString();
                    CL.ProveedorCentral = dr.GetValue(dr.GetOrdinal("ProveedorCentral")).ToString();
                    CL.Prd_CodigoProv = dr.GetValue(dr.GetOrdinal("Prd_CodigoProv")).ToString();
                    CL.Prd_DescripcionProv = dr.GetValue(dr.GetOrdinal("Prd_DescripcionProv")).ToString();
                    CL.Prd_PresentacionProv = dr.GetValue(dr.GetOrdinal("Prd_PresentacionProv")).ToString();
                    CL.Prd_UniNe = dr.GetValue(dr.GetOrdinal("UnidadEnt")).ToString();
                    CL.Prd_Unico = dr.GetValue(dr.GetOrdinal("FactorConv")).ToString();
                    CL.Prd_UniNs = dr.GetValue(dr.GetOrdinal("UnidadSal")).ToString();
                    CL.Prd_UniEmp = dr.GetValue(dr.GetOrdinal("UnidadEmp")).ToString();
                    CL.Comentarios = dr.GetValue(dr.GetOrdinal("CausaDes")).ToString();
                    CL.Aplicacion = dr.GetValue(dr.GetOrdinal("Aplicacion")).ToString();
                    CL.Prd_SubFamilia = dr.GetValue(dr.GetOrdinal("SubFamilia")).ToString();
                    CL.IdTipoProd = dr.GetValue(dr.GetOrdinal("IdTipoProd")).ToString();
                    CL.NomTipoProd = dr.GetValue(dr.GetOrdinal("NomTipoProd")).ToString();


                    //    //Precios
                    CL.Costo = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Costo")));
                    CL.PrecioAAACL = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("AAACompraLocal")));
                    CL.PrecioAAAKey = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("AAAKey")));
                    CL.PrecioPublico = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Publico")));

                    //SAP
                    CL.Prd_ClaveProdServ = dr.GetValue(dr.GetOrdinal("Prd_ClaveProdServ")).ToString();
                    CL.Prd_ClaveUnidad = dr.GetValue(dr.GetOrdinal("Prd_ClaveUnidad")).ToString();

                    //    //Clientes Exclusivos
                    CL.IdCteExc = dr.GetValue(dr.GetOrdinal("IdCte")).ToString();
                    CL.ClienteExc = dr.GetValue(dr.GetOrdinal("ClienteExc")).ToString();
                    CL.TipoCliente = dr.GetValue(dr.GetOrdinal("TipoCliente")).ToString();

                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<clPrecio> Get_PreciosCL(ref CompraLocal CL, string Conexion)
        {
            List<clPrecio> lst = new List<clPrecio>();
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = {
                    "@Id_Emp",
                    "@Id_Cd",
                    "@Id_Comp",
                    "@Id_Prd"

                };

                object[] Valores = {
                    CL.Id_Emp,
                    CL.Id_Cd,
                    CL.Id_Comp,
                    CL.Id_Prd
                };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("sp_consultaPreciosCL", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    clPrecio Obj = new clPrecio();
                    Obj.Id_Pre = Convert.ToInt32(dr[dr.GetOrdinal("Id_Pre")].ToString());
                    Obj.Prd_PreDescripcion = dr.GetValue(dr.GetOrdinal("Prd_PreDescripcion")).ToString();
                    Obj.Prd_Pesos = Convert.ToDecimal(dr[dr.GetOrdinal("Prd_Pesos")].ToString());

                    lst.Add(Obj);
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
                //Verificador = 1;
            }
            catch (Exception ex)
            {
                //Verificador = -1;
                lst = null;
            }
            return lst;
        }

        public List<ClienteExclusivo> Get_ClientesExclusivosCL(ref CompraLocal CL, string Conexion)
        {
            List<ClienteExclusivo> lst = new List<ClienteExclusivo>();
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = {
                    "@Id_Comp"
                };

                object[] Valores = {
                    CL.Id_Comp
                };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("sp_consultaClienteExclusivoCL", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    ClienteExclusivo Obj = new ClienteExclusivo();
                    Obj.Id_Cte = Convert.ToInt32(dr[dr.GetOrdinal("Id_Cte")].ToString());
                    Obj.Nombre = dr.GetValue(dr.GetOrdinal("Nombre")).ToString();
                    Obj.TipoCliente = dr.GetValue(dr.GetOrdinal("TipoCliente")).ToString();

                    lst.Add(Obj);
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
                //Verificador = 1;
            }
            catch (Exception ex)
            {
                //Verificador = -1;
                lst = null;
            }
            return lst;
        }

        public List<eCLLogs> spComprasLocales_GetLogs(int Id_Emp, int Id_Cd, int Id_Comp, string Conexion)
        {
            List<eCLLogs> lst = new List<eCLLogs>();
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = {
                    "@Id_Emp",
                    "@Id_Cd",
                    "@Id_Comp"
                };

                object[] Valores = {
                    Id_Emp,
                    Id_Cd,
                    Id_Comp
                };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spComprasLocales_GetLogs", ref dr, Parametros.ToArray(), Valores.ToArray());

                while (dr.Read())
                {
                    eCLLogs obj = new eCLLogs();
                    obj.IdCompraLocalLogs = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Comp")));
                    obj.Id_Emp = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Emp")));
                    obj.Id_Cd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cd")));
                    obj.Id_Comp = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Comp")));
                    obj.Fecha = Convert.ToString(dr.GetValue(dr.GetOrdinal("Fecha")));
                    obj.Id_Usuario = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Usuario")));
                    obj.Nota = dr.GetValue(dr.GetOrdinal("Nota")).ToString();
                    lst.Add(obj);
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                lst = null;
            }
            return lst;
        }

        public void ActualizarCompraLocal(int Id_Emp, int Id_Cd, int Id_Comp, string Id_Prd, string Prd_FechaInicio, string Prd_FechaFin, string Prd_ClaveUnidad, string Prd_ClaveProdServ, string Emp_Cnx, ref int Res, ref CompraLocal CL)
        {
            try
            {
                SqlDataReader dr = null;

                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Emp_Cnx);

                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_Comp", "@Id_Prd", "@Prd_FechaIni", "@Prd_FechaFin", "@Prd_ClaveUnidad", "@Prd_ClaveProdServ" };
                object[] Valores = { Id_Emp, Id_Cd, Id_Comp, Id_Prd, Prd_FechaInicio.ToString(), Prd_FechaFin.ToString(), Prd_ClaveUnidad, Prd_ClaveProdServ };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spActualizo_CompraLocal", ref dr, Parametros, Valores);

                if (dr.HasRows)
                {
                    dr.Read();
                    //General
                    CL.FechaSol = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("FechaSol")));
                    CL.Vigencia = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("FechaVig")));
                    CL.EstatusAut = dr.GetValue(dr.GetOrdinal("Estatus")).ToString();
                    CL.Cd_Nombre = dr.GetValue(dr.GetOrdinal("Sucursal")).ToString();
                    CL.Solicito_Nombre = dr.GetValue(dr.GetOrdinal("Solicitante")).ToString();
                    CL.IdTipoSolicitud = dr.GetInt32(dr.GetOrdinal("IdTipoSolicitud"));
                    CL.TipoSolicitud = dr.GetValue(dr.GetOrdinal("MotivoCL")).ToString();
                    CL.Id_Prd = dr.GetInt64(dr.GetOrdinal("Id_Prd"));
                    CL.Prd_Descripcion = dr.GetValue(dr.GetOrdinal("Prd_Descripcion")).ToString();
                    CL.Prd_Presentacion = dr.GetValue(dr.GetOrdinal("Prd_Presentacion")).ToString();
                    CL.CodigoPadre = dr.GetValue(dr.GetOrdinal("CodigoPadre")).ToString();
                    CL.ProveedorLocal = dr.GetValue(dr.GetOrdinal("ProveedorLocal")).ToString();
                    CL.ProveedorCentral = dr.GetValue(dr.GetOrdinal("ProveedorCentral")).ToString();
                    CL.Prd_CodigoProv = dr.GetValue(dr.GetOrdinal("Prd_CodigoProv")).ToString();
                    CL.Prd_DescripcionProv = dr.GetValue(dr.GetOrdinal("Prd_DescripcionProv")).ToString();
                    CL.Prd_PresentacionProv = dr.GetValue(dr.GetOrdinal("Prd_PresentacionProv")).ToString();
                    CL.Prd_UniNe = dr.GetValue(dr.GetOrdinal("UnidadEnt")).ToString();
                    CL.Prd_Unico = dr.GetValue(dr.GetOrdinal("FactorConv")).ToString();
                    CL.Prd_UniNs = dr.GetValue(dr.GetOrdinal("UnidadSal")).ToString();
                    CL.Prd_UniEmp = dr.GetValue(dr.GetOrdinal("UnidadEmp")).ToString();
                    CL.Comentarios = dr.GetValue(dr.GetOrdinal("CausaDes")).ToString();
                    CL.Aplicacion = dr.GetValue(dr.GetOrdinal("Aplicacion")).ToString();
                    CL.Prd_SubFamilia = dr.GetValue(dr.GetOrdinal("SubFamilia")).ToString();
                    CL.IdTipoProd = dr.GetValue(dr.GetOrdinal("IdTipoProd")).ToString();
                    CL.NomTipoProd = dr.GetValue(dr.GetOrdinal("NomTipoProd")).ToString();
                    CL.IdAplicacion = dr.GetValue(dr.GetOrdinal("IdAplicacion")).ToString();


                    //    //Precios
                    CL.Costo = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Costo")));
                    CL.PrecioAAACL = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("AAACompraLocal")));
                    CL.PrecioAAAKey = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("AAAKey")));
                    CL.PrecioPublico = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Publico")));

                    //SAP
                    CL.Prd_ClaveProdServ = dr.GetValue(dr.GetOrdinal("Prd_ClaveProdServ")).ToString();
                    CL.Prd_ClaveUnidad = dr.GetValue(dr.GetOrdinal("Prd_ClaveUnidad")).ToString();

                    //Clientes Exclusivos
                    CL.IdCteExc = dr.GetValue(dr.GetOrdinal("IdCte")).ToString();
                    CL.ClienteExc = dr.GetValue(dr.GetOrdinal("ClienteExc")).ToString();
                    CL.TipoCliente = dr.GetValue(dr.GetOrdinal("TipoCliente")).ToString();
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CL_UpdatePreciosCompraLocal(int Id_Emp, int Id_Cd, string Id_Prd, int Id_Pre, string Prd_FechaInicio, string Prd_FechaFin, float Prd_Pesos, string Emp_Cnx)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Emp_Cnx);

                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_Prd", "@Id_Pre", "@Prd_FechaInicio", "@Prd_FechaFin", "@Prd_Pesos" };
                object[] Valores = { Id_Emp, Id_Cd, Id_Prd, Id_Pre, Convert.ToDateTime(Prd_FechaInicio), Convert.ToDateTime(Prd_FechaFin), Prd_Pesos };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spUpdatePreciosComprasLocales", ref dr, Parametros, Valores);

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Reportes Compras Locales
        //RBM Marzo 2024
        //Inicio
        public void ConsultaComprasLocalesXMotivo(CompraLocal CL, ref List<CompraLocal> ListaComprasLocales, DateTime FechaIni, DateTime FechaFin, string Emp_Cnx)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Emp_Cnx);

                string[] Parametros = {
                    "@Id_Emp",
                    "@Id_Cd",
                    "@Id_Motivo",
                    "@FechaInicio",
                    "@FechaFin"
                };

                object[] Valores = {
                    CL.Id_Emp,
                    CL.Id_Cd,
                    CL.Id_Motivo,
                    FechaIni,
                    FechaFin
                };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spConsultaComprasLocalesXMotivo", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    CompraLocal Obj = new CompraLocal();
                    Obj.Id_Cd = Convert.ToInt32(dr[dr.GetOrdinal("Id_Cd")].ToString());
                    Obj.Cd_Nombre = dr.GetValue(dr.GetOrdinal("Alm_Nombre")).ToString();
                    Obj.Id_Motivo = Convert.ToInt32(dr[dr.GetOrdinal("Id_Motivo")].ToString());
                    Obj.Motivo = dr.GetValue(dr.GetOrdinal("Nom_Motivo")).ToString();
                    Obj.Unidades = Convert.ToInt32(dr[dr.GetOrdinal("Unidades")].ToString());
                    Obj.Pesos = Convert.ToDecimal(dr[dr.GetOrdinal("Pesos")].ToString());


                    ListaComprasLocales.Add(Obj);
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
                //Verificador = 1;
            }
            catch (Exception ex)
            {
                //Verificador = -1;
            }
        }
        public void ConsultaComprasLocalesXSucursal(CompraLocal CL, ref List<CompraLocal> ListaComprasLocales, DateTime FechaIni, DateTime FechaFin, string Emp_Cnx)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Emp_Cnx);

                string[] Parametros = {
                    "@Id_Emp",
                    "@Id_Cd",
                    "@Id_Motivo",
                    "@FechaInicio",
                    "@FechaFin"
                };

                object[] Valores = {
                    CL.Id_Emp,
                    CL.Id_Cd,
                    CL.Id_Motivo,
                    FechaIni,
                    FechaFin
                };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spConsultaComprasLocalesXSucursal", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    CompraLocal Obj = new CompraLocal();
                    Obj.Id_Cd = Convert.ToInt32(dr[dr.GetOrdinal("Id_Cd")].ToString());
                    Obj.Cd_Nombre = dr.GetValue(dr.GetOrdinal("Alm_Nombre")).ToString();
                    Obj.Pesos = Convert.ToDecimal(dr[dr.GetOrdinal("Pesos")].ToString());
                    Obj.Unidades = Convert.ToInt32(dr[dr.GetOrdinal("Unidades")].ToString());
                    Obj.TotalProveedores = Convert.ToInt32(dr[dr.GetOrdinal("TotalProveedores")].ToString());
                    Obj.TotalProductos = Convert.ToInt32(dr[dr.GetOrdinal("TotalProductos")].ToString());
                    Obj.Porcentaje = Convert.ToDecimal(dr[dr.GetOrdinal("Porcentaje")].ToString());

                    ListaComprasLocales.Add(Obj);
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
                //Verificador = 1;
            }
            catch (Exception ex)
            {
                //Verificador = -1;
            }
        }
        public void ConsultaComprasLocalesXTipoProduto(CompraLocal CL, ref List<CompraLocal> ListaComprasLocales, DateTime FechaIni, DateTime FechaFin, string Emp_Cnx)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Emp_Cnx);

                string[] Parametros = {
                    "@Id_Emp",
                    "@Id_Cd",
                    "@Id_Motivo",
                    "@FechaInicio",
                    "@FechaFin",
                    "@Tipoproducto"
                };

                object[] Valores = {
                    CL.Id_Emp,
                    CL.Id_Cd,
                    CL.Id_Motivo,
                    FechaIni,
                    FechaFin,
                    CL.IdTipoProd

                };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spConsultaComprasLocalesXTipoProducto", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    CompraLocal Obj = new CompraLocal();
                    Obj.Id_Cd = Convert.ToInt32(dr[dr.GetOrdinal("Id_Cd")].ToString());
                    Obj.Cd_Nombre = dr.GetValue(dr.GetOrdinal("Cd_Nombre")).ToString();
                    Obj.IdTipoProd = dr.GetValue(dr.GetOrdinal("TipoProducto")).ToString();
                    Obj.TipoProd = dr.GetValue(dr.GetOrdinal("Descripcion")).ToString();
                    Obj.Unidades = Convert.ToInt32(dr[dr.GetOrdinal("Unidades")].ToString());
                    Obj.Pesos = Convert.ToDecimal(dr[dr.GetOrdinal("Pesos")].ToString());

                    ListaComprasLocales.Add(Obj);
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
                //Verificador = 1;
            }
            catch (Exception ex)
            {
                //Verificador = -1;
            }
        }
        public void ConsultaComprasLocalesXProvLocal(CompraLocal CL, ref List<CompraLocal> ListaComprasLocales, DateTime FechaIni, DateTime FechaFin, string Emp_Cnx)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Emp_Cnx);

                string[] Parametros = {
                    "@Id_Emp",
                    "@Id_Cd",
                    "@Id_Motivo",
                    "@FechaInicio",
                    "@FechaFin",
                    "@ProveedorCentral",
                    "@Tipoproducto",
                    "@ProductoCentral"
                };

                object[] Valores = {
                    CL.Id_Emp,
                    CL.Id_Cd,
                    CL.Id_Motivo,
                    FechaIni,
                    FechaFin,
                    CL.ProveedorCentral,
                    CL.TipoProd,
                    CL.CodigoPadre
                };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spConsultaComprasLocalesXProvLocal", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    CompraLocal Obj = new CompraLocal();
                    Obj.Id_Cd = Convert.ToInt32(dr[dr.GetOrdinal("Id_Cd")].ToString());
                    Obj.Cd_Nombre = dr.GetValue(dr.GetOrdinal("Cd_Nombre")).ToString();
                    Obj.IdProveedorLocal = Convert.ToInt32(dr[dr.GetOrdinal("ProveedorLocal")].ToString());
                    Obj.ProveedorLocal = dr.GetValue(dr.GetOrdinal("Descripcion")).ToString();
                    Obj.Unidades = Convert.ToInt32(dr[dr.GetOrdinal("Unidades")].ToString());
                    Obj.Pesos = Convert.ToDecimal(dr[dr.GetOrdinal("Pesos")].ToString());

                    ListaComprasLocales.Add(Obj);
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
                //Verificador = 1;
            }
            catch (Exception ex)
            {
                //Verificador = -1;
            }
        }

        //public void ConsultaComprasLocalesXProvLocal(CompraLocal CL, ref List<CompraLocal> ListaComprasLocales, DateTime FechaIni, DateTime FechaFin, string Emp_Cnx)
        //{
        //    try
        //    {
        //        SqlDataReader dr = null;
        //        CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Emp_Cnx);

        //        string[] Parametros = {
        //            "@Id_Emp",
        //            "@Id_Cd",
        //            "@Id_Motivo",
        //            "@FechaInicio",
        //            "@FechaFin",
        //            "@ProveedorCentral",
        //            "@Tipoproducto",
        //            "@ProductoCentral"
        //        };

        //        object[] Valores = {
        //            CL.Id_Emp,
        //            CL.Id_Cd,
        //            CL.Id_Motivo,
        //            FechaIni,
        //            FechaFin,
        //            CL.ProveedorCentral,
        //            CL.TipoProd,
        //            CL.CodigoPadre
        //        };

        //        SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spConsultaComprasLocalesXProvLocal", ref dr, Parametros, Valores);

        //        while (dr.Read())
        //        {
        //            CompraLocal Obj = new CompraLocal();
        //            Obj.Id_Cd = Convert.ToInt32(dr[dr.GetOrdinal("Id_Cd")].ToString());
        //            Obj.Cd_Nombre = dr.GetValue(dr.GetOrdinal("Cd_Nombre")).ToString();
        //            Obj.IdProveedorLocal = Convert.ToInt32(dr[dr.GetOrdinal("ProveedorLocal")].ToString());
        //            Obj.ProveedorLocal = dr.GetValue(dr.GetOrdinal("Descripcion")).ToString();
        //            Obj.Unidades = Convert.ToInt32(dr[dr.GetOrdinal("Unidades")].ToString());
        //            Obj.Pesos = Convert.ToDecimal(dr[dr.GetOrdinal("Pesos")].ToString());

        //            ListaComprasLocales.Add(Obj);
        //        }
        //        CapaDatos.LimpiarSqlcommand(ref sqlcmd);
        //        //Verificador = 1;
        //    }
        //    catch (Exception ex)
        //    {
        //        //Verificador = -1;
        //    }
        //}
        public void ConsultaComprasLocalesXProvCentral(CompraLocal CL, ref List<CompraLocal> ListaComprasLocales, DateTime FechaIni, DateTime FechaFin, string Emp_Cnx)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Emp_Cnx);

                string[] Parametros = {
                    "@Id_Emp",
                    "@Id_Cd",
                    "@Id_Motivo",
                    "@FechaInicio",
                    "@FechaFin",
                    "@ProveedorCentral",
                    "@TipoProducto",
                    "@ProductoCentral"
                };

                object[] Valores = {
                    CL.Id_Emp,
                    CL.Id_Cd,
                    CL.Id_Motivo,
                    FechaIni,
                    FechaFin,
                    CL.ProveedorCentral,
                    CL.TipoProd,
                    CL.CodigoPadre
                };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spConsultaComprasLocalesXProvCentral", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    CompraLocal Obj = new CompraLocal();
                    Obj.Id_Cd = Convert.ToInt32(dr[dr.GetOrdinal("Id_Cd")].ToString());
                    Obj.Cd_Nombre = dr.GetValue(dr.GetOrdinal("Cd_Nombre")).ToString();
                    Obj.IdProveedorCentral = dr.GetValue(dr.GetOrdinal("IdProveedorCentral")).ToString();
                    Obj.ProveedorCentral = dr.GetValue(dr.GetOrdinal("ProveedorCentral")).ToString();
                    Obj.Unidades = Convert.ToInt32(dr[dr.GetOrdinal("Unidades")].ToString());
                    Obj.Pesos = Convert.ToDecimal(dr[dr.GetOrdinal("Pesos")].ToString());

                    ListaComprasLocales.Add(Obj);
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
                //Verificador = 1;
            }
            catch (Exception ex)
            {
                //Verificador = -1;
            }
        }
        public void ConsultaComprasLocalesXAplicacion(CompraLocal CL, ref List<CompraLocal> ListaComprasLocales, DateTime FechaIni, DateTime FechaFin, string Emp_Cnx)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Emp_Cnx);

                string[] Parametros = {
                    "@Id_Emp",
                    "@Id_Cd",
                    "@Id_Motivo",
                    "@FechaInicio",
                    "@FechaFin",
                    "@TipoProducto",
                    "@Aplicacion"

                };

                object[] Valores = {
                    CL.Id_Emp,
                    CL.Id_Cd,
                    CL.Id_Motivo,
                    FechaIni,
                    FechaFin,
                    CL.TipoProd,
                    CL.Aplicacion
                };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spConsultaComprasLocalesXAplicacion", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    CompraLocal Obj = new CompraLocal();
                    Obj.Id_Cd = Convert.ToInt32(dr[dr.GetOrdinal("Id_Cd")].ToString());
                    Obj.Cd_Nombre = dr.GetValue(dr.GetOrdinal("Cd_Nombre")).ToString();
                    Obj.IdAplicacion = dr.GetValue(dr.GetOrdinal("Aplicacion")).ToString();
                    Obj.Aplicacion = dr.GetValue(dr.GetOrdinal("NomAplicacion")).ToString();
                    Obj.Unidades = Convert.ToInt32(dr[dr.GetOrdinal("Unidades")].ToString());
                    Obj.Pesos = Convert.ToDecimal(dr[dr.GetOrdinal("Pesos")].ToString());

                    ListaComprasLocales.Add(Obj);
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
                //Verificador = 1;
            }
            catch (Exception ex)
            {
                //Verificador = -1;
            }
        }

        public void ConsultaComprasLocalesXPrecioAAA(CompraLocal CL, ref DataSet dsDatos, DateTime FechaIni, DateTime FechaFin, string Emp_Cnx)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Emp_Cnx);

                string[] Parametros = {
                    "@Id_Emp",
                    "@Id_Cd",
                    "@Id_Motivo",
                    "@FechaInicio",
                    "@FechaFin",
                    "@TipoPrecioAAA"
                };

                object[] Valores = {
                    CL.Id_Emp,
                    CL.Id_Cd,
                    CL.Id_Motivo,
                    FechaIni,
                    FechaFin,
                    CL.TipoAAA
                };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spConsultaComprasLocalesXPrecioAAA", ref dsDatos, Parametros, Valores);
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                //Verificador = -1;
            }
        }

        public void ConsultaComprasLocalesXTipoAAA(CompraLocal CL, ref List<CompraLocal> ListaComprasLocales, DateTime FechaIni, DateTime FechaFin, string emp_Cnx)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(emp_Cnx);

                string[] Parametros = {
                    "@Id_Emp",
                    "@Id_Cd",
                    "@Id_Motivo",
                    "@FechaInicio",
                    "@FechaFin",
                    "@TipoPrecioAAA"
                };

                object[] Valores = {
                    CL.Id_Emp,
                    CL.Id_Cd,
                    CL.Id_Motivo,
                    FechaIni,
                    FechaFin,
                    CL.TipoAAA
                };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spConsultaComprasLocalesXTipoAAA", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    CompraLocal Obj = new CompraLocal();
                    Obj.Id_Cd = Convert.ToInt32(dr[dr.GetOrdinal("Id_Cd")].ToString());
                    Obj.Cd_Nombre = dr.GetValue(dr.GetOrdinal("Cd_Nombre")).ToString();
                    Obj.Id_Prd = Convert.ToInt64(dr[dr.GetOrdinal("Id_Prd")].ToString());
                    Obj.Prd_Descripcion = dr.GetValue(dr.GetOrdinal("Prd_Descripcion")).ToString();
                    Obj.Prd_Presentacion = dr.GetValue(dr.GetOrdinal("Prd_Presentacion")).ToString();
                    Obj.TipoSolicitud = dr.GetValue(dr.GetOrdinal("TipoSolicitud")).ToString();
                    Obj.PrecioAAACL = Convert.ToDouble(dr[dr.GetOrdinal("AAACL")].ToString());
                    Obj.PrecioAAAKey = Convert.ToDouble(dr[dr.GetOrdinal("AAAKEY")].ToString());
                    Obj.TipoAAA = Convert.ToInt32(dr[dr.GetOrdinal("IdPrecioMayor")].ToString());
                    Obj.PrecioMayor = dr.GetValue(dr.GetOrdinal("PrecioMayor")).ToString();
                    Obj.Unidades = Convert.ToInt32(dr[dr.GetOrdinal("Unidades")].ToString());
                    Obj.Pesos = Convert.ToDecimal(dr[dr.GetOrdinal("Pesos")].ToString());


                    ListaComprasLocales.Add(Obj);
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                //Verificador = -1;
            }
        }

        //Fin
        public DataTable ComboMotivos()
        {
            string cnn = ClaseConexion.GetClaseConexion().conexion;
            var dbacc = new dbAccess(cnn);

            SqlParameter[] par =
            {
            };

            var res = dbacc.spExecDataSet("SP_CargaMotivosCL", par).Tables[0];

            return res;
        }

        public void ConsultaComprasLocalesXMotivoDet(CompraLocal CL, ref DataSet dtDatos, DateTime FechaIni, DateTime FechaFin, string Emp_Cnx)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Emp_Cnx);

                string[] Parametros = {
                    "@Id_Emp",
                    "@Id_Cd",
                    "@Id_Motivo",
                    "@FechaInicio",
                    "@FechaFin"
                };

                object[] Valores = {
                    CL.Id_Emp,
                    CL.Id_Cd,
                    CL.Id_Motivo,
                    FechaIni,
                    FechaFin
                };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spConsultaComprasLocalesXMotivoDet", ref dtDatos, Parametros, Valores);

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
                //Verificador = 1;
            }
            catch (Exception ex)
            {
                //Verificador = -1;
            }
        }

        public void DescargaReporteCL(int id_Emp, int id_Cd, DateTime fechaInicial, DateTime fechaFinal, int motivoCL, int tipoProductoCL, int aplicacionCL, int productoCentralCL, string proveedorCentralCL, int proveedorLocalCL, ref DataSet dsDatos, string emp_Cnx)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(emp_Cnx);

                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@FechaInicio", "@FechaFin", "@Id_Motivo", "@IdTipoProducto", "@IdAplicacion", "@IdProductoCentral", "@IdProveedorCentral", "@IdProveedorLocal" };
                object[] Valores = { id_Emp, id_Cd, fechaInicial, fechaFinal, motivoCL, tipoProductoCL, aplicacionCL, productoCentralCL, proveedorCentralCL, proveedorLocalCL };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("[spReporteComprasLocales_CL_V1]", ref dsDatos, Parametros, Valores);
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {

            }
        }

        public void LlenaCombo(int var, string conexion, string sp, ref List<Comun> lista)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(conexion);

                string[] Parametros = { "@id1", "@Id2", "@Id3" };

                object[] Valores = { 0, 0, 0 };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand(sp, ref dr, Parametros, Valores);

                Comun Comun = default(Comun);
                while (dr.Read())
                {
                    Comun = new Comun();
                    Comun.Id = dr.GetInt32(dr.GetOrdinal("Id"));
                    Comun.Descripcion = dr.GetString(dr.GetOrdinal("Descripcion"));
                    lista.Add(Comun);
                }
                dr.Close();

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}