using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class HistorialPedidos
    {
        public string Filtro_FechaInicial { get; set; }
        public string Filtro_FechaFinal { get; set; }
        public string Filtro_strPedido { get; set; }
        public string Filtro_Estatus { get; set; }

        public int Id_emp { get; set; }
        public int Id_Cd { get; set; }
        public int Id_Cte { get; set; }
        public int Id_Ped { get; set; }
        public int Id_Fac { get; set; }
        public int Orden { get; set; }
        public int IdProdcuto { get; set; }

        public string NomCte { get; set; }
        public string RSC { get; set; }
        public string TipoFecha { get; set; }

        public double porcentaje { get; set; }

        public DateTime Fecha { get; set; }
    }
}