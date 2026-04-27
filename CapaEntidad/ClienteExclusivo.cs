using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class ClienteExclusivo
    {
        private int _Id_Cte;
        private string _Nombre;
        private string _TipoCliente;

        public int Id_Cte { get => _Id_Cte; set => _Id_Cte = value; }
        public string Nombre { get => _Nombre; set => _Nombre = value; }
        public string TipoCliente { get => _TipoCliente; set => _TipoCliente = value; }
    }
}