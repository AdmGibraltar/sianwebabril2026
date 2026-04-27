using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*
 Abr9-2019  Actualizado RFH 
 FEB28-2020 Actualizacion RFH   
*/


namespace CapaEntidad
{
    public class eAcys2
    {

        private int _RecordCount;
        public int RecordCount
        {
            get { return _RecordCount; }
            set { _RecordCount = value; }
        }

        private int _Id_Emp;
        public int Id_Emp { get { return _Id_Emp; } set { _Id_Emp = value; } }

        private int _Id_Cd;
        public int Id_Cd { get { return _Id_Cd; } set { _Id_Cd = value; } }

        private Int32 _Id_Ter;
        public Int32 Id_Ter { get { return _Id_Ter; } set { _Id_Ter = value; } }

        private int _Id_Cte;
        public int Id_Cte { get { return _Id_Cte; } set { _Id_Cte = value; } }

        private int _Id_Ade;
        public int Id_Ade { get { return _Id_Ade; } set { _Id_Ade = value; } }

        private string _Cte_PagoUsoCFDI;
        public string Cte_PagoUsoCFDI { get { return _Cte_PagoUsoCFDI; } set { _Cte_PagoUsoCFDI = value; } }

        private string _Cte_NomComercial;
        public string Cte_NomComercial { get { return _Cte_NomComercial; } set { _Cte_NomComercial = value; } }

        private string _Identificador;
        public string Identificador { get { return _Identificador; } set { _Identificador = value; } }

        private int _Id_Rik;
        public int Id_Rik { get { return _Id_Rik; } set { _Id_Rik = value; } }

        private int _IDCN;
        public int IDCN
        {
            get { return _IDCN; }
            set { _IDCN = value; }
        }

        private int _TipoCuenta;
        public int TipoCuenta
        {
            get { return _TipoCuenta; }
            set { _TipoCuenta = value; }
        }

        private string _CNDescripcion;
        public string CNDescripcion
        {
            get
            {
                return _CNDescripcion;
            }
            set { _CNDescripcion = value; }
        }

        // 8 Ene 2018

        private int _Id_Rik_Actual;
        public int Id_Rik_Actual { get { return _Id_Rik_Actual; } set { _Id_Rik_Actual = value; } }

        private int _Id_Acs;
        public int Id_Acs { get { return _Id_Acs; } set { _Id_Acs = value; } }

        private int _Id_AcsVersion;
        public int Id_AcsVersion { get { return _Id_AcsVersion; } set { _Id_AcsVersion = value; } }

        private int _Id_CteDirEntrega;
        public int Id_CteDirEntrega { get { return _Id_CteDirEntrega; } set { _Id_CteDirEntrega = value; } }

        private int _Id_Val;
        public int Id_Val { get { return _Id_Val; } set { _Id_Val = value; } }

        private int _EsCuentaNacional;
        public int EsCuentaNacional { get { return _EsCuentaNacional; } set { _EsCuentaNacional = value; } }

        private string _Acs_Estatus;
        public string Acs_Estatus { get { return _Acs_Estatus; } set { _Acs_Estatus = value; } }

        private string _Acs_EstatusTexto;
        public string Acs_EstatusTexto { get { return _Acs_EstatusTexto; } set { _Acs_EstatusTexto = value; } }


        private string _Cte_Nombre;
        public string Cte_Nombre { get { return _Cte_Nombre; } set { _Cte_Nombre = value; } }

        private string _Acs_Fecha;
        public string Acs_Fecha { get { return _Acs_Fecha; } set { _Acs_Fecha = value; } }

        private string _Acs_ConsigFechaFin;
        public string Acs_ConsigFechaFin { get { return _Acs_ConsigFechaFin; } set { _Acs_ConsigFechaFin = value; } }

        private int _Acs_CanTotal;
        public int Acs_CanTotal { get { return _Acs_CanTotal; } set { _Acs_CanTotal = value; } }

        private int _Acs_VisFrecuencia;
        public int Acs_VisFrecuencia { get { return _Acs_VisFrecuencia; } set { _Acs_VisFrecuencia = value; } }

        // _Acs_FechaInicioDocumento
        private string _Acs_FechaInicio;
        public string Acs_FechaInicio { get { return _Acs_FechaInicio; } set { _Acs_FechaInicio = value; } }

        private string _Acs_FechaFin;
        public string Acs_FechaFin { get { return _Acs_FechaFin; } set { _Acs_FechaFin = value; } }
        private string _Acs_EstatusStr;
        public string Acs_EstatusStr { get { return _Acs_EstatusStr; } set { _Acs_EstatusStr = value; } }
        private string _Filtro_FecIni;
        public string Filtro_FecIni { get { return _Filtro_FecIni; } set { _Filtro_FecIni = value; } }
        private string _Filtro_FecFin;
        public string Filtro_FecFin { get { return _Filtro_FecFin; } set { _Filtro_FecFin = value; } }
        private string _Filtro_Estatus;
        public string Filtro_Estatus { get { return _Filtro_Estatus; } set { _Filtro_Estatus = value; } }

        private int _Filtro_FolIni;
        public int Filtro_FolIni { get { return _Filtro_FolIni; } set { _Filtro_FolIni = value; } }

        private int _Filtro_FolFin;
        public int Filtro_FolFin { get { return _Filtro_FolFin; } set { _Filtro_FolFin = value; } }


        private string _Filtro_Usuario;
        public string Filtro_Usuario { get { return _Filtro_Usuario; } set { _Filtro_Usuario = value; } }

        //private int _id_Rik;
        //public int id_Rik { get { return _id_Rik; } set { _id_Rik = value; } }

        private string _Acs_Contacto;
        public string Acs_Contacto { get { return _Acs_Contacto; } set { _Acs_Contacto = value; } }
        private string _Acs_Puesto;
        public string Acs_Puesto { get { return _Acs_Puesto; } set { _Acs_Puesto = value; } }

        private string _Acs_Telefono;
        public string Acs_Telefono { get { return _Acs_Telefono; } set { _Acs_Telefono = value; } }

        private string _Acs_email;
        public string Acs_email { get { return _Acs_email; } set { _Acs_email = value; } }
        private string _Acs_Contacto2;
        public string Acs_Contacto2 { get { return _Acs_Contacto2; } set { _Acs_Contacto2 = value; } }
        private string _Acs_Telefono2;
        public string Acs_Telefono2 { get { return _Acs_Telefono2; } set { _Acs_Telefono2 = value; } }
        private string _Acs_email2;
        public string Acs_email2 { get { return _Acs_email2; } set { _Acs_email2 = value; } }

        private string _Acs_Contacto3;
        public string Acs_Contacto3 { get { return _Acs_Contacto3; } set { _Acs_Contacto3 = value; } }
        private string _Acs_Telefono3;
        public string Acs_Telefono3 { get { return _Acs_Telefono3; } set { _Acs_Telefono3 = value; } }
        private string _Acs_email3;
        public string Acs_email3 { get { return _Acs_email3; } set { _Acs_email3 = value; } }

        private string _Acs_Contacto4;
        public string Acs_Contacto4 { get { return _Acs_Contacto4; } set { _Acs_Contacto4 = value; } }
        private string _Acs_Telefono4;
        public string Acs_Telefono4 { get { return _Acs_Telefono4; } set { _Acs_Telefono4 = value; } }

        private string _Acs_email4;
        public string Acs_email4 { get { return _Acs_email4; } set { _Acs_email4 = value; } }

        private string _Acs_email5;
        public string Acs_email5 { get { return _Acs_email5; } set { _Acs_email5 = value; } }

        private string _Acs_email6;
        public string Acs_email6 { get { return _Acs_email6; } set { _Acs_email6 = value; } }

        private string _Acs_NumPrv;
        public string Acs_NumPrv { get { return _Acs_NumPrv; } set { _Acs_NumPrv = value; } }

        private string _Acs_Ruta1;
        public string Acs_Ruta1 { get { return _Acs_Ruta1; } set { _Acs_Ruta1 = value; } }

        private string _Acs_Ruta2;
        public string Acs_Ruta2 { get { return _Acs_Ruta2; } set { _Acs_Ruta2 = value; } }

        private int _Acs_ReqOrden;
        public int Acs_ReqOrden { get { return _Acs_ReqOrden; } set { _Acs_ReqOrden = value; } }

        private string _Acs_VigenciaApartir;
        public string Acs_VigenciaApartir { get { return _Acs_VigenciaApartir; } set { _Acs_VigenciaApartir = value; } }

        private string _ClienteDireccion;
        public string ClienteDireccion { get { return _ClienteDireccion; } set { _ClienteDireccion = value; } }
        private string _ClienteColonia;
        public string ClienteColonia { get { return _ClienteColonia; } set { _ClienteColonia = value; } }
        private string _ClienteMunicipio;
        public string ClienteMunicipio { get { return _ClienteMunicipio; } set { _ClienteMunicipio = value; } }
        private string _ClienteEstado;
        public string ClienteEstado { get { return _ClienteEstado; } set { _ClienteEstado = value; } }
        private string _ClienteRFC;
        public string ClienteRFC { get { return _ClienteRFC; } set { _ClienteRFC = value; } }
        private string _ClienteCodPost;
        public string ClienteCodPost { get { return _ClienteCodPost; } set { _ClienteCodPost = value; } }

        private bool _CuentaCorporativa;
        public bool CuentaCorporativa { get { return _CuentaCorporativa; } set { _CuentaCorporativa = value; } }

        private string _Cd_Nombre;
        public string Cd_Nombre { get { return _Cd_Nombre; } set { _Cd_Nombre = value; } }

        private Int32 _Id_Corp;
        public Int32 Id_Corp { get { return _Id_Corp; } set { _Id_Corp = value; } }

        private bool _AddendaSI;
        public bool AddendaSI { get { return _AddendaSI; } set { _AddendaSI = value; } }

        private string _FechaInicioVersion;
        public string FechaInicioVersion { get { return _FechaInicioVersion; } set { _FechaInicioVersion = value; } }

        private string _FechaFinVersion;
        public string FechaFinVersion { get { return _FechaFinVersion; } set { _FechaFinVersion = value; } }

        private string _Acs_version;
        public string Acs_version { get { return _Acs_version; } set { _Acs_version = value; } }

        private string _DireccionEntrega;
        public string DireccionEntrega { get { return _DireccionEntrega; } set { _DireccionEntrega = value; } }
        private string _ClienteColoniaE;
        public string ClienteColoniaE { get { return _ClienteColoniaE; } set { _ClienteColoniaE = value; } }
        private string _ClienteMunicipioE;
        public string ClienteMunicipioE { get { return _ClienteMunicipioE; } set { _ClienteMunicipioE = value; } }
        private string _ClienteEstadoE;
        public string ClienteEstadoE { get { return _ClienteEstadoE; } set { _ClienteEstadoE = value; } }
        private string _ClienteCPE;
        public string ClienteCPE { get { return _ClienteCPE; } set { _ClienteCPE = value; } }
        private string _Acs_Territorio;
        public string Acs_Territorio { get { return _Acs_Territorio; } set { _Acs_Territorio = value; } }
        private string _Acs_PedidoEncargadoEnviar;
        public string Acs_PedidoEncargadoEnviar { get { return _Acs_PedidoEncargadoEnviar; } set { _Acs_PedidoEncargadoEnviar = value; } }

        private string _Acs_PedidoPuesto;
        public string Acs_PedidoPuesto { get { return _Acs_PedidoPuesto; } set { _Acs_PedidoPuesto = value; } }

        private string _Acs_Pedidotelefono;
        public string Acs_Pedidotelefono { get { return _Acs_Pedidotelefono; } set { _Acs_Pedidotelefono = value; } }

        private string _Acs_PedidoTelefono;
        public string Acs_PedidoTelefono { get { return _Acs_PedidoTelefono; } set { _Acs_PedidoTelefono = value; } }
        private string _Acs_PedidoTelefono2;
        public string Acs_PedidoTelefono2 { get { return _Acs_PedidoTelefono2; } set { _Acs_PedidoTelefono2 = value; } }
        private string _Acs_PedidoEmail;
        public string Acs_PedidoEmail { get { return _Acs_PedidoEmail; } set { _Acs_PedidoEmail = value; } }
        private string _Acs_PedidoEmail2;
        public string Acs_PedidoEmail2 { get { return _Acs_PedidoEmail2; } set { _Acs_PedidoEmail2 = value; } }

        private bool _Acs_RecDocReposicion;
        public bool Acs_RecDocReposicion { get { return _Acs_RecDocReposicion; } set { _Acs_RecDocReposicion = value; } }
        private bool _Acs_RecDocFolio;
        public bool Acs_RecDocFolio { get { return _Acs_RecDocFolio; } set { _Acs_RecDocFolio = value; } }
        private string _Acs_RecDocOtro;
        public string Acs_RecDocOtro { get { return _Acs_RecDocOtro; } set { _Acs_RecDocOtro = value; } }

        private bool _Cte_Contado;
        public bool Cte_Contado { get { return _Cte_Contado; } set { _Cte_Contado = value; } }

        private int _Acs_Contado;
        public int Acs_Contado { get { return _Acs_Contado; } set { _Acs_Contado = value; } }

        private bool _Cte_TarjetaDebito;
        public bool Cte_TarjetaDebito { get { return _Cte_TarjetaDebito; } set { _Cte_TarjetaDebito = value; } }
        private bool _CteTarjetaCredito;
        public bool CteTarjetaCredito { get { return _CteTarjetaCredito; } set { _CteTarjetaCredito = value; } }

        private bool _CteDeposito;
        public bool CteDeposito { get { return _CteDeposito; } set { _CteDeposito = value; } }
        private string _Acs_VisitaOtro;
        public string Acs_VisitaOtro { get { return _Acs_VisitaOtro; } set { _Acs_VisitaOtro = value; } }
        private bool _Acs_ReqServAsesoria;
        public bool Acs_ReqServAsesoria { get { return _Acs_ReqServAsesoria; } set { _Acs_ReqServAsesoria = value; } }
        private bool _Acs_ReqServTecnicoRelleno;
        public bool Acs_ReqServTecnicoRelleno { get { return _Acs_ReqServTecnicoRelleno; } set { _Acs_ReqServTecnicoRelleno = value; } }
        private bool _Acs_ReqServMantenimiento;
        public bool Acs_ReqServMantenimiento { get { return _Acs_ReqServMantenimiento; } set { _Acs_ReqServMantenimiento = value; } }
        private string _Acs_Notas;
        public string Acs_Notas { get { return _Acs_Notas; } set { _Acs_Notas = value; } }
        private int _Acs_ContactoRepVenta;
        public int Acs_ContactoRepVenta { get { return _Acs_ContactoRepVenta; } set { _Acs_ContactoRepVenta = value; } }
        private string _Acs_ContactoRepVentaTel;
        public string Acs_ContactoRepVentaTel { get { return _Acs_ContactoRepVentaTel; } set { _Acs_ContactoRepVentaTel = value; } }
        private string _Acs_ContactoRepVentaEmail;
        public string Acs_ContactoRepVentaEmail { get { return _Acs_ContactoRepVentaEmail; } set { _Acs_ContactoRepVentaEmail = value; } }
        private int _Acs_ContactoRepServ;
        public int Acs_ContactoRepServ { get { return _Acs_ContactoRepServ; } set { _Acs_ContactoRepServ = value; } }
        private string _Acs_ContactoRepServTel;
        public string Acs_ContactoRepServTel { get { return _Acs_ContactoRepServTel; } set { _Acs_ContactoRepServTel = value; } }
        private string _Acs_ContactoRepServEmail;
        public string Acs_ContactoRepServEmail { get { return _Acs_ContactoRepServEmail; } set { _Acs_ContactoRepServEmail = value; } }
        private int _Acs_ContactoJefServ;
        public int Acs_ContactoJefServ { get { return _Acs_ContactoJefServ; } set { _Acs_ContactoJefServ = value; } }
        private string _Acs_ContactoJefServTel;
        public string Acs_ContactoJefServTel { get { return _Acs_ContactoJefServTel; } set { _Acs_ContactoJefServTel = value; } }
        private string _Acs_ContactoJefServEmail;
        public string Acs_ContactoJefServEmail { get { return _Acs_ContactoJefServEmail; } set { _Acs_ContactoJefServEmail = value; } }
        private int _Acs_ContactoAseServ;
        public int Acs_ContactoAseServ { get { return _Acs_ContactoAseServ; } set { _Acs_ContactoAseServ = value; } }
        private string _Acs_ContactoAseServTel;
        public string Acs_ContactoAseServTel { get { return _Acs_ContactoAseServTel; } set { _Acs_ContactoAseServTel = value; } }
        private string _Acs_ContactoAseServEmail;
        public string Acs_ContactoAseServEmail { get { return _Acs_ContactoAseServEmail; } set { _Acs_ContactoAseServEmail = value; } }
        private int _Acs_ContactoJefOper;
        public int Acs_ContactoJefOper { get { return _Acs_ContactoJefOper; } set { _Acs_ContactoJefOper = value; } }
        private string _Acs_ContactoJefOperTel;
        public string Acs_ContactoJefOperTel { get { return _Acs_ContactoJefOperTel; } set { _Acs_ContactoJefOperTel = value; } }
        private string _Acs_ContactoJefOperEmail;
        public string Acs_ContactoJefOperEmail { get { return _Acs_ContactoJefOperEmail; } set { _Acs_ContactoJefOperEmail = value; } }
        private int _Acs_ContactoCAlmRep;
        public int Acs_ContactoCAlmRep { get { return _Acs_ContactoCAlmRep; } set { _Acs_ContactoCAlmRep = value; } }
        private string _Acs_ContactoCAlmRepTel;
        public string Acs_ContactoCAlmRepTel { get { return _Acs_ContactoCAlmRepTel; } set { _Acs_ContactoCAlmRepTel = value; } }
        private string _Acs_ContactoCAlmRepEmail;
        public string Acs_ContactoCAlmRepEmail { get { return _Acs_ContactoCAlmRepEmail; } set { _Acs_ContactoCAlmRepEmail = value; } }
        private int _Acs_ContactoCServTec;
        public int Acs_ContactoCServTec { get { return _Acs_ContactoCServTec; } set { _Acs_ContactoCServTec = value; } }
        private string _Acs_ContactoCServTecTel;
        public string Acs_ContactoCServTecTel { get { return _Acs_ContactoCServTecTel; } set { _Acs_ContactoCServTecTel = value; } }
        private string _Acs_ContactoCServTecEmail;
        public string Acs_ContactoCServTecEmail { get { return _Acs_ContactoCServTecEmail; } set { _Acs_ContactoCServTecEmail = value; } }
        private int _Acs_ContactoCCreCob;
        public int Acs_ContactoCCreCob { get { return _Acs_ContactoCCreCob; } set { _Acs_ContactoCCreCob = value; } }
        private string _Acs_ContactoCCreCobTel;
        public string Acs_ContactoCCreCobTel { get { return _Acs_ContactoCCreCobTel; } set { _Acs_ContactoCCreCobTel = value; } }
        private string _Acs_ContactoCCreCobEmail;
        public string Acs_ContactoCCreCobEmail { get { return _Acs_ContactoCCreCobEmail; } set { _Acs_ContactoCCreCobEmail = value; } }
        private string _Acs_Unique;
        public string Acs_Unique { get { return _Acs_Unique; } set { _Acs_Unique = value; } }
        private int _Acs_Solicitar;
        public int Acs_Solicitar { get { return _Acs_Solicitar; } set { _Acs_Solicitar = value; } }
        private int _Acs_Sustituye;
        public int Acs_Sustituye { get { return _Acs_Sustituye; } set { _Acs_Sustituye = value; } }
        private string _Acs_Vencido;
        public string Acs_Vencido { get { return _Acs_Vencido; } set { _Acs_Vencido = value; } }

        private string _Acs_Modalidad;
        public string Acs_Modalidad { get { return _Acs_Modalidad; } set { _Acs_Modalidad = value; } }

        private string _Acs_Sucursal;
        public string Acs_Sucursal { get { return _Acs_Sucursal; } set { _Acs_Sucursal = value; } }
        private int _Acs_ParcialidadesSi;
        public int Acs_ParcialidadesSi { get { return _Acs_ParcialidadesSi; } set { _Acs_ParcialidadesSi = value; } }

        private int _Acs_ParcialidadesNo;
        public int Acs_ParcialidadesNo { get { return _Acs_ParcialidadesNo; } set { _Acs_ParcialidadesNo = value; } }

        //

        private int _Acs_ParcialidadTipo;
        public int Acs_ParcialidadTipo { get { return _Acs_ParcialidadTipo; } set { _Acs_ParcialidadTipo = value; } }

        private int _Acs_ConfirmacionPedidosSI;
        public int Acs_ConfirmacionPedidosSI { get { return _Acs_ConfirmacionPedidosSI; } set { _Acs_ConfirmacionPedidosSI = value; } }
        private int _Acs_ConfirmacionPedidosnO;
        public int Acs_ConfirmacionPedidosnO { get { return _Acs_ConfirmacionPedidosnO; } set { _Acs_ConfirmacionPedidosnO = value; } }

        // L M M J V S D
        private int _Acs_RecRevLunes;
        public int Acs_RecRevLunes { get { return _Acs_RecRevLunes; } set { _Acs_RecRevLunes = value; } }
        private int _Acs_RecRevMartes;
        public int Acs_RecRevMartes { get { return _Acs_RecRevMartes; } set { _Acs_RecRevMartes = value; } }
        private int _Acs_RecRevMiercoles;
        public int Acs_RecRevMiercoles { get { return _Acs_RecRevMiercoles; } set { _Acs_RecRevMiercoles = value; } }
        private int _Acs_RecRevJueves;
        public int Acs_RecRevJueves { get { return _Acs_RecRevJueves; } set { _Acs_RecRevJueves = value; } }
        private int _Acs_RecRevViernes;
        public int Acs_RecRevViernes { get { return _Acs_RecRevViernes; } set { _Acs_RecRevViernes = value; } }
        private int _Acs_RecRevSabado;
        public int Acs_RecRevSabado { get { return _Acs_RecRevSabado; } set { _Acs_RecRevSabado = value; } }
        private int _Acs_RecRevDomingo;
        public int Acs_RecRevDomingo { get { return _Acs_RecRevDomingo; } set { _Acs_RecRevDomingo = value; } }
        private int _Acs_RecRevCualquierDia;
        public int Acs_RecRevCualquierDia { get { return _Acs_RecRevCualquierDia; } set { _Acs_RecRevCualquierDia = value; } }

