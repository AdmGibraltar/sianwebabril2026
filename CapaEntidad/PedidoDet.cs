using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class PedidoDet
    {
        private int _Id_Ter;
        private long _Id_Prd;
        public long Id_PrdOri { get; set; }
        private string _Prd_Desc;
        private int _Prd_Ord;
        private int _Prd_OrdDisp;
        private int _Prd_Asig;
        private int _Prd_Faltante;
        private int _Prd_Existencia;
        private int _Prd_Disponible;
        private int _Id_PedDet;
        private int _Id_Emp;
        private int _Ped_CantF;
        private int _ped_CantidadDisponible;
        private int _ped_PorcentajeCantidadDisponible;
        private int _ped_ImporteOrdenado;
        private int _ped_ImporteDisponible;
        private int _ped_PorcentajeImporteDisponible;
        private int _ped_Asignado;
        private int _ped_PorcentajeAsignado;
        private int _ped_ImporteAsignado;
        private int _ped_PorcentajeImporteAsignado;
        private string _Prd_Descripcion;
        private string _Prd_Presentacion;
        private string _Prd_UniNe;
        private int _ped_Cantidad;
        private string _TipoPedido;


        private int _ped_Picking;
        private int _ped_Facturado;
        private int _ped_Remisionado;
        private int _ped_Pendiente;

        private string _ruta;

        private bool _Credito;
        private string _CreditoStr;
        private bool _agrupado;

        private string _ped_PermiteParcialidades;

        public string Ruta
        {
            get { return _ruta; }
            set { _ruta = value; }
        }

        public bool Agrupado
        {
            get { return _agrupado; }
            set { _agrupado = value; }
        }

        public int Ped_CantF
        {
            get { return _Ped_CantF; }
            set { _Ped_CantF = value; }
        }
        private int _Ped_CantR;

        public int Ped_CantR
        {
            get { return _Ped_CantR; }
            set { _Ped_CantR = value; }
        }
        public string TipoPedido
        {
            get { return _TipoPedido; }
            set { _TipoPedido = value; }
        }

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
        private int _Id_Ped;
        private string _Ter_Descripcion;

        public string Ter_Descripcion
        {
            get { return _Ter_Descripcion; }
            set { _Ter_Descripcion = value; }
        }
        private int _Original;

        public int Original
        {
            get { return _Original; }
            set { _Original = value; }
        }
        private int _Cancelado;

        public int Cancelado
        {
            get { return _Cancelado; }
            set { _Cancelado = value; }
        }
        private int _Pendiente;

        public int Pendiente
        {
            get { return _Pendiente; }
            set { _Pendiente = value; }
        }
        private int _Final;

        public int Final
        {
            get { return _Final; }
            set { _Final = value; }
        }

        public int Id_Ped
        {
            get { return _Id_Ped; }
            set { _Id_Ped = value; }
        }

        public int Id_PedDet
        {
            get { return _Id_PedDet; }
            set { _Id_PedDet = value; }
        }

        public int Id_Ter
        {
            get { return _Id_Ter; }
            set { _Id_Ter = value; }
        }
        public long Id_Prd
        {
            get { return _Id_Prd; }
            set { _Id_Prd = value; }
        }
        public string Prd_Desc
        {
            get { return _Prd_Desc; }
            set { _Prd_Desc = value; }
        }
        public int Prd_Ord
        {
            get { return _Prd_Ord; }
            set { _Prd_Ord = value; }
        }
        public int Prd_OrdDisp
        {
            get { return _Prd_OrdDisp; }
            set { _Prd_OrdDisp = value; }
        }
        public int Prd_Asig
        {
            get { return _Prd_Asig; }
            set { _Prd_Asig = value; }
        }
        public int Prd_Faltante
        {
            get { return _Prd_Faltante; }
            set { _Prd_Faltante = value; }
        }
        public int Prd_Existencia
        {
            get { return _Prd_Existencia; }
            set { _Prd_Existencia = value; }
        }
        public int Prd_Disponible
        {
            get { return _Prd_Disponible; }
            set { _Prd_Disponible = value; }
        }

        private int _prd_NoConf;
        public int Prd_NoConf
        {
            get { return _prd_NoConf; }
            set { _prd_NoConf = value; }
        }

        private int _prd_NoEnc;
        public int Prd_NoEnc
        {
            get { return _prd_NoEnc; }
            set { _prd_NoEnc = value; }
        }

        private int _prd_PorcentajeAsignado;
        public int Prd_PorcentajeAsignado
        {
            get { return _prd_PorcentajeAsignado; }
            set { _prd_PorcentajeAsignado = value; }
        }

        private int _TotalProd;
        public int TotalProd
        {
            get { return _TotalProd; }
            set { _TotalProd = value; }
        }
        public int Ped_CantidadDisponible
        {
            get { return _ped_CantidadDisponible; }
            set { _ped_CantidadDisponible = value; }
        }
        public int Ped_PorcentajeCantidadDisponible
        {
            get { return _ped_PorcentajeCantidadDisponible; }
            set { _ped_PorcentajeCantidadDisponible = value; }
        }
        public int Ped_ImporteOrdenado
        {
            get { return _ped_ImporteOrdenado; }
            set { _ped_ImporteOrdenado = value; }
        }
        public int Ped_ImporteDisponible
        {
            get { return _ped_ImporteDisponible; }
            set { _ped_ImporteDisponible = value; }
        }
        public int Ped_PorcentajeImporteDisponible
        {
            get { return _ped_PorcentajeImporteDisponible; }
            set { _ped_PorcentajeImporteDisponible = value; }
        }
        public int Ped_Asignado
        {
            get { return _ped_Asignado; }
            set { _ped_Asignado = value; }
        }
        public int Ped_PorcentajeAsignado
        {
            get { return _ped_PorcentajeAsignado; }
            set { _ped_PorcentajeAsignado = value; }
        }
        public int Ped_ImporteAsignado
        {
            get { return _ped_ImporteAsignado; }
            set { _ped_ImporteAsignado = value; }
        }
        public int Ped_PorcentajeImporteAsignado
        {
            get { return _ped_PorcentajeImporteAsignado; }
            set { _ped_PorcentajeImporteAsignado = value; }
        }
        public string Prd_Descripcion
        {
            get { return _Prd_Descripcion; }
            set { _Prd_Descripcion = value; }
        }
        public string Prd_Presentacion
        {
            get { return _Prd_Presentacion; }
            set { _Prd_Presentacion = value; }
        }
        public string Prd_UniNe
        {
            get { return _Prd_UniNe; }
            set { _Prd_UniNe = value; }
        }
        public int Ped_Cantidad
        {
            get { return _ped_Cantidad; }
            set { _ped_Cantidad = value; }
        }

        public int Ped_Picking
        {
            get { return _ped_Picking; }
            set { _ped_Picking = value; }
        }




        public int Ped_Facturado
        {
            get { return _ped_Facturado; }
            set { _ped_Facturado = value; }
        }

        public int Ped_Remisionado
        {
            get { return _ped_Remisionado; }
            set { _ped_Remisionado = value; }
        }

        public int Ped_Pendiente
        {
            get { return _ped_Pendiente; }
            set { _ped_Pendiente = value; }
        }

        public bool Credito
        {
            get { return _Credito; }
            set { _Credito = value; }
        }
        public string CreditoStr
        {
            get { return _CreditoStr; }
            set { _CreditoStr = value; }
        }
        public string Ped_PermiteParcialidades
        {
            get { return _ped_PermiteParcialidades; }
            set { _ped_PermiteParcialidades = value; }
        }


        public int id_sol { get; set; }
        public string Prd_DescripcionOri { get; set; }
        public double Importe { get; set; }
        public double AAA { get; set; }
        public int Id_Cliente { get; set; }
        public int Prd_Activo { get; set; }
        public int TipoCancelacion { get; set; }

    }
}