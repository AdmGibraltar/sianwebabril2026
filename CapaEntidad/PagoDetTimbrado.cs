using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    [Serializable]
    public class PagoDetTimbrado
    {
        private int _Id_Emp;
        private int _Id_Cd;
        private int _Id_Pag;
        private int _Id_Cte;
        private int _Id_PagComp;
        private int _Id_U;
        private int _Id_PagDet;
        private int? _Id_PagDetTimb;
        private int? _Cte_Fpago;
        private string _Cte_UsoCFDI;
        private string _Pago_FolioFiscal;
        private object _Pago_Xml;
        private object _Pago_Pdf;
        private string _Pago_Serie;
        private object _Cancelacion_XML;
        private double _Total_Pagado_Timbrado;
        private int _Parcialidad_Timbrada;
        private string _Pag_Referencia;
        public DateTime? _FechaCreacionXML;
        public DateTime? _fechaCancelacion;
        public DateTime _Pag_Fecha;

        public DateTime Pag_Fecha
        {
            get { return _Pag_Fecha; }
            set { _Pag_Fecha = value; }
        }


        public DateTime? FechaCreacionXML
        {
            get { return _FechaCreacionXML; }
            set { _FechaCreacionXML = value; }
        }

        public DateTime? FechaCancelacion
        {
            get { return _fechaCancelacion; }
            set { _fechaCancelacion = value; }
        }


        public int Id_PagDet
        {
            get { return _Id_PagDet; }
            set { _Id_PagDet = value; }
        }


        public int Id_U
        {
            get { return _Id_U; }
            set { _Id_U = value; }
        }

        public int? Id_PagDetTimb
        {
            get { return _Id_PagDetTimb; }
            set { _Id_PagDetTimb = value; }
        }

        public string Pago_Serie
        {
            get { return _Pago_Serie; }
            set { _Pago_Serie = value; }
        }

        public object Pago_Pdf
        {
            get { return _Pago_Pdf; }
            set { _Pago_Pdf = value; }
        }
        public object Pago_Xml
        {
            get { return _Pago_Xml; }
            set { _Pago_Xml = value; }
        }
        public string Pago_FolioFiscal
        {
            get { return _Pago_FolioFiscal; }
            set { _Pago_FolioFiscal = value; }
        }
        public int Id_Pag
        {
            get { return _Id_Pag; }
            set { _Id_Pag = value; }
        }
        public int Id_Emp
        {
            get { return _Id_Emp; }
            set { _Id_Emp = value; }
        }
        public int Id_Cte
        {
            get { return _Id_Cte; }
            set { _Id_Cte = value; }
        }
        public int Id_PagComp
        {
            get { return _Id_PagComp; }
            set { _Id_PagComp = value; }
        }
        public int Id_Cd
        {
            get { return _Id_Cd; }
            set { _Id_Cd = value; }
        }
        public string Cte_UsoCFDI
        {
            get { return _Cte_UsoCFDI; }
            set { _Cte_UsoCFDI = value; }
        }
        public int? Cte_Fpago
        {
            get { return _Cte_Fpago; }
            set { _Cte_Fpago = value; }
        }
        public object Cancelacion_XML
        {
            get { return _Cancelacion_XML; }
            set { _Cancelacion_XML = value; }
        }
        public double Total_Pagado_Timbrado
        {
            get { return _Total_Pagado_Timbrado; }
            set { _Total_Pagado_Timbrado = value; }
        }
        public int Parcialidad_Timbrada
        {
            get { return _Parcialidad_Timbrada; }
            set { _Parcialidad_Timbrada = value; }
        }
        public string pag_referencia
        {
            get { return _Pag_Referencia; }
            set { _Pag_Referencia = value; }
        }
    }
}