        private string _Acs_TimePicker1;
        public string Acs_TimePicker1 { get { return _Acs_TimePicker1; } set { _Acs_TimePicker1 = value; } }
        private string _Acs_TimePicker2;
        public string Acs_TimePicker2 { get { return _Acs_TimePicker2; } set { _Acs_TimePicker2 = value; } }
        private string _Acs_TimePicker3;
        public string Acs_TimePicker3 { get { return _Acs_TimePicker3; } set { _Acs_TimePicker3 = value; } }
        private string _Acs_TimePicker4;
        public string Acs_TimePicker4 { get { return _Acs_TimePicker4; } set { _Acs_TimePicker4 = value; } }
        private string _Acs_RecPersonaRecibe;
        public string Acs_RecPersonaRecibe { get { return _Acs_RecPersonaRecibe; } set { _Acs_RecPersonaRecibe = value; } }
        private string _Acs_RecPuesto;
        public string Acs_RecPuesto { get { return _Acs_RecPuesto; } set { _Acs_RecPuesto = value; } }
        private int _Acs_RecCitaMismoDia;
        public int Acs_RecCitaMismoDia { get { return _Acs_RecCitaMismoDia; } set { _Acs_RecCitaMismoDia = value; } }
        private int _Acs_RecCitaSinCita;
        public int Acs_RecCitaSinCita { get { return _Acs_RecCitaSinCita; } set { _Acs_RecCitaSinCita = value; } }
        private int _Acs_RecCitaPrevia;
        public int Acs_RecCitaPrevia { get { return _Acs_RecCitaPrevia; } set { _Acs_RecCitaPrevia = value; } }
        private string _Acs_RecCitaContacto;
        public string Acs_RecCitaContacto { get { return _Acs_RecCitaContacto; } set { _Acs_RecCitaContacto = value; } }
        private string _Acs_RecCitaTelefono;
        public string Acs_RecCitaTelefono { get { return _Acs_RecCitaTelefono; } set { _Acs_RecCitaTelefono = value; } }
        private int _Acs_RecCitaDiasdeAnticipacion;
        public int Acs_RecCitaDiasdeAnticipacion { get { return _Acs_RecCitaDiasdeAnticipacion; } set { _Acs_RecCitaDiasdeAnticipacion = value; } }
        private int _Acs_RecAreaPropia;
        public int Acs_RecAreaPropia { get { return _Acs_RecAreaPropia; } set { _Acs_RecAreaPropia = value; } }
        private int _Acs_RecAreaPlaza;
        public int Acs_RecAreaPlaza { get { return _Acs_RecAreaPlaza; } set { _Acs_RecAreaPlaza = value; } }
        private int _Acs_RecAreaCalle;
        public int Acs_RecAreaCalle { get { return _Acs_RecAreaCalle; } set { _Acs_RecAreaCalle = value; } }
        private int _Acs_RecAreaAvTransitada;
        public int Acs_RecAreaAvTransitada { get { return _Acs_RecAreaAvTransitada; } set { _Acs_RecAreaAvTransitada = value; } }
        private int _Acs_RecEstCortesia;
        public int Acs_RecEstCortesia { get { return _Acs_RecEstCortesia; } set { _Acs_RecEstCortesia = value; } }
        private int _Acs_RecEstCosto;
        public int Acs_RecEstCosto { get { return _Acs_RecEstCosto; } set { _Acs_RecEstCosto = value; } }

        private double _Acs_RecEstMonto;
        public double Acs_RecEstMonto { get { return _Acs_RecEstMonto; } set { _Acs_RecEstMonto = value; } }

        private int _Acs_EspecsAdic1;
        public int Acs_EspecsAdic1 { get { return _Acs_EspecsAdic1; } set { _Acs_EspecsAdic1 = value; } }

        //private int _ACS_chk63CualquierDia;
        //public int ACS_chk63CualquierDia { get { return _ACS_chk63CualquierDia; } set { _ACS_chk63CualquierDia = value; } }

        private int _ACS_ST_Aplicar;
        public int ACS_ST_Aplicar { get { return _ACS_ST_Aplicar; } set { _ACS_ST_Aplicar = value; } }

        private int _ACS_SA_Aplicar;
        public int ACS_SA_Aplicar { get { return _ACS_SA_Aplicar; } set { _ACS_SA_Aplicar = value; } }

        //private int _ACS_chk62CualquierDia;
        //public int ACS_chk62CualquierDia { get { return _ACS_chk62CualquierDia; } set { _ACS_chk62CualquierDia = value; } }

        private int _Acs_RecDocFactFranquiciaEnt;
        public int Acs_RecDocFactFranquiciaEnt { get { return _Acs_RecDocFactFranquiciaEnt; } set { _Acs_RecDocFactFranquiciaEnt = value; } }

        private int _Acs_RecDocFactFranquiciaEntCop;
        public int Acs_RecDocFactFranquiciaEntCop { get { return _Acs_RecDocFactFranquiciaEntCop; } set { _Acs_RecDocFactFranquiciaEntCop = value; } }

        private int _Acs_RecDocFactFranquiciaRec;
        public int Acs_RecDocFactFranquiciaRec { get { return _Acs_RecDocFactFranquiciaRec; } set { _Acs_RecDocFactFranquiciaRec = value; } }

        private int _Acs_RecDocFactFranquiciaRecCop;
        public int Acs_RecDocFactFranquiciaRecCop { get { return _Acs_RecDocFactFranquiciaRecCop; } set { _Acs_RecDocFactFranquiciaRecCop = value; } }

        // 2.2 RECPCION DE PEDIDOS -> DOCUMENTOS ENTREGA RECEPCION

        //Condiciones de Pago / 
        // ESPECIFICACIONES ADICIONALES

        private CapAcys_EspecAdi _RP_EA;
        public CapAcys_EspecAdi RP_EA { get { return _RP_EA; } set { _RP_EA = value; } }

        private int _Acs_RecDocFactKeyEnt;
        public int Acs_RecDocFactKeyEnt { get { return _Acs_RecDocFactKeyEnt; } set { _Acs_RecDocFactKeyEnt = value; } }
        private int _Acs_RecDocFactKeyEntCop;
        public int Acs_RecDocFactKeyEntCop { get { return _Acs_RecDocFactKeyEntCop; } set { _Acs_RecDocFactKeyEntCop = value; } }
        private int _Acs_RecDocFactKeyRec;
        public int Acs_RecDocFactKeyRec { get { return _Acs_RecDocFactKeyRec; } set { _Acs_RecDocFactKeyRec = value; } }
        private int _Acs_RecDocFactKeyRecCop;
        public int Acs_RecDocFactKeyRecCop { get { return _Acs_RecDocFactKeyRecCop; } set { _Acs_RecDocFactKeyRecCop = value; } }

        // FACTURAS KEY
        private int _Acs_RecDocOrdCompraEnt;
        public int Acs_RecDocOrdCompraEnt { get { return _Acs_RecDocOrdCompraEnt; } set { _Acs_RecDocOrdCompraEnt = value; } }
        private int _Acs_RecDocOrdCompraEntCop;
        public int Acs_RecDocOrdCompraEntCop { get { return _Acs_RecDocOrdCompraEntCop; } set { _Acs_RecDocOrdCompraEntCop = value; } }
        private int _Acs_RecDocOrdCompraRec;
        public int Acs_RecDocOrdCompraRec { get { return _Acs_RecDocOrdCompraRec; } set { _Acs_RecDocOrdCompraRec = value; } }
        private int _Acs_RecDocOrdCompraRecCop;
        public int Acs_RecDocOrdCompraRecCop { get { return _Acs_RecDocOrdCompraRecCop; } set { _Acs_RecDocOrdCompraRecCop = value; } }

        // ONDE DE REPOSICION
        private int _Acs_RecDocOrdReposEnt;
        public int Acs_RecDocOrdReposEnt { get { return _Acs_RecDocOrdReposEnt; } set { _Acs_RecDocOrdReposEnt = value; } }
        private int _Acs_RecDocOrdReposEntCop;
        public int Acs_RecDocOrdReposEntCop { get { return _Acs_RecDocOrdReposEntCop; } set { _Acs_RecDocOrdReposEntCop = value; } }
        private int _Acs_RecDocOrdReposRec;
        public int Acs_RecDocOrdReposRec { get { return _Acs_RecDocOrdReposRec; } set { _Acs_RecDocOrdReposRec = value; } }
        private int _Acs_RecDocOrdReposRecCop;
        public int Acs_RecDocOrdReposRecCop { get { return _Acs_RecDocOrdReposRecCop; } set { _Acs_RecDocOrdReposRecCop = value; } }

        // COPIA DE PEDIDO
        private int _Acs_RecDocCopPedidoEnt;
        public int Acs_RecDocCopPedidoEnt { get { return _Acs_RecDocCopPedidoEnt; } set { _Acs_RecDocCopPedidoEnt = value; } }
        private int _Acs_RecDocCopPedidoEntCop;
        public int Acs_RecDocCopPedidoEntCop { get { return _Acs_RecDocCopPedidoEntCop; } set { _Acs_RecDocCopPedidoEntCop = value; } }
        private int _Acs_RecDocCopPedidoRec;
        public int Acs_RecDocCopPedidoRec { get { return _Acs_RecDocCopPedidoRec; } set { _Acs_RecDocCopPedidoRec = value; } }
        private int _Acs_RecDocCopPedidoRecCop;
        public int Acs_RecDocCopPedidoRecCop { get { return _Acs_RecDocCopPedidoRecCop; } set { _Acs_RecDocCopPedidoRecCop = value; } }

        // REMISION
        private int _Acs_RecDocRemisionEnt;
        public int Acs_RecDocRemisionEnt { get { return _Acs_RecDocRemisionEnt; } set { _Acs_RecDocRemisionEnt = value; } }
        private int _Acs_RecDocRemisionEntCop;
        public int Acs_RecDocRemisionEntCop { get { return _Acs_RecDocRemisionEntCop; } set { _Acs_RecDocRemisionEntCop = value; } }
        private int _Acs_RecDocRemisionRec;
        public int Acs_RecDocRemisionRec { get { return _Acs_RecDocRemisionRec; } set { _Acs_RecDocRemisionRec = value; } }
        private int _Acs_RecDocRemisionRecCop;
        public int Acs_RecDocRemisionRecCop { get { return _Acs_RecDocRemisionRecCop; } set { _Acs_RecDocRemisionRecCop = value; } }

        // CERTIFICADO DE CALIDAD 1
        private int _Acs_RecDocCertificadoEnt;
        public int Acs_RecDocCertificadoEnt
        {
            get { return _Acs_RecDocCertificadoEnt; }
            set { _Acs_RecDocCertificadoEnt = value; }
        }
        private int _Acs_RecDocCertificadoEntCop;
        public int Acs_RecDocCertificadoEntCop
        {
            get { return _Acs_RecDocCertificadoEntCop; }
            set { _Acs_RecDocCertificadoEntCop = value; }
        }
        private int _Acs_RecDocCertificadoRec;
        public int Acs_RecDocCertificadoRec
        {
            get { return _Acs_RecDocCertificadoRec; }
            set { _Acs_RecDocCertificadoRec = value; }
        }
        private int _Acs_RecDocCertificadoRecCop;
        public int Acs_RecDocCertificadoRecCop
        {
            get { return _Acs_RecDocCertificadoRecCop; }
            set { _Acs_RecDocCertificadoRecCop = value; }
        }

        // OCT16-2019
        private string _Acs_CorreoRecibirFacturas;
        public string Acs_CorreoRecibirFacturas
        {
            get { return _Acs_CorreoRecibirFacturas; }
            set { _Acs_CorreoRecibirFacturas = value; }
        }

        private string _Acs_CorreoRecibirComplemento;
        public string Acs_CorreoRecibirComplemento
        {
            get { return _Acs_CorreoRecibirComplemento; }
            set { _Acs_CorreoRecibirComplemento = value; }
        }

        private int _Acs_CorreoRecibir_NA;
        public int Acs_CorreoRecibir_NA
        {
            get { return _Acs_CorreoRecibir_NA; }
            set { _Acs_CorreoRecibir_NA = value; }
        }

        //
        private int _Acs_DocEntregaFormaPago;
        public int Acs_DocEntregaFormaPago { get { return _Acs_DocEntregaFormaPago; } set { _Acs_DocEntregaFormaPago = value; } }

        private int _ACS_RecDocFolioEnt;
        public int ACS_RecDocFolioEnt { get { return _ACS_RecDocFolioEnt; } set { _ACS_RecDocFolioEnt = value; } }
        private int _ACS_RecDocFolioEntCop;
        public int ACS_RecDocFolioEntCop { get { return _ACS_RecDocFolioEntCop; } set { _ACS_RecDocFolioEntCop = value; } }
        private int _ACS_RecDocFolioRec;
        public int ACS_RecDocFolioRec { get { return _ACS_RecDocFolioRec; } set { _ACS_RecDocFolioRec = value; } }
        private int _ACS_RecDocFolioRecCop;
        public int ACS_RecDocFolioRecCop { get { return _ACS_RecDocFolioRecCop; } set { _ACS_RecDocFolioRecCop = value; } }

        private int _ACS_RecDocContraRecEnt;
        public int ACS_RecDocContraRecEnt { get { return _ACS_RecDocContraRecEnt; } set { _ACS_RecDocContraRecEnt = value; } }
        private int _ACS_RecDocContraRecEntCop;
        public int ACS_RecDocContraRecEntCop { get { return _ACS_RecDocContraRecEntCop; } set { _ACS_RecDocContraRecEntCop = value; } }
        private int _ACS_RecDocContraRecRec;
        public int ACS_RecDocContraRecRec { get { return _ACS_RecDocContraRecRec; } set { _ACS_RecDocContraRecRec = value; } }
        private int _ACS_RecDocContraRecRecCop;
        public int ACS_RecDocContraRecRecCop { get { return _ACS_RecDocContraRecRecCop; } set { _ACS_RecDocContraRecRecCop = value; } }

        private int _ACS_RecDocEntAlmacenEnt;
        public int ACS_RecDocEntAlmacenEnt { get { return _ACS_RecDocEntAlmacenEnt; } set { _ACS_RecDocEntAlmacenEnt = value; } }
        private int _ACS_RecDocEntAlmacenEntCop;
        public int ACS_RecDocEntAlmacenEntCop { get { return _ACS_RecDocEntAlmacenEntCop; } set { _ACS_RecDocEntAlmacenEntCop = value; } }
        private int _ACS_RecDocEntAlmacenRec;
        public int ACS_RecDocEntAlmacenRec { get { return _ACS_RecDocEntAlmacenRec; } set { _ACS_RecDocEntAlmacenRec = value; } }
        private int _ACS_RecDocEntAlmacenRecCop;
        public int ACS_RecDocEntAlmacenRecCop { get { return _ACS_RecDocEntAlmacenRecCop; } set { _ACS_RecDocEntAlmacenRecCop = value; } }

        private int _ACS_RecDocSopServicioEnt;
        public int ACS_RecDocSopServicioEnt { get { return _ACS_RecDocSopServicioEnt; } set { _ACS_RecDocSopServicioEnt = value; } }
        private int _ACS_RecDocSopServicioEntCop;
        public int ACS_RecDocSopServicioEntCop { get { return _ACS_RecDocSopServicioEntCop; } set { _ACS_RecDocSopServicioEntCop = value; } }
        private int _ACS_RecDocSopServicioRec;
        public int ACS_RecDocSopServicioRec { get { return _ACS_RecDocSopServicioRec; } set { _ACS_RecDocSopServicioRec = value; } }
        private int _ACS_RecDocSopServicioRecCop;
        public int ACS_RecDocSopServicioRecCop { get { return _ACS_RecDocSopServicioRecCop; } set { _ACS_RecDocSopServicioRecCop = value; } }

        private int _ACS_RecDocNomFirmaEnt;
        public int ACS_RecDocNomFirmaEnt { get { return _ACS_RecDocNomFirmaEnt; } set { _ACS_RecDocNomFirmaEnt = value; } }
        private int _ACS_RecDocNomFirmaEntCop;
        public int ACS_RecDocNomFirmaEntCop { get { return _ACS_RecDocNomFirmaEntCop; } set { _ACS_RecDocNomFirmaEntCop = value; } }
        private int _ACS_RecDocNomFirmaoRec;
        public int ACS_RecDocNomFirmaoRec { get { return _ACS_RecDocNomFirmaoRec; } set { _ACS_RecDocNomFirmaoRec = value; } }
        private int _ACS_RecDocNomFirmaRecCop;
        public int ACS_RecDocNomFirmaRecCop { get { return _ACS_RecDocNomFirmaRecCop; } set { _ACS_RecDocNomFirmaRecCop = value; } }

        private int _ACS_RecCitaEnt;
        public int ACS_RecCitaEnt { get { return _ACS_RecCitaEnt; } set { _ACS_RecCitaEnt = value; } }
        private int _ACS_RecCitaEntCop;
        public int ACS_RecCitaEntCop { get { return _ACS_RecCitaEntCop; } set { _ACS_RecCitaEntCop = value; } }
        private int _ACS_RecCitaRec;
        public int ACS_RecCitaRec { get { return _ACS_RecCitaRec; } set { _ACS_RecCitaRec = value; } }
        private int _ACS_RecCitaRecCop;
        public int ACS_RecCitaRecCop { get { return _ACS_RecCitaRecCop; } set { _ACS_RecCitaRecCop = value; } }

        // Fin

        private string _ACS_RecOtroRec;
        public string ACS_RecOtroRec { get { return _ACS_RecOtroRec; } set { _ACS_RecOtroRec = value; } }

        // SERVICIOS ASESORIA
        // SERVICIOS ASESORIA
        // SERVICIOS ASESORIA

        private int _ACS_chk62Aplicar;
        public int ACS_chk62Aplicar { get { return _ACS_chk62Aplicar; } set { _ACS_chk62Aplicar = value; } }

        private int _ACS_chk62Tipo1;
        public int ACS_chk62Tipo1 { get { return _ACS_chk62Tipo1; } set { _ACS_chk62Tipo1 = value; } }

        private int _ACS_chk62Tipo2;
        public int ACS_chk62Tipo2 { get { return _ACS_chk62Tipo2; } set { _ACS_chk62Tipo2 = value; } }

        // L M M J V S D
        private int _ACS_chk62Lunes;
        public int ACS_chk62Lunes { get { return _ACS_chk62Lunes; } set { _ACS_chk62Lunes = value; } }
        private int _ACS_chk62Martes;
        public int ACS_chk62Martes { get { return _ACS_chk62Martes; } set { _ACS_chk62Martes = value; } }
        private int _ACS_chk62Miercoles;
        public int ACS_chk62Miercoles { get { return _ACS_chk62Miercoles; } set { _ACS_chk62Miercoles = value; } }
        private int _ACS_chk62Jueves;
        public int ACS_chk62Jueves { get { return _ACS_chk62Jueves; } set { _ACS_chk62Jueves = value; } }
        private int _ACS_chk62Viernes;
        public int ACS_chk62Viernes { get { return _ACS_chk62Viernes; } set { _ACS_chk62Viernes = value; } }
        private int _ACS_chk62Sabado;
        public int ACS_chk62Sabado { get { return _ACS_chk62Sabado; } set { _ACS_chk62Sabado = value; } }
        private int _ACS_chk62Domingo;
        public int ACS_chk62Domingo { get { return _ACS_chk62Domingo; } set { _ACS_chk62Domingo = value; } }

        private int _ACS_chk62CualquierDia;
        public int ACS_chk62CualquierDia { get { return _ACS_chk62CualquierDia; } set { _ACS_chk62CualquierDia = value; } }

        private string _ACS_RadTimePicker162;
        public string ACS_RadTimePicker162 { get { return _ACS_RadTimePicker162; } set { _ACS_RadTimePicker162 = value; } }
        private string _ACS_RadTimePicker262;
        public string ACS_RadTimePicker262 { get { return _ACS_RadTimePicker262; } set { _ACS_RadTimePicker262 = value; } }
        private string _ACS_RadTimePicker362;
        public string ACS_RadTimePicker362 { get { return _ACS_RadTimePicker362; } set { _ACS_RadTimePicker362 = value; } }
        private string _ACS_RadTimePicker462;
        public string ACS_RadTimePicker462 { get { return _ACS_RadTimePicker462; } set { _ACS_RadTimePicker462 = value; } }
        private string _ACS_txtRecPersonaRecibe62;
        public string ACS_txtRecPersonaRecibe62 { get { return _ACS_txtRecPersonaRecibe62; } set { _ACS_txtRecPersonaRecibe62 = value; } }
        private string _ACS_txtRecPuesto62;
        public string ACS_txtRecPuesto62 { get { return _ACS_txtRecPuesto62; } set { _ACS_txtRecPuesto62 = value; } }
        private int _ACS_Chk62Mismodia;
        public int ACS_Chk62Mismodia { get { return _ACS_Chk62Mismodia; } set { _ACS_Chk62Mismodia = value; } }
        private int _ACS_Chk62Sincita;
        public int ACS_Chk62Sincita { get { return _ACS_Chk62Sincita; } set { _ACS_Chk62Sincita = value; } }
        private int _ACS_Chk62Previa;
        public int ACS_Chk62Previa { get { return _ACS_Chk62Previa; } set { _ACS_Chk62Previa = value; } }
        private string _ACS_txt62CitaContacto;
        public string ACS_txt62CitaContacto { get { return _ACS_txt62CitaContacto; } set { _ACS_txt62CitaContacto = value; } }
        private string _ACS_txt62CitaTelefono;
        public string ACS_txt62CitaTelefono { get { return _ACS_txt62CitaTelefono; } set { _ACS_txt62CitaTelefono = value; } }
        private int _ACS_txt62CitaDiasdeAnticipacion;
        public int ACS_txt62CitaDiasdeAnticipacion { get { return _ACS_txt62CitaDiasdeAnticipacion; } set { _ACS_txt62CitaDiasdeAnticipacion = value; } }
        private int _ACS_chk62AreaPropia;
        public int ACS_chk62AreaPropia { get { return _ACS_chk62AreaPropia; } set { _ACS_chk62AreaPropia = value; } }
        private int _ACS_chk62AreaPlaza;
        public int ACS_chk62AreaPlaza { get { return _ACS_chk62AreaPlaza; } set { _ACS_chk62AreaPlaza = value; } }
        private int _ACS_chk62AreaCalle;
        public int ACS_chk62AreaCalle { get { return _ACS_chk62AreaCalle; } set { _ACS_chk62AreaCalle = value; } }
        private int _ACS_chk62AreaAvTransitada;
        public int ACS_chk62AreaAvTransitada { get { return _ACS_chk62AreaAvTransitada; } set { _ACS_chk62AreaAvTransitada = value; } }
        private int _ACS_chk62EstCortesia;
        public int ACS_chk62EstCortesia { get { return _ACS_chk62EstCortesia; } set { _ACS_chk62EstCortesia = value; } }
        private int _ACS_chk62EstCosto;
        public int ACS_chk62EstCosto { get { return _ACS_chk62EstCosto; } set { _ACS_chk62EstCosto = value; } }
        private int _ACS_txt62EstMonto;
        public int ACS_txt62EstMonto { get { return _ACS_txt62EstMonto; } set { _ACS_txt62EstMonto = value; } }
        private string _ACS_txt62ClienteDireccion;
        public string ACS_txt62ClienteDireccion { get { return _ACS_txt62ClienteDireccion; } set { _ACS_txt62ClienteDireccion = value; } }
        private string _ACS_txt62ClienteColonia;
        public string ACS_txt62ClienteColonia { get { return _ACS_txt62ClienteColonia; } set { _ACS_txt62ClienteColonia = value; } }
        private string _ACS_txt62ClienteMunicipio;
        public string ACS_txt62ClienteMunicipio { get { return _ACS_txt62ClienteMunicipio; } set { _ACS_txt62ClienteMunicipio = value; } }
        private string _ACS_txt62ClienteEstado;
        public string ACS_txt62ClienteEstado { get { return _ACS_txt62ClienteEstado; } set { _ACS_txt62ClienteEstado = value; } }
        private string _ACS_txt62ClienteCodPost;
        public string ACS_txt62ClienteCodPost { get { return _ACS_txt62ClienteCodPost; } set { _ACS_txt62ClienteCodPost = value; } }


