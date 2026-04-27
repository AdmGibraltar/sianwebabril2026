using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
	public class ReporteRemisiones
	{
		public int Id_Emp { get; set; }
		public int Id_Cd { get; set; }

		public int Id_Cte { get; set; }
		public int Vencido { get; set; }
		public DateTime FechaIni { get; set; }
		public DateTime FechaFin { get; set; }
		public int TipoRemision { get; set; }

		public string nombreMatriz { get; set; }
		public int id_matriz { get; set; }
		public int Id_Ter { get; set; }
		public string Ter_Nombre { get; set; }
		public int id_cd { get; set; }

		public string Cte_NomComercial { get; set; }
		public int Id_Rem { get; set; }
		public DateTime Rem_Fecha { get; set; }
		public Int64 Id_Prd { get; set; }
		public string Prd_Descripcion { get; set; }
		public int Rem_Cant { get; set; }
		public int vencido { get; set; }
		public int Rem_Dev { get; set; }
		public double Prd_Pesos { get; set; }
		public double SaldoUnidades { get; set; }
		public int Folio { get; set; }
	}
}