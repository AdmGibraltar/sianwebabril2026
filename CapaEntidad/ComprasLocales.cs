using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class ComprasLocales
    {
        private int _Id_Cd;
        public int Id_Cd
        {
            get { return _Id_Cd; }
            set { _Id_Cd = value; }
        }

        private string _Cd_Nombre;
        public string Cd_Nombre
        {
            get { return _Cd_Nombre; }
            set { _Cd_Nombre = value; }
        }

        private int _Comp_Solicito;
        public int Comp_Solicito
        {
            get { return _Comp_Solicito; }
            set { _Comp_Solicito = value; }
        }

        private int _Id_Comp;
        public int Id_Comp
        {
            get { return _Id_Comp; }
            set { _Id_Comp = value; }
        }

        private string _Comp_FechaSol;
        public string Comp_FechaSol
        {
            get { return _Comp_FechaSol; }
            set { _Comp_FechaSol = value; }
        }

        private string _Det_Estatus;
        public string Det_Estatus
        {
            get { return _Det_Estatus; }
            set { _Det_Estatus = value; }
        }

        private string _Det_FecAut;
        public string Det_FecAut
        {
            get { return _Det_FecAut; }
            set { _Det_FecAut = value; }
        }

        private string _U_Nombre;
        public string U_Nombre
        {
            get { return _U_Nombre; }
            set { _U_Nombre = value; }
        }

        private string _U_Correo;
        public string U_Correo
        {
            get { return _U_Correo; }
            set { _U_Correo = value; }
        }

        private string _Estatus;
        public string Estatus
        {
            get { return _Estatus; }
            set { _Estatus = value; }
        }

        private string _Comentarios;
        public string Comentarios
        {
            get { return _Comentarios; }
            set { _Comentarios = value; }
        }

        private string _Vigencia;
        public string Vigencia
        {
            get { return _Vigencia; }
            set { _Vigencia = value; }
        }

        private int _IdTipoSolicitud;
        public int IdTipoSolicitud
        {
            get { return _IdTipoSolicitud; }
            set { _IdTipoSolicitud = value; }
        }

        private string _TipoSolicitud;
        public string TipoSolicitud
        {
            get { return _TipoSolicitud; }
            set { _TipoSolicitud = value; }
        }

        private int _Autorizados;
        public int Autorizados
        {
            get { return _Autorizados; }
            set { _Autorizados = value; }
        }

        private int _Totales;
        public int Totales
        {
            get { return _Totales; }
            set { _Totales = value; }
        }


        //

    }
}