        private int _ACS_chk62DocFactFranquiciaEnt;
        public int ACS_chk62DocFactFranquiciaEnt { get { return _ACS_chk62DocFactFranquiciaEnt; } set { _ACS_chk62DocFactFranquiciaEnt = value; } }
        private int _ACS_txt62DocFactFranquiciaEntCop;
        public int ACS_txt62DocFactFranquiciaEntCop { get { return _ACS_txt62DocFactFranquiciaEntCop; } set { _ACS_txt62DocFactFranquiciaEntCop = value; } }
        private int _ACS_chk62DocFactFranquiciaRec;
        public int ACS_chk62DocFactFranquiciaRec { get { return _ACS_chk62DocFactFranquiciaRec; } set { _ACS_chk62DocFactFranquiciaRec = value; } }
        private int _ACS_txt62DocFactFranquiciaRecCop;
        public int ACS_txt62DocFactFranquiciaRecCop { get { return _ACS_txt62DocFactFranquiciaRecCop; } set { _ACS_txt62DocFactFranquiciaRecCop = value; } }
        private int _ACS_chk62DocFactKeyEnt;
        public int ACS_chk62DocFactKeyEnt { get { return _ACS_chk62DocFactKeyEnt; } set { _ACS_chk62DocFactKeyEnt = value; } }

        private int _ACS_txt62DocFactKeyEntCop;
        public int ACS_txt62DocFactKeyEntCop { get { return _ACS_txt62DocFactKeyEntCop; } set { _ACS_txt62DocFactKeyEntCop = value; } }

        private int _ACS_chk62DocFactKeyRec;
        public int ACS_chk62DocFactKeyRec { get { return _ACS_chk62DocFactKeyRec; } set { _ACS_chk62DocFactKeyRec = value; } }

        private int _ACS_txt62DocFactKeyRecCop;
        public int ACS_txt62DocFactKeyRecCop { get { return _ACS_txt62DocFactKeyRecCop; } set { _ACS_txt62DocFactKeyRecCop = value; } }
        private int _ACS_chk62DocOrdCompraEnt;
        public int ACS_chk62DocOrdCompraEnt { get { return _ACS_chk62DocOrdCompraEnt; } set { _ACS_chk62DocOrdCompraEnt = value; } }
        private int _ACS_txt62DocOrdCompraEntCop;
        public int ACS_txt62DocOrdCompraEntCop { get { return _ACS_txt62DocOrdCompraEntCop; } set { _ACS_txt62DocOrdCompraEntCop = value; } }
        private int _ACS_chk62DocOrdCompraRec;
        public int ACS_chk62DocOrdCompraRec { get { return _ACS_chk62DocOrdCompraRec; } set { _ACS_chk62DocOrdCompraRec = value; } }
        private int _ACS_txt62DocOrdCompraRecCop;
        public int ACS_txt62DocOrdCompraRecCop { get { return _ACS_txt62DocOrdCompraRecCop; } set { _ACS_txt62DocOrdCompraRecCop = value; } }
        private int _ACS_chk62DocOrdReposEnt;
        public int ACS_chk62DocOrdReposEnt { get { return _ACS_chk62DocOrdReposEnt; } set { _ACS_chk62DocOrdReposEnt = value; } }
        private int _ACS_txt62DocOrdReposEntCop;
        public int ACS_txt62DocOrdReposEntCop { get { return _ACS_txt62DocOrdReposEntCop; } set { _ACS_txt62DocOrdReposEntCop = value; } }
        private int _ACS_chk62DocOrdReposRec;
        public int ACS_chk62DocOrdReposRec { get { return _ACS_chk62DocOrdReposRec; } set { _ACS_chk62DocOrdReposRec = value; } }
        private int _ACS_txt62DocOrdReposRecCop;
        public int ACS_txt62DocOrdReposRecCop { get { return _ACS_txt62DocOrdReposRecCop; } set { _ACS_txt62DocOrdReposRecCop = value; } }
        private int _ACS_chk62DocCopPedidoEnt;
        public int ACS_chk62DocCopPedidoEnt { get { return _ACS_chk62DocCopPedidoEnt; } set { _ACS_chk62DocCopPedidoEnt = value; } }
        private int _ACS_txt62DocCopPedidoEntCop;
        public int ACS_txt62DocCopPedidoEntCop { get { return _ACS_txt62DocCopPedidoEntCop; } set { _ACS_txt62DocCopPedidoEntCop = value; } }
        private int _ACS_chk62DocCopPedidoRec;
        public int ACS_chk62DocCopPedidoRec { get { return _ACS_chk62DocCopPedidoRec; } set { _ACS_chk62DocCopPedidoRec = value; } }
        private int _ACS_txt62DocCopPedidoRecCop;
        public int ACS_txt62DocCopPedidoRecCop { get { return _ACS_txt62DocCopPedidoRecCop; } set { _ACS_txt62DocCopPedidoRecCop = value; } }

        // REMISION
        private int _ACS_chk62DocRemisionEnt;
        public int ACS_chk62DocRemisionEnt
        {
            get { return _ACS_chk62DocRemisionEnt; }
            set { _ACS_chk62DocRemisionEnt = value; }
        }
        private int _ACS_txt62DocRemisionEntCop;
        public int ACS_txt62DocRemisionEntCop
        {
            get { return _ACS_txt62DocRemisionEntCop; }
            set { _ACS_txt62DocRemisionEntCop = value; }
        }
        private int _ACS_chk62DocRemisionRec;
        public int ACS_chk62DocRemisionRec
        {
            get { return _ACS_chk62DocRemisionRec; }
            set { _ACS_chk62DocRemisionRec = value; }
        }
        private int _ACS_txt62DocRemisionRecCop;
        public int ACS_txt62DocRemisionRecCop
        {
            get { return _ACS_txt62DocRemisionRecCop; }
            set { _ACS_txt62DocRemisionRecCop = value; }
        }

        // CERTIFICADO 
        private int _ACS_chk62DocCertificadoEnt;
        public int ACS_chk62DocCertificadoEnt
        {
            get { return _ACS_chk62DocCertificadoEnt; }
            set { _ACS_chk62DocCertificadoEnt = value; }
        }
        private int _ACS_txt62DocCertificadoEntCop;
        public int ACS_txt62DocCertificadoEntCop
        {
            get { return _ACS_txt62DocCertificadoEntCop; }
            set { _ACS_txt62DocCertificadoEntCop = value; }
        }
        private int _ACS_chk62DocCertificadoRec;
        public int ACS_chk62DocCertificadoRec
        {
            get { return _ACS_chk62DocCertificadoRec; }
            set { _ACS_chk62DocCertificadoRec = value; }
        }
        private int _ACS_txt62DocCertificadoRecCop;
        public int ACS_txt62DocCertificadoRecCop
        {
            get { return _ACS_txt62DocCertificadoRecCop; }
            set { _ACS_txt62DocCertificadoRecCop = value; }
        }

        private int _ACS_chk62DocFolioEnt;
        public int ACS_chk62DocFolioEnt { get { return _ACS_chk62DocFolioEnt; } set { _ACS_chk62DocFolioEnt = value; } }
        private int _ACS_txt62DocFolioEntCop;
        public int ACS_txt62DocFolioEntCop { get { return _ACS_txt62DocFolioEntCop; } set { _ACS_txt62DocFolioEntCop = value; } }
        private int _ACS_chk62DocFolioRec;
        public int ACS_chk62DocFolioRec { get { return _ACS_chk62DocFolioRec; } set { _ACS_chk62DocFolioRec = value; } }
        private int _ACS_txt62DocFolioRecCop;
        public int ACS_txt62DocFolioRecCop { get { return _ACS_txt62DocFolioRecCop; } set { _ACS_txt62DocFolioRecCop = value; } }
        private int _ACS_chk62DocContraRecEnt;
        public int ACS_chk62DocContraRecEnt { get { return _ACS_chk62DocContraRecEnt; } set { _ACS_chk62DocContraRecEnt = value; } }
        private int _ACS_txt62DocContraRecEntCop;
        public int ACS_txt62DocContraRecEntCop { get { return _ACS_txt62DocContraRecEntCop; } set { _ACS_txt62DocContraRecEntCop = value; } }
        private int _ACS_chk62DocContraRecRec;
        public int ACS_chk62DocContraRecRec { get { return _ACS_chk62DocContraRecRec; } set { _ACS_chk62DocContraRecRec = value; } }
        private int _ACS_txt62DocContraRecRecCop;
        public int ACS_txt62DocContraRecRecCop { get { return _ACS_txt62DocContraRecRecCop; } set { _ACS_txt62DocContraRecRecCop = value; } }
        private int _ACS_chk62DocEntAlmacenEnt;
        public int ACS_chk62DocEntAlmacenEnt { get { return _ACS_chk62DocEntAlmacenEnt; } set { _ACS_chk62DocEntAlmacenEnt = value; } }
        private int _ACS_txt62DocEntAlmacenEntCop;
        public int ACS_txt62DocEntAlmacenEntCop { get { return _ACS_txt62DocEntAlmacenEntCop; } set { _ACS_txt62DocEntAlmacenEntCop = value; } }
        private int _ACS_chk62DocEntAlmacenRec;
        public int ACS_chk62DocEntAlmacenRec { get { return _ACS_chk62DocEntAlmacenRec; } set { _ACS_chk62DocEntAlmacenRec = value; } }
        private int _ACS_txt62DocEntAlmacenRecCop;
        public int ACS_txt62DocEntAlmacenRecCop { get { return _ACS_txt62DocEntAlmacenRecCop; } set { _ACS_txt62DocEntAlmacenRecCop = value; } }
        private int _ACS_chk62DocSopServicioEnt;
        public int ACS_chk62DocSopServicioEnt { get { return _ACS_chk62DocSopServicioEnt; } set { _ACS_chk62DocSopServicioEnt = value; } }
        private int _ACS_txt62DocSopServicioEntCop;
        public int ACS_txt62DocSopServicioEntCop { get { return _ACS_txt62DocSopServicioEntCop; } set { _ACS_txt62DocSopServicioEntCop = value; } }
        private int _ACS_chk62DocSopServicioRec;
        public int ACS_chk62DocSopServicioRec { get { return _ACS_chk62DocSopServicioRec; } set { _ACS_chk62DocSopServicioRec = value; } }
        private int _ACS_txt62DocSopServicioRecCop;
        public int ACS_txt62DocSopServicioRecCop { get { return _ACS_txt62DocSopServicioRecCop; } set { _ACS_txt62DocSopServicioRecCop = value; } }
        private int _ACS_chk62DocNomFirmaEnt;
        public int ACS_chk62DocNomFirmaEnt { get { return _ACS_chk62DocNomFirmaEnt; } set { _ACS_chk62DocNomFirmaEnt = value; } }
        private int _ACS_txt62DocNomFirmaEntCop;
        public int ACS_txt62DocNomFirmaEntCop { get { return _ACS_txt62DocNomFirmaEntCop; } set { _ACS_txt62DocNomFirmaEntCop = value; } }
        private int _ACS_chk62DocNomFirmaoRec;
        public int ACS_chk62DocNomFirmaoRec { get { return _ACS_chk62DocNomFirmaoRec; } set { _ACS_chk62DocNomFirmaoRec = value; } }
        private int _ACS_txt62DocNomFirmaRecCop;
        public int ACS_txt62DocNomFirmaRecCop { get { return _ACS_txt62DocNomFirmaRecCop; } set { _ACS_txt62DocNomFirmaRecCop = value; } }
        private int _ACS_chk62CitaEnt;
        public int ACS_chk62CitaEnt { get { return _ACS_chk62CitaEnt; } set { _ACS_chk62CitaEnt = value; } }
        private int _ACS_txt62CitaEntCop;
        public int ACS_txt62CitaEntCop { get { return _ACS_txt62CitaEntCop; } set { _ACS_txt62CitaEntCop = value; } }
        private int _ACS_chk62CitaRec;
        public int ACS_chk62CitaRec { get { return _ACS_chk62CitaRec; } set { _ACS_chk62CitaRec = value; } }
        private int _ACS_txt62CitaRecCop;
        public int ACS_txt62CitaRecCop { get { return _ACS_txt62CitaRecCop; } set { _ACS_txt62CitaRecCop = value; } }

        // SERVICIO TECNICO 

        private int _ACS_chk63Aplicar;
        public int ACS_chk63Aplicar { get { return _ACS_chk63Aplicar; } set { _ACS_chk63Aplicar = value; } }

        // L M M J V S D
        private int _ACS_chk63Lunes;
        public int ACS_chk63Lunes { get { return _ACS_chk63Lunes; } set { _ACS_chk63Lunes = value; } }
        private int _ACS_chk63Martes;
        public int ACS_chk63Martes { get { return _ACS_chk63Martes; } set { _ACS_chk63Martes = value; } }
        private int _ACS_chk63Miercoles;
        public int ACS_chk63Miercoles { get { return _ACS_chk63Miercoles; } set { _ACS_chk63Miercoles = value; } }
        private int _ACS_chk63Jueves;
        public int ACS_chk63Jueves { get { return _ACS_chk63Jueves; } set { _ACS_chk63Jueves = value; } }
        private int _ACS_chk63Viernes;
        public int ACS_chk63Viernes { get { return _ACS_chk63Viernes; } set { _ACS_chk63Viernes = value; } }
        private int _ACS_chk63Sabado;
        public int ACS_chk63Sabado { get { return _ACS_chk63Sabado; } set { _ACS_chk63Sabado = value; } }
        private int _ACS_chk63Domingo;
        public int ACS_chk63Domingo { get { return _ACS_chk63Domingo; } set { _ACS_chk63Domingo = value; } }
        private int _ACS_chk63CualquierDia;
        public int ACS_chk63CualquierDia { get { return _ACS_chk63CualquierDia; } set { _ACS_chk63CualquierDia = value; } }

        private string _ACS_Rad63TimePicker163;
        public string ACS_Rad63TimePicker163 { get { return _ACS_Rad63TimePicker163; } set { _ACS_Rad63TimePicker163 = value; } }
        private string _ACS_Rad63TimePicker263;
        public string ACS_Rad63TimePicker263 { get { return _ACS_Rad63TimePicker263; } set { _ACS_Rad63TimePicker263 = value; } }
        private string _ACS_Rad63TimePicker363;
        public string ACS_Rad63TimePicker363 { get { return _ACS_Rad63TimePicker363; } set { _ACS_Rad63TimePicker363 = value; } }
        private string _ACS_Rad63TimePicker463;
        public string ACS_Rad63TimePicker463 { get { return _ACS_Rad63TimePicker463; } set { _ACS_Rad63TimePicker463 = value; } }

