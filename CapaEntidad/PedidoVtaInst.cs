using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class PedidoVtaInst
    {
        public PedidoVtaInst()
        {
            Id_TG = 0;
            ModalidadGarantia = "";
        }

        public int ID_sol { get; set; }


        public string IsTieneOC { get; set; }
        public string Acs_Contacto2 { get; set; }
        public string Acs_Contacto4 { get; set; }
        public string Acs_Contacto5 { get; set; }
        public string Acs_Telefono2 { get; set; }
        public string Acs_Telefono4 { get; set; }
        public string Acs_Telefono5 { get; set; }
        public string Acs_Email2 { get; set; }
        public string Acs_Email4 { get; set; }
        public string Acs_Email5 { get; set; }
        public string EstatusSOl { get; set; }
        public string EstatusStr { get; set; }

        public bool DocOrdenCompra { get; set; }
        public bool DocOrdReposEnt { get; set; }
        public bool DocCopPedidoEnt { get; set; }

        public int Acs_Mes { get; set; }

        public string Acs_NomComercial { get; set; }
        public string Identificador { get; set; }
        public string filtro_pedido { get; set; }
        public bool Seleccionado { get; set; }
        private int _Id_Emp;
        private int _Id_Cd;
        private int _Id_Cte;
        private string _Cte_Nom;
        private int _Id_Ter;
        private DateTime _Acs_Fecha;
        private int _Acs_Semana;
        private int _Acs_Cantidad;
        private string _Estatus;
        private int _Id_TG;
        private string _ModalidadGarantia;

        public string Acs_Vigencia { get; set; }
        public string Filtro_Vigencia { get; set; }
        public string Acs_EstatusStr { get; set; }
        public string Acs_VigenciaStr { get; set; }

        public int fechaConcat { get; set; }
        public int Id_AcsVersion { get; set; }
        public string Acs_PedidoEncargadoEnviar { get; set; }
        public string Acs_PedidoPuesto { get; set; }
        public string Acs_PedidoTelefono { get; set; }
        public string Acs_PedidoEmail { get; set; }
        public bool Acs_ReqDocReposicion { get; set; }
        public bool Acs_ReqDocFolio { get; set; }


        public bool Acs_ReqFacturaKey { get; set; }
        public bool ACS_ReqRemision { get; set; }
        public bool Acs_ReqCopia { get; set; }

        public int Acs_ReqOrdencop { get; set; }
        public int Acs_ReqDocReposicioncop { get; set; }
        public int Acs_ReqDocFoliocop { get; set; }
        public int Acs_ReqFacturaKeyCop { get; set; }
        public int ACS_ReqRemisionCop { get; set; }
        public int Acs_ReqCopiaCop { get; set; }

        public string Acs_ReqDocOtro { get; set; }
        public string Acs_Modalidad { get; set; }
        public string Acs_Contacto3 { get; set; }
        public string Acs_Telefono3 { get; set; }
        public string Acs_Email3 { get; set; }

        public long Id_Prd { get; set; }
        public string Prd_Descripcion { get; set; }
        public bool Acs_Lunes { get; set; }
        public bool Acs_Martes { get; set; }
        public bool Acs_Miercoles { get; set; }
        public bool Acs_Jueves { get; set; }
        public bool Acs_Viernes { get; set; }
        public bool Acs_Sabado { get; set; }
        public string Acs_Documento { get; set; }
        public string pedido { get; set; }

        public string PedAcys { get; set; }
        public string ReqAcys { get; set; }
        public string OcAcys { get; set; }
        public DateTime FechaFacAcys { get; set; }
        public string Filtro_SemIni { get; set; }
        public string Filtro_SemFin { get; set; }
        public string Filtro_AnioIni { get; set; }
        public string Filtro_AnioFin { get; set; }
        public string Filtro_tipoPed { get; set; }


        public string Filtro_Frecuencia { get; set; }

        private string _Filtro_CteIni;
        private string _Filtro_CteFin;
        private double? _Filtro_TerIni;
        private double? _Filtro_TerFin;
        private double? _Filtro_Sem;
        private double? _Filtro_Anio;
        private int? _Id_U;
        private bool _Cte_Credito;
        private string _Cte_CreditoLetra;
        private int _Id_Acs;
        private string _Ter_Nombre;
        private int _Id_Rik;
        private string _Rik_Nombre;
        private string _Acs_Contacto;
        private string _Acs_Puesto;
        private string _Acs_Telefono;
        private string _Acs_email;
        private bool _Acs_ReqOrden;

        public string Filtro_Cte { get; set; }
        public string Filtro_mes { get; set; }
        public string Filtro_Anios { get; set; }
        public string Filtro_AnioFinal { get; set; }
        public string Filtro_mesFinal { get; set; }

        private DateTime _Ped_Fecha;

        private string _Pedido_del;
        private string _Requisicion;
        private string _Ped_Solicito;
        private string _Ped_Flete;
        private string _Ped_OrdenEntrega;
        private int _Ped_CondEntrega;
        private DateTime _Ped_FechaEntrega;
        private string _Ped_Observaciones;
        private int _Ped_DescPorcen2;
        private string _Ped_Desc1;
        private string _Ped_Desc2;
        private double _Ped_Importe;
        private double _Ped_Subtotal;
        private double _Ped_Iva;
        private double _Ped_Total;
        private int _Ped_DescPorcen;
        private int _Ped_Tipo;
        private int _Id_Ped;
        private int _Ped_DescPorcen1;
        private string _Ped_Comentarios;
        private int _Acs_Anio;
        public string Ped_SolicitoTel;
        public string Ped_SolicitoEmail;
        public string Ped_SolicitoPuesto;
        public string Ped_ConsignadoCalle;
        public string Ped_ConsignadoNo;
        public string Ped_ConsignadoCp;
        public string Ped_ConsignadoMunicipio;
        public string Ped_ConsignadoEstado;
        public string Ped_ConsignadoColonia;
        public bool Ped_ReqOrden;
        public string Ped_OrdenCompra;
        public int Ped_AcysSemana;
        public int Ped_AcysAnio;
        public string Filtro_Nombre;
        public string Filtro_Tipo;
        public DateTime? Filtro_FecIni;
        public DateTime? Filtro_FecFin;
        public DateTime? Filtro_FecFIni;
        public DateTime? Filtro_FecFFin;

        private string _Filtro_usuario;
        public string UsoCFDI { get; set; }

        public string Filtro_usuario
        {
            get { return _Filtro_usuario; }
            set { _Filtro_usuario = value; }
        }



        private string _Filtro_Estatus;

        public string Filtro_Estatus
        {
            get { return _Filtro_Estatus; }
            set { _Filtro_Estatus = value; }
        }

        private string _Filtro_Credito;

        public string Filtro_Credito
        {
            get { return _Filtro_Credito; }
            set { _Filtro_Credito = value; }
        }

        private string _Ped_AsignStr;

        public string Ped_AsignStr
        {
            get { return _Ped_AsignStr; }
            set { _Ped_AsignStr = value; }
        }
        private string _Ped_Asign;
        private string _Rut_Descripcion;
        public string Filtro_Documento;
        private string _U_Nombre;

        public string U_Nombre
        {
            get { return _U_Nombre; }
            set { _U_Nombre = value; }
        }

        public string Rut_Descripcion
        {
            get { return _Rut_Descripcion; }
            set { _Rut_Descripcion = value; }
        }

        public string Ped_Asign
        {
            get { return _Ped_Asign; }
            set { _Ped_Asign = value; }
        }


        public int Acs_Anio
        {
            get { return _Acs_Anio; }
            set { _Acs_Anio = value; }
        }


        public string Ped_Comentarios
        {
            get { return _Ped_Comentarios; }
            set { _Ped_Comentarios = value; }
        }

        public int Ped_DescPorcen1
        {
            get { return _Ped_DescPorcen1; }
            set { _Ped_DescPorcen1 = value; }
        }

        public int Id_Ped
        {
            get { return _Id_Ped; }
            set { _Id_Ped = value; }
        }

        public int Ped_Tipo
        {
            get { return _Ped_Tipo; }
            set { _Ped_Tipo = value; }
        }

        public DateTime Ped_Fecha
        {
            get { return _Ped_Fecha; }
            set { _Ped_Fecha = value; }
        }

        public string Pedido_del
        {
            get { return _Pedido_del; }
            set { _Pedido_del = value; }
        }

        public string Requisicion
        {
            get { return _Requisicion; }
            set { _Requisicion = value; }
        }

        public string Ped_Solicito
        {
            get { return _Ped_Solicito; }
            set { _Ped_Solicito = value; }
        }

        public string Ped_Flete
        {
            get { return _Ped_Flete; }
            set { _Ped_Flete = value; }
        }

        public string Ped_OrdenEntrega
        {
            get { return _Ped_OrdenEntrega; }
            set { _Ped_OrdenEntrega = value; }
        }

        public int Ped_CondEntrega
        {
            get { return _Ped_CondEntrega; }
            set { _Ped_CondEntrega = value; }
        }

        public DateTime Ped_FechaEntrega
        {
            get { return _Ped_FechaEntrega; }
            set { _Ped_FechaEntrega = value; }
        }

        public string Ped_Observaciones
        {
            get { return _Ped_Observaciones; }
            set { _Ped_Observaciones = value; }
        }

        public int Ped_DescPorcen2
        {
            get { return _Ped_DescPorcen2; }
            set { _Ped_DescPorcen2 = value; }
        }

        public string Ped_Desc1
        {
            get { return _Ped_Desc1; }
            set { _Ped_Desc1 = value; }
        }

        public string Ped_Desc2
        {
            get { return _Ped_Desc2; }
            set { _Ped_Desc2 = value; }
        }

        public double Ped_Importe
        {
            get { return _Ped_Importe; }
            set { _Ped_Importe = value; }
        }

        public double Ped_Subtotal
        {
            get { return _Ped_Subtotal; }
            set { _Ped_Subtotal = value; }
        }

        public double Ped_Iva
        {
            get { return _Ped_Iva; }
            set { _Ped_Iva = value; }
        }


        public int Ped_DescPorcen11
        {
            get { return _Ped_DescPorcen; }
            set { _Ped_DescPorcen = value; }
        }

        public double Ped_Total
        {
            get { return _Ped_Total; }
            set { _Ped_Total = value; }
        }

        public bool Acs_ReqOrden
        {
            get { return _Acs_ReqOrden; }
            set { _Acs_ReqOrden = value; }
        }

        public string Ter_Nombre
        {
            get { return _Ter_Nombre; }
            set { _Ter_Nombre = value; }
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
        public string Acs_Contacto
        {
            get { return _Acs_Contacto; }
            set { _Acs_Contacto = value; }
        }
        public string Acs_Puesto
        {
            get { return _Acs_Puesto; }
            set { _Acs_Puesto = value; }
        }
        public string Acs_Telefono
        {
            get { return _Acs_Telefono; }
            set { _Acs_Telefono = value; }
        }
        public string Acs_email
        {
            get { return _Acs_email; }
            set { _Acs_email = value; }
        }

        public int Id_Acs
        {
            get { return _Id_Acs; }
            set { _Id_Acs = value; }
        }
        public bool Cte_Credito
        {
            get { return _Cte_Credito; }
            set { _Cte_Credito = value; }
        }
        public string Cte_CreditoLetra
        {
            get { return _Cte_CreditoLetra; }
            set { _Cte_CreditoLetra = value; }
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
        public int Id_Cte
        {
            get { return _Id_Cte; }
            set { _Id_Cte = value; }
        }
        public string Cte_Nom
        {
            get { return _Cte_Nom; }
            set { _Cte_Nom = value; }
        }
        public int Id_Ter
        {
            get { return _Id_Ter; }
            set { _Id_Ter = value; }
        }
        public DateTime Acs_Fecha
        {
            get { return _Acs_Fecha; }
            set { _Acs_Fecha = value; }
        }
        public int Acs_Semana
        {
            get { return _Acs_Semana; }
            set { _Acs_Semana = value; }
        }
        public string Estatus
        {
            get { return _Estatus; }
            set { _Estatus = value; }
        }
        public int Acs_Cantidad
        {
            get { return _Acs_Cantidad; }
            set { _Acs_Cantidad = value; }
        }

        public string Filtro_CteIni
        {
            get { return _Filtro_CteIni; }
            set { _Filtro_CteIni = value; }
        }
        public string Filtro_CteFin
        {
            get { return _Filtro_CteFin; }
            set { _Filtro_CteFin = value; }
        }
        public double? Filtro_TerIni
        {
            get { return _Filtro_TerIni; }
            set { _Filtro_TerIni = value; }
        }
        public double? Filtro_TerFin
        {
            get { return _Filtro_TerFin; }
            set { _Filtro_TerFin = value; }
        }
        public double? Filtro_Sem
        {
            get { return _Filtro_Sem; }
            set { _Filtro_Sem = value; }
        }
        public double? Filtro_Anio
        {
            get { return _Filtro_Anio; }
            set { _Filtro_Anio = value; }
        }

        public int? Id_U
        {
            get { return _Id_U; }
            set { _Id_U = value; }
        }

        public int Id_TG
        {
            get
            {
                return _Id_TG;
            }
            set
            {
                _Id_TG = value;
            }
        }
        // Edsg 08042015
        public string Ped_TipoStr { get; set; }
        public string ModalidadGarantia
        {
            get
            {
                return _ModalidadGarantia;
            }
            set
            {
                _ModalidadGarantia = value;
            }
        }

        public string TG_Nombre { get; set; }

        public string OrdenCompra { get; set; }
        public string nombreDocumento { get; set; }
        public string extension { get; set; }
        public string archivo { get; set; }

        public int id_cteDirEntrega { get; set; }
        public string Cte_Calle { get; set; }
        public string cte_numero { get; set; }
        public string CTE_Colonia { get; set; }
        public string CTe_Estado { get; set; }

        public string Direccion { get; set; }
        public int PedExterno { get; set; }
        public string str_Tipo_pedido { get; set; }

        public string IdOC { get; set; }

        public DateTime FechaInicial { get; set; }
        public DateTime FechaFinal { get; set; }

        public string EstatusSolicitud { get; set; }
        public int id_Sol { get; set; }

        public string estatusSTR { get; set; }
        public int cantidad { get; set; }
        public double TotalFacturacion { get; set; }

        public string ValidaPicking { get; set; }

        public int TipoCancelacion { get; set; }

        public double Precio { get; set; }
        public string ClaveProdServ { get; set; }
        public string ClaveUnidad { get; set; }
        public string Prd_UniNe { get; set; }
        public string Prd_UniNs { get; set; }
        public long prodOriginal { get; set; }
        public string Prd_Presentacion { get; set; }
    }
}