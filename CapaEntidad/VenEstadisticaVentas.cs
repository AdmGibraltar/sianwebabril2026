using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class VenEstadisticaVentas
    {
        #region Variables
        int id_Cd;
        int filtro;
        string sFiltro;
        string sucursal;
        string sSucursal;
        string territorio;
        string sTerritorio;
        string cliente;
        string sCliente;
        string producto;
        string sProducto;
        int anio;
        string sAnio;
        int mostrar;
        string sMostrar;
        int nivel;
        string sNivel;
        int nivel2;
        string sNivel2;
        string encabezado;
        string encabezado1;
        int reporte;
        int filtroSem;
        DateTime? fechaInicio { get; set; }
        DateTime? fechaFin { get; set; }
        public DateTime? FechaInicio
        {
            get { return fechaInicio; }
            set { fechaInicio = value; }
        }
        public DateTime? FechaFin
        {
            get { return fechaFin; }
            set { fechaFin = value; }
        }
        public int FiltroSem
        {
            get { return filtroSem; }
            set { filtroSem = value; }
        }
        #endregion

        #region Refactorizado
        public int Id_Cd
        {
            get { return id_Cd; }
            set { id_Cd = value; }
        }
        public int Filtro
        {
            get { return filtro; }
            set { filtro = value; }
        }
        public string SFiltro
        {
            get { return sFiltro; }
            set { sFiltro = value; }
        }
        public string Sucursal
        {
            get { return sucursal; }
            set { sucursal = value; }
        }
        public string Territorio
        {
            get { return territorio; }
            set { territorio = value; }
        }
        public string Cliente
        {
            get { return cliente; }
            set { cliente = value; }
        }
        public string Producto
        {
            get { return producto; }
            set { producto = value; }
        }
        public int Anio
        {
            get { return anio; }
            set { anio = value; }
        }
        public string SAnio
        {
            get { return sAnio; }
            set { sAnio = value; }
        }
        public int Mostrar
        {
            get { return mostrar; }
            set { mostrar = value; }
        }
        public string SMostrar
        {
            get { return sMostrar; }
            set { sMostrar = value; }
        }
        public int Nivel
        {
            get { return nivel; }
            set { nivel = value; }
        }
        public string SNivel
        {
            get { return sNivel; }
            set { sNivel = value; }
        }
        public int Nivel2
        {
            get { return nivel2; }
            set { nivel2 = value; }
        }
        public string SNivel2
        {
            get { return sNivel2; }
            set { sNivel2 = value; }
        }
        public string SSucursal
        {
            get { return sSucursal; }
            set { sSucursal = value; }
        }
        public string STerritorio
        {
            get { return sTerritorio; }
            set { sTerritorio = value; }
        }
        public string SCliente
        {
            get { return sCliente; }
            set { sCliente = value; }
        }
        public string SProducto
        {
            get { return sProducto; }
            set { sProducto = value; }
        }
        public string Encabezado
        {
            get { return encabezado; }
            set { encabezado = value; }
        }
        public string Encabezado1
        {
            get { return encabezado1; }
            set { encabezado1 = value; }
        }
        public int Reporte
        {
            get { return reporte; }
            set { reporte = value; }
        }
        #endregion


        #region datosRepServ

        public int id_emp { get; set; }
        public int id_usu { get; set; }
        public int id_cte { get; set; }
        public int id_ter { get; set; }
        public int id_tu { get; set; }
        public int id_rik { get; set; }

        public string nombre_Comercial { get; set; }
        public string nombre_terr { get; set; }
        public string nombre_rik { get; set; }
        public string tipoUsuario { get; set; }

        public double mes1 { get; set; }
        public double mes2 { get; set; }
        public double mes3 { get; set; }
        public double mes4 { get; set; }
        public double mes5 { get; set; }
        public double mes6 { get; set; }
        public double mes7 { get; set; }
        public double mes8 { get; set; }
        public double mes9 { get; set; }
        public double mes10 { get; set; }
        public double mes11 { get; set; }
        public double mes12 { get; set; }
        public double total { get; set; }
        #endregion

        public long id_prd { get; set; }
        public string categoria { get; set; }
        public string sCategoria { get; set; }
        public Dictionary<string, long> Meses { get; set; } = new Dictionary<string, long>();

    }
}