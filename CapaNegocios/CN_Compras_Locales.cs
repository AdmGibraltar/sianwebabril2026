using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using CapaDatos;
using CapaModelo;
using System.Data;

// ENE20-2020 RFH 

namespace CapaNegocios
{
    public class CN_Compras_Locales
    {

        //
        // FEB20-2020 RFH 
        // Compras Locales / Indice 
        // 
        //public List<CapaEntidad.ComprasLocales> Get_ComprasLocales_Index(
        //    int Id_Emp, int Id_Cd, int PageNo, int PageSize, int Id_Com, int Id_Estatus, string Conexion)
        //{
        //    CD_ComprasLocales CD = new CD_ComprasLocales();
        //    return CD.Get_ComprasLocales_Index(
        //        Id_Emp, Id_Cd, PageNo, PageSize, Id_Com, Id_Estatus, Conexion);
        //}

        //
        // ENE28-2020 RFH 
        // Compras Locales / Productos y Servicios
        // 

        public List<eCL_SATProductosYServicios> Get_CatProductosYServiciosSAT(string CveProdServ, string Conexion)
        {
            CD_CL_Motivo CD = new CD_CL_Motivo();
            return CD.spSATProductosYServicios(CveProdServ, Conexion);
        }

        // FEB10-2020 RFH 
        public string SelCL_CodigoHomologado_Maximo_Consulta(int Id_Prov, string Conexion)
        {
            CD_CL_Motivo CD = new CD_CL_Motivo();
            return CD.SelCL_CodigoHomologado_Maximo_Consulta(Id_Prov, Conexion);
        }

        // ENE20-2020 RFH 
        public List<eCL_Motivo> Get_ListadoMotivo(int Id_MotivoCL, string Conexion)
        {
            CD_CL_Motivo CD = new CD_CL_Motivo();
            return CD.SelCL_Motivo(Id_MotivoCL, Conexion);
        }

        // ENE20-2020 RFH 
        public List<eListaGenerica> Get_ListadoCausaDesabasto(int Id_Causa, string Conexion)
        {
            CD_CL_Motivo CD = new CD_CL_Motivo();
            return CD.SelCL_CausaDesabasto(Id_Causa, Conexion);
        }

        // ENE20-2020 RFH 
        public List<eListaGenerica> Get_ListadoTipoProducto(int Id_TipoProducto, int Id_Emp, string Conexion)
        {
            CD_CL_Motivo CD = new CD_CL_Motivo();
            return CD.SelCL_ListadoTipoProducto(Id_TipoProducto, Id_Emp, Conexion);
        }

        // ENE20-2020 RFH 
        public List<eListaGenerica> Get_ListadoProductoFamiliaCte(int Id_ProductoFamiliaCte, int Id2, string Conexion)
        {
            CD_CL_Motivo CD = new CD_CL_Motivo();
            return CD.SelCL_ListadoProductoFamiliaCte(Id_ProductoFamiliaCte, Id2, Conexion);
        }

        // ENE20-2020 RFH 
        public List<eListaGenerica> Get_ListadoProductoSubFamilia(int Id1, int Id2, int Id3, string Conexion)
        {
            CD_CL_Motivo CD = new CD_CL_Motivo();
            return CD.SelCL_ListadoProductoSubFamilia(Id1, Id2, Id3, Conexion);
        }

        // ENE20-2020 RFH 
        public List<eListaGenerica> CompraLocalPedidosProducto_Lista(int Solicitud, Int64 Id_Producto, string Conexion)
        {
            CD_CapOrdenCompra CN = new CD_CapOrdenCompra();
            return CN.spCompraLocalPedidosProducto_Lista(Solicitud, Id_Producto, Conexion);
        }

        //RBM MARZO 2024
        public List<eComprasLocales> Get_ComprasLocales_Index(int Id_U, int Id_Emp, int Id_Cd, int PageNo, int PageSize, string Vencido, Int64 Id_Prd, int IdProveedorLocal, int Id_Motivo, int Id_Comp, int Id_Estatus, ref int Verificador, string Conexion)
        {
            CD_ComprasLocales CD = new CD_ComprasLocales();
            return CD.Get_ComprasLocales_Index(Id_U, Id_Emp, Id_Cd, PageNo, PageSize, Vencido, Id_Prd, IdProveedorLocal, Id_Motivo, Id_Comp, Id_Estatus, ref Verificador, Conexion);

        }

        //RBM MARZO 2024
        public void Get_DetalleCL(ref CompraLocal CL, string emp_Cnx)
        {
            CD_ComprasLocales CD = new CD_ComprasLocales();
            CD.Get_DetalleCL(ref CL, emp_Cnx);
        }

        //RBM MARZO 2024
        public List<clPrecio> Get_PreciosCL(ref CompraLocal CL, string emp_Cnx)
        {
            CD_ComprasLocales CD = new CD_ComprasLocales();
            return CD.Get_PreciosCL(ref CL, emp_Cnx);
        }

        //RBM MARZO 2024
        public List<ClienteExclusivo> Get_ClientesExclusivosCL(ref CompraLocal CL, string emp_Cnx)
        {
            CD_ComprasLocales CD = new CD_ComprasLocales();
            return CD.Get_ClientesExclusivosCL(ref CL, emp_Cnx);
        }

