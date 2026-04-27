using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Text;
using CapaEntidad;
using CapaNegocios;
using CapaDatos;
using System.Configuration;
namespace SIANWEB
{
    public partial class ProIncompleto_Picking : System.Web.UI.Page
    {
        #region Variables
        private bool _PermisoGuardar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoModificar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoEliminar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoImprimir { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        #endregion
        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                if (Sesion == null)
                    CerrarVentana();
                else
                {
                    if (!Page.IsPostBack)
                    {
                        if (Sesion.Cu_Modif_Pass_Voluntario == false)
                        {
                            RAM1.ResponseScripts.Add("AbrirContrasenas(); return false;");
                            return;
                        }
                        Inicializar();

                        double ancho = 0;

                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }


        protected void ImbConfirmar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if ((Convert.ToInt32(TxtNoEncontrado.Text) > 0) || (Convert.ToInt32(TxtNoConforme.Text) > 0))
                {

                    if ((Convert.ToInt32(TxtNoEncontrado.Text) > 0) && (Convert.ToInt32(TxtNoConforme.Text) > 0))
                    {


                        if (Convert.ToInt32(txtInventario.Text) < (Convert.ToInt32(TxtNoEncontrado.Text) + Convert.ToInt32(TxtNoConforme.Text)))
                        {
                            Alerta("Favor de revisar el Producto No Encontrado + No Conforme no puede ser mayor al Inventario");
                        }
                        else
                        {
                            InsertaRemision(61, Convert.ToInt64(txtProducto.Text), Convert.ToInt32(TxtNoEncontrado.Text));   /// No Encontrado

                            InsertaRemision(65, Convert.ToInt64(txtProducto.Text), Convert.ToInt32(TxtNoConforme.Text));   /// No Conforme

                            Alerta("Actualizado Producto No Encontrado y No Conforme");
                            CerrarVentana();

                        }

                    }
                    else
                    {
                        if (Convert.ToInt32(TxtNoEncontrado.Text) > 0)
                        {

                            if (Convert.ToInt32(txtInventario.Text) < Convert.ToInt32(TxtNoEncontrado.Text))
                            {
                                Alerta("Favor de revisar el Producto No Encontrado no puede ser mayor al Inventario");
                            }
                            else
                            {

                                InsertaRemision(61, Convert.ToInt64(txtProducto.Text), Convert.ToInt32(TxtNoEncontrado.Text));   /// No Encontrado

                                Alerta("Actualizado Producto No Encontrado");
                                CerrarVentana();

                            }
                        }

                        if (Convert.ToInt32(TxtNoConforme.Text) > 0)
                        {

                            if (Convert.ToInt32(txtInventario.Text) < Convert.ToInt32(TxtNoConforme.Text))
                            {
                                Alerta("Favor de revisar el Producto No Conforme no puede ser mayor al Inventario");
                            }
                            else
                            {
                                InsertaRemision(65, Convert.ToInt64(txtProducto.Text), Convert.ToInt32(TxtNoConforme.Text));   /// No Conforme
                                Alerta("Actualizado Producto No Conforme");
                                CerrarVentana();

                            }
                        }
                    }
                }
                else
                {
                    if ((Convert.ToInt32(TxtNoEncontrado.Text) == 0) && (Convert.ToInt32(TxtNoConforme.Text) == 0))
                    {
                        Alerta("Favor de Capturar la Cantidad del producto No Encontrado o No Conforme");
                    }
                    else
                    {
                        if (Convert.ToInt32(TxtNoEncontrado.Text) == 0)
                        {
                            Alerta("Favor de Capturar la Cantidad del producto No Encontrado");
                        }
                        if (Convert.ToInt32(TxtNoConforme.Text) == 0)
                        {
                            Alerta("Favor de Capturar la Cantidad del producto No Conforme");
                        }
                    }
                }



            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
                //throw ex;
            }

        }


        public void ConfirmarPedido(Int64 Id_Prd, String Ruta, String Credito, String Parcialidades, String TipoPedido)
        {
            try
            {
                Sesion sesion = new Sesion();
                sesion = (Sesion)Session["Sesion" + Session.SessionID];

                CN_CapPedido cn_cappedido = new CN_CapPedido();

                PedidoDet ped = new PedidoDet();
                ped.Id_Emp = sesion.Id_Emp;
                ped.Id_Cd = sesion.Id_Cd_Ver;
                ped.Id_Prd = Id_Prd;
                ped.Ruta = Ruta;
                ped.CreditoStr = Credito;
                ped.Ped_PermiteParcialidades = Parcialidades;
                ped.TipoPedido = TipoPedido;

                int verificador = 0;

                cn_cappedido.ConfirmarPedido(ped, sesion.Emp_Cnx, ref verificador);

                if (verificador == 0)
                {
                    Alerta("Las cantidades fueron actualizadas correctamente.");
                }

            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
                //throw ex;
            }
        }


