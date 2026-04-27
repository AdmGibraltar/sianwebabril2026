using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//
// 9JUL-2019 RFH
// MAPEA CAPACYSLOGS
// 

namespace CapaEntidad
{
    public class eAcys2Logs
    {
        private int _IdCapAcysLogs;
        public int IdCapAcysLogs
        {
            get { return _IdCapAcysLogs; }
            set { _IdCapAcysLogs = value; }
        }

        private int _Id_Emp;
        public int Id_Emp
        {
            get { return _Id_Emp; }
            set { _Id_Emp = value; }
        }

        private int _Id_Cd;
        public int Id_Cd
        {
            get { return _Id_Cd; }
            set { _Id_Cd = value; }
        }

        private int _Id_Acs;
        public int Id_Acs
        {
            get { return _Id_Acs; }
            set { _Id_Acs = value; }
        }

        private string _Fecha;
        public string Fecha
        {
            get { return _Fecha; }
            set { _Fecha = value; }
        }

        private string _Nota;
        public string Nota
        {
            get { return _Nota; }
            set { _Nota = value; }
        }

        private int _Id_Usuario;
        public int Id_Usuario
        {
            get { return _Id_Usuario; }
            set { _Id_Usuario = value; }
        }

        //

    }
}