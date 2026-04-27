using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CapaEntidad;
using System.Data.SqlClient;
using CapaModelo;

namespace CapaDatos
{
    public class CD_CatProducto
    {
        // 26JUN2019 RFH  
        List<string> Parametros = new List<string>();
        List<object> Valores = new List<object>();

        public int Parametro(string Parametro, object Valor)
        {
            Parametros.Add(Parametro);
            Valores.Add(Valor);
            return 0;
        }

        // 10Dic-2021 RFH  
        // Consulta de Productos Substituto                               
        public List<Producto> spCaptacionPedido_ProductosSubstituto(long Id_Prd, int Id_Cd, string Conexion)
        {
            List<Producto> Lst = new List<Producto>();
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = { "@Id_Cd", "@Id_Prd" };
                object[] Valores = { Id_Cd, Id_Prd };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCaptacionPedido_ProductosSubstituto", ref dr, Parametros, Valores);
                if (!dr.HasRows)
                {
                    //NO TIENE PRODUCTOS SUBSTITUTO
                    Lst = null;
                }
                else
                    while (dr.Read())
                    {
                        Producto P = new Producto();
                        P.Id_Prd = Convert.ToInt64(dr.GetValue(dr.GetOrdinal("Id_Prd")));
                        P.Prd_Descripcion = dr.GetValue(dr.GetOrdinal("Prd_Descripcion")).ToString();
                        P.Prd_Disponible = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Prd_Disponible")));

                        /*
                        P.Id_Emp = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Emp")));
                        P.Id_Cd = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Cd"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Id_Cd"));
                        P.Id_Prd = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Prd"))) ? 0 : dr.GetInt64(dr.GetOrdinal("Id_Prd"));
                        P.Id_Spo = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Spo"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Id_Spo"));
                        P.Id_Ptp = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Ptp"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Id_Ptp"));
                        P.Id_Cpr = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Cpr"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Id_Cpr"));
                        P.Id_Fam = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Fam"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Id_Fam"));
                        P.Id_Sub = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Sub"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Id_Sub"));
                        P.Id_Pvd = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Pvd"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Id_Pvd"));
                        P.Prd_Descripcion = dr.GetString(dr.GetOrdinal("Prd_Descripcion"));
                        P.Prd_Presentacion = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Presentacion"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_Presentacion"));
                        P.Prd_InvInicial = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_InvInicial"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_InvInicial"));
                        P.Prd_InvFinal = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_InvFinal"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_InvFinal"));
                        P.Prd_AgrupadoSpo = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_AgrupadoSpo"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_AgrupadoSpo"));
                        P.Prd_FactorConv = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_FactorConv"))) ? 0 : Convert.ToSingle(dr.GetValue(dr.GetOrdinal("Prd_FactorConv")));
                        P.Prd_AparatoSisProp = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_AparatoSisProp"))) ? false : dr.GetBoolean(dr.GetOrdinal("Prd_AparatoSisProp"));
                        P.Prd_Fisico = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Fisico"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_Fisico"));
                        P.Prd_Ordenado = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Ordenado"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_Ordenado"));
                        P.Prd_Asignado = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Asignado"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_Asignado"));
                        P.Prd_InvSeg = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_InvSeg"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_InvSeg"));
                        P.Prd_TTrans = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_TTrans"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_TTrans"));
                        P.Prd_TEntre = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_TEntre"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_TEntre"));
                        P.Prd_Transito = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Transito"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_Transito"));
                        P.Prd_UniNe = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_UniNe"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_UniNe"));
                        P.Prd_UniNs = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_UniNs"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_UniNs"));
                        P.Prd_InvInicial = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_InvInicial"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_InvInicial"));
                        P.Prd_InvFinal = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_InvFinal"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_InvFinal"));
                        P.Prd_Unico = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Unico"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_Unico"));
                        P.Prd_UniEmp = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_UniEmp"))) ? 0 : Convert.ToSingle(dr.GetValue(dr.GetOrdinal("Prd_UniEmp")));
                        P.Prd_Colo = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Colo"))) ? false : dr.GetBoolean(dr.GetOrdinal("Prd_Colo"));
                        P.Prd_Ren = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Ren"))) ? Convert.ToChar(string.Empty) : Convert.ToChar(dr.GetValue(dr.GetOrdinal("Prd_Ren")));
                        P.Prd_Mes = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Mes"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_Mes"));
                        P.Prd_CLNomFab = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_CLNomFab"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_CLNomFab"));
                        P.Prd_CLIdFab = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_CLIdFab"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_CLIdFab"));
                        P.Prd_CLDesFab = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_CLDesFab"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_CLDesFab"));
                        P.Prd_CLPreFab = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_CLPreFab"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_CLPreFab"));
                        P.Prd_CLNomPro = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_CLNomPro"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_CLNomPro"));
                        P.Prd_CLIdPro = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_CLIdPro"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_CLIdPro"));
                        P.Prd_CLDesPro = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_CLDesPro"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_CLDesPro"));
                        P.Prd_CLPrePro = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_CLPrePro"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_CLPrePro"));
                        P.Prd_MaxExistencia = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_MaxExistencia"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_MaxExistencia"));
                        P.Prd_Ubicacion = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Ubicacion"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_Ubicacion"));
                        P.Prd_Contribucion = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Contribucion"))) ? 0 : Convert.ToSingle(dr.GetValue(dr.GetOrdinal("Prd_Contribucion")));
                        P.Prd_PorUtilidades = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_PorUtilidades"))) ? 0 : Convert.ToSingle(dr.GetValue(dr.GetOrdinal("Prd_PorUtilidades")));
                        P.Prd_Nuevo = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Nuevo"))) ? true : dr.GetBoolean(dr.GetOrdinal("Prd_Nuevo"));
                        P.Prd_PesConTecnico = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_PesConTecnico"))) ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Prd_PesConTecnico")));
                        P.Prd_CptSv = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_CptSv"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_CptSv"));
                        P.Prd_Activo = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Activo"))) ? false : dr.GetBoolean(dr.GetOrdinal("Prd_Activo"));
                        */
                        Lst.Add(P);
                    }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                Lst = null;
            }
            return Lst;
        }

        /// <summary>
        /// Consulta un producto según la orden de compra donde esta registrado.
        /// </summary>
        public void ConsultaProducto_OrdenCompra(ref Producto producto, string Conexion, int id_Ord, long id_Prd, int id_Emp, int id_Cd)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Ord", "@Id_Prd", "@Id_Emp", "@Id_Cd_Ver" };
                object[] Valores = { id_Ord, id_Prd, id_Emp, id_Cd };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatProducto_OrdenCompra_Consulta", ref dr, Parametros, Valores);

