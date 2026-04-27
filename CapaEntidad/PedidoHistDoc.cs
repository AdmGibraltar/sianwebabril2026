using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class PedidoHistDoc
    {
        public int Id_Emp { get; set; }
        public int Id_Cd { get; set; }
        public int cantidadRegistros { get; set; }

        public int mes { get; set; }
        public DateTime FechaRegistro { get; set; }
        public DateTime FechaInicial { get; set; }
        public DateTime FechaFinal { get; set; }
        public int SemanaRegistro { get; set; }
        public int Id_registro { get; set; }
        public byte[] DocExcel { get; set; }

        public int Id_U { get; set; }
        public int CantVtaNuExp { get; set; }
    }
}