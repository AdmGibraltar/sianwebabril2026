using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class DiasInhabiles
    {
        private int _IdDiaInhabil;
        private int _Id_Emp;
        private int _Id_Cd;
        private int _Año;
        private int _Secuencia;
        DateTime _Fecha;
        bool _Activo;

        public int IdDiaInhabil
        {
            get { return _IdDiaInhabil; }
            set { _IdDiaInhabil = value; }
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

        public int Año
        {
            get { return _Año; }
            set { _Año = value; }
        }

        public int Secuencia
        {
            get { return _Secuencia; }
            set { _Secuencia = value; }
        }

        public DateTime Fecha
        {
            get { return _Fecha; }
            set { _Fecha = value; }
        }

        public bool Activo
        {
            get { return _Activo; }
            set { _Activo = value; }
        }


    }
}
