using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
	public class Geolocalizacion
	{
		public int id_emp { get; set; }
		public int ID_Cd { get; set; }
		public int Id_usu { get; set; }
		public int Id_Agenda { get; set; }
		public decimal Longitud { get; set; }
		public decimal Latitud { get; set; }
		public DateTime FechaRegistro { get; set; }
		public string Estatus { get; set; }
		public string NombreCorto { get; set; }
		public int verificador { get; set; }
	}
}