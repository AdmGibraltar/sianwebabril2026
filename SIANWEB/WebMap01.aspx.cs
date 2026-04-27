using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaEntidad;

using CapaDatos;
using CapaNegocios;
using System.Threading;
using System.IO;
using System.Text;


namespace SIANWEB
{
    public partial class WebMap01 : System.Web.UI.Page
    {
        public string Origen = "";
        public string MarcadorOrigen = "";
        public string DetalleRuta = "";
        public string FinalDeRuta = "";
        public string Direcciones = "";
        public string Direcciones2 = "";
        public string Direcciones3 = "";
        public string Direcciones4 = "";
        public string Direcciones5 = "";
        public string Direcciones6 = "";
        public string Direcciones7 = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                if (Sesion == null)
                {
                    string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);
                    Session["dir" + Session.SessionID] = pag[pag.Length - 1];
                    Response.Redirect("Login.aspx", false);
                }
                else
                {
                    ObtieneCoordenadasOrigen();
                    if (!Page.IsPostBack)
                    {

                    }
                }
            }
            catch (Exception ex)
            {
                this.ErrorManager(ex, "Page_Load");
            }

        }


        #region Funciones

        protected void cmbClientesSelected(object sender, EventArgs e)
        {
            ObtieneDirecciones(-1);
            this.cmbRutasCliente.Items.Clear();
            ObtieneRutas();
        }

        protected void cmbRutasClienteSelected(object sender, EventArgs e)
        {
            ObtieneDetalleRuta();
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                string rutaId = Request.Form[hfRuta.UniqueID];
                Sesion session = (Sesion)Session["Sesion" + Session.SessionID];
                CN_Maps clsMapa = new CN_Maps();
                Maps Mapa = new Maps();

                Mapa.Id_Ruta = 0;
                Mapa.Ruta = this.txtNombreRuta.Text;
                Mapa.Id_Cd = session.Id_Cd;
                Mapa.Id_Emp = session.Id_Emp;
                Mapa.Id_Cliente = Convert.ToInt32(this.cmbClientes.SelectedValue);
                clsMapa.GrabaRuta(ref Mapa, session.Emp_Cnx);

                int ori = 0;
                int uno = 0;
                string[] coor;
                string ruta = this.address.Text;
                char[] delimit1 = { '|' };
                string[] segmentos = ruta.Split(delimit1);
                foreach (string seg in segmentos)
                {
                    if (seg.Length > 0)
                    {

                        char[] delimit2 = { ';' };
                        string[] parte = seg.Split(delimit2);
                        uno = 0;
                        ori = 0;
                        foreach (string coorde in parte)
                        {
                            if (coorde.Contains('(') == true)
                            {
                                // es alguna de las coordenadas
                                if (ori == 0)
                                {
                                    ori = 1;
                                    coor = coorde.Replace('(', ' ').Replace(')', ' ').Split(',');
                                    Mapa.LatOrigen = Convert.ToDecimal(coor[0]);
                                    Mapa.LngOrigen = Convert.ToDecimal(coor[1]);
                                }
                                else
                                {
                                    coor = coorde.Replace('(', ' ').Replace(')', ' ').Split(',');
                                    Mapa.LatDestino = Convert.ToDecimal(coor[0]);
                                    Mapa.LngDestino = Convert.ToDecimal(coor[1]);
                                }
                            }
                            else
                            {
                                if (uno == 0)
                                {
                                    // es el numero de segmento
                                    uno = 1;
                                    Mapa.Segmento = Convert.ToInt32(coorde);
                                }
                                else
                                {
                                    // es el kilometraje
                                    Mapa.Kilometros = Convert.ToInt32(coorde);
                                }
                            }
                        }
                        //  aqui se manda a grabar
                        clsMapa.GrabaSegmento(Mapa, session.Emp_Cnx);
                    }
                }
                //avisa que se grabo la ruta
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        #endregion


        #region Procesos

        public void ObtieneCoordenadasOrigen()
        {
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
            CN_Maps clsMapa = new CN_Maps();

            Maps Mapa1 = new Maps();
            Mapa1.Id_Cd = sesion.Id_Cd;
            Mapa1.Id_Emp = sesion.Id_Emp;
            clsMapa.ObtieneOrigen(ref Mapa1, sesion.Emp_Cnx);

            Origen = "{ lat: " + Mapa1.Latitud.ToString() + ", lng: " + Mapa1.Longitud.ToString() + " }";
            MarcadorOrigen = "addMarker({ lat: " + Mapa1.Latitud.ToString() + ", lng: " + Mapa1.Longitud.ToString() + " }, map, '" + Mapa1.Sucursal + "');";
        }

        public void ObtieneDirecciones(int Dirr)
        {

            string vari = "";
            cmbDomicilios.Items.Clear();
            System.Collections.Generic.List<Comun> Lista = new System.Collections.Generic.List<Comun>();
            string[,] direcciones = new string[9, 2];
            if (this.cmbClientes.SelectedValue == "1")
            {
                direcciones = new string[9, 2] {
                    {"Home Depot Revolucion", "Av. Revolución 2096, Fracc. Estadio, 64830, Monterrey" },
                    {"Home Depot Cumbres", "Paseo De Los Triunfadores 3200, , 64610, Monterrey" },
                    {"Home Depot Lazaro Cardenas", "Av. Lázaro Cárdenas 2800 , 64790, Monterrey" },
                    {"Home Depot Serena", "Plaza Serena Carretera Nacional, 500, , 64989, Monterrey" },
                    {"Home Depot Fundadores", "Av. Garza Lagüera (Antes Av. Fundadores) 101 , 66269, San Pedro Garza García" },
                    {"Home Depot Miguel Aleman", "Av. Miguel Alemán , 66447, San Nicolás De Los Garza" },
                    {"Home Depot Sendero Nte", "Sendero Norte 1502, , 66059, General Escobedo" },
                    {"Home Depot Eloy Cavazos", "Av. Eloy Cavazos 7800, , 67150, Guadalupe (Nuevo León)" },
                    {"Home Depot Santa Catarina","Blvd. Díaz Ordaz 100, , 66100, Santa Catarina (Nuevo León)" }
                };
            }

            if (this.cmbClientes.SelectedValue == "2")
            {
                direcciones = new string[4, 2] {
                    {"Johnson Controls Mitras", "Carretera km 9.5 C.P. 66000, Calle A, Monterrey" },
                    {"Johnson Controls Centro", "Olga Sánchez de Hinojosa SN, Centro, 64720 Monterrey, Nuevo León" },
                    {"Johnson Controls Sn Pedro", "David Alfaro Siqueiros 104 Piso 1, San Pedro Garza García, 66269 Monterrey, N.L." },
                    {"Johnson Controls Coorporativo", "Valle del Campestre, 66266 San Pedro Garza García, N.L." }
                };
            }

            if (this.cmbClientes.SelectedValue == "3")
            {
                direcciones = new string[3, 2] {
                    {"CERVECERIA CUAUHTEMOC MOCTEZUMA", "Av. Miguel Alemán #1000, Valle de Huinala, 66633 Cd Apodaca, N.L." },
                    {"CERVECERIA CUAUHTEMOC MOCTEZUMA", "Gustavo Díaz Ordaz 320, Cuatro de Octubre, 64650 Guadalupe, Nuevo León" },
                    {"CERVECERIA CUAUHTEMOC MOCTEZUMA", "Av. Alfonso Reyes Norte 2202, Bella Vista, 64410 Monterrey, N.L." }
                };
            }

            if (this.cmbClientes.SelectedValue == "4")
            {
                direcciones = new string[2, 2] {
                    {"MITSUBA Sabinas", "Av. Industria Eléctrica Mz. 6, Lote 10, Zona Parque Industrial, 65260 Sabinas Hidalgo, N.L." },
                    {"MITSUBA Apodaca", "Antiguo Camino A. Huinala 210, Encinos Residencial, Cd Apodaca, N.L." }
                };
            }

            for (int i = 0; i < direcciones.GetLength(0); i++)
            {
                vari = " address" + i.ToString();
                /*
                var address0 = 'Paseo De Los Triunfadores 3200, , 64610, Monterrey';
                geocoder.geocode({ 'address': address0 }, function (results, status)
                  {addMarker( results[0].geometry.location , map, 'HD Cumbres');
                  });
                */
                Direcciones = Direcciones + " var " + vari + " = '" + direcciones[i, 1] + "';  geocoder.geocode({ 'address': " + vari + " }, function (results, status) { addMarker( results[0].geometry.location , map, '" + direcciones[i, 0] + "');    ";

                //if (Dirr == i)
                //{
                //    Direcciones = Direcciones + "directionsService.route({origin: { lat: 25.712888, lng: -100.305081 }, destination: results[0].geometry.location, travelMode: 'DRIVING' }, function(response, status) { directionsDisplay.setDirections(response); });";
                //}

                Direcciones = Direcciones + " });";

                Comun com = new Comun();
                com.Id = i;
                com.Descripcion = direcciones[i, 0];
                com.IdStr2 = direcciones[i, 1];
                Lista.Add(com);
            }

            cmbDomicilios.DataSource = Lista;
            cmbDomicilios.DataValueField = "IdStr2";
            cmbDomicilios.DataTextField = "Descripcion";
            cmbDomicilios.DataBind();

        }


        public void ObtieneRutas()
        {
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
            CN_Maps clsMapa = new CN_Maps();

            Maps Mapa1 = new Maps();
            Mapa1.Id_Cd = sesion.Id_Cd;
            Mapa1.Id_Emp = sesion.Id_Emp;
            Mapa1.Id_Cliente = Convert.ToInt32(this.cmbClientes.SelectedValue);
            List<Maps> Mapa2 = new List<Maps>();
            clsMapa.ObtieneRutasCliente(Mapa1, ref Mapa2, sesion.Emp_Cnx);
            //  ObtieneRutasCliente

            cmbRutasCliente.DataSource = Mapa2;
            cmbRutasCliente.DataValueField = "Id_Ruta";
            cmbRutasCliente.DataTextField = "Ruta";
            cmbRutasCliente.DataBind();
            cmbRutasCliente.Items.Insert(0, new ListItem("-- Seleccionar --", "-1"));
            txtNumRuta.Text = cmbRutasCliente.Items.Count.ToString();

        }

        public void ObtieneDetalleRuta()
        {
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
            CN_Maps clsMapa = new CN_Maps();

            Maps Mapa1 = new Maps();
            Mapa1.Id_Cd = sesion.Id_Cd;
            Mapa1.Id_Emp = sesion.Id_Emp;
            Mapa1.Id_Cliente = Convert.ToInt32(this.cmbClientes.SelectedValue);
            Mapa1.Id_Ruta = Convert.ToInt32(this.cmbRutasCliente.SelectedValue);
            clsMapa.ObtieneDetalleRuta(Mapa1, ref DetalleRuta, ref FinalDeRuta, sesion.Emp_Cnx);
            //  ObtieneRutasCliente

        }

        #endregion


        #region ErrorManager

        private void Alerta(string mensaje)
        {
            try
            {
                RAM1.ResponseScripts.Add("radalert('" + mensaje + "', 330, 150);");
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "Alerta");
            }
        }

        private void ErrorManager()
        {
            try
            {
                this.lblMensaje.Text = "";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ErrorManager(string Message)
        {
            try
            {
                this.lblMensaje.Text = Message;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ErrorManager(Exception eme, string NombreFuncion)
        {
            try
            {
                this.lblMensaje.Text = "Error: [" + NombreFuncion + "] " + eme.Message.ToString();

            }
            catch (Exception ex)
            {
                this.lblMensaje.Text = "Error grave: " + eme.Message.ToString() + " --> " + ex.Message.ToString();
            }
        }

        #endregion
    }
}