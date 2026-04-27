using CapaModelo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class CD_CatCRMInt_Integralidad
    {
        List<string> Parametros = new List<string>();
        List<object> Valores = new List<object>();

        // 5OCT-2021 
        public int Parametro(string Parametro, object Valor)
        {
            Parametros.Add(Parametro);
            Valores.Add(Valor);
            return 0;
        }

        //CatCRM_ListadoRIKS
        public List<CatCRM_ListadoRIKS> ListadoRiks(int id_emp, int id_cd, string Riks, string Ctes, string Segs, string Conexion)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = {
                                           "@Id_Emp",
                                           "@Id_Cd",
                                           "@riks",
                                           "@Id_Ctes",
                                           "@Id_Segs"

                                      };
                object[] Valores = {
                                     id_emp, id_cd, Riks, Ctes ,Segs
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCrmInt_IntegralidadMes_Por_RIK_Listado", ref dr, Parametros, Valores);

                List<CatCRM_ListadoRIKS> riks = new List<CatCRM_ListadoRIKS>();

                while (dr.Read())
                {
                    var rik = new CatCRM_ListadoRIKS();
                    //a.Id_Acs = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Acs")));

                    rik.Id_Cte = int.Parse(dr["Id_Cte"].ToString());
                    rik.Id_Rik = int.Parse(dr["Id_Rik"].ToString());
                    rik.Id_Seg = int.Parse(dr["Id_Seg"].ToString());
                    rik.Id_Ter = int.Parse(dr["Id_Ter"].ToString());

                    rik.Seg_Descripcion = dr["Seg_Descripcion"].ToString();
                    rik.UltFechafac = DateTime.Parse(dr["UltFechafac"].ToString());
                    rik.Venta = double.Parse(dr["Venta"].ToString());

                    rik.VPT = double.Parse(dr["VPT"].ToString());

                    rik.Rik_Nombre = dr["Rik_Nombre"].ToString();
                    rik.Cd_nombre = dr["Cd_nombre"].ToString();
                    rik.Cte_NomComercial = dr["Cte_NomComercial"].ToString();


                    rik.Seg_ValUniDim = double.Parse(dr["Seg_ValUniDim"].ToString());
                    rik.Seg_Unidades = dr["Seg_Unidades"].ToString();
                    rik.Cantidad = double.Parse(dr["Cantidad"].ToString());

                    rik.VPT = double.Parse(dr["VPT"].ToString());
                    rik.VPO = double.Parse(dr["VPO"].ToString());

                    rik.Integralidad = double.Parse(dr["Integralidad"].ToString());
                    rik.Integralidad_Obs = double.Parse(dr["Integralidad_Obs"].ToString());

                    rik.PromedioTrimestral = double.Parse(dr["PromedioTrimestral"].ToString());

                    riks.Add(rik);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

                return riks;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CatCRM_ListadoRIKS> ListadoRiks_Ver2(int id_emp, int id_cd, string Riks, string Ctes, string Segs, string Conexion)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = {
                    "@Debug",
                                           "@Id_Emp",
                                           "@Id_Cd",
                                           "@riks",
                                           "@Id_Ctes",
                                           "@Id_Segs"

                                      };
                object[] Valores = {
                                     0, id_emp, id_cd, Riks, Ctes ,Segs
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCrmInt_IntegralidadMes_Por_RIK_Listado_Ver2", ref dr, Parametros, Valores);

                List<CatCRM_ListadoRIKS> riks = new List<CatCRM_ListadoRIKS>();

                while (dr.Read())
                {
                    var rik = new CatCRM_ListadoRIKS();
                    //a.Id_Acs = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Acs")));

                    rik.Id_Cte = int.Parse(dr["Id_Cte"].ToString());
                    rik.Id_Rik = int.Parse(dr["Id_Rik"].ToString());
                    rik.Id_Seg = int.Parse(dr["Id_Seg"].ToString());
                    rik.Id_Ter = int.Parse(dr["Id_Ter"].ToString());

                    rik.Seg_Descripcion = dr["Seg_Descripcion"].ToString();
                    rik.UltFechafac = DateTime.Parse(dr["UltFechafac"].ToString());
                    rik.Venta = double.Parse(dr["Venta"].ToString());

                    rik.VPT = double.Parse(dr["VPT"].ToString());

                    rik.Rik_Nombre = dr["Rik_Nombre"].ToString();
                    rik.Cd_nombre = dr["Cd_nombre"].ToString();
                    rik.Cte_NomComercial = dr["Cte_NomComercial"].ToString();


                    rik.Seg_ValUniDim = double.Parse(dr["Seg_ValUniDim"].ToString());
                    rik.Seg_Unidades = dr["Seg_Unidades"].ToString();
                    rik.Cantidad = double.Parse(dr["Cantidad"].ToString());

                    rik.VPT = double.Parse(dr["VPT"].ToString());
                    rik.VPO = double.Parse(dr["VPO"].ToString());
                    rik.VPOMeta = double.Parse(dr["VPOMeta"].ToString());



                    rik.Integralidad = double.Parse(dr["Integralidad"].ToString());
                    rik.Integralidad_Obs = double.Parse(dr["Integralidad_Obs"].ToString());

                    rik.PromedioTrimestral = double.Parse(dr["PromedioTrimestral"].ToString());

                    riks.Add(rik);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

                return riks;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        //22SEP-2021 RFH
        public DataTable ListadoRiksToDt(int Id_Emp, int Id_Cd, string Riks, string Ctes, string Segs, string Conexion)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                /*
                string[] Parametros = {
                                           "@Id_Emp",
                                           "@Id_Cd",
                                           "@riks",
                                           "@Id_Ctes",
                                           "@Id_Segs"
                                      };
                object[] Valores = {
                                     id_emp, id_cd, Riks, Ctes ,Segs
                                   };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCrmInt_IntegralidadMes_Por_RIK_Listado", ref dr, Parametros, Valores);
                */

                SqlConnection cn = new SqlConnection(Conexion);

                cn.Open();
                SqlCommand sqlcmdA = new SqlCommand("spCrmInt_IntegralidadMes_Por_RIK_Listado", cn);
                sqlcmdA.CommandType = CommandType.StoredProcedure;

                sqlcmdA.Parameters.AddWithValue("@Id_Emp", Id_Emp);
                sqlcmdA.Parameters.AddWithValue("@Id_Cd", Id_Cd);
                sqlcmdA.Parameters.AddWithValue("@riks", Riks);
                sqlcmdA.Parameters.AddWithValue("@Id_Ctes", Ctes);
                sqlcmdA.Parameters.AddWithValue("@Id_Segs", Segs);

                SqlDataAdapter dtInt = new SqlDataAdapter(sqlcmdA);
                DataTable dtDetalleInt = new DataTable();
                dtInt.Fill(dtDetalleInt);
                cn.Close();

                // List<CatCRM_ListadoRIKS> riks = new List<CatCRM_ListadoRIKS>();

                /*
                while (dr.Read())
                {
                    var rik = new CatCRM_ListadoRIKS();
                    //a.Id_Acs = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Acs")));
                    rik.Id_Cte = int.Parse(dr["Id_Cte"].ToString());
                    rik.Id_Rik = int.Parse(dr["Id_Rik"].ToString());
                    rik.Id_Seg = int.Parse(dr["Id_Seg"].ToString());
                    rik.Id_Ter = int.Parse(dr["Id_Ter"].ToString());
                    rik.Seg_Descripcion = dr["Seg_Descripcion"].ToString();
                    rik.UltFechafac = DateTime.Parse(dr["UltFechafac"].ToString());
                    rik.Venta = double.Parse(dr["Venta"].ToString());
                    rik.VPT = double.Parse(dr["VPT"].ToString());
                    rik.Rik_Nombre = dr["Rik_Nombre"].ToString();
                    rik.Cd_nombre = dr["Cd_nombre"].ToString();
                    rik.Cte_NomComercial = dr["Cte_NomComercial"].ToString();
                    rik.Seg_ValUniDim = double.Parse(dr["Seg_ValUniDim"].ToString());
                    rik.Seg_Unidades = dr["Seg_Unidades"].ToString();
                    rik.Cantidad = double.Parse(dr["Cantidad"].ToString());
                    rik.VPT = double.Parse(dr["VPT"].ToString());
                    rik.VPO = double.Parse(dr["VPO"].ToString());
                    rik.Integralidad = double.Parse(dr["Integralidad"].ToString());
                    rik.Integralidad_Obs = double.Parse(dr["Integralidad_Obs"].ToString());
                    rik.PromedioTrimestral = 0;
                    riks.Add(rik);
                }
                */

                //CapaDatos.LimpiarSqlcommand(ref sqlcmd);

                return dtDetalleInt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<CatCRM_IntegralidadMes> ListadoAplicaciones(int Id_Cte, int Id_Rik, int Id_Ter, int Id_Seg, string Conexion)
        {
            try
            {
                SqlConnection cn = new SqlConnection(Conexion);

                List<CatCRM_IntegralidadMes> ListadoAniosMes = new List<CatCRM_IntegralidadMes>();

                cn.Open();
                SqlCommand sqlcmdA = new SqlCommand("spCrmInt_IntegralidadMes", cn);
                sqlcmdA.CommandType = CommandType.StoredProcedure;

                sqlcmdA.Parameters.AddWithValue("@Id_Cte", Id_Cte);
                sqlcmdA.Parameters.AddWithValue("@Id_Ter", Id_Ter);
                sqlcmdA.Parameters.AddWithValue("@Id_Usu", Id_Rik);

                SqlDataAdapter dtInt = new SqlDataAdapter(sqlcmdA);
                DataTable dtDetalleInt = new DataTable();
                dtInt.Fill(dtDetalleInt);
                cn.Close();


                foreach (DataRow rowInt in dtDetalleInt.Rows)
                {
                    CatCRM_IntegralidadMes AnioMes = new CatCRM_IntegralidadMes();

                    AnioMes.Anio = int.Parse(rowInt["anio"].ToString());
                    AnioMes.Mes = rowInt["mes"].ToString();
                    AnioMes.Venta = double.Parse(rowInt["Venta"].ToString());
                    AnioMes.MesNum = int.Parse(rowInt["mesnum"].ToString());

                    cn.Open();
                    SqlCommand sqlcmdDet = new SqlCommand("spCrmInt_MapaAplicaciones_Detalle", cn);


                    sqlcmdDet.CommandType = CommandType.StoredProcedure;

                    sqlcmdDet.Parameters.AddWithValue("@Id_Cte", Id_Cte);
                    sqlcmdDet.Parameters.AddWithValue("@Id_Ter", Id_Ter);
                    sqlcmdDet.Parameters.AddWithValue("@Id_Usu", Id_Rik);
                    sqlcmdDet.Parameters.AddWithValue("@Id_Seg", Id_Seg);
                    sqlcmdDet.Parameters.AddWithValue("@Cal_Anio", AnioMes.Anio);
                    sqlcmdDet.Parameters.AddWithValue("@Cal_Mes", AnioMes.MesNum);

                    SqlDataAdapter dtA = new SqlDataAdapter(sqlcmdDet);
                    DataTable dtDetalle = new DataTable();

                    dtA.Fill(dtDetalle);
                    cn.Close();

                    List<CatCRM_IntMapaApps> apps = new List<CatCRM_IntMapaApps>();
                    DataView view = new DataView(dtDetalle);
                    DataTable dtDetalleAreas = view.ToTable(true, "Id_Seg", "Seg_Descripcion", "Id_Area", "Area_Descripcion", "Id_Sol", "Sol_Descripcion");

                    foreach (DataRow rowArea in dtDetalleAreas.Rows)
                    {
                        var ap = new CatCRM_IntMapaApps();
                        //a.Id_Acs = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Acs")));

                        ap.Id_Seg = int.Parse(rowArea["Id_Seg"].ToString());
                        ap.Seg_Descripcion = rowArea["Seg_Descripcion"].ToString();

                        ap.Id_Area = int.Parse(rowArea["Id_Area"].ToString());
                        ap.Area_Descripcion = rowArea["Area_Descripcion"].ToString();

                        ap.Id_Sol = int.Parse(rowArea["Id_Sol"].ToString());
                        ap.Sol_Descripcion = rowArea["Sol_Descripcion"].ToString();

                        var dtDetalleFiltrado = dtDetalle.Select("Id_Sol=" + ap.Id_Sol.ToString());


                        List<CatCRM_IntAplicaciones> listaApp = new List<CatCRM_IntAplicaciones>();

                        foreach (DataRow row in dtDetalleFiltrado)
                        {
                            CatCRM_IntAplicaciones apDet = new CatCRM_IntAplicaciones();

                            apDet.Id_Apl = int.Parse(row["Id_Apl"].ToString());
                            apDet.Apl_Descripcion = row["Apl_Descripcion"].ToString();
                            apDet.Venta = Double.Parse(row["Venta"].ToString());

                            apDet.Apl_Potencial = Double.Parse(row["Apl_Potencial"].ToString());

                            apDet.Id_Op = int.Parse(row["Id_Op"].ToString());
                            apDet.VPO = Double.Parse(row["VPO"].ToString());
                            apDet.VPT = Double.Parse(row["VPT"].ToString());

                            apDet.GAP_Teorico = Double.Parse(row["VPT"].ToString()) - Double.Parse(row["Venta"].ToString());

                            apDet.GAP_Observado = Double.Parse(row["VPO"].ToString()) - Double.Parse(row["Venta"].ToString());

                            if (apDet.GAP_Teorico.ToString() == null)
                            {
                                apDet.GAP_Teorico = 0;
                            }

                            if (apDet.GAP_Observado.ToString() == null)
                            {
                                apDet.GAP_Observado = 0;
                            }

                            listaApp.Add(apDet);

                        }

                        //CapaDatosDet.LimpiarSqlcommand(ref sqlcmdDet);
                        ap.Aplicaciones = listaApp;
                        apps.Add(ap);
                    }

                    //CapaDatos.LimpiarSqlcommand(ref sqlcmd);

                    AnioMes.Segmentos = apps;
                    ListadoAniosMes.Add(AnioMes);

                }


                //CapaDatosA.LimpiarSqlcommand(ref sqlcmdA);
                return ListadoAniosMes;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public List<CatCRM_IntegralidadMes> ListadoAplicaciones_Ver2(int Id_Cte, int Id_Rik, int Id_Ter, int Id_Seg, string Conexion)
        {
            try
            {
                SqlConnection cn = new SqlConnection(Conexion);
                List<CatCRM_IntegralidadMes> ListadoAniosMes = new List<CatCRM_IntegralidadMes>();
                cn.Open();
                SqlCommand sqlcmdA = new SqlCommand("spCrmInt_IntegralidadMes_Ver2", cn);
                sqlcmdA.CommandType = CommandType.StoredProcedure;

                sqlcmdA.Parameters.AddWithValue("@Id_Cte", Id_Cte);
                sqlcmdA.Parameters.AddWithValue("@Id_Ter", Id_Ter);
                sqlcmdA.Parameters.AddWithValue("@Id_Usu", Id_Rik);

                SqlDataAdapter dtInt = new SqlDataAdapter(sqlcmdA);
                DataTable dtDetalleInt = new DataTable();
                dtInt.Fill(dtDetalleInt);
                cn.Close();

                foreach (DataRow rowInt in dtDetalleInt.Rows)
                {
                    CatCRM_IntegralidadMes AnioMes = new CatCRM_IntegralidadMes();
                    AnioMes.Anio = int.Parse(rowInt["anio"].ToString());
                    AnioMes.Mes = rowInt["mes"].ToString();
                    AnioMes.Venta = double.Parse(rowInt["Venta"].ToString());
                    AnioMes.MesNum = int.Parse(rowInt["mesnum"].ToString());
                    cn.Open();
                    SqlCommand sqlcmdDet = new SqlCommand("spCrmInt_MapaAplicaciones_Detalle_Ver2", cn);
                    sqlcmdDet.CommandType = CommandType.StoredProcedure;
                    sqlcmdDet.Parameters.AddWithValue("@Id_Cte", Id_Cte);
                    sqlcmdDet.Parameters.AddWithValue("@Id_Ter", Id_Ter);
                    sqlcmdDet.Parameters.AddWithValue("@Id_Usu", Id_Rik);
                    sqlcmdDet.Parameters.AddWithValue("@Id_Seg", Id_Seg);
                    sqlcmdDet.Parameters.AddWithValue("@Cal_Anio", AnioMes.Anio);
                    sqlcmdDet.Parameters.AddWithValue("@Cal_Mes", AnioMes.MesNum);

                    SqlDataAdapter dtA = new SqlDataAdapter(sqlcmdDet);
                    DataTable dtDetalle = new DataTable();

                    dtA.Fill(dtDetalle);
                    cn.Close();

                    List<CatCRM_IntMapaApps> apps = new List<CatCRM_IntMapaApps>();
                    DataView view = new DataView(dtDetalle);
                    DataTable dtDetalleAreas = view.ToTable(true, "Id_Seg", "Seg_Descripcion", "Id_Area", "Area_Descripcion", "Id_Sol", "Sol_Descripcion");

                    foreach (DataRow rowArea in dtDetalleAreas.Rows)
                    {
                        var ap = new CatCRM_IntMapaApps();
                        //a.Id_Acs = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Acs")));
                        ap.Id_Seg = int.Parse(rowArea["Id_Seg"].ToString());
                        ap.Seg_Descripcion = rowArea["Seg_Descripcion"].ToString();
                        ap.Id_Area = int.Parse(rowArea["Id_Area"].ToString());
                        ap.Area_Descripcion = rowArea["Area_Descripcion"].ToString();
                        ap.Id_Sol = int.Parse(rowArea["Id_Sol"].ToString());
                        ap.Sol_Descripcion = rowArea["Sol_Descripcion"].ToString();

                        var dtDetalleFiltrado = dtDetalle.Select("Id_Sol=" + ap.Id_Sol.ToString());

                        List<CatCRM_IntAplicaciones> listaApp = new List<CatCRM_IntAplicaciones>();

                        foreach (DataRow row in dtDetalleFiltrado)
                        {
                            CatCRM_IntAplicaciones apDet = new CatCRM_IntAplicaciones();
                            apDet.Id_Apl = int.Parse(row["Id_Apl"].ToString());
                            apDet.Apl_Descripcion = row["Apl_Descripcion"].ToString();
                            apDet.Venta = Double.Parse(row["Venta"].ToString());
                            apDet.Apl_Potencial = Double.Parse(row["Apl_Potencial"].ToString());
                            apDet.Id_Op = int.Parse(row["Id_Op"].ToString());
                            apDet.VPO = Double.Parse(row["VPO"].ToString());
                            apDet.VPOMeta = Double.Parse(row["VPOMeta"].ToString());
                            apDet.VPT = Double.Parse(row["VPT"].ToString());
                            // 22Nov2021 RFH                            
                            apDet.GAP_Teorico = Double.Parse(row["Venta"].ToString()) - Double.Parse(row["VPT"].ToString());
                            apDet.GAP_Observado = Double.Parse(row["Venta"].ToString()) - Double.Parse(row["VPO"].ToString());

                            if (apDet.GAP_Teorico.ToString() == null)
                            {
                                apDet.GAP_Teorico = 0;
                            }

                            if (apDet.GAP_Observado.ToString() == null)
                            {
                                apDet.GAP_Observado = 0;
                            }
                            listaApp.Add(apDet);
                        }
                        //CapaDatosDet.LimpiarSqlcommand(ref sqlcmdDet);
                        ap.Aplicaciones = listaApp;
                        apps.Add(ap);
                    }
                    //CapaDatos.LimpiarSqlcommand(ref sqlcmd);
                    AnioMes.Segmentos = apps;
                    ListadoAniosMes.Add(AnioMes);
                }
                //CapaDatosA.LimpiarSqlcommand(ref sqlcmdA);
                return ListadoAniosMes;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public List<CatCRM_IntegralidadMes> CDActualizaVPOMeta(int Id_Cte, int Id_Ter, string Conexion, float VPOMeta)
        {
            try
            {
                SqlConnection cn = new SqlConnection(Conexion);
                List<CatCRM_IntegralidadMes> ListadoAniosMes = new List<CatCRM_IntegralidadMes>();
                cn.Open();
                SqlCommand sqlcmdA = new SqlCommand("spCrmInt_IntegralidadMes_ActualizaVPOMeta", cn);
                sqlcmdA.CommandType = CommandType.StoredProcedure;

                sqlcmdA.Parameters.AddWithValue("@Id_Cte", Id_Cte);
                sqlcmdA.Parameters.AddWithValue("@Id_Ter", Id_Ter);
                sqlcmdA.Parameters.AddWithValue("@VPOMeta", VPOMeta);

                SqlDataAdapter dtInt = new SqlDataAdapter(sqlcmdA);
                DataTable dtDetalleInt = new DataTable();
                dtInt.Fill(dtDetalleInt);
                cn.Close();

                
                //CapaDatosA.LimpiarSqlcommand(ref sqlcmdA);
                return ListadoAniosMes;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        //5OCT-2021 RFH
        public List<eCrmInt_MapaAplicaciones_Detalle> spCrmInt_MapaAplicaciones_Detalle_Ver2(
            int Id_Cte, int Id_Ter, int Id_Rik, int Id_Seg, int Cal_Anio, int Cal_Mes, string Conexion)
        {
            List<eCrmInt_MapaAplicaciones_Detalle> Lst = new List<eCrmInt_MapaAplicaciones_Detalle>();
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                Parametro("@Id_Cte", Id_Cte);
                Parametro("@Id_Ter", Id_Ter);
                Parametro("@Id_Usu", Id_Rik);
                Parametro("@Id_Seg", Id_Seg);
                Parametro("@Cal_Anio", Cal_Anio);
                Parametro("@Cal_Mes", Cal_Mes);

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCrmInt_MapaAplicaciones_Detalle_Ver2", ref dr, Parametros.ToArray(), Valores.ToArray());
                while (dr.Read())
                {
                    eCrmInt_MapaAplicaciones_Detalle obj = new eCrmInt_MapaAplicaciones_Detalle();

                    obj.Id_Op = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Op")));
                    obj.Id_Cte = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cte")));
                    obj.Id_Ter = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Ter")));
                    obj.Id_Usu = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Usu")));
                    obj.Estatus = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Estatus")));
                    obj.Id_Seg = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Seg")));
                    obj.Seg_Descripcion = dr.GetValue(dr.GetOrdinal("Seg_Descripcion")).ToString();
                    obj.Id_Area = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Area")));
                    obj.Area_Descripcion = dr.GetValue(dr.GetOrdinal("Area_Descripcion")).ToString();
                    obj.Id_Sol = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Sol")));
                    obj.Sol_Descripcion = dr.GetValue(dr.GetOrdinal("Sol_Descripcion")).ToString();
                    obj.Id_Apl = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Apl")));
                    obj.Apl_Descripcion = dr.GetValue(dr.GetOrdinal("Apl_Descripcion")).ToString();
                    obj.Apl_Potencial = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Apl_Potencial")));
                    obj.VPO = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("VPO")));
                    obj.SegVal_UniDim = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Seg_ValUniDim")));
                    obj.VPT_Total = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("VPT_Total")));
                    obj.VPT = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("VPT")));
                    obj.Venta_Real = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Venta")));
                    obj.GAP = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("GAP")));
                    obj.Anio = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Anio")));
                    obj.Mes = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Mes")));
                    obj.GAP_Teorico = obj.VPT - obj.Venta_Real;
                    obj.GAP_Observado = obj.VPO - obj.Venta_Real;
                    obj.VPOMeta = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("VPOMeta")));


                    if (obj.GAP_Teorico.ToString() == null)
                    {
                        obj.GAP_Teorico = 0;
                    }

                    if (obj.GAP_Observado.ToString() == null)
                    {
                        obj.GAP_Observado = 0;
                    }

                    Lst.Add(obj);
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                Lst = null;
            }
            return Lst;

            /*
            try
            {
                SqlConnection cn = new SqlConnection(Conexion);
                List<CatCRM_IntegralidadMes> ListadoAniosMes = new List<CatCRM_IntegralidadMes>();
                cn.Open();
                SqlCommand sqlcmdA = new SqlCommand("spCrmInt_IntegralidadMes_Ver2", cn);
                sqlcmdA.CommandType = CommandType.StoredProcedure;
                sqlcmdA.Parameters.AddWithValue("@Id_Cte", Id_Cte);
                sqlcmdA.Parameters.AddWithValue("@Id_Ter", Id_Ter);
                sqlcmdA.Parameters.AddWithValue("@Id_Usu", Id_Rik);
                SqlDataAdapter dtInt = new SqlDataAdapter(sqlcmdA);
                DataTable dtDetalleInt = new DataTable();
                dtInt.Fill(dtDetalleInt);
                cn.Close();
                foreach (DataRow rowInt in dtDetalleInt.Rows)
                {
                    CatCRM_IntegralidadMes AnioMes = new CatCRM_IntegralidadMes();
                    AnioMes.Anio = int.Parse(rowInt["anio"].ToString());
                    AnioMes.Mes = rowInt["mes"].ToString();
                    AnioMes.Venta = double.Parse(rowInt["Venta"].ToString());
                    AnioMes.MesNum = int.Parse(rowInt["mesnum"].ToString());
                    cn.Open();
                    SqlCommand sqlcmdDet = new SqlCommand("spCrmInt_MapaAplicaciones_Detalle", cn);
                    sqlcmdDet.CommandType = CommandType.StoredProcedure;
                    sqlcmdDet.Parameters.AddWithValue("@Id_Cte", Id_Cte);
                    sqlcmdDet.Parameters.AddWithValue("@Id_Ter", Id_Ter);
                    sqlcmdDet.Parameters.AddWithValue("@Id_Usu", Id_Rik);
                    sqlcmdDet.Parameters.AddWithValue("@Id_Seg", Id_Seg);
                    sqlcmdDet.Parameters.AddWithValue("@Cal_Anio", AnioMes.Anio);
                    sqlcmdDet.Parameters.AddWithValue("@Cal_Mes", AnioMes.MesNum);
                    SqlDataAdapter dtA = new SqlDataAdapter(sqlcmdDet);
                    DataTable dtDetalle = new DataTable();
                    dtA.Fill(dtDetalle);
                    cn.Close();
                    List<CatCRM_IntMapaApps> apps = new List<CatCRM_IntMapaApps>();
                    DataView view = new DataView(dtDetalle);
                    DataTable dtDetalleAreas = view.ToTable(true, "Id_Seg", "Seg_Descripcion", "Id_Area", "Area_Descripcion", "Id_Sol", "Sol_Descripcion");
                    foreach (DataRow rowArea in dtDetalleAreas.Rows)
                    {
                        var ap = new CatCRM_IntMapaApps();
                        //a.Id_Acs = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Acs")));
                        ap.Id_Seg = int.Parse(rowArea["Id_Seg"].ToString());
                        ap.Seg_Descripcion = rowArea["Seg_Descripcion"].ToString();
                        ap.Id_Area = int.Parse(rowArea["Id_Area"].ToString());
                        ap.Area_Descripcion = rowArea["Area_Descripcion"].ToString();
                        ap.Id_Sol = int.Parse(rowArea["Id_Sol"].ToString());
                        ap.Sol_Descripcion = rowArea["Sol_Descripcion"].ToString();
                        var dtDetalleFiltrado = dtDetalle.Select("Id_Sol=" + ap.Id_Sol.ToString());
                        List<CatCRM_IntAplicaciones> listaApp = new List<CatCRM_IntAplicaciones>();
                        foreach (DataRow row in dtDetalleFiltrado)
                        {
                            CatCRM_IntAplicaciones apDet = new CatCRM_IntAplicaciones();
                            apDet.Id_Apl = int.Parse(row["Id_Apl"].ToString());
                            apDet.Apl_Descripcion = row["Apl_Descripcion"].ToString();
                            apDet.Venta = Double.Parse(row["Venta"].ToString());
                            apDet.Apl_Potencial = Double.Parse(row["Apl_Potencial"].ToString());
                            apDet.Id_Op = int.Parse(row["Id_Op"].ToString());
                            apDet.VPO = Double.Parse(row["VPO"].ToString());
                            apDet.VPT = Double.Parse(row["VPT"].ToString());
                            apDet.GAP_Teorico = Double.Parse(row["VPT"].ToString()) - Double.Parse(row["Venta"].ToString());
                            apDet.GAP_Observado = Double.Parse(row["VPO"].ToString()) - Double.Parse(row["Venta"].ToString());
                            listaApp.Add(apDet);
                        }
                        //CapaDatosDet.LimpiarSqlcommand(ref sqlcmdDet);
                        ap.Aplicaciones = listaApp;
                        apps.Add(ap);
                    }
                    //CapaDatos.LimpiarSqlcommand(ref sqlcmd);
                    AnioMes.Segmentos = apps;
                    ListadoAniosMes.Add(AnioMes);
                }
                //CapaDatosA.LimpiarSqlcommand(ref sqlcmdA);
                return ListadoAniosMes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            */

        }

        // 21OCt-2021 RFH 
        public List<CatCRM_ReporteIntegralidadGral> spCrmIntegralidad_PorRik(int Id_Emp, int Id_Cd, string riksSeleccionados, int param1, string Conexion)
        {
            try
            {
                SqlConnection cn = new SqlConnection(Conexion);
                List<CatCRM_IntegralidadMes> ListadoAniosMes = new List<CatCRM_IntegralidadMes>();

                cn.Open();
                SqlCommand sqlcmdA = new SqlCommand("spCrmIntegralidad_PorRik", cn);
                sqlcmdA.CommandType = CommandType.StoredProcedure;

                sqlcmdA.Parameters.AddWithValue("@Id_Emp", Id_Emp);
                sqlcmdA.Parameters.AddWithValue("@Id_Cd", Id_Cd);
                sqlcmdA.Parameters.AddWithValue("@riks", riksSeleccionados);

                SqlDataAdapter dtInt = new SqlDataAdapter(sqlcmdA);
                DataTable dtDetalleInt = new DataTable();
                dtInt.Fill(dtDetalleInt);
                cn.Close();

                List<CatCRM_ReporteIntegralidadGral> riksLista = new List<CatCRM_ReporteIntegralidadGral>();
                foreach (DataRow rowInt in dtDetalleInt.Rows)
                {
                    var obj = new CatCRM_ReporteIntegralidadGral();
                    obj.Id_Usu = int.Parse(rowInt["Id_Usu"].ToString());
                    obj.Rik_Nombre = rowInt["Rik_Nombre"].ToString();
                    obj.Integralidad = double.Parse(rowInt["Integralidad"].ToString());
                    obj.Integralidad_Obs = double.Parse(rowInt["Integralidad_Obs"].ToString());
                    riksLista.Add(obj);
                }

                return riksLista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CatCRM_ReporteIntegralidadGral> ListadoIntegralidadRIK(int Id_Emp, int Id_Cd, string riksSeleccionados, string Conexion)
        {
            try
            {
                SqlConnection cn = new SqlConnection(Conexion);
                List<CatCRM_IntegralidadMes> ListadoAniosMes = new List<CatCRM_IntegralidadMes>();

                cn.Open();
                SqlCommand sqlcmdA = new SqlCommand("spCrmInt_IntegralidadMes_Por_RIK", cn);
                sqlcmdA.CommandType = CommandType.StoredProcedure;

                sqlcmdA.Parameters.AddWithValue("@Id_Emp", Id_Emp);
                sqlcmdA.Parameters.AddWithValue("@Id_Cd", Id_Cd);
                sqlcmdA.Parameters.AddWithValue("@riks", riksSeleccionados);

                SqlDataAdapter dtInt = new SqlDataAdapter(sqlcmdA);
                DataTable dtDetalleInt = new DataTable();
                dtInt.Fill(dtDetalleInt);
                cn.Close();

                List<CatCRM_ReporteIntegralidadGral> riksLista = new List<CatCRM_ReporteIntegralidadGral>();
                foreach (DataRow rowInt in dtDetalleInt.Rows)
                {
                    var rikItem = new CatCRM_ReporteIntegralidadGral();

                    rikItem.Id_Cte = int.Parse(rowInt["Id_Cte"].ToString());
                    rikItem.Id_Ter = int.Parse(rowInt["Id_Ter"].ToString());
                    rikItem.Id_Usu = int.Parse(rowInt["Id_Usu"].ToString());
                    rikItem.Id_Seg = int.Parse(rowInt["Id_Seg"].ToString());

                    rikItem.VPT = double.Parse(rowInt["VPT"].ToString());
                    rikItem.VPO = double.Parse(rowInt["VPO"].ToString());
                    rikItem.Venta_Real = double.Parse(rowInt["Venta_Real"].ToString());
                    rikItem.GAP = double.Parse(rowInt["GAP"].ToString());
                    rikItem.Integralidad = double.Parse(rowInt["Integralidad"].ToString());
                    rikItem.Integralidad_Obs = double.Parse(rowInt["Integralidad_Obs"].ToString());

                    rikItem.Rik_Nombre = rowInt["Rik_Nombre"].ToString();

                    riksLista.Add(rikItem);
                }

                return riksLista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable ListadoIntegralidadRIK_Excel(int Id_Emp, int Id_Cd, string Conexion)
        {
            try
            {

                SqlConnection cn = new SqlConnection(Conexion);

                List<CatCRM_IntegralidadMes> ListadoAniosMes = new List<CatCRM_IntegralidadMes>();

                cn.Open();
                SqlCommand sqlcmdA = new SqlCommand("spCrmInt_IntegralidadMes_Por_RIK_Todos", cn);
                sqlcmdA.CommandType = CommandType.StoredProcedure;

                sqlcmdA.Parameters.AddWithValue("@Id_Emp", Id_Emp);
                sqlcmdA.Parameters.AddWithValue("@Id_Cd", Id_Cd);


                SqlDataAdapter dtInt = new SqlDataAdapter(sqlcmdA);
                DataTable dtDetalleInt = new DataTable();
                dtInt.Fill(dtDetalleInt);
                cn.Close();


                //List<CatCRM_ReporteIntegralidadGral_sExcel> riksListaExcel = new List<CatCRM_ReporteIntegralidadGral_sExcel>();
                //foreach (DataRow rowInt in dtDetalleInt.Rows)
                //{
                //    var rikItem = new CatCRM_ReporteIntegralidadGral_sExcel();

                //    rikItem.Id_Cte = int.Parse(rowInt["Id_Cte"].ToString());
                //    rikItem.Id_Ter = int.Parse(rowInt["Id_Ter"].ToString());
                //    rikItem.Id_Seg = int.Parse(rowInt["Id_Seg"].ToString());
                //    rikItem.Seg_Descripcion = rowInt["Seg_Descripcion"].ToString();

                //    rikItem.Id_Area = int.Parse(rowInt["Id_Area"].ToString());
                //    rikItem.Area_Descripcion = rowInt["Area_Descripcion"].ToString();

                //    rikItem.Id_Sol = int.Parse(rowInt["Id_Sol"].ToString());
                //    rikItem.Sol_Descripcion = rowInt["Sol_Descripcion"].ToString();

                //    rikItem.Id_Apl = int.Parse(rowInt["Id_Apl"].ToString());
                //    rikItem.Apl_Descripcion = rowInt["Apl_Descripcion"].ToString();

                //    rikItem.Apl_Potencial = int.Parse(rowInt["Apl_Potencial"].ToString());
                //    rikItem.VPO = double.Parse(rowInt["VPO"].ToString());
                //    rikItem.SegVal_UniDim = int.Parse(rowInt["SegVal_UniDim"].ToString());

                //    rikItem.VPT_Total= double.Parse(rowInt["VPT_Total"].ToString());
                //    rikItem.VPT = double.Parse(rowInt["VPT"].ToString());
                //    rikItem.Venta_Real = double.Parse(rowInt["Venta"].ToString());
                //    rikItem.GAP = double.Parse(rowInt["GAP"].ToString());
                //    rikItem.Anio = int.Parse(rowInt["Anio"].ToString());
                //    rikItem.Mes = int.Parse(rowInt["Mes"].ToString());
                //    rikItem.Rik_Nombre = rowInt["Rik_Nombre"].ToString();

                //    riksListaExcel.Add(rikItem);

                //}


                return dtDetalleInt;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }



    }
}