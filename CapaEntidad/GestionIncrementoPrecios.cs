using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{

    public class GestionIncrementoPrecios
    {
        public int? Id_Emp { get; set; }
        public int? Id_Cd { get; set; }
        public string NomCDI { get; set; }
        public int? Id_Zona { get; set; }
        public string NomZona { get; set; }
        public int? IdReporteGP { get; set; }
        public int? Id_Rik { get; set; }
        public string Nombre_Rik { get; set; }
        public int Activo { get; set; }
        public int? Id_Cte { get; set; }
        public string Cte_NomComercial { get; set; }
        public string Id_Tamaño { get; set; }
        public int Id_Categoria { get; set; }
        public string NomCategoria { get; set; }
        public Int64 Id_Prd { get; set; }
        public string Prd_Descripcion { get; set; }
        public string NomProducto { get; set; }
        public double Unidades { get; set; }
        public double Ventas { get; set; }
        public double VentasPO { get; set; }

        public double GAPIngresos_Monto { get; set; }
        public double GAPIngresos_Porc { get; set; }

        public double PrecioVenta { get; set; }
        public double PrecioObjetivo { get; set; }
        public double MgRed_Monto { get; set; }
        public double MgRedPO_Monto { get; set; }
        public double MgRed_Porc { get; set; }
        public double MgRedPO_Porc { get; set; }
        public double MgRedActual { get; set; }



        public double GAPMgRed_Monto { get; set; }
        public double GAPMgRed_Porc { get; set; }

        public int? IdUsuario { get; set; }
        public DateTime? FechaAlta { get; set; }
        public DateTime? FechaUltMod { get; set; }

        public int? Mes { get; set; }
        public int? Año { get; set; }
        public DateTime? FechaInicial { get; set; }
        public DateTime? FechaFinal { get; set; }
        public int Id_Estatus { get; set; }
        public string NomEstatus { get; set; }
        public int? Id_TipoReporte { get; set; }
        public double POvsPV { get; set; }
        public string TipoCuenta { get; set; }
        public int? Id_Matriz { get; set; }
        public string NombreMatriz { get; set; }
        public double VentasPA { get; set; }
        public double Var_VentaMonto { get; set; }
        public double Var_VentaPorc { get; set; }
        public double MgRed_MontoActual { get; set; }
        public double MgRed_PorcActual { get; set; }
        public double MgRed_Proyectada { get; set; }
        public double VarMgRed_Monto { get; set; }
        public double VarMgRed_Porc { get; set; }
        public double VarPpbRed_Porc { get; set; }

        public string Pc_NoConvenio { get; set; }
        public string PC_Nombre { get; set; }
        public string NombreCategoriaConvenio { get; set; }
        public int Id_Pc { get; set; }
        public string CategoriaConv { get; set; }


        public string EjecutivoCuenta { get; set; }
        public double PrecioListaActual { get; set; }
        public double PrecioVentaPromedioActual { get; set; }
        public double MgRed_MensualPesos { get; set; }
        public double MgRed_MensualPorc { get; set; }

        public double PrecioObjetivoProy { get; set; }
        public double PrecioListaProy { get; set; }
        public double PrecioMinRikProy { get; set; }
        public double PrecioGteProy { get; set; }
        public double PrecioNegociadoProy { get; set; }
        public double PorcIncrementoProy { get; set; }
        public double DescuentoSobrePlistaProy { get; set; }
        public double VentaProy { get; set; }

        public double MgRed_PesosProy { get; set; }
        public double MgRed_PorcProy { get; set; }
        public string Comentarios { get; set; }
        public double CostoAAAActual { get; set; }
        public double CostoAAAAFuturo { get; set; }
        public double UnidadesProyectadas { get; set; }
        public double DiasVigencia { get; set; }
        public DateTime? FechaInicioIncremento { get; set; }
        public string Telefono { get; set; }
        public double PorcentualIncremental { get; set; }
        public int TieneIncremento { get; set; }



        public GestionIncrementoPrecios()
        {
            this.Id_Emp = null;
            this.Id_Cd = null;
            this.NomCDI = null;
            this.Id_Zona = null;
            this.NomZona = null;
            this.IdReporteGP = null;
            this.Id_Rik = null;
            this.Nombre_Rik = null;
            this.Activo = 0;
            this.Id_Cte = null;
            this.Cte_NomComercial = null;
            this.Id_Categoria = 0;
            this.NomCategoria = null;
            this.Id_Prd = 0;
            this.Prd_Descripcion = "";
            this.NomProducto = null;
            this.Unidades = 0.00;
            this.Ventas = 0;
            this.VentasPO = 0;

            this.GAPIngresos_Monto = 0;
            this.GAPIngresos_Porc = 0;

            this.PrecioVenta = 0;
            this.PrecioObjetivo = 0;
            this.MgRed_Monto = 0;
            this.MgRedPO_Monto = 0;
            this.MgRed_Porc = 0;
            this.MgRedPO_Porc = 0;

            this.GAPMgRed_Monto = 0;
            this.GAPMgRed_Porc = 0;

            this.IdUsuario = 0;
            this.FechaAlta = null;
            this.FechaUltMod = null;

            this.Mes = null;
            this.Año = null;
            this.FechaInicial = null;
            this.FechaFinal = null;
            this.NomEstatus = null;
            this.Id_TipoReporte = null;
            this.POvsPV = 0;
            this.TipoCuenta = "";
            this.Id_Matriz = 0;
            this.NombreMatriz = "";
            this.VentasPA = 0;
            this.Var_VentaMonto = 0;
            this.Var_VentaPorc = 0;
            this.MgRed_MontoActual = 0;
            this.MgRed_PorcActual = 0;
            this.MgRed_Proyectada = 0;
            this.VarMgRed_Monto = 0;
            this.VarMgRed_Porc = 0;
            this.VarPpbRed_Porc = 0;
            this.Pc_NoConvenio = "";
            this.PC_Nombre = "";
            this.NombreCategoriaConvenio = "";
            this.Id_Pc = 0;
            this.CategoriaConv = "";

            this.EjecutivoCuenta = "";
            this.PrecioListaActual = 0;
            this.PrecioVentaPromedioActual = 0;
            this.MgRed_MensualPesos = 0;
            this.MgRed_MensualPorc = 0;
            this.PrecioObjetivoProy = 0;
            this.PrecioListaProy = 0;
            this.PrecioMinRikProy = 0;
            this.PrecioGteProy = 0;
            this.PrecioNegociadoProy = 0;
            this.PorcIncrementoProy = 0;
            this.DescuentoSobrePlistaProy = 0;
            this.VentaProy = 0;
            this.MgRed_PesosProy = 0;
            this.MgRed_PorcProy = 0;
            this.Comentarios = "";
            this.CostoAAAActual = 0;
            this.CostoAAAAFuturo = 0;
            this.UnidadesProyectadas = 0;
            this.DiasVigencia = 0;
            this.FechaInicioIncremento = null;
            this.Telefono = "";
            this.PorcentualIncremental = 0;
            this.Id_Estatus = 0;
            this.TieneIncremento = 0;



        }

        public GestionIncrementoPrecios(

        int? Id_Emp,
        int? Id_Cd,
        string NomCDI,
        int? Id_Zona,
        string NomZona,
        int? IdReporteGP,
        int? Id_Rik,
        string Nombre_Rik,
        int Activo,
        int? Id_Cte,
        string Cte_NomComercial,
        int Id_Categoria,
        string NomCategoria,
        Int64 Id_Prd,
        string Prd_Descripcion,
        string NomProducto,
        double Unidades,
        double Ventas,
        double VentasPO,

        double GAPIngresos_Monto,
        double GAPIngresos_Porc,

        double PrecioVenta,
        double PrecioObjetivo,
        double MgRed_Monto,
        double MgRedPO_Monto,
        double MgRed_Porc,
        double MgRedPO_Porc,

        double GAPMgRed_Monto,
        double GAPMgRed_Porc,

        int? IdUsuario,
        DateTime? FechaAlta,
        DateTime? FechaUltMod,

        int? Mes,
        int? Año,
        DateTime? FechaInicial,
        DateTime? FechaFinal,
        string NomEstatus,
        int? Id_TipoReporte,
        double POvsPV,
        string TipoCuenta,
        int? Id_Matriz,
        string NombreMatriz,
        double VentasPA,
        double Var_VentaMonto,
        double Var_VentaPorc,
        double MgRed_MontoActual,
        double MgRed_PorcActual,
        double MgRed_Proyectada,
        double VarMgRed_Monto,
        double VarMgRed_Porc,
        double VarPpbRed_Porc,
        string Pc_NoConvenio,
        string PC_Nombre,
        string NombreCategoriaConvenio,
        int Id_Pc,
        string CategoriaConv,

        string EjecutivoCuenta,
        double PrecioListaActual,
        double PrecioVentaPromedioActual,
        double MgRed_MensualPesos,
        double MgRed_MensualPorc,

        double PrecioObjetivoProy,
        double PrecioListaProy,
        double PrecioMinRikProy,
        double PrecioGteProy,
        double PrecioNegociadoProy,
        double PorcIncrementoProy,
        double DescuentoSobrePlistaProy,
        double VentaProy,

        double MgRed_PesosProy,
        double MgRed_PorcProy,
        string Comentarios,
        double CostoAAAActual,
        double CostoAAAAFuturo,
        double UnidadesProyectadas,
        double DiasVigencia,
        DateTime? FechaInicioIncremento,
        string Telefono,
        double PorcentualIncremental,
        int Id_Estatus,
        int TieneIncremento

        )

        {
            this.Id_Emp = null;
            this.Id_Cd = null;
            this.NomCDI = null;
            this.Id_Zona = null;
            this.NomZona = null;
            this.IdReporteGP = null;
            this.Id_Rik = null;
            this.Nombre_Rik = null;
            this.Activo = 0;
            this.Id_Cte = null;
            this.Cte_NomComercial = null;
            this.Id_Categoria = 0;
            this.NomCategoria = null;
            this.Id_Prd = 0;
            this.NomProducto = null;
            this.Unidades = 0.00;
            this.Ventas = 0;
            this.VentasPO = 0;

            this.GAPIngresos_Monto = 0;
            this.GAPIngresos_Porc = 0;

            this.PrecioVenta = 0;
            this.PrecioObjetivo = 0;
            this.MgRed_Monto = 0;
            this.MgRedPO_Monto = 0;
            this.MgRed_Porc = 0;
            this.MgRedPO_Porc = 0;

            this.GAPMgRed_Monto = 0;
            this.GAPMgRed_Porc = 0;

            this.IdUsuario = 0;
            this.FechaAlta = null;
            this.FechaUltMod = null;

            this.Mes = null;
            this.Año = null;
            this.FechaInicial = null;
            this.FechaFinal = null;
            this.NomEstatus = null;
            this.Id_TipoReporte = null;
            this.POvsPV = 0;
            this.TipoCuenta = "";
            this.Id_Matriz = 0;
            this.NombreMatriz = "";
            this.VentasPA = 0;
            this.Var_VentaMonto = 0;
            this.Var_VentaPorc = 0;
            this.MgRed_MontoActual = 0;
            this.MgRed_PorcActual = 0;
            this.MgRed_Proyectada = 0;
            this.VarMgRed_Monto = 0;
            this.VarMgRed_Porc = 0;
            this.VarPpbRed_Porc = 0;
            this.Pc_NoConvenio = "";
            this.PC_Nombre = "";
            this.NombreCategoriaConvenio = "";
            this.Id_Pc = 0;
            this.CategoriaConv = "";

            this.EjecutivoCuenta = "";
            this.PrecioListaActual = 0;
            this.PrecioVentaPromedioActual = 0;
            this.MgRed_MensualPesos = 0;
            this.MgRed_MensualPorc = 0;
            this.PrecioObjetivoProy = 0;
            this.PrecioListaProy = 0;
            this.PrecioMinRikProy = 0;
            this.PrecioGteProy = 0;
            this.PrecioNegociadoProy = 0;
            this.PorcIncrementoProy = 0;
            this.DescuentoSobrePlistaProy = 0;
            this.VentaProy = 0;
            this.MgRed_PesosProy = 0;
            this.MgRed_PorcProy = 0;
            this.Comentarios = "";
            this.CostoAAAActual = 0;
            this.CostoAAAAFuturo = 0;
            this.UnidadesProyectadas = 0;
            this.DiasVigencia = 0;
            this.FechaInicioIncremento = null;
            this.Telefono = "";
            this.PorcentualIncremental = 0;
            this.Id_Estatus = 0;
            this.TieneIncremento = 0;
        }
    }
}