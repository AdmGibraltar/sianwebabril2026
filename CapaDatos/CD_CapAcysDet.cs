
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaModelo;
using System.Data.SqlClient;
using System.Data;
using CapaEntidad;

namespace CapaDatos
{
    public class CD_CapAcysDet
    {
        List<string> Parametros = new List<string>();
        List<object> Valores = new List<object>();
        public CD_CapAcysDet(ICD_Contexto icdCtx)
        {
            _icdCtx = icdCtx;
        }
        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        // 9 Ene 2019 

        public int Parametro(string Parametro, object Valor)
        {
            Parametros.Add(Parametro);
            Valores.Add(Valor);
            return 0;
        }


        public CD_CapAcysDet(string cadenaDeConexion)
        {
            _cadenaDeConexion = cadenaDeConexion;
        }

#warning "Esta versión del método no cumple con el requerimiento. Su existencia tiene propósitos de compatiblidad y para la compilación exitosa de su implementación actual. Se recomienda retirar esta versión y sustituirla por su versión sobrecargada que acepta el identificador del tipo de garantía."

        /// <summary>
        /// Consulta la existencia de un producto asociado a un ACYS.
        /// </summary>
        /// <param name="idEmp"></param>
        /// <param name="idCd"></param>
        /// <param name="idPrd"></param>
        /// <param name="idTer"></param>
        /// <param name="idCte"></param>
        /// <param name="idRik"></param>
        /// <returns></returns>
        public CapAcysDet ConsultarPorProducto(int idEmp, int idCd, long idPrd, int idTer, int idCte, int idRik)
        {
            CapAcysDet ret = null;
            using (sianwebmty_gEntities ctx = new sianwebmty_gEntities(_cadenaDeConexion))
            {
                var res = ctx.spCapAcysDet_ConsultarPorProducto(idEmp, idCd, idPrd, idTer, idCte, idRik).ToList();
                if (res.Count > 0)
                {
                    ret = res[0];
                }
            }
            return ret;
        }

