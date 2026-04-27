using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class Queja
    {
        #region Variables
        int id_Cliente;
        string nom_Cliente;
        int id_Sucursal;
        int id_Estatus;
        int id_Emp;
        int id_Cd;
        string correo;
        int id_TQueja;
        int id_Prioridad;
        int id_Clas;
        string asunto;
        string descripcion;
        int id_Queja;
        private string embarque;
        private string nomUbicacion;
        private string nomPuesto;
        private string guia_Flete;
        private string motivos;
        private string tipoQueja;
        private DateTime? fec_Evento;
        private int dondeOcurrio;
        private int? id_CteDirecto;
        private string nom_CteDirecto;
        private string otroMotivo;
        private string nomComTransporte;
        private string nomChofer;
        private string placas;
        private DateTime? fec_Arribo;
        private DateTime? fec_Embarque;
        private DateTime? fechaCreacion;

        public DateTime? FechaCreacion
        {
            get { return fechaCreacion; }
            set { fechaCreacion = value; }
        }


        #endregion

        #region Metodos
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
        public int Id_Estatus
        {
            get { return id_Estatus; }
            set { id_Estatus = value; }
        }
        public int Id_Sucursal
        {
            get { return id_Sucursal; }
            set { id_Sucursal = value; }
        }
        public string Nom_Cliente
        {
            get { return nom_Cliente; }
            set { nom_Cliente = value; }
        }
        public string Correo
        {
            get { return correo; }
            set { correo = value; }
        }
        public string NomUbicacion
        {
            get { return nomUbicacion; }
            set { nomUbicacion = value; }
        }
        public string NomPuesto
        {
            get { return nomPuesto; }
            set { nomPuesto = value; }
        }
        public string Embarque
        {
            get { return embarque; }
            set { embarque = value; }
        }
        public string TipoQueja
        {
            get { return tipoQueja; }
            set { tipoQueja = value; }
        }
        public string Motivos
        {
            get { return motivos; }
            set { motivos = value; }
        }
        public string Guia_Flete
        {
            get { return guia_Flete; }
            set { guia_Flete = value; }
        }
        public int Id_Cliente
        {
            get { return id_Cliente; }
            set { id_Cliente = value; }
        }
        public int Id_TQueja
        {
            get { return id_TQueja; }
            set { id_TQueja = value; }
        }
        public int Id_Prioridad
        {
            get { return id_Prioridad; }
            set { id_Prioridad = value; }
        }
        public int Id_Clas
        {
            get { return id_Clas; }
            set { id_Clas = value; }
        }
        public string Asunto
        {
            get { return asunto; }
            set { asunto = value; }
        }
        public string Descripcion
        {
            get { return descripcion; }
            set { descripcion = value; }
        }
        public int Id_Queja
        {
            get { return id_Queja; }
            set { id_Queja = value; }
        }
        public DateTime? Fec_Evento
        {
            get { return fec_Evento; }
            set { fec_Evento = value; }
        }
        public int DondeOcurrio
        {
            get { return dondeOcurrio; }
            set { dondeOcurrio = value; }
        }
        public int? Id_CteDirecto
        {
            get { return id_CteDirecto; }
            set { id_CteDirecto = value; }
        }
        public string Nom_CteDirecto
        {
            get { return nom_CteDirecto; }
            set { nom_CteDirecto = value; }
        }
        public string OtroMotivo
        {
            get { return otroMotivo; }
            set { otroMotivo = value; }
        }
        public string NomComTransporte
        {
            get { return nomComTransporte; }
            set { nomComTransporte = value; }
        }
        public string NomChofer
        {
            get { return nomChofer; }
            set { nomChofer = value; }
        }
        public string Placas
        {
            get { return placas; }
            set { placas = value; }
        }
        public DateTime? Fec_Arribo
        {
            get { return fec_Arribo; }
            set { fec_Arribo = value; }
        }
        public DateTime? Fec_Embarque
        {
            get { return fec_Embarque; }
            set { fec_Embarque = value; }
        }

        #endregion

        private DateTime? fechaEmbarque;

        public DateTime? FechaEmbarque
        {
            get { return fechaEmbarque; }
            set { fechaEmbarque = value; }
        }

        private int id_Rem;

        public int Id_Rem
        {
            get { return id_Rem; }
            set { id_Rem = value; }
        }
    }
}