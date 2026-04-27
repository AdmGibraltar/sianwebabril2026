using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class CartaPorte
    {
        public int CFDI_EstatusCanc;
        public string CFDI_AcuseCanc;
        private int id_Emp;
        private int id_Cd;
        private int id_Rem;
        private int id_CFDI;
        private string cFDI_FolioFiscal;
        private string cFDI_Sello;
        private DateTime cFDI_Fecha;

        public int Id_Emp { get => id_Emp; set => id_Emp = value; }
        public int Id_Cd { get => id_Cd; set => id_Cd = value; }
        public int Id_Rem { get => id_Rem; set => id_Rem = value; }
        public int Id_CFDI { get => id_CFDI; set => id_CFDI = value; }
        public string CFDI_FolioFiscal { get => cFDI_FolioFiscal; set => cFDI_FolioFiscal = value; }
        public string CFDI_Sello { get => cFDI_Sello; set => cFDI_Sello = value; }
        public object CFDI_Xml { get; set; }
        public byte[] CFDI_Pdf { get; set; }
        public string CFDI_Estatus { get; set; }
        public DateTime CFDI_Fecha { get => cFDI_Fecha; set => cFDI_Fecha = value; }
        public object CFDI_Cancelacion { get; set; }

        //Se utilizan para manejo de informacion del grid
        public long Id_Prd { get; set; }
        public string Descripcion { get; set; }
        public int Cantidad { get; set; }
        public string Origen { get; set; }
        public string Destino { get; set; }
        public DateTime Fecha { get; set; }
        public bool EsPeligroso { get; set; }
        public string ClaveMatPeligroso { get; set; }
        public double Kg { get; set; }
        public string ClaveEmbalaje { get; set; }
        public string Embalaje { get; set; }
        public double Distancia { get; set; }
        public string DescripcionEsp { get; set; }
        public long Id_PrdEsp { get; set; }
        public string CodigoPostal { get; set; }
        public string Colonia { get; set; }
        public int Id_RemDet { get; set; }
        public int Id_Doc { get; set; }
        public int Id_Cte { get; set; }
        public string TipoDoc { get; set; }
        public string Nom_Cliente { get; set; }
        public string PlacaVehiculo { get; set; }
        public string RfcChofer { get; set; }
        public int Id_U { get; set; }
    }
}