using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class InventariosNoRota
    {
        #region Variables
        private int id_Cd;
        private string sucursal;
        private string proveedor;
        private Int64 codigo;
        private string descripcion;
        private double monto;
        private int cantidad;
        private int disponible;
        private string estatus;
        private string categoria;
        private bool rota;

        public bool Rota
        {
            get { return rota; }
            set { rota = value; }
        }
        #endregion

        #region Metodos
        public int Id_Cd
        {
            get { return id_Cd; }
            set { id_Cd = value; }
        }
        public string Sucursal
        {
            get { return sucursal; }
            set { sucursal = value; }
        }
        public string Proveedor
        {
            get { return proveedor; }
            set { proveedor = value; }
        }
        public Int64 Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }
        public string Descripcion
        {
            get { return descripcion; }
            set { descripcion = value; }
        }
        public double Monto
        {
            get { return monto; }
            set { monto = value; }
        }
        public int Cantidad
        {
            get { return cantidad; }
            set { cantidad = value; }
        }
        public int Disponible
        {
            get { return disponible; }
            set { disponible = value; }
        }
        public string Estatus
        {
            get { return estatus; }
            set { estatus = value; }
        }
        public string Categoria
        {
            get { return categoria; }
            set { categoria = value; }
        }

        #endregion
    }
}