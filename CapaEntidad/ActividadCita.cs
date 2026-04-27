using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class ActividadCita
    {
        private int _Id_TipoVisita;
        private int _Id_Emp;
        private int _Id_Cd;
        string _TipoVisita;
        string _Complemento;
        string _ColorFondo;
        string _Icono;
        bool _Activo;

        public int Id_TipoVisita
        {
            get { return _Id_TipoVisita; }
            set { _Id_TipoVisita = value; }
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

        public string TipoVisita
        {
            get { return _TipoVisita; }
            set { _TipoVisita = value; }
        }

        public string Complemento
        {
            get { return _Complemento; }
            set { _Complemento = value; }
        }

        public string Icono
        {
            get { return _Icono; }
            set { _Icono = value; }
        }

        public string ColorFondo
        {
            get { return _ColorFondo; }
            set { _ColorFondo = value; }
        }

        public bool Activo
        {
            get { return _Activo; }
            set { _Activo = value; }
        }


    }
}
