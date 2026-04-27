using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class PreRequisitos
    {
        private int _Id_PreRequi;
        private int _Id_Emp;
        private int _Id_Cd;
        string _PreRequisito;
        bool _Activo;

        public int Id_PreRequi
        {
            get { return _Id_PreRequi; }
            set { _Id_PreRequi = value; }
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

        public string PreRequisito
        {
            get { return _PreRequisito; }
            set { _PreRequisito = value; }
        }

        public bool Activo
        {
            get { return _Activo; }
            set { _Activo = value; }
        }


    }
}
