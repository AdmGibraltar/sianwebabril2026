//Autor: Oscar Casillas
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace CapaEntidad
{
    public class CompraLocal
    {
        private int _Id_Emp;
        private int _Id_Cd;
        private string _Cd_Nombre;
        private int _Id_Comp;
        private int _Comp_Solicito;
        private string _Solicito_Nombre;
        private DateTime _FechaSol;
        private string _FechaAut;
        private int _Comp_Autorizo;
        private double _Comp_Descuento;
        private string _EstatusAut;
        private string _Comentarios;
        private string _TipoSolicitud;
        private int _IdTipoSolicitud;
        private DateTime _Vigencia;
        private int _PermisoAutorizar;
        private int _Id_U;

        public int Id_U
        {
            get { return _Id_U; }
            set { _Id_U = value; }
        }

        public int PermisoAutorizar
        {
            get { return _PermisoAutorizar; }
            set { _PermisoAutorizar = value; }
        }
        public int Id_Emp
        {
            get { return _Id_Emp; }
            set { _Id_Emp = value; }
        }
        public int Id_Cd
        {
            get { return _Id_Cd; }
            set { _Id_Cd = value; }
        }
        public string Cd_Nombre
        {
            get { return _Cd_Nombre; }
            set { _Cd_Nombre = value; }
        }
        public int Id_Comp
        {
            get { return _Id_Comp; }
            set { _Id_Comp = value; }
        }
        public int Comp_Solicito
        {
            get { return _Comp_Solicito; }
            set { _Comp_Solicito = value; }
        }
        public string Solicito_Nombre
        {
            get { return _Solicito_Nombre; }
            set { _Solicito_Nombre = value; }
        }
        public DateTime FechaSol
        {
            get { return _FechaSol; }
            set { _FechaSol = value; }
        }
        public string FechaAut
        {
            get { return _FechaAut; }
            set { _FechaAut = value; }
        }
        public int Comp_Autorizo
        {
            get { return _Comp_Autorizo; }
            set { _Comp_Autorizo = value; }
        }
        public double Comp_Descuento
        {
            get { return _Comp_Descuento; }
            set { _Comp_Descuento = value; }
        }

        public string EstatusAut
        {
            get { return _EstatusAut; }
            set { _EstatusAut = value; }
        }

        public string TipoSolicitud
        {
            get { return _TipoSolicitud; }
            set { _TipoSolicitud = value; }
        }

        public string Comentarios
        {
            get { return _Comentarios; }
            set { _Comentarios = value; }
        }

        public DateTime Vigencia
        {
            get { return _Vigencia; }
            set { _Vigencia = value; }
        }

        public int IdTipoSolicitud
        {
            get { return _IdTipoSolicitud; }
            set { _IdTipoSolicitud = value; }
        }

        //RBM MARZO 2024
        //Nuevos Campos 
        private Int64 _Id_Prd;
        private string _Prd_Descripcion;
        private int _IdAutorizador;
        private string _CodigoPadre;
        private string _Motivo;
        private string _ProveedorLocal;
        private string _ProveedorCentral;
        private double _Costo;
        private double _PrecioAAACL;
        private double _PrecioAAAKey;
        private double _PrecioPublico;
        private string _ClienteExc;
        private string _Aplicacion;
        private string _TipoProd;
        private int _Vencido;
        private int _IdProveedorLocal;
        private string _Prd_UniNe;
        private string _Prd_UniNs;
        private string _Prd_Unico;
        private string _Prd_UniEmp;
        private string _Prd_CodigoProv;
        private string _Prd_DescripcionProv;
        private string _Prd_PresentacionProv;
        private string _Prd_ClaveProdServ;
        private string _Prd_ClaveUnidad;
        private string _IdCteExc;
        private string _TipoCliente;
        private string _Prd_Presentacion;
        private string _Prd_SubFamilia;
        public string _IdTipoProd;
        public string _NomTipoProd;
        private int id_Motivo;

        public string IdTipoProd
        {
            get { return _IdTipoProd; }
            set { _IdTipoProd = value; }
        }

        public string NomTipoProd
        {
            get { return _NomTipoProd; }
            set { _NomTipoProd = value; }
        }

        public string Prd_SubFamilia
        {
            get { return _Prd_SubFamilia; }
            set { _Prd_SubFamilia = value; }
        }
        public string Prd_Presentacion
        {
            get { return _Prd_Presentacion; }
            set { _Prd_Presentacion = value; }
        }

        public string IdCteExc
        {
            get { return _IdCteExc; }
            set { _IdCteExc = value; }
        }

        public string TipoCliente
        {
            get { return _TipoCliente; }
            set { _TipoCliente = value; }
        }

        public string Prd_ClaveProdServ
        {
            get { return _Prd_ClaveProdServ; }
            set { _Prd_ClaveProdServ = value; }
        }

        public string Prd_ClaveUnidad
        {
            get { return _Prd_ClaveUnidad; }
            set { _Prd_ClaveUnidad = value; }
        }

        public string Prd_CodigoProv
        {
            get { return _Prd_CodigoProv; }
            set { _Prd_CodigoProv = value; }
        }
        public string Prd_DescripcionProv
        {
            get { return _Prd_DescripcionProv; }
            set { _Prd_DescripcionProv = value; }
        }
        public string Prd_PresentacionProv
        {
            get { return _Prd_PresentacionProv; }
            set { _Prd_PresentacionProv = value; }
        }
        public string Prd_UniNe
        {
            get { return _Prd_UniNe; }
            set { _Prd_UniNe = value; }
        }
        public string Prd_UniNs
        {
            get { return _Prd_UniNs; }
            set { _Prd_UniNs = value; }
        }
        public string Prd_Unico
        {
            get { return _Prd_Unico; }
            set { _Prd_Unico = value; }
        }
        public string Prd_UniEmp
        {
            get { return _Prd_UniEmp; }
            set { _Prd_UniEmp = value; }
        }



        public Int32 Vencido
        {
            get { return _Vencido; }
            set { _Vencido = value; }
        }
        public string ProveedorCentral
        {
            get { return _ProveedorCentral; }
            set { _ProveedorCentral = value; }
        }

        public Int32 IdProveedorLocal
        {
            get { return _IdProveedorLocal; }
            set { _IdProveedorLocal = value; }
        }
        public string CodigoPadre
        {
            get { return _CodigoPadre; }
            set { _CodigoPadre = value; }
        }

        public string Motivo
        {
            get { return _Motivo; }
            set { _Motivo = value; }
        }

        public string ProveedorLocal
        {
            get { return _ProveedorLocal; }
            set { _ProveedorLocal = value; }
        }

        public double Costo
        {
            get { return _Costo; }
            set { _Costo = value; }
        }

        public double PrecioAAACL
        {
            get { return _PrecioAAACL; }
            set { _PrecioAAACL = value; }
        }
        public double PrecioAAAKey
        {
            get { return _PrecioAAAKey; }
            set { _PrecioAAAKey = value; }
        }

        public double PrecioPublico
        {
            get { return _PrecioPublico; }
            set { _PrecioPublico = value; }
        }
        public string ClienteExc
        {
            get { return _ClienteExc; }
            set { _ClienteExc = value; }
        }

        public string Aplicacion
        {
            get { return _Aplicacion; }
            set { _Aplicacion = value; }
        }

        public string TipoProd
        {
            get { return _TipoProd; }
            set { _TipoProd = value; }
        }

        public Int64 Id_Prd
        {
            get { return _Id_Prd; }
            set { _Id_Prd = value; }
        }

        public string Prd_Descripcion
        {
            get { return _Prd_Descripcion; }
            set { _Prd_Descripcion = value; }
        }

        public int IdAutorizador
        {
            get { return _IdAutorizador; }
            set { _IdAutorizador = value; }
        }

        public string IdAplicacion { get; set; }
        public int Id_Motivo { get => id_Motivo; set => id_Motivo = value; }
        public int Unidades { get; set; }
        public decimal Pesos { get; set; }
        public int TipoAAA { get; set; }
        public string IdProveedorCentral { get; set; }
        public int TotalProveedores { get; set; }
        public int TotalProductos { get; set; }
        public decimal Porcentaje { get; set; }
        public string PrecioMayor { get; set; }

    }
}