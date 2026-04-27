using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
	public class CatPresupuesto
	{
		public int Id_Emp { get; set; }
		public int Id_Cd { get; set; }
		public string Sucursal { get; set; }
		public int Id_Rik { get; set; }
		public string NombreRik { get; set; }
		public int Mes { get; set; }
		public int Mesfinal { get; set; }
		public string Nombre_Mes { get; set; }
		public int Anio { get; set; }
		public int Aniofinal { get; set; }
		public double Presupuesto { get; set; }
		public string Presupuestostr { get; set; }
		public Decimal TotalPresupuesto { get; set; }
		public int Id_u { get; set; }
		public DateTime FechaInicial { get; set; }
		public DateTime fechafinal { get; set; }
		public DateTime fecha { get; set; }
		public int permiso { get; set; }

		public int MesInicial { get; set; }
		public int AnioInicial { get; set; }
		public int MesFinal { get; set; }
		public int AnioFinal { get; set; }

		public int id_ter { get; set; }
		public string ter_nombre { get; set; }
		public Double cantidad { get; set; }
		public Double PrecioDistribuidor { get; set; }
		public Double PrecioLista { get; set; }
		public Double PrecioVenta { get; set; }
		public Double venta { get; set; }
		public Double Costo { get; set; }
		public Double utilidadBruta { get; set; }
		public Double porcubreal { get; set; }
		public Double porcubplaneada { get; set; }
		public Double varianzaubrutapuntos { get; set; }
		public Double impactopesos { get; set; }
		public Double importecostopublico { get; set; }
		public string tot_clientes { get; set; }
		public string prom_cliente { get; set; }
		public int trimestre { get; set; }
		public int anioTrimestre { get; set; }
		public int vnr { get; set; }
		public double VNRPorc { get; set; }

		public int id_cte { get; set; }
		public string cte_nomcomercial { get; set; }
		public int id_matriz { get; set; }
		public string Matriz { get; set; }

		public Double AcysVenta { get; set; }
		public Double AcysUP { get; set; }
		public Double AcysUPProc { get; set; }
		public Double UtilidadPrima { get; set; }
		public Double VNR { get; set; }
		public Double VNRPor { get; set; }
		public Double Mezcla { get; set; }
		public Double MezclaUP { get; set; }
		public Double MezclaAcys { get; set; }

		public long id_prd { get; set; }
		public string prd_nombre { get; set; }
		public int id_seg { get; set; }
		public string nom_segmento { get; set; }
		public int Id_Cpr { get; set; }
		public string Cpr_Descripcion { get; set; }
		public decimal UB { get; set; }
		public decimal cargavnr { get; set; }

		public Double ventaNacional { get; set; }
		public Double utilidadBrutaNacional { get; set; }
		public Double porcubrealNacional { get; set; }
		public Double ventaLocal { get; set; }
		public Double utilidadBrutaLocal { get; set; }
		public Double porcubrealLocal { get; set; }
		public Double MezclaNacional { get; set; }
		public Double MezclaUPNacional { get; set; }
		public Double MezclaLocal { get; set; }
		public Double MezclaUPLocal { get; set; }

		public string TipoCuenta { get; set; }
		public string Categoria { get; set; }
		public double utilidadPrima { get; set; }
		public double percutilidadPrima { get; set; }
		public decimal up { get; set; }
		public decimal pup { get; set; }

		public decimal PresupuestoventaNacional { get; set; }
		public decimal PresupuestoutilidadBrutaNacional { get; set; }
		public decimal PresupuestoporcubrealNacional { get; set; }
		public decimal PresupuestoventaLocal { get; set; }
		public decimal PresupuestoutilidadBrutaLocal { get; set; }
		public decimal PresupuestoporcubrealLocal { get; set; }
		public decimal Presupuestoventa { get; set; }
		public decimal PresupuestoutilidadBruta { get; set; }
		public decimal Presupuestoporcubreal { get; set; }
	}
}