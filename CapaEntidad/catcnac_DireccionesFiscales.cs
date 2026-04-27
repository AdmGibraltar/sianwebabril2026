using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class catcnac_DireccionesFiscales
    {
        public int id_ClienteMatriz { get; set; }
        public int Id { get; set; }
        public string ClienteDirFis { get; set; }
        public string DireccionDirFis { get; set; }
        public string MunicipioDirFis { get; set; }
        public string Pais { get; set; }
        public int CPDirFis { get; set; }
        public string EstadoDirFis { get; set; }
        public string RFCDirFis { get; set; }
        public string EmailFacturasDirFis { get; set; }
        public string ColoniaDirFis { get; set; }
        public string NumeroDirFis { get; set; }
        public int FranqConsecionada { get; set; }
        public int Addenda { get; set; }
        public string AddendaTipo { get; set; }
        public string AddendaDesc { get; set; }

    }
}