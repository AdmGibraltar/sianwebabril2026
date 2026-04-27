using CapaEntidad;
using CapaNegocios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SIANWEB
{
    public partial class VentanaFacturasMotivo : System.Web.UI.Page
    {
        public static string param { get; set; }

        public enum enumTipoMotivo
        {           
            RefacturarParcial=0,
            RefacturarTotal= 1,
            Baja = 2
        }

        #region Metodos
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                     
                    CN_CapFactura cnFactura = new CN_CapFactura();
                    Factura entFactura = new Factura();
                    List<Comun> lstMotivo = new List<Comun>();
                    Sesion session = new Sesion();
                    session = (Sesion)Session["Sesion" + Session.SessionID];
                    int intTipoMotivo = (int)enumTipoMotivo.Baja;
                    cnFactura.ConsultarMotivo(session.Emp_Cnx, intTipoMotivo, ref lstMotivo);

                    this.cmbMotivo.DataSource = lstMotivo;
                    this.cmbMotivo.DataTextField = "Descripcion";
                    this.cmbMotivo.DataValueField = "Id";
                    this.cmbMotivo.DataBind();
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
          
                
        }
        
        protected void btnBaja_Click(object sender, EventArgs e)
        {
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
            CN_CapFactura cnFactura = new CN_CapFactura();
            bool boolFolioValido = false;
            string strFolio = string.Empty;
            string strMensaje = string.Empty;
            int Id_Fac_Reemplaza = 0;
            int Id_MotivoBaja = int.Parse(cmbMotivo.SelectedValue);
            string strMotivo = cmbMotivo.SelectedItem.Text;

            int intIdEmp = int.Parse(Page.Request.QueryString["Id_Emp"]);
            int intIdCd = int.Parse(Page.Request.QueryString["Id_Cd"]);
            int intIdFac = int.Parse(Page.Request.QueryString["Id_Fac"]);
            int intNotificarCorreo = int.Parse(Page.Request.QueryString["NotificarCorreo"]);
            lblMensajeBaja.Text = "";
            if (Id_MotivoBaja <= 0){
                // mensaje: Seleccione el motivo 
                //Alerta("Seleccione el motivo de baja.");
                lblMensajeBaja.Text = "Seleccione el motivo de baja.";
                return;
            }

            if (!string.IsNullOrEmpty(txtFolio.Text))
            {
                strFolio = txtFolio.Text;
                boolFolioValido = int.TryParse(strFolio, out Id_Fac_Reemplaza);
                if (!boolFolioValido)
                {
                    // mensaje: Ingrese el numero de factura sin Serie.
                    //Alerta("Formato incorrecto,ingrese el numero de factura sin Serie.");
                    lblMensajeBaja.Text = "Formato incorrecto,ingrese el numero de factura sin Serie.";
                    return;
                }
                // inicializar para segunda la validacion
                boolFolioValido = false;
                // validar folio nuevo, cliente debe ser el mismo.
                cnFactura.ConsultarFacturaRelacionada(sesion.Emp_Cnx, intIdEmp, intIdCd, intIdFac, Id_Fac_Reemplaza, ref boolFolioValido, ref strMensaje);
                if (!boolFolioValido)
                {
                    // mensaje: strMensaje
                    Alerta(strMensaje);
                    lblMensajeBaja.Text = strMensaje;
                    return;
                }
            }

            cnFactura.GuardarMotivoCancelacionFactura(sesion.Emp_Cnx, intIdEmp, intIdCd, intIdFac,  Id_MotivoBaja,  Id_Fac_Reemplaza);
            if (intNotificarCorreo==0)
            {
                param = "CancelarFactura";
                func_cerrarventana(param);
            }
            else
            {
                param = "Eliminar";
                func_cerrarventana(param);
            }
        }

        protected void btnRefacturar_Click(object sender, EventArgs e)
        {
            param = "Refacturar";
            func_cerrarventana(param);
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            try
            {
                func_cerrarventana(param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        protected void func_cerrarventana(string param)
        {
            string funcion = "setTimeout(function() { CloseAndRebind('" + param + "'); }, 500);";
            string script = "<script>" + funcion + "</script>";
            ScriptManager.RegisterStartupScript(this, this.GetType(), funcion, script, false);
        }
        #endregion

        #region Funciones

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
                Alerta(Message);
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
                Alerta("Error: [" + NombreFuncion + "] " + eme.Message.ToString());
                //this.lblMensaje.Text = "Error: [" + NombreFuncion + "] " + eme.Message.ToString();

            }
            catch (Exception ex)
            {
                Alerta("Error grave: " + eme.Message.ToString() + " --> " + ex.Message.ToString());
                //this.lblMensaje.Text = "Error grave: " + eme.Message.ToString() + " --> " + ex.Message.ToString();
            }
        }
        #endregion

       
    }
}