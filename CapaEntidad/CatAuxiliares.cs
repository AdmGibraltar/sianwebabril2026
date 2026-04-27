using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class CatAuxiliares
    {
        int _Id_Emp;
        int _Id_Cd;
        int _Id_Aux;

        bool _Estatus;
        string _EstatusStr;
        string _Aux_Nombre;


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
        public int Id_Aux
        {
            get { return _Id_Aux; }
            set { _Id_Aux = value; }
        }
        public string Aux_Nombre
        {
            get { return _Aux_Nombre; }
            set { _Aux_Nombre = value; }
        }

        public bool Estatus
        {
            get { return _Estatus; }
            set { _Estatus = value; }
        }
        public string EstatusStr
        {
            get { return _EstatusStr; }
            set { _EstatusStr = value; }
        }

    }
}
