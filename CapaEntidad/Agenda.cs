using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Agenda
    {
        public int ID { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Subject { get; set; }
        public string Descripcion { get; set; }
        public string Location { get; set; }
        public string AllDay { get; set; }
        public string estatus { get; set; }
        public string label { get; set; }

        public int Id_Emp { get; set; }
        public int Id_Cd { get; set; }
        public int Id_Usu { get; set; }
        public int id_ActividadGral { get; set; }
        public int id_cte { get; set; }
        public int id_Actividad { get; set; }
        public int id_tu { get; set; }
        public int id_agendaGrupal { get; set; }

        public string ActividadGral { get; set; }
        public string tipo { get; set; }
        public string Actividad { get; set; }
        public string nombre { get; set; }
        public string Comentarios { get; set; }
        public string color { get; set; }
        public string inicioEjecucion { get; set; }
        public string finalEjecucion { get; set; }
        public string usuario { get; set; }
        public string TipoUsuario { get; set; }
        public string nombreCDI { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime fechaFinal { get; set; }
        public DateTime HoraInicio { get; set; }
        public DateTime HoraFinal { get; set; }

        public string Estatus { get; set; }
        public int Cantidad { get; set; }
        public string colorACtividad { get; set; }
        public int verificador { get; set; }
        public int Id_Rik { get; set; }
    }
}