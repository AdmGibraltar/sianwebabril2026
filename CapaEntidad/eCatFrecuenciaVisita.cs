using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// 29 Nov 2018 RFH

namespace CapaEntidad
{
    public class eCatFrecuenciaVisita
    {
        private int _Id_Frecuencia;
        public int Id_Frecuencia
        {
            get { return _Id_Frecuencia; } set { _Id_Frecuencia = value; }
        }

        private string _Frecuencia;
        public string Frecuencia
        {
            get { return _Frecuencia; }
            set { _Frecuencia = value; }
        }
        //
    }
}
