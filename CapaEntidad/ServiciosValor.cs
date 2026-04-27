using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class ServiciosValor
    {
        private int _Id_Emp;
        private int _Id_Cd;

        private int _IdRol;
        private string _Rol;

        private int _IdServicio;
        private string _Servicio;

        private int _IdSegmento;
        private string _Segmento;

        private int _SecuenciaApp;
        private int _IdAplicacion;
        private string _Aplicacion;

        private int _Secuencia;

        private string _IdSegmentoX;
        private string _SegmentoX;

        private string _IdAplicacionX;
        private string _AplicacionX;

        private double _Costo;
        private int _IdPrd;
        private string _Prd;

        private int _SecuenciaTipoApp;
        private int _IdTipoApp;
        private string _TipoApp;

        private string _IdTipoAppX;
        private string _TipoAppX;

        private int _Despiece;
        private int _EsEquipo;
        bool _Activo;
        
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

        public int IdRol
        {
            get { return _IdRol; }
            set { _IdRol = value; }
        }

        public string Rol
        {
            get { return _Rol; }
            set { _Rol = value; }
        }

        public int IdServicio
        {
            get { return _IdServicio; }
            set { _IdServicio = value; }
        }

        public string Servicio
        {
            get { return _Servicio; }
            set { _Servicio = value; }
        }

        public int IdSegmento
        {
            get { return _IdSegmento; }
            set { _IdSegmento = value; }
        }

        public string Segmento
        {
            get { return _Segmento; }
            set { _Segmento = value; }
        }

        public int SecuenciaApp
        {
            get { return _SecuenciaApp; }
            set { _SecuenciaApp = value; }
        }

        public int IdAplicacion
        {
            get { return _IdAplicacion; }
            set { _IdAplicacion = value; }
        }

        public string Aplicacion
        {
            get { return _Aplicacion; }
            set { _Aplicacion = value; }
        }

        public string IdAplicacionX
        {
            get { return _IdAplicacionX; }
            set { _IdAplicacionX = value; }
        }

        public string AplicacionX
        {
            get { return _AplicacionX; }
            set { _AplicacionX = value; }
        }

        public int Secuencia
        {
            get { return _Secuencia; }
            set { _Secuencia = value; }
        }

        public string IdSegmentoX
        {
            get { return _IdSegmentoX; }
            set { _IdSegmentoX = value; }
        }

        public string SegmentoX
        {
            get { return _SegmentoX; }
            set { _SegmentoX = value; }
        }

        public double Costo
        {
            get { return _Costo; }
            set { _Costo = value; }
        }

        public int IdPrd
        {
            get { return _IdPrd; }
            set { _IdPrd = value; }
        }
        public string Prd
        {
            get { return _Prd; }
            set { _Prd = value; }
        }

        public bool Activo
        {
            get { return _Activo; }
            set { _Activo = value; }
        }


        public int SecuenciaTipoApp
        {
            get { return _SecuenciaTipoApp; }
            set { _SecuenciaTipoApp = value; }
        }

        public int IdTipoApp
        {
            get { return _IdTipoApp; }
            set { _IdTipoApp = value; }
        }

        public string TipoApp
        {
            get { return _TipoApp; }
            set { _TipoApp = value; }
        }

        public string IdTipoAppX
        {
            get { return _IdTipoAppX; }
            set { _IdTipoAppX = value; }
        }

        public string TipoAppX
        {
            get { return _TipoAppX; }
            set { _TipoAppX = value; }
        }

        public int Despiece
        {
            get { return _Despiece; }
            set { _Despiece = value; }
        }

        public int EsEquipo
        {
            get { return _EsEquipo; }
            set { _EsEquipo = value; }
        }

        public int id_cte { get; set; }
        public string  nombreCliente { get; set; }
        public string filtrofehcaInicial { get; set; }
        public string filtroFechaFinal { get; set; }
        public int idTicket { get; set; }
        public int idUsuario { get; set; }
        public string idOrdenServicio { get; set; }
        public string fechasolicitud { get; set; }
        public string fechaAsignacion { get; set; }
        public string fechaProgramacion { get; set; }
        public string estatusstr { get; set; }
        public int estatus { get; set; }
        public string TipoTicket { get; set; }
        public string nombreUsuario { get; set; }
        public string descripcionticket { get; set; }

        public int idSecuencia { get; set; }
        public int IDFoto { get; set; }
        public string strRutaFoto { get; set; }
    }
}
