using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class CN_Matriz
    {
        public int id_emp { get; set; }
        public int id_Cd { get; set; }
        public int id { get; set; }
        public int id_matriz { get; set; }
        public string nombre { get; set; }

        public string nombrenodo { get; set; }
        public int id_Cte { get; set; }
        public string nombreCliente { get; set; }
        public string NombreMatriz { get; set; }
        public int id_acys { get; set; }
        public string AcysNombre { get; set; }
        public int IdEstatus { get; set; }
        public string nombreEstatus { get; set; }

        public bool mov80 { get; set; }

        public string correo { get; set; }
        public int cdik { get; set; }
        public string telefono { get; set; }
        public int rol_auditorias { get; set; }
        public string contrasenia { get; set; }
        public int rol_ecommerce { get; set; }



        public int clienteSian { get; set; }
        public string razonsocial { get; set; }
        public string territorio { get; set; }
        public int sucursal { get; set; }
        public int asesorid { get; set; }
        public int estatus { get; set; }
        public string usuario { get; set; }
        public string comentarios { get; set; }
        public string calle { get; set; }
        public string numinterior { get; set; }
        public string numExterior { get; set; }
        public string colonia { get; set; }
        public string municipio { get; set; }
        public string Cp { get; set; }
        public string estado { get; set; }
        public string RFC { get; set; }
        public string telefonos { get; set; }
        public string fax { get; set; }
        public int id_Estructura { get; set; }
        public bool Desvinc { get; set; }

        public DateTime Fecha { get; set; }
        public int Rem_Cta_Nac { get; set; }
    }
}