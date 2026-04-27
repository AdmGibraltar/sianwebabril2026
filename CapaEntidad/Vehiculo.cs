using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
	public class Vehiculo
	{
		public int Folio { get; set; }
		public int Id_Emp { get; set; }
		public int Id_Cd { get; set; }
		public string Id_ConfigVehiculo { get; set; }
		public string ConfiguracionVehiculo { get; set; }
		public string PlacaVehiculo { get; set; }
		public string ModeloVehiculo { get; set; }
		public string TipoRemolque { get; set; }
		public string PlacaRemolque { get; set; }
		public string PermisoSCT { get; set; }
		public string NumPermiso { get; set; }
		public string Poliza { get; set; }
		public string Aseguradora { get; set; }
		public string Nom_Vehiculo { get; set; }
		public string IdPermisoSCT { get; set; }
	}
}