        private string _ACS_txtRecPersonaRecibe63;
        public string ACS_txtRecPersonaRecibe63 { get { return _ACS_txtRecPersonaRecibe63; } set { _ACS_txtRecPersonaRecibe63 = value; } }
        private string _ACS_txtRecPuesto63;
        public string ACS_txtRecPuesto63 { get { return _ACS_txtRecPuesto63; } set { _ACS_txtRecPuesto63 = value; } }
        private int _ACS_Chk63Mismodia;
        public int ACS_Chk63Mismodia { get { return _ACS_Chk63Mismodia; } set { _ACS_Chk63Mismodia = value; } }
        private int _ACS_Chk63Sincita;
        public int ACS_Chk63Sincita { get { return _ACS_Chk63Sincita; } set { _ACS_Chk63Sincita = value; } }
        private int _ACS_Chk63Previa;
        public int ACS_Chk63Previa { get { return _ACS_Chk63Previa; } set { _ACS_Chk63Previa = value; } }
        private string _ACS_txt63CitaContacto;
        public string ACS_txt63CitaContacto { get { return _ACS_txt63CitaContacto; } set { _ACS_txt63CitaContacto = value; } }
        private string _ACS_txt63CitaTelefono;
        public string ACS_txt63CitaTelefono { get { return _ACS_txt63CitaTelefono; } set { _ACS_txt63CitaTelefono = value; } }
        private int _ACS_txt63CitaDiasdeAnticipacion;
        public int ACS_txt63CitaDiasdeAnticipacion { get { return _ACS_txt63CitaDiasdeAnticipacion; } set { _ACS_txt63CitaDiasdeAnticipacion = value; } }
        private int _ACS_chk63AreaPropia;
        public int ACS_chk63AreaPropia { get { return _ACS_chk63AreaPropia; } set { _ACS_chk63AreaPropia = value; } }
        private int _ACS_chk63AreaPlaza;
        public int ACS_chk63AreaPlaza { get { return _ACS_chk63AreaPlaza; } set { _ACS_chk63AreaPlaza = value; } }
        private int _ACS_chk63AreaCalle;
        public int ACS_chk63AreaCalle { get { return _ACS_chk63AreaCalle; } set { _ACS_chk63AreaCalle = value; } }
        private int _ACS_chk63AreaAvTransitada;
        public int ACS_chk63AreaAvTransitada { get { return _ACS_chk63AreaAvTransitada; } set { _ACS_chk63AreaAvTransitada = value; } }
        private int _ACS_chk63EstCortesia;
        public int ACS_chk63EstCortesia { get { return _ACS_chk63EstCortesia; } set { _ACS_chk63EstCortesia = value; } }
        private int _ACS_chk63EstCosto;
        public int ACS_chk63EstCosto { get { return _ACS_chk63EstCosto; } set { _ACS_chk63EstCosto = value; } }
        private int _ACS_txt63EstMonto;
        public int ACS_txt63EstMonto { get { return _ACS_txt63EstMonto; } set { _ACS_txt63EstMonto = value; } }
        private string _ACS_txt63ClienteDireccion;
        public string ACS_txt63ClienteDireccion { get { return _ACS_txt63ClienteDireccion; } set { _ACS_txt63ClienteDireccion = value; } }
        private string _ACS_txt63ClienteColonia;
        public string ACS_txt63ClienteColonia { get { return _ACS_txt63ClienteColonia; } set { _ACS_txt63ClienteColonia = value; } }
        private string _ACS_txt63ClienteMunicipio;
        public string ACS_txt63ClienteMunicipio { get { return _ACS_txt63ClienteMunicipio; } set { _ACS_txt63ClienteMunicipio = value; } }
        private string _ACS_txt63ClienteEstado;
        public string ACS_txt63ClienteEstado { get { return _ACS_txt63ClienteEstado; } set { _ACS_txt63ClienteEstado = value; } }
        private string _ACS_txt63ClienteCodPost;
        public string ACS_txt63ClienteCodPost { get { return _ACS_txt63ClienteCodPost; } set { _ACS_txt63ClienteCodPost = value; } }
        private int _ACS_chk63DocFactFranquiciaEnt;
        public int ACS_chk63DocFactFranquiciaEnt { get { return _ACS_chk63DocFactFranquiciaEnt; } set { _ACS_chk63DocFactFranquiciaEnt = value; } }
        private int _ACS_txt63DocFactFranquiciaEntCop;
        public int ACS_txt63DocFactFranquiciaEntCop { get { return _ACS_txt63DocFactFranquiciaEntCop; } set { _ACS_txt63DocFactFranquiciaEntCop = value; } }
        private int _ACS_chk63DocFactFranquiciaRec;
        public int ACS_chk63DocFactFranquiciaRec { get { return _ACS_chk63DocFactFranquiciaRec; } set { _ACS_chk63DocFactFranquiciaRec = value; } }
        private int _ACS_txt63DocFactFranquiciaRecCop;
        public int ACS_txt63DocFactFranquiciaRecCop { get { return _ACS_txt63DocFactFranquiciaRecCop; } set { _ACS_txt63DocFactFranquiciaRecCop = value; } }
        private int _ACS_chk63DocFactKeyEnt;
        public int ACS_chk63DocFactKeyEnt { get { return _ACS_chk63DocFactKeyEnt; } set { _ACS_chk63DocFactKeyEnt = value; } }
        private int _ACS_txt63DocFactKeyEntCop;
        public int ACS_txt63DocFactKeyEntCop { get { return _ACS_txt63DocFactKeyEntCop; } set { _ACS_txt63DocFactKeyEntCop = value; } }
        private int _ACS_chk63DocFactKeyRec;
        public int ACS_chk63DocFactKeyRec { get { return _ACS_chk63DocFactKeyRec; } set { _ACS_chk63DocFactKeyRec = value; } }
        private int _ACS_txt63DocFactKeyRecCop;
        public int ACS_txt63DocFactKeyRecCop { get { return _ACS_txt63DocFactKeyRecCop; } set { _ACS_txt63DocFactKeyRecCop = value; } }
        private int _ACS_chk63DocOrdCompraEnt;
        public int ACS_chk63DocOrdCompraEnt { get { return _ACS_chk63DocOrdCompraEnt; } set { _ACS_chk63DocOrdCompraEnt = value; } }
        private int _ACS_txt63DocOrdCompraEntCop;
        public int ACS_txt63DocOrdCompraEntCop { get { return _ACS_txt63DocOrdCompraEntCop; } set { _ACS_txt63DocOrdCompraEntCop = value; } }
        private int _ACS_chk63DocOrdCompraRec;
        public int ACS_chk63DocOrdCompraRec { get { return _ACS_chk63DocOrdCompraRec; } set { _ACS_chk63DocOrdCompraRec = value; } }
        private int _ACS_txt63DocOrdCompraRecCop;
        public int ACS_txt63DocOrdCompraRecCop { get { return _ACS_txt63DocOrdCompraRecCop; } set { _ACS_txt63DocOrdCompraRecCop = value; } }
        private int _ACS_chk63DocOrdReposEnt;
        public int ACS_chk63DocOrdReposEnt { get { return _ACS_chk63DocOrdReposEnt; } set { _ACS_chk63DocOrdReposEnt = value; } }
        private int _ACS_txt63DocOrdReposEntCop;
        public int ACS_txt63DocOrdReposEntCop { get { return _ACS_txt63DocOrdReposEntCop; } set { _ACS_txt63DocOrdReposEntCop = value; } }
        private int _ACS_chk63DocOrdReposRec;
        public int ACS_chk63DocOrdReposRec { get { return _ACS_chk63DocOrdReposRec; } set { _ACS_chk63DocOrdReposRec = value; } }
        private int _ACS_txt63DocOrdReposRecCop;
        public int ACS_txt63DocOrdReposRecCop { get { return _ACS_txt63DocOrdReposRecCop; } set { _ACS_txt63DocOrdReposRecCop = value; } }
        private int _ACS_chk63DocCopPedidoEnt;
        public int ACS_chk63DocCopPedidoEnt { get { return _ACS_chk63DocCopPedidoEnt; } set { _ACS_chk63DocCopPedidoEnt = value; } }
        private int _ACS_txt63DocCopPedidoEntCop;
        public int ACS_txt63DocCopPedidoEntCop { get { return _ACS_txt63DocCopPedidoEntCop; } set { _ACS_txt63DocCopPedidoEntCop = value; } }
        private int _ACS_chk63DocCopPedidoRec;
        public int ACS_chk63DocCopPedidoRec { get { return _ACS_chk63DocCopPedidoRec; } set { _ACS_chk63DocCopPedidoRec = value; } }
        private int _ACS_txt63DocCopPedidoRecCop;
        public int ACS_txt63DocCopPedidoRecCop { get { return _ACS_txt63DocCopPedidoRecCop; } set { _ACS_txt63DocCopPedidoRecCop = value; } }
        private int _ACS_chk63DocRemisionEnt;
        public int ACS_chk63DocRemisionEnt { get { return _ACS_chk63DocRemisionEnt; } set { _ACS_chk63DocRemisionEnt = value; } }
        private int _ACS_txt63DocRemisionEntCop;
        public int ACS_txt63DocRemisionEntCop { get { return _ACS_txt63DocRemisionEntCop; } set { _ACS_txt63DocRemisionEntCop = value; } }
        private int _ACS_chk63DocRemisionRec;
        public int ACS_chk63DocRemisionRec { get { return _ACS_chk63DocRemisionRec; } set { _ACS_chk63DocRemisionRec = value; } }
        private int _ACS_txt63DocRemisionRecCop;
        public int ACS_txt63DocRemisionRecCop { get { return _ACS_txt63DocRemisionRecCop; } set { _ACS_txt63DocRemisionRecCop = value; } }
        private int _ACS_chk63DocFolioEnt;
        public int ACS_chk63DocFolioEnt { get { return _ACS_chk63DocFolioEnt; } set { _ACS_chk63DocFolioEnt = value; } }
        private int _ACS_txt63DocFolioEntCop;
        public int ACS_txt63DocFolioEntCop { get { return _ACS_txt63DocFolioEntCop; } set { _ACS_txt63DocFolioEntCop = value; } }
        private int _ACS_chk63DocFolioRec;
        public int ACS_chk63DocFolioRec { get { return _ACS_chk63DocFolioRec; } set { _ACS_chk63DocFolioRec = value; } }
        private int _ACS_txt63DocFolioRecCop;
        public int ACS_txt63DocFolioRecCop { get { return _ACS_txt63DocFolioRecCop; } set { _ACS_txt63DocFolioRecCop = value; } }
        private int _ACS_chk63DocContraRecEnt;
        public int ACS_chk63DocContraRecEnt { get { return _ACS_chk63DocContraRecEnt; } set { _ACS_chk63DocContraRecEnt = value; } }
        private int _ACS_txt63DocContraRecEntCop;
        public int ACS_txt63DocContraRecEntCop { get { return _ACS_txt63DocContraRecEntCop; } set { _ACS_txt63DocContraRecEntCop = value; } }
        private int _ACS_chk63DocContraRecRec;
        public int ACS_chk63DocContraRecRec { get { return _ACS_chk63DocContraRecRec; } set { _ACS_chk63DocContraRecRec = value; } }
        private int _ACS_txt63DocContraRecRecCop;
        public int ACS_txt63DocContraRecRecCop { get { return _ACS_txt63DocContraRecRecCop; } set { _ACS_txt63DocContraRecRecCop = value; } }
        private int _ACS_chk63DocEntAlmacenEnt;
        public int ACS_chk63DocEntAlmacenEnt { get { return _ACS_chk63DocEntAlmacenEnt; } set { _ACS_chk63DocEntAlmacenEnt = value; } }
        private int _ACS_txt63DocEntAlmacenEntCop;
        public int ACS_txt63DocEntAlmacenEntCop { get { return _ACS_txt63DocEntAlmacenEntCop; } set { _ACS_txt63DocEntAlmacenEntCop = value; } }
        private int _ACS_chk63DocEntAlmacenRec;
        public int ACS_chk63DocEntAlmacenRec { get { return _ACS_chk63DocEntAlmacenRec; } set { _ACS_chk63DocEntAlmacenRec = value; } }
        private int _ACS_txt63DocEntAlmacenRecCop;
        public int ACS_txt63DocEntAlmacenRecCop { get { return _ACS_txt63DocEntAlmacenRecCop; } set { _ACS_txt63DocEntAlmacenRecCop = value; } }
        private int _ACS_chk63DocSopServicioEnt;
        public int ACS_chk63DocSopServicioEnt { get { return _ACS_chk63DocSopServicioEnt; } set { _ACS_chk63DocSopServicioEnt = value; } }
        private int _ACS_txt63DocSopServicioEntCop;
        public int ACS_txt63DocSopServicioEntCop { get { return _ACS_txt63DocSopServicioEntCop; } set { _ACS_txt63DocSopServicioEntCop = value; } }
        private int _ACS_chk63DocSopServicioRec;
        public int ACS_chk63DocSopServicioRec { get { return _ACS_chk63DocSopServicioRec; } set { _ACS_chk63DocSopServicioRec = value; } }
        private int _ACS_txt63DocSopServicioRecCop;
        public int ACS_txt63DocSopServicioRecCop { get { return _ACS_txt63DocSopServicioRecCop; } set { _ACS_txt63DocSopServicioRecCop = value; } }
        private int _ACS_chk63DocNomFirmaEnt;
        public int ACS_chk63DocNomFirmaEnt { get { return _ACS_chk63DocNomFirmaEnt; } set { _ACS_chk63DocNomFirmaEnt = value; } }
        private int _ACS_txt63DocNomFirmaEntCop;
        public int ACS_txt63DocNomFirmaEntCop { get { return _ACS_txt63DocNomFirmaEntCop; } set { _ACS_txt63DocNomFirmaEntCop = value; } }
        private int _ACS_chk63DocNomFirmaoRec;
        public int ACS_chk63DocNomFirmaoRec { get { return _ACS_chk63DocNomFirmaoRec; } set { _ACS_chk63DocNomFirmaoRec = value; } }
        private int _ACS_txt63DocNomFirmaRecCop;
        public int ACS_txt63DocNomFirmaRecCop { get { return _ACS_txt63DocNomFirmaRecCop; } set { _ACS_txt63DocNomFirmaRecCop = value; } }
        private int _ACS_chk63CitaEnt;
        public int ACS_chk63CitaEnt { get { return _ACS_chk63CitaEnt; } set { _ACS_chk63CitaEnt = value; } }
        private int _ACS_txt63CitaEntCop;
        public int ACS_txt63CitaEntCop { get { return _ACS_txt63CitaEntCop; } set { _ACS_txt63CitaEntCop = value; } }
        private int _ACS_chk63CitaRec;
        public int ACS_chk63CitaRec { get { return _ACS_chk63CitaRec; } set { _ACS_chk63CitaRec = value; } }

        private int _ACS_txt63CitaRecCop;
        public int ACS_txt63CitaRecCop { get { return _ACS_txt63CitaRecCop; } set { _ACS_txt63CitaRecCop = value; } }

        private int _Acs_NumericTextBox;
        public int Acs_NumericTextBox { get { return _Acs_NumericTextBox; } set { _Acs_NumericTextBox = value; } }

        private int _Acs_OrdenAbiertaConRep;
        public int Acs_OrdenAbiertaConRep { get { return _Acs_OrdenAbiertaConRep; } set { _Acs_OrdenAbiertaConRep = value; } }

        private int _IdCte_DirEntrega;
        public int IdCte_DirEntrega { get { return _IdCte_DirEntrega; } set { _IdCte_DirEntrega = value; } }

        private int _Id_Modalidad;
        public int Id_Modalidad { get { return _Id_Modalidad; } set { _Id_Modalidad = value; } }
        private string _Acs_NomComercial;
        public string Acs_NomComercial { get { return _Acs_NomComercial; } set { _Acs_NomComercial = value; } }

        private string _Acs_Telefono5;
        public string Acs_Telefono5 { get { return _Acs_Telefono5; } set { _Acs_Telefono5 = value; } }

        private string _Acs_Correo5;
        public string Acs_Correo5 { get { return _Acs_Correo5; } set { _Acs_Correo5 = value; } }

        private string _Acs_Contacto6;
        public string Acs_Contacto6 { get { return _Acs_Contacto6; } set { _Acs_Contacto6 = value; } }

        private string _Acs_Telefono6;
        public string Acs_Telefono6 { get { return _Acs_Telefono6; } set { _Acs_Telefono6 = value; } }

        private string _Acs_Correo6;
        public string Acs_Correo6 { get { return _Acs_Correo6; } set { _Acs_Correo6 = value; } }
        private string _Acs_Proveedor;
        public string Acs_Proveedor { get { return _Acs_Proveedor; } set { _Acs_Proveedor = value; } }
        private int _Acs_RutaEntrega;
        public int Acs_RutaEntrega { get { return _Acs_RutaEntrega; } set { _Acs_RutaEntrega = value; } }
        private int _Acs_RutaServicio;
        public int Acs_RutaServicio { get { return _Acs_RutaServicio; } set { _Acs_RutaServicio = value; } }
        private bool _Acs_ReqOrdenCompra;
        public bool Acs_ReqOrdenCompra { get { return _Acs_ReqOrdenCompra; } set { _Acs_ReqOrdenCompra = value; } }

        private string _Acs_VigenciaIni;
        public string Acs_VigenciaIni { get { return _Acs_VigenciaIni; } set { _Acs_VigenciaIni = value; } }

        private int _Acs_Semana;
        public int Acs_Semana { get { return _Acs_Semana; } set { _Acs_Semana = value; } }

        private string _Acs_VigenciaTermina;
        public string Acs_VigenciaTermina { get { return _Acs_VigenciaTermina; } set { _Acs_VigenciaTermina = value; } }

        private bool _Acs_ReqConfirmacion;
        public bool Acs_ReqConfirmacion { get { return _Acs_ReqConfirmacion; } set { _Acs_ReqConfirmacion = value; } }

        private bool _Acs_RecCorreo;
        public bool Acs_RecCorreo { get { return _Acs_RecCorreo; } set { _Acs_RecCorreo = value; } }
        private bool _Acs_RecFax;
        public bool Acs_RecFax { get { return _Acs_RecFax; } set { _Acs_RecFax = value; } }
        private bool _Acs_RecTelefono;
        public bool Acs_RecTelefono { get { return _Acs_RecTelefono; } set { _Acs_RecTelefono = value; } }

        private bool _Acs_RecWhatsApp;
        public bool Acs_RecWhatsApp { get { return _Acs_RecWhatsApp; } set { _Acs_RecWhatsApp = value; } }

        private bool _Acs_RecRIK;
        public bool Acs_RecRIK { get { return _Acs_RecRIK; } set { _Acs_RecRIK = value; } }

        private bool _Acs_RecRepresentante;
        public bool Acs_RecRepresentante { get { return _Acs_RecRepresentante; } set { _Acs_RecRepresentante = value; } }
        private bool _Acs_RecPedWhats;
        public bool Acs_RecPedWhats { get { return _Acs_RecPedWhats; } set { _Acs_RecPedWhats = value; } }
        private bool _Acs_RecOtro;
        public bool Acs_RecOtro { get { return _Acs_RecOtro; } set { _Acs_RecOtro = value; } }
        private string _Acs_RecOtroDesc;
        public string Acs_RecOtroDesc { get { return _Acs_RecOtroDesc; } set { _Acs_RecOtroDesc = value; } }

        private bool _Acs_RecPedCorreo;
        public bool Acs_RecPedCorreo { get { return _Acs_RecPedCorreo; } set { _Acs_RecPedCorreo = value; } }
        private bool _Acs_RecPedFax;
        public bool Acs_RecPedFax { get { return _Acs_RecPedFax; } set { _Acs_RecPedFax = value; } }
        private bool _Acs_RecPedTel;
        public bool Acs_RecPedTel { get { return _Acs_RecPedTel; } set { _Acs_RecPedTel = value; } }
        private bool _Acs_RecPedRep;
        public bool Acs_RecPedRep { get { return _Acs_RecPedRep; } set { _Acs_RecPedRep = value; } }
        private bool _Acs_RecPedOtro;
        public bool Acs_RecPedOtro { get { return _Acs_RecPedOtro; } set { _Acs_RecPedOtro = value; } }
        private string _Acs_RecPedOtroStr;
        public string Acs_RecPedOtroStr { get { return _Acs_RecPedOtroStr; } set { _Acs_RecPedOtroStr = value; } }
        private int _Id_U;
        public int Id_U { get { return _Id_U; } set { _Id_U = value; } }
        private string _Acs_RikNombre;
        public string Acs_RikNombre { get { return _Acs_RikNombre; } set { _Acs_RikNombre = value; } }
        private string _Acs_RscNombre;
        public string Acs_RscNombre { get { return _Acs_RscNombre; } set { _Acs_RscNombre = value; } }
        private int _Acs_RscIdTerr;
        public int Acs_RscIdTerr { get { return _Acs_RscIdTerr; } set { _Acs_RscIdTerr = value; } }
        private string _Acs_RscTerritorio;
        public string Acs_RscTerritorio { get { return _Acs_RscTerritorio; } set { _Acs_RscTerritorio = value; } }

        private double _Vis_Frecuencia;
        public double Vis_Frecuencia { get { return _Vis_Frecuencia; } set { _Vis_Frecuencia = value; } }

        // L M M J V S D
        private bool _Vis_Lunes;
        public bool Vis_Lunes { get { return _Vis_Lunes; } set { _Vis_Lunes = value; } }
        private bool _Vis_Martes;
        public bool Vis_Martes { get { return _Vis_Martes; } set { _Vis_Martes = value; } }
        private bool _Vis_Miercoles;
        public bool Vis_Miercoles { get { return _Vis_Miercoles; } set { _Vis_Miercoles = value; } }
        private bool _Vis_Jueves;
        public bool Vis_Jueves { get { return _Vis_Jueves; } set { _Vis_Jueves = value; } }
        private bool _Vis_Viernes;
        public bool Vis_Viernes { get { return _Vis_Viernes; } set { _Vis_Viernes = value; } }
        private bool _Vis_Sabado;
        public bool Vis_Sabado { get { return _Vis_Sabado; } set { _Vis_Sabado = value; } }
        private bool _Vis_Domingo;
        public bool Vis_Domingo { get { return _Vis_Domingo; } set { _Vis_Domingo = value; } }
        private bool _Vis_CualquierDia;
        public bool Vis_CualquierDia { get { return _Vis_CualquierDia; } set { _Vis_CualquierDia = value; } }

        private string _Vis_HrAm1;
        public string Vis_HrAm1 { get { return _Vis_HrAm1; } set { _Vis_HrAm1 = value; } }
        private string _Vis_HrAm2;
        public string Vis_HrAm2 { get { return _Vis_HrAm2; } set { _Vis_HrAm2 = value; } }
        private string _Vis_HrPm1;
        public string Vis_HrPm1 { get { return _Vis_HrPm1; } set { _Vis_HrPm1 = value; } }
        private string _Vis_HrPm2;
        public string Vis_HrPm2 { get { return _Vis_HrPm2; } set { _Vis_HrPm2 = value; } }
        private string _Rec_Semanas;
        public string Rec_Semanas { get { return _Rec_Semanas; } set { _Rec_Semanas = value; } }

        // L M M J V S D
        private bool _Rec_Lunes;
        public bool Rec_Lunes { get { return _Rec_Lunes; } set { _Rec_Lunes = value; } }
        private bool _Rec_Martes;
        public bool Rec_Martes { get { return _Rec_Martes; } set { _Rec_Martes = value; } }
        private bool _Rec_Miercoles;
        public bool Rec_Miercoles { get { return _Rec_Miercoles; } set { _Rec_Miercoles = value; } }
        private bool _Rec_Jueves;
        public bool Rec_Jueves { get { return _Rec_Jueves; } set { _Rec_Jueves = value; } }
        private bool _Rec_Viernes;
        public bool Rec_Viernes { get { return _Rec_Viernes; } set { _Rec_Viernes = value; } }
        private bool _Rec_Sabado;
        public bool Rec_Sabado { get { return _Rec_Sabado; } set { _Rec_Sabado = value; } }
        private bool _Rec_Domingo;
        public bool Rec_Domingo { get { return _Rec_Domingo; } set { _Rec_Domingo = value; } }
        private bool _Rec_CualquierDia;
        public bool Rec_CualquierDia { get { return _Rec_CualquierDia; } set { _Rec_CualquierDia = value; } }

        private bool _Rec_Confirmacion;
        public bool Rec_Confirmacion { get { return _Rec_Confirmacion; } set { _Rec_Confirmacion = value; } }
        private bool _Rec_Correo;
        public bool Rec_Correo { get { return _Rec_Correo; } set { _Rec_Correo = value; } }
        private bool _Rec_Fax;
        public bool Rec_Fax { get { return _Rec_Fax; } set { _Rec_Fax = value; } }
        private bool _Rec_Telefono;
        public bool Rec_Telefono { get { return _Rec_Telefono; } set { _Rec_Telefono = value; } }
        private bool _Rec_Representante;
        public bool Rec_Representante { get { return _Rec_Representante; } set { _Rec_Representante = value; } }
        private bool _Rec_Otro;
        public bool Rec_Otro { get { return _Rec_Otro; } set { _Rec_Otro = value; } }

        private string _Rec_OtroStr;
        public string Rec_OtroStr { get { return _Rec_OtroStr; } set { _Rec_OtroStr = value; } }
        private double _VentaMes;
        public double VentaMes { get { return _VentaMes; } set { _VentaMes = value; } }
        private double _VentaInst;
        public double VentaInst { get { return _VentaInst; } set { _VentaInst = value; } }
        private double _VentaProm;
        public double VentaProm { get { return _VentaProm; } set { _VentaProm = value; } }

        // CONDICIONES DE PAGO -> ESPECIFICACIONES ADICIONALES                                         
        // Tipo 2

        private CapAcys_EspecAdi _CondPago_EA;
        public CapAcys_EspecAdi CondPago_EA { get { return _CondPago_EA; } set { _CondPago_EA = value; } }

        private eCapAcys2_ServicioValor _ServTecnico;
        public eCapAcys2_ServicioValor ServTecnico { get { return _ServTecnico; } set { _ServTecnico = value; } }

        private eCapAcys2_ServicioValor _ServCapacitacion;
        public eCapAcys2_ServicioValor ServCapacitacion { get { return _ServCapacitacion; } set { _ServCapacitacion = value; } }

        private eCapAcys2_ServicioValor _ServAuditoria;
        public eCapAcys2_ServicioValor ServAuditoria { get { return _ServAuditoria; } set { _ServAuditoria = value; } }

        private eCapAcys2_ServicioValor _ServAsesoria;
        public eCapAcys2_ServicioValor ServAsesoria { get { return _ServAsesoria; } set { _ServAsesoria = value; } }

