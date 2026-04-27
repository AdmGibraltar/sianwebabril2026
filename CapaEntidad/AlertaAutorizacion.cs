using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace CapaEntidad
{
    public class AlertaAutorizacion
    {
        int _IdAutorizacionPrecio;
        int _Id_AutorizacionPrecioDet;
        string _Tipo; //es nulo o es GPMA
        int _Id_Emp;
        int _Id_Cd;
        int _IdRepresentante;
        string _Cte_NomComercial;
        string _Prd_Descripcion;
        string _Nom_Representante;
        string _NomEstatus;
        string _NomTipoSolicitud;
        string _Nom_CDI;
        bool _Activo;
        int _Estatus;
        int _IdUSolicita;
        int _TipoAutorizacion;
        long _Id_Prd;
        int _Id_Cte;
        int _Id_Ter;
        Double _Precio_Vta;
        Double _Precio_VtaAutorizado;

        Double _Precio_MinimoRik;
        Double _Precio_MinimoGte;

        Double _Cantidad;
        Double _Precio_AAA;
        Double _Porc_Utilidad;
        Double _Utilidad;
        Double _Importe_Utilidad;
        Double _Importe;

        int _Req_Aut_Director;
        int _IdUsuarioGteAutorizo;
        int _IdUsuarioDirAutorizo;
        int _IdUsuarioGteRechazo;
        int _IdUsuarioDirRechazo;
        string _MotivoRechazo;
        string _MotivoCancelacion;
        DateTime? _FechaSolicitud;
        DateTime? _Fecha_UltMod;
        DateTime? _FechaAutorizacionGte;
        DateTime? _FechaAutorizacionDir;
        DateTime? _FechaRechazoGte;
        DateTime? _FechaRechazoDir;
        DateTime? _FechaInicial;
        DateTime? _FechaFinal;
        string _Justificacion;
        string _JustificacionMemo;
        Double _PrecioLista;
        int _Id_Motivo;
        string _JustificacionGte;
        string _Nom_Motivo;
        DateTime? _FechaVigencia;
        int _Id_Cpr;
        int _IdAutorizacionAnterior;
        Double _PrecioObjetivo;
        string _Id_Tamaño;
        string _Tipo_Cliente;
        int _Id_MotivoRechazo;
        //GPMa
        int _Id_ReporteGP;
        string _JustificacionGeneral;
        int _Id_Seg;
        Double _UnidadesProyectadas;
        Double _VentaProy;
        public string Tipo
        {
            get { return _Tipo; }
            set { _Tipo = value; }
        }

        public int IdAutorizacionPrecio
        {
            get { return _IdAutorizacionPrecio; }
            set { _IdAutorizacionPrecio = value; }
        }
        public int Id_AutorizacionPrecioDet
        {
            get { return _Id_AutorizacionPrecioDet; }
            set { _Id_AutorizacionPrecioDet = value; }
        }
        public string Cte_NomComercial
        {
            get { return _Cte_NomComercial; }
            set { _Cte_NomComercial = value; }
        }
        public string Prd_Descripcion
        {
            get { return _Prd_Descripcion; }
            set { _Prd_Descripcion = value; }
        }
        public string Nom_Representante
        {
            get { return _Nom_Representante; }
            set { _Nom_Representante = value; }
        }
        public string MotivoRechazo
        {
            get { return _MotivoRechazo; }
            set { _MotivoRechazo = value; }
        }
        public string MotivoCancelacion
        {
            get { return _MotivoCancelacion; }
            set { _MotivoCancelacion = value; }
        }
        public string Nom_CDI
        {
            get { return _Nom_CDI; }
            set { _Nom_CDI = value; }
        }
        public string NomEstatus
        {
            get { return _NomEstatus; }
            set { _NomEstatus = value; }
        }
        public string NomTipoSolicitud
        {
            get { return _NomTipoSolicitud; }
            set { _NomTipoSolicitud = value; }
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
        public int IdRepresentante
        {
            get { return _IdRepresentante; }
            set { _IdRepresentante = value; }
        }
        public bool Activo
        {
            get { return _Activo; }
            set { _Activo = value; }
        }
        public int Estatus
        {
            get { return _Estatus; }
            set { _Estatus = value; }
        }
        public int IdUSolicita
        {
            get { return _IdUSolicita; }
            set { _IdUSolicita = value; }
        }
        public long Id_Prd
        {
            get { return _Id_Prd; }
            set { _Id_Prd = value; }
        }
        public Double Precio_Vta
        {
            get { return _Precio_Vta; }
            set { _Precio_Vta = value; }
        }
        public Double Precio_VtaAutorizado
        {
            get { return _Precio_VtaAutorizado; }
            set { _Precio_VtaAutorizado = value; }
        }

        public Double Precio_MinimoRik
        {
            get { return _Precio_MinimoRik; }
            set { _Precio_MinimoRik = value; }
        }

        public Double Precio_MinimoGte
        {
            get { return _Precio_MinimoGte; }
            set { _Precio_MinimoGte = value; }
        }


        public int TipoAutorizacion
        {
            get { return _TipoAutorizacion; }
            set { _TipoAutorizacion = value; }
        }
        public int Req_Aut_Director
        {
            get { return _Req_Aut_Director; }
            set { _Req_Aut_Director = value; }
        }
        public int IdUsuarioGteAutorizo
        {
            get { return _IdUsuarioGteAutorizo; }
            set { _IdUsuarioGteAutorizo = value; }
        }
        public int IdUsuarioDirAutorizo
        {
            get { return _IdUsuarioDirAutorizo; }
            set { _IdUsuarioDirAutorizo = value; }
        }
        public int IdUsuarioGteRechazo
        {
            get { return _IdUsuarioGteRechazo; }
            set { _IdUsuarioGteRechazo = value; }
        }
        public int IdUsuarioDirRechazo
        {
            get { return _IdUsuarioDirRechazo; }
            set { _IdUsuarioDirRechazo = value; }
        }

        public DateTime? FechaSolicitud
        {
            get { return _FechaSolicitud; }
            set { _FechaSolicitud = value; }
        }
        public DateTime? Fecha_UltMod
        {
            get { return _Fecha_UltMod; }
            set { _Fecha_UltMod = value; }
        }
        public DateTime? FechaAutorizacionGte
        {
            get { return _FechaAutorizacionGte; }
            set { _FechaAutorizacionGte = value; }
        }
        public DateTime? FechaAutorizacionDir
        {
            get { return _FechaAutorizacionDir; }
            set { _FechaAutorizacionDir = value; }
        }
        public DateTime? FechaRechazoGte
        {
            get { return _FechaRechazoGte; }
            set { _FechaRechazoGte = value; }
        }
        public DateTime? FechaRechazoDir
        {
            get { return _FechaRechazoDir; }
            set { _FechaRechazoDir = value; }
        }
        public DateTime? FechaInicial
        {
            get { return _FechaInicial; }
            set { _FechaInicial = value; }
        }
        public DateTime? FechaFinal
        {
            get { return _FechaFinal; }
            set { _FechaFinal = value; }
        }
        public int Id_Cte
        {
            get { return _Id_Cte; }
            set { _Id_Cte = value; }
        }
        public int Id_Ter
        {
            get { return _Id_Ter; }
            set { _Id_Ter = value; }
        }
        public string Justificacion
        {
            get { return _Justificacion; }
            set { _Justificacion = value; }
        }
        public string JustificacionMemo
        {
            get { return _JustificacionMemo; }
            set { _JustificacionMemo = value; }
        }
        public Double PrecioLista
        {
            get { return _PrecioLista; }
            set { _PrecioLista = value; }
        }


        public Double Cantidad
        {
            get { return _Cantidad; }
            set { _Cantidad = value; }
        }

        public Double Porc_Utilidad
        {
            get { return _Porc_Utilidad; }
            set { _Porc_Utilidad = value; }
        }
        public Double Precio_AAA
        {
            get { return _Precio_AAA; }
            set { _Precio_AAA = value; }
        }

        public Double Utilidad
        {
            get { return _Utilidad; }
            set { _Utilidad = value; }
        }
        public Double Importe_Utilidad
        {
            get { return _Importe_Utilidad; }
            set { _Importe_Utilidad = value; }
        }
        public Double Importe
        {
            get { return _Importe; }
            set { _Importe = value; }
        }
        public int Id_Motivo
        {
            get { return _Id_Motivo; }
            set { _Id_Motivo = value; }
        }
        public string JustificacionGte
        {
            get { return _JustificacionGte; }
            set { _JustificacionGte = value; }
        }
        public string Nom_Motivo
        {
            get { return _Nom_Motivo; }
            set { _Nom_Motivo = value; }
        }
        public DateTime? FechaVigencia
        {
            get { return _FechaVigencia; }
            set { _FechaVigencia = value; }
        }

        public int Id_Cpr
        {
            get { return _Id_Cpr; }
            set { _Id_Cpr = value; }
        }

        public int IdAutorizacionAnterior
        {
            get { return _IdAutorizacionAnterior; }
            set { _IdAutorizacionAnterior = value; }
        }

        public Double PrecioObjetivo
        {
            get { return _PrecioObjetivo; }
            set { _PrecioObjetivo = value; }
        }
        public string Id_Tamaño
        {
            get { return _Id_Tamaño; }
            set { _Id_Tamaño = value; }
        }

        public string Tipo_Cliente
        {
            get { return _Tipo_Cliente; }
            set { _Tipo_Cliente = value; }
        }

        public int Id_MotivoRechazo
        {
            get { return _Id_MotivoRechazo; }
            set { _Id_MotivoRechazo = value; }
        }
        public string JustificacionGeneral
        {
            get { return _JustificacionGeneral; }
            set { _JustificacionGeneral = value; }
        }
        public int Id_Seg
        {
            get { return _Id_Seg; }
            set { _Id_Seg = value; }
        }
        public int Id_ReporteGP
        {
            get { return _Id_ReporteGP; }
            set { _Id_ReporteGP = value; }
        }
        public double UnidadesProyectadas
        {
            get { return _UnidadesProyectadas; }
            set { _UnidadesProyectadas = value; }
        }
        public double VentaProy
        {
            get { return _VentaProy; }
            set { _VentaProy = value; }
        }

    }
}