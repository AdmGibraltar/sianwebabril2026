using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class eAcysCN_Productos
    {
        private string _NombreNodo;
        public string NombreNodo
        {
            get { return _NombreNodo; }
            set { _NombreNodo = value; }
        }

        private int _Id_Matriz;
        public int Id_Matriz
        {
            get { return _Id_Matriz; }
            set { _Id_Matriz = value; }
        }

        private int _Id_ACYS;
        public int Id_ACYS
        {
            get { return _Id_ACYS; }
            set { _Id_ACYS = value; }
        }

        private string _Nombre;
        public string Nombre
        {
            get { return _Nombre; }
            set { _Nombre = value; }
        }

        private Int64 _Id_Prd;
        public Int64 Id_Prd
        {
            get { return _Id_Prd; }
            set { _Id_Prd = value; }
        }

        private int _Id_TG;
        public int Id_TG
        {
            get { return _Id_TG; }
            set { _Id_TG = value; }
        }

        private double _Cantidad;
        public double Cantidad
        {
            get { return _Cantidad; }
            set { _Cantidad = value; }
        }

        private double _Precio;
        public double Precio
        {
            get { return _Precio; }
            set { _Precio = value; }
        }

        private double _Subtotal;
        public double Subtotal
        {
            get { return _Subtotal; }
            set { _Subtotal = value; }
        }

        private int _Frecuencia;
        public int Frecuencia
        {
            get { return _Frecuencia; }
            set { _Frecuencia = value; }
        }

        private int _Lun;
        public int Lun
        {
            get { return _Lun; }
            set { _Lun = value; }
        }

        private int _Mar;
        public int Mar
        {
            get { return _Mar; }
            set { _Mar = value; }
        }

        private int _Mie;
        public int Mie
        {
            get { return _Mie; }
            set { _Mie = value; }
        }

        private int _Jue;
        public int Jue
        {
            get { return _Jue; }
            set { _Jue = value; }
        }

        private int _Vie;
        public int Vie
        {
            get { return _Vie; }
            set { _Vie = value; }
        }

        private int _Sab;
        public int Sab
        {
            get { return _Sab; }
            set { _Sab = value; }
        }

        private string _Documento;
        public string Documento
        {
            get { return _Documento; }
            set { _Documento = value; }
        }

        private string _Descripcion;
        public string Descripcion
        {
            get { return _Descripcion; }
            set { _Descripcion = value; }
        }

        private string _Unidad;
        public string Unidad
        {
            get { return _Unidad; }
            set { _Unidad = value; }
        }

        private double _Presentacion;
        public double Presentacion
        {
            get { return _Presentacion; }
            set { _Presentacion = value; }
        }

        //

    }
}