        //RBM MARZO 2024
        public List<eCLLogs> spComprasLocales_GetLogs(int Id_Emp, int Id_Cd, int Id_Comp, string emp_Cnx)
        {
            CD_ComprasLocales CD = new CD_ComprasLocales();
            return CD.spComprasLocales_GetLogs(Id_Emp, Id_Cd, Id_Comp, emp_Cnx);
        }

        //RBM MARZO 2024 Reportes Compras Locales 
        public void ConsultaComprasLocalesXMotivo(CompraLocal CL, ref List<CompraLocal> ListaComprasLocales, DateTime FechaIni, DateTime FechaFin, string Emp_Cnx)
        {
            CD_ComprasLocales CD = new CD_ComprasLocales();
            CD.ConsultaComprasLocalesXMotivo(CL, ref ListaComprasLocales, FechaIni, FechaFin, Emp_Cnx);
        }

        public void ConsultaComprasLocalesXSucursal(CompraLocal CL, ref List<CompraLocal> ListaComprasLocales, DateTime FechaIni, DateTime FechaFin, string Emp_Cnx)
        {
            CD_ComprasLocales CD = new CD_ComprasLocales();
            CD.ConsultaComprasLocalesXSucursal(CL, ref ListaComprasLocales, FechaIni, FechaFin, Emp_Cnx);
        }

        public void ConsultaComprasLocalesXTipoProduto(CompraLocal CL, ref List<CompraLocal> ListaComprasLocales, DateTime FechaIni, DateTime FechaFin, string Emp_Cnx)
        {
            CD_ComprasLocales CD = new CD_ComprasLocales();
            CD.ConsultaComprasLocalesXTipoProduto(CL, ref ListaComprasLocales, FechaIni, FechaFin, Emp_Cnx);
        }

        public void ConsultaComprasLocalesXProvLocal(CompraLocal CL, ref List<CompraLocal> ListaComprasLocales, DateTime FechaIni, DateTime FechaFin, string Emp_Cnx)
        {
            CD_ComprasLocales CD = new CD_ComprasLocales();
            CD.ConsultaComprasLocalesXProvLocal(CL, ref ListaComprasLocales, FechaIni, FechaFin, Emp_Cnx);
        }

        //public void ConsultaComprasLocalesXProvLocal(CompraLocal CL, ref DataSet dtResultado, DateTime FechaIni, DateTime FechaFin, string Emp_Cnx)
        //{
        //    CD_ComprasLocales CD = new CD_ComprasLocales();
        //    CD.ConsultaComprasLocalesXProvLocal(CL, ref dtResultado, FechaIni, FechaFin, Emp_Cnx);
        //}

        public void ConsultaComprasLocalesXProvCentral(CompraLocal CL, ref List<CompraLocal> ListaComprasLocales, DateTime FechaIni, DateTime FechaFin, string Emp_Cnx)
        {
            CD_ComprasLocales CD = new CD_ComprasLocales();
            CD.ConsultaComprasLocalesXProvCentral(CL, ref ListaComprasLocales, FechaIni, FechaFin, Emp_Cnx);
        }
        public void ConsultaComprasLocalesXAplicacion(CompraLocal CL, ref List<CompraLocal> ListaComprasLocales, DateTime FechaIni, DateTime FechaFin, string Emp_Cnx)
        {
            CD_ComprasLocales CD = new CD_ComprasLocales();
            CD.ConsultaComprasLocalesXAplicacion(CL, ref ListaComprasLocales, FechaIni, FechaFin, Emp_Cnx);
        }
        public void ConsultaComprasLocalesXPrecioAAA(CompraLocal CL, ref DataSet dsDatos, DateTime FechaIni, DateTime FechaFin, string Emp_Cnx)
        {
            CD_ComprasLocales CD = new CD_ComprasLocales();
            CD.ConsultaComprasLocalesXPrecioAAA(CL, ref dsDatos, FechaIni, FechaFin, Emp_Cnx);
        }

        public void ConsultaComprasLocalesXMotivoDet(CompraLocal CL, ref DataSet dtDatos, DateTime FechaIni, DateTime FechaFin, string Emp_Cnx)
        {
            CD_ComprasLocales CD = new CD_ComprasLocales();
            CD.ConsultaComprasLocalesXMotivoDet(CL, ref dtDatos, FechaIni, FechaFin, Emp_Cnx);
        }

        public void DescargaReporteCL(int id_Emp, int id_Cd, DateTime fechaInicial, DateTime fechaFinal, int motivoCL, int tipoProductoCL, int aplicacionCL, int productoCentralCL, string proveedorCentralCL, int proveedorLocalCL, ref DataSet dsDatos, string emp_Cnx)
        {
            CD_ComprasLocales CD = new CD_ComprasLocales();
            CD.DescargaReporteCL(id_Emp, id_Cd, fechaInicial, fechaFinal, motivoCL, tipoProductoCL, aplicacionCL, productoCentralCL, proveedorCentralCL, proveedorLocalCL, ref dsDatos, emp_Cnx);
        }

        public void ConsultaComprasLocalesXTipoAAA(CompraLocal CL, ref List<CompraLocal> ListaComprasLocales, DateTime FechaIni, DateTime FechaFin, string Emp_Cnx)
        {
            CD_ComprasLocales CD = new CD_ComprasLocales();
            CD.ConsultaComprasLocalesXTipoAAA(CL, ref ListaComprasLocales, FechaIni, FechaFin, Emp_Cnx);
        }
    }
}