        /*
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
        private int _Id_Acs;
        public int Id_Acs
        {
            get { return _Id_Acs; }
            set { _Id_Acs = value; }
        }
        private int _Id_AcsVersion;
        public int Id_AcsVersion
        {
            get { return _Id_AcsVersion; }
            set { _Id_AcsVersion = value; }
        }  
            
        private string _Cte_NomComercial;        
         
        
        // Cliente 
        private int _Id_Cte;
        public int Id_Cte
        {
            get { return _Id_Cte; }
            set { _Id_Cte= value; }
        }
        private string _Cte_Nombre;
        public string Cte_Nombre
        {
            get { return _Cte_Nombre; }
            set { _Cte_Nombre= value; }
        }
        
        private int _Id_U;
        public int Id_U
        {
            get { return _Id_U; }
            set { _Id_U = value; }
        }        
        
        public string Cte_NomComercial
        {
            get { return _Cte_NomComercial; }
            set { _Cte_NomComercial = value; }
        }
        private string _Id_Ter;
        public string Id_Ter
        {
            get { return _Id_Ter; }
            set { _Id_Ter= value; }
        }
        private int _Id_Rik;
        public int Id_Rik
        {
            get { return _Id_Rik; }
            set { _Id_Rik= value; }
        }
        //ACyS
        private string _Acs_Fecha;
        public string Acs_Fecha
        {
            get { return _Acs_Fecha; }
            set { _Acs_Fecha= value; }
        }
        private string _Acs_FechaInicioDocumento;
        public string Acs_FechaInicioDocumento
        {
            get { return _Acs_FechaInicioDocumento; }
            set { _Acs_FechaInicioDocumento= value; }
        }
        private string _Acs_FechaFinDocumento;
        public string Acs_FechaFinDocumento
        {
            get { return _Acs_FechaFinDocumento; }
            set { _Acs_FechaFinDocumento= value; }
        }
        private string _Acs_Vencido;
        public string Acs_Vencido
        {
            get { return _Acs_Vencido; }
            set { _Acs_Vencido = value; }
        }
       
        private string _Acs_Contacto;
        public string Acs_Contacto
        {
            get { return _Acs_Contacto; }
            set { _Acs_Contacto = value; }
        }
        private string _Acs_Puesto;
        public string Acs_Puesto
        {
            get { return _Acs_Puesto; }
            set { _Acs_Puesto= value; }
        }
        private string _Acs_Telefono;
        public string Acs_Telefono
        {
            get { return _Acs_Telefono; }
            set { _Acs_Telefono = value; }
        }
        private string _Acs_Correo;
        public string Acs_Correo
        {
            get { return _Acs_Correo; }
            set { _Acs_Correo = value; }
        }
        // Cliente 
        private string _ClienteDireccion;
        public string ClienteDireccion
        {
            get { return _ClienteDireccion; }
            set { _ClienteDireccion = value; }
        }
        private string _ClienteColonia ;
        public string ClienteColonia 
        {
            get { return _ClienteColonia; }
            set { _ClienteColonia = value; }
        }
        private string _ClienteMunicipio;
        public string ClienteMunicipio
        {
            get { return _ClienteMunicipio; }
            set { _ClienteMunicipio= value; }
        }
        private string _ClienteEstado;
        public string ClienteEstado
        {
            get { return _ClienteEstado; }
            set { _ClienteEstado= value; }
        }
        private string _ClienteRFC;
        public string ClienteRFC
        {
            get { return _ClienteRFC; }
            set { _ClienteRFC= value; }
        }
        private string _ClienteCodPost;
        public string ClienteCodPost
        {
            get { return _ClienteCodPost; }
            set { _ClienteCodPost= value; }
        }
        private bool _CuentaCorporativa;
        public bool CuentaCorporativa
        {
            get { return _CuentaCorporativa; }
            set { _CuentaCorporativa= value; }
        }
        private bool _AddendaSI;
        public bool AddendaSI { get { return _AddendaSI; } set { _AddendaSI= value; } }
        
        private string _DireccionEntrega;
        public string DireccionEntrega { get { return _DireccionEntrega; } set { _DireccionEntrega= value; }}
        private string _ClienteColoniaE;
        public string ClienteColoniaE { get { return _ClienteColoniaE; } set { _ClienteColoniaE= value; } }
        private string _ClienteMunicipioE;
        public string ClienteMunicipioE { get { return _ClienteMunicipioE; } set { _ClienteMunicipioE= value; } }
        private string _ClienteCPE;
        public string ClienteCPE
        {
            get { return _ClienteCPE; }
            set { _ClienteCPE = value; }
        }
        private string _ClienteEstadoE;
        public string ClienteEstadoE
        {
            get { return _ClienteEstadoE; }
            set { _ClienteEstadoE= value; }
        }
        private string _Acs_Contacto2;
        public string Acs_Contacto2
        {
            get { return _Acs_Contacto2; }
            set { _Acs_Contacto2= value; }
        }
        private string _Acs_Telefono2;
        public string Acs_Telefono2
        {
            get { return _Acs_Telefono2; }
            set { _Acs_Telefono2= value; }
        }
        private string _Acs_Correo2;
        public string Acs_Correo2
        {
            get { return _Acs_Correo2; }
            set { _Acs_Correo2= value; }
        }
        private string _Acs_Contacto3;
        public string Acs_Contacto3 { get { return _Acs_Contacto3; } set { _Acs_Contacto3 = value; } }
        private string _Acs_Telefono3;
        public string Acs_Telefono3 { get { return _Acs_Telefono3; } set { _Acs_Telefono3 = value; } }
        private string _Acs_Correo3;
        public string Acs_Correo3 { get { return _Acs_Correo3; } set { _Acs_Correo3 = value; } }
        private string _Acs_Contacto4;
        public string Acs_Contacto4 { get { return _Acs_Contacto4; } set { _Acs_Contacto4 = value; } }
        private string _Acs_Telefono4;
        public string Acs_Telefono4 { get { return _Acs_Telefono4; } set { _Acs_Telefono4 = value; } }
        private string _Acs_Correo4;
        public string Acs_Correo4 { get { return _Acs_Correo4; } set { _Acs_Correo4 = value; } }
         */

        private string _Acs_Contacto5;
        public string Acs_Contacto5 { get { return _Acs_Contacto5; } set { _Acs_Contacto5 = value; } }

