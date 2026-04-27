using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class ProPrecioConv_Usuarios
    {


        private int _Id_Emp;
        private int _Id_Cd;
        private int _Id_Conusu;
        private int _Id_Usu;
        private int _Tipo_usuario;
        private string _Usu_correo;
        private string _U_Nombre;



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
        public int Id_Conusu
        {
            get { return _Id_Conusu; }
            set { _Id_Conusu = value; }
        }
        public int Id_Usu
        {
            get { return _Id_Usu; }
            set { _Id_Usu = value; }
        }
        public int Tipo_usuario
        {
            get { return _Tipo_usuario; }
            set { _Tipo_usuario = value; }
        }
        public string Usu_correo
        {
            get { return _Usu_correo; }
            set { _Usu_correo = value; }
        }
        public string U_Nombre
        {
            get { return _U_Nombre; }
            set { _U_Nombre = value; }
        }


    }
}