                if (!dr.HasRows)
                {
                    throw new Exception("ProductoBuscarNoExiste");
                }
                else
                    while (dr.Read())
                    {
                        producto = new Producto();
                        producto.Id_Emp = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Emp")));
                        producto.Id_Cd = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Cd"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Id_Cd"));
                        producto.Id_Prd = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Prd"))) ? 0 : dr.GetInt64(dr.GetOrdinal("Id_Prd"));
                        producto.Id_Spo = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Spo"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Id_Spo"));
                        producto.Id_Ptp = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Ptp"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Id_Ptp"));
                        producto.Id_Cpr = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Cpr"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Id_Cpr"));
                        producto.Id_Fam = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Fam"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Id_Fam"));
                        producto.Id_Sub = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Sub"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Id_Sub"));
                        producto.Id_Pvd = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Pvd"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Id_Pvd"));
                        producto.Prd_Descripcion = dr.GetString(dr.GetOrdinal("Prd_Descripcion"));
                        producto.Prd_Presentacion = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Presentacion"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_Presentacion"));

                        producto.Prd_InvInicial = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_InvInicial"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_InvInicial"));
                        producto.Prd_InvFinal = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_InvFinal"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_InvFinal"));
                        producto.Prd_AgrupadoSpo = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_AgrupadoSpo"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_AgrupadoSpo"));
                        producto.Prd_FactorConv = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_FactorConv"))) ? 0 : Convert.ToSingle(dr.GetValue(dr.GetOrdinal("Prd_FactorConv")));
                        producto.Prd_AparatoSisProp = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_AparatoSisProp"))) ? false : dr.GetBoolean(dr.GetOrdinal("Prd_AparatoSisProp"));
                        producto.Prd_Fisico = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Fisico"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_Fisico"));
                        producto.Prd_Ordenado = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Ordenado"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_Ordenado"));
                        producto.Prd_Asignado = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Asignado"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_Asignado"));
                        producto.Prd_InvSeg = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_InvSeg"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_InvSeg"));
                        producto.Prd_TTrans = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_TTrans"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_TTrans"));
                        producto.Prd_TEntre = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_TEntre"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_TEntre"));
                        producto.Prd_Transito = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Transito"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_Transito"));
                        producto.Prd_UniNe = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_UniNe"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_UniNe"));
                        producto.Prd_UniNs = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_UniNs"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_UniNs"));

                        producto.Prd_InvInicial = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_InvInicial"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_InvInicial"));
                        producto.Prd_InvFinal = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_InvFinal"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_InvFinal"));
                        producto.Prd_Unico = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Unico"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_Unico"));
                        producto.Prd_UniEmp = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_UniEmp"))) ? 0 : Convert.ToSingle(dr.GetValue(dr.GetOrdinal("Prd_UniEmp")));
                        producto.Prd_Colo = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Colo"))) ? false : dr.GetBoolean(dr.GetOrdinal("Prd_Colo"));
                        producto.Prd_Ren = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Ren"))) ? Convert.ToChar(string.Empty) : Convert.ToChar(dr.GetValue(dr.GetOrdinal("Prd_Ren")));
                        producto.Prd_Mes = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Mes"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_Mes"));
                        producto.Prd_CLNomFab = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_CLNomFab"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_CLNomFab"));
                        producto.Prd_CLIdFab = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_CLIdFab"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_CLIdFab"));
                        producto.Prd_CLDesFab = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_CLDesFab"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_CLDesFab"));
                        producto.Prd_CLPreFab = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_CLPreFab"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_CLPreFab"));
                        producto.Prd_CLNomPro = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_CLNomPro"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_CLNomPro"));
                        producto.Prd_CLIdPro = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_CLIdPro"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_CLIdPro"));
                        producto.Prd_CLDesPro = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_CLDesPro"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_CLDesPro"));
                        producto.Prd_CLPrePro = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_CLPrePro"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_CLPrePro"));
                        producto.Prd_MaxExistencia = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_MaxExistencia"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_MaxExistencia"));
                        producto.Prd_Ubicacion = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Ubicacion"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_Ubicacion"));
                        producto.Prd_Contribucion = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Contribucion"))) ? 0 : Convert.ToSingle(dr.GetValue(dr.GetOrdinal("Prd_Contribucion")));
                        producto.Prd_PorUtilidades = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_PorUtilidades"))) ? 0 : Convert.ToSingle(dr.GetValue(dr.GetOrdinal("Prd_PorUtilidades")));
                        producto.Prd_Nuevo = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Nuevo"))) ? true : dr.GetBoolean(dr.GetOrdinal("Prd_Nuevo"));
                        producto.Prd_PesConTecnico = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_PesConTecnico"))) ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Prd_PesConTecnico")));
                        producto.Prd_CptSv = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_CptSv"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_CptSv"));
                        producto.Prd_Activo = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Activo"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_Activo"));  //Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Activo"))) ? false : dr.GetBoolean(dr.GetOrdinal("Prd_Activo"));
                    }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ConsultaProductoInventario(ref int verificador, string Conexion, int id_Emp, int id_Cd, int Id_Es, int Es_Naturaleza, int Id_EsDet)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = {   "@Id_Emp",
                                          "@Id_Cd",
                                          "@Id_Es",
                                          "@Es_Naturaleza",
                                          "@Id_EsDet"
                                      };
                object[] Valores = {   id_Emp,
                                       id_Cd <= 0 ? (object)null : id_Cd,
                                       Id_Es,
                                       Es_Naturaleza,
                                       Id_EsDet
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatProducto_ConsultaInventario", ref verificador, Parametros, Valores);
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaProducto(ref Producto producto, string Conexion, int id_Emp, int id_Cd, int id_Cd_Ver, long id_Prd, int ValidaInv)
        {
            try
            {

                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_Cd_Ver", "@Id_Prd", "@Id_Ter", "@Id_Cte", "@EmpBen", "@ValidaInv", "@Id_Mov", "@SisProp" };
                // string[] Parametros = { "@Id_Emp", "@Id_cd", "@Id_Prd", "@Id_Cte", "@Id_Ter", "@Id_Mov", "@SisProp", "@EmpBen", "@ValidaInactivo" };
                object[] Valores = { id_Emp,
                                       id_Cd <= 0 ? (object)null : id_Cd,
                                       id_Cd_Ver,
                                       id_Prd,
                                       (producto == null) ? (object)null: (producto.Id_Ter == null) ? (object)null: producto.Id_Ter,
                                       (producto == null) ? (object)null: (producto.Id_Cte == null) ? (object)null: producto.Id_Cte,
                                       (producto == null) ? (object)null: producto.EmpBen,
                                       ValidaInv,
                                           producto == null ? (object)null : producto.Id_Mov,
                                           producto == null ? (object)null : producto.Prd_AparatoSisProp
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatProducto_Consulta", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    producto = new Producto();

                    producto.Id_Emp = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Emp")));
                    producto.Id_Cd = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Cd"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Id_Cd"));
                    producto.Id_Prd = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Prd"))) ? 0 : dr.GetInt64(dr.GetOrdinal("Id_Prd"));
                    producto.Id_Spo = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Spo"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Id_Spo"));
                    producto.Id_Ptp = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Ptp"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Id_Ptp"));
                    producto.Id_Cpr = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Cpr"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Id_Cpr"));
                    producto.Id_Fam = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Fam"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Id_Fam"));
                    producto.Id_Sub = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Sub"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Id_Sub"));
                    producto.Id_Pvd = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Pvd"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Id_Pvd"));
                    producto.Prd_Descripcion = dr.GetString(dr.GetOrdinal("Prd_Descripcion"));
                    producto.Prd_Presentacion = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Presentacion"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_Presentacion"));

                    producto.Prd_InvInicial = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_InvInicial"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_InvInicial"));
                    producto.Prd_InvFinal = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_InvFinal"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_InvFinal"));
                    producto.Prd_AgrupadoSpo = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_AgrupadoSpo"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_AgrupadoSpo"));
                    producto.Prd_FactorConv = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_FactorConv"))) ? 0 : Convert.ToSingle(dr.GetValue(dr.GetOrdinal("Prd_FactorConv")));
                    producto.Prd_AparatoSisProp = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_AparatoSisProp"))) ? false : dr.GetBoolean(dr.GetOrdinal("Prd_AparatoSisProp"));
                    producto.Prd_Fisico = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Fisico"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_Fisico"));
                    producto.Prd_Ordenado = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Ordenado"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_Ordenado"));
                    producto.Prd_Asignado = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Asignado"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_Asignado"));
                    producto.Prd_InvSeg = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_InvSeg"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_InvSeg"));
                    producto.Prd_TTrans = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_TTrans"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_TTrans"));
                    producto.Prd_TEntre = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_TEntre"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_TEntre"));
                    producto.Prd_Transito = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Transito"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_Transito"));
                    producto.Prd_UniNe = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_UniNe"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_UniNe"));
                    producto.Prd_UniNs = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_UniNs"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_UniNs"));

                    producto.Prd_InvInicial = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_InvInicial"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_InvInicial"));
                    producto.Prd_InvFinal = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_InvFinal"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_InvFinal"));
                    producto.Prd_Unico = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Unico"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_Unico"));
                    producto.Prd_UniEmp = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_UniEmp"))) ? 0 : Convert.ToSingle(dr.GetValue(dr.GetOrdinal("Prd_UniEmp")));
                    producto.Prd_Colo = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Colo"))) ? false : dr.GetBoolean(dr.GetOrdinal("Prd_Colo"));
                    producto.Prd_Ren = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Ren"))) ? Convert.ToChar(' ') : Convert.ToChar(dr.GetValue(dr.GetOrdinal("Prd_Ren")));
                    producto.Prd_Mes = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Mes"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_Mes"));
                    producto.Prd_CLNomFab = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_CLNomFab"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_CLNomFab"));
                    producto.Prd_CLIdFab = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_CLIdFab"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_CLIdFab"));
                    producto.Prd_CLDesFab = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_CLDesFab"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_CLDesFab"));
                    producto.Prd_CLPreFab = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_CLPreFab"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_CLPreFab"));
                    producto.Prd_CLNomPro = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_CLNomPro"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_CLNomPro"));
                    producto.Prd_CLIdPro = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_CLIdPro"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_CLIdPro"));
                    producto.Prd_CLDesPro = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_CLDesPro"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_CLDesPro"));
                    producto.Prd_CLPrePro = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_CLPrePro"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_CLPrePro"));
                    producto.Prd_MaxExistencia = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_MaxExistencia"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_MaxExistencia"));
                    producto.Prd_Ubicacion = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Ubicacion"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_Ubicacion"));
                    producto.Prd_Contribucion = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Contribucion"))) ? 0 : Convert.ToSingle(dr.GetValue(dr.GetOrdinal("Prd_Contribucion")));
                    producto.Prd_PorUtilidades = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_PorUtilidades"))) ? 0 : Convert.ToSingle(dr.GetValue(dr.GetOrdinal("Prd_PorUtilidades")));
                    producto.Prd_Nuevo = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Nuevo"))) ? true : dr.GetBoolean(dr.GetOrdinal("Prd_Nuevo"));
                    producto.Prd_PesConTecnico = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_PesConTecnico"))) ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Prd_PesConTecnico")));
                    producto.Prd_CptSv = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_CptSv"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_CptSv"));
                    producto.Prd_Activo = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Activo"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_Activo"));  //Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Activo"))) ? false : dr.GetBoolean(dr.GetOrdinal("Prd_Activo"));
                    producto.Prd_Precio = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Precio"))) ? string.Empty : string.Format("{0:0.00}", dr.GetValue(dr.GetOrdinal("Prd_Precio")));
                    producto.Prd_Minimo = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Minimo"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_Minimo"));
                    producto.Prd_PlanAbasto = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_PlanAbasto"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_PlanAbasto"));
                    producto.Prd_AAACadena = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_AAA"))) ? string.Empty : string.Format("{0:0.00}", dr.GetValue(dr.GetOrdinal("Prd_AAA")));

                    producto.VtaUPromedio = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("VtaUPromedio"))) ? 0 : dr.GetInt32(dr.GetOrdinal("VtaUPromedio"));
                    producto.VtaCPromedio = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("VtaCPromedio"))) ? string.Empty : string.Format("{0:0.00}", dr.GetValue(dr.GetOrdinal("VtaCPromedio")));
                    producto.Prd_Notas = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("strNotas"))) ? string.Empty : dr.GetString(dr.GetOrdinal("strNotas")) + "";
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        // especial del nuevo repositorio

        public void ConsultaProducto_CL(ref Producto producto, string Conexion, int id_Emp, int id_Cd, int id_Cd_Ver, long id_Prd, bool catalogo)
        {
            try
            {

                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_Cd_Ver", "@Id_Prd" };
                object[] Valores = { id_Emp,
                                       id_Cd <= 0 ? (object)null : id_Cd,
                                       id_Cd_Ver,
                                       id_Prd
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatProducto_ConsultaCatalogo_CL", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    producto = new Producto();

                    producto.Id_Emp = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Emp")));
                    producto.Id_Cd = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Cd"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Id_Cd"));
                    producto.Id_Prd = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Prd"))) ? 0 : dr.GetInt64(dr.GetOrdinal("Id_Prd"));
                    producto.Id_Spo = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Spo"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Id_Spo"));
                    producto.Id_Ptp = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Ptp"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Id_Ptp"));
                    producto.Id_Cpr = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Cpr"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Id_Cpr"));
                    producto.Id_Fam = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Fam"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Id_Fam"));
                    producto.Id_Sub = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Sub"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Id_Sub"));
                    producto.Id_Pvd = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Pvd"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Id_Pvd"));
                    producto.Prd_Descripcion = dr.GetString(dr.GetOrdinal("Prd_Descripcion"));
                    producto.Prd_Presentacion = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Presentacion"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_Presentacion"));

                    producto.Prd_InvInicial = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_InvInicial"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_InvInicial"));
                    producto.Prd_InvFinal = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_InvFinal"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_InvFinal"));
                    producto.Prd_AgrupadoSpo = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_AgrupadoSpo"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_AgrupadoSpo"));
                    producto.Prd_FactorConv = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_FactorConv"))) ? 0 : Convert.ToSingle(dr.GetValue(dr.GetOrdinal("Prd_FactorConv")));
                    producto.Prd_AparatoSisProp = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_AparatoSisProp"))) ? false : dr.GetBoolean(dr.GetOrdinal("Prd_AparatoSisProp"));
                    producto.Prd_Fisico = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Fisico"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_Fisico"));
                    producto.Prd_Ordenado = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Ordenado"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_Ordenado"));
                    producto.Prd_Asignado = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Asignado"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_Asignado"));
                    producto.Prd_InvSeg = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_InvSeg"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_InvSeg"));
                    producto.Prd_TTrans = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_TTrans"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_TTrans"));
                    producto.Prd_TEntre = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_TEntre"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_TEntre"));
                    producto.Prd_Transito = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Transito"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_Transito"));
                    producto.Prd_UniNe = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_UniNe"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_UniNe"));
                    producto.Prd_UniNs = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_UniNs"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_UniNs"));

                    producto.Prd_InvInicial = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_InvInicial"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_InvInicial"));
                    producto.Prd_InvFinal = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_InvFinal"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_InvFinal"));
                    producto.Prd_Unico = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Unico"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_Unico"));
                    producto.Prd_UniEmp = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_UniEmp"))) ? 0 : Convert.ToSingle(dr.GetValue(dr.GetOrdinal("Prd_UniEmp")));
                    producto.Prd_Colo = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Colo"))) ? false : dr.GetBoolean(dr.GetOrdinal("Prd_Colo"));
                    producto.Prd_Ren = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Ren"))) ? Convert.ToChar(' ') : Convert.ToChar(dr.GetValue(dr.GetOrdinal("Prd_Ren")));
                    producto.Prd_Mes = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Mes"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_Mes"));
                    producto.Prd_CLNomFab = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_CLNomFab"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_CLNomFab"));
                    producto.Prd_CLIdFab = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_CLIdFab"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_CLIdFab"));
                    producto.Prd_CLDesFab = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_CLDesFab"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_CLDesFab"));
                    producto.Prd_CLPreFab = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_CLPreFab"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_CLPreFab"));
                    producto.Prd_CLNomPro = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_CLNomPro"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_CLNomPro"));
                    producto.Prd_CLIdPro = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_CLIdPro"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_CLIdPro"));
                    producto.Prd_CLDesPro = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_CLDesPro"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_CLDesPro"));
                    producto.Prd_CLPrePro = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_CLPrePro"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_CLPrePro"));
                    producto.Prd_MaxExistencia = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_MaxExistencia"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_MaxExistencia"));
                    producto.Prd_Ubicacion = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Ubicacion"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_Ubicacion"));
                    producto.Prd_Contribucion = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Contribucion"))) ? 0 : Convert.ToSingle(dr.GetValue(dr.GetOrdinal("Prd_Contribucion")));
                    producto.Prd_PorUtilidades = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_PorUtilidades"))) ? 0 : Convert.ToSingle(dr.GetValue(dr.GetOrdinal("Prd_PorUtilidades")));
                    producto.Prd_Nuevo = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Nuevo"))) ? true : dr.GetBoolean(dr.GetOrdinal("Prd_Nuevo"));
                    producto.Prd_PesConTecnico = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_PesConTecnico"))) ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Prd_PesConTecnico")));
                    producto.Prd_CptSv = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_CptSv"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_CptSv"));
                    producto.Prd_Activo = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Activo"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_Activo"));  //Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Activo"))) ? false : dr.GetBoolean(dr.GetOrdinal("Prd_Activo"));
                    producto.Prd_Minimo = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Minimo"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_Minimo"));
                    producto.Prd_PlanAbasto = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_PlanAbasto"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_PlanAbasto"));

                    producto.Prd_ClaveProdServ = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_ClaveProdServ"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_ClaveProdServ"));
                    producto.Prd_ClaveUnidad = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_ClaveUnidad"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_ClaveUnidad"));

                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // ENE21-2020 RFH Compras Locales consulta producto 
        // 11Abr2022 RFH Actualizacion 
        public CapaEntidad.Producto ConsultaProductoById(ref int Validador, int id_Emp, int id_Cd, int id_Cd_Ver, long id_Prd, bool catalogo, string Conexion)
        {
            CapaEntidad.Producto producto = new CapaEntidad.Producto();

            Validador = 0;
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_Cd_Ver", "@Id_Prd" };
                object[] Valores = { id_Emp,
                                       id_Cd <= 0 ? (object)null : id_Cd,
                                       id_Cd_Ver,
                                       id_Prd
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatProducto_ConsultaCatalogo_CL", ref dr, Parametros, Valores);

                if (dr.Read())
                {
                    producto.Id_Emp = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Emp")));
                    producto.Id_Cd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cd")));
                    producto.Id_Prd = Convert.ToInt64(dr.GetValue(dr.GetOrdinal("Id_Prd")));
                    producto.Id_Spo = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Spo")));

                    producto.Id_Ptp = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Ptp")));

                    producto.Id_Cpr = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Cpr"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Id_Cpr"));
                    producto.Id_Fam = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Fam"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Id_Fam"));
                    producto.Id_Sub = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Sub"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Id_Sub"));
                    producto.Id_Pvd = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Pvd"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Id_Pvd"));
                    producto.Prd_Descripcion = dr.GetString(dr.GetOrdinal("Prd_Descripcion"));
                    producto.Prd_Presentacion = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Presentacion"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_Presentacion"));

                    producto.Prd_InvInicial = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_InvInicial"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_InvInicial"));
                    producto.Prd_InvFinal = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_InvFinal"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_InvFinal"));
                    producto.Prd_AgrupadoSpo = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_AgrupadoSpo"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_AgrupadoSpo"));
                    producto.Prd_FactorConv = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_FactorConv"))) ? 0 : Convert.ToSingle(dr.GetValue(dr.GetOrdinal("Prd_FactorConv")));
                    producto.Prd_AparatoSisProp = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_AparatoSisProp"))) ? false : dr.GetBoolean(dr.GetOrdinal("Prd_AparatoSisProp"));
                    producto.Prd_Fisico = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Fisico"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_Fisico"));
                    producto.Prd_Ordenado = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Ordenado"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_Ordenado"));
                    producto.Prd_Asignado = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Asignado"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_Asignado"));
                    producto.Prd_InvSeg = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_InvSeg"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_InvSeg"));
                    producto.Prd_TTrans = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_TTrans"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_TTrans"));
                    producto.Prd_TEntre = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_TEntre"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_TEntre"));
                    producto.Prd_Transito = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Transito"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_Transito"));
                    producto.Prd_UniNe = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_UniNe"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_UniNe"));
                    producto.Prd_UniNs = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_UniNs"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_UniNs"));

                    producto.Prd_InvInicial = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_InvInicial"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_InvInicial"));
                    producto.Prd_InvFinal = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_InvFinal"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_InvFinal"));
                    producto.Prd_Unico = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Unico"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_Unico"));
                    producto.Prd_UniEmp = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_UniEmp"))) ? 0 : Convert.ToSingle(dr.GetValue(dr.GetOrdinal("Prd_UniEmp")));
                    producto.Prd_Colo = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Colo"))) ? false : dr.GetBoolean(dr.GetOrdinal("Prd_Colo"));
                    producto.Prd_Ren = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Ren"))) ? Convert.ToChar(' ') : Convert.ToChar(dr.GetValue(dr.GetOrdinal("Prd_Ren")));
                    producto.Prd_Mes = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Mes"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_Mes"));
                    producto.Prd_CLNomFab = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_CLNomFab"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_CLNomFab"));
                    producto.Prd_CLIdFab = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_CLIdFab"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_CLIdFab"));
                    producto.Prd_CLDesFab = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_CLDesFab"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_CLDesFab"));
                    producto.Prd_CLPreFab = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_CLPreFab"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_CLPreFab"));
                    producto.Prd_CLNomPro = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_CLNomPro"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_CLNomPro"));
                    producto.Prd_CLIdPro = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_CLIdPro"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_CLIdPro"));
                    producto.Prd_CLDesPro = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_CLDesPro"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_CLDesPro"));
                    producto.Prd_CLPrePro = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_CLPrePro"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_CLPrePro"));
                    producto.Prd_MaxExistencia = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_MaxExistencia"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_MaxExistencia"));
                    producto.Prd_Ubicacion = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Ubicacion"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_Ubicacion"));
                    producto.Prd_Contribucion = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Contribucion"))) ? 0 : Convert.ToSingle(dr.GetValue(dr.GetOrdinal("Prd_Contribucion")));
                    producto.Prd_PorUtilidades = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_PorUtilidades"))) ? 0 : Convert.ToSingle(dr.GetValue(dr.GetOrdinal("Prd_PorUtilidades")));
                    producto.Prd_Nuevo = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Nuevo"))) ? true : dr.GetBoolean(dr.GetOrdinal("Prd_Nuevo"));
                    producto.Prd_PesConTecnico = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_PesConTecnico"))) ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Prd_PesConTecnico")));
                    producto.Prd_CptSv = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_CptSv"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_CptSv"));
                    producto.Prd_Activo = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Activo"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_Activo"));  //Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Activo"))) ? false : dr.GetBoolean(dr.GetOrdinal("Prd_Activo"));
                    producto.Prd_Minimo = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Minimo"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_Minimo"));
                    producto.Prd_PlanAbasto = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_PlanAbasto"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_PlanAbasto"));


                    producto.Id_ClaveSAT = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_ClaveSAT")));
                    producto.Prd_ClaveProdServ = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_ServicioSat"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_ServicioSat"));
                    producto.Id_UnidadSAT = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_UnidadSAT")));
                    producto.Prd_ClaveUnidad = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_UnidadSat"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_UnidadSat"));

                    producto.Aplicacion = dr.GetValue(dr.GetOrdinal("APLICACION")).ToString();
                    producto.Subfamilia = dr.GetValue(dr.GetOrdinal("SUBFAMILIA")).ToString();

                    // NOV 2023
                    //RBM Se agrega proveedor desde CU
                    producto.Prd_IdProvCentral = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_IdProvCentral"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_IdProvCentral"));
                    producto.Prd_NomProvCentral = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_NomProvCentral"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_NomProvCentral"));
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
                Validador = 1;
            }
            catch (Exception ex)
            {
                producto = null;
                Validador = -1;
            }
            return producto;
        }

        // JUN03-2020 RFH CRM - Consulta Producto del Catalogo

        public CapaEntidad.Producto CRM_BusquedaProducto(
            int Id_Emp, int Id_Cd, int Id_Cte, int Id_Op, long Id_Prd, int TipoBusqueda, string Conexion)
        {
            CapaEntidad.Producto producto = new CapaEntidad.Producto();

            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_Cte", "@Id_Op", "@Id_Prd", "@TipoBusqueda" };
                object[] Valores = { Id_Emp, Id_Cd, Id_Cte, Id_Op, Id_Prd, TipoBusqueda };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCRM_BusquedaProductoCatalogo", ref dr, Parametros, Valores);

                if (dr.Read())
                {
                    //while (dr.Read())

                    producto.Id_Emp = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Emp")));
                    producto.Id_Cd = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Cd"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Id_Cd"));
                    producto.Id_Prd = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Prd"))) ? 0 : dr.GetInt64(dr.GetOrdinal("Id_Prd"));
                    producto.Id_Spo = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Spo"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Id_Spo"));
                    producto.Id_Ptp = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Ptp"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Id_Ptp"));
                    producto.Id_Cpr = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Cpr"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Id_Cpr"));
                    producto.Id_Fam = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Fam"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Id_Fam"));
                    producto.Id_Sub = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Sub"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Id_Sub"));
                    producto.Id_Pvd = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Pvd"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Id_Pvd"));
                    producto.Prd_Descripcion = dr.GetString(dr.GetOrdinal("Prd_Descripcion"));
                    producto.Prd_Presentacion = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Presentacion"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_Presentacion"));

                    producto.ImagenProductoBajaRes = dr.GetValue(dr.GetOrdinal("ImagenProductoBajaRes")).ToString();
                    producto.ImagenProductoAltaRes = dr.GetValue(dr.GetOrdinal("ImagenProductoAltaRes")).ToString();
                    producto.Prd_Activo = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Activo"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_Activo"));// Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Activo"))) ? false : dr.GetBoolean(dr.GetOrdinal("Prd_Activo"));

                    /*
                    producto.Prd_InvInicial = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_InvInicial"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_InvInicial"));
                    producto.Prd_InvFinal = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_InvFinal"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_InvFinal"));
                    producto.Prd_AgrupadoSpo = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_AgrupadoSpo"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_AgrupadoSpo"));
                    producto.Prd_FactorConv = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_FactorConv"))) ? 0 : Convert.ToSingle(dr.GetValue(dr.GetOrdinal("Prd_FactorConv")));
                    producto.Prd_AparatoSisProp = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_AparatoSisProp"))) ? false : dr.GetBoolean(dr.GetOrdinal("Prd_AparatoSisProp"));
                    producto.Prd_Fisico = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Fisico"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_Fisico"));
                    producto.Prd_Ordenado = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Ordenado"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_Ordenado"));
                    producto.Prd_Asignado = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Asignado"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_Asignado"));
                    producto.Prd_InvSeg = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_InvSeg"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_InvSeg"));
                    producto.Prd_TTrans = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_TTrans"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_TTrans"));
                    producto.Prd_TEntre = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_TEntre"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_TEntre"));
                    producto.Prd_Transito = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Transito"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_Transito"));
                    producto.Prd_UniNe = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_UniNe"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_UniNe"));
                    producto.Prd_UniNs = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_UniNs"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_UniNs"));
                    producto.Prd_InvInicial = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_InvInicial"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_InvInicial"));
                    producto.Prd_InvFinal = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_InvFinal"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_InvFinal"));
                    producto.Prd_Unico = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Unico"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_Unico"));
                    producto.Prd_UniEmp = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_UniEmp"))) ? 0 : Convert.ToSingle(dr.GetValue(dr.GetOrdinal("Prd_UniEmp")));
                    producto.Prd_Colo = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Colo"))) ? false : dr.GetBoolean(dr.GetOrdinal("Prd_Colo"));
                    producto.Prd_Ren = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Ren"))) ? Convert.ToChar(' ') : Convert.ToChar(dr.GetValue(dr.GetOrdinal("Prd_Ren")));
                    producto.Prd_Mes = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Mes"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_Mes"));
                    producto.Prd_CLNomFab = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_CLNomFab"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_CLNomFab"));
                    producto.Prd_CLIdFab = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_CLIdFab"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_CLIdFab"));
                    producto.Prd_CLDesFab = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_CLDesFab"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_CLDesFab"));
                    producto.Prd_CLPreFab = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_CLPreFab"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_CLPreFab"));
                    producto.Prd_CLNomPro = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_CLNomPro"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_CLNomPro"));
                    producto.Prd_CLIdPro = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_CLIdPro"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_CLIdPro"));
                    producto.Prd_CLDesPro = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_CLDesPro"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_CLDesPro"));
                    producto.Prd_CLPrePro = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_CLPrePro"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_CLPrePro"));
                    producto.Prd_MaxExistencia = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_MaxExistencia"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_MaxExistencia"));
                    producto.Prd_Ubicacion = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Ubicacion"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_Ubicacion"));
                    producto.Prd_Contribucion = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Contribucion"))) ? 0 : Convert.ToSingle(dr.GetValue(dr.GetOrdinal("Prd_Contribucion")));
                    producto.Prd_PorUtilidades = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_PorUtilidades"))) ? 0 : Convert.ToSingle(dr.GetValue(dr.GetOrdinal("Prd_PorUtilidades")));
                    producto.Prd_Nuevo = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Nuevo"))) ? true : dr.GetBoolean(dr.GetOrdinal("Prd_Nuevo"));
                    producto.Prd_PesConTecnico = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_PesConTecnico"))) ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Prd_PesConTecnico")));
                    producto.Prd_CptSv = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_CptSv"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_CptSv"));
                    producto.Prd_Activo = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Activo"))) ? false : dr.GetBoolean(dr.GetOrdinal("Prd_Activo"));
                    producto.Prd_Minimo = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Minimo"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_Minimo"));
                    producto.Prd_PlanAbasto = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_PlanAbasto"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_PlanAbasto"));
                    producto.Prd_ClaveProdServ = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_ClaveProdServ"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_ClaveProdServ"));
                    producto.Prd_ClaveUnidad = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_ClaveUnidad"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_ClaveUnidad"));
                    */
                }
                else
                {
                    producto = null;
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                //throw ex;
                producto = null;
            }

            return producto;
        }


        public void ConsultaProducto(ref Producto producto, string Conexion, int id_Emp, int id_Cd, int id_Cd_Ver, long id_Prd, bool catalogo)
        {
            try
            {

                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_Cd_Ver", "@Id_Prd" };
                object[] Valores = { id_Emp,
                                       id_Cd <= 0 ? (object)null : id_Cd,
                                       id_Cd_Ver,
                                       id_Prd
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatProducto_ConsultaCatalogo", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    producto = new Producto();

                    producto.Id_Emp = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Emp")));
                    producto.Id_Cd = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Cd"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Id_Cd"));
                    producto.Id_Prd = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Prd"))) ? 0 : dr.GetInt64(dr.GetOrdinal("Id_Prd"));
                    producto.Id_Spo = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Spo"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Id_Spo"));
                    producto.Id_Ptp = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Ptp"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Id_Ptp"));
                    producto.Id_Cpr = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Cpr"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Id_Cpr"));
                    producto.Id_Fam = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Fam"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Id_Fam"));
                    producto.Id_Sub = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Sub"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Id_Sub"));
                    producto.Id_Pvd = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Pvd"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Id_Pvd"));
                    producto.Prd_Descripcion = dr.GetString(dr.GetOrdinal("Prd_Descripcion"));
                    producto.Prd_Presentacion = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Presentacion"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_Presentacion"));

                    producto.Prd_InvInicial = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_InvInicial"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_InvInicial"));
                    producto.Prd_InvFinal = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_InvFinal"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_InvFinal"));
                    producto.Prd_AgrupadoSpo = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_AgrupadoSpo"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_AgrupadoSpo"));
                    producto.Prd_FactorConv = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_FactorConv"))) ? 0 : Convert.ToSingle(dr.GetValue(dr.GetOrdinal("Prd_FactorConv")));
                    producto.Prd_AparatoSisProp = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_AparatoSisProp"))) ? false : dr.GetBoolean(dr.GetOrdinal("Prd_AparatoSisProp"));
                    producto.Prd_Fisico = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Fisico"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_Fisico"));
                    producto.Prd_Ordenado = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Ordenado"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_Ordenado"));
                    producto.Prd_Asignado = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Asignado"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_Asignado"));
                    producto.Prd_InvSeg = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_InvSeg"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_InvSeg"));
                    producto.Prd_TTrans = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_TTrans"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_TTrans"));
                    producto.Prd_TEntre = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_TEntre"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_TEntre"));
                    producto.Prd_Transito = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Transito"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_Transito"));
                    producto.Prd_UniNe = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_UniNe"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_UniNe"));
                    producto.Prd_UniNs = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_UniNs"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_UniNs"));

                    producto.Prd_InvInicial = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_InvInicial"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_InvInicial"));
                    producto.Prd_InvFinal = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_InvFinal"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_InvFinal"));
                    producto.Prd_Unico = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Unico"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_Unico"));
                    producto.Prd_UniEmp = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_UniEmp"))) ? 0 : Convert.ToSingle(dr.GetValue(dr.GetOrdinal("Prd_UniEmp")));
                    producto.Prd_Colo = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Colo"))) ? false : dr.GetBoolean(dr.GetOrdinal("Prd_Colo"));
                    producto.Prd_Ren = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Ren"))) ? Convert.ToChar(' ') : Convert.ToChar(dr.GetValue(dr.GetOrdinal("Prd_Ren")));
                    producto.Prd_Mes = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Mes"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_Mes"));
                    producto.Prd_CLNomFab = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_CLNomFab"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_CLNomFab"));
                    producto.Prd_CLIdFab = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_CLIdFab"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_CLIdFab"));
                    producto.Prd_CLDesFab = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_CLDesFab"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_CLDesFab"));
                    producto.Prd_CLPreFab = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_CLPreFab"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_CLPreFab"));
                    producto.Prd_CLNomPro = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_CLNomPro"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_CLNomPro"));
                    producto.Prd_CLIdPro = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_CLIdPro"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_CLIdPro"));
                    producto.Prd_CLDesPro = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_CLDesPro"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_CLDesPro"));
                    producto.Prd_CLPrePro = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_CLPrePro"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_CLPrePro"));
                    producto.Prd_MaxExistencia = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_MaxExistencia"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_MaxExistencia"));
                    producto.Prd_Ubicacion = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Ubicacion"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_Ubicacion"));
                    producto.Prd_Contribucion = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Contribucion"))) ? 0 : Convert.ToSingle(dr.GetValue(dr.GetOrdinal("Prd_Contribucion")));
                    producto.Prd_PorUtilidades = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_PorUtilidades"))) ? 0 : Convert.ToSingle(dr.GetValue(dr.GetOrdinal("Prd_PorUtilidades")));
                    producto.Prd_Nuevo = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Nuevo"))) ? true : dr.GetBoolean(dr.GetOrdinal("Prd_Nuevo"));
                    producto.Prd_PesConTecnico = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_PesConTecnico"))) ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Prd_PesConTecnico")));
                    producto.Prd_CptSv = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_CptSv"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_CptSv"));
                    producto.Prd_Activo = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Activo"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_Activo"));  //Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Activo"))) ? false : dr.GetBoolean(dr.GetOrdinal("Prd_Activo"));
                    producto.Prd_Minimo = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Minimo"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_Minimo"));
                    producto.Prd_PlanAbasto = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_PlanAbasto"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_PlanAbasto"));
                    producto.Prd_SGCUP = dr.GetString(dr.GetOrdinal("Prd_SGCUP")); ;

                    producto.Prd_ClaveProdServ = dr.GetString(dr.GetOrdinal("Prd_ClaveProdServ"));
                    producto.Prd_ClaveUnidad = dr.GetString(dr.GetOrdinal("Prd_ClaveUnidad"));

                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ConsultaProducto(ref Producto producto, string Conexion, int id_Emp, int Id_Cd, long id_Prd, int ValidaInactivo)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);



                //   if (ValidaInactivo.HasValue)
                //     {

                string[] Parametros = { "@Id_Emp", "@Id_cd", "@Id_Prd", "@Id_Cte", "@Id_Ter", "@Id_Mov", "@SisProp", "@EmpBen", "@ValidaInactivo" };

                object[] Valores = { id_Emp, Id_Cd, id_Prd,
                                           producto == null ? (object)null : producto.Id_Cte,
                                           producto == null ? (object)null : producto.Id_Ter,
                                           producto == null ? (object)null : producto.Id_Mov,
                                           producto == null ? (object)null : producto.Prd_AparatoSisProp,
                                           producto.EmpBen,
                                           ValidaInactivo
                                       };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatProducto_Consultar", ref dr, Parametros, Valores);

                if (dr.HasRows)
                {
                    dr.Read();
                    producto = new Producto();
                    producto.Id_Emp = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Emp")));
                    producto.Id_Cd = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Cd"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Id_Cd"));
                    producto.Id_Prd = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Prd"))) ? 0 : dr.GetInt64(dr.GetOrdinal("Id_Prd"));
                    producto.Id_Spo = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Spo"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Id_Spo"));
                    producto.Id_Ptp = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Ptp"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Id_Ptp"));
                    producto.Id_Cpr = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Cpr"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Id_Cpr"));
                    producto.Id_Fam = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Fam"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Id_Fam"));
                    producto.Id_Sub = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Sub"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Id_Sub"));
                    producto.Id_Pvd = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Pvd"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Id_Pvd"));
                    producto.Prd_Descripcion = dr.GetString(dr.GetOrdinal("Prd_Descripcion"));
                    producto.Prd_Presentacion = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Presentacion"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_Presentacion"));

                    producto.Prd_InvInicial = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_InvInicial"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_InvInicial"));
                    producto.Prd_InvFinal = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_InvFinal"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_InvFinal"));
                    producto.Prd_AgrupadoSpo = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_AgrupadoSpo"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_AgrupadoSpo"));
                    producto.Prd_FactorConv = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_FactorConv"))) ? 0 : Convert.ToSingle(dr.GetValue(dr.GetOrdinal("Prd_FactorConv")));
                    producto.Prd_AparatoSisProp = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_AparatoSisProp"))) ? false : dr.GetBoolean(dr.GetOrdinal("Prd_AparatoSisProp"));
                    producto.Prd_Fisico = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Fisico"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_Fisico"));
                    producto.Prd_Ordenado = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Ordenado"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_Ordenado"));
                    producto.Prd_Asignado = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Asignado"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_Asignado"));
                    producto.Prd_InvSeg = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_InvSeg"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_InvSeg"));
                    producto.Prd_TTrans = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_TTrans"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_TTrans"));
                    producto.Prd_TEntre = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_TEntre"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_TEntre"));
                    producto.Prd_Transito = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Transito"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_Transito"));
                    producto.Prd_UniNe = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_UniNe"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_UniNe"));
                    producto.Prd_UniNs = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_UniNs"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_UniNs"));

                    producto.Prd_InvInicial = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_InvInicial"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_InvInicial"));
                    producto.Prd_InvFinal = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_InvFinal"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_InvFinal"));
                    producto.Prd_Unico = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Unico"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_Unico"));
                    producto.Prd_UniEmp = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_UniEmp"))) ? 0 : Convert.ToSingle(dr.GetValue(dr.GetOrdinal("Prd_UniEmp")));
                    producto.Prd_Colo = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Colo"))) ? false : dr.GetBoolean(dr.GetOrdinal("Prd_Colo"));
                    if (!Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Ren"))))
                    {
                        producto.Prd_Ren = Convert.ToChar(dr.GetValue(dr.GetOrdinal("Prd_Ren")));
                    }

                    //producto.Prd_Ren = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Ren"))) ?  Convert.ToChar(string.Empty) : Convert.ToChar(dr.GetValue(dr.GetOrdinal("Prd_Ren")));
                    producto.Prd_Mes = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Mes"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_Mes"));
                    producto.Prd_CLNomFab = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_CLNomFab"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_CLNomFab"));
                    producto.Prd_CLIdFab = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_CLIdFab"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_CLIdFab"));
                    producto.Prd_CLDesFab = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_CLDesFab"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_CLDesFab"));
                    producto.Prd_CLPreFab = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_CLPreFab"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_CLPreFab"));
                    producto.Prd_CLNomPro = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_CLNomPro"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_CLNomPro"));
                    producto.Prd_CLIdPro = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_CLIdPro"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_CLIdPro"));
                    producto.Prd_CLDesPro = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_CLDesPro"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_CLDesPro"));
                    producto.Prd_CLPrePro = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_CLPrePro"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_CLPrePro"));
                    producto.Prd_MaxExistencia = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_MaxExistencia"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_MaxExistencia"));
                    producto.Prd_Ubicacion = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Ubicacion"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_Ubicacion"));
                    producto.Prd_Contribucion = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Contribucion"))) ? 0 : Convert.ToSingle(dr.GetValue(dr.GetOrdinal("Prd_Contribucion")));
                    producto.Prd_PorUtilidades = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_PorUtilidades"))) ? 0 : Convert.ToSingle(dr.GetValue(dr.GetOrdinal("Prd_PorUtilidades")));
                    producto.Prd_Nuevo = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Nuevo"))) ? true : dr.GetBoolean(dr.GetOrdinal("Prd_Nuevo"));
                    producto.Prd_PesConTecnico = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_PesConTecnico"))) ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Prd_PesConTecnico")));
                    producto.Prd_CptSv = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_CptSv"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_CptSv"));
                    producto.Prd_Activo = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Activo"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_Activo"));  //Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Activo"))) ? false : dr.GetBoolean(dr.GetOrdinal("Prd_Activo"));
                    producto.Prd_Precio = dr.IsDBNull(dr.GetOrdinal("Prd_Precio")) ? (string)null : Convert.ToString(dr.GetValue(dr.GetOrdinal("Prd_Precio")));
                    producto.CantFact = (int?)dr.GetValue(dr.GetOrdinal("CantFact"));
                    producto.UltimaVta = dr.IsDBNull(dr.GetOrdinal("UltimaVta")) ? (DateTime?)null : (DateTime?)dr.GetValue(dr.GetOrdinal("UltimaVta"));
                    producto.Prd_AAA = dr.IsDBNull(dr.GetOrdinal("AAA")) ? 0 : (double?)dr.GetValue(dr.GetOrdinal("AAA"));
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

                /*          }
                          else
                          {
                              string[] Parametros = { "@Id_Emp", "@Id_cd", "@Id_Prd", "@Id_Cte", "@Id_Ter", "@Id_Mov", "@SisProp", "@EmpBen" };
                              object[] Valores = { id_Emp, Id_Cd, id_Prd, 
                                                     producto == null ? (object)null : producto.Id_Cte,
                                                     producto == null ? (object)null : producto.Id_Ter, 
                                                     producto == null ? (object)null : producto.Id_Mov, 
                                                     producto == null ? (object)null : producto.Prd_AparatoSisProp,
                                                     producto.EmpBen
                                                 };
                              SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatProducto_Consultar", ref dr, Parametros, Valores);
                              if (dr.HasRows)
                              {
                                  dr.Read();
                                  producto = new Producto();
                                  producto.Id_Emp = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Emp")));
                                  producto.Id_Cd = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Cd"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Id_Cd"));
                                  producto.Id_Prd = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Prd"))) ? 0 : dr.GetInt64(dr.GetOrdinal("Id_Prd"));
                                  producto.Id_Spo = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Spo"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Id_Spo"));
                                  producto.Id_Ptp = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Ptp"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Id_Ptp"));
                                  producto.Id_Cpr = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Cpr"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Id_Cpr"));
                                  producto.Id_Fam = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Fam"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Id_Fam"));
                                  producto.Id_Sub = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Sub"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Id_Sub"));
                                  producto.Id_Pvd = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Pvd"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Id_Pvd"));
                                  producto.Prd_Descripcion = dr.GetString(dr.GetOrdinal("Prd_Descripcion"));
                                  producto.Prd_Presentacion = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Presentacion"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_Presentacion"));
                                  producto.Prd_InvInicial = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_InvInicial"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_InvInicial"));
                                  producto.Prd_InvFinal = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_InvFinal"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_InvFinal"));
                                  producto.Prd_AgrupadoSpo = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_AgrupadoSpo"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_AgrupadoSpo"));
                                  producto.Prd_FactorConv = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_FactorConv"))) ? 0 : Convert.ToSingle(dr.GetValue(dr.GetOrdinal("Prd_FactorConv")));
                                  producto.Prd_AparatoSisProp = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_AparatoSisProp"))) ? false : dr.GetBoolean(dr.GetOrdinal("Prd_AparatoSisProp"));
                                  producto.Prd_Fisico = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Fisico"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_Fisico"));
                                  producto.Prd_Ordenado = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Ordenado"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_Ordenado"));
                                  producto.Prd_Asignado = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Asignado"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_Asignado"));
                                  producto.Prd_InvSeg = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_InvSeg"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_InvSeg"));
                                  producto.Prd_TTrans = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_TTrans"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_TTrans"));
                                  producto.Prd_TEntre = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_TEntre"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_TEntre"));
                                  producto.Prd_Transito = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Transito"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_Transito"));
                                  producto.Prd_UniNe = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_UniNe"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_UniNe"));
                                  producto.Prd_UniNs = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_UniNs"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_UniNs"));
                                  producto.Prd_InvInicial = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_InvInicial"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_InvInicial"));
                                  producto.Prd_InvFinal = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_InvFinal"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_InvFinal"));
                                  producto.Prd_Unico = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Unico"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_Unico"));
                                  producto.Prd_UniEmp = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_UniEmp"))) ? 0 : Convert.ToSingle(dr.GetValue(dr.GetOrdinal("Prd_UniEmp")));
                                  producto.Prd_Colo = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Colo"))) ? false : dr.GetBoolean(dr.GetOrdinal("Prd_Colo"));
                                  if (!Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Ren"))))
                                  {
                                      producto.Prd_Ren = Convert.ToChar(dr.GetValue(dr.GetOrdinal("Prd_Ren")));
                                  }
                                  //producto.Prd_Ren = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Ren"))) ?  Convert.ToChar(string.Empty) : Convert.ToChar(dr.GetValue(dr.GetOrdinal("Prd_Ren")));
                                  producto.Prd_Mes = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Mes"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_Mes"));
                                  producto.Prd_CLNomFab = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_CLNomFab"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_CLNomFab"));
                                  producto.Prd_CLIdFab = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_CLIdFab"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_CLIdFab"));
                                  producto.Prd_CLDesFab = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_CLDesFab"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_CLDesFab"));
                                  producto.Prd_CLPreFab = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_CLPreFab"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_CLPreFab"));
                                  producto.Prd_CLNomPro = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_CLNomPro"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_CLNomPro"));
                                  producto.Prd_CLIdPro = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_CLIdPro"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_CLIdPro"));
                                  producto.Prd_CLDesPro = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_CLDesPro"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_CLDesPro"));
                                  producto.Prd_CLPrePro = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_CLPrePro"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_CLPrePro"));
                                  producto.Prd_MaxExistencia = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_MaxExistencia"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_MaxExistencia"));
                                  producto.Prd_Ubicacion = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Ubicacion"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_Ubicacion"));
                                  producto.Prd_Contribucion = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Contribucion"))) ? 0 : Convert.ToSingle(dr.GetValue(dr.GetOrdinal("Prd_Contribucion")));
                                  producto.Prd_PorUtilidades = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_PorUtilidades"))) ? 0 : Convert.ToSingle(dr.GetValue(dr.GetOrdinal("Prd_PorUtilidades")));
                                  producto.Prd_Nuevo = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Nuevo"))) ? true : dr.GetBoolean(dr.GetOrdinal("Prd_Nuevo"));
                                  producto.Prd_PesConTecnico = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_PesConTecnico"))) ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Prd_PesConTecnico")));
                                  producto.Prd_CptSv = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_CptSv"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_CptSv"));
                                  producto.Prd_Activo = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Activo"))) ? false : dr.GetBoolean(dr.GetOrdinal("Prd_Activo"));
                                  producto.Prd_Precio = dr.IsDBNull(dr.GetOrdinal("Prd_Precio")) ? (string)null : Convert.ToString(dr.GetValue(dr.GetOrdinal("Prd_Precio")));
                                  producto.CantFact = (int?)dr.GetValue(dr.GetOrdinal("CantFact"));
                                  producto.UltimaVta = dr.IsDBNull(dr.GetOrdinal("UltimaVta")) ? (DateTime?)null : (DateTime?)dr.GetValue(dr.GetOrdinal("UltimaVta"));
                                  producto.Prd_AAA = dr.IsDBNull(dr.GetOrdinal("AAA")) ? 0 : (double?)dr.GetValue(dr.GetOrdinal("AAA"));
                              }
                              CapaDatos.LimpiarSqlcommand(ref sqlcmd);
                          }*/

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaProductos(ref Producto producto, string Conexion, int id_Emp, int Id_Cd, long id_Prd, ref int productoNuevo, int ValidaInactivo)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);


                /*                if (ValidaInactivo.HasValue)
                                {
                                */
                string[] Parametros = { "@Id_Emp", "@Id_cd", "@Id_Prd", "@Id_Cte", "@Id_Ter", "@Id_Mov", "@SisProp", "@Nuevo", "@ValidaInactivo" };
                object[] Valores = { id_Emp, Id_Cd, id_Prd,
                                       producto == null ? (object)null : producto.Id_Cte,
                                       producto == null ? (object)null : producto.Id_Ter,
                                       producto == null ? (object)null : producto.Id_Mov,
                                       producto == null ? (object)null : producto.Prd_AparatoSisProp,
                                       productoNuevo,
                                       ValidaInactivo
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatProducto_Consultar", ref dr, Parametros, Valores);

                if (dr.HasRows)
                {
                    dr.Read();
                    producto = new Producto();
                    producto.Id_Emp = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Emp")));
                    producto.Id_Cd = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Cd"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Id_Cd"));
                    producto.Id_Prd = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Prd"))) ? 0 : Convert.ToInt64(dr.GetOrdinal("Id_Prd").ToString());
                    producto.Id_Spo = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Spo"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Id_Spo"));
                    producto.Id_Ptp = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Ptp"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Id_Ptp"));
                    producto.Id_Cpr = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Cpr"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Id_Cpr"));
                    producto.Id_Fam = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Fam"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Id_Fam"));
                    producto.Id_Sub = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Sub"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Id_Sub"));
                    producto.Id_Pvd = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Pvd"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Id_Pvd"));
                    producto.Prd_Descripcion = dr.GetString(dr.GetOrdinal("Prd_Descripcion"));
                    producto.Prd_Presentacion = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Presentacion"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_Presentacion"));

                    producto.Prd_InvInicial = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_InvInicial"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_InvInicial"));
                    producto.Prd_InvFinal = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_InvFinal"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_InvFinal"));
                    producto.Prd_AgrupadoSpo = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_AgrupadoSpo"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_AgrupadoSpo"));
                    producto.Prd_FactorConv = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_FactorConv"))) ? 0 : Convert.ToSingle(dr.GetValue(dr.GetOrdinal("Prd_FactorConv")));
                    producto.Prd_AparatoSisProp = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_AparatoSisProp"))) ? false : dr.GetBoolean(dr.GetOrdinal("Prd_AparatoSisProp"));
                    producto.Prd_Fisico = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Fisico"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_Fisico"));
                    producto.Prd_Ordenado = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Ordenado"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_Ordenado"));
                    producto.Prd_Asignado = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Asignado"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_Asignado"));
                    producto.Prd_InvSeg = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_InvSeg"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_InvSeg"));
                    producto.Prd_TTrans = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_TTrans"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_TTrans"));
                    producto.Prd_TEntre = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_TEntre"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_TEntre"));
                    producto.Prd_Transito = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Transito"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_Transito"));
                    producto.Prd_UniNe = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_UniNe"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_UniNe"));
                    producto.Prd_UniNs = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_UniNs"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_UniNs"));

                    producto.Prd_InvInicial = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_InvInicial"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_InvInicial"));
                    producto.Prd_InvFinal = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_InvFinal"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_InvFinal"));
                    producto.Prd_Unico = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Unico"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_Unico"));
                    producto.Prd_UniEmp = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_UniEmp"))) ? 0 : Convert.ToSingle(dr.GetValue(dr.GetOrdinal("Prd_UniEmp")));
                    producto.Prd_Colo = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Colo"))) ? false : dr.GetBoolean(dr.GetOrdinal("Prd_Colo"));
                    if (!Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Ren"))))
                    {
                        producto.Prd_Ren = Convert.ToChar(dr.GetValue(dr.GetOrdinal("Prd_Ren")));
                    }
                    producto.Prd_Mes = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Mes"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_Mes"));
                    producto.Prd_CLNomFab = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_CLNomFab"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_CLNomFab"));
                    producto.Prd_CLIdFab = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_CLIdFab"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_CLIdFab"));
                    producto.Prd_CLDesFab = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_CLDesFab"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_CLDesFab"));
                    producto.Prd_CLPreFab = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_CLPreFab"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_CLPreFab"));
                    producto.Prd_CLNomPro = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_CLNomPro"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_CLNomPro"));
                    producto.Prd_CLIdPro = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_CLIdPro"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_CLIdPro"));
                    producto.Prd_CLDesPro = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_CLDesPro"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_CLDesPro"));
                    producto.Prd_CLPrePro = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_CLPrePro"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_CLPrePro"));
                    producto.Prd_MaxExistencia = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_MaxExistencia"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_MaxExistencia"));
                    producto.Prd_Ubicacion = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Ubicacion"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_Ubicacion"));
                    producto.Prd_Contribucion = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Contribucion"))) ? 0 : Convert.ToSingle(dr.GetValue(dr.GetOrdinal("Prd_Contribucion")));
                    producto.Prd_PorUtilidades = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_PorUtilidades"))) ? 0 : Convert.ToSingle(dr.GetValue(dr.GetOrdinal("Prd_PorUtilidades")));
                    producto.Prd_Nuevo = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Nuevo"))) ? true : dr.GetBoolean(dr.GetOrdinal("Prd_Nuevo"));
                    producto.Prd_PesConTecnico = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_PesConTecnico"))) ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Prd_PesConTecnico")));
                    producto.Prd_CptSv = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_CptSv"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_CptSv"));
                    producto.Prd_Activo = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Activo"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_Activo"));  //Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Activo"))) ? false : dr.GetBoolean(dr.GetOrdinal("Prd_Activo"));
                    producto.Prd_Precio = dr.IsDBNull(dr.GetOrdinal("Prd_Precio")) ? (string)null : Convert.ToString(dr.GetValue(dr.GetOrdinal("Prd_Precio")));
                    producto.CantFact = (int?)dr.GetValue(dr.GetOrdinal("CantFact"));
                    producto.UltimaVta = dr.IsDBNull(dr.GetOrdinal("UltimaVta")) ? (DateTime?)null : (DateTime?)dr.GetValue(dr.GetOrdinal("UltimaVta"));
                    producto.Prd_AAA = dr.IsDBNull(dr.GetOrdinal("AAA")) ? 0 : (double?)dr.GetValue(dr.GetOrdinal("AAA"));
                    producto.Prd_PLista = dr.IsDBNull(dr.GetOrdinal("PrecioLista")) ? 0 : (double)dr.GetValue(dr.GetOrdinal("PrecioLista"));
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
                /*  }
                  else
                  {
                      string[] Parametros = { "@Id_Emp", "@Id_cd", "@Id_Prd", "@Id_Cte", "@Id_Ter", "@Id_Mov", "@SisProp", "@Nuevo" };
                      object[] Valores = { id_Emp, Id_Cd, id_Prd, 
                                         producto == null ? (object)null : producto.Id_Cte,
                                         producto == null ? (object)null : producto.Id_Ter, 
                                         producto == null ? (object)null : producto.Id_Mov, 
                                         producto == null ? (object)null : producto.Prd_AparatoSisProp,
                                         productoNuevo
                                     };
                      SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatProducto_Consultar", ref dr, Parametros, Valores);
                      if (dr.HasRows)
                      {
                          dr.Read();
                          producto = new Producto();
                          producto.Id_Emp = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Emp")));
                          producto.Id_Cd = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Cd"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Id_Cd"));
                          producto.Id_Prd = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Prd"))) ? 0 : dr.GetInt64(dr.GetOrdinal("Id_Prd"));
                          producto.Id_Spo = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Spo"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Id_Spo"));
                          producto.Id_Ptp = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Ptp"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Id_Ptp"));
                          producto.Id_Cpr = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Cpr"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Id_Cpr"));
                          producto.Id_Fam = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Fam"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Id_Fam"));
                          producto.Id_Sub = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Sub"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Id_Sub"));
                          producto.Id_Pvd = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Pvd"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Id_Pvd"));
                          producto.Prd_Descripcion = dr.GetString(dr.GetOrdinal("Prd_Descripcion"));
                          producto.Prd_Presentacion = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Presentacion"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_Presentacion"));
                          producto.Prd_InvInicial = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_InvInicial"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_InvInicial"));
                          producto.Prd_InvFinal = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_InvFinal"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_InvFinal"));
                          producto.Prd_AgrupadoSpo = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_AgrupadoSpo"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_AgrupadoSpo"));
                          producto.Prd_FactorConv = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_FactorConv"))) ? 0 : Convert.ToSingle(dr.GetValue(dr.GetOrdinal("Prd_FactorConv")));
                          producto.Prd_AparatoSisProp = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_AparatoSisProp"))) ? false : dr.GetBoolean(dr.GetOrdinal("Prd_AparatoSisProp"));
                          producto.Prd_Fisico = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Fisico"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_Fisico"));
                          producto.Prd_Ordenado = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Ordenado"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_Ordenado"));
                          producto.Prd_Asignado = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Asignado"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_Asignado"));
                          producto.Prd_InvSeg = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_InvSeg"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_InvSeg"));
                          producto.Prd_TTrans = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_TTrans"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_TTrans"));
                          producto.Prd_TEntre = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_TEntre"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_TEntre"));
                          producto.Prd_Transito = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Transito"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_Transito"));
                          producto.Prd_UniNe = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_UniNe"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_UniNe"));
                          producto.Prd_UniNs = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_UniNs"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_UniNs"));
                          producto.Prd_InvInicial = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_InvInicial"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_InvInicial"));
                          producto.Prd_InvFinal = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_InvFinal"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_InvFinal"));
                          producto.Prd_Unico = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Unico"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_Unico"));
                          producto.Prd_UniEmp = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_UniEmp"))) ? 0 : Convert.ToSingle(dr.GetValue(dr.GetOrdinal("Prd_UniEmp")));
                          producto.Prd_Colo = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Colo"))) ? false : dr.GetBoolean(dr.GetOrdinal("Prd_Colo"));
                          if (!Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Ren"))))
                          {
                              producto.Prd_Ren = Convert.ToChar(dr.GetValue(dr.GetOrdinal("Prd_Ren")));
                          }
                          producto.Prd_Mes = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Mes"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_Mes"));
                          producto.Prd_CLNomFab = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_CLNomFab"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_CLNomFab"));
                          producto.Prd_CLIdFab = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_CLIdFab"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_CLIdFab"));
                          producto.Prd_CLDesFab = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_CLDesFab"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_CLDesFab"));
                          producto.Prd_CLPreFab = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_CLPreFab"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_CLPreFab"));
                          producto.Prd_CLNomPro = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_CLNomPro"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_CLNomPro"));
                          producto.Prd_CLIdPro = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_CLIdPro"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_CLIdPro"));
                          producto.Prd_CLDesPro = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_CLDesPro"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_CLDesPro"));
                          producto.Prd_CLPrePro = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_CLPrePro"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_CLPrePro"));
                          producto.Prd_MaxExistencia = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_MaxExistencia"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_MaxExistencia"));
                          producto.Prd_Ubicacion = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Ubicacion"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_Ubicacion"));
                          producto.Prd_Contribucion = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Contribucion"))) ? 0 : Convert.ToSingle(dr.GetValue(dr.GetOrdinal("Prd_Contribucion")));
                          producto.Prd_PorUtilidades = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_PorUtilidades"))) ? 0 : Convert.ToSingle(dr.GetValue(dr.GetOrdinal("Prd_PorUtilidades")));
                          producto.Prd_Nuevo = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Nuevo"))) ? true : dr.GetBoolean(dr.GetOrdinal("Prd_Nuevo"));
                          producto.Prd_PesConTecnico = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_PesConTecnico"))) ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Prd_PesConTecnico")));
                          producto.Prd_CptSv = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_CptSv"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_CptSv"));
                          producto.Prd_Activo = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Activo"))) ? false : dr.GetBoolean(dr.GetOrdinal("Prd_Activo"));
                          producto.Prd_Precio = dr.IsDBNull(dr.GetOrdinal("Prd_Precio")) ? (string)null : Convert.ToString(dr.GetValue(dr.GetOrdinal("Prd_Precio")));
                          producto.CantFact = (int?)dr.GetValue(dr.GetOrdinal("CantFact"));
                          producto.UltimaVta = dr.IsDBNull(dr.GetOrdinal("UltimaVta")) ? (DateTime?)null : (DateTime?)dr.GetValue(dr.GetOrdinal("UltimaVta"));
                          producto.Prd_AAA = dr.IsDBNull(dr.GetOrdinal("AAA")) ? 0 : (double?)dr.GetValue(dr.GetOrdinal("AAA"));
                      }
                      CapaDatos.LimpiarSqlcommand(ref sqlcmd);
                  }*/
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //
        // 31 Jul 2018 RFH
        // 

        public void Consulta_Producto(ref Producto producto, string Conexion, int id_Emp, int id_Cd, int id_Cd_Ver, long id_Prd, int ValidaInv)
        {
            try
            {

                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp",
                                          "@Id_Cd",
                                          "@Id_Cd_Ver",
                                          "@Id_Prd",
                                          "@Id_Ter",
                                          "@Id_Cte",
                                          "@EmpBen",
                                          "@ValidaInv",
                                          "@Id_Mov",
                                          "@SisProp"
                };

                object[] Valores = { id_Emp,
                                       id_Cd <= 0 ? (object)null : id_Cd,
                                       id_Cd_Ver,
                                       id_Prd,
                                       (producto == null) ? (object)null: (producto.Id_Ter == null) ? (object)null: producto.Id_Ter,
                                       (producto == null) ? (object)null: (producto.Id_Cte == null) ? (object)null: producto.Id_Cte,
                                       (producto == null) ? (object)null: producto.EmpBen,
                                       ValidaInv,
                                       producto == null ? (object)null : producto.Id_Mov,
                                       producto == null ? (object)null : producto.Prd_AparatoSisProp
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatProducto_Consulta", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    producto = new Producto();

                    producto.Id_Emp = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Emp")));
                    producto.Id_Cd = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Cd"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Id_Cd"));
                    producto.Id_Prd = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Prd"))) ? 0 : dr.GetInt64(dr.GetOrdinal("Id_Prd"));
                    producto.Id_Spo = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Spo"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Id_Spo"));
                    producto.Id_Ptp = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Ptp"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Id_Ptp"));
                    producto.Id_Cpr = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Cpr"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Id_Cpr"));
                    producto.Id_Fam = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Fam"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Id_Fam"));
                    producto.Id_Sub = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Sub"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Id_Sub"));
                    producto.Id_Pvd = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Pvd"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Id_Pvd"));
                    producto.Prd_Descripcion = dr.GetString(dr.GetOrdinal("Prd_Descripcion"));
                    producto.Prd_Presentacion = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Presentacion"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_Presentacion"));

                    producto.Prd_InvInicial = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_InvInicial"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_InvInicial"));
                    producto.Prd_InvFinal = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_InvFinal"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_InvFinal"));
                    producto.Prd_AgrupadoSpo = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_AgrupadoSpo"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_AgrupadoSpo"));
                    producto.Prd_FactorConv = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_FactorConv"))) ? 0 : Convert.ToSingle(dr.GetValue(dr.GetOrdinal("Prd_FactorConv")));
                    producto.Prd_AparatoSisProp = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_AparatoSisProp"))) ? false : dr.GetBoolean(dr.GetOrdinal("Prd_AparatoSisProp"));
                    producto.Prd_Fisico = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Fisico"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_Fisico"));
                    producto.Prd_Ordenado = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Ordenado"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_Ordenado"));
                    producto.Prd_Asignado = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Asignado"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_Asignado"));
                    producto.Prd_InvSeg = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_InvSeg"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_InvSeg"));
                    producto.Prd_TTrans = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_TTrans"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_TTrans"));
                    producto.Prd_TEntre = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_TEntre"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_TEntre"));
                    producto.Prd_Transito = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Transito"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_Transito"));
                    producto.Prd_UniNe = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_UniNe"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_UniNe"));
                    producto.Prd_UniNs = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_UniNs"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_UniNs"));

                    producto.Prd_InvInicial = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_InvInicial"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_InvInicial"));
                    producto.Prd_InvFinal = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_InvFinal"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_InvFinal"));
                    producto.Prd_Unico = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Unico"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_Unico"));
                    producto.Prd_UniEmp = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_UniEmp"))) ? 0 : Convert.ToSingle(dr.GetValue(dr.GetOrdinal("Prd_UniEmp")));
                    producto.Prd_Colo = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Colo"))) ? false : dr.GetBoolean(dr.GetOrdinal("Prd_Colo"));
                    producto.Prd_Ren = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Ren"))) ? Convert.ToChar(' ') : Convert.ToChar(dr.GetValue(dr.GetOrdinal("Prd_Ren")));
                    producto.Prd_Mes = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Mes"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_Mes"));
                    producto.Prd_CLNomFab = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_CLNomFab"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_CLNomFab"));
                    producto.Prd_CLIdFab = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_CLIdFab"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_CLIdFab"));
                    producto.Prd_CLDesFab = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_CLDesFab"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_CLDesFab"));
                    producto.Prd_CLPreFab = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_CLPreFab"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_CLPreFab"));
                    producto.Prd_CLNomPro = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_CLNomPro"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_CLNomPro"));
                    producto.Prd_CLIdPro = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_CLIdPro"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_CLIdPro"));
                    producto.Prd_CLDesPro = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_CLDesPro"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_CLDesPro"));
                    producto.Prd_CLPrePro = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_CLPrePro"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_CLPrePro"));
                    producto.Prd_MaxExistencia = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_MaxExistencia"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_MaxExistencia"));
                    producto.Prd_Ubicacion = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Ubicacion"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_Ubicacion"));
                    producto.Prd_Contribucion = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Contribucion"))) ? 0 : Convert.ToSingle(dr.GetValue(dr.GetOrdinal("Prd_Contribucion")));
                    producto.Prd_PorUtilidades = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_PorUtilidades"))) ? 0 : Convert.ToSingle(dr.GetValue(dr.GetOrdinal("Prd_PorUtilidades")));
                    producto.Prd_Nuevo = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Nuevo"))) ? true : dr.GetBoolean(dr.GetOrdinal("Prd_Nuevo"));
                    producto.Prd_PesConTecnico = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_PesConTecnico"))) ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Prd_PesConTecnico")));
                    producto.Prd_CptSv = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_CptSv"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_CptSv"));
                    producto.Prd_Activo = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Activo"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_Activo"));  //Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Activo"))) ? false : dr.GetBoolean(dr.GetOrdinal("Prd_Activo"));

                    // PRECIO de VENTA / ACYS  
                    producto.Prd_Precio = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Precio"))) ? string.Empty : string.Format("{0:0.00}", dr.GetValue(dr.GetOrdinal("Prd_Precio")));
                    producto.Prd_Minimo = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Minimo"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_Minimo"));
                    producto.Prd_PlanAbasto = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_PlanAbasto"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_PlanAbasto"));
                    producto.Prd_AAACadena = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_AAA"))) ? string.Empty : string.Format("{0:0.00}", dr.GetValue(dr.GetOrdinal("Prd_AAA")));

                    producto.Prd_PrecioLista = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("PrecioLista"))) ? string.Empty : string.Format("{0:0.00}", dr.GetValue(dr.GetOrdinal("PrecioLista")));
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void ConsultaListaProducto(ref Producto producto, string Conexion, int id_Emp, int id_Cd, string filtro, ref List<Producto> List, object Activo)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                int idProducto = 0;
                object[] Valores = new object[5];

                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@filtroInt", "@filtroString", "@Activo" };
                Valores[0] = id_Emp;
                Valores[1] = id_Cd;
                if (filtro == string.Empty)
                {
                    Valores[2] = null;
                    Valores[3] = null;
                }
                else
                {
                    if (int.TryParse(filtro, out idProducto))
                    {
                        Valores[2] = idProducto;
                        Valores[3] = null;
                    }
                    else
                    {
                        Valores[2] = null;
                        Valores[3] = filtro;
                    }
                }
                Valores[4] = Activo;
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatProductoLista_Consulta", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    producto = new Producto();
                    producto.Id_Cd = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Cd"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Id_Cd"));
                    producto.Id_Prd = dr.GetInt64(dr.GetOrdinal("Id_Prd"));
                    producto.Prd_Descripcion = dr.GetString(dr.GetOrdinal("Prd_Descripcion"));
                    producto.Prd_Activo = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Activo"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_Activo"));  //Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Activo"))) ? false : dr.GetBoolean(dr.GetOrdinal("Prd_Activo"));

                    producto.Prd_Colo = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Colo"))) ? false : dr.GetBoolean(dr.GetOrdinal("Prd_Colo"));
                    producto.Prd_Presentacion = dr.GetString(dr.GetOrdinal("Prd_Presentacion")); //agregado ricardo                   
                    producto.Prd_AgrupadoSpo = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_AgrupadoSpo"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_AgrupadoSpo")); //agregado ricardo
                    producto.Uni_Descripcion = dr.GetString(dr.GetOrdinal("Uni_Descripcion")); // agregado oscar
                    producto.Relacion = dr.GetValue(dr.GetOrdinal("Relacion")).ToString();
                    List.Add(producto);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaListaProductoSpo(ref Producto producto, string Conexion, int id_Emp, int id_Cd, string filtro, ref List<Producto> List, object Activo)
        {//rm lista de productos sistema de propietarios
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                int idProducto = 0;
                object[] Valores = new object[5];

                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@filtroInt", "@filtroString", "@Activo" };
                Valores[0] = id_Emp;
                Valores[1] = id_Cd;
                if (filtro == string.Empty)
                {
                    Valores[2] = null;
                    Valores[3] = null;
                }
                else
                {
                    if (int.TryParse(filtro, out idProducto))
                    {
                        Valores[2] = idProducto;
                        Valores[3] = null;
                    }
                    else
                    {
                        Valores[2] = null;
                        Valores[3] = filtro;
                    }
                }
                Valores[4] = Activo;
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatProductoListaSpo_Consulta", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    producto = new Producto();
                    producto.Id_Cd = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Cd"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Id_Cd"));
                    producto.Id_Prd = dr.GetInt64(dr.GetOrdinal("Id_Prd"));
                    producto.Prd_Descripcion = dr.GetString(dr.GetOrdinal("Prd_Descripcion"));
                    producto.Prd_Activo = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Activo"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_Activo"));  //Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Activo"))) ? false : dr.GetBoolean(dr.GetOrdinal("Prd_Activo"));

                    producto.Prd_Colo = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Colo"))) ? false : dr.GetBoolean(dr.GetOrdinal("Prd_Colo"));
                    producto.Prd_Presentacion = dr.GetString(dr.GetOrdinal("Prd_Presentacion")); //agregado ricardo                   
                    producto.Prd_AgrupadoSpo = dr.GetInt32(dr.GetOrdinal("Prd_AgrupadoSpo")); //agregado ricardo
                    producto.Uni_Descripcion = dr.GetString(dr.GetOrdinal("Uni_Descripcion")); // agregado oscar
                    List.Add(producto);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaListaProductoFacturacion(ref Producto producto, string Conexion, int id_Emp, int id_Cd, int id_Ter, string filtro, ref List<Producto> List, object Activo)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                int idProducto = 0;
                object[] Valores = new object[6];

                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_Ter", "@filtroInt", "@filtroString", "@Activo" };
                Valores[0] = id_Emp;
                Valores[1] = id_Cd;
                Valores[2] = id_Ter;
                if (filtro == string.Empty)
                {
                    Valores[3] = null;
                    Valores[4] = null;
                }
                else
                {
                    if (int.TryParse(filtro, out idProducto))
                    {
                        Valores[3] = idProducto;
                        Valores[4] = null;
                    }
                    else
                    {
                        Valores[3] = null;
                        Valores[4] = filtro;
                    }
                }
                Valores[5] = Activo;
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatProductoListaFacturacion_Consulta", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    producto = new Producto();
                    producto.Id_Cd = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Cd"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Id_Cd"));
                    producto.Id_Prd = dr.GetInt64(dr.GetOrdinal("Id_Prd"));
                    producto.Prd_Descripcion = dr.GetString(dr.GetOrdinal("Prd_Descripcion"));
                    producto.Prd_Activo = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Activo"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_Activo"));  //Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Activo"))) ? false : dr.GetBoolean(dr.GetOrdinal("Prd_Activo"));
                    producto.Prd_Colo = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Colo"))) ? false : dr.GetBoolean(dr.GetOrdinal("Prd_Colo"));
                    producto.Prd_Presentacion = dr.GetString(dr.GetOrdinal("Prd_Presentacion")); //agregado ricardo                   
                    producto.Prd_AgrupadoSpo = dr.GetInt32(dr.GetOrdinal("Prd_AgrupadoSpo")); //agregado ricardo
                    List.Add(producto);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void InsertarProducto_CL(Producto producto, string Conexion, ref int verificador)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            try
            {
                CapaDatos.StartTrans();

                object Id_Spo = null;
                object Id_Cpr = null;
                object Id_Fam = null;
                object Id_Sub = null;

                string[] Parametros = {
                                        "@Id_Emp"
                                        ,"@Id_Cd"
                                        ,"@Id_Prd"
                                        ,"@Id_Spo"
                                        ,"@Id_Ptp"
                                        ,"@Id_Cpr"
                                        ,"@Id_Fam"
                                        ,"@Id_Sub"
                                        ,"@Id_Pvd"
                                        ,"@Prd_Descripcion"
                                        ,"@Prd_Presentacion"
                                        ,"@Prd_InvInicial"
                                        ,"@Prd_InvFinal"
                                        ,"@Prd_AgrupadoSpo"
                                        ,"@Prd_FactorConv"
                                        ,"@Prd_AparatoSisProp"
                                        ,"@Prd_Fisico"
                                        ,"@Prd_Ordenado"
                                        ,"@Prd_Asignado"
                                        ,"@Prd_InvSeg"
                                        ,"@Prd_TTrans"
                                        ,"@Prd_TEntre"
                                        ,"@Prd_Transito"
                                        ,"@Prd_UniNe"
                                        ,"@Prd_UniNs"
                                        ,"@Prd_Unico"
                                        ,"@Prd_UniEmp"
                                        ,"@Prd_Colo"
                                        ,"@Prd_Ren"
                                        ,"@Prd_Mes"
                                        ,"@Prd_CLNomFab"
                                        ,"@Prd_CLIdFab"
                                        ,"@Prd_CLDesFab"
                                        ,"@Prd_CLPreFab"
                                        ,"@Prd_CLNomPro"
                                        ,"@Prd_CLIdPro"
                                        ,"@Prd_CLDesPro"
                                        ,"@Prd_CLPrePro"
                                        ,"@Prd_MaxExistencia"
                                        ,"@Prd_Ubicacion"
                                        ,"@Prd_Contribucion"
                                        ,"@Prd_PorUtilidades"
                                        ,"@Prd_Nuevo"
                                        ,"@Prd_PesConTecnico"
                                        ,"@Prd_CptSv"
                                        ,"@Prd_Activo"
                                        ,"@Prd_FecAlta"
                                        ,"@Prd_Minimo"
                                        ,"Prd_PlanAbasto"
                                        ,"@Prd_NoFacturable"
                                      };
                object[] Valores = {
                                        producto.Id_Emp
                                        ,producto.Id_Cd == 0 ? (object)null : producto.Id_Cd
                                        ,producto.Id_Prd
                                        ,producto.Id_Spo == 0 ? Id_Spo : producto.Id_Spo
                                        ,producto.Id_Ptp
                                        ,producto.Id_Cpr == 0 ? Id_Cpr: producto.Id_Cpr
                                        ,producto.Id_Fam == 0 ? Id_Fam: producto.Id_Fam
                                        ,producto.Id_Sub == 0 ? Id_Sub: producto.Id_Sub
                                        ,producto.Id_Pvd
                                        ,producto.Prd_Descripcion
                                        ,producto.Prd_Presentacion
                                        ,producto.Prd_InvInicial
                                        ,producto.Prd_InvFinal
                                        ,producto.Prd_AgrupadoSpo
                                        ,producto.Prd_FactorConv
                                        ,producto.Prd_AparatoSisProp
                                        ,producto.Prd_Fisico
                                        ,producto.Prd_Ordenado
                                        ,producto.Prd_Asignado
                                        ,producto.Prd_InvSeg
                                        ,producto.Prd_TTrans
                                        ,producto.Prd_TEntre
                                        ,producto.Prd_Transito
                                        ,producto.Prd_UniNe
                                        ,producto.Prd_UniNs
                                        ,producto.Prd_Unico
                                        ,producto.Prd_UniEmp
                                        ,producto.Prd_Colo
                                        ,producto.Prd_Ren
                                        ,producto.Prd_Mes
                                        ,producto.Prd_CLNomFab
                                        ,producto.Prd_CLIdFab
                                        ,producto.Prd_CLDesFab
                                        ,producto.Prd_CLPreFab
                                        ,producto.Prd_CLNomPro
                                        ,producto.Prd_CLIdPro
                                        ,producto.Prd_CLDesPro
                                        ,producto.Prd_CLPrePro
                                        ,producto.Prd_MaxExistencia
                                        ,producto.Prd_Ubicacion
                                        ,producto.Prd_Contribucion
                                        ,producto.Prd_PorUtilidades
                                        ,producto.Prd_Nuevo
                                        ,producto.Prd_PesConTecnico
                                        ,producto.Prd_CptSv
                                        ,producto.Prd_Activo
                                        ,producto.Prd_FecAlta
                                        ,producto.Prd_Minimo
                                        ,producto.Prd_PlanAbasto
                                        ,producto.NoFacturable
                                   };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatProducto_Insertar_CL", ref verificador, Parametros, Valores);

                // ------------------------------------------------------------
                // Eliminar datos de precio de productos del producto actual
                // ------------------------------------------------------------
                string[] ParametrosDelete = {
                                        "@Id_Emp",
                                        "@Id_Cd",
                                        "@Id_Prd",
                                      };
                object[] ValoresDelete = {
                                        producto.Id_Emp
                                        ,producto.Id_Cd
                                        ,producto.Id_Prd
                                   };
                sqlcmd = CapaDatos.GenerarSqlCommand("spProductoPrecios_Eliminar_CL", ref verificador, ParametrosDelete, ValoresDelete);

                // ------------------------------------------------------------
                // Insertar datos de precio de productos
                // ------------------------------------------------------------
                foreach (ProductoPrecios productoPrecios in producto.ListaProductoPrecios)
                {
                    string[] ParametrosInsert = {
                                        "@Id_Emp",
                                        "@Id_Cd",
                                        "@Id_Prd",
                                        "@Id_Pre",
                                        "@Prd_Actual",
                                        "@Prd_FechaInicio",
                                        "@Prd_FechaFin",
                                        "@Prd_PreDescripcion",
                                        "@Prd_Pesos"
                                      };
                    object[] ValoresInsert = {
                                        productoPrecios.Id_Emp
                                        ,productoPrecios.Id_Cd
                                        ,producto.Id_Prd //toma la clave de producto del 'papa'
                                        ,productoPrecios.Id_Pre
                                        ,productoPrecios.Prd_Actual
                                        ,productoPrecios.Prd_FechaInicio
                                        ,productoPrecios.Prd_FechaFin
                                        ,productoPrecios.Prd_PreDescripcion
                                        ,productoPrecios.Prd_Pesos
                                   };
                    //Inserta solo si las fechas son validas para SQL Server
                    if (Convert.ToDateTime(productoPrecios.Prd_FechaFin).CompareTo(new DateTime(1753, 1, 1)) > 0 && Convert.ToDateTime(productoPrecios.Prd_FechaInicio).CompareTo(new DateTime(1753, 1, 1)) > 0)
                        sqlcmd = CapaDatos.GenerarSqlCommand("spProductoPrecios_Insertar_CL", ref verificador, ParametrosInsert, ValoresInsert);
                }
                CapaDatos.CommitTrans();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                CapaDatos.RollBackTrans();
                throw ex;
            }
        }


        //
        // ENE31-2020 RFH Compras Locales 
        // Lisado de precios del producto 
        //
        public List<eCL_ProductoPrecios> spSel_ProductoPrecios_CL_ver2(int Id_Emp, int Id_Cd, long Id_Prd, string Conexion)
        {
            List<eCL_ProductoPrecios> lst = new List<eCL_ProductoPrecios>();
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = {
                                          "@Id_Emp",
                                          "@Id_Cd",
                                          "@Id_Prd"
                                      };
                object[] Valores = {
                                       Id_Emp,
                                       Id_Cd,
                                       Id_Prd
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spSel_ProductoPrecios_CL_ver2", ref dr, Parametros, Valores);


                while (dr.Read())
                {
                    eCL_ProductoPrecios obj = new eCL_ProductoPrecios();

                    obj.Id_Emp = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Emp")));
                    obj.Id_Cd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cd")));
                    obj.Id_Prd = Convert.ToInt64(dr.GetValue(dr.GetOrdinal("Id_Prd")));
                    obj.Prd_Actual = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Prd_Actual")));
                    obj.Prd_FechaInicio = dr.GetValue(dr.GetOrdinal("Prd_FechaFin")).ToString();
                    obj.Prd_FechaFin = dr.GetValue(dr.GetOrdinal("Prd_FechaFin")).ToString();
                    obj.Prd_Descripcion = dr.GetValue(dr.GetOrdinal("Prd_Descripcion")).ToString();
                    obj.Prd_Pesos = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Prd_Pesos")));

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

        // SP 1
        public void InsertarProducto_CL_ver3(
            int Id_Emp, int Id_Cd, Producto P, string Conexion, ref int verificador)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            try
            {
                string[] Parametros = {
                    "@Id_Emp",
                    "@Id_Cd",
                    "@Id_Prd",
                    "@Id_Spo",
                    "@Id_Ptp",
                    "@Id_Cpr",
                    "@Id_Fam",
                    "@Id_Sub",
                    "@Id_Pvd",
                    "@Prd_Descripcion",
                    "@Prd_Presentacion",
                    "@Prd_InvInicial",
                    "@Prd_InvFinal",
                    "@Prd_AgrupadoSpo",
                    "@Prd_FactorConv",
                    "@Prd_AparatoSisProp",
                    "@Prd_Fisico",
                    "@Prd_Ordenado",
                    "@Prd_Asignado",
                    "@Prd_InvSeg",
                    "@Prd_TTrans",
                    "@Prd_TEntre",
                    "@Prd_Transito",
                    "@Prd_UniNe",
                    "@Prd_UniNs",
                    "@Prd_Unico",
                    "@Prd_UniEmp",
                    "@Prd_Colo",
                    "@Prd_Ren",
                    "@Prd_Mes",
                    "@Prd_CLNomFab",
                    "@Prd_CLIdFab",
                    "@Prd_CLDesFab",
                    "@Prd_CLPreFab",
                    "@Prd_CLNomPro",
                    "@Prd_CLIdPro",
                    "@Prd_CLDesPro",
                    "@Prd_CLPrePro",
                    "@Prd_MaxExistencia",
                    "@Prd_Ubicacion",
                    "@Prd_Contribucion",
                    "@Prd_PorUtilidades",
                    "@Prd_Nuevo",
                    "@Prd_PesConTecnico",
                    "@Prd_CptSv",
                    "@Prd_Activo",
                    "@Prd_Minimo",
                    "Prd_PlanAbasto",
                    "@Prd_NoFacturable",
                    "@Prd_ClaveProdServ",
                    "@Prd_ClaveUnidad",
                    "@Prd_FechaInicio",
                    "@Prd_FechaFin",
                    "@Prd_CodigoProv",
                    "@Prd_DescripcionProv",
                    "@Prd_PresentacionProv",
                    "@Prd_IdProvLocal",
                    "@Prd_NomProvLocal",
                    "@NomFamilia",
                    "@NomSubFamilia"
                };

                object[] Valores = {
                    Id_Emp,
                    Id_Cd,
                    P.Id_Prd,
                    P.Id_Spo,
                    P.Id_Ptp,
                    P.Id_Cpr,
                    P.Id_Fam,
                    P.Id_Sub,
                    P.Id_Pvd,
                    P.Prd_Descripcion,
                    P.Prd_Presentacion,
                    P.Prd_InvInicial,
                    P.Prd_InvFinal,
                    P.Prd_AgrupadoSpo,
                    P.Prd_FactorConv,
                    P.Prd_AparatoSisProp,
                    P.Prd_Fisico,
                    P.Prd_Ordenado,
                    P.Prd_Asignado,
                    P.Prd_InvSeg,
                    P.Prd_TTrans,
                    P.Prd_TEntre,
                    P.Prd_Transito,
                    P.Prd_UniNe,
                    P.Prd_UniNs,
                    P.Prd_Unico,
                    P.Prd_UniEmp,
                    P.Prd_Colo,
                    P.Prd_Ren,
                    P.Prd_Mes,
                    P.Prd_CLNomFab,
                    P.Prd_CLIdFab,
                    P.Prd_CLDesFab,
                    P.Prd_CLPreFab,
                    P.Prd_CLNomPro,
                    P.Prd_CLIdPro,
                    P.Prd_CLDesPro,
                    P.Prd_CLPrePro,
                    P.Prd_MaxExistencia,
                    P.Prd_Ubicacion,
                    P.Prd_Contribucion,
                    P.Prd_PorUtilidades,
                    P.Prd_Nuevo,
                    P.Prd_PesConTecnico,
                    P.Prd_CptSv,
                    P.Prd_Activo,
                    P.Prd_Minimo,
                    P.Prd_PlanAbasto,
                    P.NoFacturable,
                    P.Prd_ClaveProdServ,
                    P.Prd_ClaveUnidad,
                    Convert.ToDateTime(P.Prd_FechaInicio),
                    Convert.ToDateTime(P.Prd_FechaFin),
                    P.Prd_CodigoProv,
                    P.Prd_DescripcionProv,
                    P.Prd_PresentacionProv,
                    P.Prd_IdProvLocal,
                    P.Prd_NomProvLocal,
                    P.Prd_NomFamilia,
                    P.Prd_NomSubFamilia
                };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatProducto_Insertar_CL_ver2", ref verificador, Parametros, Valores);
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            CapaDatos.CD_Datos CapaDatos2 = new CapaDatos.CD_Datos(Conexion);

            try
            {

                // ------------------------------------------------------------
                // Eliminar datos de precio de productos del producto actual
                // ------------------------------------------------------------
                string[] ParametrosDelete = {
                                        "@Id_Emp",
                                        "@Id_Cd",
                                        "@Id_Prd",
                                      };
                object[] ValoresDelete = {
                                        Id_Emp,
                                        Id_Cd,
                                        P.Id_Prd
                                   };
                SqlCommand sqlcmd = CapaDatos2.GenerarSqlCommand("spProductoPrecios_Eliminar_CL_ver2", ref verificador, ParametrosDelete, ValoresDelete);

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {

                throw ex;
            }



            try
            {
                // ------------------------------------------------------------
                // Insertar datos de precio de productos
                // ------------------------------------------------------------
                foreach (ProductoPrecios productoPrecios in P.ListaProductoPrecios)
                {
                    CapaDatos.CD_Datos CapaDatos3 = new CapaDatos.CD_Datos(Conexion);
                    string[] ParametrosInsert = {
                                        "@Id_Emp",
                                        "@Id_Cd",
                                        "@Id_Comp",
                                        "@Id_Prd",
                                        "@Id_Pre",
                                        "@Prd_Actual",
                                        "@Prd_FechaInicio",
                                        "@Prd_FechaFin",
                                        "@Prd_PreDescripcion",
                                        "@Prd_Pesos"
                                      };
                    object[] ValoresInsert = {
                                        Id_Emp
                                        ,Id_Cd
                                        ,verificador
                                        ,P.Id_Prd //toma la clave de producto del 'papa'
                                        ,productoPrecios.Id_Pre
                                        ,productoPrecios.Prd_Actual
                                        ,P.Prd_FechaInicio
                                        ,P.Prd_FechaFin
                                        ,productoPrecios.Prd_PreDescripcion
                                        ,productoPrecios.Prd_Pesos
                                   };
                    //Inserta solo si las fechas son validas para SQL Server
                    //if (Convert.ToDateTime(productoPrecios.Prd_FechaFin).CompareTo(new DateTime(1753, 1, 1)) > 0 && Convert.ToDateTime(productoPrecios.Prd_FechaInicio).CompareTo(new DateTime(1753, 1, 1)) > 0)

                    SqlCommand sqlcmd = CapaDatos3.GenerarSqlCommand("spProductoPrecios_Insertar_CL_ver2", ref verificador, ParametrosInsert, ValoresInsert);
                    CapaDatos.LimpiarSqlcommand(ref sqlcmd);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        //COMPRAS LOCALES - ENE28-2020        
        public void InsertarProducto_CL_ver2(
            int Id_Emp, int Id_Cd, Producto P, string Conexion, ref int verificador)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            try
            {
                CapaDatos.StartTrans();

                Parametro("@Id_Emp", Id_Emp);
                Parametro("@Id_Cd", Id_Cd);
                Parametro("@Id_Prd", P.Id_Prd);
                Parametro("@Id_Spo", P.Id_Spo);
                Parametro("@Id_Ptp", P.Id_Ptp);
                Parametro("@Id_Cpr", P.Id_Cpr);
                Parametro("@Id_Fam", P.Id_Fam);
                Parametro("@Id_Sub", P.Id_Sub);
                Parametro("@Id_Pvd", P.Id_Pvd);
                Parametro("@Prd_Descripcion", P.Prd_Descripcion);
                Parametro("@Prd_Presentacion", P.Prd_Presentacion);
                Parametro("@Prd_InvInicial", P.Prd_InvInicial);
                Parametro("@Prd_InvFinal", P.Prd_InvFinal);
                Parametro("@Prd_AgrupadoSpo", P.Prd_AgrupadoSpo);
                Parametro("@Prd_FactorConv", P.Prd_FactorConv);
                Parametro("@Prd_AparatoSisProp", P.Prd_AparatoSisProp);
                Parametro("@Prd_Fisico", P.Prd_Fisico);
                Parametro("@Prd_Ordenado", P.Prd_Ordenado);
                Parametro("@Prd_Asignado", P.Prd_Asignado);
                Parametro("@Prd_InvSeg", P.Prd_InvSeg);
                Parametro("@Prd_TTrans", P.Prd_TTrans);
                Parametro("@Prd_TEntre", P.Prd_TEntre);
                Parametro("@Prd_Transito", P.Prd_Transito);
                Parametro("@Prd_UniNe", P.Prd_UniNe);
                Parametro("@Prd_UniNs", P.Prd_UniNs);
                Parametro("@Prd_Unico", P.Prd_Unico);
                Parametro("@Prd_UniEmp", P.Prd_UniEmp);
                Parametro("@Prd_Colo", P.Prd_Colo);
                Parametro("@Prd_Ren", P.Prd_Ren);
                Parametro("@Prd_Mes", P.Prd_Mes);
                Parametro("@Prd_CLNomFab", P.Prd_CLNomFab);
                Parametro("@Prd_CLIdFab", P.Prd_CLIdFab);
                Parametro("@Prd_CLDesFab", P.Prd_CLDesFab);
                Parametro("@Prd_CLPreFab", P.Prd_CLPreFab);
                Parametro("@Prd_CLNomPro", P.Prd_CLNomPro);
                Parametro("@Prd_CLIdPro", P.Prd_CLIdPro);
                Parametro("@Prd_CLDesPro", P.Prd_CLDesPro);
                Parametro("@Prd_CLPrePro", P.Prd_CLPrePro);
                Parametro("@Prd_MaxExistencia", P.Prd_MaxExistencia);
                Parametro("@Prd_Ubicacion", P.Prd_Ubicacion);
                Parametro("@Prd_Contribucion", P.Prd_Contribucion);
                Parametro("@Prd_PorUtilidades", P.Prd_PorUtilidades);
                Parametro("@Prd_Nuevo", P.Prd_Nuevo);
                Parametro("@Prd_PesConTecnico", P.Prd_PesConTecnico);
                Parametro("@Prd_CptSv", P.Prd_CptSv);
                Parametro("@Prd_Activo", P.Prd_Activo);
                //Parametro("@Prd_FecAlta", P.Prd_FecAlta);
                Parametro("@Prd_Minimo", P.Prd_Minimo);
                Parametro("Prd_PlanAbasto", P.Prd_PlanAbasto);
                Parametro("@Prd_NoFacturable", P.NoFacturable);

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatProducto_Insertar_CL_ver2", ref verificador, Parametros.ToArray(), Valores.ToArray());

                // ------------------------------------------------------------
                // Eliminar datos de precio de productos del producto actual
                // ------------------------------------------------------------
                string[] ParametrosDelete = {
                                        "@Id_Emp",
                                        "@Id_Cd",
                                        "@Id_Prd",
                                      };
                object[] ValoresDelete = {
                                        Id_Emp,
                                        Id_Cd,
                                        P.Id_Prd
                                   };
                sqlcmd = CapaDatos.GenerarSqlCommand("spProductoPrecios_Eliminar_CL_ver2", ref verificador, ParametrosDelete, ValoresDelete);

                // ------------------------------------------------------------
                // Insertar datos de precio de productos
                // ------------------------------------------------------------
                foreach (ProductoPrecios productoPrecios in P.ListaProductoPrecios)
                {
                    string[] ParametrosInsert = {
                                        "@Id_Emp",
                                        "@Id_Cd",
                                        "@Id_Comp",
                                        "@Id_Prd",
                                        "@Id_Pre",
                                        "@Prd_Actual",
                                        "@Prd_FechaInicio",
                                        "@Prd_FechaFin",
                                        "@Prd_PreDescripcion",
                                        "@Prd_Pesos"
                                      };
                    object[] ValoresInsert = {
                                        Id_Emp
                                        ,Id_Cd
                                        ,verificador
                                        ,P.Id_Prd //toma la clave de producto del 'papa'
                                        ,productoPrecios.Id_Pre
                                        ,productoPrecios.Prd_Actual
                                        ,productoPrecios.Prd_FechaInicio
                                        ,productoPrecios.Prd_FechaFin
                                        ,productoPrecios.Prd_PreDescripcion
                                        ,productoPrecios.Prd_Pesos
                                   };
                    //Inserta solo si las fechas son validas para SQL Server
                    //if (Convert.ToDateTime(productoPrecios.Prd_FechaFin).CompareTo(new DateTime(1753, 1, 1)) > 0 && Convert.ToDateTime(productoPrecios.Prd_FechaInicio).CompareTo(new DateTime(1753, 1, 1)) > 0)

                    sqlcmd = CapaDatos.GenerarSqlCommand("spProductoPrecios_Insertar_CL_ver2", ref verificador, ParametrosInsert, ValoresInsert);

                }
                CapaDatos.CommitTrans();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                CapaDatos.RollBackTrans();
                throw ex;
            }
        }

        public void ModificarProducto_CL(Producto producto, string Conexion, ref int verificador)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            try
            {
                CapaDatos.StartTrans();

                object Id_Spo = null;
                object Id_Cpr = null;
                object Id_Fam = null;
                object Id_Sub = null;

                string[] Parametros = {
                                        "@Id_Emp"
                                        ,"@Id_Cd"
                                        ,"@Id_Prd"
                                        ,"@Id_Spo"
                                        ,"@Id_Ptp"
                                        ,"@Id_Cpr"
                                        ,"@Id_Fam"
                                        ,"@Id_Sub"
                                        ,"@Id_Pvd"
                                        ,"@Prd_Descripcion"
                                        ,"@Prd_Presentacion"
                                        ,"@Prd_InvInicial"
                                        ,"@Prd_InvFinal"
                                        ,"@Prd_AgrupadoSpo"
                                        ,"@Prd_FactorConv"
                                        ,"@Prd_AparatoSisProp"
                                        ,"@Prd_Fisico"
                                        ,"@Prd_Ordenado"
                                        ,"@Prd_Asignado"
                                        ,"@Prd_InvSeg"
                                        ,"@Prd_TTrans"
                                        ,"@Prd_TEntre"
                                        ,"@Prd_Transito"
                                        ,"@Prd_UniNe"
                                        ,"@Prd_UniNs"
                                        ,"@Prd_Unico"
                                        ,"@Prd_UniEmp"
                                        ,"@Prd_Colo"
                                        ,"@Prd_Ren"
                                        ,"@Prd_Mes"
                                        ,"@Prd_CLNomFab"
                                        ,"@Prd_CLIdFab"
                                        ,"@Prd_CLDesFab"
                                        ,"@Prd_CLPreFab"
                                        ,"@Prd_CLNomPro"
                                        ,"@Prd_CLIdPro"
                                        ,"@Prd_CLDesPro"
                                        ,"@Prd_CLPrePro"
                                        ,"@Prd_MaxExistencia"
                                        ,"@Prd_Ubicacion"
                                        ,"@Prd_Contribucion"
                                        ,"@Prd_PorUtilidades"
                                        ,"@Prd_Nuevo"
                                        ,"@Prd_PesConTecnico"
                                        ,"@Prd_CptSv"
                                        ,"@Prd_Activo"
                                        ,"@Prd_Minimo"
                                        ,"Prd_PlanAbasto"
                                      };
                object[] Valores = {
                                        producto.Id_Emp
                                        ,producto.Id_Cd
                                        ,producto.Id_Prd
                                        ,producto.Id_Spo == 0 ? Id_Spo : producto.Id_Spo
                                        ,producto.Id_Ptp
                                        ,producto.Id_Cpr == 0 ? Id_Cpr: producto.Id_Cpr
                                        ,producto.Id_Fam == 0 ? Id_Fam: producto.Id_Fam
                                        ,producto.Id_Sub == 0 ? Id_Sub: producto.Id_Sub
                                        ,producto.Id_Pvd
                                        ,producto.Prd_Descripcion
                                        ,producto.Prd_Presentacion
                                        ,producto.Prd_InvInicial
                                        ,producto.Prd_InvFinal
                                        ,producto.Prd_AgrupadoSpo
                                        ,producto.Prd_FactorConv
                                        ,producto.Prd_AparatoSisProp
                                        ,producto.Prd_Fisico
                                        ,producto.Prd_Ordenado
                                        ,producto.Prd_Asignado
                                        ,producto.Prd_InvSeg
                                        ,producto.Prd_TTrans
                                        ,producto.Prd_TEntre
                                        ,producto.Prd_Transito
                                        ,producto.Prd_UniNe
                                        ,producto.Prd_UniNs
                                        ,producto.Prd_Unico
                                        ,producto.Prd_UniEmp
                                        ,producto.Prd_Colo
                                        ,producto.Prd_Ren
                                        ,producto.Prd_Mes
                                        ,producto.Prd_CLNomFab
                                        ,producto.Prd_CLIdFab
                                        ,producto.Prd_CLDesFab
                                        ,producto.Prd_CLPreFab
                                        ,producto.Prd_CLNomPro
                                        ,producto.Prd_CLIdPro
                                        ,producto.Prd_CLDesPro
                                        ,producto.Prd_CLPrePro
                                        ,producto.Prd_MaxExistencia
                                        ,producto.Prd_Ubicacion
                                        ,producto.Prd_Contribucion
                                        ,producto.Prd_PorUtilidades
                                        ,producto.Prd_Nuevo
                                        ,producto.Prd_PesConTecnico
                                        ,producto.Prd_CptSv
                                        ,producto.Prd_Activo
                                        ,producto.Prd_Minimo
                                        ,producto.Prd_PlanAbasto
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatProducto_Modificar_CL", ref verificador, Parametros, Valores);

                // ------------------------------------------------------------
                // Eliminar datos de precio de productos del producto actual
                // ------------------------------------------------------------
                string[] ParametrosDelete = {
                                        "@Id_Emp",
                                        "@Id_Cd",
                                        "@Id_Prd",
                                      };
                object[] ValoresDelete = {
                                        producto.Id_Emp
                                        ,producto.Id_Cd
                                        ,producto.Id_Prd
                                   };
                sqlcmd = CapaDatos.GenerarSqlCommand("spProductoPrecios_Eliminar_CL", ref verificador, ParametrosDelete, ValoresDelete);

                // ------------------------------------------------------------
                // Insertar datos de precio de productos
                // ------------------------------------------------------------
                foreach (ProductoPrecios productoPrecios in producto.ListaProductoPrecios)
                {
                    string[] ParametrosInsert = {
                                        "@Id_Emp",
                                        "@Id_Cd",
                                        "@Id_Prd",
                                        "@Id_Pre",
                                        "@Prd_Actual",
                                        "@Prd_FechaInicio",
                                        "@Prd_FechaFin",
                                        "@Prd_PreDescripcion",
                                        "@Prd_Pesos"
                                      };
                    object[] ValoresInsert = {
                                        productoPrecios.Id_Emp
                                        ,productoPrecios.Id_Cd
                                        ,producto.Id_Prd //toma la clave de producto del 'papa'
                                        ,productoPrecios.Id_Pre
                                        ,productoPrecios.Prd_Actual
                                        ,productoPrecios.Prd_FechaInicio
                                        ,productoPrecios.Prd_FechaFin
                                        ,productoPrecios.Prd_PreDescripcion
                                        ,productoPrecios.Prd_Pesos
                                   };

                    //Inserta solo si las fechas son validas para SQL Server
                    if (Convert.ToDateTime(productoPrecios.Prd_FechaFin).CompareTo(new DateTime(1753, 1, 1)) > 0 && Convert.ToDateTime(productoPrecios.Prd_FechaInicio).CompareTo(new DateTime(1753, 1, 1)) > 0)
                    {
                        sqlcmd = CapaDatos.GenerarSqlCommand("spProductoPrecios_Insertar_CL", ref verificador, ParametrosInsert, ValoresInsert);
                    }
                }
                CapaDatos.CommitTrans();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                CapaDatos.RollBackTrans();
                throw ex;
            }
        }

        public void InsertarProducto(Producto producto, string Conexion, ref int verificador)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            try
            {
                CapaDatos.StartTrans();

                object Id_Spo = null;
                object Id_Cpr = null;
                object Id_Fam = null;
                object Id_Sub = null;

                string[] Parametros = {
                                        "@Id_Emp"
                                        ,"@Id_Cd"
                                        ,"@Id_Prd"
                                        ,"@Id_Spo"
                                        ,"@Id_Ptp"
                                        ,"@Id_Cpr"
                                        ,"@Id_Fam"
                                        ,"@Id_Sub"
                                        ,"@Id_Pvd"
                                        ,"@Prd_Descripcion"
                                        ,"@Prd_Presentacion"
                                        ,"@Prd_InvInicial"
                                        ,"@Prd_InvFinal"
                                        ,"@Prd_AgrupadoSpo"
                                        ,"@Prd_FactorConv"
                                        ,"@Prd_AparatoSisProp"
                                        ,"@Prd_Fisico"
                                        ,"@Prd_Ordenado"
                                        ,"@Prd_Asignado"
                                        ,"@Prd_InvSeg"
                                        ,"@Prd_TTrans"
                                        ,"@Prd_TEntre"
                                        ,"@Prd_Transito"
                                        ,"@Prd_UniNe"
                                        ,"@Prd_UniNs"
                                        ,"@Prd_Unico"
                                        ,"@Prd_UniEmp"
                                        ,"@Prd_Colo"
                                        ,"@Prd_Ren"
                                        ,"@Prd_Mes"
                                        ,"@Prd_CLNomFab"
                                        ,"@Prd_CLIdFab"
                                        ,"@Prd_CLDesFab"
                                        ,"@Prd_CLPreFab"
                                        ,"@Prd_CLNomPro"
                                        ,"@Prd_CLIdPro"
                                        ,"@Prd_CLDesPro"
                                        ,"@Prd_CLPrePro"
                                        ,"@Prd_MaxExistencia"
                                        ,"@Prd_Ubicacion"
                                        ,"@Prd_Contribucion"
                                        ,"@Prd_PorUtilidades"
                                        ,"@Prd_Nuevo"
                                        ,"@Prd_PesConTecnico"
                                        ,"@Prd_CptSv"
                                        ,"@Prd_Activo"
                                        ,"@Prd_FecAlta"
                                        ,"@Prd_Minimo"
                                        ,"Prd_PlanAbasto"
                                        ,"@Prd_NoFacturable"
                                        ,"@Prd_ClaveProdServ"
                                        ,"@Prd_ClaveUnidad"

                                      };
                object[] Valores = {
                                        producto.Id_Emp
                                        ,producto.Id_Cd == 0 ? (object)null : producto.Id_Cd
                                        ,producto.Id_Prd
                                        ,producto.Id_Spo == 0 ? Id_Spo : producto.Id_Spo
                                        ,producto.Id_Ptp
                                        ,producto.Id_Cpr == 0 ? Id_Cpr: producto.Id_Cpr
                                        ,producto.Id_Fam == 0 ? Id_Fam: producto.Id_Fam
                                        ,producto.Id_Sub == 0 ? Id_Sub: producto.Id_Sub
                                        ,producto.Id_Pvd
                                        ,producto.Prd_Descripcion
                                        ,producto.Prd_Presentacion
                                        ,producto.Prd_InvInicial
                                        ,producto.Prd_InvFinal
                                        ,producto.Prd_AgrupadoSpo
                                        ,producto.Prd_FactorConv
                                        ,producto.Prd_AparatoSisProp
                                        ,producto.Prd_Fisico
                                        ,producto.Prd_Ordenado
                                        ,producto.Prd_Asignado
                                        ,producto.Prd_InvSeg
                                        ,producto.Prd_TTrans
                                        ,producto.Prd_TEntre
                                        ,producto.Prd_Transito
                                        ,producto.Prd_UniNe
                                        ,producto.Prd_UniNs
                                        ,producto.Prd_Unico
                                        ,producto.Prd_UniEmp
                                        ,producto.Prd_Colo
                                        ,producto.Prd_Ren
                                        ,producto.Prd_Mes
                                        ,producto.Prd_CLNomFab
                                        ,producto.Prd_CLIdFab
                                        ,producto.Prd_CLDesFab
                                        ,producto.Prd_CLPreFab
                                        ,producto.Prd_CLNomPro
                                        ,producto.Prd_CLIdPro
                                        ,producto.Prd_CLDesPro
                                        ,producto.Prd_CLPrePro
                                        ,producto.Prd_MaxExistencia
                                        ,producto.Prd_Ubicacion
                                        ,producto.Prd_Contribucion
                                        ,producto.Prd_PorUtilidades
                                        ,producto.Prd_Nuevo
                                        ,producto.Prd_PesConTecnico
                                        ,producto.Prd_CptSv
                                        ,producto.Prd_Activo
                                        ,producto.Prd_FecAlta
                                        ,producto.Prd_Minimo
                                        ,producto.Prd_PlanAbasto
                                        ,producto.NoFacturable
                                        ,producto.Prd_ClaveProdServ
                                        ,producto.Prd_ClaveUnidad
                                   };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatProducto_Insertar", ref verificador, Parametros, Valores);

                // ------------------------------------------------------------
                // Eliminar datos de precio de productos del producto actual
                // ------------------------------------------------------------
                string[] ParametrosDelete = {
                                        "@Id_Emp",
                                        "@Id_Cd",
                                        "@Id_Prd",
                                      };
                object[] ValoresDelete = {
                                        producto.Id_Emp
                                        ,producto.Id_Cd
                                        ,producto.Id_Prd
                                   };
                sqlcmd = CapaDatos.GenerarSqlCommand("spProductoPrecios_Eliminar", ref verificador, ParametrosDelete, ValoresDelete);

                // ------------------------------------------------------------
                // Insertar datos de precio de productos
                // ------------------------------------------------------------
                foreach (ProductoPrecios productoPrecios in producto.ListaProductoPrecios)
                {
                    string[] ParametrosInsert = {
                                        "@Id_Emp",
                                        "@Id_Cd",
                                        "@Id_Prd",
                                        "@Id_Pre",
                                        "@Prd_Actual",
                                        "@Prd_FechaInicio",
                                        "@Prd_FechaFin",
                                        "@Prd_PreDescripcion",
                                        "@Prd_Pesos"
                                      };
                    object[] ValoresInsert = {
                                        productoPrecios.Id_Emp
                                        ,productoPrecios.Id_Cd
                                        ,producto.Id_Prd //toma la clave de producto del 'papa'
                                        ,productoPrecios.Id_Pre
                                        ,productoPrecios.Prd_Actual
                                        ,productoPrecios.Prd_FechaInicio
                                        ,productoPrecios.Prd_FechaFin
                                        ,productoPrecios.Prd_PreDescripcion
                                        ,productoPrecios.Prd_Pesos
                                   };
                    //Inserta solo si las fechas son validas para SQL Server
                    if (Convert.ToDateTime(productoPrecios.Prd_FechaFin).CompareTo(new DateTime(1753, 1, 1)) > 0 && Convert.ToDateTime(productoPrecios.Prd_FechaInicio).CompareTo(new DateTime(1753, 1, 1)) > 0)
                        sqlcmd = CapaDatos.GenerarSqlCommand("spProductoPrecios_Insertar", ref verificador, ParametrosInsert, ValoresInsert);
                }
                CapaDatos.CommitTrans();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                CapaDatos.RollBackTrans();
                throw ex;
            }
        }

        public void ModificarProducto(Producto producto, string Conexion, ref int verificador)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            try
            {
                CapaDatos.StartTrans();

                object Id_Spo = null;
                object Id_Cpr = null;
                object Id_Fam = null;
                object Id_Sub = null;

                string[] Parametros = {
                                        "@Id_Emp"
                                        ,"@Id_Cd"
                                        ,"@Id_Prd"
                                        ,"@Id_Spo"
                                        ,"@Id_Ptp"
                                        ,"@Id_Cpr"
                                        ,"@Id_Fam"
                                        ,"@Id_Sub"
                                        ,"@Id_Pvd"
                                        ,"@Prd_Descripcion"
                                        ,"@Prd_Presentacion"
                                        ,"@Prd_InvInicial"
                                        ,"@Prd_InvFinal"
                                        ,"@Prd_AgrupadoSpo"
                                        ,"@Prd_FactorConv"
                                        ,"@Prd_AparatoSisProp"
                                        ,"@Prd_Fisico"
                                        ,"@Prd_Ordenado"
                                        ,"@Prd_Asignado"
                                        ,"@Prd_InvSeg"
                                        ,"@Prd_TTrans"
                                        ,"@Prd_TEntre"
                                        ,"@Prd_Transito"
                                        ,"@Prd_UniNe"
                                        ,"@Prd_UniNs"
                                        ,"@Prd_Unico"
                                        ,"@Prd_UniEmp"
                                        ,"@Prd_Colo"
                                        ,"@Prd_Ren"
                                        ,"@Prd_Mes"
                                        ,"@Prd_CLNomFab"
                                        ,"@Prd_CLIdFab"
                                        ,"@Prd_CLDesFab"
                                        ,"@Prd_CLPreFab"
                                        ,"@Prd_CLNomPro"
                                        ,"@Prd_CLIdPro"
                                        ,"@Prd_CLDesPro"
                                        ,"@Prd_CLPrePro"
                                        ,"@Prd_MaxExistencia"
                                        ,"@Prd_Ubicacion"
                                        ,"@Prd_Contribucion"
                                        ,"@Prd_PorUtilidades"
                                        ,"@Prd_Nuevo"
                                        ,"@Prd_PesConTecnico"
                                        ,"@Prd_CptSv"
                                        ,"@Prd_Activo"
                                        ,"@Prd_Minimo"
                                        ,"Prd_PlanAbasto"
                                        ,"@Prd_ClaveProdServ"
                                        ,"@Prd_ClaveUnidad"
                                      };
                object[] Valores = {
                                        producto.Id_Emp
                                        ,producto.Id_Cd
                                        ,producto.Id_Prd
                                        ,producto.Id_Spo == 0 ? Id_Spo : producto.Id_Spo
                                        ,producto.Id_Ptp
                                        ,producto.Id_Cpr == 0 ? Id_Cpr: producto.Id_Cpr
                                        ,producto.Id_Fam == 0 ? Id_Fam: producto.Id_Fam
                                        ,producto.Id_Sub == 0 ? Id_Sub: producto.Id_Sub
                                        ,producto.Id_Pvd
                                        ,producto.Prd_Descripcion
                                        ,producto.Prd_Presentacion
                                        ,producto.Prd_InvInicial
                                        ,producto.Prd_InvFinal
                                        ,producto.Prd_AgrupadoSpo
                                        ,producto.Prd_FactorConv
                                        ,producto.Prd_AparatoSisProp
                                        ,producto.Prd_Fisico
                                        ,producto.Prd_Ordenado
                                        ,producto.Prd_Asignado
                                        ,producto.Prd_InvSeg
                                        ,producto.Prd_TTrans
                                        ,producto.Prd_TEntre
                                        ,producto.Prd_Transito
                                        ,producto.Prd_UniNe
                                        ,producto.Prd_UniNs
                                        ,producto.Prd_Unico
                                        ,producto.Prd_UniEmp
                                        ,producto.Prd_Colo
                                        ,producto.Prd_Ren
                                        ,producto.Prd_Mes
                                        ,producto.Prd_CLNomFab
                                        ,producto.Prd_CLIdFab
                                        ,producto.Prd_CLDesFab
                                        ,producto.Prd_CLPreFab
                                        ,producto.Prd_CLNomPro
                                        ,producto.Prd_CLIdPro
                                        ,producto.Prd_CLDesPro
                                        ,producto.Prd_CLPrePro
                                        ,producto.Prd_MaxExistencia
                                        ,producto.Prd_Ubicacion
                                        ,producto.Prd_Contribucion
                                        ,producto.Prd_PorUtilidades
                                        ,producto.Prd_Nuevo
                                        ,producto.Prd_PesConTecnico
                                        ,producto.Prd_CptSv
                                        ,producto.Prd_Activo
                                        ,producto.Prd_Minimo
                                        ,producto.Prd_PlanAbasto
                                        ,producto.Prd_ClaveProdServ
                                        ,producto.Prd_ClaveUnidad
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatProducto_Modificar", ref verificador, Parametros, Valores);

                // ------------------------------------------------------------
                // Eliminar datos de precio de productos del producto actual
                // ------------------------------------------------------------
                string[] ParametrosDelete = {
                                        "@Id_Emp",
                                        "@Id_Cd",
                                        "@Id_Prd",
                                      };
                object[] ValoresDelete = {
                                        producto.Id_Emp
                                        ,producto.Id_Cd
                                        ,producto.Id_Prd
                                   };
                sqlcmd = CapaDatos.GenerarSqlCommand("spProductoPrecios_Eliminar", ref verificador, ParametrosDelete, ValoresDelete);

                // ------------------------------------------------------------
                // Insertar datos de precio de productos
                // ------------------------------------------------------------
                foreach (ProductoPrecios productoPrecios in producto.ListaProductoPrecios)
                {
                    string[] ParametrosInsert = {
                                        "@Id_Emp",
                                        "@Id_Cd",
                                        "@Id_Prd",
                                        "@Id_Pre",
                                        "@Prd_Actual",
                                        "@Prd_FechaInicio",
                                        "@Prd_FechaFin",
                                        "@Prd_PreDescripcion",
                                        "@Prd_Pesos"
                                      };
                    object[] ValoresInsert = {
                                        productoPrecios.Id_Emp
                                        ,productoPrecios.Id_Cd
                                        ,producto.Id_Prd //toma la clave de producto del 'papa'
                                        ,productoPrecios.Id_Pre
                                        ,productoPrecios.Prd_Actual
                                        ,productoPrecios.Prd_FechaInicio
                                        ,productoPrecios.Prd_FechaFin
                                        ,productoPrecios.Prd_PreDescripcion
                                        ,productoPrecios.Prd_Pesos
                                   };

                    //Inserta solo si las fechas son validas para SQL Server
                    if (Convert.ToDateTime(productoPrecios.Prd_FechaFin).CompareTo(new DateTime(1753, 1, 1)) > 0 && Convert.ToDateTime(productoPrecios.Prd_FechaInicio).CompareTo(new DateTime(1753, 1, 1)) > 0)
                    {
                        sqlcmd = CapaDatos.GenerarSqlCommand("spProductoPrecios_Insertar", ref verificador, ParametrosInsert, ValoresInsert);
                    }
                }
                CapaDatos.CommitTrans();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                CapaDatos.RollBackTrans();
                throw ex;
            }
        }

        public void ConsultaProductoCte(ref Producto producto, string Conexion, int cliente)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_Prd", "@Id_Cte", "@Fecha" };
                object[] Valores = { producto.Id_Emp, producto.Id_Cd, producto.Id_Prd, cliente, producto.Fecha };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatProductoCte_Consulta", ref dr, Parametros, Valores);

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        producto = new Producto();
                        producto.Prd_Presentacion = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Presentacion"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_Presentacion"));
                        producto.Prd_Precio = dr.IsDBNull(dr.GetOrdinal("Prd_Precio")) ? "0" : dr.GetValue(dr.GetOrdinal("Prd_Precio")).ToString();
                        producto.Prd_UniNom = dr.GetValue(dr.GetOrdinal("Prd_UniNom")).ToString();
                        producto.Tprecio = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Prd_TPrecio")));
                        producto.Sol_PEsp = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Sol_PEsp")));
                        producto.Prd_Descripcion = dr.GetValue(dr.GetOrdinal("Prd_Descripcion")).ToString();
                        producto.Prd_FechaFinEsp = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("Prd_FechaFin")));
                    }
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultarMaxLocal(int Id_Cd, int Id_Emp, ref int max, string Conexion)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = { "@Id_Emp", "@Id_Cd" };
                object[] Valores = { Id_Emp, Id_Cd };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatProductoMaxLocal_Consultar", ref dr, Parametros, Valores);

                if (dr.HasRows)
                {
                    dr.Read();
                    max = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Loc_Max")));
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaProducto_Disponible(int Empresa, int Cd, string Prd, ref List<int> Actuales, string Conexion)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_Prd" };
                object[] Valores = { Empresa, Cd, Prd };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatProducto_Disponible", ref dr, Parametros, Valores);

                if (dr.HasRows)
                {
                    dr.Read();
                    Actuales.Add(Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Prd_InvFinal"))));
                    Actuales.Add(Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Prd_Asignado"))));
                    Actuales.Add(Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Prd_Disponible"))));
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaProductoAsig_Admin(Producto prd, string Conexion, ref List<Producto> List)
        {
            try
            {
                string aplicaAutorizacion = System.Configuration.ConfigurationManager.AppSettings["AplicaAutorizacion"].ToString();
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = {
                                          "@Id_Emp",
                                          "@Id_Cd",
                                          "@Filtro_Nombre",
                                          "@Filtro_PrdIni",
                                          "@Filtro_PrdFin",
                                          "@Filtro_Autorizados"
                                      };
                object[] Valores = {
                                       prd.Id_Emp,
                                       prd.Id_Cd,
                                       prd.Filtro_Nombre,
                                       prd.Filtro_PrdIni==""?(object)null:prd.Filtro_PrdIni,
                                       prd.Filtro_PrdFin==""?(object)null:prd.Filtro_PrdFin,
                                       aplicaAutorizacion
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spProAsignPedxPrd_Lista", ref dr, Parametros, Valores);


                while (dr.Read())
                {
                    prd = new Producto();
                    prd.Id_Prd = Convert.ToInt64(dr.GetValue(dr.GetOrdinal("Id_Prd")));
                    prd.Prd_Descripcion = dr.GetValue(dr.GetOrdinal("Prd_Descripcion")).ToString();
                    prd.Prd_Asignado = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Prd_Asignado")));
                    prd.Prd_InvFinal = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Prd_InvFinal")));
                    prd.Prd_Presentacion = dr.GetValue(dr.GetOrdinal("Prd_Presentacion")).ToString();
                    prd.Prd_Ordenado = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Prd_Ordenado")));
                    prd.Prd_Sobrante = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Prd_Sobrante")));
                    prd.Prd_Pendiente = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Prd_Pendiente")));

                    List.Add(prd);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }




        public void ConsultaAsignPedxPrd(ProductoDet prdDet, string Conexion, ref List<ProductoDet> List)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = {
                                          "@Id_Emp",
                                          "@Id_Cd",
                                          "@Id_Prd"
                                      };
                object[] Valores = {
                                        prdDet.Id_Emp,
                                        prdDet.Id_Cd,
                                        prdDet.Id_Prd
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spProAsignPedxPrd_Consultar", ref dr, Parametros, Valores);

                ProductoDet p;
                while (dr.Read())
                {
                    p = new ProductoDet();
                    p.Id_Ped = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Ped")));
                    p.Ped_Fecha = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("Ped_Fecha")));
                    p.Id_Ter = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Ter")));
                    p.Id_Cte = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cte")));
                    p.Cte_NomComercial = dr.GetValue(dr.GetOrdinal("Cte_NomComercial")).ToString();
                    p.Cte_Credito = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Cte_Credito")));
                    p.Cte_CreditoStr = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Cte_Credito"))) ? "Si" : "No";
                    p.Ped_Ord = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Ped_Ord")));
                    p.Ped_Disp = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Ped_Disp")));
                    p.Ped_Asignar = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Ped_Asignar")));
                    p.Ped_Faltante = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Ped_Faltante")));
                    p.Prd_InvFinal = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Prd_InvFinal")));
                    p.Prd_Disp = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Prd_Disp")));
                    p.Id_PedDet = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_PedDet")));
                    p.Ped_Picking = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Ped_picking")));
                    List.Add(p);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }




        public void AsignarPedXPrd(Pedido pedido, string Conexion, List<ProductoDet> list, ref int verificador, int asignable, long Id_Prd)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            try
            {
                SqlCommand sqlcmd = default(SqlCommand);
                CapaDatos.StartTrans();

                List<int> actuales = new List<int>();
                CD_CatProducto claseCapaDatos = new CD_CatProducto();
                claseCapaDatos.ConsultaProducto_Disponible(pedido.Id_Emp, pedido.Id_Cd, Id_Prd.ToString(), ref actuales, Conexion);

                if (asignable > actuales[2])
                {
                    throw new Exception("inventario_insuficiente");
                }

                string[] Parametros = {
                                          "@Id_Emp",
                                          "@Id_Cd",
                                          "@Id_Ped",
                                          "@Id_Prd",
                                          "@Id_Asig",
                                          "@FecAsig",
                                          "@UsrAsig",
                                          "@Id_PedDet"
                                      };
                object[] Valores = null;

                for (int x = 0; x < list.Count; x++)
                {
                    Valores = new object[] {
                                        pedido.Id_Emp,
                                        pedido.Id_Cd,
                                        list[x].Id_Ped,
                                        list[x].Id_Prd,
                                        list[x].Ped_Asignar,
                                        pedido.Ped_Fecha,
                                        pedido.Id_U,
                                        list[x].Id_PedDet

                                   };

                    sqlcmd = CapaDatos.GenerarSqlCommand("spProAsignPedxPrd", ref verificador, Parametros, Valores);
                    if (verificador == 2 || verificador == 3)
                    {
                        CapaDatos.RollBackTrans();
                        CapaDatos.LimpiarSqlcommand(ref sqlcmd);
                        return;
                    }
                }
                CapaDatos.CommitTrans();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                CapaDatos.RollBackTrans();
                throw ex;
            }
        }

        public void ConsultaListaProducto(Producto prd, string Conexion, int Id_Cte, int Id_Acs, ref List<Producto> list)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_Cte", "@Id_Acs" };
                object[] Valores = { prd.Id_Emp, prd.Id_Cd, Id_Cte, Id_Acs == -1 ? (object)null : Id_Acs };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatProductoCte_Lista", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    prd = new Producto();
                    prd.Id_Prd = dr.GetInt32(dr.GetOrdinal("Id_Prd"));
                    prd.Prd_Descripcion = dr.GetString(dr.GetOrdinal("Prd_Descripcion"));
                    prd.Prd_Presentacion = dr.GetString(dr.GetOrdinal("Prd_Presentacion"));
                    prd.Uni_Descripcion = dr.GetString(dr.GetOrdinal("Uni_Descripcion"));
                    prd.Prd_Precio = dr.GetValue(dr.GetOrdinal("Clp_Pesos")).ToString();
                    list.Add(prd);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void ConsultarProduto_Ecommerce(Producto producto, string Conexion, ref int verificador)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_Prd" };
                object[] Valores = { producto.Id_Emp, producto.Id_Cd, producto.Id_Prd };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatProducto_Ecommerce", ref verificador, Parametros, Valores);


                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultarVentas(Producto producto, string Conexion, int Id_Cte)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_Cte", "@Id_Prd" };
                object[] Valores = { producto.Id_Emp, producto.Id_Cd, Id_Cte, producto.Id_Prd };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spVentas", ref dr, Parametros, Valores);

                producto.ventaMes = new List<double>();
                producto.ventaMesDescr = new List<string>();

                while (dr.Read())
                {
                    producto.ventaMes.Add(Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Venta"))));
                    producto.ventaMesDescr.Add(dr.GetValue(dr.GetOrdinal("Cal_MesDesc")).ToString());
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        // Consulta si el producto ya se encuentra en compras locales
        public int spCL_ConsultarProducto(
            int Id_Emp, int Id_Cd, Int64 Id_Prd, string Conexion)
        {
            int iContar = 0;
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_Prd" };
                object[] Valores = { Id_Emp, Id_Cd, Id_Prd };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCL_ConsultarProducto", ref dr, Parametros, Valores);
                while (dr.Read())
                {
                    iContar = dr.GetInt32(dr.GetOrdinal("iContar"));
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                iContar = -1;
            }
            return iContar;
        }

        public void ValidarProductoEcommerce(Int64 producto, string Conexion, ref int Verificador)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Prd" };
                object[] Valores = { producto };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapPedido_ProductoEccomerce", ref Verificador, Parametros, Valores);


                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaListaProducto(Producto prd, int Id_Acs, string Conexion, ref List<Comun> list)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_Prd", "@Id_Acs" };
                object[] Valores = { prd.Id_Emp, prd.Id_Cd, prd.Id_Prd, Id_Acs };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatProductoGrp_Lista", ref dr, Parametros, Valores);

                Comun com;
                while (dr.Read())
                {
                    com = new Comun();
                    com.IdStr = dr.GetInt32(dr.GetOrdinal("Id")).ToString();
                    com.Descripcion = dr.GetString(dr.GetOrdinal("Descripcion"));
                    com.ValorBool = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("ValorBool")));
                    list.Add(com);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaBuscar(Producto prd, string Conexion, ref List<Comun> List, object FiltroId, object FiltroDesc)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_Pvd", "@FiltroId", "@FiltroDesc" };
                object[] Valores = { prd.Id_Emp, prd.Id_Cd, prd.Id_Pvd, FiltroId, FiltroDesc };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatProductoBuscar_Lista", ref dr, Parametros, Valores);

                Comun com;
                while (dr.Read())
                {
                    com = new Comun();
                    com.IdStr = dr.GetInt32(dr.GetOrdinal("Id")).ToString();
                    com.Descripcion = dr.GetString(dr.GetOrdinal("Descripcion"));

                    List.Add(com);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // Compras Locales
        // ENE21-2020 RFH -  Busqueda de producto 
        public List<CapaEntidad.Producto> BusquedaDeProducto(string Termino, string Conexion)
        {
            List<CapaEntidad.Producto> Lst = new List<CapaEntidad.Producto>();
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = { "@Termino" };
                object[] Valores = { Termino };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spAABuscaProducto_Ver2", ref dr, Parametros, Valores);
                while (dr.Read())
                {
                    CapaEntidad.Producto obj = new Producto();
                    obj.Id_Prd = Convert.ToInt64(dr.GetValue(dr.GetOrdinal("Id_Prd")));
                    obj.Prd_Descripcion = dr.GetString(dr.GetOrdinal("Prd_Descripcion"));
                    Lst.Add(obj);
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                Lst = null;
            }
            return Lst;
        }

        // Compras Locales
        // FEB7-2020 RFH 

        public List<CapaEntidad.Producto> Busqueda_spAABuscaProductosCompraLocalTodos(long Id_Prd, string Conexion)
        {
            List<CapaEntidad.Producto> Lst = new List<CapaEntidad.Producto>();
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = { "@Id_Prd" };
                object[] Valores = { Id_Prd };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spAABuscaProductosCompraLocalTodos_ver2", ref dr, Parametros, Valores);
                while (dr.Read())
                {
                    CapaEntidad.Producto obj = new Producto();
                    obj.Id_Prd = Convert.ToInt64(dr.GetValue(dr.GetOrdinal("Id_Prd")));
                    obj.Prd_Descripcion = dr.GetString(dr.GetOrdinal("Prd_Descripcion"));
                    Lst.Add(obj);
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                Lst = null;
            }
            return Lst;
        }

        public void ConsultaProducto_Descripcion(long Id_Prd, ref string Prd_Descripcion, string Conexion)
        {
            try
            {
                CD_Datos cd_datos = new CD_Datos(Conexion);
                SqlDataReader dr = null;

                string[] Parametros = { "@Id_Prd" };
                object[] Valores = { Id_Prd };

                SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("spCatProducto_ConsultaDescripcion", ref dr, Parametros, Valores);

                if (dr.Read())
                {
                    Prd_Descripcion = dr["Prd_Descripcion"].ToString();
                }
                dr.Close();

                cd_datos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <summary>
        /// Versión que acepta un contexto de conexión a la fuente de datos
        /// </summary>
        /// <param name="Id_Prd"></param>
        /// <param name="Conexion"></param>
        /// <param name="EsPapel"></param>
        /// <param name="Prd_PesosConTecnico"></param>
        /// <param name="Prd_Mes"></param>
        /// <param name="Prd_PesosAAA"></param>
        /// <param name="icdCtx">Contexto de conexión a la fuente de datos</param>
        public void CatProducto_Informacion_VP(long Id_Prd, string Conexion, ref string EsPapel, ref double Prd_PesosConTecnico, ref Int32 Prd_Mes, ref double Prd_PesosAAA, ICD_Contexto icdCtx)
        {
            try
            {
                SqlDataReader dr = null;

                string[] Parametros = { "@Id_Prd" };
                object[] Valores = { Id_Prd };

                SqlCommand sqlcmd = CD_Datos.GenerarSqlCommand("spCatProducto_Informacion_VP", ref dr, Parametros, Valores, icdCtx);

                if (dr.Read())
                {
                    EsPapel = dr.GetString(dr.GetOrdinal("SiEsPapel"));
                    Prd_PesosConTecnico = dr.GetDouble(dr.GetOrdinal("Prd_PesConTecnico"));
                    Prd_Mes = dr.GetInt32(dr.GetOrdinal("Prd_Mes"));
                    Prd_PesosAAA = dr.GetDouble(dr.GetOrdinal("Prd_PesosAAA"));
                }
                else
                {
                    EsPapel = "N";
                    Prd_PesosConTecnico = 0;
                    Prd_Mes = 0;
                    Prd_PesosAAA = 0;
                }
                dr.Close();
                sqlcmd.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void CatProducto_Informacion_VP(long Id_Prd, string Conexion, ref string EsPapel, ref double Prd_PesosConTecnico, ref Int32 Prd_Mes, ref double Prd_PesosAAA)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Prd" };
                object[] Valores = { Id_Prd };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatProducto_Informacion_VP", ref dr, Parametros, Valores);

                if (dr.Read())
                {
                    EsPapel = dr.GetString(dr.GetOrdinal("SiEsPapel"));
                    Prd_PesosConTecnico = dr.GetDouble(dr.GetOrdinal("Prd_PesConTecnico"));
                    Prd_Mes = dr.GetInt32(dr.GetOrdinal("Prd_Mes"));
                    Prd_PesosAAA = dr.GetDouble(dr.GetOrdinal("Prd_PesosAAA"));
                }
                else
                {
                    EsPapel = "N";
                    Prd_PesosConTecnico = 0;
                    Prd_Mes = 0;
                    Prd_PesosAAA = 0;
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// Determina si un producto tiene marcado el atributo "No Facturable"
        /// </summary>
        /// <param name="idEmp">Identificador de la empresa</param>
        /// <param name="idCd">Identificador del centro de distribución</param>
        /// <param name="idPrd">Identificador del producto</param>
        /// <param name="conexion">Cadena de conexión</param>
        /// <returns>true si el producto es "No Facturable"; false en caso contrario</returns>
        public bool EsProductoNoFacturable(int idEmp, int idCd, long idPrd, string conexion)
        {
            bool res = false;
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(conexion);

                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_Prd" };
                object[] Valores = { idEmp, idCd, idPrd };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatProducto_EsNoFacturable", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    res = dr.GetBoolean(dr.GetOrdinal("NoFacturable"));
                    break;
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return res;
        }

        /// <summary>
        /// Devuelve el resultado de la consulta a la entidad [CatProducto], dado el identificador del producto.
        /// </summary>
        /// <param name="idEmp">Identificador de la empresa a la que pertenece el centro de distribución idCd</param>
        /// <param name="idCd">Identificador del centro de distribución en donde se encuentra el producto idPrd</param>
        /// <param name="idPrd">Identificador del producto de interés</param>
        /// <param name="cadenaConexionEF">Cadena de conexión a la fuente de datos con formato compatible con Entity Framework</param>
        /// <returns>CatProducto en caso de que el producto se encuentre en el repositorio; null en caso contrario.</returns>
        public CatProducto ConsultarPorId(int idEmp, int idCd, long idPrd, string cadenaConexionEF)
        {
            CatProducto resultado = null;
            using (sianwebmty_gEntities ctx = new sianwebmty_gEntities(cadenaConexionEF))
            {
                var productos = (from p in ctx.CatProductoes
                                 where p.Id_Emp == idEmp && p.Id_Cd == idCd && p.Id_Prd == idPrd
                                 select p).ToList();
                if (productos.Count > 0)
                {
                    resultado = productos[0];
                }
            }
            return resultado;
        }

        /// <summary>
        /// Devuelve el resultado de la consulta a la entidad [CatProducto], dado el identificador del producto.
        /// </summary>
        /// <param name="idEmp">Identificador de la empresa a la que pertenece el centro de distribución idCd</param>
        /// <param name="idCd">Identificador del centro de distribución en donde se encuentra el producto idPrd</param>
        /// <param name="idPrd">Identificador del producto de interés</param>
        /// <param name="cadenaConexionEF">Cadena de conexión a la fuente de datos con formato compatible con Entity Framework</param>
        /// <param name="icdCtx">Transacción de capa de datos</param>
        /// <returns>CatProducto en caso de que el producto se encuentre en el repositorio; null en caso contrario.</returns>
        /// 

        //  
        // 17JUN-2019 RFH
        // Se modifica ; se comenta la opcion de validacion de prd_activo 
        // Ya que para ejecutar este proceso no debe hacer validacion de producto
        // Ya que el prodcuto paso en el proceso anteriro como activo.
        //

        public CatProducto ConsultarPorId(int idEmp, int idCd, long idPrd, ICD_Contexto icdCtx)
        {
            CatProducto resultado = null;
            sianwebmty_gEntities ctx = ((ICD_Contexto<sianwebmty_gEntities>)icdCtx).Contexto;
            var productos = (from p in ctx.CatProductoes
                                 //where p.Id_Emp == idEmp /*&& p.Id_Cd == idCd*/ && p.Id_Prd == idPrd && p.Prd_Activo == true
                             where p.Id_Emp == idEmp /*&& p.Id_Cd == idCd*/ && p.Id_Prd == idPrd
                             select p).ToList();
            if (productos.Count > 0)
            {
                resultado = productos[0];
            }
            return resultado;
        }
        public void ConsultaProducto_ClaveProveedor(Int64 Id_Prd, ref string Prd_ClaveProveedor, string Conexion)
        {
            try
            {
                CD_Datos cd_datos = new CD_Datos(Conexion);
                SqlDataReader dr = null;

                string[] Parametros = { "@Id_Prd" };
                object[] Valores = { Id_Prd };

                SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("spCatProducto_ConsultaClaveProveedor", ref dr, Parametros, Valores);

                if (dr.Read())
                {
                    Prd_ClaveProveedor = dr["Prd_ClaveProv"].ToString();
                }
                dr.Close();

                cd_datos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        //RBM Nov 2023
        //Se consultan los tipos de presentaciones desde Catalogo Unico
        public List<CapaEntidad.eListaGenerica> spPresentacion_ComboCompraLocal(int Id_Emp, string Conexion)
        {
            List<CapaEntidad.eListaGenerica> Lst = new List<CapaEntidad.eListaGenerica>();
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { };
                object[] Valores = { };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spPresentacion_ComboCompraLocal", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    CapaEntidad.eListaGenerica obj = new CapaEntidad.eListaGenerica();
                    obj.Id = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id")));
                    obj.Descripcion = dr.GetValue(dr.GetOrdinal("Descripcion")).ToString();
                    Lst.Add(obj);
                }
            }
            catch (Exception ex)
            {
                //throw ex;
                Lst = null;
            }
            return Lst;
        }

        public Producto ConsultaCompraLocalById(ref int Validador, int id_Emp, int id_Cd, int id_Cd_Ver, long id_Prd, bool catalogo, string conexion)
        {
            CapaEntidad.Producto producto = new CapaEntidad.Producto();

            Validador = 0;
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(conexion);

                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_Cd_Ver", "@Id_Prd" };
                object[] Valores = { id_Emp,
                                       id_Cd <= 0 ? (object)null : id_Cd,
                                       id_Cd_Ver,
                                       id_Prd
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCompraLocal_Consulta_CL", ref dr, Parametros, Valores);

                if (dr.Read())
                {
                    producto.Id_Emp = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Emp")));
                    producto.Id_Cd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cd")));
                    producto.Id_Prd = Convert.ToInt64(dr.GetValue(dr.GetOrdinal("Id_Prd")));
                    producto.Id_Spo = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Spo")));

                    producto.Id_Ptp = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Ptp")));

                    producto.Id_Cpr = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Cpr"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Id_Cpr"));
                    producto.Id_Fam = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Fam"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Id_Fam"));
                    producto.Id_Sub = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Sub"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Id_Sub"));
                    producto.Id_Pvd = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Pvd"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Id_Pvd"));
                    producto.Prd_Descripcion = dr.GetString(dr.GetOrdinal("Prd_Descripcion"));
                    producto.Prd_Presentacion = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Presentacion"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_Presentacion"));

                    producto.Prd_InvInicial = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_InvInicial"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_InvInicial"));
                    producto.Prd_InvFinal = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_InvFinal"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_InvFinal"));
                    producto.Prd_AgrupadoSpo = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_AgrupadoSpo"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_AgrupadoSpo"));
                    producto.Prd_FactorConv = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_FactorConv"))) ? 0 : Convert.ToSingle(dr.GetValue(dr.GetOrdinal("Prd_FactorConv")));
                    producto.Prd_AparatoSisProp = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_AparatoSisProp"))) ? false : dr.GetBoolean(dr.GetOrdinal("Prd_AparatoSisProp"));
                    producto.Prd_Fisico = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Fisico"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_Fisico"));
                    producto.Prd_Ordenado = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Ordenado"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_Ordenado"));
                    producto.Prd_Asignado = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Asignado"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_Asignado"));
                    producto.Prd_InvSeg = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_InvSeg"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_InvSeg"));
                    producto.Prd_TTrans = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_TTrans"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_TTrans"));
                    producto.Prd_TEntre = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_TEntre"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_TEntre"));
                    producto.Prd_Transito = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Transito"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_Transito"));
                    producto.Prd_UniNe = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_UniNe"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_UniNe"));
                    producto.Prd_UniNs = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_UniNs"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_UniNs"));

                    producto.Prd_InvInicial = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_InvInicial"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_InvInicial"));
                    producto.Prd_InvFinal = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_InvFinal"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_InvFinal"));
                    producto.Prd_Unico = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Unico"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_Unico"));
                    producto.Prd_UniEmp = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_UniEmp"))) ? 0 : Convert.ToSingle(dr.GetValue(dr.GetOrdinal("Prd_UniEmp")));
                    producto.Prd_Colo = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Colo"))) ? false : dr.GetBoolean(dr.GetOrdinal("Prd_Colo"));
                    producto.Prd_Ren = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Ren"))) ? Convert.ToChar(' ') : Convert.ToChar(dr.GetValue(dr.GetOrdinal("Prd_Ren")));
                    producto.Prd_Mes = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Mes"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_Mes"));
                    producto.Prd_CLNomFab = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_CLNomFab"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_CLNomFab"));
                    producto.Prd_CLIdFab = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_CLIdFab"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_CLIdFab"));
                    producto.Prd_CLDesFab = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_CLDesFab"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_CLDesFab"));
                    producto.Prd_CLPreFab = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_CLPreFab"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_CLPreFab"));
                    producto.Prd_CLNomPro = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_CLNomPro"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_CLNomPro"));
                    producto.Prd_CLIdPro = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_CLIdPro"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_CLIdPro"));
                    producto.Prd_CLDesPro = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_CLDesPro"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_CLDesPro"));
                    producto.Prd_CLPrePro = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_CLPrePro"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_CLPrePro"));
                    producto.Prd_MaxExistencia = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_MaxExistencia"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_MaxExistencia"));
                    producto.Prd_Ubicacion = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Ubicacion"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_Ubicacion"));
                    producto.Prd_Contribucion = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Contribucion"))) ? 0 : Convert.ToSingle(dr.GetValue(dr.GetOrdinal("Prd_Contribucion")));
                    producto.Prd_PorUtilidades = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_PorUtilidades"))) ? 0 : Convert.ToSingle(dr.GetValue(dr.GetOrdinal("Prd_PorUtilidades")));
                    producto.Prd_Nuevo = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Nuevo"))) ? true : dr.GetBoolean(dr.GetOrdinal("Prd_Nuevo"));
                    producto.Prd_PesConTecnico = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_PesConTecnico"))) ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Prd_PesConTecnico")));
                    producto.Prd_CptSv = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_CptSv"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_CptSv"));
                    producto.Prd_Activo = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Activo"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_Activo"));  //Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Activo"))) ? false : dr.GetBoolean(dr.GetOrdinal("Prd_Activo"));
                    producto.Prd_Minimo = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Minimo"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_Minimo"));
                    producto.Prd_PlanAbasto = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_PlanAbasto"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_PlanAbasto"));


                    producto.Id_ClaveSAT = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_ClaveSAT")));
                    producto.Prd_ClaveProdServ = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_ServicioSat"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_ServicioSat"));
                    producto.Id_UnidadSAT = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_UnidadSAT")));
                    producto.Prd_ClaveUnidad = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_UnidadSat"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_UnidadSat"));

                    producto.Aplicacion = dr.GetValue(dr.GetOrdinal("APLICACION")).ToString();
                    producto.Subfamilia = dr.GetValue(dr.GetOrdinal("SUBFAMILIA")).ToString();

                    // NOV 2023
                    //RBM Se agrega proveedor desde CU
                    producto.Prd_IdProvCentral = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_IdProvCentral"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_IdProvCentral"));
                    producto.Prd_NomProvCentral = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_NomProvCentral"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_NomProvCentral"));
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
                Validador = 1;
            }
            catch (Exception ex)
            {
                producto = null;
                Validador = -1;
            }
            return producto;
        }
        public void Consulta_ProductoPrecioObjetivo(ref Producto producto, ref GestionIncrementoPrecios gestionincremento, string Conexion, int id_Emp, int id_Cd, int id_Cd_Ver, long id_Prd, int ValidaInv, int Id_Cte, string Id_Tamaño, int id_rik)
        {
            try
            {

                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp",
                                          "@Id_Cd",
                                          "@Id_Cd_Ver",
                                          "@Id_Prd",
                                          "@Id_Ter",
                                          "@Id_Cte",
                                          "@EmpBen",
                                          "@ValidaInv",
                                          "@Id_Mov",
                                          "@SisProp"
                };

                object[] Valores = { id_Emp,
                                       id_Cd <= 0 ? (object)null : id_Cd,
                                       id_Cd_Ver,
                                       id_Prd,
                                       (producto == null) ? (object)null: (producto.Id_Ter == null) ? (object)null: producto.Id_Ter,
                                       Id_Cte,
                                       (producto == null) ? (object)null: producto.EmpBen,
                                       ValidaInv,
                                       producto == null ? (object)null : producto.Id_Mov,
                                       producto == null ? (object)null : producto.Prd_AparatoSisProp
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatProducto_Consulta", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    producto = new Producto();

                    producto.Id_Emp = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Emp")));
                    producto.Id_Cd = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Cd"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Id_Cd"));
                    producto.Id_Prd = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Prd"))) ? 0 : dr.GetInt64(dr.GetOrdinal("Id_Prd"));
                    producto.Id_Spo = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Spo"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Id_Spo"));
                    producto.Id_Ptp = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Ptp"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Id_Ptp"));
                    producto.Id_Cpr = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Cpr"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Id_Cpr"));
                    producto.Id_Fam = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Fam"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Id_Fam"));
                    producto.Id_Sub = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Sub"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Id_Sub"));
                    producto.Id_Pvd = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Pvd"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Id_Pvd"));
                    producto.Prd_Descripcion = dr.GetString(dr.GetOrdinal("Prd_Descripcion"));
                    producto.Prd_Presentacion = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Presentacion"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_Presentacion"));

                    producto.Prd_InvInicial = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_InvInicial"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_InvInicial"));
                    producto.Prd_InvFinal = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_InvFinal"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_InvFinal"));
                    producto.Prd_AgrupadoSpo = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_AgrupadoSpo"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_AgrupadoSpo"));
                    producto.Prd_FactorConv = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_FactorConv"))) ? 0 : Convert.ToSingle(dr.GetValue(dr.GetOrdinal("Prd_FactorConv")));
                    producto.Prd_AparatoSisProp = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_AparatoSisProp"))) ? false : dr.GetBoolean(dr.GetOrdinal("Prd_AparatoSisProp"));
                    producto.Prd_Fisico = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Fisico"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_Fisico"));
                    producto.Prd_Ordenado = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Ordenado"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_Ordenado"));
                    producto.Prd_Asignado = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Asignado"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_Asignado"));
                    producto.Prd_InvSeg = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_InvSeg"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_InvSeg"));
                    producto.Prd_TTrans = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_TTrans"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_TTrans"));
                    producto.Prd_TEntre = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_TEntre"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_TEntre"));
                    producto.Prd_Transito = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Transito"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_Transito"));
                    producto.Prd_UniNe = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_UniNe"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_UniNe"));
                    producto.Prd_UniNs = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_UniNs"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_UniNs"));

                    producto.Prd_InvInicial = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_InvInicial"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_InvInicial"));
                    producto.Prd_InvFinal = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_InvFinal"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_InvFinal"));
                    producto.Prd_Unico = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Unico"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_Unico"));
                    producto.Prd_UniEmp = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_UniEmp"))) ? 0 : Convert.ToSingle(dr.GetValue(dr.GetOrdinal("Prd_UniEmp")));
                    producto.Prd_Colo = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Colo"))) ? false : dr.GetBoolean(dr.GetOrdinal("Prd_Colo"));
                    producto.Prd_Ren = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Ren"))) ? Convert.ToChar(' ') : Convert.ToChar(dr.GetValue(dr.GetOrdinal("Prd_Ren")));
                    producto.Prd_Mes = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Mes"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_Mes"));
                    producto.Prd_CLNomFab = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_CLNomFab"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_CLNomFab"));
                    producto.Prd_CLIdFab = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_CLIdFab"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_CLIdFab"));
                    producto.Prd_CLDesFab = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_CLDesFab"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_CLDesFab"));
                    producto.Prd_CLPreFab = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_CLPreFab"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_CLPreFab"));
                    producto.Prd_CLNomPro = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_CLNomPro"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_CLNomPro"));
                    producto.Prd_CLIdPro = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_CLIdPro"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_CLIdPro"));
                    producto.Prd_CLDesPro = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_CLDesPro"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_CLDesPro"));
                    producto.Prd_CLPrePro = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_CLPrePro"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_CLPrePro"));
                    producto.Prd_MaxExistencia = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_MaxExistencia"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_MaxExistencia"));
                    producto.Prd_Ubicacion = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Ubicacion"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_Ubicacion"));
                    producto.Prd_Contribucion = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Contribucion"))) ? 0 : Convert.ToSingle(dr.GetValue(dr.GetOrdinal("Prd_Contribucion")));
                    producto.Prd_PorUtilidades = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_PorUtilidades"))) ? 0 : Convert.ToSingle(dr.GetValue(dr.GetOrdinal("Prd_PorUtilidades")));
                    producto.Prd_Nuevo = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Nuevo"))) ? true : dr.GetBoolean(dr.GetOrdinal("Prd_Nuevo"));
                    producto.Prd_PesConTecnico = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_PesConTecnico"))) ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Prd_PesConTecnico")));
                    producto.Prd_CptSv = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_CptSv"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_CptSv"));
                    //producto.Prd_Activo = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Activo"))) ? false : dr.GetBoolean(dr.GetOrdinal("Prd_Activo"));
                    producto.Prd_Activo = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Activo"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_Activo"));
                    // PRECIO de VENTA / ACYS  
                    producto.Prd_Precio = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Precio"))) ? string.Empty : string.Format("{0:0.00}", dr.GetValue(dr.GetOrdinal("Prd_Precio")));
                    producto.Prd_Minimo = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Minimo"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_Minimo"));
                    producto.Prd_PlanAbasto = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_PlanAbasto"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_PlanAbasto"));
                    producto.Prd_AAACadena = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_AAA"))) ? string.Empty : string.Format("{0:0.00}", dr.GetValue(dr.GetOrdinal("Prd_AAA")));

                    producto.Prd_PrecioLista = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("PrecioLista"))) ? string.Empty : string.Format("{0:0.00}", dr.GetValue(dr.GetOrdinal("PrecioLista")));
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

                //ejecutar consulta del producto y traerme los datos de la gestion de incremento
                SqlDataReader dr2 = null;
                CapaDatos.CD_Datos CapaDatos2 = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros2 = { "@Id_Emp",
                                          "@Id_Cd",
                                          "@Id_Prd",
                                          "@Id_Ter",
                                          "@Id_Cte",
                                          "@Id_Tamaño",
                                          "@Id_Rik"

                };

                object[] Valores2 = { id_Emp,
                                       id_Cd_Ver,
                                       id_Prd,
                                       (producto == null) ? (object)null: (producto.Id_Ter == null) ? (object)null: producto.Id_Ter,
                                        Id_Cte,
                                        Id_Tamaño,
                                       id_rik
                                   };

                SqlCommand sqlcmd2 = CapaDatos2.GenerarSqlCommand("spGestionPreciosIncrementoProducto_Consulta", ref dr2, Parametros2, Valores2);

                while (dr2.Read())
                {
                    gestionincremento = new GestionIncrementoPrecios();

                    gestionincremento.Id_Emp = Convert.ToInt32(dr2.GetValue(dr2.GetOrdinal("Id_Emp")));
                    gestionincremento.Id_Cd = Convert.IsDBNull(dr2.GetValue(dr2.GetOrdinal("Id_Cd"))) ? 0 : dr2.GetInt32(dr2.GetOrdinal("Id_Cd"));
                    gestionincremento.Id_Prd = Convert.IsDBNull(dr2.GetValue(dr2.GetOrdinal("Id_Prd"))) ? 0 : dr2.GetInt64(dr2.GetOrdinal("Id_Prd"));
                    //gestionincremento.Id_Spo = Convert.IsDBNull(dr2.GetValue(dr2.GetOrdinal("Id_Spo"))) ? 0 : dr2.GetInt32(dr2.GetOrdinal("Id_Spo"));
                    //gestionincremento.Id_Ptp = Convert.IsDBNull(dr2.GetValue(dr2.GetOrdinal("Id_Ptp"))) ? 0 : dr2.GetInt32(dr2.GetOrdinal("Id_Ptp"));
                    //gestionincremento.Id_Cpr = Convert.IsDBNull(dr2.GetValue(dr2.GetOrdinal("Id_Cpr"))) ? 0 : dr2.GetInt32(dr2.GetOrdinal("Id_Cpr"));
                    //gestionincremento.Id_Fam = Convert.IsDBNull(dr2.GetValue(dr2.GetOrdinal("Id_Fam"))) ? 0 : dr2.GetInt32(dr2.GetOrdinal("Id_Fam"));
                    //gestionincremento.Id_Sub = Convert.IsDBNull(dr2.GetValue(dr2.GetOrdinal("Id_Sub"))) ? 0 : dr2.GetInt32(dr2.GetOrdinal("Id_Sub"));
                    //gestionincremento.Id_Pvd = Convert.IsDBNull(dr2.GetValue(dr2.GetOrdinal("Id_Pvd"))) ? 0 : dr2.GetInt32(dr2.GetOrdinal("Id_Pvd"));
                    gestionincremento.Prd_Descripcion = producto.Prd_Descripcion;
                    //gestionincremento.Prd_Presentacion = Producto.Prd_Presentacion;
                    //gestionincremento.Uni_Descripcion = producto._Prd_UniNs

                    //$('#tbPrecio_' + NR).val(RES.Acs_Precio);
                    //$('#lbSubTotal_' + NR).val(RES.Acs_Precio);
                    //$('#lbPrecioLista_' + NR).text(RES.Prd_PrecioLista);

                    gestionincremento.Id_Rik = id_rik;
                    gestionincremento.Id_Cte = Id_Cte;
                    gestionincremento.Id_Tamaño = Id_Tamaño;


                    gestionincremento.PrecioVenta = Convert.IsDBNull(dr2.GetValue(dr2.GetOrdinal("PrecioVenta"))) ? 0 : Convert.ToDouble(dr2.GetValue(dr2.GetOrdinal("PrecioVenta")));
                    gestionincremento.PrecioListaActual = Convert.IsDBNull(dr2.GetValue(dr2.GetOrdinal("PrecioListaActual"))) ? 0 : Convert.ToDouble(dr2.GetValue(dr2.GetOrdinal("PrecioListaActual")));
                    gestionincremento.PrecioObjetivoProy = Convert.IsDBNull(dr2.GetValue(dr2.GetOrdinal("PrecioObjetivoProy"))) ? 0 : Convert.ToDouble(dr2.GetValue(dr2.GetOrdinal("PrecioObjetivoProy")));
                    gestionincremento.PrecioListaProy = Convert.IsDBNull(dr2.GetValue(dr2.GetOrdinal("PrecioListaProy"))) ? 0 : Convert.ToDouble(dr2.GetValue(dr2.GetOrdinal("PrecioListaProy")));
                    gestionincremento.PrecioMinRikProy = Convert.IsDBNull(dr2.GetValue(dr2.GetOrdinal("PrecioMinRikProy"))) ? 0 : Convert.ToDouble(dr2.GetValue(dr2.GetOrdinal("PrecioMinRikProy")));
                    gestionincremento.PrecioGteProy = Convert.IsDBNull(dr2.GetValue(dr2.GetOrdinal("PrecioGteProy"))) ? 0 : Convert.ToDouble(dr2.GetValue(dr2.GetOrdinal("PrecioGteProy")));
                    gestionincremento.PrecioNegociadoProy = Convert.IsDBNull(dr2.GetValue(dr2.GetOrdinal("PrecioNegociadoProy"))) ? 0 : Convert.ToDouble(dr2.GetValue(dr2.GetOrdinal("PrecioNegociadoProy")));
                    gestionincremento.PorcIncrementoProy = Convert.IsDBNull(dr2.GetValue(dr2.GetOrdinal("PorcIncrementoProy"))) ? 0 : Convert.ToDouble(dr2.GetValue(dr2.GetOrdinal("PorcIncrementoProy")));


                    gestionincremento.DescuentoSobrePlistaProy = Convert.IsDBNull(dr2.GetValue(dr2.GetOrdinal("DescuentoSobrePlistaProy"))) ? 0 : Convert.ToDouble(dr2.GetValue(dr2.GetOrdinal("DescuentoSobrePlistaProy")));
                    gestionincremento.UnidadesProyectadas = Convert.IsDBNull(dr2.GetValue(dr2.GetOrdinal("UnidadesProyectadas"))) ? 0 : Convert.ToDouble(dr2.GetValue(dr2.GetOrdinal("UnidadesProyectadas")));
                    gestionincremento.VentaProy = Convert.IsDBNull(dr2.GetValue(dr2.GetOrdinal("VentaProy"))) ? 0 : Convert.ToDouble(dr2.GetValue(dr2.GetOrdinal("VentaProy")));
                    gestionincremento.MgRed_PesosProy = Convert.IsDBNull(dr2.GetValue(dr2.GetOrdinal("MgRed_PesosProy"))) ? 0 : Convert.ToDouble(dr2.GetValue(dr2.GetOrdinal("MgRed_PesosProy")));
                    gestionincremento.MgRed_PorcProy = Convert.IsDBNull(dr2.GetValue(dr2.GetOrdinal("MgRed_PorcProy"))) ? 0 : Convert.ToDouble(dr2.GetValue(dr2.GetOrdinal("MgRed_PorcProy")));
                    gestionincremento.CostoAAAAFuturo = Convert.IsDBNull(dr2.GetValue(dr2.GetOrdinal("CostoAAAAFuturo"))) ? 0 : Convert.ToDouble(dr2.GetValue(dr2.GetOrdinal("CostoAAAAFuturo")));
                    gestionincremento.Id_Pc = Convert.IsDBNull(dr2.GetValue(dr2.GetOrdinal("Id_Pc"))) ? 0 : Convert.ToInt32(dr2.GetValue(dr2.GetOrdinal("Id_Pc")));
                    gestionincremento.NomCategoria = Convert.IsDBNull(dr2.GetValue(dr2.GetOrdinal("NomCategoria"))) ? "" : Convert.ToString(dr2.GetValue(dr2.GetOrdinal("NomCategoria")));
                    gestionincremento.NombreCategoriaConvenio = Convert.IsDBNull(dr2.GetValue(dr2.GetOrdinal("NombreCategoriaConvenio"))) ? "" : Convert.ToString(dr2.GetValue(dr2.GetOrdinal("NombreCategoriaConvenio")));
                    gestionincremento.Pc_NoConvenio = Convert.IsDBNull(dr2.GetValue(dr2.GetOrdinal("Pc_NoConvenio"))) ? "" : Convert.ToString(dr2.GetValue(dr2.GetOrdinal("Pc_NoConvenio")));
                    gestionincremento.PC_Nombre = Convert.IsDBNull(dr2.GetValue(dr2.GetOrdinal("PC_Nombre"))) ? "" : Convert.ToString(dr2.GetValue(dr2.GetOrdinal("PC_Nombre")));
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}