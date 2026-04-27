using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{

    public class ReporteComisiones
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
        private int _id;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }


        private int _imprimir;

        public int Imprimir
        {
            get { return _imprimir; }
            set { _imprimir = value; }
        }


        private int _id_TipoRepresentante;
        public int Id_TipoRepresentante
        {
            get { return _id_TipoRepresentante; }
            set { _id_TipoRepresentante = value; }
        }


        private string _rik_Descripcion;

        public string Rik_Descripcion
        {
            get { return _rik_Descripcion; }
            set { _rik_Descripcion = value; }
        }


        private string _Nombre_Empleado;

        public string Nombre_Empleado
        {
            get { return _Nombre_Empleado; }
            set { _Nombre_Empleado = value; }
        }


        private string _CdiNombre;

        public string CdiNombre
        {
            get { return _CdiNombre; }
            set { _CdiNombre = value; }
        }

        private int _estatus;
        public int Estatus
        {
            get { return _estatus; }
            set { _estatus = value; }
        }



        private double _VtaCob;
        public double VtaCob
        {
            get { return _VtaCob; }
            set { _VtaCob = value; }
        }


        private double _UP;
        public double UP
        {
            get { return _UP; }
            set { _UP = value; }
        }

        private double _UB;
        public double UB
        {
            get { return _UB; }
            set { _UB = value; }
        }


        private double _CP;
        public double CP
        {
            get { return _CP; }
            set { _CP = value; }
        }

        private double _CND;
        public double CND
        {
            get { return _CND; }
            set { _CND = value; }
        }


        private double _MVI;
        public double MVI
        {
            get { return _MVI; }
            set { _MVI = value; }
        }

        private double _ComBaseAjustada;
        public double ComBaseAjustada
        {
            get { return _ComBaseAjustada; }
            set { _ComBaseAjustada = value; }
        }

        private double _GtoAdmin;
        public double GtoAdmin
        {
            get { return _GtoAdmin; }
            set { _GtoAdmin = value; }
        }

        private double _ComisionNeta;
        public double ComisionNeta
        {
            get { return _ComisionNeta; }
            set { _ComisionNeta = value; }
        }

        private double _Amortizacion;
        public double Amortizacion
        {
            get { return _Amortizacion; }
            set { _Amortizacion = value; }
        }

        private double _TecSer;
        public double TecSer
        {
            get { return _TecSer; }
            set { _TecSer = value; }
        }

        private int _UTrem;
        public int UTrem
        {
            get { return _UTrem; }
            set { _UTrem = value; }
        }

        private double _GST;
        public double GST
        {
            get { return _GST; }
            set { _GST = value; }
        }

        private double _PPPA;
        public double PPPA
        {
            get { return _PPPA; }
            set { _PPPA = value; }
        }

        private double _FR;
        public double FR
        {
            get { return _FR; }
            set { _FR = value; }
        }
        private double _CB;
        public double CB
        {
            get { return _CB; }
            set { _CB = value; }
        }

        private int _Id_Cte;
        public int Id_Cte
        {
            get { return _Id_Cte; }
            set { _Id_Cte = value; }
        }

        private string _Pag_referencia;

        public string Pag_referencia
        {
            get { return _Pag_referencia; }
            set { _Pag_referencia = value; }
        }

        private int _Id_Territorio;
        public int Id_Territorio
        {
            get { return _Id_Territorio; }
            set { _Id_Territorio = value; }
        }


        private int _Dias;
        public int Dias
        {
            get { return _Dias; }
            set { _Dias = value; }
        }

        private double _Importe;
        public double Importe
        {
            get { return _Importe; }
            set { _Importe = value; }
        }

        private double _Mult_Porc;
        public double Mult_Porc
        {
            get { return _Mult_Porc; }
            set { _Mult_Porc = value; }
        }

        private double _AjCobranza;
        public double AjCobranza
        {
            get { return _AjCobranza; }
            set { _AjCobranza = value; }
        }

        private DateTime _FechaVencimiento;
        public DateTime FechaVencimiento
        {
            get { return _FechaVencimiento; }
            set { _FechaVencimiento = value; }
        }

        private DateTime _FechaPago;
        public DateTime FechaPago
        {
            get { return _FechaPago; }
            set { _FechaPago = value; }
        }

        private int _Año;
        public int Año
        {
            get { return _Año; }
            set { _Año = value; }
        }

        private int _Mes;
        public int Mes
        {
            get { return _Mes; }
            set { _Mes = value; }
        }

        //JFCV 08jul2019 agregue el id del producto de tipo BigInt
        private Int64 _id_prd;
        public Int64 Id_Prd
        {
            get { return _id_prd; }
            set { _id_prd = value; }
        }

    }
}
