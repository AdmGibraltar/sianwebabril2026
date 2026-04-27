using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/// <summary>
/// Summary description for ListaPrecios
/// </summary>
/// 

namespace CapaEntidad
{

    public class ListaPrecios
    {
        public Int64? Id_Prd { get; set; }
        public string Descripcion { get; set; }
        public string ESTATUS { get; set; }
        public string APLICACION { get; set; }
        public string SUBFAMILIA { get; set; }
        public string TIPOPRODUCTO { get; set; }
        public string LISTADEPRECIOS { get; set; }
        public string NOPROVEEDOR { get; set; }
        public string NOMBREPROVEEDOR { get; set; }
        public string NODEARTDEPROVEEDOR { get; set; }
        public decimal? PAAAACTUAL { get; set; }
        public decimal? PLISTAACTUAL { get; set; }
        public decimal? PAAAAnterior { get; set; }
        public decimal? PLISTAANTERIOR { get; set; }
        public decimal? PAAAFUTURA { get; set; }
        public decimal? PLISTAFUTURA { get; set; }
        public decimal? PVariacionPAAA { get; set; }
        public decimal? PVariacionPLISTA { get; set; }
        public DateTime? FECHAINICIOVIG { get; set; }
        public DateTime? FECHAINICIOVIGFUT { get; set; }
        public DateTime? FECHAFINVIG { get; set; }
        public DateTime? FECHAFINVIGFUT { get; set; }
        public bool TIENEPRECIOFUTURO { get; set; }
        public string RESPONSABLE { get; set; }
        public string PLANEACION { get; set; }
        public decimal? margenred { get; set; }
        //23 junio requerimiento 
        public string Presentacion { get; set; }
        public decimal? UnidaddeVenta { get; set; }
        public decimal? PVariacionPAAAFUTURO { get; set; }
        public decimal? PVariacionPLISTAFUTURO { get; set; }




        public ListaPrecios()
        {
            this.Id_Prd = null;
            this.Descripcion = "";
            this.ESTATUS = null;
            this.APLICACION = null;
            this.SUBFAMILIA = null;
            this.TIPOPRODUCTO = null;
            this.LISTADEPRECIOS = null;
            this.NOPROVEEDOR = null;
            this.NOMBREPROVEEDOR = null;
            this.NODEARTDEPROVEEDOR = null;
            this.PAAAACTUAL = null;
            this.PLISTAACTUAL = null;
            this.PAAAAnterior = null;
            this.PLISTAANTERIOR = null;
            this.PAAAFUTURA = null;
            this.PLISTAFUTURA = null;
            this.PVariacionPAAA = null;
            this.PVariacionPLISTA = null;
            this.FECHAINICIOVIG = null;
            this.FECHAINICIOVIGFUT = null;
            this.FECHAFINVIG = null;
            this.FECHAFINVIGFUT = null;
            this.TIENEPRECIOFUTURO = false;
            this.PLANEACION = null;
            this.RESPONSABLE = null;
            this.margenred = null;
            //23 junio requerimiento 
            this.Presentacion = "";
            this.UnidaddeVenta = null;
            this.PVariacionPAAAFUTURO = null;
            this.PVariacionPLISTAFUTURO = null;

        }

        public ListaPrecios(

            Int64 Id_Prd,
            string Descripcion,
            string ESTATUS,
            string APLICACION,
            string SUBFAMILIA,
            string TIPOPRODUCTO,
            string LISTADEPRECIOS,
            string NOPROVEEDOR,
            string NOMBREPROVEEDOR,
            string NODEARTDEPROVEEDOR,
            decimal? PAAAACTUAL,
            decimal? PLISTAACTUAL,
            decimal? PAAAAnterior,
            decimal? PLISTAANTERIOR,
            decimal? PAAAFUTURA,
            decimal? PLISTAFUTURA,
            decimal? PVariacionPAAA,
            decimal? PVariacionPLISTA,
            DateTime? FECHAINICIOVIG,
            DateTime? FECHAINICIOVIGFUT,
            DateTime? FECHAFINVIG,
            DateTime? FECHAFINVIGFUT,
            bool TIENEPRECIOFUTURO,
            string RESPONSABLE,
            string PLANEACION,
            decimal? margenred,
            //23 junio requerimiento 
            string Presentacion,
            decimal? UnidaddeVenta,
            decimal? PVariacionPAAAFUTURO,
            decimal? PVariacionPLISTAFUTURO
        )
        {
            this.Id_Prd = null;
            this.Descripcion = null;
            //this.Tipo_Documento = null;
            //this.Serie = null;
            //this.Folio_Documento = null;
            //this.Fecha_Documento = null;
            //this.Hora_Documento = null;
            this.PAAAACTUAL = null;
            //this.nombre = nombre;
            //this.rfc = rfc;
            //this.ArchivoPDF = null;
            //this.ArchivoXML = null;
            //this.IsGroupHeader = true;
        }

        public ListaPrecios(
            Int64? Id_Prd,
            string Descripcion,
            //string Tipo_Documento,
            //string Serie,
            //string Folio_Documento,
            //DateTime? Fecha_Documento,
            //string Hora_Documento,
            decimal? Precio
        //string nombre,
        //string rfc,
        //string ArchivoPDF,
        //string ArchivoXML
        )
        {
            this.Id_Prd = Id_Prd;
            this.Descripcion = Descripcion;
            this.PAAAACTUAL = Precio;
            //this.Tipo_Documento = Tipo_Documento;
            //this.Serie = Serie;
            //this.Folio_Documento = Folio_Documento;
            //this.Fecha_Documento = Fecha_Documento;
            //this.Hora_Documento = Hora_Documento;
            //this.Importe_Total_Documento = Importe_Total_Documento;
            //this.nombre = nombre;
            //this.rfc = rfc;
            //this.ArchivoPDF = ArchivoPDF;
            //this.ArchivoXML = ArchivoXML;
            //this.IsGroupHeader = false;
        }

        public override string ToString()
        {
            return String.Format(
                "{0}{1} Fecha:{2} Importe:{3} ",
                this.Descripcion,
                "Campo 1 ",
                Convert.ToDateTime(this.FECHAFINVIG).ToString("MM/dd/yyyy"),
                Convert.ToDecimal(this.PAAAACTUAL).ToString("C")

            );
        }
    }

}