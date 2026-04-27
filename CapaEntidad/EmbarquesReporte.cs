using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class EmbarquesReporte
    {
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
        private int _Id_Emb;
        public int Id_Emb
        {
            get { return _Id_Emb; }
            set { _Id_Emb = value; }
        }

        private int _Id_U;
        public int Id_U
        {
            get { return _Id_U; }
            set { _Id_U = value; }
        }

        private string _Emb_Estatus;
        public string Emb_Estatus
        {
            get { return _Emb_Estatus; }
            set { _Emb_Estatus = value; }
        }

        private string _Emb_EstatusStr;
        public string Emb_EstatusStr
        {
            get { return _Emb_EstatusStr; }
            set { _Emb_EstatusStr = value; }
        }

        private string _U_Nombre;
        public string U_Nombre
        {
            get { return _U_Nombre; }
            set { _U_Nombre = value; }
        }

        private DateTime _Emb_Fec;
        public DateTime Emb_Fec
        {
            get { return _Emb_Fec; }
            set { _Emb_Fec = value; }
        }

        private DateTime _Emb_Dia;
        public DateTime Emb_Dia
        {
            get { return _Emb_Dia; }
            set { _Emb_Dia = value; }
        }

        private string _Emb_Chofer;
        public string Emb_Chofer
        {
            get { return _Emb_Chofer; }
            set { _Emb_Chofer = value; }
        }

        private string _Emb_Camioneta;
        public string Emb_Camioneta
        {
            get { return _Emb_Camioneta; }
            set { _Emb_Camioneta = value; }
        }

        private int _Id_Doc;
        public int Id_Doc
        {
            get { return _Id_Doc; }
            set { _Id_Doc = value; }
        }



        private string _Prd_Descripcion;
        public string Prd_Descripcion
        {
            get { return _Prd_Descripcion; }
            set { _Prd_Descripcion = value; }
        }
        private string _NomContacto;
        public string NomContacto
        {
            get { return _NomContacto; }
            set { _NomContacto = value; }
        }
        private string _Telefono;
        public string Telefono
        {
            get { return _Telefono; }
            set { _Telefono = value; }
        }
        private string _Email_Contacto;
        public string Email_Contacto
        {
            get { return _Email_Contacto; }
            set { _Email_Contacto = value; }
        }


        private string _Direccion;
        public string Direccion
        {
            get { return _Direccion; }
            set { _Direccion = value; }
        }
        private string _Latitud;
        public string Latitud
        {
            get { return _Latitud; }
            set { _Latitud = value; }
        }
        private string _Longitud;
        public string Longitud
        {
            get { return _Longitud; }
            set { _Longitud = value; }
        }



        private int _Fac_Cant;
        public int Fac_Cant
        {
            get { return _Fac_Cant; }
            set { _Fac_Cant = value; }
        }
        private Int64 _Id_Prd;
        public Int64 Id_Prd
        {
            get { return _Id_Prd; }
            set { _Id_Prd = value; }
        }
        private string _Id_Contacto;
        public string Id_Contacto
        {
            get { return _Id_Contacto; }
            set { _Id_Contacto = value; }
        }



        private DateTime _Fecha_MIN_ENTREGA;
        public DateTime Fecha_MIN_ENTREGA
        {
            get { return _Fecha_MIN_ENTREGA; }
            set { _Fecha_MIN_ENTREGA = value; }
        }

        private DateTime _Fecha_Max_entrega;
        public DateTime Fecha_Max_entrega
        {
            get { return _Fecha_Max_entrega; }
            set { _Fecha_Max_entrega = value; }
        }


        private string _MIN_VENTANAHORARIA1;
        public string MIN_VENTANAHORARIA1
        {
            get { return _MIN_VENTANAHORARIA1; }
            set { _MIN_VENTANAHORARIA1 = value; }
        }

        private string _MIN_VENTANAHORARIA2;
        public string MIN_VENTANAHORARIA2
        {
            get { return _MIN_VENTANAHORARIA2; }
            set { _MIN_VENTANAHORARIA2 = value; }
        }

        private string _MAX_VENTANAHORARIA1;
        public string MAX_VENTANAHORARIA1
        {
            get { return _MAX_VENTANAHORARIA1; }
            set { _MAX_VENTANAHORARIA1 = value; }
        }

        private string _MAX_VENTANAHORARIA2;
        public string MAX_VENTANAHORARIA2
        {
            get { return _MAX_VENTANAHORARIA2; }
            set { _MAX_VENTANAHORARIA2 = value; }
        }

        private double? _Costo;
        public double? Costo
        {
            get { return _Costo; }
            set { _Costo = value; }
        }

        private double? _Peso;
        public double? Peso
        {
            get { return _Peso; }
            set { _Peso = value; }
        }

        private double? _Volumen;
        public double? Volumen
        {
            get { return _Volumen; }
            set { _Volumen = value; }
        }



        private string _CT_Destino;
        public string CT_Destino
        {
            get { return _CT_Destino; }
            set { _CT_Destino = value; }
        }

        private double? _PesoVolumen;
        public double? PesoVolumen
        {
            get { return _PesoVolumen; }
            set { _PesoVolumen = value; }
        }
        private int _Importancia;
        public int Importancia
        {
            get { return _Importancia; }
            set { _Importancia = value; }
        }
        private int _ServiceTime;
        public int ServiceTime
        {
            get { return _ServiceTime; }
            set { _ServiceTime = value; }
        }

        private string _Campos_Avanzados;
        public string Campos_Avanzados
        {
            get { return _Campos_Avanzados; }
            set { _Campos_Avanzados = value; }
        }

        private string _Campos_Avanzados2;
        public string Campos_Avanzados2
        {
            get { return _Campos_Avanzados2; }
            set { _Campos_Avanzados2 = value; }
        }
        private string _Campos_Avanzados3;
        public string Campos_Avanzados3
        {
            get { return _Campos_Avanzados3; }
            set { _Campos_Avanzados3 = value; }
        }
        private string _Campos_Avanzados4;
        public string Campos_Avanzados4
        {
            get { return _Campos_Avanzados4; }
            set { _Campos_Avanzados4 = value; }
        }
        private string _Campos_Avanzados5;
        public string Campos_Avanzados5
        {
            get { return _Campos_Avanzados5; }
            set { _Campos_Avanzados5 = value; }
        }
        private string _Campos_Avanzados6;
        public string Campos_Avanzados6
        {
            get { return _Campos_Avanzados6; }
            set { _Campos_Avanzados6 = value; }
        }
        private string _Zona;
        public string Zona
        {
            get { return _Zona; }
            set { _Zona = value; }
        }

        private string _Calle;
        public string Calle
        {
            get { return _Calle; }
            set { _Calle = value; }
        }

        private string _Colonia;
        public string Colonia
        {
            get { return _Colonia; }
            set { _Colonia = value; }
        }

        private string _Pais;
        public string Pais
        {
            get { return _Pais; }
            set { _Pais = value; }
        }

        private string _CodigoPostal;
        public string CodigoPostal
        {
            get { return _CodigoPostal; }
            set { _CodigoPostal = value; }
        }

        private string _Municipio;
        public string Municipio
        {
            get { return _Municipio; }
            set { _Municipio = value; }
        }

        private string _Estado;
        public string Estado
        {
            get { return _Estado; }
            set { _Estado = value; }
        }

    }
}