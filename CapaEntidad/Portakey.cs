using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Portakey
    {
        public int Id_Emp { get; set; }
        public int id_Cd { get; set; }
        public int id_pedido { get; set; }
        public int Id_Portal { get; set; }
        public int CantidadPedidoPortal { get; set; }
        public int CantidadPedidoTotal { get; set; }
        public int ClientePtes { get; set; }
        public int ClienteAlta { get; set; }
        public int ClienteTotal { get; set; }
        public int Territorio { get; set; }
        public int id_cte { get; set; }
        public int id_rik { get; set; }
        public int Id_Region { get; set; }
        public int id_CorreoUsuario { get; set; }
        public int Id_Usu { get; set; }
        public int Tipo { get; set; }
        public int Credito { get; set; }
        public int limite { get; set; }
        public int Uen { get; set; }
        public int id_Seg { get; set; }
        public int Cte_CP { get; set; }
        public int DirEntregacte_Cp { get; set; }
        public int Año { get; set; }
        public int Mes { get; set; }
        public int Id_Direccion { get; set; }
        public int intpermiso { get; set; }

        public string nombre { get; set; }
        public string nombreRik { get; set; }
        public string nombreCliente { get; set; }
        public string NombreMatriz { get; set; }
        public string Correo { get; set; }
        public string NombreTipo { get; set; }
        public string NombreRegion { get; set; }
        public string NombreCorreoUsuario { get; set; }
        public string CorreoUsuario { get; set; }
        public string descricpion { get; set; }
        public string Sucursal { get; set; }
        public string unidad { get; set; }
        public string Contrasena { get; set; }
        public string clave { get; set; }
        public string NombreCliente { get; set; }
        public string RazonSocial { get; set; }
        public string Nombre { get; set; }
        public string Estatus { get; set; }
        public string EstatusStr { get; set; }
        public string Cte_FacRfc { get; set; }
        public string Cte_Telefono { get; set; }
        public string calle { get; set; }
        public string Cte_Numero { get; set; }
        public string Cte_Municipio { get; set; }
        public string Cte_Estado { get; set; }
        public string DirEntregacte_calle { get; set; }
        public string DirEntregacte_numero { get; set; }
        public string DirEntregaCte_colonia { get; set; }
        public string DirEntregaCte_municipio { get; set; }
        public string DirEntregaCte_Estado { get; set; }
        public string DirEntregacte_telefono { get; set; }
        public string nombre2 { get; set; }
        public string nombre3 { get; set; }
        public string rik1 { get; set; }
        public string rik2 { get; set; }
        public string rik3 { get; set; }
        public string NombreMes { get; set; }
        public string Apellidos { get; set; }
        public string ApellidosCorreoUsuario { get; set; }
        public string NombreCompleto { get; set; }
        public string numero { get; set; }
        public string DireccionCompleta { get; set; }

        public DateTime fechainicio { get; set; }
        public DateTime fecchafinal { get; set; }

        public double PorcPortal { get; set; }
        public double facturacionPortal { get; set; }
        public double facturaciontotal { get; set; }
        public double porcfacturacionPortal { get; set; }
        public double PorcCliente { get; set; }
        public double Ene { get; set; }
        public double Feb { get; set; }
        public double Mar { get; set; }
        public double Abr { get; set; }
        public double May { get; set; }
        public double Jun { get; set; }
        public double Jul { get; set; }
        public double Ago { get; set; }
        public double Sep { get; set; }
        public double Oct { get; set; }
        public double Nov { get; set; }
        public double Dic { get; set; }
        public double Presupuesto { get; set; }

        public bool ClientedeAlta { get; set; }
        public bool permiso { get; set; }
    }
}