        /*
        private string _Acs_Proveedor;
        public string Acs_Proveedor { get { return _Acs_Proveedor; } set { _Acs_Proveedor = value; } }
        
        private int _Acs_RutaServicio;
        public int Acs_RutaServicio { get { return _Acs_RutaServicio; } set { _Acs_RutaServicio = value; } }
        private int _Acs_RutaEntrega;
        public int Acs_RutaEntrega { get { return _Acs_RutaEntrega; } set { _Acs_RutaEntrega = value; } }
        
        private bool _Acs_ReqOrdenCompra;
        public bool Acs_ReqOrdenCompra { get { return _Acs_ReqOrdenCompra; } set { _Acs_ReqOrdenCompra = value; } }
        private string _Acs_VigenciaIni;
        public string Acs_VigenciaIni { get { return _Acs_VigenciaIni; } set { _Acs_VigenciaIni = value; } }
        private int _Acs_Semana;
        public int Acs_Semana { get { return _Acs_Semana; } set { _Acs_Semana = value; } }
        
        //
        private bool _Acs_ReqConfirmacion;
        public bool Acs_ReqConfirmacion { get { return _Acs_ReqConfirmacion; } set { _Acs_ReqConfirmacion = value; } }
        private bool _Acs_RecCorreo;
        public bool Acs_RecCorreo { get { return _Acs_RecCorreo; } set { _Acs_RecCorreo = value; } }
        private bool _Acs_RecFax;
        public bool Acs_RecFax { get { return _Acs_RecFax; } set { _Acs_RecFax = value; } }
        private bool _Acs_RecTelefono;
        public bool Acs_RecTelefono { get { return _Acs_RecTelefono; } set { _Acs_RecTelefono = value; } }
        private bool _Acs_RecRepresentante;
        public bool Acs_RecRepresentante { get { return _Acs_RecRepresentante; } set { _Acs_RecRepresentante = value; } }
        
        private bool _Acs_RecPedWhats;
        public bool Acs_RecPedWhats { get { return _Acs_RecPedWhats; } set { _Acs_RecPedWhats = value; } }
        
        private bool _Acs_RecOtro;
        public bool Acs_RecOtro { get { return _Acs_RecOtro; } set { _Acs_RecOtro = value; } }
        
        private string _Acs_RecOtroDesc;
        public string Acs_RecOtroDesc { get { return _Acs_RecOtroDesc; } set { _Acs_RecOtroDesc = value; } }
        private string _Acs_Estatus;
        public string Acs_Estatus { get { return _Acs_Estatus; } set { _Acs_Estatus = value; } }
        private string _Acs_EstatusTexto;
        public string Acs_EstatusTexto { get { return _Acs_EstatusTexto; } set { _Acs_EstatusTexto = value; } }
        //
        
        private int _Acs_ContactoCCreCob;
        public int Acs_ContactoCCreCob { get { return _Acs_ContactoCCreCob; } set { _Acs_ContactoCCreCob = value; } }
        private string _Acs_ContactoCCreCobTel;
        public string Acs_ContactoCCreCobTel { get { return _Acs_ContactoCCreCobTel; } set { _Acs_ContactoCCreCobTel = value; } }
        private string _Acs_ContactoCCreCobEmail;
        public string Acs_ContactoCCreCobEmail { get { return _Acs_ContactoCCreCobEmail; } set { _Acs_ContactoCCreCobEmail = value; } }
        private string _Acs_Modalidad;
        public string Acs_Modalidad { get { return _Acs_Modalidad; } set { _Acs_Modalidad = value; } }
        private string _Acs_Version;
        public string Acs_Version { get { return _Acs_Version; } set { _Acs_Version = value; } }
        private string _Acs_RikNombre;
        public string Acs_RikNombre { get { return _Acs_RikNombre; } set { _Acs_RikNombre = value; } }
        private int _IdCte_DirEntrega;
        public int IdCte_DirEntrega { get { return _IdCte_DirEntrega; } set { _IdCte_DirEntrega = value; } }
        private string _Acs_Sucursal;
        public string Acs_Sucursal { get { return _Acs_Sucursal; } set { _Acs_Sucursal = value; } }
        private int _Acs_ParcialidadesSi;
        public int Acs_ParcialidadesSi { get { return _Acs_ParcialidadesSi; } set { _Acs_ParcialidadesSi = value; } }
        private int _Acs_ParcialidadesNo;
        public int Acs_ParcialidadesNo { get { return _Acs_ParcialidadesNo; } set { _Acs_ParcialidadesNo = value; } }
        private int _Acs_ConfirmacionPedidosSI;
        public int Acs_ConfirmacionPedidosSI { get { return _Acs_ConfirmacionPedidosSI; } set { _Acs_ConfirmacionPedidosSI = value; } }
        private int _Acs_ConfirmacionPedidosnO;
        public int Acs_ConfirmacionPedidosnO { get { return _Acs_ConfirmacionPedidosnO; } set { _Acs_ConfirmacionPedidosnO = value; } }
        private int _Acs_chkRecRevLunes;
        public int Acs_chkRecRevLunes { get { return _Acs_chkRecRevLunes; } set { _Acs_chkRecRevLunes = value; } }
        private int _Acs_RecRevMartes;
        public int Acs_RecRevMartes { get { return _Acs_RecRevMartes; } set { _Acs_RecRevMartes = value; } }
        private int _Acs_RecRevMiercoles;
        public int Acs_RecRevMiercoles { get { return _Acs_RecRevMiercoles; } set { _Acs_RecRevMiercoles = value; } }
        private int _Acs_RecRevJueves;
        public int Acs_RecRevJueves { get { return _Acs_RecRevJueves; } set { _Acs_RecRevJueves = value; } }
        private int _Acs_RecRevViernes;
        public int Acs_RecRevViernes { get { return _Acs_RecRevViernes; } set { _Acs_RecRevViernes = value; } }
        private int _Acs_RecRevSabado;
        public int Acs_RecRevSabado { get { return _Acs_RecRevSabado; } set { _Acs_RecRevSabado = value; } }
        private string _Acs_TimePicker1;
        public string Acs_TimePicker1 { get { return _Acs_TimePicker1; } set { _Acs_TimePicker1 = value; } }
        private string _Acs_TimePicker2;
        public string Acs_TimePicker2 { get { return _Acs_TimePicker2; } set { _Acs_TimePicker2 = value; } }
        private string _Acs_TimePicker3;
        public string Acs_TimePicker3 { get { return _Acs_TimePicker3; } set { _Acs_TimePicker3 = value; } }
        private string _Acs_TimePicker4;
        public string Acs_TimePicker4 { get { return _Acs_TimePicker4; } set { _Acs_TimePicker4 = value; } }
        private string _Acs_RecPersonaRecibe;
        public string Acs_RecPersonaRecibe { get { return _Acs_RecPersonaRecibe; } set { _Acs_RecPersonaRecibe = value; } }
        private string _Acs_RecPuesto;
        public string Acs_RecPuesto { get { return _Acs_RecPuesto; } set { _Acs_RecPuesto = value; } }
        private int _Acs_RecCitaMismoDia;
        public int Acs_RecCitaMismoDia { get { return _Acs_RecCitaMismoDia; } set { _Acs_RecCitaMismoDia = value; } }
        private int _Acs_RecCitaSinCita;
        public int Acs_RecCitaSinCita { get { return _Acs_RecCitaSinCita; } set { _Acs_RecCitaSinCita = value; } }
        private int _Acs_RecCitaPrevia;
        public int Acs_RecCitaPrevia { get { return _Acs_RecCitaPrevia; } set { _Acs_RecCitaPrevia = value; } }
        private string _Acs_RecCitaContacto;
        public string Acs_RecCitaContacto { get { return _Acs_RecCitaContacto; } set { _Acs_RecCitaContacto = value; } }
        private string _Acs_RecCitaTelefono;
        public string Acs_RecCitaTelefono { get { return _Acs_RecCitaTelefono; } set { _Acs_RecCitaTelefono = value; } }
        private int _Acs_RecCitaDiasdeAnticipacion;
        public int Acs_RecCitaDiasdeAnticipacion { get { return _Acs_RecCitaDiasdeAnticipacion; } set { _Acs_RecCitaDiasdeAnticipacion = value; } }
        private int _Acs_RecAreaPropia;
        public int Acs_RecAreaPropia { get { return _Acs_RecAreaPropia; } set { _Acs_RecAreaPropia = value; } }
        private int _Acs_RecAreaPlaza;
        public int Acs_RecAreaPlaza { get { return _Acs_RecAreaPlaza; } set { _Acs_RecAreaPlaza = value; } }
        private int _Acs_RecAreaCalle;
        public int Acs_RecAreaCalle { get { return _Acs_RecAreaCalle; } set { _Acs_RecAreaCalle = value; } }
        private int _Acs_RecAreaAvTransitada;
        public int Acs_RecAreaAvTransitada { get { return _Acs_RecAreaAvTransitada; } set { _Acs_RecAreaAvTransitada = value; } }
        private int _Acs_RecEstCortesia;
        public int Acs_RecEstCortesia { get { return _Acs_RecEstCortesia; } set { _Acs_RecEstCortesia = value; } }
        private int _Acs_RecEstCosto;
        public int Acs_RecEstCosto { get { return _Acs_RecEstCosto; } set { _Acs_RecEstCosto = value; } }
        private int _Acs_RecEstMonto;
        public int Acs_RecEstMonto { get { return _Acs_RecEstMonto; } set { _Acs_RecEstMonto = value; } }
        private int _Acs_RecDocFactFranquiciaEnt;
        public int Acs_RecDocFactFranquiciaEnt { get { return _Acs_RecDocFactFranquiciaEnt; } set { _Acs_RecDocFactFranquiciaEnt = value; } }
        private int _Acs_RecDocFactFranquiciaEntCop;
        public int Acs_RecDocFactFranquiciaEntCop { get { return _Acs_RecDocFactFranquiciaEntCop; } set { _Acs_RecDocFactFranquiciaEntCop = value; } }
        private int _Acs_RecDocFactFranquiciaRec;
        public int Acs_RecDocFactFranquiciaRec { get { return _Acs_RecDocFactFranquiciaRec; } set { _Acs_RecDocFactFranquiciaRec = value; } }
        private int _Acs_RecDocFactFranquiciaRecCop;
        public int Acs_RecDocFactFranquiciaRecCop { get { return _Acs_RecDocFactFranquiciaRecCop; } set { _Acs_RecDocFactFranquiciaRecCop = value; } }
        private int _Acs_RecDocFactKeyEnt;
        public int Acs_RecDocFactKeyEnt { get { return _Acs_RecDocFactKeyEnt; } set { _Acs_RecDocFactKeyEnt = value; } }
        private int _Acs_RecDocFactKeyEntCop;
        public int Acs_RecDocFactKeyEntCop { get { return _Acs_RecDocFactKeyEntCop; } set { _Acs_RecDocFactKeyEntCop = value; } }
        private int _Acs_RecDocFactKeyRec;
        public int Acs_RecDocFactKeyRec { get { return _Acs_RecDocFactKeyRec; } set { _Acs_RecDocFactKeyRec = value; } }
        private int _Acs_RecDocFactKeyRecCop;
        public int Acs_RecDocFactKeyRecCop { get { return _Acs_RecDocFactKeyRecCop; } set { _Acs_RecDocFactKeyRecCop = value; } }
        private int _Acs_RecDocOrdCompraEnt;
        public int Acs_RecDocOrdCompraEnt { get { return _Acs_RecDocOrdCompraEnt; } set { _Acs_RecDocOrdCompraEnt = value; } }
        private int _Acs_RecDocOrdCompraEntCop;
        public int Acs_RecDocOrdCompraEntCop { get { return _Acs_RecDocOrdCompraEntCop; } set { _Acs_RecDocOrdCompraEntCop = value; } }
        private int _Acs_RecDocOrdCompraRec;
        public int Acs_RecDocOrdCompraRec { get { return _Acs_RecDocOrdCompraRec; } set { _Acs_RecDocOrdCompraRec = value; } }
        private int _Acs_RecDocOrdCompraRecCop;
        public int Acs_RecDocOrdCompraRecCop { get { return _Acs_RecDocOrdCompraRecCop; } set { _Acs_RecDocOrdCompraRecCop = value; } }
        private int _Acs_RecDocOrdReposEnt;
        public int Acs_RecDocOrdReposEnt { get { return _Acs_RecDocOrdReposEnt; } set { _Acs_RecDocOrdReposEnt = value; } }
        private int _Acs_RecDocOrdReposEntCop;
        public int Acs_RecDocOrdReposEntCop { get { return _Acs_RecDocOrdReposEntCop; } set { _Acs_RecDocOrdReposEntCop = value; } }
        private int _Acs_RecDocOrdReposRec;
        public int Acs_RecDocOrdReposRec { get { return _Acs_RecDocOrdReposRec; } set { _Acs_RecDocOrdReposRec = value; } }
        private int _Acs_RecDocOrdReposRecCop;
        public int Acs_RecDocOrdReposRecCop { get { return _Acs_RecDocOrdReposRecCop; } set { _Acs_RecDocOrdReposRecCop = value; } }
        private int _Acs_RecDocCopPedidoEnt;
        public int Acs_RecDocCopPedidoEnt { get { return _Acs_RecDocCopPedidoEnt; } set { _Acs_RecDocCopPedidoEnt = value; } }
        private int _Acs_RecDocCopPedidoEntCop;
        public int Acs_RecDocCopPedidoEntCop { get { return _Acs_RecDocCopPedidoEntCop; } set { _Acs_RecDocCopPedidoEntCop = value; } }
        private int _Acs_RecDocCopPedidoRec;
        public int Acs_RecDocCopPedidoRec { get { return _Acs_RecDocCopPedidoRec; } set { _Acs_RecDocCopPedidoRec = value; } }
        private int _Acs_RecDocCopPedidoRecCop;
        public int Acs_RecDocCopPedidoRecCop { get { return _Acs_RecDocCopPedidoRecCop; } set { _Acs_RecDocCopPedidoRecCop = value; } }
        private int _ACS_RecDocRemisionEnt;
        public int ACS_RecDocRemisionEnt { get { return _ACS_RecDocRemisionEnt; } set { _ACS_RecDocRemisionEnt = value; } }
        private int _ACS_RecDocRemisionEntCop;
        public int ACS_RecDocRemisionEntCop { get { return _ACS_RecDocRemisionEntCop; } set { _ACS_RecDocRemisionEntCop = value; } }
        private int _ACS_RecDocRemisionRec;
        public int ACS_RecDocRemisionRec { get { return _ACS_RecDocRemisionRec; } set { _ACS_RecDocRemisionRec = value; } }
        private int _ACS_RecDocRemisionRecCop;
        public int ACS_RecDocRemisionRecCop { get { return _ACS_RecDocRemisionRecCop; } set { _ACS_RecDocRemisionRecCop = value; } }
        private int _ACS_RecDocFolioEnt;
        public int ACS_RecDocFolioEnt { get { return _ACS_RecDocFolioEnt; } set { _ACS_RecDocFolioEnt = value; } }
        private int _ACS_RecDocFolioEntCop;
        public int ACS_RecDocFolioEntCop { get { return _ACS_RecDocFolioEntCop; } set { _ACS_RecDocFolioEntCop = value; } }
        private int _ACS_RecDocFolioRec;
        public int ACS_RecDocFolioRec { get { return _ACS_RecDocFolioRec; } set { _ACS_RecDocFolioRec = value; } }
        private int _ACS_RecDocFolioRecCop;
        public int ACS_RecDocFolioRecCop { get { return _ACS_RecDocFolioRecCop; } set { _ACS_RecDocFolioRecCop = value; } }
        private int _ACS_RecDocContraRecEnt;
        public int ACS_RecDocContraRecEnt { get { return _ACS_RecDocContraRecEnt; } set { _ACS_RecDocContraRecEnt = value; } }
        private int _ACS_RecDocContraRecEntCop;
        public int ACS_RecDocContraRecEntCop { get { return _ACS_RecDocContraRecEntCop; } set { _ACS_RecDocContraRecEntCop = value; } }
        private int _ACS_RecDocContraRecRec;
        public int ACS_RecDocContraRecRec { get { return _ACS_RecDocContraRecRec; } set { _ACS_RecDocContraRecRec = value; } }
        private int _ACS_RecDocContraRecRecCop;
        public int ACS_RecDocContraRecRecCop { get { return _ACS_RecDocContraRecRecCop; } set { _ACS_RecDocContraRecRecCop = value; } }
        private int _ACS_RecDocEntAlmacenEnt;
        public int ACS_RecDocEntAlmacenEnt { get { return _ACS_RecDocEntAlmacenEnt; } set { _ACS_RecDocEntAlmacenEnt = value; } }
        private int _ACS_RecDocEntAlmacenEntCop;
        public int ACS_RecDocEntAlmacenEntCop { get { return _ACS_RecDocEntAlmacenEntCop; } set { _ACS_RecDocEntAlmacenEntCop = value; } }
        private int _ACS_RecDocEntAlmacenRec;
        public int ACS_RecDocEntAlmacenRec { get { return _ACS_RecDocEntAlmacenRec; } set { _ACS_RecDocEntAlmacenRec = value; } }
        private int _ACS_RecDocEntAlmacenRecCop;
        public int ACS_RecDocEntAlmacenRecCop { get { return _ACS_RecDocEntAlmacenRecCop; } set { _ACS_RecDocEntAlmacenRecCop = value; } }
        private int _ACS_RecDocSopServicioEnt;
        public int ACS_RecDocSopServicioEnt { get { return _ACS_RecDocSopServicioEnt; } set { _ACS_RecDocSopServicioEnt = value; } }
        private int _ACS_RecDocSopServicioEntCop;
        public int ACS_RecDocSopServicioEntCop { get { return _ACS_RecDocSopServicioEntCop; } set { _ACS_RecDocSopServicioEntCop = value; } }
        private int _ACS_RecDocSopServicioRec;
        public int ACS_RecDocSopServicioRec { get { return _ACS_RecDocSopServicioRec; } set { _ACS_RecDocSopServicioRec = value; } }
        private int _ACS_RecDocSopServicioRecCop;
        public int ACS_RecDocSopServicioRecCop { get { return _ACS_RecDocSopServicioRecCop; } set { _ACS_RecDocSopServicioRecCop = value; } }
        private int _ACS_RecDocNomFirmaEnt;
        public int ACS_RecDocNomFirmaEnt { get { return _ACS_RecDocNomFirmaEnt; } set { _ACS_RecDocNomFirmaEnt = value; } }
        private int _ACS_RecDocNomFirmaEntCop;
        public int ACS_RecDocNomFirmaEntCop { get { return _ACS_RecDocNomFirmaEntCop; } set { _ACS_RecDocNomFirmaEntCop = value; } }
        private int _ACS_RecDocNomFirmaoRec;
        public int ACS_RecDocNomFirmaoRec { get { return _ACS_RecDocNomFirmaoRec; } set { _ACS_RecDocNomFirmaoRec = value; } }
        private int _ACS_RecDocNomFirmaRecCop;
        public int ACS_RecDocNomFirmaRecCop { get { return _ACS_RecDocNomFirmaRecCop; } set { _ACS_RecDocNomFirmaRecCop = value; } }
        private int _ACS_RecCitaEnt;
        public int ACS_RecCitaEnt { get { return _ACS_RecCitaEnt; } set { _ACS_RecCitaEnt = value; } }
        private int _ACS_RecCitaEntCop;
        public int ACS_RecCitaEntCop { get { return _ACS_RecCitaEntCop; } set { _ACS_RecCitaEntCop = value; } }
        private int _ACS_RecCitaRec;
        public int ACS_RecCitaRec { get { return _ACS_RecCitaRec; } set { _ACS_RecCitaRec = value; } }
        private int _ACS_RecCitaRecCop;
        public int ACS_RecCitaRecCop { get { return _ACS_RecCitaRecCop; } set { _ACS_RecCitaRecCop = value; } }
        private string _ACS_RecOtroRec;
        public string ACS_RecOtroRec { get { return _ACS_RecOtroRec; } set { _ACS_RecOtroRec = value; } }
        private int _ACS_chk62Lunes;
        public int ACS_chk62Lunes { get { return _ACS_chk62Lunes; } set { _ACS_chk62Lunes = value; } }
        private int _ACS_chk62Martes;
        public int ACS_chk62Martes { get { return _ACS_chk62Martes; } set { _ACS_chk62Martes = value; } }
        private int _ACS_chk62Miercoles;
        public int ACS_chk62Miercoles { get { return _ACS_chk62Miercoles; } set { _ACS_chk62Miercoles = value; } }
        private int _ACS_chk62Jueves;
        public int ACS_chk62Jueves { get { return _ACS_chk62Jueves; } set { _ACS_chk62Jueves = value; } }
        private int _ACS_chk62Viernes;
        public int ACS_chk62Viernes { get { return _ACS_chk62Viernes; } set { _ACS_chk62Viernes = value; } }
        private int _ACS_chk62Sabado;
        public int ACS_chk62Sabado { get { return _ACS_chk62Sabado; } set { _ACS_chk62Sabado = value; } }
        private string _ACS_RadTimePicker162;
        public string ACS_RadTimePicker162 { get { return _ACS_RadTimePicker162; } set { _ACS_RadTimePicker162 = value; } }
        private string _ACS_RadTimePicker262;
        public string ACS_RadTimePicker262 { get { return _ACS_RadTimePicker262; } set { _ACS_RadTimePicker262 = value; } }
        private string _ACS_RadTimePicker362;
        public string ACS_RadTimePicker362 { get { return _ACS_RadTimePicker362; } set { _ACS_RadTimePicker362 = value; } }
        private string _ACS_RadTimePicker462;
        public string ACS_RadTimePicker462 { get { return _ACS_RadTimePicker462; } set { _ACS_RadTimePicker462 = value; } }
        private string _ACS_txtRecPersonaRecibe62;
        public string ACS_txtRecPersonaRecibe62 { get { return _ACS_txtRecPersonaRecibe62; } set { _ACS_txtRecPersonaRecibe62 = value; } }
        private string _ACS_txtRecPuesto62;
        public string ACS_txtRecPuesto62 { get { return _ACS_txtRecPuesto62; } set { _ACS_txtRecPuesto62 = value; } }
        private int _ACS_Chk62Mismodia;
        public int ACS_Chk62Mismodia { get { return _ACS_Chk62Mismodia; } set { _ACS_Chk62Mismodia = value; } }
        private int _ACS_Chk62Sincita;
        public int ACS_Chk62Sincita { get { return _ACS_Chk62Sincita; } set { _ACS_Chk62Sincita = value; } }
        private int _ACS_Chk62Previa;
        public int ACS_Chk62Previa { get { return _ACS_Chk62Previa; } set { _ACS_Chk62Previa = value; } }
        private string _ACS_txt62CitaContacto;
        public string ACS_txt62CitaContacto { get { return _ACS_txt62CitaContacto; } set { _ACS_txt62CitaContacto = value; } }
        private string _ACS_txt62CitaTelefono;
        public string ACS_txt62CitaTelefono { get { return _ACS_txt62CitaTelefono; } set { _ACS_txt62CitaTelefono = value; } }
        private int _ACS_txt62CitaDiasdeAnticipacion;
        public int ACS_txt62CitaDiasdeAnticipacion { get { return _ACS_txt62CitaDiasdeAnticipacion; } set { _ACS_txt62CitaDiasdeAnticipacion = value; } }
        private int _ACS_chk62AreaPropia;
        public int ACS_chk62AreaPropia { get { return _ACS_chk62AreaPropia; } set { _ACS_chk62AreaPropia = value; } }
        private int _ACS_chk62AreaPlaza;
        public int ACS_chk62AreaPlaza { get { return _ACS_chk62AreaPlaza; } set { _ACS_chk62AreaPlaza = value; } }
        private int _ACS_chk62AreaCalle;
        public int ACS_chk62AreaCalle { get { return _ACS_chk62AreaCalle; } set { _ACS_chk62AreaCalle = value; } }
        private int _ACS_chk62AreaAvTransitada;
        public int ACS_chk62AreaAvTransitada { get { return _ACS_chk62AreaAvTransitada; } set { _ACS_chk62AreaAvTransitada = value; } }
        private int _ACS_chk62EstCortesia;
        public int ACS_chk62EstCortesia { get { return _ACS_chk62EstCortesia; } set { _ACS_chk62EstCortesia = value; } }
        private int _ACS_chk62EstCosto;
        public int ACS_chk62EstCosto { get { return _ACS_chk62EstCosto; } set { _ACS_chk62EstCosto = value; } }
        private int _ACS_txt62EstMonto;
        public int ACS_txt62EstMonto { get { return _ACS_txt62EstMonto; } set { _ACS_txt62EstMonto = value; } }
        private string _ACS_txt62ClienteDireccion;
        public string ACS_txt62ClienteDireccion { get { return _ACS_txt62ClienteDireccion; } set { _ACS_txt62ClienteDireccion = value; } }
        private string _ACS_txt62ClienteColonia;
        public string ACS_txt62ClienteColonia { get { return _ACS_txt62ClienteColonia; } set { _ACS_txt62ClienteColonia = value; } }
        private string _ACS_txt62ClienteMunicipio;
        public string ACS_txt62ClienteMunicipio { get { return _ACS_txt62ClienteMunicipio; } set { _ACS_txt62ClienteMunicipio = value; } }
        private string _ACS_txt62ClienteEstado;
        public string ACS_txt62ClienteEstado { get { return _ACS_txt62ClienteEstado; } set { _ACS_txt62ClienteEstado = value; } }
        private string _ACS_txt62ClienteCodPost;
        public string ACS_txt62ClienteCodPost { get { return _ACS_txt62ClienteCodPost; } set { _ACS_txt62ClienteCodPost = value; } }
        private int _ACS_chk62DocFactFranquiciaEnt;
        public int ACS_chk62DocFactFranquiciaEnt { get { return _ACS_chk62DocFactFranquiciaEnt; } set { _ACS_chk62DocFactFranquiciaEnt = value; } }
        private int _ACS_txt62DocFactFranquiciaEntCop;
        public int ACS_txt62DocFactFranquiciaEntCop { get { return _ACS_txt62DocFactFranquiciaEntCop; } set { _ACS_txt62DocFactFranquiciaEntCop = value; } }
        private int _ACS_chk62DocFactFranquiciaRec;
        public int ACS_chk62DocFactFranquiciaRec { get { return _ACS_chk62DocFactFranquiciaRec; } set { _ACS_chk62DocFactFranquiciaRec = value; } }
        private int _ACS_txt62DocFactFranquiciaRecCop;
        public int ACS_txt62DocFactFranquiciaRecCop { get { return _ACS_txt62DocFactFranquiciaRecCop; } set { _ACS_txt62DocFactFranquiciaRecCop = value; } }
        private int _ACS_chk62DocFactKeyEnt;
        public int ACS_chk62DocFactKeyEnt { get { return _ACS_chk62DocFactKeyEnt; } set { _ACS_chk62DocFactKeyEnt = value; } }
        private int _ACS_txt62DocFactKeyEntCop;
        public int ACS_txt62DocFactKeyEntCop { get { return _ACS_txt62DocFactKeyEntCop; } set { _ACS_txt62DocFactKeyEntCop = value; } }
        private int _ACS_chk62DocFactKeyRec;
        public int ACS_chk62DocFactKeyRec { get { return _ACS_chk62DocFactKeyRec; } set { _ACS_chk62DocFactKeyRec = value; } }
        private int _ACS_txt62DocFactKeyRecCop;
        public int ACS_txt62DocFactKeyRecCop { get { return _ACS_txt62DocFactKeyRecCop; } set { _ACS_txt62DocFactKeyRecCop = value; } }
        private int _ACS_chk62DocOrdCompraEnt;
        public int ACS_chk62DocOrdCompraEnt { get { return _ACS_chk62DocOrdCompraEnt; } set { _ACS_chk62DocOrdCompraEnt = value; } }
        private int _ACS_txt62DocOrdCompraEntCop;
        public int ACS_txt62DocOrdCompraEntCop { get { return _ACS_txt62DocOrdCompraEntCop; } set { _ACS_txt62DocOrdCompraEntCop = value; } }
        private int _ACS_chk62DocOrdCompraRec;
        public int ACS_chk62DocOrdCompraRec { get { return _ACS_chk62DocOrdCompraRec; } set { _ACS_chk62DocOrdCompraRec = value; } }
        private int _ACS_txt62DocOrdCompraRecCop;
        public int ACS_txt62DocOrdCompraRecCop { get { return _ACS_txt62DocOrdCompraRecCop; } set { _ACS_txt62DocOrdCompraRecCop = value; } }
        private int _ACS_chk62DocOrdReposEnt;
        public int ACS_chk62DocOrdReposEnt { get { return _ACS_chk62DocOrdReposEnt; } set { _ACS_chk62DocOrdReposEnt = value; } }
        private int _ACS_txt62DocOrdReposEntCop;
        public int ACS_txt62DocOrdReposEntCop { get { return _ACS_txt62DocOrdReposEntCop; } set { _ACS_txt62DocOrdReposEntCop = value; } }
        private int _ACS_chk62DocOrdReposRec;
        public int ACS_chk62DocOrdReposRec { get { return _ACS_chk62DocOrdReposRec; } set { _ACS_chk62DocOrdReposRec = value; } }
        private int _ACS_txt62DocOrdReposRecCop;
        public int ACS_txt62DocOrdReposRecCop { get { return _ACS_txt62DocOrdReposRecCop; } set { _ACS_txt62DocOrdReposRecCop = value; } }
        private int _ACS_chk62DocCopPedidoEnt;
        public int ACS_chk62DocCopPedidoEnt { get { return _ACS_chk62DocCopPedidoEnt; } set { _ACS_chk62DocCopPedidoEnt = value; } }
        private int _ACS_txt62DocCopPedidoEntCop;
        public int ACS_txt62DocCopPedidoEntCop { get { return _ACS_txt62DocCopPedidoEntCop; } set { _ACS_txt62DocCopPedidoEntCop = value; } }
        private int _ACS_chk62DocCopPedidoRec;
        public int ACS_chk62DocCopPedidoRec { get { return _ACS_chk62DocCopPedidoRec; } set { _ACS_chk62DocCopPedidoRec = value; } }
        private int _ACS_txt62DocCopPedidoRecCop;
        public int ACS_txt62DocCopPedidoRecCop { get { return _ACS_txt62DocCopPedidoRecCop; } set { _ACS_txt62DocCopPedidoRecCop = value; } }
        private int _ACS_chk62DocRemisionEnt;
        public int ACS_chk62DocRemisionEnt { get { return _ACS_chk62DocRemisionEnt; } set { _ACS_chk62DocRemisionEnt = value; } }
        private int _ACS_txt62DocRemisionEntCop;
        public int ACS_txt62DocRemisionEntCop { get { return _ACS_txt62DocRemisionEntCop; } set { _ACS_txt62DocRemisionEntCop = value; } }
        private int _ACS_chk62DocRemisionRec;
        public int ACS_chk62DocRemisionRec { get { return _ACS_chk62DocRemisionRec; } set { _ACS_chk62DocRemisionRec = value; } }
        private int _ACS_txt62DocRemisionRecCop;
        public int ACS_txt62DocRemisionRecCop { get { return _ACS_txt62DocRemisionRecCop; } set { _ACS_txt62DocRemisionRecCop = value; } }
        private int _ACS_chk62DocFolioEnt;
        public int ACS_chk62DocFolioEnt { get { return _ACS_chk62DocFolioEnt; } set { _ACS_chk62DocFolioEnt = value; } }
        private int _ACS_txt62DocFolioEntCop;
        public int ACS_txt62DocFolioEntCop { get { return _ACS_txt62DocFolioEntCop; } set { _ACS_txt62DocFolioEntCop = value; } }
        private int _ACS_chk62DocFolioRec;
        public int ACS_chk62DocFolioRec { get { return _ACS_chk62DocFolioRec; } set { _ACS_chk62DocFolioRec = value; } }
        private int _ACS_txt62DocFolioRecCop;
        public int ACS_txt62DocFolioRecCop { get { return _ACS_txt62DocFolioRecCop; } set { _ACS_txt62DocFolioRecCop = value; } }
        private int _ACS_chk62DocContraRecEnt;
        public int ACS_chk62DocContraRecEnt { get { return _ACS_chk62DocContraRecEnt; } set { _ACS_chk62DocContraRecEnt = value; } }
        private int _ACS_txt62DocContraRecEntCop;
        public int ACS_txt62DocContraRecEntCop { get { return _ACS_txt62DocContraRecEntCop; } set { _ACS_txt62DocContraRecEntCop = value; } }
        private int _ACS_chk62DocContraRecRec;
        public int ACS_chk62DocContraRecRec { get { return _ACS_chk62DocContraRecRec; } set { _ACS_chk62DocContraRecRec = value; } }
        private int _ACS_txt62DocContraRecRecCop;
        public int ACS_txt62DocContraRecRecCop { get { return _ACS_txt62DocContraRecRecCop; } set { _ACS_txt62DocContraRecRecCop = value; } }
        private int _ACS_chk62DocEntAlmacenEnt;
        public int ACS_chk62DocEntAlmacenEnt { get { return _ACS_chk62DocEntAlmacenEnt; } set { _ACS_chk62DocEntAlmacenEnt = value; } }
        private int _ACS_txt62DocEntAlmacenEntCop;
        public int ACS_txt62DocEntAlmacenEntCop { get { return _ACS_txt62DocEntAlmacenEntCop; } set { _ACS_txt62DocEntAlmacenEntCop = value; } }
        private int _ACS_chk62DocEntAlmacenRec;
        public int ACS_chk62DocEntAlmacenRec { get { return _ACS_chk62DocEntAlmacenRec; } set { _ACS_chk62DocEntAlmacenRec = value; } }
        private int _ACS_txt62DocEntAlmacenRecCop;
        public int ACS_txt62DocEntAlmacenRecCop { get { return _ACS_txt62DocEntAlmacenRecCop; } set { _ACS_txt62DocEntAlmacenRecCop = value; } }
        private int _ACS_chk62DocSopServicioEnt;
        public int ACS_chk62DocSopServicioEnt { get { return _ACS_chk62DocSopServicioEnt; } set { _ACS_chk62DocSopServicioEnt = value; } }
        private int _ACS_txt62DocSopServicioEntCop;
        public int ACS_txt62DocSopServicioEntCop { get { return _ACS_txt62DocSopServicioEntCop; } set { _ACS_txt62DocSopServicioEntCop = value; } }
        private int _ACS_chk62DocSopServicioRec;
        public int ACS_chk62DocSopServicioRec { get { return _ACS_chk62DocSopServicioRec; } set { _ACS_chk62DocSopServicioRec = value; } }
        private int _ACS_txt62DocSopServicioRecCop;
        public int ACS_txt62DocSopServicioRecCop { get { return _ACS_txt62DocSopServicioRecCop; } set { _ACS_txt62DocSopServicioRecCop = value; } }
        private int _ACS_chk62DocNomFirmaEnt;
        public int ACS_chk62DocNomFirmaEnt { get { return _ACS_chk62DocNomFirmaEnt; } set { _ACS_chk62DocNomFirmaEnt = value; } }
        private int _ACS_txt62DocNomFirmaEntCop;
        public int ACS_txt62DocNomFirmaEntCop { get { return _ACS_txt62DocNomFirmaEntCop; } set { _ACS_txt62DocNomFirmaEntCop = value; } }
        private int _ACS_chk62DocNomFirmaoRec;
        public int ACS_chk62DocNomFirmaoRec { get { return _ACS_chk62DocNomFirmaoRec; } set { _ACS_chk62DocNomFirmaoRec = value; } }
        private int _ACS_txt62DocNomFirmaRecCop;
        public int ACS_txt62DocNomFirmaRecCop { get { return _ACS_txt62DocNomFirmaRecCop; } set { _ACS_txt62DocNomFirmaRecCop = value; } }
        private int _ACS_chk62CitaEnt;
        public int ACS_chk62CitaEnt { get { return _ACS_chk62CitaEnt; } set { _ACS_chk62CitaEnt = value; } }
        private int _ACS_txt62CitaEntCop;
        public int ACS_txt62CitaEntCop { get { return _ACS_txt62CitaEntCop; } set { _ACS_txt62CitaEntCop = value; } }
        private int _ACS_chk62CitaRec;
        public int ACS_chk62CitaRec { get { return _ACS_chk62CitaRec; } set { _ACS_chk62CitaRec = value; } }
        private int _ACS_txt62CitaRecCop;
        public int ACS_txt62CitaRecCop { get { return _ACS_txt62CitaRecCop; } set { _ACS_txt62CitaRecCop = value; } }
        private int _ACS_chk63Lunes;
        public int ACS_chk63Lunes { get { return _ACS_chk63Lunes; } set { _ACS_chk63Lunes = value; } }
        private int _ACS_chk63Martes;
        public int ACS_chk63Martes { get { return _ACS_chk63Martes; } set { _ACS_chk63Martes = value; } }
        private int _ACS_chk63Miercoles;
        public int ACS_chk63Miercoles { get { return _ACS_chk63Miercoles; } set { _ACS_chk63Miercoles = value; } }
        private int _ACS_chk63Jueves;
        public int ACS_chk63Jueves { get { return _ACS_chk63Jueves; } set { _ACS_chk63Jueves = value; } }
        private int _ACS_chk63Viernes;
        public int ACS_chk63Viernes { get { return _ACS_chk63Viernes; } set { _ACS_chk63Viernes = value; } }
        private int _ACS_chk63Sabado;
        public int ACS_chk63Sabado { get { return _ACS_chk63Sabado; } set { _ACS_chk63Sabado = value; } }
        private string _ACS_Rad63TimePicker163;
        public string ACS_Rad63TimePicker163 { get { return _ACS_Rad63TimePicker163; } set { _ACS_Rad63TimePicker163 = value; } }
        private string _ACS_Rad63TimePicker263;
        public string ACS_Rad63TimePicker263 { get { return _ACS_Rad63TimePicker263; } set { _ACS_Rad63TimePicker263 = value; } }
        private string _ACS_Rad63TimePicker363;
        public string ACS_Rad63TimePicker363 { get { return _ACS_Rad63TimePicker363; } set { _ACS_Rad63TimePicker363 = value; } }
        private string _ACS_Rad63TimePicker463;
        public string ACS_Rad63TimePicker463 { get { return _ACS_Rad63TimePicker463; } set { _ACS_Rad63TimePicker463 = value; } }
        private string _ACS_txtRecPersonaRecibe63;
        public string ACS_txtRecPersonaRecibe63 { get { return _ACS_txtRecPersonaRecibe63; } set { _ACS_txtRecPersonaRecibe63 = value; } }
        private string _ACS_txtRecPuesto63;
        public string ACS_txtRecPuesto63 { get { return _ACS_txtRecPuesto63; } set { _ACS_txtRecPuesto63 = value; } }
        private int _ACS_Chk63Mismodia;
        public int ACS_Chk63Mismodia { get { return _ACS_Chk63Mismodia; } set { _ACS_Chk63Mismodia = value; } }
        private int _ACS_Chk63Sincita;
        public int ACS_Chk63Sincita { get { return _ACS_Chk63Sincita; } set { _ACS_Chk63Sincita = value; } }
        private int _ACS_Chk63Previa;
        public int ACS_Chk63Previa { get { return _ACS_Chk63Previa; } set { _ACS_Chk63Previa = value; } }
        private string _ACS_txt63CitaContacto;
        public string ACS_txt63CitaContacto { get { return _ACS_txt63CitaContacto; } set { _ACS_txt63CitaContacto = value; } }
        private string _ACS_txt63CitaTelefono;
        public string ACS_txt63CitaTelefono { get { return _ACS_txt63CitaTelefono; } set { _ACS_txt63CitaTelefono = value; } }
        private int _ACS_txt63CitaDiasdeAnticipacion;
        public int ACS_txt63CitaDiasdeAnticipacion { get { return _ACS_txt63CitaDiasdeAnticipacion; } set { _ACS_txt63CitaDiasdeAnticipacion = value; } }
        private int _ACS_chk63AreaPropia;
        public int ACS_chk63AreaPropia { get { return _ACS_chk63AreaPropia; } set { _ACS_chk63AreaPropia = value; } }
        private int _ACS_chk63AreaPlaza;
        public int ACS_chk63AreaPlaza { get { return _ACS_chk63AreaPlaza; } set { _ACS_chk63AreaPlaza = value; } }
        private int _ACS_chk63AreaCalle;
        public int ACS_chk63AreaCalle { get { return _ACS_chk63AreaCalle; } set { _ACS_chk63AreaCalle = value; } }
        private int _ACS_chk63AreaAvTransitada;
        public int ACS_chk63AreaAvTransitada { get { return _ACS_chk63AreaAvTransitada; } set { _ACS_chk63AreaAvTransitada = value; } }
        private int _ACS_chk63EstCortesia;
        public int ACS_chk63EstCortesia { get { return _ACS_chk63EstCortesia; } set { _ACS_chk63EstCortesia = value; } }
        private int _ACS_chk63EstCosto;
        public int ACS_chk63EstCosto { get { return _ACS_chk63EstCosto; } set { _ACS_chk63EstCosto = value; } }
        private int _ACS_txt63EstMonto;
        public int ACS_txt63EstMonto { get { return _ACS_txt63EstMonto; } set { _ACS_txt63EstMonto = value; } }
        private string _ACS_txt63ClienteDireccion;
        public string ACS_txt63ClienteDireccion { get { return _ACS_txt63ClienteDireccion; } set { _ACS_txt63ClienteDireccion = value; } }
        private string _ACS_txt63ClienteColonia;
        public string ACS_txt63ClienteColonia { get { return _ACS_txt63ClienteColonia; } set { _ACS_txt63ClienteColonia = value; } }
        private string _ACS_txt63ClienteMunicipio;
        public string ACS_txt63ClienteMunicipio { get { return _ACS_txt63ClienteMunicipio; } set { _ACS_txt63ClienteMunicipio = value; } }
        private string _ACS_txt63ClienteEstado;
        public string ACS_txt63ClienteEstado { get { return _ACS_txt63ClienteEstado; } set { _ACS_txt63ClienteEstado = value; } }
        private string _ACS_txt63ClienteCodPost;
        public string ACS_txt63ClienteCodPost { get { return _ACS_txt63ClienteCodPost; } set { _ACS_txt63ClienteCodPost = value; } }
        private int _ACS_chk63DocFactFranquiciaEnt;
        public int ACS_chk63DocFactFranquiciaEnt { get { return _ACS_chk63DocFactFranquiciaEnt; } set { _ACS_chk63DocFactFranquiciaEnt = value; } }
        private int _ACS_txt63DocFactFranquiciaEntCop;
        public int ACS_txt63DocFactFranquiciaEntCop { get { return _ACS_txt63DocFactFranquiciaEntCop; } set { _ACS_txt63DocFactFranquiciaEntCop = value; } }
        private int _ACS_chk63DocFactFranquiciaRec;
        public int ACS_chk63DocFactFranquiciaRec { get { return _ACS_chk63DocFactFranquiciaRec; } set { _ACS_chk63DocFactFranquiciaRec = value; } }
        private int _ACS_txt63DocFactFranquiciaRecCop;
        public int ACS_txt63DocFactFranquiciaRecCop { get { return _ACS_txt63DocFactFranquiciaRecCop; } set { _ACS_txt63DocFactFranquiciaRecCop = value; } }
        private int _ACS_chk63DocFactKeyEnt;
        public int ACS_chk63DocFactKeyEnt { get { return _ACS_chk63DocFactKeyEnt; } set { _ACS_chk63DocFactKeyEnt = value; } }
        private int _ACS_txt63DocFactKeyEntCop;
        public int ACS_txt63DocFactKeyEntCop { get { return _ACS_txt63DocFactKeyEntCop; } set { _ACS_txt63DocFactKeyEntCop = value; } }
        private int _ACS_chk63DocFactKeyRec;
        public int ACS_chk63DocFactKeyRec { get { return _ACS_chk63DocFactKeyRec; } set { _ACS_chk63DocFactKeyRec = value; } }
        private int _ACS_txt63DocFactKeyRecCop;
        public int ACS_txt63DocFactKeyRecCop { get { return _ACS_txt63DocFactKeyRecCop; } set { _ACS_txt63DocFactKeyRecCop = value; } }
        private int _ACS_chk63DocOrdCompraEnt;
        public int ACS_chk63DocOrdCompraEnt { get { return _ACS_chk63DocOrdCompraEnt; } set { _ACS_chk63DocOrdCompraEnt = value; } }
        private int _ACS_txt63DocOrdCompraEntCop;
        public int ACS_txt63DocOrdCompraEntCop { get { return _ACS_txt63DocOrdCompraEntCop; } set { _ACS_txt63DocOrdCompraEntCop = value; } }
        private int _ACS_chk63DocOrdCompraRec;
        public int ACS_chk63DocOrdCompraRec { get { return _ACS_chk63DocOrdCompraRec; } set { _ACS_chk63DocOrdCompraRec = value; } }
        private int _ACS_txt63DocOrdCompraRecCop;
        public int ACS_txt63DocOrdCompraRecCop { get { return _ACS_txt63DocOrdCompraRecCop; } set { _ACS_txt63DocOrdCompraRecCop = value; } }
        private int _ACS_chk63DocOrdReposEnt;
        public int ACS_chk63DocOrdReposEnt { get { return _ACS_chk63DocOrdReposEnt; } set { _ACS_chk63DocOrdReposEnt = value; } }
        private int _ACS_txt63DocOrdReposEntCop;
        public int ACS_txt63DocOrdReposEntCop { get { return _ACS_txt63DocOrdReposEntCop; } set { _ACS_txt63DocOrdReposEntCop = value; } }
        private int _ACS_chk63DocOrdReposRec;
        public int ACS_chk63DocOrdReposRec { get { return _ACS_chk63DocOrdReposRec; } set { _ACS_chk63DocOrdReposRec = value; } }
        private int _ACS_txt63DocOrdReposRecCop;
        public int ACS_txt63DocOrdReposRecCop { get { return _ACS_txt63DocOrdReposRecCop; } set { _ACS_txt63DocOrdReposRecCop = value; } }
        private int _ACS_chk63DocCopPedidoEnt;
        public int ACS_chk63DocCopPedidoEnt { get { return _ACS_chk63DocCopPedidoEnt; } set { _ACS_chk63DocCopPedidoEnt = value; } }
        private int _ACS_txt63DocCopPedidoEntCop;
        public int ACS_txt63DocCopPedidoEntCop { get { return _ACS_txt63DocCopPedidoEntCop; } set { _ACS_txt63DocCopPedidoEntCop = value; } }
        private int _ACS_chk63DocCopPedidoRec;
        public int ACS_chk63DocCopPedidoRec { get { return _ACS_chk63DocCopPedidoRec; } set { _ACS_chk63DocCopPedidoRec = value; } }
        private int _ACS_txt63DocCopPedidoRecCop;
        public int ACS_txt63DocCopPedidoRecCop { get { return _ACS_txt63DocCopPedidoRecCop; } set { _ACS_txt63DocCopPedidoRecCop = value; } }
        private int _ACS_chk63DocRemisionEnt;
        public int ACS_chk63DocRemisionEnt { get { return _ACS_chk63DocRemisionEnt; } set { _ACS_chk63DocRemisionEnt = value; } }
        private int _ACS_txt63DocRemisionEntCop;
        public int ACS_txt63DocRemisionEntCop { get { return _ACS_txt63DocRemisionEntCop; } set { _ACS_txt63DocRemisionEntCop = value; } }
        private int _ACS_chk63DocRemisionRec;
        public int ACS_chk63DocRemisionRec { get { return _ACS_chk63DocRemisionRec; } set { _ACS_chk63DocRemisionRec = value; } }
        private int _ACS_txt63DocRemisionRecCop;
        public int ACS_txt63DocRemisionRecCop { get { return _ACS_txt63DocRemisionRecCop; } set { _ACS_txt63DocRemisionRecCop = value; } }
        private int _ACS_chk63DocFolioEnt;
        public int ACS_chk63DocFolioEnt { get { return _ACS_chk63DocFolioEnt; } set { _ACS_chk63DocFolioEnt = value; } }
        private int _ACS_txt63DocFolioEntCop;
        public int ACS_txt63DocFolioEntCop { get { return _ACS_txt63DocFolioEntCop; } set { _ACS_txt63DocFolioEntCop = value; } }
        private int _ACS_chk63DocFolioRec;
        public int ACS_chk63DocFolioRec { get { return _ACS_chk63DocFolioRec; } set { _ACS_chk63DocFolioRec = value; } }
        private int _ACS_txt63DocFolioRecCop;
        public int ACS_txt63DocFolioRecCop { get { return _ACS_txt63DocFolioRecCop; } set { _ACS_txt63DocFolioRecCop = value; } }
        private int _ACS_chk63DocContraRecEnt;
        public int ACS_chk63DocContraRecEnt { get { return _ACS_chk63DocContraRecEnt; } set { _ACS_chk63DocContraRecEnt = value; } }
        private int _ACS_txt63DocContraRecEntCop;
        public int ACS_txt63DocContraRecEntCop { get { return _ACS_txt63DocContraRecEntCop; } set { _ACS_txt63DocContraRecEntCop = value; } }
        private int _ACS_chk63DocContraRecRec;
        public int ACS_chk63DocContraRecRec { get { return _ACS_chk63DocContraRecRec; } set { _ACS_chk63DocContraRecRec = value; } }
        private int _ACS_txt63DocContraRecRecCop;
        public int ACS_txt63DocContraRecRecCop { get { return _ACS_txt63DocContraRecRecCop; } set { _ACS_txt63DocContraRecRecCop = value; } }
        private int _ACS_chk63DocEntAlmacenEnt;
        public int ACS_chk63DocEntAlmacenEnt { get { return _ACS_chk63DocEntAlmacenEnt; } set { _ACS_chk63DocEntAlmacenEnt = value; } }
        private int _ACS_txt63DocEntAlmacenEntCop;
        public int ACS_txt63DocEntAlmacenEntCop { get { return _ACS_txt63DocEntAlmacenEntCop; } set { _ACS_txt63DocEntAlmacenEntCop = value; } }
        private int _ACS_chk63DocEntAlmacenRec;
        public int ACS_chk63DocEntAlmacenRec { get { return _ACS_chk63DocEntAlmacenRec; } set { _ACS_chk63DocEntAlmacenRec = value; } }
        private int _ACS_txt63DocEntAlmacenRecCop;
        public int ACS_txt63DocEntAlmacenRecCop { get { return _ACS_txt63DocEntAlmacenRecCop; } set { _ACS_txt63DocEntAlmacenRecCop = value; } }
        private int _ACS_chk63DocSopServicioEnt;
        public int ACS_chk63DocSopServicioEnt { get { return _ACS_chk63DocSopServicioEnt; } set { _ACS_chk63DocSopServicioEnt = value; } }
        private int _ACS_txt63DocSopServicioEntCop;
        public int ACS_txt63DocSopServicioEntCop { get { return _ACS_txt63DocSopServicioEntCop; } set { _ACS_txt63DocSopServicioEntCop = value; } }
        private int _ACS_chk63DocSopServicioRec;
        public int ACS_chk63DocSopServicioRec { get { return _ACS_chk63DocSopServicioRec; } set { _ACS_chk63DocSopServicioRec = value; } }
        private int _ACS_txt63DocSopServicioRecCop;
        public int ACS_txt63DocSopServicioRecCop { get { return _ACS_txt63DocSopServicioRecCop; } set { _ACS_txt63DocSopServicioRecCop = value; } }
        private int _ACS_chk63DocNomFirmaEnt;
        public int ACS_chk63DocNomFirmaEnt { get { return _ACS_chk63DocNomFirmaEnt; } set { _ACS_chk63DocNomFirmaEnt = value; } }
        private int _ACS_txt63DocNomFirmaEntCop;
        public int ACS_txt63DocNomFirmaEntCop { get { return _ACS_txt63DocNomFirmaEntCop; } set { _ACS_txt63DocNomFirmaEntCop = value; } }
        private int _ACS_chk63DocNomFirmaoRec;
        public int ACS_chk63DocNomFirmaoRec { get { return _ACS_chk63DocNomFirmaoRec; } set { _ACS_chk63DocNomFirmaoRec = value; } }
        private int _ACS_txt63DocNomFirmaRecCop;
        public int ACS_txt63DocNomFirmaRecCop { get { return _ACS_txt63DocNomFirmaRecCop; } set { _ACS_txt63DocNomFirmaRecCop = value; } }
        private int _ACS_chk63CitaEnt;
        public int ACS_chk63CitaEnt { get { return _ACS_chk63CitaEnt; } set { _ACS_chk63CitaEnt = value; } }
        private int _ACS_txt63CitaEntCop;
        public int ACS_txt63CitaEntCop { get { return _ACS_txt63CitaEntCop; } set { _ACS_txt63CitaEntCop = value; } }
        private int _ACS_chk63CitaRec;
        public int ACS_chk63CitaRec { get { return _ACS_chk63CitaRec; } set { _ACS_chk63CitaRec = value; } }
        private int _ACS_txt63CitaRecCop;
        public int ACS_txt63CitaRecCop { get { return _ACS_txt63CitaRecCop; } set { _ACS_txt63CitaRecCop = value; } }
        // Filtros
        private string _Filtro_Usuario;                      
        public string Filtro_Usuario
        {
            get { return _Filtro_Usuario; }
            set { _Filtro_Usuario = value; }
        }
        private string _Filtro_FecIni;
        public string Filtro_FecIni { 
            get { return _Filtro_FecIni; } 
            set { _Filtro_FecIni = value; } 
        }
        
        private string _Filtro_FecFin;
        public string Filtro_FecFin
        {
            get { return _Filtro_FecFin; }
            set { _Filtro_FecFin = value; }
        }
        private int _Filtro_FolIni;
        public int Filtro_FolIni
        {
            get { return _Filtro_FolIni; }
            set { _Filtro_FolIni =  value; }
        }
        private int _Filtro_FolFin;
        public int Filtro_FolFin
        {
            get { return _Filtro_FolFin; }
            set { _Filtro_FolFin=  value; }
        }
        private string _Filtro_Estatus;
        public string Filtro_Estatus
        {
            get { return _Filtro_Estatus; }
            set { _Filtro_Estatus = value; }
        }
        private int _Id_Modalidad;
        public int Id_Modalidad
        {
            get { return _Id_Modalidad; }
            set { _Id_Modalidad = value; }
        }
        private double _Vis_Frecuencia;
        public double Vis_Frecuencia { get { return _Vis_Frecuencia; } set { _Vis_Frecuencia = value; } }
        private bool _Vis_Lunes;
        public bool Vis_Lunes { get { return _Vis_Lunes; } set { _Vis_Lunes = value; } }
        private bool _Vis_Martes;
        public bool Vis_Martes { get { return _Vis_Martes; } set { _Vis_Martes = value; } }
        private bool _Vis_Miercoles;
        public bool Vis_Miercoles { get { return _Vis_Miercoles; } set { _Vis_Miercoles = value; } }
        private bool _Vis_Jueves;
        public bool Vis_Jueves { get { return _Vis_Jueves; } set { _Vis_Jueves = value; } }
        private bool _Vis_Viernes;
        public bool Vis_Viernes { get { return _Vis_Viernes; } set { _Vis_Viernes = value; } }
        private bool _Vis_Sabado;
        public bool Vis_Sabado { get { return _Vis_Sabado; } set { _Vis_Sabado = value; } }
        private string _Vis_HrAm1;
        public string Vis_HrAm1 { get { return _Vis_HrAm1; } set { _Vis_HrAm1 = value; } }
        private string _Vis_HrAm2;
        public string Vis_HrAm2 { get { return _Vis_HrAm2; } set { _Vis_HrAm2 = value; } }
        private string _Vis_HrPm1;
        public string Vis_HrPm1 { get { return _Vis_HrPm1; } set { _Vis_HrPm1 = value; } }
        private string _Vis_HrPm2;
        public string Vis_HrPm2 { get { return _Vis_HrPm2; } set { _Vis_HrPm2 = value; } }
        private string _Rec_Semanas;
        public string Rec_Semanas { get { return _Rec_Semanas; } set { _Rec_Semanas = value; } }
        private bool _Rec_Lunes;
        public bool Rec_Lunes { get { return _Rec_Lunes; } set { _Rec_Lunes = value; } }
        private bool _Rec_Martes;
        public bool Rec_Martes { get { return _Rec_Martes; } set { _Rec_Martes = value; } }
        private bool _Rec_Miercoles;
        public bool Rec_Miercoles { get { return _Rec_Miercoles; } set { _Rec_Miercoles = value; } }
        private bool _Rec_Jueves;
        public bool Rec_Jueves { get { return _Rec_Jueves; } set { _Rec_Jueves = value; } }
        private bool _Rec_Viernes;
        public bool Rec_Viernes { get { return _Rec_Viernes; } set { _Rec_Viernes = value; } }
        private bool _Rec_Sabado;
        public bool Rec_Sabado { get { return _Rec_Sabado; } set { _Rec_Sabado = value; } }
        private bool _Rec_Confirmacion;
        public bool Rec_Confirmacion { get { return _Rec_Confirmacion; } set { _Rec_Confirmacion = value; } }
        private bool _Rec_Correo;
        public bool Rec_Correo { get { return _Rec_Correo; } set { _Rec_Correo = value; } }
        private bool _Rec_Fax;
        public bool Rec_Fax { get { return _Rec_Fax; } set { _Rec_Fax = value; } }
        private bool _Rec_Telefono;
        public bool Rec_Telefono { get { return _Rec_Telefono; } set { _Rec_Telefono = value; } }
        private bool _Rec_Representante;
        public bool Rec_Representante { get { return _Rec_Representante; } set { _Rec_Representante = value; } }
        
        private bool _Rec_Otro;
        public bool Rec_Otro { get { return _Rec_Otro; } set { _Rec_Otro = value; } }
        private bool _Rec_Whats;
        public bool Rec_Whats { get { return _Rec_Whats; } set { _Rec_Whats = value; } }
        private string _Rec_OtroStr;
        public string Rec_OtroStr { get { return _Rec_OtroStr; } set { _Rec_OtroStr = value; } }
        private string _Acs_PedidoEncargadoEnviar;
        public string Acs_PedidoEncargadoEnviar { get { return _Acs_PedidoEncargadoEnviar; } set { _Acs_PedidoEncargadoEnviar = value; } }
        private string _Acs_PedidoPuesto;
        public string Acs_PedidoPuesto { get { return _Acs_PedidoPuesto; } set { _Acs_PedidoPuesto = value; } }
        
        private string _Acs_PedidoEmail;
        public string Acs_PedidoEmail { get { return _Acs_PedidoEmail; } set { _Acs_PedidoEmail = value; } }
        private bool _Acs_RecDocReposicion;
        public bool Acs_RecDocReposicion { get { return _Acs_RecDocReposicion; } set { _Acs_RecDocReposicion = value; } }
        private bool _Acs_RecDocFolio;
        public bool Acs_RecDocFolio { get { return _Acs_RecDocFolio; } set { _Acs_RecDocFolio = value; } }
        private string _Acs_RecDocOtro;
        public string Acs_RecDocOtro { get { return _Acs_RecDocOtro; } set { _Acs_RecDocOtro = value; } }
        private string _Acs_VisitaOtro;
        public string Acs_VisitaOtro { get { return _Acs_VisitaOtro; } set { _Acs_VisitaOtro = value; } }
        private bool _Acs_ReqServAsesoria;
        public bool Acs_ReqServAsesoria { get { return _Acs_ReqServAsesoria; } set { _Acs_ReqServAsesoria = value; } }
        private bool _Acs_ReqServTecnicoRelleno;
        public bool Acs_ReqServTecnicoRelleno { get { return _Acs_ReqServTecnicoRelleno; } set { _Acs_ReqServTecnicoRelleno = value; } }
        private bool _Acs_ReqServMantenimiento;
        public bool Acs_ReqServMantenimiento { get { return _Acs_ReqServMantenimiento; } set { _Acs_ReqServMantenimiento = value; } }
        private string _Acs_Notas;
        public string Acs_Notas { get { return _Acs_Notas; } set { _Acs_Notas = value; } }
        private int _Acs_ContactoRepVenta;
        public int Acs_ContactoRepVenta { get { return _Acs_ContactoRepVenta; } set { _Acs_ContactoRepVenta = value; } }
        private string _Acs_ContactoRepVentaTel;
        public string Acs_ContactoRepVentaTel { get { return _Acs_ContactoRepVentaTel; } set { _Acs_ContactoRepVentaTel = value; } }
        private string _Acs_ContactoRepVentaEmail;
        public string Acs_ContactoRepVentaEmail { get { return _Acs_ContactoRepVentaEmail; } set { _Acs_ContactoRepVentaEmail = value; } }
        private int _Acs_ContactoRepServ;
        public int Acs_ContactoRepServ { get { return _Acs_ContactoRepServ; } set { _Acs_ContactoRepServ = value; } }
        private string _Acs_ContactoRepServTel;
        public string Acs_ContactoRepServTel { get { return _Acs_ContactoRepServTel; } set { _Acs_ContactoRepServTel = value; } }
        private string _Acs_ContactoRepServEmail;
        public string Acs_ContactoRepServEmail { get { return _Acs_ContactoRepServEmail; } set { _Acs_ContactoRepServEmail = value; } }
        private int _Acs_ContactoJefServ;
        public int Acs_ContactoJefServ { get { return _Acs_ContactoJefServ; } set { _Acs_ContactoJefServ = value; } }
        private string _Acs_ContactoJefServTel;
        public string Acs_ContactoJefServTel { get { return _Acs_ContactoJefServTel; } set { _Acs_ContactoJefServTel = value; } }
        private string _Acs_ContactoJefServEmail;
        public string Acs_ContactoJefServEmail { get { return _Acs_ContactoJefServEmail; } set { _Acs_ContactoJefServEmail = value; } }
        private int _Acs_ContactoAseServ;
        public int Acs_ContactoAseServ { get { return _Acs_ContactoAseServ; } set { _Acs_ContactoAseServ = value; } }
        private string _Acs_ContactoAseServTel;
        public string Acs_ContactoAseServTel { get { return _Acs_ContactoAseServTel; } set { _Acs_ContactoAseServTel = value; } }
        private string _Acs_ContactoAseServEmail;
        public string Acs_ContactoAseServEmail { get { return _Acs_ContactoAseServEmail; } set { _Acs_ContactoAseServEmail = value; } }
        private int _Acs_ContactoJefOper;
        public int Acs_ContactoJefOper { get { return _Acs_ContactoJefOper; } set { _Acs_ContactoJefOper = value; } }
        private string _Acs_ContactoJefOperTel;
        public string Acs_ContactoJefOperTel { get { return _Acs_ContactoJefOperTel; } set { _Acs_ContactoJefOperTel = value; } }
        private string _Acs_ContactoJefOperEmail;
        public string Acs_ContactoJefOperEmail { get { return _Acs_ContactoJefOperEmail; } set { _Acs_ContactoJefOperEmail = value; } }
        private int _Acs_ContactoCAlmRep;
        public int Acs_ContactoCAlmRep { get { return _Acs_ContactoCAlmRep; } set { _Acs_ContactoCAlmRep = value; } }
        private string _Acs_ContactoCAlmRepTel;
        public string Acs_ContactoCAlmRepTel { get { return _Acs_ContactoCAlmRepTel; } set { _Acs_ContactoCAlmRepTel = value; } }
        private string _Acs_ContactoCAlmRepEmail;
        public string Acs_ContactoCAlmRepEmail { get { return _Acs_ContactoCAlmRepEmail; } set { _Acs_ContactoCAlmRepEmail = value; } }
        private int _Acs_ContactoCServTec;
        public int Acs_ContactoCServTec { get { return _Acs_ContactoCServTec; } set { _Acs_ContactoCServTec = value; } }
        private string _Acs_ContactoCServTecTel;
        public string Acs_ContactoCServTecTel { get { return _Acs_ContactoCServTecTel; } set { _Acs_ContactoCServTecTel = value; } }
        private string _Acs_ContactoCServTecEmail;
        public string Acs_ContactoCServTecEmail { get { return _Acs_ContactoCServTecEmail; } set { _Acs_ContactoCServTecEmail = value; } }
        */

