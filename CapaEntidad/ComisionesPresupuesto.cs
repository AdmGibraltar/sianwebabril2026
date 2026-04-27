using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    [Serializable]
    public class ComisionesPresupuesto
    {



        private int _Id_Presupuesto;
        public int Id_Presupuesto
        {
            get { return _Id_Presupuesto; }
            set { _Id_Presupuesto = value; }
        }


        private int _Id_Emp;
        public int Id_Emp
        {
            get { return _Id_Emp; }
            set { _Id_Emp = value; }
        }

        private int _Num_Cdi;
        public int Num_Cdi
        {
            get { return _Num_Cdi; }
            set { _Num_Cdi = value; }
        }

        private int _Id_Cd;
        public int Id_Cd
        {
            get { return _Id_Cd; }
            set { _Id_Cd = value; }
        }


        private string _NomCdi;
        public string NomCdi
        {
            get { return _NomCdi; }
            set { _NomCdi = value; }
        }

        private int _NumNomina;
        public int NumNomina
        {
            get { return _NumNomina; }
            set { _NumNomina = value; }
        }


        private string _Nom_Empleado;
        public string Nom_Empleado
        {
            get { return _Nom_Empleado; }
            set { _Nom_Empleado = value; }
        }


        private int _Id_Rik;
        public int Id_Rik
        {
            get { return _Id_Rik; }
            set { _Id_Rik = value; }
        }

        private bool _Estatus;
        public bool Estatus
        {
            get { return _Estatus; }
            set { _Estatus = value; }
        }

        private int _Anio;
        public int Anio
        {
            get { return _Anio; }
            set { _Anio = value; }
        }

        private double _Base;
        public double Base
        {
            get { return _Base; }
            set { _Base = value; }
        }

        private double _BaseUP;
        public double BaseUP
        {
            get { return _BaseUP; }
            set { _BaseUP = value; }
        }

        private double _BaseUP_Porc;
        public double BaseUP_Porc
        {
            get { return _BaseUP_Porc; }
            set { _BaseUP_Porc = value; }
        }


        private int _Mes;
        public int Mes
        {
            get { return _Mes; }
            set { _Mes = value; }
        }

        private double _Venta;
        public double Venta
        {
            get { return _Venta; }
            set { _Venta = value; }
        }

        private double _UP;
        public double UP
        {
            get { return _UP; }
            set { _UP = value; }
        }

        private double _UP_Porc;
        public double UP_Porc
        {
            get { return _UP_Porc; }
            set { _UP_Porc = value; }
        }

        private object _FechaUltMod;
        public object FechaUltMod
        {
            get { return _FechaUltMod; }
            set { _FechaUltMod = value; }
        }

        private int _id_Usuario;
        public int id_Usuario
        {
            get { return _id_Usuario; }
            set { _id_Usuario = value; }
        }



        private int _Mes1;
        public int Mes1
        {
            get { return _Mes1; }
            set { _Mes1 = value; }
        }

        private double _Venta1;
        public double Venta1
        {
            get { return _Venta1; }
            set { _Venta1 = value; }
        }

        private double _UP1;
        public double UP1
        {
            get { return _UP1; }
            set { _UP1 = value; }
        }

        private double _UP_Porc1;
        public double UP_Porc1
        {
            get { return _UP_Porc1; }
            set { _UP_Porc1 = value; }
        }

        //Febrero
        private int _Mes2;
        public int Mes2
        {
            get { return _Mes2; }
            set { _Mes2 = value; }
        }

        private double _Venta2;
        public double Venta2
        {
            get { return _Venta2; }
            set { _Venta2 = value; }
        }

        private double _UP2;
        public double UP2
        {
            get { return _UP2; }
            set { _UP2 = value; }
        }

        private double _UP_Porc2;
        public double UP_Porc2
        {
            get { return _UP_Porc2; }
            set { _UP_Porc2 = value; }
        }


        //Marzo
        private int _Mes3;
        public int Mes3
        {
            get { return _Mes3; }
            set { _Mes3 = value; }
        }

        private double _Venta3;
        public double Venta3
        {
            get { return _Venta3; }
            set { _Venta3 = value; }
        }

        private double _UP3;
        public double UP3
        {
            get { return _UP3; }
            set { _UP3 = value; }
        }

        private double _UP_Porc3;
        public double UP_Porc3
        {
            get { return _UP_Porc3; }
            set { _UP_Porc3 = value; }
        }



        //Marzo
        private int _Mes4;
        public int Mes4
        {
            get { return _Mes4; }
            set { _Mes4 = value; }
        }

        private double _Venta4;
        public double Venta4
        {
            get { return _Venta4; }
            set { _Venta4 = value; }
        }

        private double _UP4;
        public double UP4
        {
            get { return _UP4; }
            set { _UP4 = value; }
        }

        private double _UP_Porc4;
        public double UP_Porc4
        {
            get { return _UP_Porc4; }
            set { _UP_Porc4 = value; }
        }


        //Mayo
        private int _Mes5;
        public int Mes5
        {
            get { return _Mes5; }
            set { _Mes5 = value; }
        }

        private double _Venta5;
        public double Venta5
        {
            get { return _Venta5; }
            set { _Venta5 = value; }
        }

        private double _UP5;
        public double UP5
        {
            get { return _UP5; }
            set { _UP5 = value; }
        }

        private double _UP_Porc5;
        public double UP_Porc5
        {
            get { return _UP_Porc5; }
            set { _UP_Porc5 = value; }
        }


        //Junio
        private int _Mes6;
        public int Mes6
        {
            get { return _Mes6; }
            set { _Mes6 = value; }
        }

        private double _Venta6;
        public double Venta6
        {
            get { return _Venta6; }
            set { _Venta6 = value; }
        }

        private double _UP6;
        public double UP6
        {
            get { return _UP6; }
            set { _UP6 = value; }
        }

        private double _UP_Porc6;
        public double UP_Porc6
        {
            get { return _UP_Porc6; }
            set { _UP_Porc6 = value; }
        }



        //Julio
        private int _Mes7;
        public int Mes7
        {
            get { return _Mes7; }
            set { _Mes7 = value; }
        }

        private double _Venta7;
        public double Venta7
        {
            get { return _Venta7; }
            set { _Venta7 = value; }
        }

        private double _UP7;
        public double UP7
        {
            get { return _UP7; }
            set { _UP7 = value; }
        }

        private double _UP_Porc7;
        public double UP_Porc7
        {
            get { return _UP_Porc7; }
            set { _UP_Porc7 = value; }
        }



        //Agosto
        private int _Mes8;
        public int Mes8
        {
            get { return _Mes8; }
            set { _Mes8 = value; }
        }

        private double _Venta8;
        public double Venta8
        {
            get { return _Venta8; }
            set { _Venta8 = value; }
        }

        private double _UP8;
        public double UP8
        {
            get { return _UP8; }
            set { _UP8 = value; }
        }

        private double _UP_Porc8;
        public double UP_Porc8
        {
            get { return _UP_Porc8; }
            set { _UP_Porc8 = value; }
        }


        //Septiembre
        private int _Mes9;
        public int Mes9
        {
            get { return _Mes9; }
            set { _Mes9 = value; }
        }

        private double _Venta9;
        public double Venta9
        {
            get { return _Venta9; }
            set { _Venta9 = value; }
        }

        private double _UP9;
        public double UP9
        {
            get { return _UP9; }
            set { _UP9 = value; }
        }

        private double _UP_Porc9;
        public double UP_Porc9
        {
            get { return _UP_Porc9; }
            set { _UP_Porc9 = value; }
        }


        //Octubre
        private int _Mes10;
        public int Mes10
        {
            get { return _Mes10; }
            set { _Mes10 = value; }
        }

        private double _Venta10;
        public double Venta10
        {
            get { return _Venta10; }
            set { _Venta10 = value; }
        }

        private double _UP10;
        public double UP10
        {
            get { return _UP10; }
            set { _UP10 = value; }
        }

        private double _UP_Porc10;
        public double UP_Porc10
        {
            get { return _UP_Porc10; }
            set { _UP_Porc10 = value; }
        }



        //Noviembre
        private int _Mes11;
        public int Mes11
        {
            get { return _Mes11; }
            set { _Mes11 = value; }
        }

        private double _Venta11;
        public double Venta11
        {
            get { return _Venta11; }
            set { _Venta11 = value; }
        }

        private double _UP11;
        public double UP11
        {
            get { return _UP11; }
            set { _UP11 = value; }
        }

        private double _UP_Porc11;
        public double UP_Porc11
        {
            get { return _UP_Porc11; }
            set { _UP_Porc11 = value; }
        }


        //Diciembre
        private int _Mes12;
        public int Mes12
        {
            get { return _Mes12; }
            set { _Mes12 = value; }
        }

        private double _Venta12;
        public double Venta12
        {
            get { return _Venta12; }
            set { _Venta12 = value; }
        }

        private double _UP12;
        public double UP12
        {
            get { return _UP12; }
            set { _UP12 = value; }
        }

        private double _UP_Porc12;
        public double UP_Porc12
        {
            get { return _UP_Porc12; }
            set { _UP_Porc12 = value; }
        }

        private string _MesLetra;
        public string MesLetra
        {
            get { return _MesLetra; }
            set { _MesLetra = value; }
        }

        private double _UP_Presupuesto;
        public double UP_Presupuesto
        {
            get { return _UP_Presupuesto; }
            set { _UP_Presupuesto = value; }
        }

        private double _Meta_Ppto;
        public double Meta_Ppto
        {
            get { return _Meta_Ppto; }
            set { _Meta_Ppto = value; }
        }

        private double _Incremento_Real;
        public double Incremento_Real
        {
            get { return _Incremento_Real; }
            set { _Incremento_Real = value; }
        }
        private double _Porc_Cumplimiento;
        public double Porc_Cumplimiento
        {
            get { return _Porc_Cumplimiento; }
            set { _Porc_Cumplimiento = value; }
        }

        private double _Multiplicador;
        public double Multiplicador
        {
            get { return _Multiplicador; }
            set { _Multiplicador = value; }
        }

    }
}
