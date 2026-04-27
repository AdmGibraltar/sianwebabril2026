using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaEntidad;
using CapaDatos;
using CapaNegocios;

namespace SIANWEB
{
    public partial class ObtenerNombreCliente : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                string valor_retorno = "";

                if (Request.Params["ini"] != null || Sesion == null)
                {
                    valor_retorno = "-0";
                }
                else
                {
                    try
                    {
                        int Cte = int.Parse(Request.Params["cte"]);
                        int.TryParse(Request.Params["tipoMov"], out int tipoMov);

                        Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];

                        Clientes clientes = new Clientes
                        {
                            Id_Emp = sesion.Id_Emp,
                            Id_Cd = sesion.Id_Cd_Ver,
                            Id_Cte = Cte,
                            Ignora_Inactivo = false
                        };

                        int TieneClientesDirectosSP = 0;
                        new CN_CatCliente().ConsultaClientesDirectosSP(ref clientes, ref TieneClientesDirectosSP, sesion.Emp_Cnx);

                        if (tipoMov > 0 && tipoMov == 6)
                        {
                            if (TieneClientesDirectosSP == 1 && clientes.Id_Cte != -1)
                            {
                                valor_retorno = "-1@@Este cliente solo puede hacer devolucion de comodatos con el tipo de movimiento 27";
                            }
                        }

                        if (tipoMov > 0 && tipoMov == 15)
                        {
                            if (TieneClientesDirectosSP == 1 && clientes.Id_Cte != -1)
                            {
                                valor_retorno = "-1@@Este cliente solo puede hacer cancelaciones de aparatos en comodatos con el tipo de movimiento 28";
                            }
                        }

                        if (tipoMov > 0 && tipoMov == 27)
                        {

                            if (TieneClientesDirectosSP == 0 && clientes.Id_Cte != -1)
                            {
                                valor_retorno = "-1@@Este cliente solo puede hacer devolucion de comodatos con el tipo de movimiento 6";
                            }
                        }

                        if (tipoMov > 0 && tipoMov == 28)
                        {

                            if (TieneClientesDirectosSP == 0 && clientes.Id_Cte != -1)
                            {
                                valor_retorno = "-1@@Este cliente solo puede hacer cancelaciones de aparatos en comodatos con el tipo de movimiento 15";
                            }
                        }

                        if (tipoMov > 0 && tipoMov == 83)
                        {
                            if (TieneClientesDirectosSP == 0 && clientes.Id_Cte != -1)
                            {
                                valor_retorno = "-1@@Este cliente no tiene permisos para hacer baja de refacciones clientes directos";
                            }
                        }

                        if (valor_retorno == "")
                        {
                            CN_CatCliente clsCliente = new CN_CatCliente();
                            try
                            {
                                clsCliente.ConsultaClientes(ref clientes, sesion.Emp_Cnx);
                                valor_retorno = clientes.Cte_NomComercial;
                            }
                            catch (Exception ex)
                            {
                                valor_retorno = "-1@@" + ex.Message;
                            }
                        }
                    }
                    catch
                    {
                        valor_retorno = "";
                    }
                }

                Response.Write(valor_retorno);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}