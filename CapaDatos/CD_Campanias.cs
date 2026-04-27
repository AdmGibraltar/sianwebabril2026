using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CapaEntidad;

namespace CapaDatos
{
    public class CD_Campanias
    {


        public List<Campania> ComboCampanias()
        {
            string cnn = ClaseConexion.GetClaseConexion().conexion;
            var dbacc = new dbAccess(cnn);

            List<Campania> campanias = new List<Campania>();

            var res = dbacc.spExecDataSet("spCatMonProd_ComboCampanias").Tables[0];
            IList<DataRow> camEn = res.AsEnumerable().ToList();


            foreach (DataRow r in camEn)
            {
                var itemCamp = new Campania();

                itemCamp.Id = Int32.Parse(r["Id"].ToString());
                itemCamp.Nombre = r["Nombre"].ToString();

                campanias.Add(itemCamp);
            }

            return campanias;
        }


        public DataTable Reporte_Gestion_Proyecto_Principal(int Id_Campania, string Productos, DateTime FechaInicio, DateTime FechaFin, string Riks)
        {
            string cnn = ClaseConexion.GetClaseConexion().conexion;
            var dbacc = new dbAccess(cnn);

            SqlParameter[] par =
            {
                 new SqlParameter("@Campania_Id", Id_Campania),
                 new SqlParameter("@Productos", Productos),
                 new SqlParameter("@FechaInicio", FechaInicio),
                 new SqlParameter("@FechaFin", FechaFin),
                 new SqlParameter("@Riks", Riks)
            };

            var res = dbacc.spExecDataSet("spCatMonProd_RepGestProy", par).Tables[0];
            // IList<DataRow> listado = res.AsEnumerable().ToList();
            dbacc.Dispose();
            return res;
        }


        public DataTable Reporte_Gestion_Proyecto_Detalle(int Id_Campania, int Id_Rik, string Productos, DateTime FechaInicio, DateTime FechaFin)
        {
            string cnn = ClaseConexion.GetClaseConexion().conexion;
            var dbacc = new dbAccess(cnn);

            SqlParameter[] par =
            {
                 new SqlParameter("@Campania_Id", Id_Campania),
                 new SqlParameter("@Id_Rik", Id_Rik),
                 new SqlParameter("@Productos", Productos),
                                  new SqlParameter("@FechaInicio", FechaInicio),
                 new SqlParameter("@FechaFin", FechaFin)
            };

            var res = dbacc.spExecDataSet("[spCatMonProd_RepGestProy_Detalle]", par).Tables[0];
            dbacc.Dispose();
            // IList<DataRow> listado = res.AsEnumerable().ToList();
            return res;
        }



        public DataTable Reporte_Gestion_Proyecto_Detalle_Prod(int Id_Op, int Campania_Id, string Productos, DateTime FechaInicio, DateTime FechaFin)
        {
            string cnn = ClaseConexion.GetClaseConexion().conexion;
            var dbacc = new dbAccess(cnn);

            SqlParameter[] par =
            {
                 new SqlParameter("@Id_Op", Id_Op),
                  new SqlParameter("@Campania_Id", Campania_Id),
                   new SqlParameter("@Productos", Productos),
                                    new SqlParameter("@FechaInicio", FechaInicio),
                 new SqlParameter("@FechaFin", FechaFin)
            };

            var res = dbacc.spExecDataSet("spCatMonProd_RepGestProy_Detalle_Prod", par).Tables[0];

            return res;
        }


        public DataTable Reporte_Gestion_ACYS_Principal(int Id_Campania, string Productos, DateTime FechaInicio, DateTime FechaFin, string Riks)
        {
            string cnn = ClaseConexion.GetClaseConexion().conexion;
            var dbacc = new dbAccess(cnn);

            var res = dbacc.spExecDataSet("spCatMonProd_RepGestionACYS", new SqlParameter("@Campania_Id", Id_Campania),
                 new SqlParameter("@Productos", Productos)
                 , new SqlParameter("@FechaInicio", FechaInicio),
                 new SqlParameter("@FechaFin", FechaFin),
                 new SqlParameter("@Riks", Riks)
                 ).Tables[0];
            // IList<DataRow> listado = res.AsEnumerable().ToList();

            dbacc.Dispose();
            return res;
        }


