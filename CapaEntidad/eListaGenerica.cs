using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{

    // ENE21-2020 RFH Updated

    public class eListaGenerica
    {

        private int _Id;
        public int Id
        {
            get { return _Id; }
            set { _Id = value; }
        }

        private string _IdCodigo;
        public string IdCodigo
        {
            get { return _IdCodigo; }
            set { _IdCodigo = value; }
        }

        private string _Descripcion;
        public string Descripcion
        {
            get { return _Descripcion; }
            set { _Descripcion = value; }
        }

        //RBM MARZO 2024
        // Tipo de Cliente L, N CC
        private string _TipoCliente;
        public string TipoCliente
        {
            get { return _TipoCliente; }
            set { _TipoCliente = value; }
        }
    }
}