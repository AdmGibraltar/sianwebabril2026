using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class CD_ComisionesPresupuesto
    {
        #region "CapComisionesPresupuesto"

        public ComisionesPresupuesto CapComisionesPresupuesto_InsertOrUpdate(string Conexion, ComisionesPresupuesto cCapComisionesPresupuesto, ref int verificado)
        {
            using (CapaDatos.dbAccess oDB = new CapaDatos.dbAccess(Conexion))

                try
                {
                    verificado = 0;
                    verificado = (int)oDB.spExecScalar(
                            "spCapComisionesPresupuesto_InsertOrUpdate",
                    new SqlParameter("@Id_Emp", cCapComisionesPresupuesto.Id_Emp),
                    new SqlParameter("@Id_Cd", cCapComisionesPresupuesto.Num_Cdi),
                    new SqlParameter("@Id_Rik", cCapComisionesPresupuesto.Id_Rik),
                    new SqlParameter("@Num_Nomina", cCapComisionesPresupuesto.NumNomina),
                    new SqlParameter("@Venta", cCapComisionesPresupuesto.Venta),
                    new SqlParameter("@UPrima", cCapComisionesPresupuesto.UP),
                    new SqlParameter("@Up_Porc", cCapComisionesPresupuesto.UP_Porc),
                    new SqlParameter("@Mes", cCapComisionesPresupuesto.Mes),
                    new SqlParameter("@Anio", cCapComisionesPresupuesto.Anio),
                    //new SqlParameter("@Fecha_Ult_Mod", cCapComisionesPresupuesto.FechaUltMod),
                    new SqlParameter("@Id_Usuario", cCapComisionesPresupuesto.id_Usuario)

                        );

                    if (verificado != 0)
                    {

                    }

                    return cCapComisionesPresupuesto;

                }
                catch (Exception ex)
                {
                    return null;
                }
        }

        public ComisionesPresupuesto CapComisionesPresupuesto_Insertar(string Conexion, ComisionesPresupuesto cCapComisionesPresupuesto, ref int verificado)
        {


            int verificador = 0;
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            try
            {
                #region mov. inverso
                if (!string.IsNullOrEmpty(cCapComisionesPresupuesto.Id_Rik.ToString()))
                {//sino es refactura entra aquí

                    object[] Valores1 = {
                                cCapComisionesPresupuesto.Id_Emp,
                                cCapComisionesPresupuesto.Num_Cdi,
                                cCapComisionesPresupuesto.Id_Rik,
                                cCapComisionesPresupuesto.Nom_Empleado,
                                cCapComisionesPresupuesto.Anio,
                                cCapComisionesPresupuesto.Base ,
                                cCapComisionesPresupuesto.BaseUP ,
                                cCapComisionesPresupuesto.BaseUP_Porc,
                                cCapComisionesPresupuesto.Venta1 ,
                                cCapComisionesPresupuesto.UP1,
                                cCapComisionesPresupuesto.UP_Porc1,
                                cCapComisionesPresupuesto.Venta2 ,
                                cCapComisionesPresupuesto.UP2,
                                cCapComisionesPresupuesto.UP_Porc2,
                                cCapComisionesPresupuesto.Venta3 ,
                                cCapComisionesPresupuesto.UP3,
                                cCapComisionesPresupuesto.UP_Porc3,
                                cCapComisionesPresupuesto.Venta4 ,
                                cCapComisionesPresupuesto.UP4,
                                cCapComisionesPresupuesto.UP_Porc4,
                                cCapComisionesPresupuesto.Venta5 ,
                                cCapComisionesPresupuesto.UP5,
                                cCapComisionesPresupuesto.UP_Porc5,
                                cCapComisionesPresupuesto.Venta6 ,
                                cCapComisionesPresupuesto.UP6,
                                cCapComisionesPresupuesto.UP_Porc6,
                                cCapComisionesPresupuesto.Venta7 ,
                                cCapComisionesPresupuesto.UP7,
                                cCapComisionesPresupuesto.UP_Porc7,
                                cCapComisionesPresupuesto.Venta8 ,
                                cCapComisionesPresupuesto.UP8,
                                cCapComisionesPresupuesto.UP_Porc8,
                                cCapComisionesPresupuesto.Venta9 ,
                                cCapComisionesPresupuesto.UP9,
                                cCapComisionesPresupuesto.UP_Porc9,
                                cCapComisionesPresupuesto.Venta10 ,
                                cCapComisionesPresupuesto.UP10,
                                cCapComisionesPresupuesto.UP_Porc10,
                                cCapComisionesPresupuesto.Venta11 ,
                                cCapComisionesPresupuesto.UP11,
                                cCapComisionesPresupuesto.UP_Porc11,
                                cCapComisionesPresupuesto.Venta12 ,
                                cCapComisionesPresupuesto.UP12,
                                cCapComisionesPresupuesto.UP_Porc12,
                                cCapComisionesPresupuesto.id_Usuario };
                    string[] Parametros1 = new string[] {
                                            "@Id_Emp",
                                            "@Id_Cd",
                                            "@Id_Rik",
                                            "@Nom_Empleado",
                                            "@Anio",
                                            "@Base",
                                            "@BaseUP",
                                            "@BaseUp_Porc",
                                            "@Venta1",
                                            "@UP1",
                                            "@Up_Porc1",
                                            "@Venta2",
                                            "@UP2",
                                            "@Up_Porc2",
                                            "@Venta3",
                                            "@UP3",
                                            "@Up_Porc3",
                                            "@Venta4",
                                            "@UP4",
                                            "@Up_Porc4",
                                            "@Venta5",
                                            "@UP5",
                                            "@Up_Porc5",
                                            "@Venta6",
                                            "@UP6",
                                            "@Up_Porc6",
                                            "@Venta7",
                                            "@UP7",
                                            "@Up_Porc7",
                                            "@Venta8",
                                            "@UP8",
                                            "@Up_Porc8",
                                            "@Venta9",
                                            "@UP9",
                                            "@Up_Porc9",
                                            "@Venta10",
                                            "@UP10",
                                            "@Up_Porc10",
                                            "@Venta11",
                                            "@UP11",
                                            "@Up_Porc11",
                                            "@Venta12",
                                            "@UP12",
                                            "@Up_Porc12",
                                            "@Id_Usuario" };
                    SqlCommand sqlcmd1 = CapaDatos.GenerarSqlCommand("spCapComisionesPresupuesto_InsertOrUpdate", ref verificador, Parametros1, Valores1);
                    //if (verificador == -2)
                    //{

                    //}

                }
                #endregion

            }
            catch (Exception ex)
            {

                throw ex;
            }

            return cCapComisionesPresupuesto;

        }



        //public ComisionesPresupuesto  CapComisionesPresupuesto_Insertar(string Conexion, ComisionesPresupuesto cCapComisionesPresupuesto, ref int verificado)
        //{


        //    int verificador = 0;
        //    CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
        //    try
        //    {
        //        #region mov. inverso
        //        if (!string.IsNullOrEmpty(cCapComisionesPresupuesto.Id_Rik.ToString()))
        //        {//sino es refactura entra aquí

        //                object[] Valores1 = {    
        //                                cCapComisionesPresupuesto.Id_Emp,
        //                                cCapComisionesPresupuesto.Num_Cdi,
        //                                cCapComisionesPresupuesto.Id_Rik,
        //                                cCapComisionesPresupuesto.NumNomina,
        //                                cCapComisionesPresupuesto.Venta ,
        //                                cCapComisionesPresupuesto.UP ,
        //                                cCapComisionesPresupuesto.UP_Porc,
        //                                cCapComisionesPresupuesto.Mes ,
        //                                cCapComisionesPresupuesto.Anio,
        //                                cCapComisionesPresupuesto.id_Usuario   };
        //                string[] Parametros1 = new string[] {
        //                                    "@Id_Emp",	
        //                                    "@Id_Cd", 
        //                                    "@Id_Rik", 
        //                                    "@Num_Nomina", 
        //                                    "@Venta", 
        //                                    "@UPrima",
        //                                    "@Up_Porc", 
        //                                    "@Mes", 
        //                                    "@Anio",
        //                                    "@Id_Usuario" };
        //                SqlCommand sqlcmd1 = CapaDatos.GenerarSqlCommand("spCapComisionesPresupuesto_InsertOrUpdate", ref verificador, Parametros1, Valores1);
        //                //if (verificador == -2)
        //                //{

        //                //}

        //        }
        //        #endregion

        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }

        //    return cCapComisionesPresupuesto; 

        //}


        public ComisionesPresupuesto CapComisionesPresupuesto_Delete(ComisionesPresupuesto cCapComisionesPresupuesto)
        {
            //try
            //{
            //    new SqlParameter("@Id_Emp", cCapComisionesPresupuesto.Id_Emp);
            //    ExecuteNonQuery(cCapComisionesPresupuesto, "usp_CapComisionesPresupuesto_InsertOrUpdate");
            return cCapComisionesPresupuesto;
            //}
            //catch (Exception ex)
            //{
            //    return null;
            //}
        }
        //public ComisionesPresupuesto CapComisionesPresupuesto_List(ComisionesPresupuesto cCapComisionesPresupuesto)
        //{
        //    try
        //    {
        //        //new SqlParameter("@Id_Emp", cCapComisionesPresupuesto.Id_Emp);
        //        //ExecuteDataset(cCapComisionesPresupuesto, "usp_CapComisionesPresupuesto_List");
        //        return cCapComisionesPresupuesto;
        //    }
        //    catch (Exception ex)
        //    {
        //        return null;
        //    }
        //}

        public void CapComisionesPresupuesto_List(ComisionesPresupuesto presupuesto, string Conexion, ref List<ComisionesPresupuesto> list)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_Rik", "@Anio" };
                object[] Valores = { presupuesto.Id_Emp,
                                       presupuesto.Num_Cdi,
                                       presupuesto.Id_Rik,
                                       presupuesto.Anio };



                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapComisionesPresupuesto_List", ref dr, Parametros, Valores);


                while (dr.Read())
                {

                    presupuesto = new ComisionesPresupuesto();
                    presupuesto.Id_Presupuesto = (int)dr.GetValue(dr.GetOrdinal("Id_Presupuesto"));
                    presupuesto.Id_Emp = (int)dr.GetValue(dr.GetOrdinal("Id_Emp"));
                    presupuesto.Num_Cdi = (int)dr.GetValue(dr.GetOrdinal("Id_Cd"));
                    presupuesto.Id_Cd = (int)dr.GetValue(dr.GetOrdinal("Id_Cd"));
                    presupuesto.NomCdi = dr["NomCdi"].ToString();
                    presupuesto.Id_Rik = (int)dr.GetValue(dr.GetOrdinal("Id_Rik"));
                    presupuesto.Nom_Empleado = dr["Nom_Empleado"].ToString();
                    presupuesto.Anio = (int)dr.GetValue(dr.GetOrdinal("Anio"));
                    presupuesto.Base = dr["Base"] == System.DBNull.Value ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Base")));
                    presupuesto.BaseUP = dr["BaseUP"] == System.DBNull.Value ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("BaseUP")));
                    presupuesto.BaseUP_Porc = dr["BaseUP_Porc"] == System.DBNull.Value ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("BaseUP_Porc")));
                    presupuesto.Venta1 = dr["Venta1"] == System.DBNull.Value ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Venta1")));
                    presupuesto.UP1 = dr["UP1"] == System.DBNull.Value ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("UP1")));
                    presupuesto.UP_Porc1 = dr["UP_Porc1"] == System.DBNull.Value ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("UP_Porc1")));
                    presupuesto.Venta2 = dr["Venta2"] == System.DBNull.Value ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Venta2")));
                    presupuesto.UP2 = dr["UP2"] == System.DBNull.Value ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("UP2")));
                    presupuesto.UP_Porc2 = dr["UP_Porc2"] == System.DBNull.Value ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("UP_Porc2")));
                    presupuesto.Venta3 = dr["Venta3"] == System.DBNull.Value ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Venta3")));
                    presupuesto.UP3 = dr["UP3"] == System.DBNull.Value ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("UP3")));
                    presupuesto.UP_Porc3 = dr["UP_Porc3"] == System.DBNull.Value ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("UP_Porc3")));
                    presupuesto.Venta4 = dr["Venta4"] == System.DBNull.Value ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Venta4")));
                    presupuesto.UP4 = dr["UP4"] == System.DBNull.Value ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("UP4")));
                    presupuesto.UP_Porc4 = dr["UP_Porc4"] == System.DBNull.Value ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("UP_Porc4")));
                    presupuesto.Venta5 = dr["Venta5"] == System.DBNull.Value ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Venta5")));
                    presupuesto.UP5 = dr["UP5"] == System.DBNull.Value ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("UP5")));
                    presupuesto.UP_Porc5 = dr["UP_Porc5"] == System.DBNull.Value ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("UP_Porc5")));
                    presupuesto.Venta6 = dr["Venta6"] == System.DBNull.Value ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Venta6")));
                    presupuesto.UP6 = dr["UP6"] == System.DBNull.Value ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("UP6")));
                    presupuesto.UP_Porc6 = dr["UP_Porc6"] == System.DBNull.Value ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("UP_Porc6")));
                    presupuesto.Venta7 = dr["Venta7"] == System.DBNull.Value ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Venta7")));
                    presupuesto.UP7 = dr["UP7"] == System.DBNull.Value ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("UP7")));
                    presupuesto.UP_Porc7 = dr["UP_Porc7"] == System.DBNull.Value ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("UP_Porc7")));
                    presupuesto.Venta8 = dr["Venta8"] == System.DBNull.Value ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Venta8")));
                    presupuesto.UP8 = dr["UP8"] == System.DBNull.Value ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("UP8")));
                    presupuesto.UP_Porc8 = dr["UP_Porc8"] == System.DBNull.Value ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("UP_Porc8")));
                    presupuesto.Venta9 = dr["Venta9"] == System.DBNull.Value ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Venta9")));
                    presupuesto.UP9 = dr["UP9"] == System.DBNull.Value ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("UP9")));
                    presupuesto.UP_Porc9 = dr["UP_Porc9"] == System.DBNull.Value ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("UP_Porc9")));
                    presupuesto.Venta10 = dr["Venta10"] == System.DBNull.Value ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Venta10")));
                    presupuesto.UP10 = dr["UP10"] == System.DBNull.Value ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("UP10")));
                    presupuesto.UP_Porc10 = dr["UP_Porc10"] == System.DBNull.Value ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("UP_Porc10")));
                    presupuesto.Venta11 = dr["Venta11"] == System.DBNull.Value ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Venta11")));
                    presupuesto.UP11 = dr["UP11"] == System.DBNull.Value ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("UP11")));
                    presupuesto.UP_Porc11 = dr["UP_Porc11"] == System.DBNull.Value ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("UP_Porc11")));
                    presupuesto.Venta12 = dr["Venta12"] == System.DBNull.Value ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Venta12")));
                    presupuesto.UP12 = dr["UP12"] == System.DBNull.Value ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("UP12")));
                    presupuesto.UP_Porc12 = dr["UP_Porc12"] == System.DBNull.Value ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("UP_Porc12")));
                    presupuesto.FechaUltMod = dr["Fecha_Ult_Mod"] == System.DBNull.Value ? "" : DateTime.Parse(dr["Fecha_Ult_Mod"].ToString()).ToString("dd/MM/yyyy");
                    presupuesto.id_Usuario = (int)dr.GetValue(dr.GetOrdinal("Id_Usuario"));
                    presupuesto.Meta_Ppto = dr["Meta_Ppto"] == System.DBNull.Value ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Meta_Ppto")));

                    list.Add(presupuesto);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
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
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_Rik", "@Anio" };
                object[] Valores = { presupuesto.Id_Emp,
                                       presupuesto.Num_Cdi,
                                       presupuesto.Id_Rik,
                                       presupuesto.Anio };



                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapComisionesPresupuestoVertical_List", ref dr, Parametros, Valores);


                while (dr.Read())
                {

                    presupuesto = new ComisionesPresupuesto();
                    presupuesto.Id_Emp = (int)dr.GetValue(dr.GetOrdinal("Id_Emp"));
                    presupuesto.Num_Cdi = (int)dr.GetValue(dr.GetOrdinal("Id_Cd"));
                    presupuesto.Id_Cd = (int)dr.GetValue(dr.GetOrdinal("Id_Cd"));
                    presupuesto.NomCdi = dr["NomCdi"].ToString();
                    presupuesto.Id_Rik = (int)dr.GetValue(dr.GetOrdinal("Id_Rik"));
                    presupuesto.Nom_Empleado = dr["Nom_Empleado"].ToString();

                    presupuesto.Venta = dr["Venta"] == System.DBNull.Value ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Venta")));
                    presupuesto.UP = dr["UP"] == System.DBNull.Value ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("UP")));
                    presupuesto.UP_Porc = dr["UP_Porc"] == System.DBNull.Value ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("UP_Porc")));
                    presupuesto.Mes = (int)dr.GetValue(dr.GetOrdinal("Mes"));
                    presupuesto.MesLetra = dr["MesLetra"].ToString();

                    list.Add(presupuesto);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
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
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_Rik", "@Anio" };
                object[] Valores = { presupuesto.Id_Emp,
                                       presupuesto.Num_Cdi,
                                       presupuesto.Id_Rik,
                                       presupuesto.Anio };



                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapComisionesPresupuestoPorRik_List", ref dr, Parametros, Valores);


                while (dr.Read())
                {

                    presupuesto = new ComisionesPresupuesto();
                    presupuesto.Id_Emp = (int)dr.GetValue(dr.GetOrdinal("Id_Emp"));
                    presupuesto.Num_Cdi = (int)dr.GetValue(dr.GetOrdinal("Id_Cd"));
                    presupuesto.Id_Cd = (int)dr.GetValue(dr.GetOrdinal("Id_Cd"));
                    presupuesto.NomCdi = dr["NomCdi"].ToString();
                    presupuesto.Id_Rik = (int)dr.GetValue(dr.GetOrdinal("Id_Rik"));
                    presupuesto.Nom_Empleado = dr["Nom_Empleado"].ToString();

                    presupuesto.Venta = dr["Venta"] == System.DBNull.Value ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Venta")));
                    presupuesto.UP = dr["UP_Real"] == System.DBNull.Value ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("UP_Real")));
                    presupuesto.UP_Porc = dr["UP_Porc"] == System.DBNull.Value ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("UP_Porc")));
                    presupuesto.Mes = (int)dr.GetValue(dr.GetOrdinal("Mes"));
                    presupuesto.MesLetra = dr["MesLetra"].ToString();

                    presupuesto.BaseUP = dr["BaseUP"] == System.DBNull.Value ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("BaseUP")));
                    presupuesto.UP_Presupuesto = dr["UP_Presupuesto"] == System.DBNull.Value ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("UP_Presupuesto")));
                    presupuesto.Meta_Ppto = dr["Meta_Ppto"] == System.DBNull.Value ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Meta_Ppto")));
                    presupuesto.Incremento_Real = dr["Incremento_Real"] == System.DBNull.Value ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Incremento_Real")));
                    presupuesto.Porc_Cumplimiento = dr["Porc_Cumplimiento"] == System.DBNull.Value ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Porc_Cumplimiento")));
                    presupuesto.Multiplicador = dr["Multiplicador"] == System.DBNull.Value ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Multiplicador")));
                    presupuesto.UP_Porc = dr["UP_Porc"] == System.DBNull.Value ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("UP_Porc")));

                    list.Add(presupuesto);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
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
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Anio", "@Id_Rik" };
                object[] Valores = { presupuesto.Id_Emp,
                                       presupuesto.Num_Cdi,
                                       presupuesto.Anio,
                                       presupuesto.Id_Rik };



                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapComisionesPresupuesto_UPrima_Consultar", ref dr, Parametros, Valores);


                while (dr.Read())
                {

                    presupuesto = new ComisionesPresupuesto();
                    presupuesto.Num_Cdi = (int)dr.GetValue(dr.GetOrdinal("Id_Cd"));
                    presupuesto.Id_Cd = (int)dr.GetValue(dr.GetOrdinal("Id_Cd"));
                    presupuesto.Id_Rik = (int)dr.GetValue(dr.GetOrdinal("Id_Rik"));
                    presupuesto.Nom_Empleado = dr["Nom_Empleado"].ToString();
                    presupuesto.Anio = (int)dr.GetValue(dr.GetOrdinal("Anio"));
                    presupuesto.Base = dr["Base"] == System.DBNull.Value ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Base")));
                    presupuesto.BaseUP = dr["BaseUP"] == System.DBNull.Value ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("BaseUP")));
                    presupuesto.BaseUP_Porc = dr["BaseUP_Porc"] == System.DBNull.Value ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("BaseUP_Porc")));
                    presupuesto.Venta1 = dr["Venta1"] == System.DBNull.Value ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Venta1")));
                    presupuesto.UP1 = dr["Real1"] == System.DBNull.Value ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Real1")));
                    presupuesto.Venta2 = dr["Venta2"] == System.DBNull.Value ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Venta2")));
                    presupuesto.UP2 = dr["Real2"] == System.DBNull.Value ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Real2")));
                    presupuesto.Venta3 = dr["Venta3"] == System.DBNull.Value ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Venta3")));
                    presupuesto.UP3 = dr["Real3"] == System.DBNull.Value ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Real3")));
                    presupuesto.Venta4 = dr["Venta4"] == System.DBNull.Value ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Venta4")));
                    presupuesto.UP4 = dr["Real4"] == System.DBNull.Value ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Real4")));
                    presupuesto.Venta5 = dr["Venta5"] == System.DBNull.Value ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Venta5")));
                    presupuesto.UP5 = dr["Real5"] == System.DBNull.Value ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Real5")));
                    presupuesto.Venta6 = dr["Venta6"] == System.DBNull.Value ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Venta6")));
                    presupuesto.UP6 = dr["Real6"] == System.DBNull.Value ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Real6")));
                    presupuesto.Venta7 = dr["Venta7"] == System.DBNull.Value ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Venta7")));
                    presupuesto.UP7 = dr["Real7"] == System.DBNull.Value ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Real7")));
                    presupuesto.Venta8 = dr["Venta8"] == System.DBNull.Value ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Venta8")));
                    presupuesto.UP8 = dr["Real8"] == System.DBNull.Value ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Real8")));
                    presupuesto.Venta9 = dr["Venta9"] == System.DBNull.Value ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Venta9")));
                    presupuesto.UP9 = dr["Real9"] == System.DBNull.Value ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Real9")));
                    presupuesto.Venta10 = dr["Venta10"] == System.DBNull.Value ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Venta10")));
                    presupuesto.UP10 = dr["Real10"] == System.DBNull.Value ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Real10")));
                    presupuesto.Venta11 = dr["Venta11"] == System.DBNull.Value ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Venta11")));
                    presupuesto.UP11 = dr["Real11"] == System.DBNull.Value ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Real11")));
                    presupuesto.Venta12 = dr["Venta12"] == System.DBNull.Value ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Venta12")));
                    presupuesto.UP12 = dr["Real12"] == System.DBNull.Value ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Real12")));


                    list.Add(presupuesto);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        #endregion

    }


}