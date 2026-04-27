using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class Maps
    {
        private int _Id_Emp;
        private int _Id_Cd;
        private int _Id_Cliente;
        private string _Sucursal;
        private decimal _Latitud;
        private decimal _Longitud;

        private int _Id_Ruta;
        private string _Ruta;
        private string _Direccion;
        bool _Activo;

        private int _Segmento;
        private decimal _LatOrigen;
        private decimal _LngOrigen;
        private decimal _LatDestino;
        private decimal _LngDestino;
        private decimal _Kilometros;

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

        public int Id_Cliente
        {
            get { return _Id_Cliente; }
            set { _Id_Cliente = value; }
        }

        public string Sucursal
        {
            get { return _Sucursal; }
            set { _Sucursal = value; }
        }

        public decimal Latitud
        {
            get { return _Latitud; }
            set { _Latitud = value; }
        }

        public decimal Longitud
        {
            get { return _Longitud; }
            set { _Longitud = value; }
        }

        public int Id_Ruta
        {
            get { return _Id_Ruta; }
            set { _Id_Ruta = value; }
        }

        public string Ruta
        {
            get { return _Ruta; }
            set { _Ruta = value; }
        }

        public bool Activo
        {
            get { return _Activo; }
            set { _Activo = value; }
        }

        public int Segmento
        {
            get { return _Segmento; }
            set { _Segmento = value; }
        }

        public decimal LatOrigen
        {
            get { return _LatOrigen; }
            set { _LatOrigen = value; }
        }

        public decimal LngOrigen
        {
            get { return _LngDestino; }
            set { _LngDestino = value; }
        }

        public decimal LatDestino
        {
            get { return _LatDestino; }
            set { _LatDestino = value; }
        }

        public decimal LngDestino
        {
            get { return _Longitud; }
            set { _Longitud = value; }
        }

        public decimal Kilometros
        {
            get { return _Kilometros; }
            set { _Kilometros = value; }
        }

        public string Direccion
        {
            get { return _Direccion; }
            set { _Direccion = value; }
        }
    }
}
