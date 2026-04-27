using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    //
    //17 Sep 2018  RFH
    //

    public class eCapValProyectoParametros
    {
        private int _Id_Emp { get; set; }
        public int Id_Emp { get { return _Id_Emp; } set { _Id_Emp = value; } }

        private int _Id_Cd { get; set; }
        public int Id_Cd { get { return _Id_Cd; } set { _Id_Cd = value; } }

        private int _Id_Vap { get; set; }
        public int Id_Vap { get { return _Id_Vap; } set { _Id_Vap = value; } }

        private double _CuentasPorCobrar { get; set; }
        public double CuentasPorCobrar { get { return _CuentasPorCobrar; } set { _CuentasPorCobrar = value; } }

        private double _Inventario { get; set; }
        public double Inventario { get { return _Inventario; } set { _Inventario= value; } }

        private double _GastosServirCliente { get; set; }
        public double GastosServirCliente { get { return _GastosServirCliente; } set { _GastosServirCliente = value; } }

        private double _Vigencia { get; set; }
        public double Vigencia { get { return _Vigencia; } set { _Vigencia = value; } }

        private double _FleteLocales { get; set; }
        public double FleteLocales { get { return _FleteLocales; } set { _FleteLocales = value; } }

        private double _Isr { get; set; }
        public double Isr { get { return _Isr; } set { _Isr = value; } }

        private double _Cetes { get; set; }
        public double Cetes { get { return _Cetes; } set { _Cetes = value; } }

        private double _Financiamientoproveedores { get; set; }
        public double Financiamientoproveedores { get { return _Financiamientoproveedores; } set { _Financiamientoproveedores = value; } }

        private double _Inversionactivosfijos { get; set; }
        public double Inversionactivosfijos { get { return _Inversionactivosfijos; } set { _Inversionactivosfijos = value; } }

        private double _Costodecapital { get; set; }
        public double Costodecapital { get { return _Costodecapital; } set { _Costodecapital = value; } }
        
        private double _ManoObra { get; set; }
        public double ManoObra { get { return _ManoObra; } set { _ManoObra = value; } }
        
        private double _GastosVarAplTerr { get; set; }
        public double GastosVarAplTerr { get { return _GastosVarAplTerr; } set { _GastosVarAplTerr = value; } }

        private double _FletesPagadosalCliente { get; set; }
        public double FletesPagadosalCliente { get { return _FletesPagadosalCliente; } set { _FletesPagadosalCliente = value; } }

        private int _SqlResult { get; set; }
        public int SqlResult { get { return _SqlResult; } set { _SqlResult = value; } }

    }
}
