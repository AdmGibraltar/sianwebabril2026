using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class eCrmProyecto
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

        private string _Id_Ter;
        public string Id_Ter
        {
            get { return _Id_Ter; }
            set { _Id_Ter = value; }
        }

        private int _Id_Cte;
        public int Id_Cte
        {
            get { return _Id_Cte; }
            set { _Id_Cte = value; }
        }

        private string _NombreCliente;
        public string NombreCliente
        {
            get { return _NombreCliente; }
            set { _NombreCliente = value; }
        }

        private string _AreaDeAplicacion;
        public string AreaDeAplicacion
        {
            get { return _AreaDeAplicacion; }
            set { _AreaDeAplicacion = value; }
        }

        private int _Id_Op;
        public int Id_Op
        {
            get { return _Id_Op; }
            set { _Id_Op = value; }
        }

        private string _AreAplicacion;
        public string AreAplicacion
        {
            get { return _AreAplicacion; }
            set { _AreAplicacion = value; }
        }

        private double _Vap_UtilidadRemanente;
        public double Vap_UtilidadRemanente
        {
            get { return _Vap_UtilidadRemanente; }
            set { _Vap_UtilidadRemanente = value; }
        }

        private double _Vap_ValorPresenteNeto;
        public double Vap_ValorPresenteNeto
        {
            get { return _Vap_ValorPresenteNeto; }
            set { _Vap_ValorPresenteNeto = value; }
        }

        private string _Vap_Estatus;
        public string Vap_Estatus
        {
            get { return _Vap_Estatus; }
            set { _Vap_Estatus = value; }
        }

        private string _Vap_Estatus2;
        public string Vap_Estatus2
        {
            get { return _Vap_Estatus2; }
            set { _Vap_Estatus2 = value; }
        }

        private int _Estatus;
        public int Estatus
        {
            get { return _Estatus; }
            set { _Estatus = value; }
        }

        private int _Crm_TipoVenta;
        public int Crm_TipoVenta
        {
            get { return _Crm_TipoVenta; }
            set { _Crm_TipoVenta = value; }
        }

        private int _Id_CrmProspecto;
        public int Id_CrmProspecto
        {
            get { return _Id_CrmProspecto; }
            set { _Id_CrmProspecto = value; }
        }

        private int _Valuacion;
        public int Valuacion
        {
            get { return _Valuacion; }
            set { _Valuacion = value; }
        }

        private string _VinculadoCentral;
        public string VinculadoCentral
        {
            get { return _VinculadoCentral; }
            set { _VinculadoCentral = value; }
        }


        //

        private string _Uen_Descripcion;
        public string Uen_Descripcion
        {
            get { return _Uen_Descripcion; }
            set { _Uen_Descripcion = value; }
        }

        private string _Seg_Descripcion;
        public string Seg_Descripcion
        {
            get { return _Seg_Descripcion; }
            set { _Seg_Descripcion = value; }
        }

        private string _Area_Descripcion;
        public string Area_Descripcion
        {
            get { return _Area_Descripcion; }
            set { _Area_Descripcion = value; }
        }

        private string _Sol_Descripcion;
        public string Sol_Descripcion
        {
            get { return _Sol_Descripcion; }
            set { _Sol_Descripcion = value; }
        }

        private string _Apl_Descripcion;
        public string Apl_Descripcion
        {
            get { return _Apl_Descripcion; }
            set { _Apl_Descripcion = value; }
        }

        private double _ValorPotencialTeorico;
        public double ValorPotencialTeorico
        {
            get { return _ValorPotencialTeorico; }
            set { _ValorPotencialTeorico = value; }
        }

        private double _VentaPromedioMensualEsperadaAntesCierre;
        public double VentaPromedioMensualEsperadaAntesCierre
        {
            get { return _VentaPromedioMensualEsperadaAntesCierre; }
            set { _VentaPromedioMensualEsperadaAntesCierre = value; }
        }

        private double _VentaPromedioMensualEsperada;
        public double VentaPromedioMensualEsperada
        {
            get { return _VentaPromedioMensualEsperada; }
            set { _VentaPromedioMensualEsperada = value; }
        }

        private int _Id_Seg;
        public int Id_Seg
        {
            get { return _Id_Seg; }
            set { _Id_Seg = value; }
        }
        private int _Id_CteDet;
        public int Id_CteDet
        {
            get { return _Id_CteDet; }
            set { _Id_CteDet = value; }
        }

        public int CountProd { get; set; }
    }
}