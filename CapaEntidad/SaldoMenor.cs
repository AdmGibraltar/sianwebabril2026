using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class SaldoMenor
    {
        public int Id_Emp { get; set; }
        public int Id_Cd { get; set; }
        public int Id_Doc { get; set; }
        public string Doc_tipo { get; set; }
        public string Doc { get; set; }
        public DateTime Fecha_SaldoMenor { get; set; }
        public DateTime Fecha_documento { get; set; }

        public int Id_Cte { get; set; }
        public string Cte_Nombre { get; set; }

        public double Importe { get; set; }
        public double Saldo { get; set; }
        public double Saldo_Menor { get; set; }

        public DateTime fechainicial { get; set; }
        public DateTime fechaFinal { get; set; }
    }
}