        protected void rtb1_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            try
            {
                ErrorManager();
                RadToolBarButton btn = e.Item as RadToolBarButton;
                if (btn.CommandName == "save")
                {
                    Guardar();
                }
                else if (btn.CommandName == "new")
                {
                    //Nuevo();
                }
                else if (btn.CommandName == "undo")
                {
                    CerrarVentana();
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "rtb1_ButtonClick");
            }
        }
        #endregion
        #region funciones
        private void CerrarVentana()
        {
            try
            {
                string funcion;
                if (this.HiddenRebind.Value == "0")
                    funcion = "CloseWindow()";
                else
                    funcion = "CloseAndRebind()";
                string script = "<script>" + funcion + "</script>";
                ScriptManager.RegisterStartupScript(this, this.GetType(), funcion, script, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        private void Inicializar()
        {
            HF_Ped.Value = Request.QueryString["Id_Prd"].ToString();
            txtProducto.Text = Request.QueryString["Id_Prd"].ToString();




            txtAsignado.Text = Request.QueryString["CantidadAsignada"].ToString();

            txtDisponible.Text= Request.QueryString["CantidadDisponible"].ToString();

            txtInventario.Text = (Convert.ToInt32(txtAsignado.Text) + Convert.ToInt32(txtDisponible.Text)).ToString();



            TxtNoEncontrado.Text = "0";
            TxtNoConforme.Text = "0";

            // Request.QueryString["Prd_Nom"].ToString();

            CN_CatProducto cn_producto = new CN_CatProducto();

            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];
            Producto prd = new Producto();
            cn_producto.ConsultaProducto(ref prd, Sesion.Emp_Cnx, Sesion.Id_Emp, Sesion.Id_Cd_Ver, Convert.ToInt64(HF_Ped.Value),0);
            txtProductoNombre.Text = prd.Prd_Descripcion;
            TxtPresentacion.Text = prd.Prd_Presentacion;
            TxtUnidades.Text = prd.Prd_UniNe;

            _PermisoGuardar = Convert.ToBoolean(Request.QueryString["PermisoGuardar"]);
            _PermisoModificar = Convert.ToBoolean(Request.QueryString["PermisoModificar"]);
            _PermisoEliminar = Convert.ToBoolean(Request.QueryString["PermisoEliminar"]);
            _PermisoImprimir = Convert.ToBoolean(Request.QueryString["PermisoImprimir"]);
            //rgPedido.Rebind();
            ValidarPermisos();
        }
        private float obtenerPrecioAAA(long Id_Prd)
        {
            try
            {
                float precio = 0;
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CN_ProductoPrecios cn_proprec = new CN_ProductoPrecios();
                int Id_Pre = 0;
                cn_proprec.ConsultaListaProductoPrecioAAA(ref precio, Sesion, Id_Prd, ref Id_Pre);
                return precio;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void InsertaRemision(int Id_Tm,Int64 Id_Prd ,int Cantidad)
        {
            int Id_Rem = -1;
            int verificador = -1;
            bool tipoMovimento = false;
            string mensaje = "";
            Remision remision = new Remision();
        Funciones funcion = new Funciones();

            Sesion sesion = new Sesion();
            sesion = (Sesion)Session["Sesion" + Session.SessionID];
            remision.Id_Emp = sesion.Id_Emp;
            remision.Id_Cd = sesion.Id_Cd_Ver;
            remision.Id_Rem =  -1;
            remision.Rem_ManAut = 1; // manual
            remision.Rem_Tipo = "3"; // 3=remision
            remision.Rem_Fecha = DateTime.Parse(DateTime.Now.ToString("dd/MM/yyyy") + " " + funcion.GetLocalDateTime(sesion.Minutos).ToString("HH:mm:ss"));
            remision.Id_Tm = Id_Tm;
            remision.Id_Ped = -1;
            remision.Id_Cte = 100;
            remision.Id_Ter = 50701011;
            remision.Id_Rik = 100;
            remision.Id_U = sesion.Id_U;
            remision.Rem_Calle = "";
            remision.Rem_Numero = "";
            remision.Rem_Cp = "";
            remision.Rem_Colonia = "";
            remision.Rem_Municipio = "";
            remision.Rem_Estado = "";
            remision.Rem_Rfc = "";
            remision.Rem_Telefono = "";
            remision.Rem_Contacto = "";
            remision.Rem_Conducto = "";
            remision.Rem_Guia = "";
            remision.Rem_FechaEntrega = DateTime.Parse(DateTime.Now.ToString("dd/MM/yyyy") + " " + funcion.GetLocalDateTime(sesion.Minutos).ToString("HH:mm:ss"));
            remision.Rem_Nota = "Movimiento generado desde el Picking en forma automática";
            remision.Rem_Estatus = "C";
            remision.Rem_OrdenCompra = "";
            remision.Id_Vap = 0;
            remision.Rem_CteCuentaNacional = 0;
            remision.Rem_CteCuentaContNacional = 0;
            List<RemisionDet> detalles = new List<RemisionDet>();
            RemisionDet remdetalle = new RemisionDet();
            remdetalle = new RemisionDet();
            remdetalle.Id_Emp = sesion.Id_Emp;
            remdetalle.Id_Cd = sesion.Id_Cd_Ver;
            remdetalle.Id_RemDet = 1;
            remdetalle.Id_Ter = 50701011;
            remdetalle.Id_Prd = Id_Prd;
            remdetalle.Rem_Cant = Cantidad;
            remdetalle.Rem_Precio = obtenerPrecioAAA(Id_Prd);

            remision.Rem_Subtotal = Cantidad * remdetalle.Rem_Precio;// subtotal;
            remision.Rem_Total = (Cantidad * remdetalle.Rem_Precio) * 1.16;// total;
            remision.Rem_Iva = remision.Rem_Total- remision.Rem_Subtotal;// IVA;

            detalles.Add(remdetalle);

            try
            {
                new CN_CapRemision().GuardarRemision(remision, detalles, sesion, ref verificador, false, false, ref Id_Rem, ref tipoMovimento, ref mensaje,"", ConfigurationManager.AppSettings["PermitePrecios0Remision"].ToString());
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("saldo_insuficiente") || ex.Message.Contains("error"))
                {
                    Alerta(ex.Message.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries)[0]);
                    return;
                }
                else
                {
                    if (ex.Message.Contains("no cuenta con inventario suficiente"))
                    {
                        Alerta(ex.ToString());
                    }
                    else
                    {
                        throw ex;
                    }
                }
            }
            if (!string.IsNullOrEmpty(mensaje))
            {
                Alerta(mensaje);
                return;
            }
        }

private void Guardar()
        {
            try
            {
                if ((Convert.ToInt32(TxtNoEncontrado.Text) > 0) || (Convert.ToInt32(TxtNoConforme.Text) > 0))
                {

                    if ((Convert.ToInt32(TxtNoEncontrado.Text) > 0) && (Convert.ToInt32(TxtNoConforme.Text) > 0))
                    {


                        if (Convert.ToInt32(txtInventario.Text) < (Convert.ToInt32(TxtNoEncontrado.Text) + Convert.ToInt32(TxtNoConforme.Text)))
                        {
                            Alerta("Favor de revisar el Producto No Encontrado + No Conforme no puede ser mayor al Inventario");
                        }
                        else
                        {
                            InsertaRemision(61, Convert.ToInt64(txtProducto.Text) , Convert.ToInt32(TxtNoEncontrado.Text));   /// No Encontrado

                            InsertaRemision(65, Convert.ToInt64(txtProducto.Text), Convert.ToInt32(TxtNoConforme.Text));   /// No Conforme

                            Alerta("Actualizado Producto No Encontrado y No Conforme");
                            CerrarVentana();

                        }

                    }
                    else
                    {
                        if (Convert.ToInt32(TxtNoEncontrado.Text) > 0)
                        {

                            if (Convert.ToInt32(txtInventario.Text) < Convert.ToInt32(TxtNoEncontrado.Text))
                            {
                                Alerta("Favor de revisar el Producto No Encontrado no puede ser mayor al Inventario");
                            }
                            else
                            {

                                InsertaRemision(61, Convert.ToInt64(txtProducto.Text), Convert.ToInt32(TxtNoEncontrado.Text));   /// No Encontrado

                                Alerta("Actualizado Producto No Encontrado");
                                CerrarVentana();

                            }
                        }

                        if (Convert.ToInt32(TxtNoConforme.Text) > 0)
                        {

                            if (Convert.ToInt32(txtInventario.Text) < Convert.ToInt32(TxtNoConforme.Text))
                            {
                                Alerta("Favor de revisar el Producto No Conforme no puede ser mayor al Inventario");
                            }
                            else
                            {
                                InsertaRemision(65, Convert.ToInt64(txtProducto.Text), Convert.ToInt32(TxtNoConforme.Text));   /// No Conforme
                                Alerta("Actualizado Producto No Conforme");
                                CerrarVentana();

                            }
                        }
                    }
                }
                else
                {
                    if ((Convert.ToInt32(TxtNoEncontrado.Text) == 0) && (Convert.ToInt32(TxtNoConforme.Text) == 0))
                    {
                        Alerta("Favor de Capturar la Cantidad del producto No Encontrado o No Conforme");
                    }
                    else
                    {
                        if (Convert.ToInt32(TxtNoEncontrado.Text) == 0)
                        {
                            Alerta("Favor de Capturar la Cantidad del producto No Encontrado");
                        }
                        if (Convert.ToInt32(TxtNoConforme.Text) == 0)
                        {
                            Alerta("Favor de Capturar la Cantidad del producto No Conforme");
                        }
                    }
                }



            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
                //throw ex;
            }

        }

        private void ValidarPermisos()
        {
            try
            {
                if (_PermisoGuardar == false & _PermisoModificar == false)
                    this.rtb1.Items[5].Visible = false;
                //Nuevo
                this.rtb1.Items[6].Visible = false;
                //Regresar
                this.rtb1.Items[4].Visible = false;
                //Eliminar
                this.rtb1.Items[3].Visible = false;
                //Imprimir
                this.rtb1.Items[2].Visible = false;
                //Correo
                this.rtb1.Items[1].Visible = false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region ErrorManager
        private void AlertaCerrar(string mensaje)
        {
            try
            {
                RAM1.ResponseScripts.Add("CloseAlert('" + mensaje + "');");
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "Alerta");
            }
        }
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