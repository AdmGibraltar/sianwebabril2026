using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class Solicitud
    {
        private string cliente;
        private string asignadoA;

        private string estado;
        private string prioridad;
        private string accion;
        private int id_Solicitud;
        private string comentarios;
        private int id_Prioridad;
        private int id_Usuario;

        private int id_Categoria;
        private int numTiempoEstimado;
        private DateTime? fechaCreacion;
        private int id_Cliente;
        private int id_Estado;
        private int id_Investigador;
        private int id_Queja;

        private string correo;
        private int id_tServicio;
        private string conCopia;
        private int id_Accion;
        private int num_Factura;
        private string cte_Nom;
        private int id_Cd;
        private int id_Emp;
        private string nom_Sucursal;
        private string descripcion;
        private DateTime? fechaVencimiento;
        private string tipo_Servicio;

        private string tipo_Queja;
        private Int64? id_Prd;
        private string nom_Producto;
        private int? cantidad;
        private string presentacion;
        private double? costoAAA;
        private string investigador;
        private string nom_Investigador;
        private DateTime? fechaQueja;
        private DateTime? fechaSolicitud;
        private DateTime fechaEvento;
        private string motivos;
        private string dondeOcurrio;
        private string otroMotivo;
        private string motRechazo;
        private int id_TQueja;
        private string marca;

        public string Marca
        {
            get { return marca; }
            set { marca = value; }
        }

        private int diasCierre;
        private string inv_Correo;
        private DateTime? fechaTerminacion;

        public DateTime? FechaTerminacion
        {
            get { return fechaTerminacion; }
            set { fechaTerminacion = value; }
        }
        private DateTime? fechaCierre;

        public DateTime? FechaCierre
        {
            get { return fechaCierre; }
            set { fechaCierre = value; }
        }

        public string Inv_Correo
        {
            get { return inv_Correo; }
            set { inv_Correo = value; }
        }

        public int DiasCierre
        {
            get { return diasCierre; }
            set { diasCierre = value; }
        }

        public int Id_TQueja
        {
            get { return id_TQueja; }
            set { id_TQueja = value; }
        }

        public DateTime? FechaVencimiento
        {
            get { return fechaVencimiento; }
            set { fechaVencimiento = value; }
        }

        public DateTime FechaEvento
        {
            get { return fechaEvento; }
            set { fechaEvento = value; }
        }

        public string Motivos
        {
            get { return motivos; }
            set { motivos = value; }
        }

        public string DondeOcurrio
        {
            get { return dondeOcurrio; }
            set { dondeOcurrio = value; }
        }

        public string OtroMotivo
        {
            get { return otroMotivo; }
            set { otroMotivo = value; }
        }

        public string MotRechazo
        {
            get { return motRechazo; }
            set { motRechazo = value; }
        }

        public int Id_Emp
        {
            get { return id_Emp; }
            set { id_Emp = value; }
        }

        public int Id_Cd
        {
            get { return id_Cd; }
            set { id_Cd = value; }
        }

        public string Nom_Sucursal
        {
            get { return nom_Sucursal; }
            set { nom_Sucursal = value; }
        }

        public string Cte_Nom
        {
            get { return cte_Nom; }
            set { cte_Nom = value; }
        }

        public string Descripcion
        {
            get { return descripcion; }
            set { descripcion = value; }
        }

        public DateTime? FechaCreacion
        {
            get { return fechaCreacion; }
            set { fechaCreacion = value; }
        }

        public string Tipo_Servicio
        {
            get { return tipo_Servicio; }
            set { tipo_Servicio = value; }
        }

        public string Cliente
        {
            get { return cliente; }
            set { cliente = value; }
        }

        public string AsignadoA
        {
            get { return asignadoA; }
            set { asignadoA = value; }
        }

        public string Estado
        {
            get { return estado; }
            set { estado = value; }
        }

        public string Prioridad
        {
            get { return prioridad; }
            set { prioridad = value; }
        }

        public string Accion
        {
            get { return accion; }
            set { accion = value; }
        }

        public int Id_Solicitud
        {
            get { return id_Solicitud; }
            set { id_Solicitud = value; }
        }


        public string Comentarios
        {
            get { return comentarios; }
            set { comentarios = value; }
        }

        public int Id_Prioridad
        {
            get { return id_Prioridad; }
            set { id_Prioridad = value; }
        }

        public int Id_Usuario
        {
            get { return id_Usuario; }
            set { id_Usuario = value; }
        }

        public int Id_Categoria
        {
            get { return id_Categoria; }
            set { id_Categoria = value; }
        }

        public int NumTiempoEstimado
        {
            get { return numTiempoEstimado; }
            set { numTiempoEstimado = value; }
        }

        public int Id_Investigador
        {
            get { return id_Investigador; }
            set { id_Investigador = value; }
        }

        public int Id_Estado
        {
            get { return id_Estado; }
            set { id_Estado = value; }
        }

        public string ConCopia
        {
            get { return conCopia; }
            set { conCopia = value; }
        }

        public int Id_tServicio
        {
            get { return id_tServicio; }
            set { id_tServicio = value; }
        }

        public int Num_Factura
        {
            get { return num_Factura; }
            set { num_Factura = value; }
        }

        public int Id_Accion
        {
            get { return id_Accion; }
            set { id_Accion = value; }
        }

        public string Correo
        {
            get { return correo; }
            set { correo = value; }
        }

        public int Id_Queja
        {
            get { return id_Queja; }
            set { id_Queja = value; }
        }

        public int Id_Cliente
        {
            get { return id_Cliente; }
            set { id_Cliente = value; }
        }

        public string Tipo_Queja
        {
            get { return tipo_Queja; }
            set { tipo_Queja = value; }
        }

        public Int64? Id_Prd
        {
            get { return id_Prd; }
            set { id_Prd = value; }
        }

        public string Nom_Producto
        {
            get { return nom_Producto; }
            set { nom_Producto = value; }
        }

        public int? Cantidad
        {
            get { return cantidad; }
            set { cantidad = value; }
        }

        public string Presentacion
        {
            get { return presentacion; }
            set { presentacion = value; }
        }

        public DateTime? FechaQueja
        {
            get { return fechaQueja; }
            set { fechaQueja = value; }
        }

        public DateTime? FechaSolicitud
        {
            get { return fechaSolicitud; }
            set { fechaSolicitud = value; }
        }

        public string Nom_Investigador
        {
            get { return nom_Investigador; }
            set { nom_Investigador = value; }
        }

        public string Investigador
        {
            get { return investigador; }
            set { investigador = value; }
        }

        public double? CostoAAA
        {
            get { return costoAAA; }
            set { costoAAA = value; }
        }
    }
}
