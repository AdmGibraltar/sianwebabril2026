using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class CategoriaCliente
    {
        private int _Id_CategoriaCliente;
        private int _Id_Emp;
        private int _Id_Cd;
        string _CategoCliente;
        string _DescCategoriaCliente;
        private int _RangoDesde;
        private int _RangoHasta;
        bool _Activo;
        private int _TieneDesde;
        private int _TieneHasta;

        public int Id_CategoriaCliente
        {
            get { return _Id_CategoriaCliente; }
            set { _Id_CategoriaCliente = value; }
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

        public string CategoCliente
        {
            get { return _CategoCliente; }
            set { _CategoCliente = value; }
        }

        public string DescCategoriaCliente
        {
            get { return _DescCategoriaCliente; }
            set { _DescCategoriaCliente = value; }
        }

        public int RangoHasta
        {
            get { return _RangoHasta; }
            set { _RangoHasta = value; }
        }

        public int RangoDesde
        {
            get { return _RangoDesde; }
            set { _RangoDesde = value; }
        }

        public int TieneHasta
        {
            get { return _TieneHasta; }
            set { _TieneHasta = value; }
        }

        public int TieneDesde
        {
            get { return _TieneDesde; }
            set { _TieneDesde = value; }
        }

        public bool Activo
        {
            get { return _Activo; }
            set { _Activo = value; }
        }


    }
}
