using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class eComprasLocales
    {
        public int Id_Cd { get; set; }
        public int Solicitud { get; set; }
        public DateTime FechaSolicitud { get; set; }
        public String UsuarioSolicito { get; set; }
        public int TipoSolicitud { get; set; }
        public string Vigencia { get; set; }
        public String PedidoReferencia { get; set; }
        public String Comentarios { get; set; }
        public String Estatus { get; set; }
        public DateTime FechaAutorizacion { get; set; }
        public Int64 CodigoCompraLocal { get; set; }
        public String DescripcionProducto { get; set; }
        public DateTime FechaCorreo { get; set; }
        public String CorreoAutoriza { get; set; }
        public String UsuarioAutorizo { get; set; }
        public Int64 ProductoMaestro { get; set; }
        public Double PrecioAAA { get; set; }
        public Double PrecioCostoCompralocal { get; set; }
        public Double PrecioCosto { get; set; }
        public Double PrecioPublico { get; set; }
        public String Situacion { get; set; }
        public String Cd_Nombre { get; set; }
        public int Comp_Solicitud { get; set; }
        public int Id_Comp { get; set; }
        public int Id_CompDet { get; set; }
        public string Comp_FechaSol { get; set; }
        public int FechaSolicitud_Anio { get; set; }
        public int FechaSolicitud_Mes { get; set; }
        public string Det_FecAut { get; set; }
        public string U_Nombre { get; set; }
        public string U_Correo { get; set; }
        public string VigenciaFecha { get; set; }
        public int IdTipoSolicitud { get; set; }
        public string TipoSolicitudNombre { get; set; }
        public int Autorizados { get; set; }
        public int Totales { get; set; }
        public string Det_Estatus { get; set; }
        public Int64 Id_Prd { get; set; }
        // 13Abr2022 RFH
        public Int64 Id_PrdOriginal { get; set; }
        public string ProductoNombre { get; set; }
        public int CatOrdenada { get; set; }
        public float MontoCompraLocal { get; set; }

        public int IdProveedor { get; set; }
        public string ProveedorNombre { get; set; }

        public string ProveedorPadreClave { get; set; }
        public string ProveedorPadreNombre { get; set; }

        public int IdTipoProducto { get; set; }
        public string TipoProducto { get; set; }

        public string ProductoAplicacion { get; set; }
        public string ProductoFamilia { get; set; }
        public int IdCausaDesabasto { get; set; }
        public string CausaDesabasto { get; set; }

        public int AutorizadorId { get; set; }
        public string AutorizadorNombre { get; set; }

        public int Entradas_Cant { get; set; }
        public decimal Entradas_Costo { get; set; }

        public int Salidas_Cant { get; set; }
        public decimal Salidas_Costo { get; set; }
        public string Prd_Presentacion { get; set; }
        public int Prd_UniEmp { get; set; }
    }

    // Compra Local Reporte Mensual (ClRm)
    public class eClRm_MotivoCompra
    {
        public int Id_Cd { get; set; }
        public int IdMotivo { get; set; }
        public string NombreSucursal { get; set; }
        public double Importe { get; set; }
        public double Importe1 { get; set; }
        public double Importe2 { get; set; }
        public double Importe3 { get; set; }

    }

    public class eClRm_PorSucursal
    {
        public int Id_Cd { get; set; }
        public string NombreSucursal { get; set; }
        public double Monto { get; set; }
    }

    public class eClRm_PorTipoProducto
    {
        public int Id_Cd { get; set; }
        public string NombreSucursal { get; set; }
        public double Monto { get; set; }

        public string TipoProducto { get; set; }
    }
    public class eClRm_MontoProveedorLocal
    {
        public int Id_Cd { get; set; }
        public string NombreSucursal { get; set; }
        public double Monto { get; set; }

        public string NombreProveedor { get; set; }
    }

    public class eClRm_MontoCompra_Aplicacion
    {
        public int Id_Cd { get; set; }
        public string AplicacionNombre { get; set; }
        public double Monto { get; set; }
    }

    public class eReporteMensual
    {

        public int AltasSkus { get; set; }
        public double Monto { get; set; }
        public float NoProveedores { get; set; }

        public string Sucurales { get; set; }

        public List<eClRm_MotivoCompra> MotivoCompra { get; set; }
        public List<eClRm_PorSucursal> PorSucursal { get; set; }
        public List<eClRm_PorTipoProducto> PorTipoProducto { get; set; }

        public List<eClRm_MontoProveedorLocal> MontoProveedorLocal { get; set; }
        public List<eClRm_MontoCompra_Aplicacion> MontoCompra_Aplicacion { get; set; }
    }

    public class eCLRm_Param
    {
        public eCLRm_Param(string _Nombre, string _sValor, int _iValor, Int64 _iValor64)
        {
            Nombre = _Nombre;
            sValor = _sValor;
            iValor = _iValor;
            iValor64 = _iValor64;
        }
        public string Nombre { get; set; }
        public string sValor { get; set; }
        public int iValor { get; set; }
        public Int64 iValor64 { get; set; }
    }
}