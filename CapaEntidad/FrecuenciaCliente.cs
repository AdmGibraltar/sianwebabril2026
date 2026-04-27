using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class FrecuenciaCliente
    {
        private int _Id_FrecuenciaCliente;
        private int _Id_Emp;
        private int _Id_Cd;
        string _TipoIntervalo;
        string _Frecuencia;
        bool _Activo;
        private int _IntTipoIntervalo;
        private int _Intervalo;

        public int Id_FrecuenciaCliente
        {
            get { return _Id_FrecuenciaCliente; }
            set { _Id_FrecuenciaCliente = value; }
        }

        public int Id_Emp
        {
            get { return _Id_Emp; }
            set { _Id_Emp = value; }
        }

        public int Id_Cd
        {
            get { return _Id_Cd; }
            set { _Id_Cd = value; }
        }

        public string Frecuencia
        {
            get { return _Frecuencia; }
            set { _Frecuencia = value; }
        }

        public string TipoIntervalo
        {
            get { return _TipoIntervalo; }
            set { _TipoIntervalo = value; }
        }

        public int IntTipoIntervalo
        {
            get { return _IntTipoIntervalo; }
            set { _IntTipoIntervalo = value; }
        }

        public int Intervalo
        {
            get { return _Intervalo; }
            set { _Intervalo = value; }
        }

        public bool Activo
        {
            get { return _Activo; }
            set { _Activo = value; }
        }


    }
}
