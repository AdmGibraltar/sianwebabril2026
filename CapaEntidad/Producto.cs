using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    [Serializable]
    public class Producto
    {
        private int _Id_Emp;
        private int _Prd_Cuota;
        private int _Id_Cd;
        private long _Id_Prd;
        private int _Id_Spo;
        private int _Id_Ptp;
        private int _Id_Cpr;
        private int _Id_Fam;
        private int _Id_Sub;
        private int _Id_Pvd;
        private string _Prd_Descripcion;
        private string _Prd_SGCUP;
        private string _Prd_Presentacion;
        private int _Prd_InvInicial;
        private int _Prd_InvFinal;
        private int _Prd_AgrupadoSpo;
        private bool? _Prd_AparatoSisProp;
        private int _Prd_Fisico;
        private int _Prd_Ordenado;
        private int _Prd_Asignado;
        private int _Prd_InvSeg;
        private int _Prd_TTrans;
        private int _Prd_TEntre;
        private int _Prd_Transito;
        private string _Prd_UniNe;
        private string _Prd_UniNs;
        private int _Prd_Unico;
        private float _Prd_UniEmp;
        private bool _Prd_Colo;
        private char _Prd_Ren;
        private int _Prd_Mes;
        private string _Prd_CLNomFab;
        private string _Prd_CLIdFab;
        private string _Prd_CLDesFab;
        private string _Prd_CLPreFab;
        private string _Prd_CLNomPro;
        private string _Prd_CLIdPro;
        private string _Prd_CLDesPro;
        private string _Prd_CLPrePro;
        private int _Prd_MaxExistencia;
        private int _Prd_Minimo;
        private string _Prd_PlanAbasto;
        private string _Prd_Ubicacion;
        private float _Prd_Contribucion;
        private double _Prd_PorUtilidades;
        private bool _Prd_Nuevo;
        private double _Prd_PesConTecnico;
        private string _Prd_CptSv;
        private int _Prd_Activo;
        string _Prd_Precio;
        string _Prd_PrecioLista;
        string _Prd_UniNom;
        private DateTime _Fecha;
        private int _Tprecio;
        private DateTime _Prd_FechaFinEsp;
        private int _Sol_PEsp;
        private DateTime _prd_FecAlta;
        private float _Prd_FactorConv;
        private int _Prd_Sobrante;
        private int _Prd_Pendiente;
        private List<ProductoPrecios> _listaProductoPrecios;
        private string _Filtro_Nombre;
        private string _Filtro_PrdIni;
        private string _Filtro_PrdFin;
        private string _Uni_Descripcion;
        public List<double> ventaMes;
        public List<string> ventaMesDescr;
        private int _Id_Pinv;
        public double? CantFact;
        public DateTime? UltimaVta;
        private string _TieneComentarios;
        private string _Prd_ClaveProdServ;
        private string _Prd_ClaveProdServDescripcion;
        private string _Prd_ClaveUnidad;
        private string _Prd_ClaveUnidadDescripcion;


        private bool _ServTecnicoRellenoMensual;
        private DateTime? _ServTecnicoRellenoMensualfechaIni;

        private bool _ServTecnicoRellenoBimestral;
        private DateTime? _ServTecnicoRellenoBimestralfechaIni;



        private bool _ServTecnicoRellenoTrimestral;
        private DateTime? _ServTecnicoRellenoTrimestralfechaIni;

        /// <summary>
        /// Campo mapeado a la columna [Prd_NoFacturable]
        /// </summary>
        private bool _Prd_NoFacturable = false;

        /// <summary>
        /// Propiedad mapeada a la columna [Prd_NoFacturable]
        /// </summary>
        public bool NoFacturable
        {
            get
            {
                return _Prd_NoFacturable;
            }
            set
            {
                _Prd_NoFacturable = value;
            }
        }

        public bool ServTecnicoRellenoMensual
        {
            get { return _ServTecnicoRellenoMensual; }
            set { _ServTecnicoRellenoMensual = value; }
        }

        public DateTime? ServTecnicoRellenoMensualfechaIni
        {
            get { return _ServTecnicoRellenoMensualfechaIni; }
            set { _ServTecnicoRellenoMensualfechaIni = value; }
        }

        public bool ServTecnicoRellenoBimestral
        {
            get { return _ServTecnicoRellenoBimestral; }
            set { _ServTecnicoRellenoBimestral = value; }
        }

        public DateTime? ServTecnicoRellenoBimestralfechaIni
        {
            get { return _ServTecnicoRellenoBimestralfechaIni; }
            set { _ServTecnicoRellenoBimestralfechaIni = value; }
        }

        public bool ServTecnicoRellenoTrimestral
        {
            get { return _ServTecnicoRellenoTrimestral; }
            set { _ServTecnicoRellenoTrimestral = value; }
        }

        public DateTime? ServTecnicoRellenoTrimestralfechaIni
        {
            get { return _ServTecnicoRellenoTrimestralfechaIni; }
            set { _ServTecnicoRellenoTrimestralfechaIni = value; }
        }

        private bool _ServMantenimientoMensual;
        private DateTime? _ServMantenimientoMensualfechaIni;

        private bool _ServMantenimientoBimestral;
        private DateTime? _ServMantenimientoBimestralfechaIni;

        private bool _ServMantenimientoTrimestral;
        private DateTime? _ServMantenimientoTrimestralfechaIni;

        public bool ServMantenimientoMensual
        {
            get { return _ServMantenimientoMensual; }
            set { _ServMantenimientoMensual = value; }
        }

        public DateTime? ServMantenimientoMensualfechaIni
        {
            get { return _ServMantenimientoMensualfechaIni; }
            set { _ServMantenimientoMensualfechaIni = value; }
        }

        public bool ServMantenimientoBimestral
        {
            get { return _ServMantenimientoBimestral; }
            set { _ServMantenimientoBimestral = value; }
        }

        public DateTime? ServMantenimientoBimestralfechaIni
        {
            get { return _ServMantenimientoBimestralfechaIni; }
            set { _ServMantenimientoBimestralfechaIni = value; }
        }

        public bool ServMantenimientoTrimestral
        {
            get { return _ServMantenimientoTrimestral; }
            set { _ServMantenimientoTrimestral = value; }
        }

        public DateTime? ServMantenimientoTrimestralfechaIni
        {
            get { return _ServMantenimientoTrimestralfechaIni; }
            set { _ServMantenimientoTrimestralfechaIni = value; }
        }

        public string TieneComentarios
        {
            get { return _TieneComentarios; }
            set { _TieneComentarios = value; }
        }

        public int Id_Pinv
        {
            get { return _Id_Pinv; }
            set { _Id_Pinv = value; }
        }
        public string Uni_Descripcion
        {
            get { return _Uni_Descripcion; }
            set { _Uni_Descripcion = value; }
        }
        public double Prd_PorUtilidades
        {
            get { return _Prd_PorUtilidades; }
            set { _Prd_PorUtilidades = value; }
        }
        public int Id_Emp
        {
            get { return _Id_Emp; }
            set { _Id_Emp = value; }
        }

        public int Prd_Cuota
        {
            get { return _Prd_Cuota; }
            set { _Prd_Cuota = value; }
        }


        public int Prd_Minimo
        {
            get { return _Prd_Minimo; }
            set { _Prd_Minimo = value; }
        }

        public int Id_Cd
        {
            get { return _Id_Cd; }
            set { _Id_Cd = value; }
        }
        public long Id_Prd
        {
            get { return _Id_Prd; }
            set { _Id_Prd = value; }
        }
        public int Id_Spo
        {
            get { return _Id_Spo; }
            set { _Id_Spo = value; }
        }
        public int Id_Ptp
        {
            get { return _Id_Ptp; }
            set { _Id_Ptp = value; }
        }
        public int Id_Cpr
        {
            get { return _Id_Cpr; }
            set { _Id_Cpr = value; }
        }
        public int Id_Fam
        {
            get { return _Id_Fam; }
            set { _Id_Fam = value; }
        }
        public int Id_Sub
        {
            get { return _Id_Sub; }
            set { _Id_Sub = value; }
        }
        public int Id_Pvd
        {
            get { return _Id_Pvd; }
            set { _Id_Pvd = value; }
        }
        public string Prd_Descripcion
        {
            get { return _Prd_Descripcion; }
            set { _Prd_Descripcion = value; }
        }

        public string Prd_SGCUP
        {
            get { return _Prd_SGCUP; }
            set { _Prd_SGCUP = value; }
        }

        public string Prd_PlanAbasto
        {
            get { return _Prd_PlanAbasto; }
            set { _Prd_PlanAbasto = value; }
        }

        public string Prd_Presentacion
        {
            get { return _Prd_Presentacion; }
            set { _Prd_Presentacion = value; }
        }
        public int Prd_InvInicial
        {
            get { return _Prd_InvInicial; }
            set { _Prd_InvInicial = value; }
        }
        public int Prd_InvFinal
        {
            get { return _Prd_InvFinal; }
            set { _Prd_InvFinal = value; }
        }
        public int Prd_AgrupadoSpo
        {
            get { return _Prd_AgrupadoSpo; }
            set { _Prd_AgrupadoSpo = value; }
        }
        public float Prd_FactorConv
        {
            get { return _Prd_FactorConv; }
            set { _Prd_FactorConv = value; }
        }
        public bool? Prd_AparatoSisProp
        {
            get { return _Prd_AparatoSisProp; }
            set { _Prd_AparatoSisProp = value; }
        }
        public int Prd_Fisico
        {
            get { return _Prd_Fisico; }
            set { _Prd_Fisico = value; }
        }
        public int Prd_Ordenado
        {
            get { return _Prd_Ordenado; }
            set { _Prd_Ordenado = value; }
        }
        public int Prd_Asignado
        {
            get { return _Prd_Asignado; }
            set { _Prd_Asignado = value; }
        }
        public int Prd_InvSeg
        {
            get { return _Prd_InvSeg; }
            set { _Prd_InvSeg = value; }
        }
        public int Prd_TTrans
        {
            get { return _Prd_TTrans; }
            set { _Prd_TTrans = value; }
        }
        public int Prd_TEntre
        {
            get { return _Prd_TEntre; }
            set { _Prd_TEntre = value; }
        }
        public int Prd_Transito
        {
            get { return _Prd_Transito; }
            set { _Prd_Transito = value; }
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
        public int Prd_Unico
        {
            get { return _Prd_Unico; }
            set { _Prd_Unico = value; }
        }
        public float Prd_UniEmp
        {
            get { return _Prd_UniEmp; }
            set { _Prd_UniEmp = value; }
        }
        public bool Prd_Colo
        {
            get { return _Prd_Colo; }
            set { _Prd_Colo = value; }
        }
        public char Prd_Ren
        {
            get { return _Prd_Ren; }
            set { _Prd_Ren = value; }
        }
        public int Prd_Mes
        {
            get { return _Prd_Mes; }
            set { _Prd_Mes = value; }
        }
        public string Prd_CLNomFab
        {
            get { return _Prd_CLNomFab; }
            set { _Prd_CLNomFab = value; }
        }
        public string Prd_CLIdFab
        {
            get { return _Prd_CLIdFab; }
            set { _Prd_CLIdFab = value; }
        }
        public string Prd_CLDesFab
        {
            get { return _Prd_CLDesFab; }
            set { _Prd_CLDesFab = value; }
        }
        public string Prd_CLPreFab
        {
            get { return _Prd_CLPreFab; }
            set { _Prd_CLPreFab = value; }
        }
        public string Prd_CLNomPro
        {
            get { return _Prd_CLNomPro; }
            set { _Prd_CLNomPro = value; }
        }
        public string Prd_CLIdPro
        {
            get { return _Prd_CLIdPro; }
            set { _Prd_CLIdPro = value; }
        }
        public string Prd_CLDesPro
        {
            get { return _Prd_CLDesPro; }
            set { _Prd_CLDesPro = value; }
        }
        public string Prd_CLPrePro
        {
            get { return _Prd_CLPrePro; }
            set { _Prd_CLPrePro = value; }
        }
        public int Prd_MaxExistencia
        {
            get { return _Prd_MaxExistencia; }
            set { _Prd_MaxExistencia = value; }
        }
        public string Prd_Ubicacion
        {
            get { return _Prd_Ubicacion; }
            set { _Prd_Ubicacion = value; }
        }
        public float Prd_Contribucion
        {
            get { return _Prd_Contribucion; }
            set { _Prd_Contribucion = value; }
        }
        public bool Prd_Nuevo
        {
            get { return _Prd_Nuevo; }
            set { _Prd_Nuevo = value; }
        }
        public double Prd_PesConTecnico
        {
            get { return _Prd_PesConTecnico; }
            set { _Prd_PesConTecnico = value; }
        }
        public string Prd_CptSv
        {
            get { return _Prd_CptSv; }
            set { _Prd_CptSv = value; }
        }
        public int Prd_Activo
        {
            get { return _Prd_Activo; }
            set { _Prd_Activo = value; }
        }
        public string Prd_Precio
        {
            get { return _Prd_Precio; }
            set { _Prd_Precio = value; }
        }

        public string Prd_PrecioLista
        {
            get { return _Prd_PrecioLista; }
            set { _Prd_PrecioLista = value; }
        }

        public string Prd_UniNom
        {
            get { return _Prd_UniNom; }
            set { _Prd_UniNom = value; }
        }
        public DateTime Fecha
        {
            get { return _Fecha; }
            set { _Fecha = value; }
        }
        public int Tprecio
        {
            get { return _Tprecio; }
            set { _Tprecio = value; }
        }
        public DateTime Prd_FechaFinEsp
        {
            get { return _Prd_FechaFinEsp; }
            set { _Prd_FechaFinEsp = value; }
        }
        public int Sol_PEsp
        {
            get { return _Sol_PEsp; }
            set { _Sol_PEsp = value; }
        }
        public DateTime Prd_FecAlta
        {
            get { return _prd_FecAlta; }
            set { _prd_FecAlta = value; }
        }
        public List<ProductoPrecios> ListaProductoPrecios
        {
            get { return _listaProductoPrecios; }
            set { _listaProductoPrecios = value; }
        }
        public string Filtro_Nombre
        {
            get { return _Filtro_Nombre; }
            set { _Filtro_Nombre = value; }
        }
        public string Filtro_PrdIni
        {
            get { return _Filtro_PrdIni; }
            set { _Filtro_PrdIni = value; }
        }
        public string Filtro_PrdFin
        {
            get { return _Filtro_PrdFin; }
            set { _Filtro_PrdFin = value; }
        }
        public int Prd_Pendiente
        {
            get { return _Prd_Pendiente; }
            set { _Prd_Pendiente = value; }
        }
        public int Prd_Sobrante
        {
            get { return _Prd_Sobrante; }
            set { _Prd_Sobrante = value; }
        }
        string _Prd_DescripcionEspecial;
        private int? _Id_Ter;
        private int? _Id_Mov;
        public string Id_PrdEsp;
        private int _prd_Disponible;
        private string _Relacion;
        private int _Id_Cte;
        private double? _Prd_AAA;
        public int? EmpBen;
        private string _Prd_AAACadena;

        public double? Prd_AAA
        {
            get { return _Prd_AAA; }
            set { _Prd_AAA = value; }
        }


        public string Prd_AAACadena
        {
            get { return _Prd_AAACadena; }
            set { _Prd_AAACadena = value; }
        }

        public int Id_Cte
        {
            get { return _Id_Cte; }
            set { _Id_Cte = value; }
        }

        public string Relacion
        {
            get { return _Relacion; }
            set { _Relacion = value; }
        }

        public int Prd_Disponible
        {
            get { return _prd_Disponible; }
            set { _prd_Disponible = value; }
        }

        public int? Id_Mov
        {
            get { return _Id_Mov; }
            set { _Id_Mov = value; }
        }

        private string _U_Descripcion;
        public string U_Descripcion
        {
            get { return _U_Descripcion; }
            set { _U_Descripcion = value; }
        }


        public int? Id_Ter
        {
            get { return _Id_Ter; }
            set { _Id_Ter = value; }
        }




        public string Prd_DescripcionEspecial { get { return _Prd_DescripcionEspecial; } set { _Prd_DescripcionEspecial = value; } }


        private List<Producto> _list_producto;

        public List<Producto> List_producto
        {
            get { return _list_producto; }
            set { _list_producto = value; }
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

        //quejas 

        private int id_folio;
        private string descripcion;
        private int cantidad;
        private string presentacion;
        private string lote;
        private DateTime caducidad;
        private string marca;
        private double costo;
        private int num_Fac;
        private string nom_Prov;
        private int id_Queja;
        private DateTime fabricacion;

        //Campos nuevos control de cambios
        //04-02-2018

        private int id_motivo;

        public int Id_motivo
        {
            get { return id_motivo; }
            set { id_motivo = value; }
        }
        private string motivo;

        public string Motivo
        {
            get { return motivo; }
            set { motivo = value; }
        }

        private bool mantener;

        public bool Mantener
        {
            get { return mantener; }
            set { mantener = value; }
        }
        private int transferencia;

        public int Transferencia
        {
            get { return transferencia; }
            set { transferencia = value; }
        }
        private int rem_Trans;

        public int Rem_Trans
        {
            get { return rem_Trans; }
            set { rem_Trans = value; }
        }
        private double costoestandar;

        public double Costoestandar
        {
            get { return costoestandar; }
            set { costoestandar = value; }
        }
        private int ncredito;
        public int idDocumento;
        private bool conservarProd;
        private string pvd_Descripcion;

        public string Pvd_Descripcion
        {
            get { return pvd_Descripcion; }
            set { pvd_Descripcion = value; }
        }

        public bool ConservarProd
        {
            get { return conservarProd; }
            set { conservarProd = value; }
        }

        public int IdDocumento
        {
            get { return idDocumento; }
            set { idDocumento = value; }
        }

        public int NCredito
        {
            get { return ncredito; }
            set { ncredito = value; }
        }


        public int Id_folio
        {
            get { return id_folio; }
            set { id_folio = value; }
        }

        public string Descripcion
        {
            get { return descripcion; }
            set { descripcion = value; }
        }

        public int Cantidad
        {
            get { return cantidad; }
            set { cantidad = value; }
        }

        public string Presentacion
        {
            get { return presentacion; }
            set { presentacion = value; }
        }

        public string Lote
        {
            get { return lote; }
            set { lote = value; }
        }

        public DateTime Caducidad
        {
            get { return caducidad; }
            set { caducidad = value; }
        }

        public string Marca
        {
            get { return marca; }
            set { marca = value; }
        }

        public double Costo
        {
            get { return costo; }
            set { costo = value; }
        }

        public int Num_Fac
        {
            get { return num_Fac; }
            set { num_Fac = value; }
        }

        public string Nom_Prov
        {
            get { return nom_Prov; }
            set { nom_Prov = value; }
        }

        public DateTime Fabricacion
        {
            get { return fabricacion; }
            set { fabricacion = value; }
        }

        public int Id_Queja
        {
            get { return id_Queja; }
            set { id_Queja = value; }
        }

        //JUN17-2020

        private string _ImagenProductoAltaRes;
        public string ImagenProductoAltaRes
        {
            get { return _ImagenProductoAltaRes; }
            set { _ImagenProductoAltaRes = value; }
        }

        private string _ImagenProductoBajaRes;
        public string ImagenProductoBajaRes
        {
            get { return _ImagenProductoBajaRes; }
            set { _ImagenProductoBajaRes = value; }
        }

        //11Abr2022 RFH

        private string _Aplicacion;
        public string Aplicacion
        {
            get { return _Aplicacion; }
            set { _Aplicacion = value; }
        }

        private string _Subfamilia;
        public string Subfamilia
        {
            get { return _Subfamilia; }
            set { _Subfamilia = value; }
        }

        public double Prd_PLista { get; set; }
        /*
        public string Aplicacion { get; set; }
        public string Subfamilia { get; set; }
        */
        public int MultiploVenta { get; set; }

        private string _Prd_IdProvCentral;

        public string Prd_IdProvCentral
        {
            get { return _Prd_IdProvCentral; }
            set { _Prd_IdProvCentral = value; }
        }

        private string _Prd_NomProvCentral;

        public string Prd_NomProvCentral
        {
            get { return _Prd_NomProvCentral; }
            set { _Prd_NomProvCentral = value; }
        }


        //Campos Nuevo Dic 2023
        //RBM

        private string _Prd_CodigoProv;
        private string _Prd_DescripcionProv;
        private string _Prd_PresentacionProv;
        private string _Prd_FechaInicio;
        private string _Prd_FechaFin;
        private int _Id_ClaveSAT;
        private int _Id_UnidadSAT;
        private int _Prd_IdProvLocal;
        private string _Prd_NomProvLocal;
        private string _Prd_NomFamilia;
        private string _Prd_NomSubFamilia;

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

        public string Prd_FechaInicio
        {
            get { return _Prd_FechaInicio; }
            set { _Prd_FechaInicio = value; }
        }

        public string Prd_FechaFin
        {
            get { return _Prd_FechaFin; }
            set { _Prd_FechaFin = value; }
        }

        public int Id_ClaveSAT
        {
            get { return _Id_ClaveSAT; }
            set { _Id_ClaveSAT = value; }
        }

        public int Id_UnidadSAT
        {
            get { return _Id_UnidadSAT; }
            set { _Id_UnidadSAT = value; }
        }

        public int Prd_IdProvLocal
        {
            get { return _Prd_IdProvLocal; }
            set { _Prd_IdProvLocal = value; }
        }

        public string Prd_NomProvLocal
        {
            get { return _Prd_NomProvLocal; }
            set { _Prd_NomProvLocal = value; }
        }


        public string Prd_NomFamilia
        {
            get { return _Prd_NomFamilia; }
            set { _Prd_NomFamilia = value; }
        }


        public string Prd_NomSubFamilia
        {
            get { return _Prd_NomSubFamilia; }
            set { _Prd_NomSubFamilia = value; }
        }

        ///     Validaciones Ordenes de Compra Extraordinarias

        string _VtaCPromedio;
        private int _VtaUPromedio;
        string _Prd_Notas;


        public string VtaCPromedio
        {
            get { return _VtaCPromedio; }
            set { _VtaCPromedio = value; }
        }

        public int VtaUPromedio
        {
            get { return _VtaUPromedio; }
            set { _VtaUPromedio = value; }
        }

        public string Prd_Notas
        {
            get { return _Prd_Notas; }
            set { _Prd_Notas = value; }
        }


    }
}