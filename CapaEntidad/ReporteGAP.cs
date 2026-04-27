using System;

/// <summary>
/// Summary description for Leads
/// </summary>
/// 

namespace CapaEntidad
{
    public class ReporteGAP
    {
        public int? Id_Emp { get; set; }
        public int? Id_Cd { get; set; }
        public string NomCDI { get; set; }
        public int? Id_Zona { get; set; }
        public string NomZona { get; set; }
        public int? IdReporteGAP { get; set; }
        public int? Id_Rik { get; set; }
        public string Nombre_Rik { get; set; }
        public int Activo { get; set; }
        public int? Id_Cte { get; set; }
        public string Cte_NomComercial { get; set; }
        public string Id_Tamaño { get; set; }
        public int Id_Categoria { get; set; }
        public string NomCategoria { get; set; }
        public Int64 Id_Prd { get; set; }
        public string NomProducto { get; set; }
        public int Unidades { get; set; }
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

        public double GAPMgRed_Monto { get; set; }
        public double GAPMgRed_Porc { get; set; }

        public int? IdUsuario { get; set; }
        public DateTime? FechaAlta { get; set; }
        public DateTime? FechaUltMod { get; set; }

        public int? Mes { get; set; }
        public int? Año { get; set; }
        public DateTime? FechaInicial { get; set; }
        public DateTime? FechaFinal { get; set; }
        public string NomEstatus { get; set; }
        public int? Id_TipoReporte { get; set; }
        public double POvsPV { get; set; }
        public string TipoCuenta { get; set; }


        public ReporteGAP()
        {
            this.Id_Emp = null;
            this.Id_Cd = null;
            this.NomCDI = null;
            this.Id_Zona = null;
            this.NomZona = null;
            this.IdReporteGAP = null;
            this.Id_Rik = null;
            this.Nombre_Rik = null;
            this.Activo = 0;
            this.Id_Cte = null;
            this.Cte_NomComercial = null;
            this.Id_Categoria = 0;
            this.NomCategoria = null;
            this.Id_Prd = 0;
            this.NomProducto = null;
            this.Unidades = 0;
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
        }

        public ReporteGAP(

        int? Id_Emp,
        int? Id_Cd,
        string NomCDI,
        int? Id_Zona,
        string NomZona,
        int? IdReporteGAP,
        int? Id_Rik,
        string Nombre_Rik,
        int Activo,
        int? Id_Cte,
        string Cte_NomComercial,
        int Id_Categoria,
        string NomCategoria,
        Int64 Id_Prd,
        string NomProducto,
        int Unidades,
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
        string TipoCuenta
        )

        {
            this.Id_Emp = null;
            this.Id_Cd = null;
            this.NomCDI = null;
            this.Id_Zona = null;
            this.NomZona = null;
            this.IdReporteGAP = null;
            this.Id_Rik = null;
            this.Nombre_Rik = null;
            this.Activo = 0;
            this.Id_Cte = null;
            this.Cte_NomComercial = null;
            this.Id_Categoria = 0;
            this.NomCategoria = null;
            this.Id_Prd = 0;
            this.NomProducto = null;
            this.Unidades = 0;
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
        }
    }
}