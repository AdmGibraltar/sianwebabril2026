using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class eCRM_ValuacionCamposAdicionales
    {
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

        private string _Id_Op;
        public string Id_Op
        {
            get { return _Id_Op; }
            set { _Id_Op = value; }
        }

        private string _NombreAtencion;
        public string NombreAtencion
        {
            get { return _NombreAtencion; }
            set { _NombreAtencion = value; }
        }

        private string _RepresentanteClienteNombre;
        public string RepresentanteClienteNombre
        {
            get { return _RepresentanteClienteNombre; }
            set { _RepresentanteClienteNombre = value; }
        }

        private string _NombreRik;
        public string NombreRik
        {
            get { return _NombreRik; }
            set { _NombreRik = value; }
        }

        private string _Direccion1;
        public string Direccion1
        {
            get { return _Direccion1; }
            set { _Direccion1 = value; }
        }

        private string _Direccion2;
        public string Direccion2
        {
            get { return _Direccion2; }
            set { _Direccion2 = value; }
        }

        private string _CP;
        public string CP
        {
            get { return _CP; }
            set { _CP = value; }
        }

        private string _NoExterior;
        public string NoExterior
        {
            get { return _NoExterior; }
            set { _NoExterior = value; }
        }

        private string _Telefono;
        public string Telefono
        {
            get { return _Telefono; }
            set { _Telefono = value; }
        }

        private int _DiasCredito;
        public int DiasCredito
        {
            get { return _DiasCredito; }
            set { _DiasCredito = value; }
        }

    }
}