        //

        private int _ACS_Aud_Tipo;
        public int ACS_Aud_Tipo { get { return _ACS_Aud_Tipo; } set { _ACS_Aud_Tipo = value; } }

        private int _ACS_SC_Tipo;
        public int ACS_SC_Tipo { get { return _ACS_SC_Tipo; } set { _ACS_SC_Tipo = value; } }

        // ESTOS CAMPOS SE AGREGAN POR COMPATIBILIDAD CON LA VERSION 1

        // Se refiere al tipo de Revicio

        private int _ACS_SA_Tipo;
        public int ACS_SA_Tipo { get { return _ACS_SA_Tipo; } set { _ACS_SA_Tipo = value; } }

        private bool _ACS_SC_Aplicar;
        public bool ACS_SC_Aplicar { get { return _ACS_SC_Aplicar; } set { _ACS_SC_Aplicar = value; } }

        // L M M J V S D
        private bool _ACS_SC_Lunes;
        public bool ACS_SC_Lunes { get { return _ACS_SC_Lunes; } set { _ACS_SC_Lunes = value; } }
        private bool _ACS_SC_Martes;
        public bool ACS_SC_Martes { get { return _ACS_SC_Martes; } set { _ACS_SC_Martes = value; } }
        private bool _ACS_SC_Miercoles;
        public bool ACS_SC_Miercoles { get { return _ACS_SC_Miercoles; } set { _ACS_SC_Miercoles = value; } }
        private bool _ACS_SC_Jueves;
        public bool ACS_SC_Jueves { get { return _ACS_SC_Jueves; } set { _ACS_SC_Jueves = value; } }
        private bool _ACS_SC_Viernes;
        public bool ACS_SC_Viernes { get { return _ACS_SC_Viernes; } set { _ACS_SC_Viernes = value; } }
        private bool _ACS_SC_Sabado;
        public bool ACS_SC_Sabado { get { return _ACS_SC_Sabado; } set { _ACS_SC_Sabado = value; } }
        private bool _ACS_SC_Domingo;
        public bool ACS_SC_Domingo { get { return _ACS_SC_Domingo; } set { _ACS_SC_Domingo = value; } }

