using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using System.Data;

namespace CapaNegocios
{
    public class CN_Campanias
    {

        public List<Campania> ComboCampanias()
        {
            CD_Campanias cn = new CD_Campanias();
            return cn.ComboCampanias();
        }


        public DataTable Reporte_Gestion_Proyecto_Principal(int Id_Campania, string Productos, DateTime FechaInicio, DateTime FechaFin, string Riks)
        {
            CD_Campanias cn = new CD_Campanias();
            return cn.Reporte_Gestion_Proyecto_Principal(Id_Campania, Productos, FechaInicio, FechaFin, Riks);
        }

        public DataTable Reporte_Gestion_Proyecto_Detalle(int Id_Campania, int Id_Rik, string Productos, DateTime FechaInicio, DateTime FechaFin)
        {
            CD_Campanias cn = new CD_Campanias();
            return cn.Reporte_Gestion_Proyecto_Detalle(Id_Campania, Id_Rik, Productos, FechaInicio, FechaFin);
        }


        public DataTable Reporte_Gestion_Proyecto_Detalle_Prod(int Id_Op, int Campania_Id, string Productos, DateTime FechaInicio, DateTime FechaFin)
        {
            CD_Campanias cn = new CD_Campanias();
            return cn.Reporte_Gestion_Proyecto_Detalle_Prod(Id_Op, Campania_Id, Productos, FechaInicio, FechaFin);
        }




        public DataTable Reporte_Gestion_ACYS_Principal(int Id_Campania, string Productos, DateTime FechaInicio, DateTime FechaFin, string Riks)
        {
            CD_Campanias cn = new CD_Campanias();
            return cn.Reporte_Gestion_ACYS_Principal(Id_Campania, Productos, FechaInicio, FechaFin, Riks);
        }


        public DataTable Reporte_Gestion_ACYS_Detalle(int Id_Campania, int Id_Rik, string Productos, DateTime FechaInicio, DateTime FechaFin)
        {
            CD_Campanias cn = new CD_Campanias();
            return cn.Reporte_Gestion_ACYS_Detalle(Id_Campania, Id_Rik, Productos, FechaInicio, FechaFin);
        }


        public DataTable Reporte_Gestion_ACYS_Detalle_Prod(int Id_Campania, int Id_Rik, int Id_ACYS, string Productos, DateTime FechaInicio, DateTime FechaFin)
        {
            CD_Campanias cn = new CD_Campanias();
            return cn.Reporte_Gestion_ACYS_Detalle_Prod(Id_Campania, Id_Rik, Id_ACYS, Productos, FechaInicio, FechaFin);
        }

        public DataTable Reporte_Gestion_Pedidos_Principal(int Id_Campania, string Productos, DateTime FechaInicio, DateTime FechaFin, string Riks)
        {
            CD_Campanias cn = new CD_Campanias();
            return cn.Reporte_Gestion_Pedidos_Principal(Id_Campania, Productos, FechaInicio, FechaFin, Riks);
        }


        public DataTable Reporte_Gestion_Pedidos_Detalle(int Id_Campania, int Id_Rik, string Productos, DateTime FechaInicio, DateTime FechaFin)
        {
            CD_Campanias cn = new CD_Campanias();
            return cn.Reporte_Gestion_Pedidos_Detalle(Id_Campania, Id_Rik, Productos, FechaInicio, FechaFin);
        }


        public DataTable Reporte_Gestion_Pedidos_Detalle_Prod(int Id_Campania, int Id_Ped, string Productos, DateTime FechaInicio, DateTime FechaFin)
        {
            CD_Campanias cn = new CD_Campanias();
            return cn.Reporte_Gestion_Pedidos_Prod(Id_Campania, Id_Ped, Productos, FechaInicio, FechaFin);
        }



        public DataTable Reporte_Gestion_Facturas_Principal(int Id_Campania, string Productos, DateTime FechaInicio, DateTime FechaFin, string Riks)
        {
            CD_Campanias cn = new CD_Campanias();
            return cn.Reporte_Gestion_Facturas_Principal(Id_Campania, Productos, FechaInicio, FechaFin, Riks);
        }


        public DataTable Reporte_Gestion_Facturas_Detalle(int Id_Campania, int Id_Rik, string Productos, DateTime FechaInicio, DateTime FechaFin)
        {
            CD_Campanias cn = new CD_Campanias();
            return cn.Reporte_Gestion_Facturas_Detalle(Id_Campania, Id_Rik, Productos, FechaInicio, FechaFin);
        }


        public DataTable Reporte_Gestion_Facturas_Prod(int Id_Campania, int Id_Fac, string Productos, DateTime FechaInicio, DateTime FechaFin)
        {
            CD_Campanias cn = new CD_Campanias();
            return cn.Reporte_Gestion_Facturas_Prod(Id_Campania, Id_Fac, Productos, FechaInicio, FechaFin);
        }


        public DataTable ComboProductos()
        {
            CD_Campanias cn = new CD_Campanias();
            return cn.ComboProductos();
        }


        public DataTable ComboRIKS()
        {
            CD_Campanias cn = new CD_Campanias();
            return cn.ComboRIKS();
        }


        public Boolean Aplicacion_ProdNuevos(int id_apl)
        {
            CD_Campanias cn = new CD_Campanias();
            return cn.Aplicacion_ProdNuevos(id_apl);
        }


        public Boolean ProdNuevos(Int64 id_prd)
        {
            CD_Campanias cn = new CD_Campanias();
            return cn.ProdNuevos(id_prd);
        }
    }
}