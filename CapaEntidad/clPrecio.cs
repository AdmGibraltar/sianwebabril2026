using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class clPrecio
    {
        private int _Id_Pre;
        private string _Prd_PreDescripcion;
        private decimal _Prd_Pesos;

        public int Id_Pre { get => _Id_Pre; set => _Id_Pre = value; }
        public string Prd_PreDescripcion { get => _Prd_PreDescripcion; set => _Prd_PreDescripcion = value; }
        public decimal Prd_Pesos { get => _Prd_Pesos; set => _Prd_Pesos = value; }
    }
}