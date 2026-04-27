using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
	public class ClienteECommerce
	{
		public int Id_Emp { get; set; }
		public int Id_Cd { get; set; }
		public int Id_Usu { get; set; }
		public int Id_Cte { get; set; }
		public string NombreCliente { get; set; }
		public string RazonSocial { get; set; }
		public string Nombre { get; set; }
		public string Contrasena { get; set; }
		public string Estatus { get; set; }
		public string EstatusStr { get; set; }


		public int Credito { get; set; }
		public int limite { get; set; }

		public int Uen { get; set; }
		public int id_Seg { get; set; }


		public string Cte_FacRfc { get; set; }
		public string Cte_Telefono { get; set; }
		public string calle { get; set; }
		public string Cte_Numero { get; set; }
		public string Cte_Municipio { get; set; }
		public string Cte_Estado { get; set; }
		public int Cte_CP { get; set; }
		public string DirEntregacte_calle { get; set; }
		public string DirEntregacte_numero { get; set; }
		public int DirEntregacte_Cp { get; set; }
		public string DirEntregaCte_colonia { get; set; }
		public string DirEntregaCte_municipio { get; set; }
		public string DirEntregaCte_Estado { get; set; }
		public string DirEntregacte_telefono { get; set; }
		public string nombre { get; set; }
		public string nombre2 { get; set; }
		public string nombre3 { get; set; }
		public string rik1 { get; set; }
		public string rik2 { get; set; }
		public string rik3 { get; set; }

		public string NombreUsu { get; set; }
		public string Apellido { get; set; }
		public string NombreCompleto { get; set; }
	}
}