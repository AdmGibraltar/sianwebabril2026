using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class eCL_ProductoPrecios
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

        private long _Id_Prd;
        public long Id_Prd
        {
            get { return _Id_Prd; }
            set { _Id_Prd = value; }
        }

        private int _Prd_Actual;
        public int Prd_Actual
        {
            get { return _Prd_Actual; }
            set { _Prd_Actual = value; }
        }

        private string _Prd_FechaInicio;
        public string Prd_FechaInicio
        {
            get { return _Prd_FechaInicio; }
            set { _Prd_FechaInicio = value; }
        }

        private string _Prd_FechaFin;
        public string Prd_FechaFin
        {
            get { return _Prd_FechaFin; }
            set { _Prd_FechaFin = value; }
        }

        private string _Prd_Descripcion;
        public string Prd_Descripcion
        {
            get { return _Prd_Descripcion; }
            set { _Prd_Descripcion = value; }
        }

        private double _Prd_Pesos;
        public double Prd_Pesos
        {
            get { return _Prd_Pesos; }
            set { _Prd_Pesos = value; }
        }

        //RBM MARZO 2024
        private int _Id_Pre;
        public int Id_Pre
        {
            get { return _Id_Pre; }
            set { _Id_Pre = value; }
        }

    }
}