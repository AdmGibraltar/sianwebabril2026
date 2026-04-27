using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    [Serializable]
    public class SistemaCompensacionGetXML
    {
        private int _id_Emp;

        public int Id_Emp
        {
            get { return _id_Emp; }
            set { _id_Emp = value; }
        }
        private int _id_Cd;

        public int Id_Cd
        {
            get { return _id_Cd; }
            set { _id_Cd = value; }
        }

        private int _Anio;

        public int Anio
        {
            get { return _Anio; }
            set { _Anio = value; }
        }

        private int _Mes;

        public int Mes
        {
            get { return _Mes; }
            set { _Mes = value; }
        }


        private int _Id_Rik;

        public int Id_Rik
        {
            get { return _Id_Rik; }
            set { _Id_Rik = value; }
        }


        private int _id_Sistema;

        public int Id_Sistema
        {
            get { return _id_Sistema; }
            set { _id_Sistema = value; }
        }


        private string _Parametros;

        public string Parametros
        {
            get { return _Parametros; }
            set { _Parametros = value; }
        }

        private string _Clientes;

        public string Clientes
        {
            get { return _Clientes; }
            set { _Clientes = value; }
        }

        private string _Conceptos;

        public string Conceptos
        {
            get { return _Conceptos; }
            set { _Conceptos = value; }
        }

        private string _RikNombre;

        public string RikNombre
        {
            get { return _RikNombre; }
            set { _RikNombre = value; }
        }

        private string _CdiNombre;

        public string CdiNombre
        {
            get { return _CdiNombre; }
            set { _CdiNombre = value; }
        }


        private string _MesTexto;

        public string MesTexto
        {
            get { return _MesTexto; }
            set { _MesTexto = value; }
        }


        private int _Id_Representante;

        public int Id_Representante
        {
            get { return _Id_Representante; }
            set { _Id_Representante = value; }
        }

        private int _Id_TipoRepresentante;

        public int Id_TipoRepresentante
        {
            get { return _Id_TipoRepresentante; }
            set { _Id_TipoRepresentante = value; }
        }

        ///
        /// El tipo de CDI es 3 para Franquicias y cualquier otro valor para CDI
        private int _Id_TipoCDI;

        public int Id_TipoCDI
        {
            get { return _Id_TipoCDI; }
            set { _Id_TipoCDI = value; }
        }


    }
}