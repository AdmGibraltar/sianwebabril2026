using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class eClenteBuscar_Params
    {
        private int _PageNumber;
        public int PageNumber
        {
            get { return _PageNumber; }
            set { _PageNumber = value; }
        }
        private int _PageSize;
        public int PageSize
        {
            get { return _PageSize; }
            set { _PageSize = value; }
        }

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

        private int _Anio;
        public int Anio
        {
            get { return _Anio; }
            set { _Anio = value; }
        }

        private int _Mes;
        public int Mes
        {
            get { return _Mes; }
            set { _Mes = value; }
        }

        private int _Id_Uen;
        public int Id_Uen
        {
            get { return _Id_Uen; }
            set { _Id_Uen = value; }
        }

        private int _Id_Seg;
        public int Id_Seg
        {
            get { return _Id_Seg; }
            set { _Id_Seg = value; }
        }

        private int _Id_Rik;
        public int Id_Rik
        {
            get { return _Id_Rik; }
            set { _Id_Rik = value; }
        }

        private int _Id_Ter;
        public int Id_Ter
        {
            get { return _Id_Ter; }
            set { _Id_Ter = value; }
        }
        private int _Tipo;
        public int Tipo
        {
            get { return _Tipo; }
            set { _Tipo = value; }
        }

        private string _TextoBuscar;
        public string TextoBuscar
        {
            get { return _TextoBuscar; }
            set { _TextoBuscar = value; }
        }

        private string _Conexion;
        public string Conexion
        {
            get { return _Conexion; }
            set { _Conexion = value; }
        }

        private int _Id_Cte;
        public int Id_Cte
        {
            get { return _Id_Cte; }
            set { _Id_Cte = value; }
        }

        private int _CampoOrden;
        public int CampoOrden
        {
            get { return _CampoOrden; }
            set { _CampoOrden = value; }
        }

        private int _OrdenDir;
        public int OrdenDir
        {
            get { return _OrdenDir; }
            set { _OrdenDir = value; }
        }


        public int tipoCliente { get; set; }
        public string NombreCliente { get; set; }
        public int TipoRep { get; set; }
    }

    public class eClienteBuscar
    {

        private int _Cte;
        public int Cte
        {
            get { return _Cte; }
            set { _Cte = value; }
        }

        private int _Id_Cte;
        public int Id_Cte
        {
            get { return _Id_Cte; }
            set { _Id_Cte = value; }
        }

        private string _RFC;
        public string RFC
        {
            get { return _RFC; }
            set { _RFC = value; }
        }

        private string _Cte_FacRfc;
        public string Cte_FacRfc
        {
            get { return _Cte_FacRfc; }
            set { _Cte_FacRfc = value; }
        }

        private string _NomComercial;
        public string NomComercial
        {
            get { return _NomComercial; }
            set { _NomComercial = value; }
        }

        private string _Cte_NomComercial;
        public string Cte_NomComercial
        {
            get { return _Cte_NomComercial; }
            set { _Cte_NomComercial = value; }
        }

        private double _VPObservado;
        public double VPObservado
        {
            get { return _VPObservado; }
            set { _VPObservado = value; }
        }

        private int _UEN;
        public int UEN
        {
            get { return _UEN; }
            set { _UEN = value; }
        }

        private int _Id_Uen;
        public int Id_Uen
        {
            get { return _Id_Uen; }
            set { _Id_Uen = value; }
        }

        private int _Segmento;
        public int Segmento
        {
            get { return _Segmento; }
            set { _Segmento = value; }
        }

        private int _Id_Seg;
        public int Id_Seg
        {
            get { return _Id_Seg; }
            set { _Id_Seg = value; }
        }

        private Int32 _IdTer;
        public Int32 IdTer
        {
            get { return _IdTer; }
            set { _IdTer = value; }
        }

        private Int32 _Id_Ter;
        public Int32 Id_Ter
        {
            get { return _Id_Ter; }
            set { _Id_Ter = value; }
        }

        private string _TerNombre;
        public string TerNombre
        {
            get { return _TerNombre; }
            set { _TerNombre = value; }
        }

        private string _Ter_Nombre;
        public string Ter_Nombre
        {
            get { return _Ter_Nombre; }
            set { _Ter_Nombre = value; }
        }

        private int _Activo;
        public int Activo
        { get { return _Activo; } set { _Activo = value; } }

        private Int32 _RegistrosEcontrados;
        public Int32 RegistrosEcontrados
        {
            get { return _RegistrosEcontrados; }
            set { _RegistrosEcontrados = value; }
        }

        private double _VtaINstaladaConsolidada;
        public double VtaINstaladaConsolidada
        {
            get { return _VtaINstaladaConsolidada; }
            set { _VtaINstaladaConsolidada = value; }
        }

        private double _VtaMesFacturada;
        public double VtaMesFacturada
        {
            get { return _VtaMesFacturada; }
            set { _VtaMesFacturada = value; }
        }

        private double _VtaDelMesvsVI;
        public double VtaDelMesvsVI
        {
            get { return _VtaDelMesvsVI; }
            set { _VtaDelMesvsVI = value; }
        }

        private double _CumplimientoVIMes;
        public double CumplimientoVIMes
        {
            get { return _CumplimientoVIMes; }
            set { _CumplimientoVIMes = value; }
        }

        private double _VtaTotalMes;
        public double VtaTotalMes
        {
            get { return _VtaTotalMes; }
            set { _VtaTotalMes = value; }
        }

        private double _MontoConsolidado;
        public double MontoConsolidado
        {
            get { return _MontoConsolidado; }
            set { _MontoConsolidado = value; }
        }

        private double _VtaMes;
        public double VtaMes
        {
            get { return _VtaMes; }
            set { _VtaMes = value; }
        }

        private double _VtaProm;
        public double VtaProm
        {
            get { return _VtaProm; }
            set { _VtaProm = value; }
        }

        private double _VtaInst;
        public double VtaInst
        {
            get { return _VtaInst; }
            set { _VtaInst = value; }
        }

        private double _MESVI;
        public double MESVI
        {
            get { return _MESVI; }
            set { _MESVI = value; }
        }

        private double _PorcMes;
        public double PorcMes
        {
            get { return _PorcMes; }
            set { _PorcMes = value; }
        }

        private double _TRIMVI;
        public double TRIMVI
        {
            get { return _TRIMVI; }
            set { _TRIMVI = value; }
        }

        private double _PorcTRIM;
        public double PorcTRIM
        {
            get { return _PorcTRIM; }
            set { _PorcTRIM = value; }
        }

        private double _VtaMesTot;
        public double VtaMesTot
        {
            get { return _VtaMesTot; }
            set { _VtaMesTot = value; }
        }


        private Int64 _Id_Prd;
        public Int64 Id_Prd
        {
            get { return _Id_Prd; }
            set { _Id_Prd = value; }
        }

        private string _Prd_Descripcion;
        public string Prd_Descripcion
        {
            get { return _Prd_Descripcion; }
            set { _Prd_Descripcion = value; }
        }

        private int _AcysCount;
        public int AcysCount
        { get { return _AcysCount; } set { _AcysCount = value; } }

        // 30MAY-2021 RFH

        private int _TipoVTA;
        public int TipoVTA
        { get { return _TipoVTA; } set { _TipoVTA = value; } }

        private int _TipoPTA;
        public int TipoPTA
        { get { return _TipoPTA; } set { _TipoPTA = value; } }

        public string tipoCliente { get; set; }
        public int TipoCuenta { get; set; }
        public string descripcionTipoCuenta { get; set; }
        public int CantidadTipoCuenta { get; set; }
        //
    }
}