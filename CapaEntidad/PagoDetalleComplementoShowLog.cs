using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    [Serializable]
    public class PagoDetalleComplementoShowLog
    {
        private int _Id_Emp;
        private int _Id_Cd;
        private int _Id_Pag;
        private int _Id_Cte;
        private int _Id_ShowLog;
        private int _Id_PagDet;
        private int _Id_PagComp;
        private int _id_pagdettimb;
        private bool _Atendido;
        private string _Observaciones;
        private string _Pago_Serie;
        private string _Pago_FolioFiscal;
        private string _Pag_Referencia;
        public DateTime _Fecha;
        public DateTime _Filtro_FecFin;
        public DateTime _Filtro_FecIni;

        public DateTime Filtro_FecIni
        {
            get { return _Filtro_FecIni; }
            set { _Filtro_FecIni = value; }
        }

        public DateTime Filtro_FecFin
        {
            get { return _Filtro_FecFin; }
            set { _Filtro_FecFin = value; }
        }

        public string Pag_Referencia
        {
            get { return _Pag_Referencia; }
            set { _Pag_Referencia = value; }
        }

        public int id_pagdettimb
        {
            get { return _id_pagdettimb; }
            set { _id_pagdettimb = value; }
        }

        public string Pago_FolioFiscal
        {
            get { return _Pago_FolioFiscal; }
            set { _Pago_FolioFiscal = value; }
        }

        public string Pago_Serie
        {
            get { return _Pago_Serie; }
            set { _Pago_Serie = value; }
        }

        public int Id_PagComp
        {
            get { return _Id_PagComp; }
            set { _Id_PagComp = value; }
        }

        public bool Atendido
        {
            get { return _Atendido; }
            set { _Atendido = value; }
        }

        public string Observaciones
        {
            get { return _Observaciones; }
            set { _Observaciones = value; }
        }

        public DateTime Fecha
        {
            get { return _Fecha; }
            set { _Fecha = value; }
        }


        public int Id_PagDet
        {
            get { return _Id_PagDet; }
            set { _Id_PagDet = value; }
        }

        public int Id_ShowLog
        {
            get { return _Id_ShowLog; }
            set { _Id_ShowLog = value; }
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

        public int Id_Cd
        {
            get { return _Id_Cd; }
            set { _Id_Cd = value; }
        }
    }
}