using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class ClienteDetRecibe
    {
        // Cte_Recibe Cte_HorarioEntrega Cte_DiaRecepcion Cte_AreaRecibo Cte_DocParte1 Cte_DocParte2
        private int _id_Emp;
        private int _id_Cd;
        private int _id_Cte;
        private int? _Id_Ter;
        private string _Cte_Recibe;
        private string _Cte_HorarioEntrega;
        private string _Cte_DiaRecepcion;
        private string _Cte_AreaRecibo;
        private string _Cte_DocAdicional;
        private string _Cte_Pago;
        private string _Cte_Revicion;
        private string _Cte_RevisionDocAdicional;
        private string _Cte_PagoPresencial;


        public int Id_Emp
        {
            get { return _id_Emp; }
            set { _id_Emp = value; }
        }
        public int Id_Cd
        {
            get { return _id_Cd; }
            set { _id_Cd = value; }
        }
        public int Id_Cte
        {
            get { return _id_Cte; }
            set { _id_Cte = value; }
        }

        public int? Id_Ter
        {
            get { return _Id_Ter; }
            set { _Id_Ter = value; }
        }


        public string Cte_Recibe
        {
            get { return _Cte_Recibe; }
            set { _Cte_Recibe = value; }
        }
        public string Cte_HorarioEntrega
        {
            get { return _Cte_HorarioEntrega; }
            set { _Cte_HorarioEntrega = value; }
        }
        public string Cte_DiaRecepcion
        {
            get { return _Cte_DiaRecepcion; }
            set { _Cte_DiaRecepcion = value; }
        }
        public string Cte_AreaRecibo
        {
            get { return _Cte_AreaRecibo; }
            set { _Cte_AreaRecibo = value; }
        }
        public string Cte_DocAdicional
        {
            get { return _Cte_DocAdicional; }
            set { _Cte_DocAdicional = value; }
        }

        public string Cte_Pago
        {
            get { return _Cte_Pago; }
            set { _Cte_Pago = value; }
        }

        public string Cte_Revicion
        {
            get { return _Cte_Revicion; }
            set { _Cte_Revicion = value; }
        }

        public string Cte_RevisionDocAdicional
        {
            get { return _Cte_RevisionDocAdicional; }
            set { _Cte_RevisionDocAdicional = value; }
        }

        public string Cte_PagoPresencial
        {
            get { return _Cte_PagoPresencial; }
            set { _Cte_PagoPresencial = value; }
        }


    }
}