        public DataTable Reporte_Gestion_ACYS_Detalle(int Id_Campania, int Id_Rik, string Productos, DateTime FechaInicio, DateTime FechaFin)
        {
            string cnn = ClaseConexion.GetClaseConexion().conexion;
            var dbacc = new dbAccess(cnn);

            SqlParameter[] par =
            {
                 new SqlParameter("@Campania_Id", Id_Campania),
                 new SqlParameter("@Id_Rik", Id_Rik),
                 new SqlParameter("@Productos", Productos),
                                  new SqlParameter("@FechaInicio", FechaInicio),
                 new SqlParameter("@FechaFin", FechaFin)
            };

            var res = dbacc.spExecDataSet("spCatMonProd_RepGestionACYS_Det", par).Tables[0];
            // IList<DataRow> listado = res.AsEnumerable().ToList();
            dbacc.Dispose();
            return res;
        }



        public DataTable Reporte_Gestion_ACYS_Detalle_Prod(int Id_Campania, int Id_Rik, int Id_Acs, string Productos, DateTime FechaInicio, DateTime FechaFin)
        {
            string cnn = ClaseConexion.GetClaseConexion().conexion;
            var dbacc = new dbAccess(cnn);

            SqlParameter[] par =
            {
                 new SqlParameter("@Campania_Id", Id_Campania),
                 new SqlParameter("@Id_Rik", Id_Rik) ,
                 new SqlParameter("@Id_Acs", Id_Acs),
                 new SqlParameter("@Productos", Productos),
                                  new SqlParameter("@FechaInicio", FechaInicio),
                 new SqlParameter("@FechaFin", FechaFin)
            };

            var res = dbacc.spExecDataSet("spCatMonProd_RepGestionACYS_Det_Prod", par).Tables[0];
            dbacc.Dispose();
            return res;
        }



        public DataTable Reporte_Gestion_Pedidos_Principal(int Id_Campania, string Productos, DateTime FechaInicio, DateTime FechaFin, string Riks)
        {
            string cnn = ClaseConexion.GetClaseConexion().conexion;
            var dbacc = new dbAccess(cnn);

            var res = dbacc.spExecDataSet("spCatMonProd_RepGestPedidos", new SqlParameter("@Campania_Id", Id_Campania),
                 new SqlParameter("@Productos", Productos)
                 , new SqlParameter("@FechaInicio", FechaInicio),
                 new SqlParameter("@FechaFin", FechaFin)
                 ,
                 new SqlParameter("@Riks", Riks)
                 ).Tables[0];
            // IList<DataRow> listado = res.AsEnumerable().ToList();

            dbacc.Dispose();
            return res;
        }


        public DataTable Reporte_Gestion_Pedidos_Detalle(int Id_Campania, int Id_Rik, string Productos, DateTime FechaInicio, DateTime FechaFin)
        {
            string cnn = ClaseConexion.GetClaseConexion().conexion;
            var dbacc = new dbAccess(cnn);

            SqlParameter[] par =
            {
                 new SqlParameter("@Campania_Id", Id_Campania),
                 new SqlParameter("@Id_Rik", Id_Rik),
                 new SqlParameter("@Productos", Productos),
                                  new SqlParameter("@FechaInicio", FechaInicio),
                 new SqlParameter("@FechaFin", FechaFin)

            };

            var res = dbacc.spExecDataSet("spCatMonProd_RepGestPedidos_Det", par).Tables[0];
            // IList<DataRow> listado = res.AsEnumerable().ToList();
            dbacc.Dispose();
            return res;
        }



        public DataTable Reporte_Gestion_Pedidos_Prod(int Id_Campania, int Id_Ped, string Productos, DateTime FechaInicio, DateTime FechaFin)
        {
            string cnn = ClaseConexion.GetClaseConexion().conexion;
            var dbacc = new dbAccess(cnn);

            SqlParameter[] par =
            {
                 new SqlParameter("@Campania_Id", Id_Campania),
                 new SqlParameter("@Id_Ped", Id_Ped),
                 new SqlParameter("@Productos", Productos),
                  new SqlParameter("@FechaInicio", FechaInicio),
                 new SqlParameter("@FechaFin", FechaFin)
            };

            var res = dbacc.spExecDataSet("spCatMonProd_RepGestPedidos_DetProd", par).Tables[0];
            dbacc.Dispose();
            return res;
        }


