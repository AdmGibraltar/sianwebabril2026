using CapaModelo;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidad;

namespace CapaDatos
{
    public class CD_IntegralidadV2
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
        public List<eIntegralidadv2> spListCdiByZona(eIntegralidadv2 oeIntegralidadv2Par, string Conexion)
        {
            try
            {
                SqlConnection cn = new SqlConnection(Conexion);

                List<eIntegralidadv2> ListadoAniosMes = new List<eIntegralidadv2>();

                cn.Open();
                SqlCommand sqlcmdA = new SqlCommand("spListCdiByZona", cn);
                sqlcmdA.CommandType = CommandType.StoredProcedure;

                sqlcmdA.Parameters.AddWithValue("@Tipo", oeIntegralidadv2Par.Tipo);
                sqlcmdA.Parameters.AddWithValue("@IdZona", oeIntegralidadv2Par.Id_Zona);
                sqlcmdA.Parameters.AddWithValue("@Id_Cd", oeIntegralidadv2Par.Id_Cd);

                SqlDataAdapter dtInt = new SqlDataAdapter(sqlcmdA);
                DataTable dtDetalleInt = new DataTable();
                dtInt.Fill(dtDetalleInt);
                cn.Close();


                foreach (DataRow rowInt in dtDetalleInt.Rows)
                {
                    eIntegralidadv2 AnioMes = new eIntegralidadv2();

                    AnioMes.Id_Cd = int.Parse(rowInt["Id_Cd"].ToString());
                    AnioMes.Id_Zona = int.Parse(rowInt["Zona"].ToString());
                    AnioMes.Sucursal = rowInt["Sucursal"].ToString();
                    AnioMes.Zona = rowInt["Desc_Region"].ToString();

                    ListadoAniosMes.Add(AnioMes);

                }

                return ListadoAniosMes;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public List<eIntegralidadv2> sp_MapaAplicaciones_DetalleXMes(eIntegralidadv2 oeIntegralidadv2Par, string Conexion)
        {
            try
            {
                SqlConnection cn = new SqlConnection(Conexion);

                List<eIntegralidadv2> ListadoAniosMes = new List<eIntegralidadv2>();

                cn.Open();
                SqlCommand sqlcmdA = new SqlCommand("spListIntegralidadBy2", cn);
                sqlcmdA.CommandType = CommandType.StoredProcedure;


                sqlcmdA.Parameters.AddWithValue("@Tipo", oeIntegralidadv2Par.Tipo);
                sqlcmdA.Parameters.AddWithValue("@Id_Cd", oeIntegralidadv2Par.Id_Cd);
                sqlcmdA.Parameters.AddWithValue("@Anio", oeIntegralidadv2Par.AnioIni);
                sqlcmdA.Parameters.AddWithValue("@Mes", oeIntegralidadv2Par.MesIni);
                sqlcmdA.Parameters.AddWithValue("@Id_Usu", oeIntegralidadv2Par.Id_Rik);
                sqlcmdA.Parameters.AddWithValue("@Id_Uen", oeIntegralidadv2Par.Id_Uen);
                sqlcmdA.Parameters.AddWithValue("@Id_Seg", oeIntegralidadv2Par.Id_Seg);

                sqlcmdA.Parameters.AddWithValue("@Id_Cte", oeIntegralidadv2Par.Id_Cte);
                //sqlcmdA.Parameters.AddWithValue("@Id_Rik", oeIntegralidadv2Par.Id_Rik);
                //sqlcmdA.Parameters.AddWithValue("@Id_Seg", oeIntegralidadv2Par.Id_Seg);
                //sqlcmdA.Parameters.AddWithValue("@MesIni", oeIntegralidadv2Par.MesIni);
                //sqlcmdA.Parameters.AddWithValue("@AnioIni", oeIntegralidadv2Par.AnioIni);
                //sqlcmdA.Parameters.AddWithValue("@MesFin", oeIntegralidadv2Par.MesFin);
                //sqlcmdA.Parameters.AddWithValue("@AnioFin", oeIntegralidadv2Par.AnioFin);

                SqlDataAdapter dtInt = new SqlDataAdapter(sqlcmdA);
                DataTable dtDetalleInt = new DataTable();
                dtInt.Fill(dtDetalleInt);
                cn.Close();


                foreach (DataRow rowInt in dtDetalleInt.Rows)
                {
                    eIntegralidadv2 AnioMes = new eIntegralidadv2();

                    AnioMes.Id_Cte = int.Parse(rowInt["Id_Cte"].ToString());
                    AnioMes.Id_Op = int.Parse(rowInt["Id_Op"].ToString());

                    AnioMes.Id_Seg = int.Parse(rowInt["Id_Seg"].ToString());
                    AnioMes.Id_Apl = int.Parse(rowInt["Id_Apl"].ToString());

                    try
                    {
                        AnioMes.Id_Rik = int.Parse(rowInt["Id_Usu"].ToString());
                    }
                    catch (Exception ex) { }
                    try
                    {
                        AnioMes.Id_Area = int.Parse(rowInt["Id_Area"].ToString());
                    }
                    catch (Exception ex) { }
                    try
                    {
                        AnioMes.Id_Sol = int.Parse(rowInt["Id_Sol"].ToString());
                    }
                    catch (Exception ex) { }
                    try
                    {
                        AnioMes.Id_Ter = int.Parse(rowInt["Id_Ter"].ToString());
                    }
                    catch (Exception ex) { }
                    try
                    {
                        AnioMes.Id_Ter = int.Parse(rowInt["Id_Ter"].ToString());
                    }
                    catch (Exception ex) { }
                    try
                    {
                        AnioMes.Apl_Descripcion = rowInt["Apl_Descripcion"].ToString();
                    }
                    catch (Exception ex) { }


                    try
                    {
                        AnioMes.Sol_Descripcion = rowInt["Sol_Descripcion"].ToString();
                    }
                    catch (Exception ex) { }
                    try
                    {
                        AnioMes.Area_Desc = rowInt["Area_Desc"].ToString();
                    }
                    catch (Exception ex) { }
                    try
                    {
                        AnioMes.Cliente = rowInt["Cliente"].ToString();
                    }
                    catch (Exception ex) { }

                    try
                    {
                        AnioMes.Anio = int.Parse(rowInt["Anio"].ToString());
                    }
                    catch (Exception ex) { }
                    try
                    {
                        AnioMes.Mes = int.Parse(rowInt["Mes"].ToString());
                    }
                    catch (Exception ex) { }
                    AnioMes.VPO = double.Parse(rowInt["VPO"].ToString());
                    AnioMes.VPT = double.Parse(rowInt["VPT"].ToString());
                    AnioMes.Venta = double.Parse(rowInt["Venta"].ToString());
                    AnioMes.GAP = double.Parse(rowInt["GAP"].ToString());
                    AnioMes.VPOMeta = double.Parse(rowInt["VpoMeta"].ToString());
                    AnioMes.Rik = rowInt["Rik"].ToString();
                    AnioMes.Sucursal = rowInt["Sucursal"].ToString();
                    try
                    {
                        AnioMes.Uen_Desc = rowInt["Uen_Desc"].ToString();
                    }
                    catch (Exception ex) { }
                    try
                    {
                        AnioMes.Seg_Descripcion = rowInt["Seg_Descripcion"].ToString();
                    }
                    catch (Exception ex) { }
                    try
                    {
                        AnioMes.Bracket = rowInt["Bracket"].ToString();
                    }
                    catch (Exception ex) { }
                    try
                    {
                        AnioMes.TipoProducto = rowInt["TipoProducto"].ToString();
                    }
                    catch (Exception ex) { }
                    try
                    {
                        AnioMes.Porc_CoberturaVPO = double.Parse(rowInt["Porc_CoberturaVPO"].ToString());
                    }
                    catch (Exception ex) { }
                    try
                    {
                        AnioMes.Porc_CoberturaVPT = double.Parse(rowInt["Porc_CoberturaVPT"].ToString());
                    }
                    catch (Exception ex) { }
                    try
                    {
                        AnioMes.PorcentajeAplicacion = double.Parse(rowInt["PorcentajeAplicacion"].ToString());
                    }
                    catch (Exception ex) { }

                    try
                    {
                        AnioMes.PorcentajeAplicacionPotencial = double.Parse(rowInt["PorcentajeAplicacionPotencial"].ToString());
                    }
                    catch (Exception ex) { }

                    try
                    {
                        AnioMes.Id_Uen = int.Parse(rowInt["Id_Uen"].ToString());
                    }
                    catch (Exception ex) { }
                    try
                    {
                        AnioMes.Seg_Unidades = rowInt["Seg_Unidades"].ToString();
                    }
                    catch (Exception ex) { }
                    try
                    {
                        AnioMes.Seg_ValUniDim = double.Parse(rowInt["Seg_ValUniDim"].ToString());
                    }
                    catch (Exception ex) { }

                    try
                    {
                        AnioMes.Cantidad = double.Parse(rowInt["Cantidad"].ToString());
                    }
                    catch (Exception ex) { }

                    try
                    {
                        AnioMes.AplPotencialGeneralVende = double.Parse(rowInt["AplPotencialGeneralVende"].ToString());
                    }
                    catch (Exception ex) { }

                    try
                    {
                        AnioMes.AplPotencialGeneralNoVende = double.Parse(rowInt["AplPotencialGeneralNoVende"].ToString());
                    }
                    catch (Exception ex) { }

                    ListadoAniosMes.Add(AnioMes);

                }

                return ListadoAniosMes;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public List<eIntegralidadv2> spListIntegralidadByTipoProducto(string Conexion, eIntegralidadv2 parInte)
        {
            List<eIntegralidadv2> Lst = new List<eIntegralidadv2>();
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = { "@Tipo", "@Anio", "@Mes", "@AnioFin", "@MesFin", "@Id_Cd", "@IdZona", "@Id_Usu", "@Id_Cte", "@Id_Uen", "@Id_Seg" };
                object[] Valores = { parInte.Tipo, parInte.AnioIni, parInte.MesIni, parInte.AnioFin, parInte.MesFin, parInte.Id_Cd, 0, parInte.Id_Rik, parInte.Id_Cte, parInte.Id_Uen, parInte.Id_Seg };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spListIntegralidadBy2", ref dr, Parametros, Valores);
                while (dr.Read())
                {
                    var Obj = new eIntegralidadv2();
                    int Id_Cd = dr.IsDBNull(dr.GetOrdinal("Id_Cd")) ? 0 : Convert.ToInt32(dr["Id_Cd"]);
                    Obj.Id_Cd = Id_Cd;
                    Obj.Sucursal = dr.GetValue(dr.GetOrdinal("Sucursal")).ToString();

                    Obj.TipoProducto = dr.IsDBNull(dr.GetOrdinal("TipoProducto")) ? "" : dr.GetValue(dr.GetOrdinal("TipoProducto")).ToString();
                    Obj.Venta = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Venta")));
                    Obj.PorcentajeAplicacion = double.Parse(dr["PorcentajeAplicacion"].ToString());
                    try
                    {
                        Obj.Id_Rik = dr.IsDBNull(dr.GetOrdinal("Id_Usu")) ? 0 : Convert.ToInt32(dr["Id_Usu"]);
                    }
                    catch (Exception ex) { }

                    Lst.Add(Obj);
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                Lst = null;
            }
            return Lst;
        }
        public eIntegralidadv2 sp_IntegralidadMes_ActualizaVPOMetaV2(eIntegralidadv2 oeIntegralidadv2Par, string Conexion)
        {
            try
            {
                SqlConnection cn = new SqlConnection(Conexion);
                eIntegralidadv2 ListadoAniosMes = new eIntegralidadv2();
                cn.Open();
                SqlCommand sqlcmdA = new SqlCommand("sp_Actualizar_IntegralidadMes_VPT", cn);
                sqlcmdA.CommandType = CommandType.StoredProcedure;

                sqlcmdA.Parameters.AddWithValue("@Id_Cte", oeIntegralidadv2Par.Id_Cte);
                sqlcmdA.Parameters.AddWithValue("@Id_Ter", oeIntegralidadv2Par.Id_Ter);
                sqlcmdA.Parameters.AddWithValue("@VPOMeta", oeIntegralidadv2Par.VPOMeta);
                sqlcmdA.Parameters.AddWithValue("@Id_Cd", oeIntegralidadv2Par.Id_Cd);
                sqlcmdA.Parameters.AddWithValue("@Mes", oeIntegralidadv2Par.Mes);
                sqlcmdA.Parameters.AddWithValue("@Anio", oeIntegralidadv2Par.Anio);
                sqlcmdA.Parameters.AddWithValue("@Cantidad", oeIntegralidadv2Par.Cantidad);
                sqlcmdA.Parameters.AddWithValue("@VPT", oeIntegralidadv2Par.VPT);
                sqlcmdA.Parameters.AddWithValue("@IdUen", oeIntegralidadv2Par.Id_Uen);
                sqlcmdA.Parameters.AddWithValue("@IdSegmento", oeIntegralidadv2Par.Id_Seg);

                SqlDataAdapter dtInt = new SqlDataAdapter(sqlcmdA);
                DataTable dtDetalleInt = new DataTable();
                dtInt.Fill(dtDetalleInt);
                cn.Close();
                ListadoAniosMes.VPT = oeIntegralidadv2Par.VPT;
                //CapaDatosA.LimpiarSqlcommand(ref sqlcmdA);
                return ListadoAniosMes;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public List<eIntegralidadv2> spConsultar_SegmentoUen(int Id_Emp, int Id_Cd, string Conexion, int Tipo, int Id_Uen)
        {
            List<eIntegralidadv2> Lst = new List<eIntegralidadv2>();
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = { "@Id_Cd", "@Tipo", "@Id_Uen" };
                object[] Valores = { Id_Cd, Tipo, Id_Uen };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spConsultar_SegmentoUen", ref dr, Parametros, Valores);
                while (dr.Read())
                {
                    eIntegralidadv2 e = new eIntegralidadv2();
                    e.Id_Seg = (int)dr.GetValue(dr.GetOrdinal("Id"));
                    e.Seg_Descripcion = dr.GetValue(dr.GetOrdinal("Descripcion")).ToString();
                    Lst.Add(e);
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                Lst = null;
            }
            return Lst;
        }
    }
}