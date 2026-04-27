using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


using CapaEntidad;
using CapaNegocios;
using CapaDatos;

namespace SIANWEB
{
    public partial class CatConfiguraOrdenesExtraordinarias : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                LlenarListado();
                lblMensaje.Text = "";   ///  "En caso de requerir ajustar los porcentajes, debe hacerse desde el modulo de SIANCentral.";
            }
        }


        void LlenarListado()
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                List<ConfiguraOCE> Lista = new List<ConfiguraOCE>();
                CN_ConfiguraOCE CN = new CN_ConfiguraOCE();

                CN.LlenaListadoConfiguraOCE(Sesion.Emp_Cnx, "spSelConfiguraOCE", Sesion.Id_Cd, ref Lista);
                if (Lista.Count() > 0)
                {
                    foreach (var lsItem in Lista)
                    {
                        switch (int.Parse(lsItem.IdCveConfiguraOCE))
                        {
                            case 1:
                                this.hdfConfigura01.Value = lsItem.IdConfiguraOCE.ToString();
                                this.lbl01.Text = lsItem.ConceptoConfiguraOCE;
                                this.txtProVta3Meses.Text = lsItem.ValorConfiguraOCE;
                                break;
                            case 2:
                                this.hdfConfigura02.Value = lsItem.IdConfiguraOCE.ToString();
                                this.lbl02.Text = lsItem.ConceptoConfiguraOCE;
                                this.txtProVta12Meses.Text = lsItem.ValorConfiguraOCE;
                                break;
                            case 3:
                                this.hdfConfigura03.Value = lsItem.IdConfiguraOCE.ToString();
                                this.lbl03.Text = lsItem.ConceptoConfiguraOCE;
                                this.txtDesvEst3Meses.Text = lsItem.ValorConfiguraOCE;
                                break;
                            case 4:
                                this.hdfConfigura04.Value = lsItem.IdConfiguraOCE.ToString();
                                this.lbl04.Text = lsItem.ConceptoConfiguraOCE;
                                this.txtDesvEst12Meses.Text = lsItem.ValorConfiguraOCE;
                                break;
                            case 5:
                                this.hdfConfigura05.Value = lsItem.IdConfiguraOCE.ToString();
                                this.lbl05.Text = lsItem.ConceptoConfiguraOCE;
                                this.txtDiasAcumCantOCEx.Text = lsItem.ValorConfiguraOCE;
                                break;
                            case 6:
                                this.hdfConfigura06.Value = lsItem.IdConfiguraOCE.ToString();
                                this.lbl06.Text = lsItem.ConceptoConfiguraOCE;
                                this.txtMesePicoMax.Text = lsItem.ValorConfiguraOCE;
                                break;
                            case 7:
                                //  Parametro 1
                                this.hdfConfigura07.Value = lsItem.IdConfiguraOCE.ToString();
                                this.lbl07.Text = lsItem.ConceptoConfiguraOCE;
                                this.drpCondicion1.SelectedValue = lsItem.Condicion0;
                                this.drpParam1Op1.SelectedValue = lsItem.Param01;
                                this.drpParam1Op2.SelectedValue = lsItem.Param02;
                                this.drpFactor1P1.SelectedValue = lsItem.Factor1;
                                this.drpFactor2P1.SelectedValue = lsItem.Factor2;
                                this.txtMultiplicadorP1O1.Text = lsItem.Multiplicador1;
                                this.drpFactor3P1.SelectedValue = lsItem.Factor3;
                                this.txtMultiplicadorP1O2.Text = lsItem.Multiplicador2;
                                break;
                            case 8:
                                //  Parametro 2
                                this.hdfConfigura08.Value = lsItem.IdConfiguraOCE.ToString();
                                this.lbl08.Text = lsItem.ConceptoConfiguraOCE;
                                this.drpCondicion2.SelectedValue = lsItem.Condicion0;
                                this.drpParam2Op1.SelectedValue = lsItem.Param01;
                                this.drpParam2Op2.SelectedValue = lsItem.Param02;
                                this.drpFactor1P2.SelectedValue = lsItem.Factor1;
                                this.drpFactor2P2.SelectedValue = lsItem.Factor2;
                                this.txtMultiplicadorP2O1.Text = lsItem.Multiplicador1;
                                this.drpFactor3P2.SelectedValue = lsItem.Factor3;
                                this.txtMultiplicadorP2O2.Text = lsItem.Multiplicador2;
                                break;
                            case 9:
                                //  Parametro 3
                                this.hdfConfigura09.Value = lsItem.IdConfiguraOCE.ToString();
                                this.lbl09.Text = lsItem.ConceptoConfiguraOCE;
                                this.drpCondicion3.SelectedValue = lsItem.Condicion0;
                                this.drpParam3Op1.SelectedValue = lsItem.Param01;
                                this.drpParam3Op2.SelectedValue = lsItem.Param02;
                                this.drpFactor1P3.SelectedValue = lsItem.Factor1;
                                this.drpFactor2P3.SelectedValue = lsItem.Factor2;
                                this.txtMultiplicadorP3O1.Text = lsItem.Multiplicador1;
                                this.drpFactor3P3.SelectedValue = lsItem.Factor3;
                                this.txtMultiplicadorP3O2.Text = lsItem.Multiplicador2;
                                break;
                            default:
                                break;
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
                ///     this.lblMensaje.Text = "Error, " + ex.Message;
            }

        }


        private bool VerificaDatos(ref List<ConfiguraOCE> lista)
        {
            bool bsalida = true;
            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];
            ConfiguraOCE item = new ConfiguraOCE();
            int i = 1;

            while (i <= 9)
            {
                item = new ConfiguraOCE();
                switch (i)
                {
                    case 1:
                        item.IdConfiguraOCE = int.Parse(this.hdfConfigura01.Value);
                        item.IdCveConfiguraOCE = i.ToString();
                        item.Id_Cd = Sesion.Id_Cd;
                        item.ConceptoConfiguraOCE = this.lbl01.Text;
                        item.ValorConfiguraOCE = this.txtProVta3Meses.Text;
                        break;
                    case 2:
                        item.IdConfiguraOCE = int.Parse(this.hdfConfigura02.Value);
                        item.IdCveConfiguraOCE = i.ToString();
                        item.Id_Cd = Sesion.Id_Cd;
                        item.ConceptoConfiguraOCE = this.lbl02.Text;
                        item.ValorConfiguraOCE = this.txtProVta12Meses.Text;
                        break;
                    case 3:
                        item.IdConfiguraOCE = int.Parse(this.hdfConfigura03.Value);
                        item.IdCveConfiguraOCE = i.ToString();
                        item.Id_Cd = Sesion.Id_Cd;
                        item.ConceptoConfiguraOCE = this.lbl03.Text;
                        item.ValorConfiguraOCE = this.txtDesvEst3Meses.Text;
                        break;
                    case 4:
                        item.IdConfiguraOCE = int.Parse(this.hdfConfigura04.Value);
                        item.IdCveConfiguraOCE = i.ToString();
                        item.Id_Cd = Sesion.Id_Cd;
                        item.ConceptoConfiguraOCE = this.lbl04.Text;
                        item.ValorConfiguraOCE = this.txtDesvEst12Meses.Text;
                        break;
                    case 5:
                        item.IdConfiguraOCE = int.Parse(this.hdfConfigura05.Value);
                        item.IdCveConfiguraOCE = i.ToString();
                        item.Id_Cd = Sesion.Id_Cd;
                        item.ConceptoConfiguraOCE = this.lbl05.Text;
                        item.ValorConfiguraOCE = this.txtDiasAcumCantOCEx.Text;
                        break;
                    case 6:
                        item.IdConfiguraOCE = int.Parse(this.hdfConfigura06.Value);
                        item.IdCveConfiguraOCE = i.ToString();
                        item.Id_Cd = Sesion.Id_Cd;
                        item.ConceptoConfiguraOCE = this.lbl06.Text;
                        item.ValorConfiguraOCE = this.txtMesePicoMax.Text;
                        break;
                    case 7:
                        //  Parametro 1
                        item.IdConfiguraOCE = int.Parse(this.hdfConfigura07.Value);
                        item.IdCveConfiguraOCE = i.ToString();
                        item.Id_Cd = Sesion.Id_Cd;
                        item.ConceptoConfiguraOCE = this.lbl07.Text;
                        item.ValorConfiguraOCE = "";
                        item.Condicion0 = this.drpCondicion1.SelectedValue;
                        item.Param01 = this.drpParam1Op1.SelectedValue;
                        item.Param02 = this.drpParam1Op2.SelectedValue;
                        item.Factor1 = this.drpFactor1P1.SelectedValue;
                        item.Factor2 = this.drpFactor2P1.SelectedValue;
                        item.Multiplicador1 = this.txtMultiplicadorP1O1.Text;
                        item.Factor3 = this.drpFactor3P1.SelectedValue;
                        item.Multiplicador2 = this.txtMultiplicadorP1O2.Text;
                        break;
                    case 8:
                        //  Parametro 2
                        item.IdConfiguraOCE = int.Parse(this.hdfConfigura08.Value);
                        item.IdCveConfiguraOCE = i.ToString();
                        item.Id_Cd = Sesion.Id_Cd;
                        item.ConceptoConfiguraOCE = this.lbl08.Text;
                        item.ValorConfiguraOCE = "";
                        item.Condicion0 = this.drpCondicion2.SelectedValue;
                        item.Param01 = this.drpParam2Op1.SelectedValue;
                        item.Param02 = this.drpParam2Op2.SelectedValue;
                        item.Factor1 = this.drpFactor1P2.SelectedValue;
                        item.Factor2 = this.drpFactor2P2.SelectedValue;
                        item.Multiplicador1 = this.txtMultiplicadorP2O1.Text;
                        item.Factor3 = this.drpFactor3P2.SelectedValue;
                        item.Multiplicador2 = this.txtMultiplicadorP2O2.Text;
                        break;
                    case 9:
                        //  Parametro 3
                        item.IdConfiguraOCE = int.Parse(this.hdfConfigura09.Value);
                        item.IdCveConfiguraOCE = i.ToString();
                        item.Id_Cd = Sesion.Id_Cd;
                        item.ConceptoConfiguraOCE = this.lbl09.Text;
                        item.ValorConfiguraOCE = "";
                        item.Condicion0 = this.drpCondicion3.SelectedValue;
                        item.Param01 = this.drpParam3Op1.SelectedValue;
                        item.Param02 = this.drpParam3Op2.SelectedValue;
                        item.Factor1 = this.drpFactor1P3.SelectedValue;
                        item.Factor2 = this.drpFactor2P3.SelectedValue;
                        item.Multiplicador1 = this.txtMultiplicadorP3O1.Text;
                        item.Factor3 = this.drpFactor3P3.SelectedValue;
                        item.Multiplicador2 = this.txtMultiplicadorP3O2.Text;
                        break;
                }
                lista.Add(item);
                i++;
            }

            return bsalida;
        }

        protected void btnActualiza_Click(object sender, EventArgs e)
        {
            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];
            List<ConfiguraOCE> Lista = new List<ConfiguraOCE>();
            CN_ConfiguraOCE CN = new CN_ConfiguraOCE();

            int ii = 0;
            int iii = 0;
            List<ConfiguraOCE> listado = new List<ConfiguraOCE>();
            if (VerificaDatos(ref listado))
            {
                foreach (ConfiguraOCE iitem in listado)
                {
                    CN.ActualizaConfiguraOCE(Sesion.Emp_Cnx, "spInsConfiguraOCE", iitem, ref ii);
                    iii = iii + ii;
                }
                //      ii vs listado.count
                if (iii == listado.Count())
                {
                    this.lblMensaje.Text = "Ajustes Grabados!!";
                }
            }
        }
    }
}