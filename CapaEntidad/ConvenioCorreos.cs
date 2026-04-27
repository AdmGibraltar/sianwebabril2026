
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class ConvenioCorreos
    {


        private int _Id_Emp;
        private int _Id_Cd;
        private int _Id_rik;
        private int _Id_U;
        private int _Id_Ter;
        private int _Tipo_correo;
        private string _U_correo;
        private string _U_Nombre;
        private string _Cd_Nombre;
        private string _NombreTerritorio;
        private string _Mensaje;
        private string _Asunto;



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
        public int Id_rik
        {
            get { return _Id_rik; }
            set { _Id_rik = value; }
        }
        public int Id_U
        {
            get { return _Id_U; }
            set { _Id_U = value; }
        }
        public int Id_Ter
        {
            get { return _Id_Ter; }
            set { _Id_Ter = value; }
        }
        public int Tipo_correo
        {
            get { return _Tipo_correo; }
            set { _Tipo_correo = value; }
        }

        public string U_correo
        {
            get { return _U_correo; }
            set { _U_correo = value; }
        }
        public string U_Nombre
        {
            get { return _U_Nombre; }
            set { _U_Nombre = value; }
        }

        public string Cd_Nombre
        {
            get { return _Cd_Nombre; }
            set { _Cd_Nombre = value; }
        }

        public string NombreTerritorio
        {
            get { return _NombreTerritorio; }
            set { _NombreTerritorio = value; }
        }



        public string Mensaje
        {
            get { return _Mensaje; }
            set { _Mensaje = value; }
        }

        public string Asunto
        {
            get { return _Asunto; }
            set { _Asunto = value; }
        }


    }
}

