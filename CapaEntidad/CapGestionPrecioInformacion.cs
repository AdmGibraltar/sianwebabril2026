using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 
namespace CapaEntidad
{
    public class CapGestionPrecioInformacion
    {
        int _Id;
        string _Descripcion;
        string _NombreArchivo;
        string _RutaArchivo;
        int _Empresa;
        int _Tipo;
        bool _Estatus;
        int _Id_U;
      
        public int Id_Gestion
        {
            get { return _Id; }
            set { _Id = value; }
        }

        public string Gestion_Descripcion
        {
            get { return _Descripcion; }
            set { _Descripcion = value; }
        }
        public string Gestion_NombreArchivo
        {
            get { return _NombreArchivo; }
            set { _NombreArchivo = value; }
        }
        public string Gestion_RutaArchivo
        {
            get { return _RutaArchivo; }
            set { _RutaArchivo = value; }
        }
        public int Id_Emp
        {
            get { return _Empresa; }
            set { _Empresa = value; }
        }
        public int Gestion_Tipo
        {
            get { return _Tipo; }
            set { _Tipo = value; }
        }
        public bool Gestion_Estatus
        {
            get { return _Estatus; }
            set { _Estatus = value; }
        }
        public int Gestion_Id_U
        {
            get { return _Id_U; }
            set { _Id_U = value; }
        }
    }
}

