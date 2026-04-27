using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Renglon
    {
        private int _idRng;
        private string _sDescripcion;
        private string _sValor;

        public int idRng
        {
            get { return _idRng; }
            set { _idRng = value; }
        }

        public string sDescripcion
        {
            get { return _sDescripcion; }
            set { _sDescripcion = value; }
        }

        public string sValor
        {
            get { return _sValor; }
            set { _sValor = value; }
        }
    }

    public class DashboardACyS_Resumen
    {
        private int _idCdi;
        private int _IdRik;
        private int _Ordern;
        private string _Concepto;
        private string _sValor;

        public int IdCdi
        {
            get { return _idCdi; }
            set { _idCdi = value; }
        }

        public int IdRik
        {
            get { return _IdRik; }
            set { _IdRik = value; }
        }

        public int Ordern
        {
            get { return _Ordern; }
            set { _Ordern = value; }
        }

        public string Concepto
        {
            get { return _Concepto; }
            set { _Concepto = value; }
        }

        public string sValor
        {
            get { return _sValor; }
            set { _sValor = value; }
        }
    }

    public class DashboardACyS_Estatus
    {
        private int _idCdi;
        private int _IdRik;
        private string _AcsEstatusTexto;
        private int _NACySValor;

        public int IdCdi
        {
            get { return _idCdi; }
            set { _idCdi = value; }
        }

        public int IdRik
        {
            get { return _IdRik; }
            set { _IdRik = value; }
        }

        public string AcsEstatusTexto
        {
            get { return _AcsEstatusTexto; }
            set { _AcsEstatusTexto = value; }
        }

        public int NACySValor
        {
            get { return _NACySValor; }
            set { _NACySValor = value; }
        }
    }

    public class DashboardACyS_RIKS
    {
        private int _idCdi;
        private int _IdRik;
        private string _Rik_Nombre;
        private int _NACySValor;

        public int IdCdi
        {
            get { return _idCdi; }
            set { _idCdi = value; }
        }

        public int IdRik
        {
            get { return _IdRik; }
            set { _IdRik = value; }
        }

        public string Rik_Nombre
        {
            get { return _Rik_Nombre; }
            set { _Rik_Nombre = value; }
        }

        public int NACySValor
        {
            get { return _NACySValor; }
            set { _NACySValor = value; }
        }
    }

    //  Id_Acs	Acs_EstatusTexto	Id_Cte	Cte_NomComercial	TipoCuenta	Acs_Fecha	Id_Rik	Rik_Nombre	Id_Ter	
    //  Acs_FechaInicioDocumento	Acs_FechaFinDocumento	Acs_Vencido	Acs_ReqAutGerente	Acs_ReqAutJefeOp
    //  1089	Autorizado	30450	AUTOSERVICIO LA PLAYA SA DE CV  L	03/01/2020	100	VACANTE	40204032	
    //  06/01/2020	31/12/2021	NO	2	2

    public class DashboardACyS_DetalleRIKS
    {
        private int _idCdi;
        private string _Cdi;
        private int _Id_Acs;
        private string _AcsEstatus;
        private int _Id_Cte;
        private string _NombreCliente;
        private string _TipoCuenta;
        private string _FechaAcs;
        private int _Id_Rik;
        private string _Rik_Nombre;
        private int _Id_Ter;
        private string _FechaInicioAcs;
        private string _FechaFinAcs;
        private string _AcsVencido;
        //  private int _ReqAutGerente;
        //  private int _ReqAutJefeOp;

        public int IdCdi
        {
            get { return _idCdi; }
            set { _idCdi = value; }
        }

        public string CDI
        {
            get { return _Cdi; }
            set { _Cdi = value; }
        }

        public int Id_Acs
        {
            get { return _Id_Acs; }
            set { _Id_Acs = value; }
        }

        public int Id_Rik
        {
            get { return _Id_Rik; }
            set { _Id_Rik = value; }
        }

        public string Rik_Nombre
        {
            get { return _Rik_Nombre; }
            set { _Rik_Nombre = value; }
        }

        public int Id_Ter
        {
            get { return _Id_Ter; }
            set { _Id_Ter = value; }
        }

        public int Id_Cte
        {
            get { return _Id_Cte; }
            set { _Id_Cte = value; }
        }

        public string NombreCliente
        {
            get { return _NombreCliente; }
            set { _NombreCliente = value; }
        }

        public string TipoCuenta
        {
            get { return _TipoCuenta; }
            set { _TipoCuenta = value; }
        }

        public string FechaAcs
        {
            get { return _FechaAcs; }
            set { _FechaAcs = value; }
        }

        public string FechaInicioAcs
        {
            get { return _FechaInicioAcs; }
            set { _FechaInicioAcs = value; }
        }

        public string FechaFinAcs
        {
            get { return _FechaFinAcs; }
            set { _FechaFinAcs = value; }
        }

        public string AcsVencido
        {
            get { return _AcsVencido; }
            set { _AcsVencido = value; }
        }

        public string AcsEstatus
        {
            get { return _AcsEstatus; }
            set { _AcsEstatus = value; }
        }
        /*
        public int ReqAutGerente
        {
            get { return _ReqAutGerente; }
            set { _ReqAutGerente = value; }
        }

        public int ReqAutJefeOp
        {
            get { return _ReqAutJefeOp; }
            set { _ReqAutJefeOp = value; }
        }
        */
    }

    public class DashboardACyS_Clientes
    {
        /// Id Nombre
        private int _idCdi;
        private int _IdRik;
        private int _Ordern;
        private int _iIdCliente;
        private string _Cliente;


        public int IdCdi
        {
            get { return _idCdi; }
            set { _idCdi = value; }
        }
        public int IdRik
        {
            get { return _IdRik; }
            set { _IdRik = value; }
        }

        public int Ordern
        {
            get { return _Ordern; }
            set { _Ordern = value; }
        }

        public string Cliente
        {
            get { return _Cliente; }
            set { _Cliente = value; }
        }

        public int iIdCliente
        {
            get { return _iIdCliente; }
            set { _iIdCliente = value; }
        }
    }


    //  Folio	Estatus	    NumCliente	Cliente	                                        Territorio	RIK	FechaACyS	
    //  FechaInicio	    FechaFin	TipoCuenta	Vencido
    //  1	    Capturado	31649	    NACIONAL DE CONDUCTORES ELECTRICOS SA DE CV 	30301011	959	11/12/2010	
    //  11/12/2010	    31/12/2021	L           NO
    public class DashboardACyS_DetalleACyS
    {
        private int _idCdi;
        private string _Cdi;
        private int _Folio;
        private string _Estatus;
        private int _NumCliente;
        private string _Cliente;
        private string _Territorio;
        private int _Rik;
        private string _FechaACyS;
        private string _FechaInicio;
        private string _FechaFin;
        private string _TipoCuenta;
        private string _Vencido;

        public int IdCdi
        {
            get { return _idCdi; }
            set { _idCdi = value; }
        }

        public string CDI
        {
            get { return _Cdi; }
            set { _Cdi = value; }
        }

        public int Folio
        {
            get { return _Folio; }
            set { _Folio = value; }
        }

        public string Estatus
        {
            get { return _Estatus; }
            set { _Estatus = value; }
        }

        public int NumCliente
        {
            get { return _NumCliente; }
            set { _NumCliente = value; }
        }

        public string Cliente
        {
            get { return _Cliente; }
            set { _Cliente = value; }
        }

        public string Territorio
        {
            get { return _Territorio; }
            set { _Territorio = value; }
        }

        public int Rik
        {
            get { return _Rik; }
            set { _Rik = value; }
        }

        public string FechaACyS
        {
            get { return _FechaACyS; }
            set { _FechaACyS = value; }
        }

        public string FechaInicio
        {
            get { return _FechaInicio; }
            set { _FechaInicio = value; }
        }

        public string FechaFin
        {
            get { return _FechaFin; }
            set { _FechaFin = value; }
        }

        public string TipoCuenta
        {
            get { return _TipoCuenta; }
            set { _TipoCuenta = value; }
        }

        public string Vencido
        {
            get { return _Vencido; }
            set { _Vencido = value; }
        }

    }


    public class DashboardACyS_CDIACyS
    {
        /// Cd_Tipo	Id_Cd	CDI

        private int _Id_Cd;
        private int _Cd_Tipo;
        private string _CDI;

        public int Id_Cd
        {
            get { return _Id_Cd; }
            set { _Id_Cd = value; }
        }

        public int Cd_Tipo
        {
            get { return _Cd_Tipo; }
            set { _Cd_Tipo = value; }
        }

        public string CDI
        {
            get { return _CDI; }
            set { _CDI = value; }
        }

    }


    #region RVD

    public class RVDIndicadores
    {
        private int _idCdi;
        private string _TotalAcumulado;
        private string _TotalUnidades;
        private string _TotalTrimestre;
        private string _TotalDelMes;
        private string _strTrimestre;
        private string _strEsteMes;

        public int IdCdi
        {
            get { return _idCdi; }
            set { _idCdi = value; }
        }

        public string TotalAcumulado
        {
            get { return _TotalAcumulado; }
            set { _TotalAcumulado = value; }
        }

        public string TotalUnidades
        {
            get { return _TotalUnidades; }
            set { _TotalUnidades = value; }
        }

        public string TotalTrimestre
        {
            get { return _TotalTrimestre; }
            set { _TotalTrimestre = value; }
        }

        public string TotalDelMes
        {
            get { return _TotalDelMes; }
            set { _TotalDelMes = value; }
        }

        public string strEsteMes
        {
            get { return _strEsteMes; }
            set { _strEsteMes = value; }
        }

        public string strTrimestre
        {
            get { return _strTrimestre; }
            set { _strTrimestre = value; }
        }
    }

    public class RVDReporte
    {
        //  Id Nombre
        ////    IdPrd	PdrDescrip	IdAgrupa	Agrupador
        //  Enero U Enero Febrero U Febrero   Marzo U Marzo
        //  Abril   U Abril Mayo U Mayo Junio   U Junio
        //  Julio U Julio Agosto  U Agosto    Septiembre U Septiembre
        //  Octubre U Octubre   Noviembre U Noviembre Diciembre   U Diciembre
        //  Total U Total
        private int _Id;
        private string _Nombre;
        private Int64 _IdPrd;
        private string _PdrDescrip;
        private int _IdAgrupa;
        private string _Agrupador;

        private float _Enero;
        private int _UnEnero;
        private float _UtEnero;
        private float _Febrero;
        private int _UnFebrero;
        private float _UtFebrero;
        private float _Marzo;
        private int _UnMarzo;
        private float _UtMarzo;

        private float _Abril;
        private int _UnAbril;
        private float _UtAbril;
        private float _Mayo;
        private int _UnMayo;
        private float _UtMayo;
        private float _Junio;
        private int _UnJunio;
        private float _UtJunio;

        private float _Julio;
        private int _UnJulio;
        private float _UtJulio;
        private float _Agosto;
        private int _UnAgosto;
        private float _UtAgosto;
        private float _Septiembre;
        private int _UnSeptiembre;
        private float _UtSeptiembre;

        private float _Octubre;
        private int _UnOctubre;
        private float _UtOctubre;
        private float _Noviembre;
        private int _UnNoviembre;
        private float _UtNoviembre;
        private float _Diciembre;
        private int _UnDiciembre;
        private float _UtDiciembre;

        private float _Total;
        private int _UnTotal;
        private float _UtTotal;

        public int Id
        {
            get { return _Id; }
            set { _Id = value; }
        }

        public string Nombre
        {
            get { return _Nombre; }
            set { _Nombre = value; }
        }
        public Int64 IdPrd
        {
            get { return _IdPrd; }
            set { _IdPrd = value; }
        }

        public string PdrDescrip
        {
            get { return _PdrDescrip; }
            set { _PdrDescrip = value; }
        }
        public int IdAgrupa
        {
            get { return _IdAgrupa; }
            set { _IdAgrupa = value; }
        }

        public string Agrupador
        {
            get { return _Agrupador; }
            set { _Agrupador = value; }
        }


        public float Enero
        {
            get { return _Enero; }
            set { _Enero = value; }
        }
        public int UnEnero
        {
            get { return _UnEnero; }
            set { _UnEnero = value; }
        }
        public float UtEnero
        {
            get { return _UtEnero; }
            set { _UtEnero = value; }
        }

        public float Febrero
        {
            get { return _Febrero; }
            set { _Febrero = value; }
        }
        public int UnFebrero
        {
            get { return _UnFebrero; }
            set { _UnFebrero = value; }
        }
        public float UtFebrero
        {
            get { return _UtFebrero; }
            set { _UtFebrero = value; }
        }

        public float Marzo
        {
            get { return _Marzo; }
            set { _Marzo = value; }
        }
        public int UnMarzo
        {
            get { return _UnMarzo; }
            set { _UnMarzo = value; }
        }
        public float UtMarzo
        {
            get { return _UtMarzo; }
            set { _UtMarzo = value; }
        }

        public float Abril
        {
            get { return _Abril; }
            set { _Abril = value; }
        }
        public int UnAbril
        {
            get { return _UnAbril; }
            set { _UnAbril = value; }
        }
        public float UtAbril
        {
            get { return _UtAbril; }
            set { _UtAbril = value; }
        }

        public float Mayo
        {
            get { return _Mayo; }
            set { _Mayo = value; }
        }
        public int UnMayo
        {
            get { return _UnMayo; }
            set { _UnMayo = value; }
        }
        public float UtMayo
        {
            get { return _UtMayo; }
            set { _UtMayo = value; }
        }

        public float Junio
        {
            get { return _Junio; }
            set { _Junio = value; }
        }
        public int UnJunio
        {
            get { return _UnJunio; }
            set { _UnJunio = value; }
        }
        public float UtJunio
        {
            get { return _UtJunio; }
            set { _UtJunio = value; }
        }

        public float Julio
        {
            get { return _Julio; }
            set { _Julio = value; }
        }
        public int UnJulio
        {
            get { return _UnJulio; }
            set { _UnJulio = value; }
        }
        public float UtJulio
        {
            get { return _UtJulio; }
            set { _UtJulio = value; }
        }

        public float Agosto
        {
            get { return _Agosto; }
            set { _Agosto = value; }
        }
        public int UnAgosto
        {
            get { return _UnAgosto; }
            set { _UnAgosto = value; }
        }
        public float UtAgosto
        {
            get { return _UtAgosto; }
            set { _UtAgosto = value; }
        }

        public float Septiembre
        {
            get { return _Septiembre; }
            set { _Septiembre = value; }
        }
        public int UnSeptiembre
        {
            get { return _UnSeptiembre; }
            set { _UnSeptiembre = value; }
        }
        public float UtSeptiembre
        {
            get { return _UtSeptiembre; }
            set { _UtSeptiembre = value; }
        }

        public float Octubre
        {
            get { return _Octubre; }
            set { _Octubre = value; }
        }
        public int UnOctubre
        {
            get { return _UnOctubre; }
            set { _UnOctubre = value; }
        }
        public float UtOctubre
        {
            get { return _UtOctubre; }
            set { _UtOctubre = value; }
        }

        public float Noviembre
        {
            get { return _Noviembre; }
            set { _Noviembre = value; }
        }
        public int UnNoviembre
        {
            get { return _UnNoviembre; }
            set { _UnNoviembre = value; }
        }
        public float UtNoviembre
        {
            get { return _UtNoviembre; }
            set { _UtNoviembre = value; }
        }

        public float Diciembre
        {
            get { return _Diciembre; }
            set { _Diciembre = value; }
        }
        public int UnDiciembre
        {
            get { return _UnDiciembre; }
            set { _UnDiciembre = value; }
        }
        public float UtDiciembre
        {
            get { return _UtDiciembre; }
            set { _UtDiciembre = value; }
        }

        public float Total
        {
            get { return _Total; }
            set { _Total = value; }
        }
        public int UnTotal
        {
            get { return _UnTotal; }
            set { _UnTotal = value; }
        }
        public float UtTotal
        {
            get { return _UtTotal; }
            set { _UtTotal = value; }
        }


    }


    public class RVDSemanal
    {
        #region Atributos
        private int id_TerSrv;
        private string nom_TerSrv;
        private int id_Ter;
        private string nom_Ter;
        private int id_Cte;
        private string nom_Cte;
        private long id_prd;
        private string nom_Prd;
        private int unidades;
        private float importe;
        private int anio;
        private int semana;
        private string mes;

        #endregion

        #region Metodos

        public int Id_TerSrv
        {
            get { return id_TerSrv; }
            set { id_TerSrv = value; }
        }

        public string Nom_TerSrv
        {
            get { return nom_TerSrv; }
            set { nom_TerSrv = value; }
        }

        public int Id_Ter
        {
            get { return id_Ter; }
            set { id_Ter = value; }
        }

        public string Nom_Ter
        {
            get { return nom_Ter; }
            set { nom_Ter = value; }
        }

        public int Id_Cte
        {
            get { return id_Cte; }
            set { id_Cte = value; }
        }

        public string Nom_Cte
        {
            get { return nom_Cte; }
            set { nom_Cte = value; }
        }

        public long Id_prd
        {
            get { return id_prd; }
            set { id_prd = value; }
        }

        public string Nom_Prd
        {
            get { return nom_Prd; }
            set { nom_Prd = value; }
        }

        public int Unidades
        {
            get { return unidades; }
            set { unidades = value; }
        }

        public float Importe
        {
            get { return importe; }
            set { importe = value; }
        }

        public int Anio
        {
            get { return anio; }
            set { anio = value; }
        }

        public int Semana
        {
            get { return semana; }
            set { semana = value; }
        }

        public string Mes
        {
            get { return mes; }
            set { mes = value; }
        }
        #endregion
    }

    #endregion

}