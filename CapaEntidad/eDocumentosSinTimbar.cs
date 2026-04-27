using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    // ABR16-2020 RFH

    public class eDocumentosSinTimbar
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

        private int _DocumentosSinTimbar;
        public int DocumentosSinTimbar
        {
            get { return _DocumentosSinTimbar; }
            set { _DocumentosSinTimbar = value; }
        }

        private int _Count_Facturas;
        public int Count_Facturas
        {
            get { return _Count_Facturas; }
            set { _Count_Facturas = value; }
        }

        private int _Count_NotasCargo;
        public int Count_NotasCargo
        {
            get { return _Count_NotasCargo; }
            set { _Count_NotasCargo = value; }
        }

        private int _Count_NotasCredito;
        public int Count_NotasCredito
        {
            get { return _Count_NotasCredito; }
            set { _Count_NotasCredito = value; }
        }

        private int _Count_ComplementosPago;
        public int Count_ComplementosPago
        {
            get { return _Count_ComplementosPago; }
            set { _Count_ComplementosPago = value; }
        }

        //

    }
}