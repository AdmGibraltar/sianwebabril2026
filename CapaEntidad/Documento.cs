using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class Documento
    {
        int id_Doc;

        public int Id_Doc
        {
            get { return id_Doc; }
            set { id_Doc = value; }
        }
        string doc_Nombre;

        public string Doc_Nombre
        {
            get { return doc_Nombre; }
            set { doc_Nombre = value; }
        }
        private string formato;

        public string Formato
        {
            get { return formato; }
            set { formato = value; }
        }
        private long tamano;

        public long Tamano
        {
            get { return tamano; }
            set { tamano = value; }
        }


        string url;

        public string Url
        {
            get { return url; }
            set { url = value; }
        }

        byte[] archivo;

        public byte[] Archivo
        {
            get { return archivo; }
            set { archivo = value; }
        }
        int id_Emp;

        public int Id_Emp
        {
            get { return id_Emp; }
            set { id_Emp = value; }
        }

        int id_Cd;

        public int Id_Cd
        {
            get { return id_Cd; }
            set { id_Cd = value; }
        }
        string tipoDoc;

        public string TipoDoc
        {
            get { return tipoDoc; }
            set { tipoDoc = value; }
        }

    }
}
