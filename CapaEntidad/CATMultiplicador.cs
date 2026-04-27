using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
	public class CatMultiplicador
	{
		public int Id_Emp { get; set; }
		public int Id_Cd { get; set; }
		public string Sucursal { get; set; }
		public int Id_Rik { get; set; }
		public string NombreRik { get; set; }
		public int Mes { get; set; }
		public string Nombre_Mes { get; set; }
		public int Anio { get; set; }
		public string Multiplicador { get; set; }
		public decimal TotalMultiplicador { get; set; }
		public decimal TotalPresupuesto { get; set; }
		public double totalVenta { get; set; }
		public int permiso { get; set; }
		public DateTime FechaInicial { get; set; }
		public DateTime FechaFinal { get; set; }

		public double UtilidadPrima { get; set; }
		public double UtilidadPrimaPorc { get; set; }

		public double VNR { get; set; }
		public double VNRPorc { get; set; }
		public double VNRpres { get; set; }
		public double UtilidadPrimaPres { get; set; }
		public double UtilidadPrimaPresPorc { get; set; }

		public string matriz { get; set; }
		public int id_cte { get; set; }
		public string nombrecomercial { get; set; }
		public string territorio { get; set; }

		public string Cuenta { get; set; }

		public double difVta { get; set; }
		public double difup { get; set; }
		public double difpercup { get; set; }
	}
}