        /// <summary>
        /// Esta version reemplaza la de arriba sin utilizar EF.
        /// </summary>
        /// <param name="idEmp"></param>
        /// <param name="idCd"></param>
        /// <param name="idPrd"></param>
        /// <param name="idTer"></param>
        /// <param name="idCte"></param>
        /// <param name="idRik"></param>
        /// <returns></returns>
        public CapAcysDet Consultar_PorProducto(int idEmp, int idCd, long idPrd, int idTer, int idCte, int idRik)
        {
            CapAcysDet obj = new CapAcysDet();

            SqlDataReader dr = null;
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(_cadenaDeConexion);

            string[] Parametros = { "@Id_Emp",
                                    "@Id_Cd",
                                    "@Id_Prd",
                                    "@Id_Ter",
                                    "@Id_Cte",
                                    "@Id_Rik"};

            object[] Valores = { idEmp, idCd, idPrd, idTer, idCte, idRik };

            SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapAcysDet_ConsultarPorProducto", ref dr, Parametros, Valores);

            if (dr.Read())
            {
                obj.Id_Emp = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Emp")));
                obj.Id_Cd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cd")));
                obj.Id_Acs = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Acs")));
                obj.Id_AcsDet = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_AcsDet")));
                obj.Id_Reg = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Reg")));
                obj.Id_Prd = Convert.ToInt64(dr.GetValue(dr.GetOrdinal("Id_Prd")));

                obj.Acs_Cantidad = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_Cantidad")));
                obj.Acs_Frecuencia = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_Frecuencia")));

                obj.Acs_Lunes = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_Lunes"))) == 1 ? true : false;
                obj.Acs_Martes = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_Martes"))) == 1 ? true : false;
                obj.Acs_Miercoles = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_Miercoles"))) == 1 ? true : false;
                obj.Acs_Miercoles = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_Miercoles"))) == 1 ? true : false;
                obj.Acs_Jueves = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_Jueves"))) == 1 ? true : false;
                obj.Acs_Viernes = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_Viernes"))) == 1 ? true : false;
                obj.Acs_Sabado = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_Sabado"))) == 1 ? true : false;

                obj.Acs_Documento = dr.GetValue(dr.GetOrdinal("Acs_Documento")).ToString();
                obj.Acs_Precio = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Acs_Precio")));

                obj.Id_Ter = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Ter")));
                obj.Acs_UltACpt = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_UltACpt")));
                obj.Acs_UltSCpt = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_UltSCpt")));

                obj.Acs_Modalidad = dr.GetValue(dr.GetOrdinal("Acs_Documento")).ToString();
                obj.Acs_ConsigFechaInicio = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("Acs_ConsigFechaInicio")));
                obj.Acs_ConsigFechaFin = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("Acs_ConsigFechaFin")));
                obj.Acs_canTTotal = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_canTTotal")));
                obj.Id_AcsVersion = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_AcsVersion")));
                obj.Id_TG = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_TG")));

                obj.Id_AcsVersion = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_AcsVersion")));

            }
            else
            {
                obj = null;
            }

            dr.Close();
            CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            return obj;
        }

        //
        public List<CapaEntidad.eCapAcysDet> Consulta_ProductosDeACYS(int idEmp, int idCd, int idCte, int idAcys, int idTer)
        {

            List<CapaEntidad.eCapAcysDet> Lst = new List<CapaEntidad.eCapAcysDet>();

            try
            {


                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(_cadenaDeConexion);

                string[] Parametros = { "@Id_Emp",
                                    "@Id_Cd",
                                    "@Id_Acys",
                                    "@Id_Cte",
                                    "@Id_Ter"};

                object[] Valores = { idEmp, idCd, idAcys, idCte, idTer };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("SP_Consulta_CapAcysDet_PorId", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    CapaEntidad.eCapAcysDet obj = new CapaEntidad.eCapAcysDet();

                    obj.Id_Emp = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Emp")));
                    obj.Id_Cd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cd")));
                    obj.Id_Acs = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Acs")));
                    obj.Id_AcsDet = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_AcsDet")));
                    obj.Id_Reg = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Reg")));
                    obj.Id_Prd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Prd")));

                    obj.Acs_Cantidad = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_Cantidad")));
                    obj.Acs_Frecuencia = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_Frecuencia")));
                    obj.Acs_FrecuenciaTipo = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_FrecuenciaTipo")));
                    obj.Acs_Lunes = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_Lunes"))) == 1 ? true : false;
                    obj.Acs_Martes = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_Martes"))) == 1 ? true : false;
                    obj.Acs_Miercoles = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_Miercoles"))) == 1 ? true : false;
                    obj.Acs_Miercoles = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_Miercoles"))) == 1 ? true : false;
                    obj.Acs_Jueves = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_Jueves"))) == 1 ? true : false;
                    obj.Acs_Viernes = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_Viernes"))) == 1 ? true : false;
                    obj.Acs_Sabado = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_Sabado"))) == 1 ? true : false;

                    obj.Acs_Documento = dr.GetValue(dr.GetOrdinal("Acs_Documento")).ToString();
                    obj.Acs_Precio = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Acs_Precio")));

                    obj.Id_Ter = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Ter")));
                    obj.Acs_UltACpt = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_UltACpt")));
                    obj.Acs_UltSCpt = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_UltSCpt")));

                    obj.Acs_Modalidad = dr.GetValue(dr.GetOrdinal("Acs_Documento")).ToString();
                    obj.Acs_ConsigFechaInicio = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("Acs_ConsigFechaInicio")));
                    obj.Acs_ConsigFechaFin = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("Acs_ConsigFechaFin")));
                    obj.Acs_canTTotal = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_canTTotal")));
                    obj.Id_AcsVersion = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_AcsVersion")));
                    obj.Id_TG = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_TG")));

                    obj.Id_AcsVersion = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_AcsVersion")));

                    Lst.Add(obj);
                }

                dr.Close();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                Lst = null;
            }

            return Lst;
        }

        // 9 Nov 2019 RFH
        // 17 Ene 2019 RFH Updated
        public int InsertUpdate(int Id_Acs, int Id_Acs_Version, eAcysDet2 AD, Sesion sesion)
        {
            int iResult = 0;
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(_cadenaDeConexion);

                Parametro("@Id_Emp", sesion.Id_Emp);
                Parametro("@Id_Cd", sesion.Id_Cd);
                Parametro("@Id_Acs", Id_Acs);
                Parametro("@Id_AcsDet", AD.Id_AcsDet);
                Parametro("@Id_Reg", AD.Id_Reg);
                Parametro("@Id_Prd", AD.Id_Prd);
                Parametro("@Acs_Cantidad", AD.Acs_Cantidad);
                Parametro("@Acs_Frecuencia", AD.Acs_Frecuencia);
                Parametro("@Acs_FrecuenciaTipo", AD.Acs_FrecuenciaTipo);
                Parametro("@Acs_Lunes", AD.Acs_Lunes);
                Parametro("@Acs_Martes", AD.Acs_Martes);
                Parametro("@Acs_Miercoles", AD.Acs_Miercoles);
                Parametro("@Acs_Jueves", AD.Acs_Jueves);
                Parametro("@Acs_Viernes", AD.Acs_Viernes);
                Parametro("@Acs_Sabado", AD.Acs_Sabado);
                Parametro("@Acs_Documento", AD.Acs_Documento);
                Parametro("@Acs_Precio", AD.Acs_Precio);
                Parametro("@Id_Ter", AD.Id_Ter);
                Parametro("@Acs_UltSCpt", AD.Acs_UltSCpt);
                Parametro("@Acs_UltACpt", AD.Acs_UltACpt);
                Parametro("@Acs_Modalidad", AD.Acs_Modalidad);
                Parametro("@Acs_ConsigFechaInicio", AD.Acs_ConsigFechaInicio);
                Parametro("@Acs_ConsigFechaFin", AD.Acs_ConsigFechaFin);
                Parametro("@Acs_canTTotal", AD.Acs_canTTotal);
                Parametro("@Id_AcsVersion", AD.Id_AcsVersion);
                Parametro("@Id_TG", AD.Id_TG);
                Parametro("@RequiereOC", AD.RequiereOC);
                Parametro("@Acs_FrecMesIni", AD.Acs_FrecMesIni);
                Parametro("@Acs_FrecAnioIni", AD.Acs_FrecAnioIni);

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("SP_CapAcysDet_InsertUpdate", ref dr, Parametros.ToArray(), Valores.ToArray());

                if (dr.HasRows)
                {
                    dr.Read();
                    iResult = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("AfecteRows")));
                }
                dr.Close();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                iResult = -1;
            }

            return iResult;
        }


        //05/03/2024
        public int Update_RptVti(int Id_Acs, int Id_Acs_Version, eAcysDet2 AD, Sesion sesion)
        {
            int iResult = 0;
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(_cadenaDeConexion);

                Parametro("@Id_Emp", sesion.Id_Emp);
                Parametro("@Id_Cd", sesion.Id_Cd);
                Parametro("@Id_Acs", Id_Acs);
                Parametro("@Id_Rik", sesion.Id_Rik);
                Parametro("@Id_AcsDet", AD.Id_AcsDet);
                Parametro("@Id_Reg", AD.Id_Reg);
                Parametro("@Id_Prd", AD.Id_Prd);
                Parametro("@Acs_Cantidad", AD.Acs_Cantidad);
                Parametro("@Acs_Frecuencia", AD.Acs_Frecuencia);
                Parametro("@Acs_FrecuenciaTipo", AD.Acs_FrecuenciaTipo);
                Parametro("@Acs_Lunes", AD.Acs_Lunes);
                Parametro("@Acs_Martes", AD.Acs_Martes);
                Parametro("@Acs_Miercoles", AD.Acs_Miercoles);
                Parametro("@Acs_Jueves", AD.Acs_Jueves);
                Parametro("@Acs_Viernes", AD.Acs_Viernes);
                Parametro("@Acs_Sabado", AD.Acs_Sabado);
                Parametro("@Acs_Documento", AD.Acs_Documento);
                Parametro("@Acs_Precio", AD.Acs_Precio);
                Parametro("@Id_Ter", AD.Id_Ter);
                Parametro("@Acs_UltSCpt", AD.Acs_UltSCpt);
                Parametro("@Acs_UltACpt", AD.Acs_UltACpt);
                Parametro("@Acs_Modalidad", AD.Acs_Modalidad);
                Parametro("@Acs_ConsigFechaInicio", AD.Acs_ConsigFechaInicio);
                Parametro("@Acs_ConsigFechaFin", AD.Acs_ConsigFechaFin);
                Parametro("@Acs_canTTotal", AD.Acs_canTTotal);
                Parametro("@Id_AcsVersion", AD.Id_AcsVersion);
                Parametro("@Id_TG", AD.Id_TG);
                Parametro("@RequiereOC", AD.RequiereOC);

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("SP_CapAcysDet_Insert_RptVI", ref dr, Parametros.ToArray(), Valores.ToArray());

                if (dr.HasRows)
                {
                    dr.Read();
                    iResult = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("AfecteRows")));
                }
                dr.Close();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                iResult = -1;
            }

            return iResult;
        }

        public int SP_CapAcysDet_Exist(int idEmp, int idCd, long idPrd, int idacys, string Emp_Cnx)
        {

            SqlDataReader dr = null;
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Emp_Cnx);

            string[] Parametros = { "@Id_Emp",
                                    "@Id_Cd",
                                    "@Id_Acs",
                                    "@Id_Prd"};

            object[] Valores = { idEmp, idCd, idacys, idPrd };

            SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("SP_CapAcysDet_Exist", ref dr, Parametros, Valores);
            int exist = 0;

            if (dr.Read())
            {
                exist = 1;
            }

            dr.Close();
            CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            return exist;
        }

        // 17 Ene 2019 RFH Updated
        public int InsertUpdate_CN(int Id_Acs, int Id_Acs_Version, eAcysDet2 AD, Sesion sesion)
        {
            int iResult = 0;
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(_cadenaDeConexion);
                Parametro("@Id_Matriz", AD.Id_Matriz);
                Parametro("@Id_Acys", AD.Id_Acys);
                Parametro("@Id_Emp", sesion.Id_Emp);
                Parametro("@Id_Cd", sesion.Id_Cd);
                Parametro("@Id_Acs", Id_Acs);
                Parametro("@Id_AcsDet", AD.Id_AcsDet);
                Parametro("@Id_Reg", AD.Id_Reg);
                Parametro("@Id_Prd", AD.Id_Prd);
                Parametro("@Acs_Cantidad", AD.Acs_Cantidad);
                Parametro("@Acs_Frecuencia", AD.Acs_Frecuencia);
                Parametro("@Acs_FrecuenciaTipo", AD.Acs_FrecuenciaTipo);
                Parametro("@Acs_Lunes", AD.Acs_Lunes);
                Parametro("@Acs_Martes", AD.Acs_Martes);
                Parametro("@Acs_Miercoles", AD.Acs_Miercoles);
                Parametro("@Acs_Jueves", AD.Acs_Jueves);
                Parametro("@Acs_Viernes", AD.Acs_Viernes);
                Parametro("@Acs_Sabado", AD.Acs_Sabado);
                Parametro("@Acs_Documento", AD.Acs_Documento);
                Parametro("@Acs_Precio", AD.Acs_Precio);
                Parametro("@Id_Ter", AD.Id_Ter);
                Parametro("@Acs_UltSCpt", AD.Acs_UltSCpt);
                Parametro("@Acs_UltACpt", AD.Acs_UltACpt);
                Parametro("@Acs_Modalidad", AD.Acs_Modalidad);
                Parametro("@Acs_ConsigFechaInicio", AD.Acs_ConsigFechaInicio);
                Parametro("@Acs_ConsigFechaFin", AD.Acs_ConsigFechaFin);
                Parametro("@Acs_canTTotal", AD.Acs_canTTotal);
                Parametro("@Id_AcsVersion", AD.Id_AcsVersion);
                Parametro("@Id_TG", AD.Id_TG);
                Parametro("@RequiereOC", AD.RequiereOC);

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("SP_CapAcysDet_InsertUpdate_CN", ref dr, Parametros.ToArray(), Valores.ToArray());

                if (dr.HasRows)
                {
                    dr.Read();
                    iResult = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("AfecteRows")));
                }
                dr.Close();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                iResult = -1;
            }

            return iResult;
        }

        // 25Mar2020 Insert Update CapAcysDet (Si existe el producto lo Acumula)
        public int InsertUpdateADD(int Id_Acs, int Id_Acs_Version, eAcysDet2 AD, Sesion sesion)
        {
            int iResult = 0;
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(_cadenaDeConexion);

                Parametro("@Id_Emp", sesion.Id_Emp);
                Parametro("@Id_Cd", sesion.Id_Cd);
                Parametro("@Id_Acs", Id_Acs);
                Parametro("@Id_AcsDet", AD.Id_AcsDet);
                Parametro("@Id_Reg", AD.Id_Reg);
                Parametro("@Id_Prd", AD.Id_Prd);
                Parametro("@Acs_Cantidad", AD.Acs_Cantidad);
                Parametro("@Acs_Frecuencia", AD.Acs_Frecuencia);
                Parametro("@Acs_FrecuenciaTipo", AD.Acs_FrecuenciaTipo);
                Parametro("@Acs_Lunes", AD.Acs_Lunes);
                Parametro("@Acs_Martes", AD.Acs_Martes);
                Parametro("@Acs_Miercoles", AD.Acs_Miercoles);
                Parametro("@Acs_Jueves", AD.Acs_Jueves);
                Parametro("@Acs_Viernes", AD.Acs_Viernes);
                Parametro("@Acs_Sabado", AD.Acs_Sabado);
                Parametro("@Acs_Documento", AD.Acs_Documento);
                Parametro("@Acs_Precio", AD.Acs_Precio);
                Parametro("@Id_Ter", AD.Id_Ter);
                Parametro("@Acs_UltSCpt", AD.Acs_UltSCpt);
                Parametro("@Acs_UltACpt", AD.Acs_UltACpt);
                Parametro("@Acs_Modalidad", AD.Acs_Modalidad);
                Parametro("@Acs_ConsigFechaInicio", AD.Acs_ConsigFechaInicio);
                Parametro("@Acs_ConsigFechaFin", AD.Acs_ConsigFechaFin);
                Parametro("@Acs_canTTotal", AD.Acs_canTTotal);
                Parametro("@Id_AcsVersion", AD.Id_AcsVersion);
                Parametro("@Id_TG", AD.Id_TG);
                Parametro("@RequiereOC", AD.RequiereOC);

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("SP_CapAcysDet_InsertUpdate_ADD", ref dr, Parametros.ToArray(), Valores.ToArray());

                if (dr.HasRows)
                {
                    dr.Read();
                    iResult = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("AfecteRows")));
                }
                dr.Close();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                iResult = -1;
            }

            return iResult;
        }


        //
        public int InsertUpdate_MarcarBorrarTodo(
            int Id_Emp, int Id_Cd, int Id_Acs, int Id_AcsVersion, int Tipo, Sesion sesion, int TipoCuenta)
        {
            int iResult = 0;
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(_cadenaDeConexion);

                Parametro("@Id_Emp", Id_Emp);
                Parametro("@Id_Cd", Id_Cd);
                Parametro("@Id_Acs", Id_Acs);
                Parametro("@Id_AcsVersion", Id_AcsVersion);
                Parametro("@Tipo", Tipo);
                Parametro("@TipoCuenta", TipoCuenta);

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("SP_CapAcysDet_MarcarBorrarTodo", ref dr, Parametros.ToArray(), Valores.ToArray());

                if (dr.HasRows)
                {
                    dr.Read();
                    iResult = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("AfecteRows")));
                }
                dr.Close();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                iResult = -1;
            }

            return iResult;
        }

        /// <summary>
        /// Consulta la existencia de un producto asociado al grupo de una garantía en particular del ACYS del cliente.
        /// </summary>
        /// <param name="idEmp"></param>
        /// <param name="idCd"></param>
        /// <param name="idPrd"></param>
        /// <param name="idTer"></param>
        /// <param name="idCte"></param>
        /// <param name="idRik"></param>
        /// <param name="idTg"></param>
        /// <returns></returns>
        public CapAcysDet ConsultarPorProducto(int idEmp, int idCd, long idPrd, int idTer, int idCte, int idRik, int idTg)
        {
            CapAcysDet ret = null;
            using (sianwebmty_gEntities ctx = new sianwebmty_gEntities(_cadenaDeConexion))
            {
                var res = ctx.spCapAcysDet_ConsultarPorProductoYGarantia(idEmp, idCd, idPrd, idTer, idCte, idRik, idTg).ToList();
                if (res.Count > 0)
                {
                    ret = res[0];
                }
            }
            return ret;
        }

        public CapAcysDet Insertar(CapAcysDet entity)
        {
            sianwebmty_gEntities ctx = ((ICD_Contexto<sianwebmty_gEntities>)_icdCtx).Contexto;
            entity = ctx.CapAcysDets.Add(entity);
            return entity;
        }

        public IEnumerable<CapAcysDet> Insertar(IEnumerable<CapAcysDet> entities)
        {
            sianwebmty_gEntities ctx = ((ICD_Contexto<sianwebmty_gEntities>)_icdCtx).Contexto;
            entities = ctx.CapAcysDets.AddRange(entities);
            return entities;
        }

        /// <summary>
        /// Regresa el resultado de la consulta sobre el respositorio CapAcysDet dado el identificador del ACYS, su cliente y territorio. Se infiere que se interesa trabajar sobre la última versión del resultado.
        /// </summary>
        /// <param name="idEmp">Identificador de la empresa</param>
        /// <param name="idCd">Identificador del centro de distribución</param>
        /// <param name="idAcys">Identificador del ACYS</param>
        /// <param name="idCte">Identificador del cliente</param>
        /// <param name="idTerritorio">Identificador del territorio</param>
        /// <param name="icdCtx">Contexto de conexión a la fuente de datos</param>
        /// <returns>IEnumerable[CapAcysDet]</returns>
        public IEnumerable<CapAcysDet> ConsultarPorAcys(int idEmp, int idCd, int idAcys, int idCte, int idTerritorio, ICD_Contexto icdCtx)
        {
            sianwebmty_gEntities ctx = ((ICD_Contexto<sianwebmty_gEntities>)_icdCtx).Contexto;
            var productos = from cad in ctx.CapAcysDets
                            join ca in ctx.CapAcys
                            on new { Id_Emp = cad.Id_Emp, Id_Cd = cad.Id_Cd, Id_Acs = cad.Id_Acs, Id_Ter = cad.Id_Ter, Id_AcsVersion = cad.Id_AcsVersion, Id_Cte = idCte } equals new { Id_Emp = ca.Id_Emp, Id_Cd = ca.Id_Cd, Id_Acs = ca.Id_Acs, Id_Ter = ca.Id_Ter, Id_AcsVersion = ca.Id_AcsVersion, Id_Cte = ca.Id_Cte.Value }
                            group cad by cad.Id_AcsVersion into grp
                            select grp.Where(det => det.Id_Acs == idAcys && det.Id_Cd == idCd && det.Id_Ter == idTerritorio && det.Id_Emp == idEmp && det.Id_AcsVersion == grp.Max(d => d.Id_AcsVersion));
            return productos.SelectMany(col => col);
        }

        private ICD_Contexto _icdCtx = null;
        private string _cadenaDeConexion;
    }
}