        public DataTable Reporte_Gestion_Facturas_Principal(int Id_Campania, string Productos, DateTime FechaInicio, DateTime FechaFin, string Riks)
        {
            string cnn = ClaseConexion.GetClaseConexion().conexion;
            var dbacc = new dbAccess(cnn);

            var res = dbacc.spExecDataSet("spCatMonProd_RepGestFacturas", new SqlParameter("@Campania_Id", Id_Campania),
                 new SqlParameter("@Productos", Productos),
                new SqlParameter("@FechaInicio", FechaInicio),
                 new SqlParameter("@FechaFin", FechaFin)
                 ,
                 new SqlParameter("@Riks", Riks)).Tables[0];
            // IList<DataRow> listado = res.AsEnumerable().ToList();
            dbacc.Dispose();
            return res;
        }


        public DataTable Reporte_Gestion_Facturas_Detalle(int Id_Campania, int Id_Rik, string Productos, DateTime FechaInicio, DateTime FechaFin)
        {
            string cnn = ClaseConexion.GetClaseConexion().conexion;
            var dbacc = new dbAccess(cnn);

            SqlParameter[] par =
            {
                 new SqlParameter("@Campania_Id", Id_Campania),
                 new SqlParameter("@Id_Rik", Id_Rik),
                 new SqlParameter("@Productos", Productos),
                 new SqlParameter("@FechaInicio", FechaInicio),
                 new SqlParameter("@FechaFin", FechaFin)
            };

            var res = dbacc.spExecDataSet("spCatMonProd_RepGestFacturas_Det", par).Tables[0];
            // IList<DataRow> listado = res.AsEnumerable().ToList();
            dbacc.Dispose();
            return res;
        }

        public DataTable Reporte_Gestion_Facturas_Prod(int Id_Campania, int Id_Fac, string Productos, DateTime FechaInicio, DateTime FechaFin)
        {
            string cnn = ClaseConexion.GetClaseConexion().conexion;
            var dbacc = new dbAccess(cnn);

            SqlParameter[] par =
            {
                 new SqlParameter("@Campania_Id", Id_Campania),
                 new SqlParameter("@Id_Fac", Id_Fac),
                 new SqlParameter("@Productos", Productos),
                 new SqlParameter("@FechaInicio", FechaInicio),
                 new SqlParameter("@FechaFin", FechaFin)
            };

            var res = dbacc.spExecDataSet("spCatMonProd_RepGestFacturas_DetProd", par).Tables[0];
            dbacc.Dispose();
            return res;
        }

        public DataTable ComboProductos()
        {
            string cnn = ClaseConexion.GetClaseConexion().conexion;
            var dbacc = new dbAccess(cnn);

            SqlParameter[] par =
            {
            };

            var res = dbacc.spExecDataSet("spCatMonProd_ComboProductos", par).Tables[0];

            return res;
        }


        public DataTable ComboRIKS()
        {
            string cnn = ClaseConexion.GetClaseConexion().conexion;
            var dbacc = new dbAccess(cnn);

            SqlParameter[] par =
            {
            };

            var res = dbacc.spExecDataSet("spCatMonProd_ComboRiks", par).Tables[0];
            dbacc.Dispose();
            return res;
        }


        public Boolean Aplicacion_ProdNuevos(int id_apl)
        {
            string cnn = ClaseConexion.GetClaseConexion().conexion;
            var dbacc = new dbAccess(cnn);
            Boolean resultado = false;

            SqlParameter[] par =
            {
                   new SqlParameter("@Id_apl", id_apl),
            };

            var res = dbacc.spExecDataSet("spCatMonProd_NuevosProductosAp", par).Tables[0];

            if (res.Rows.Count > 0)
            {
                resultado = true;
            }


            dbacc.Dispose();


            return resultado;
        }


        public Boolean ProdNuevos(Int64 id_prd)
        {
            string cnn = ClaseConexion.GetClaseConexion().conexion;
            var dbacc = new dbAccess(cnn);
            Boolean resultado = false;

            SqlParameter[] par =
            {
                   new SqlParameter("@Id_prd", id_prd),
            };

            var res = dbacc.spExecDataSet("spCatMonProd_NuevosProductos", par).Tables[0];

            if (res.Rows.Count > 0)
            {
                resultado = true;
            }


            dbacc.Dispose();


            return resultado;
        }










    }
}