        private bool _ACS_SC_CualquierDia;
        public bool ACS_SC_CualquierDia { get { return _ACS_SC_CualquierDia; } set { _ACS_SC_CualquierDia = value; } }

        private string _ACS_SC_Horario1;
        private string _ACS_SC_Horario2;
        private string _ACS_SC_Horario3;
        private string _ACS_SC_Horario4;

        public string ACS_SC_Horario1 { get { return _ACS_SC_Horario1; } set { _ACS_SC_Horario1 = value; } }
        public string ACS_SC_Horario2 { get { return _ACS_SC_Horario2; } set { _ACS_SC_Horario2 = value; } }
        public string ACS_SC_Horario3 { get { return _ACS_SC_Horario3; } set { _ACS_SC_Horario3 = value; } }
        public string ACS_SC_Horario4 { get { return _ACS_SC_Horario4; } set { _ACS_SC_Horario4 = value; } }

        private bool _ACS_SC_CitaPrev_MismoDia;
        public bool ACS_SC_CitaPrev_MismoDia { get { return _ACS_SC_CitaPrev_MismoDia; } set { _ACS_SC_CitaPrev_MismoDia = value; } }

        private bool _ACS_SC_CitaPrev_Pevia;
        public bool ACS_SC_CitaPrev_Pevia { get { return _ACS_SC_CitaPrev_Pevia; } set { _ACS_SC_CitaPrev_Pevia = value; } }

        private int _ACS_SC_CitaPrev_Tipo;
        public int ACS_SC_CitaPrev_Tipo { get { return _ACS_SC_CitaPrev_Tipo; } set { _ACS_SC_CitaPrev_Tipo = value; } }

        //OCT23-2019 
        private bool _ServRelleno;
        public bool ServRelleno { get { return _ServRelleno; } set { _ServRelleno = value; } }

        private bool _ServPreventivo;
        public bool ServPreventivo { get { return _ServPreventivo; } set { _ServPreventivo = value; } }

        private int _SelectorTipoServ;
        public int SelectorTipoServ { get { return _SelectorTipoServ; } set { _SelectorTipoServ = value; } }


        private string _PersonaRecibe;
        public string PersonaRecibe { get { return _PersonaRecibe; } set { _PersonaRecibe = value; } }

        private string _Puesto;
        public string Puesto { get { return _Puesto; } set { _Puesto = value; } }


        private bool _ACS_Aud_Aplicar;
        public bool ACS_Aud_Aplicar { get { return _ACS_Aud_Aplicar; } set { _ACS_Aud_Aplicar = value; } }

        private int _ACS_Aud_CitaPrev_Tipo;
        public int ACS_Aud_CitaPrev_Tipo { get { return _ACS_Aud_CitaPrev_Tipo; } set { _ACS_Aud_CitaPrev_Tipo = value; } }

        //
        // SERVICIO AUDITORIA
        //

        // L M M J V S D
        private bool _ACS_Aud_Lunes;
        public bool ACS_Aud_Lunes { get { return _ACS_Aud_Lunes; } set { _ACS_Aud_Lunes = value; } }
        private bool _ACS_Aud_Martes;
        public bool ACS_Aud_Martes { get { return _ACS_Aud_Martes; } set { _ACS_Aud_Martes = value; } }
        private bool _ACS_Aud_Miercoles;
        public bool ACS_Aud_Miercoles { get { return _ACS_Aud_Miercoles; } set { _ACS_Aud_Miercoles = value; } }
        private bool _ACS_Aud_Jueves;
        public bool ACS_Aud_Jueves { get { return _ACS_Aud_Jueves; } set { _ACS_Aud_Jueves = value; } }
        private bool _ACS_Aud_Viernes;
        public bool ACS_Aud_Viernes { get { return _ACS_Aud_Viernes; } set { _ACS_Aud_Viernes = value; } }
        private bool _ACS_Aud_Sabado;
        public bool ACS_Aud_Sabado { get { return _ACS_Aud_Sabado; } set { _ACS_Aud_Sabado = value; } }

        private bool _ACS_Aud_CualquierDia;
        public bool ACS_Aud_CualquierDia { get { return _ACS_Aud_CualquierDia; } set { _ACS_Aud_CualquierDia = value; } }

        private string _ACS_Aud_Horario1;
        private string _ACS_Aud_Horario2;
        private string _ACS_Aud_Horario3;
        private string _ACS_Aud_Horario4;

        public string ACS_Aud_Horario1 { get { return _ACS_Aud_Horario1; } set { _ACS_Aud_Horario1 = value; } }
        public string ACS_Aud_Horario2 { get { return _ACS_Aud_Horario2; } set { _ACS_Aud_Horario2 = value; } }
        public string ACS_Aud_Horario3 { get { return _ACS_Aud_Horario3; } set { _ACS_Aud_Horario3 = value; } }
        public string ACS_Aud_Horario4 { get { return _ACS_Aud_Horario4; } set { _ACS_Aud_Horario4 = value; } }

        private bool _ACS_Aud_CitaPrev_MismoDia;
        public bool ACS_Aud_CitaPrev_MismoDia { get { return _ACS_Aud_CitaPrev_MismoDia; } set { _ACS_Aud_CitaPrev_MismoDia = value; } }

        private int _ACS_Aud_CitaPrev_Pevia;
        public int ACS_Aud_CitaPrev_Pevia { get { return _ACS_Aud_CitaPrev_Pevia; } set { _ACS_Aud_CitaPrev_Pevia = value; } }

        //Condiciones de Pago 
        //Documentos 

        /*
        private int _Acs_CondPagFac;
        public int Acs_CondPagFac { get { return _Acs_CondPagFac; } set { _Acs_CondPagFac= value; } }
        private int _Acs_CondPagFacCop;
        public int Acs_CondPagFacCop { get { return _Acs_CondPagFacCop; } set { _Acs_CondPagFacCop= value; } }
        private int _Acs_CondPagOrdCom;
        public int Acs_CondPagOrdCom { get { return _Acs_CondPagOrdCom; } set { _Acs_CondPagOrdCom= value; } }
        private int _Acs_CondPagOrdComCop;
        public int Acs_CondPagOrdComCop { get { return _Acs_CondPagOrdComCop; } set { _Acs_CondPagOrdComCop= value; } }
        private int _Acs_CondPagOrdRep;
        public int Acs_CondPagOrdRep { get { return _Acs_CondPagOrdRep; } set { Acs_CondPagOrdRep= value; } }
        private int _Acs_CondPagOrdRepCop;
        public int Acs_CondPagOrdRepCop { get { return _Acs_CondPagOrdRepCop; } set { Acs_CondPagOrdRepCop= value; } }
        private int _Acs_CondPagRem;
        public int Acs_CondPagRem { get { return _Acs_CondPagRem; } set { Acs_CondPagRem= value; } }
        private int _Acs_CondPagRemCop;
        public int Acs_CondPagRemCop { get { return _Acs_CondPagRemCop; } set { Acs_CondPagRemCop = value; } }
         * */

        private int _Acs_CondPagEntFac;
        public int Acs_CondPagEntFac { get { return _Acs_CondPagEntFac; } set { _Acs_CondPagEntFac = value; } }

        private int _Acs_CondPagEntFacCop;
        public int Acs_CondPagEntFacCop { get { return _Acs_CondPagEntFacCop; } set { _Acs_CondPagEntFacCop = value; } }

        private int _Acs_CondPagReFac;
        public int Acs_CondPagReFac { get { return _Acs_CondPagReFac; } set { _Acs_CondPagReFac = value; } }

        private int _Acs_CondPagReFacCop;
        public int Acs_CondPagReFacCop { get { return _Acs_CondPagReFacCop; } set { _Acs_CondPagReFacCop = value; } }

        /*
        Parametro("@Acs_CondPagEntFac", A.Acs_CondPagEntFac);
        Parametro("@Acs_CondPagEntFacCop", A.Acs_CondPagEntFacCop);
        Parametro("@Acs_CondPagReFac", A.Acs_CondPagReFac);
        Parametro("@Acs_CondPagReFacCop", A.Acs_CondPagReFacCop);
         */

        private int _Acs_CondPagEntOrdCom;
        public int Acs_CondPagEntOrdCom { get { return _Acs_CondPagEntOrdCom; } set { _Acs_CondPagEntOrdCom = value; } }

        private int _Acs_CondPagEntOrdComCop;
        public int Acs_CondPagEntOrdComCop { get { return _Acs_CondPagEntOrdComCop; } set { _Acs_CondPagEntOrdComCop = value; } }

        private int _Acs_CondPagReOrdCom;
        public int Acs_CondPagReOrdCom { get { return _Acs_CondPagReOrdCom; } set { _Acs_CondPagReOrdCom = value; } }

        private int _Acs_CondPagReOrdComCop;
        public int Acs_CondPagReOrdComCop { get { return _Acs_CondPagReOrdComCop; } set { _Acs_CondPagReOrdComCop = value; } }

        /*
                Parametro("@Acs_CondPagEntOrdCom", A.Acs_CondPagEntOrdCom);
                Parametro("@Acs_CondPagEntOrdComCop", A.Acs_CondPagEntOrdComCop);
                Parametro("@Acs_CondPagReOrdCom", A.Acs_CondPagReOrdCom);
                Parametro("@Acs_CondPagReOrdComCop", A.Acs_CondPagReOrdComCop);
        */

        private int _Acs_CondPagEntOrdRep;
        public int Acs_CondPagEntOrdRep { get { return _Acs_CondPagEntOrdRep; } set { _Acs_CondPagEntOrdRep = value; } }

        private int _Acs_CondPagEntOrdRepCop;
        public int Acs_CondPagEntOrdRepCop { get { return _Acs_CondPagEntOrdRepCop; } set { _Acs_CondPagEntOrdRepCop = value; } }

        private int _Acs_CondPagReOrdRep;
        public int Acs_CondPagReOrdRep { get { return _Acs_CondPagReOrdRep; } set { _Acs_CondPagReOrdRep = value; } }

        private int _Acs_CondPagReOrdRepCop;
        public int Acs_CondPagReOrdRepCop { get { return _Acs_CondPagReOrdRepCop; } set { _Acs_CondPagReOrdRepCop = value; } }
        /*
                Parametro("@Acs_CondPagEntOrdRep", A.Acs_CondPagEntOrdRep);
                Parametro("@Acs_CondPagEntOrdRepCop", A.Acs_CondPagEntOrdRepCop);
                Parametro("@Acs_CondPagReOrdRep", A.Acs_CondPagReOrdRep);
                Parametro("@Acs_CondPagReOrdRepCop", A.Acs_CondPagReOrdRepCop);
         * */

        private int _Acs_CondPagEntCopPed;
        public int Acs_CondPagEntCopPed { get { return _Acs_CondPagEntCopPed; } set { _Acs_CondPagEntCopPed = value; } }

        private int _Acs_CondPagEntCopPedCop;
        public int Acs_CondPagEntCopPedCop { get { return _Acs_CondPagEntCopPedCop; } set { _Acs_CondPagEntCopPedCop = value; } }

        private int _Acs_CondPagReCopPed;
        public int Acs_CondPagReCopPed { get { return _Acs_CondPagReCopPed; } set { _Acs_CondPagReCopPed = value; } }

        private int _Acs_CondPagReCopPedCop;
        public int Acs_CondPagReCopPedCop { get { return _Acs_CondPagReCopPedCop; } set { _Acs_CondPagReCopPedCop = value; } }

        /*
                Parametro("@Acs_CondPagEntCopPed", A.Acs_CondPagEntCopPed);
                Parametro("@Acs_CondPagEntCopPedCop", A.Acs_CondPagEntCopPedCop);
                Parametro("@Acs_CondPagReCopPed", A.Acs_CondPagReCopPed);
                Parametro("@Acs_CondPagReCopPedCop", A.Acs_CondPagReCopPedCop);
        */

        private int _Acs_CondPagEntPagRem;
        public int Acs_CondPagEntPagRem { get { return _Acs_CondPagEntPagRem; } set { _Acs_CondPagEntPagRem = value; } }

        private int _Acs_CondPagEntPagRemCop;
        public int Acs_CondPagEntPagRemCop { get { return _Acs_CondPagEntPagRemCop; } set { _Acs_CondPagEntPagRemCop = value; } }

        private int _Acs_CondPagRePagRem;
        public int Acs_CondPagRePagRem { get { return _Acs_CondPagRePagRem; } set { _Acs_CondPagRePagRem = value; } }

        private int _Acs_CondPagRePagRemCop;
        public int Acs_CondPagRePagRemCop { get { return _Acs_CondPagRePagRemCop; } set { _Acs_CondPagRePagRemCop = value; } }

        private int _Documento_PermiteEditara;
        public int Documento_PermiteEditara { get { return _Documento_PermiteEditara; } set { _Documento_PermiteEditara = value; } }

        /*
                Parametro("@Acs_CondPagEntPagRem", A.Acs_CondPagEntPagRem);
                Parametro("@Acs_CondPagEntPagRemCop", A.Acs_CondPagEntPagRemCop);
                Parametro("@Acs_CondPagRePagRem", A.Acs_CondPagRePagRem);
                Parametro("@Acs_CondPagRePagRemCop", A.Acs_CondPagRePagRemCop);
        */


        // Eléctronica

        private int _RevFacEmail;
        private string _RevFacEmailTexto;
        private string _RevFacEmailTexto2;
        private int _RevFacPortal;
        private string _RevFacPortalTexto;
        private string _RevFacHttp;
        private string _RevFacUsuario;
        private string _RevFacContrasenia;

        public int RevFacEmail { get { return _RevFacEmail; } set { _RevFacEmail = value; } }
        public string RevFacEmailTexto { get { return _RevFacEmailTexto; } set { _RevFacEmailTexto = value; } }
        public string RevFacEmailTexto2 { get { return _RevFacEmailTexto2; } set { _RevFacEmailTexto2 = value; } }
        public int RevFacPortal { get { return _RevFacPortal; } set { _RevFacPortal = value; } }
        public string RevFacPortalTexto { get { return _RevFacPortalTexto; } set { _RevFacPortalTexto = value; } }
        public string RevFacHttp { get { return _RevFacHttp; } set { _RevFacHttp = value; } }
        public string RevFacUsuario { get { return _RevFacUsuario; } set { _RevFacUsuario = value; } }
        public string RevFacContrasenia { get { return _RevFacContrasenia; } set { _RevFacContrasenia = value; } }


        // Autorizaciones de Usuarios 
        // 3Jul2019 RFH
        private int _Acs_ReqAutGerente;
        public int Acs_ReqAutGerente
        {
            get { return _Acs_ReqAutGerente; }
            set { _Acs_ReqAutGerente = value; }
        }

        private int _Acs_ReqAutJefeOp;
        public int Acs_ReqAutJefeOp
        {
            get { return _Acs_ReqAutJefeOp; }
            set { _Acs_ReqAutJefeOp = value; }
        }

        private int _Desplegar_BtnAutorizar;
        public int Desplegar_BtnAutorizar
        {
            get { return _Desplegar_BtnAutorizar; }
            set { _Desplegar_BtnAutorizar = value; }
        }

        private int _UsuarioConsulta_Id_Tu;
        public int UsuarioConsulta_Id_Tu
        {
            get { return _UsuarioConsulta_Id_Tu; }
            set { _UsuarioConsulta_Id_Tu = value; }
        }

        private int _UsuarioConsulta_Id_Rik;
        public int UsuarioConsulta_Id_Rik
        {
            get { return _UsuarioConsulta_Id_Rik; }
            set { _UsuarioConsulta_Id_Rik = value; }
        }

        private string _Acs_ComentariosRecomendaciones;
        public string Acs_ComentariosRecomendaciones
        {
            get { return _Acs_ComentariosRecomendaciones; }
            set { _Acs_ComentariosRecomendaciones = value; }
        }

        // 03MAY-2021
        private int _Acs_Procedencia;
        public int Acs_Procedencia
        {
            get { return _Acs_Procedencia; }
            set { _Acs_Procedencia = value; }
        }

        //
        // Acs_RevisionFolio Acs_RevisionEntAlmacen  Acs_RevisionOrdenCompra Acs_RevisionRepConsumo  Acs_RevisionCopiaFactura
        //Acs_RevisionOtroDoc
        // Acs_PagoContraEntrega Acs_VisitaGestorCobranza
        private bool _Acs_RevisionFolio;
        private bool _Acs_RevisionEntAlmacen;
        private bool _Acs_RevisionOrdenCompra;
        private bool _Acs_RevisionRepConsumo;
        private bool _Acs_RevisionCopiaFactura;

        private string _Acs_RevisionOtroDoc;

        private bool _Acs_PagoContraEntrega;
        private bool _Acs_VisitaGestorCobranza;

        public bool Acs_RevisionFolio
        {
            get { return _Acs_RevisionFolio; }
            set { _Acs_RevisionFolio = value; }
        }
        public bool Acs_RevisionEntAlmacen
        {
            get { return _Acs_RevisionEntAlmacen; }
            set { _Acs_RevisionEntAlmacen = value; }
        }
        public bool Acs_RevisionOrdenCompra
        {
            get { return _Acs_RevisionOrdenCompra; }
            set { _Acs_RevisionOrdenCompra = value; }
        }
        public bool Acs_RevisionRepConsumo
        {
            get { return _Acs_RevisionRepConsumo; }
            set { _Acs_RevisionRepConsumo = value; }
        }

        public bool Acs_RevisionCopiaFactura
        {
            get { return _Acs_RevisionCopiaFactura; }
            set { _Acs_RevisionCopiaFactura = value; }
        }

        public string Acs_RevisionOtroDoc
        {
            get { return _Acs_RevisionOtroDoc; }
            set { _Acs_RevisionOtroDoc = value; }
        }
        public bool Acs_PagoContraEntrega
        {
            get { return _Acs_PagoContraEntrega; }
            set { _Acs_PagoContraEntrega = value; }
        }

        public bool Acs_VisitaGestorCobranza
        {
            get { return _Acs_VisitaGestorCobranza; }
            set { _Acs_VisitaGestorCobranza = value; }
        }



    }
}