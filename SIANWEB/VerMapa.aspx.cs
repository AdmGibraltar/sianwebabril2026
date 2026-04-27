using CapaEntidad;
using CapaNegocios;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SIANWEB
{
    public partial class VerMapa : System.Web.UI.Page
    {
        #region Variables 

        public Sesion session
        {
            get
            {
                return (Sesion)Session["Sesion" + Session.SessionID];
            }
            set
            {
                Session["session" + Session.SessionID] = value;

            }
        }

        private string Emp_CnxCentral
        {
            get { return ConfigurationManager.AppSettings.Get("strConnectionCentral"); }
        }

        #endregion

        public string strUbicaciones;
        public string strInicio;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["idAgendaMapa"] != null)
            {
                cargarMapa(Convert.ToInt32(Request.QueryString["idAgendaMapa"].ToString()));
            }
        }

        public void cargarMapa(int id_agenda)
        {

            strUbicaciones = "";
            CNRSCAgenda CN = new CNRSCAgenda();
            Geolocalizacion geo = new Geolocalizacion();
            List<Geolocalizacion> List = new List<Geolocalizacion>();
            geo.id_emp = session.Id_Emp;
            geo.ID_Cd = session.Id_Cd;
            geo.Id_Agenda = id_agenda;

            CN.ConsultaragendaGeolocalizacion(geo, ref List, session.Emp_Cnx);

            AgendaRsc Registro = new AgendaRsc();

            List<Agenda> list = new List<Agenda>();

            Registro.Id_Emp = session.Id_Emp;
            Registro.Id_Cd = session.Id_Cd;
            Registro.ID = id_agenda;

            CN.ConsultarAgendaDetallada(Registro, ref list, session.Emp_Cnx);




            string jsonFeature = "";

            for (var i = 0; i < List.Count; i++)
            {
                string direcciones = "";
                direccion(List[i].Longitud.ToString(), List[i].Latitud.ToString(), ref direcciones);
                string estatus = "";
                if (i != 0)
                {
                    strUbicaciones = strUbicaciones + ",";
                }
                else
                {
                    strInicio = List[i].Latitud + "," + List[i].Longitud;
                }
                if (List[i].Estatus == "I")
                {
                    estatus = "Inicio de Actividad </br>" + "Actividad: " + list.First().Actividad + "</br>" + "Cliente: " + list.First().nombre.ToString().Split('-')[2] + "</br>" + "Dirección: " + direcciones + "</br>" + "Fecha: " + list.First().inicioEjecucion;
                }
                else
                {
                    estatus = "Terminación de Actividad </br>" + "Actividad: " + list.First().Actividad + "</br>" + "Cliente: " + list.First().nombre.ToString().Split('-')[2] + "</br>" + "Dirección: " + direcciones + "</br>" + "Fecha: " + list.First().finalEjecucion;
                }
                jsonFeature = "{\"type\":\"Feature\",\"geometry\":{\"type\":\"Point\",\"coordinates\":[" + List[i].Latitud + "," + List[i].Longitud + "]},\"properties\":{\"title\":\"" + estatus + "\"}}";
                strUbicaciones = strUbicaciones + jsonFeature;
            }

            strUbicaciones = strUbicaciones.PadRight(strUbicaciones.Length - 1);
        }



        public void direccion(string latitud, string longitud, ref string direccion)
        {
            try
            {
                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                string URL = "https://maps.google.com/maps/api/geocode/json?latlng=" + latitud + "," + longitud + "&sensor=false&key=AIzaSyAnDl3nkqqzLL07fy4gWQI2Wmb_Sey7Ik8"; ;
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(URL);
                var httpResponse = client.PostAsync(URL, null).Result;
                if (httpResponse.Content != null)
                {
                    var responseContent = httpResponse.Content.ReadAsStringAsync();
                    dynamic obj = JsonConvert.DeserializeObject<dynamic>(responseContent.Result);
                    direccion = obj.results[0].formatted_address;
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}