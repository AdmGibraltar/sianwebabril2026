using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using CapaEntidad;
using CapaDatos;
using System.Web;
using System.Net;
using System.Net.Mail;
using System.Net.NetworkInformation;
using System.Net.Mime;
using System.Configuration;
using System.Web;
using System.IO;

namespace CapaNegocios
{
    public class CN_ComprasLocales
    {
        public void ConsultaProducto(string txtsql, string Conexion, ref List<string> List)
        {
            try
            {
                CD_ComprasLocales claseComprasLocalescmb = new CD_ComprasLocales();

                claseComprasLocalescmb.ConsultaComprasCombo(txtsql, Conexion, ref List);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // Compras Locales
        // ENE21-2020 RFH 

        public int CompraLocalPedidosProducto_ChecaDuplicado(long CodigoUsadoProd, int Param2, string Conexion)
        {
            CD_ComprasLocales CD = new CD_ComprasLocales();
            return CD.CompraLocalPedidosProducto_ChecaDuplicado(CodigoUsadoProd, Param2, Conexion);
        }

        // Compras Locales
        // ENE21-2020 RFH 

        public string LlenarProdcutosHermanos(long Producto, string Conexion)
        {
            CD_ComprasLocales CD = new CD_ComprasLocales();
            return CD.CL_LlenarProdcutosHermanos(Producto, Conexion);
        }

        // Compras Locales
        // FEB13-2020 RFH 
        // Guarda Clientes Exclusivo
        // RBM  MARZO 2024
        // Se agrega TipoCliente para clientes exclusivos
        public int CL_InsertClienteExclusivo(int IdCte, string Nombre, long Id_Sol, string TipoCliente, string Conexion)
        {
            CD_ComprasLocales claseComprasLocalesgrd = new CD_ComprasLocales();
            return claseComprasLocalesgrd.CL_InsertClienteExclusivo(IdCte, Nombre, Id_Sol, TipoCliente, Conexion);
        }

        // Compras Locales
        // MAR4-2020 RFH 
        // Clientes Exclusivo

        public int CL_InsertClienteExclusivo_UpdateSol(
            string KeyArray_ClienteExclusivos, long Id_Solicitud, string Conexion)
        {
            CD_ComprasLocales CD = new CD_ComprasLocales();
            return CD.CL_InsertClienteExclusivo_UpdateSol(
                KeyArray_ClienteExclusivos, Id_Solicitud, Conexion);
        }


        public void ConsultarSolicitudes(CompraLocal cl, string Conexion, ref List<CompraLocal> List)
        {
            try
            {

                CD_ComprasLocales claseComprasLocalesgrd = new CD_ComprasLocales();
                claseComprasLocalesgrd.ConsultarSolicitudes(cl, Conexion, ref List);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaSolicitudCombo(int SolComId, long IdProd, int IdProv, string Conexion, ref List<CompraLocal> List)
        {
            try
            {
                CD_ComprasLocales claseComprasLocalesgrd = new CD_ComprasLocales();
                claseComprasLocalesgrd.ConsultaSolicitudCombo(SolComId, IdProd, IdProv, Conexion, ref List);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaSolicitudXProveedor(int IdProv, string Conexion, ref List<CompraLocal> List)
        {
            try
            {
                CD_ComprasLocales claseComprasLocalesgrd = new CD_ComprasLocales();
                claseComprasLocalesgrd.ConsultaSolicitudXProveedor(IdProv, Conexion, ref List);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaSolicitudXProducto(int IdProd, string Conexion, ref List<CompraLocal> List)
        {
            try
            {
                CD_ComprasLocales claseComprasLocalesgrd = new CD_ComprasLocales();
                claseComprasLocalesgrd.ConsultaSolicitudXProducto(IdProd, Conexion, ref List);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaSolicitudDirecta(int SolComId, string Conexion, ref List<CompraLocal> List)
        {
            try
            {
                CD_ComprasLocales claseComprasLocalesgrd = new CD_ComprasLocales();
                claseComprasLocalesgrd.ConsultaSolicitudUnica(SolComId, Conexion, ref List);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void GrabaSoloComentariosCliente(string comenta, int solicitud, string Conexion, ref int verifica)
        {
            CD_ComprasLocales CD = new CD_ComprasLocales();
            CD.GrabaSoloComentariosCliente(comenta, solicitud, Conexion, ref verifica);
        }
        public void DesAutorizaSolicitud(int solicitud, string Conexion, ref int verifica)
        {
            try
            {

                CD_ComprasLocales claseComprasLocalesgrd = new CD_ComprasLocales();
                claseComprasLocalesgrd.DesAutorizaSolicitud(solicitud, Conexion, ref verifica);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void GrabaComentariosCliente(string comenta, string fVige, int TipoSol, int solicitud, string Conexion, ref int verifica)
        {
            try
            {

                CD_ComprasLocales claseComprasLocalesgrd = new CD_ComprasLocales();
                claseComprasLocalesgrd.GrabaComentariosCliente(comenta, fVige, TipoSol, solicitud, Conexion, ref verifica);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void EliminaClientesExclusivos(int solicitud, string Conexion, ref int verifica)
        {
            try
            {

                CD_ComprasLocales claseComprasLocalesgrd = new CD_ComprasLocales();
                claseComprasLocalesgrd.EliminaClientesExclusivos(solicitud, Conexion, ref verifica);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void GrabaClientesExclusivos(long Prod, int Cliente, int solicitud, string Conexion, ref int verifica)
        {
            try
            {

                CD_ComprasLocales claseComprasLocalesgrd = new CD_ComprasLocales();
                claseComprasLocalesgrd.GrabaClientesExclusivos(Prod, Cliente, solicitud, Conexion, ref verifica);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void GrabaTipoCompraLocal(int solicitud, int tiposolicitud, string Conexion, ref int verifica)
        {
            CD_ComprasLocales claseComprasLocalesgrd = new CD_ComprasLocales();
            claseComprasLocalesgrd.GrabaTipoCompraLocal(solicitud, tiposolicitud, Conexion, ref verifica);
        }

        // Compras Locales 
        // 19FEB-2020 RFH

        public int GrabaTipoCompraLocal_ver2(
            int Id_Solicitud, int IdCausaDesabasto, string Comentarios, string Vigencia, int TipoSolicitud,
            string PedidoReferencia, string Conexion)
        {
            CD_ComprasLocales CD = new CD_ComprasLocales();
            return CD.GrabaTipoCompraLocal_ver2(
                Id_Solicitud, IdCausaDesabasto, Comentarios, Vigencia, TipoSolicitud, PedidoReferencia, Conexion);
        }

        // 17Mar2022 
        public void GrabaVigencia(string fVige, int solicitud, string Conexion, ref int verifica)
        {
            CD_ComprasLocales CD = new CD_ComprasLocales();
            CD.GrabaVigencia(fVige, solicitud, Conexion, ref verifica);
        }

        /*
         * ENE30-2020 RFH Compras Locales 
         *  SAT
         */

        public void GrabaDatosProductoSAT_ver2(
            int solicitud, long producto, string CUnidad, string CProducto, string Conexion)
        {
            CD_ComprasLocales CD = new CD_ComprasLocales();
            CD.GrabaDatosProductoSAT_ver2(solicitud, producto, CUnidad, CProducto, Conexion);
        }


        /*
         * ENE30-2020 RFH Compras Locales 
         * Envio Correo
         */

        public void Correo_SolicitudDeCompra(int TipoSolicitud, int SolFolio,
            int Id_Cd, string Cd_Nombre, int Id_U, string Emp_Nombre,
            string Id_Prv, string ProviderName, string CodProducto,
            string Prd_Descripcion, string TipoProducto, string Familia, string SubFamilia, string Vigencia, string MotivoDesabasto,
            string Conexion)
        {
            string TipoCompra = "";
            string Titulo = "";
            int famk = 0;

            switch (TipoSolicitud)
            {
                case 1:
                    TipoCompra = "OPCIÓN 1: POR DESABASTO";
                    Titulo = "Se ha colocado una <b>NUEVA SOLICITUD DE COMPRA LOCAL</b> con los siguientes datos:";
                    break;
                case 2:
                    TipoCompra = "OPCIÓN 2:";
                    Titulo = "Se ha colocado una <b>NUEVA SOLICITUD DE COMPRA LOCAL</b> con los siguientes datos:";
                    break;
                case 3:
                    TipoCompra = "OPCIÓN 3:";
                    Titulo = "Se ha <b>MODIFICADO la SOLICITUD DE COMPRA LOCAL</b> con los siguientes datos:";
                    break;
            }

            StringBuilder SB = new StringBuilder();

            SB.Append("<html xmlns='http://www.w3.org/1999/xhtml'>");
            SB.Append("<head>");
            SB.Append("<title></title>");
            SB.Append("</head>");
            SB.Append("<body>");

            SB.Append("<style type='text/css'>");
            SB.Append("tbl_Prec td, th  { border-collapse: collapse; border: 1px solid #6a737b;}");
            SB.Append("");
            SB.Append("");
            SB.Append("");
            SB.Append("");
            SB.Append("");
            SB.Append("</style>");

            SB.Append("<div align='center'>");
            SB.Append("<table width='700px'>");

            SB.Append("<tr>");
            SB.Append("<td colspan='2'>");
            SB.Append("<p>" + Titulo + "</p>");
            SB.Append("</td>");
            SB.Append("</tr>");

            //SB.Append("<tr>");
            //SB.Append("<td><b>Sufamilia del producto:</b></td>"); // *********************
            //SB.Append("<td>" + SubFmilia + "</td>");
            //SB.Append("</tr>");

            SB.Append("<tr>");
            SB.Append("<td><b>#Folio:</b></td>");
            SB.Append("<td>" + SolFolio.ToString() + "</td>");
            SB.Append("</tr>");

            SB.Append("<tr>");
            SB.Append("<td><b>Centro de distribución:</b></td>");
            SB.Append("<td>" + Id_Cd.ToString() + " - " + Cd_Nombre + "</td>");
            SB.Append("</tr>");

            SB.Append("<tr>");
            SB.Append("<td><b>Generado por:</b></td>");
            SB.Append("<td>" + Emp_Nombre + "</td>");
            SB.Append("</tr>");

            if (TipoSolicitud == 1 || TipoSolicitud == 2)
            {
                SB.Append("<tr>");
                SB.Append("<td><b>Tipo de compra local/:</b></td>");
                SB.Append("<td>" + TipoCompra + "</td>");
                SB.Append("</tr>");
            }

            SB.Append("<tr>");
            SB.Append("<td colspan='2'>&nbsp;</td>"); // ESPACIO 
            SB.Append("</tr>");

            SB.Append("<tr>");
            SB.Append("<td><b>Código de producto:</b></td>");
            SB.Append("<td>" + CodProducto + "</td>");
            SB.Append("</tr>");

            if (TipoSolicitud == 3)
            {
                SB.Append("<tr>");
                SB.Append("<td colspan='2'><b>Detalle de modificacion/:</b></td>");
                SB.Append("</tr>");
            }

            if (TipoSolicitud == 1 || TipoSolicitud == 2)
            {
                SB.Append("<tr>");
                SB.Append("<td><b>Proveedor:</b></td>");
                SB.Append("<td>" + ProviderName + "</td>");
                SB.Append("</tr>");

                SB.Append("<tr>");
                SB.Append("<td><b>Descripción del producto:</b></td>");
                SB.Append("<td>" + Prd_Descripcion + "</td>");
                SB.Append("</tr>");

                SB.Append("<tr>");
                SB.Append("<td><b>Tipo de producto:</b></td>");
                SB.Append("<td>" + TipoProducto + "</td>");
                SB.Append("</tr>");

                SB.Append("<tr>");
                SB.Append("<td><b>Familia de producto:</b></td>");
                SB.Append("<td>" + Familia + "</td>");
                SB.Append("</tr>");

                SB.Append("<tr>");
                SB.Append("<td><b>Sufamilia del producto:</b></td>"); // *********************
                SB.Append("<td>" + SubFamilia + "</td>");
                SB.Append("</tr>");

                SB.Append("<tr>");
                SB.Append("<td><b>Vigencia del uso del código:</b></td>");
                SB.Append("<td>" + Vigencia + "</td>");
                SB.Append("</tr>");

                SB.Append("<tr>");
                SB.Append("<td><b>Motivo del desabasto:</b></td>");
                SB.Append("<td>" + MotivoDesabasto + "</td>");
                SB.Append("</tr>");

                SB.Append("<tr>");
                SB.Append("<td colspan='2'>&nbsp;</td>"); // ESPACIO 
                SB.Append("</tr>");

            }

            // - - - - - - - - - - - - - - - - - - 
            // PRECIOS 
            // - - - - - - - - - - - - - - - - - - 

            long lCodProducto = 0;
            long.TryParse(CodProducto, out lCodProducto);

            List<eCL_ProductoPrecios> lstPrecios = new List<eCL_ProductoPrecios>();
            eCL_ProductoPrecios objPrec = new eCL_ProductoPrecios();

            CD_CatProducto CD_Prod_Precs = new CD_CatProducto();
            lstPrecios = CD_Prod_Precs.spSel_ProductoPrecios_CL_ver2(Sesion.Id_Emp, Sesion.Id_Cd, lCodProducto, Sesion.Emp_Cnx);

            SB.Append("<tr>");
            SB.Append("<td><b>Precios:</b></td>");
            SB.Append("<td>");

            SB.Append("<table width='400px' style='border-collapse: collapse; border: 1px solid #6a737b;'>");
            SB.Append("<tr>");
            SB.Append("<th style='text-align:center; border-collapse: collapse; border: 1px solid #6a737b; background-color: #faeb7f;'>Tipo de precio</th>");
            SB.Append("<th style='text-align:center; border-collapse: collapse; border: 1px solid #6a737b;'>Precio</th>");
            SB.Append("<tr>");

            int BG_Color = 0;

            foreach (eCL_ProductoPrecios Prec in lstPrecios)
            {
                if (BG_Color == 1)
                {
                    SB.Append("<tr>");
                    SB.Append("<td stype='border-collapse: collapse; border: 1px solid #6a737b; background-color: #6a737b;'>" + Prec.Prd_Descripcion + "</td>");
                    SB.Append("<td stype='border-collapse: collapse; border: 1px solid #6a737b; background-color: #6a737b; text-align:right;'>" + Prec.Prd_Pesos.ToString() + "</td>");
                    SB.Append("<tr>");
                    BG_Color = 0;
                }
                else
                {
                    SB.Append("<tr>");
                    SB.Append("<td stype='border-collapse: collapse; border: 1px solid #6a737b; background-color: #fff;'>" + Prec.Prd_Descripcion + "</td>");
                    SB.Append("<td stype='border-collapse: collapse; border: 1px solid #6a737b; background-color: #fff; text-align:right;'>" + Prec.Prd_Pesos.ToString() + "</td>");
                    SB.Append("<tr>");
                    BG_Color = 1;
                }
            }

            SB.Append("</table>");

            SB.Append("</td>");
            SB.Append("</tr>");

            SB.Append("<tr>");
            SB.Append("<td colspan='2'><b></b></td>");
            SB.Append("</tr>");

            SB.Append("</table>");

            SB.Append("</body>");
            SB.Append("</html>");

            // ENIVIO DE CORREO 

            ConfiguracionGlobal configuracion = new ConfiguracionGlobal();
            configuracion.Id_Cd = Sesion.Id_Cd_Ver;
            configuracion.Id_Emp = Sesion.Id_Emp;
            CN_Configuracion cn_configuracion = new CN_Configuracion();
            cn_configuracion.Consulta(ref configuracion, Sesion.Emp_Cnx);

            string body = SB.ToString();
            AlternateView vistaHtml = AlternateView.CreateAlternateViewFromString(body, null, MediaTypeNames.Text.Html);

            //this.RespaldoCorreo(hfNumSolicitudAbasto.Value, body, correo);

            SmtpClient sm = new SmtpClient(configuracion.Mail_Servidor, Convert.ToInt32(configuracion.Mail_Puerto));
            sm.Credentials = new NetworkCredential(configuracion.Mail_Usuario, configuracion.Mail_Contraseña);
            sm.EnableSsl = false;
            MailMessage m = new MailMessage();
            //  string[] eVirtual = configuracion.Mail_EVirtual.Split(',');
            m.From = new MailAddress(configuracion.Mail_Remitente);

            string correo = "";

            this.CorreosAutorizadorxMotivoxApp(ref correo, TipoSolicitud, famk);
            string[] eVirtual2 = correo.Split(',');

            int reng = 1;

            reng = 0;

            foreach (string core in eVirtual2)
            {
                if (core != " ")
                {
                    if (reng == 0)
                    {
                        m.To.Add(new MailAddress(core));
                        reng = 1;
                    }
                    else
                    {
                        m.CC.Add(new MailAddress(core));
                    }

                }
            }

            m.Bcc.Add(new MailAddress("raul.borquez@gibraltar.com.mx"));
            /*m.Subject = "Nueva Solicitud de Compra Local: " + SolFolio.ToString();            
            */
            m.IsBodyHtml = true;

            try
            {
                //LinkedResource logo = new LinkedResource(MapPath(@"Imagenes/logo.jpg"), MediaTypeNames.Image.Jpeg);
                //logo.ContentId = "companylogo";
                //vistaHtml.LinkedResources.Add(logo);
            }
            catch (Exception)
            {
            }

            m.AlternateViews.Add(vistaHtml);

            int CorreoEnviado = 0;
            try
            {
                sm.Send(m);
                CorreoEnviado = 1;
            }
            catch (Exception exx)
            {
                CorreoEnviado = -1;
            }
            this.RespaldoCorreo(SolFolio.ToString(), body, correo);
        }

        // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
        // Compar Locales - ENVIO de CORREO
        // FEB13-2020 RFH 
        // RBM MARZO 2024
        public void Correo_SolicitudDeCompra_ver2(
            int TipoSolicitud,
            int SolFolio,
            int Id_Cd,
            string Cd_Nombre,
            int Id_U,
            string Emp_Nombre,
            int IdPrvoveedor,
            string ProveedorNombre,
            string CodigoProducto,
            string CodigoProductoPadre,
            string Prd_Descripcion,
            string TipoProducto, //12
            string Familia,
            string SubFamilia,
            string Vigencia,
            string MotivoDesabasto,
            long KeyArray_ClienteExclusivos,
            int IdAplicacion,
            List<eCL_ResponsableAutorizador> LstAutorizadores,
            string Conexion,
            //Nuevos
            string ProveedorLocal,
            string CodigoProductoProv,
            string DescripcionProductoProv,
            string PresentacionProductoProv

            )
        {
            string TipoCompra = "";
            string Titulo = "";
            //int famk = 0;
            int Verificador = 0;

            //List<eCL_ResponsableAutorizador> LstAutorizadores = new List<eCL_ResponsableAutorizador>();
            //LstAutorizadores = this.CorreosAutorizador_xMotivoxApp(ref Verificador, TipoSolicitud, IdAplicacion);

            switch (TipoSolicitud)
            {
                case 1: // Por Desabasto
                    TipoCompra = "OPCIÓN 1: Activación de código por falta de producto";
                    Titulo = "Se ha colocado una nueva solicitud de <b>COMPRA LOCAL</b> con los siguientes datos:";
                    break;
                case 2: // Abasto Local
                    TipoCompra = "OPCIÓN 2: Código central con abasto local";
                    Titulo = "Se ha colocado una nueva solicitud de <b>COMPRA LOCAL</b> con los siguientes datos:";
                    break;
                case 3: // Solicitud cliente
                    TipoCompra = "OPCIÓN 3: Solicitud del cliente";
                    Titulo = "Se ha colocado una nueva solicitud de <b>COMPRA LOCAL</b> con los siguientes datos:";
                    break;
                case 4: // Por Desabasto
                    TipoCompra = "OPCIÓN 4: Compra por Estrategia";
                    Titulo = "Se ha colocado una nueva solicitud de <b>COMPRA LOCAL</b> con los siguientes datos:";
                    break;
            }

            StringBuilder SB = new StringBuilder();

            string Tmp = string.Empty;

            string s_temp_file = "";
            switch (TipoSolicitud)
            {
                case 1:
                    // Por Desabasto
                    s_temp_file = HttpContext.Current.Server.MapPath("~/CL/CL_EmailTemplate.htm");
                    break;
                case 2:
                    // Abasto Local
                    s_temp_file = HttpContext.Current.Server.MapPath("~/CL/CL_EmailTemplate_CodigoCentralAbastoLocal.htm");
                    break;
                case 3:
                    // Solicitud cliente
                    s_temp_file = HttpContext.Current.Server.MapPath("~/CL/CL_EmailTemplate_SolCliente.htm");
                    break;
                case 4:
                    // Por Desabasto
                    s_temp_file = HttpContext.Current.Server.MapPath("~/CL/CL_EmailTemplate.htm");
                    break;
            }

            //using (StreamReader reader = new StreamReader(Server.MapPath("~/CL_EmailTemplate.htm")))
            using (StreamReader reader = new StreamReader(s_temp_file))
            {
                Tmp = reader.ReadToEnd();
                SB.Append(Tmp);
            }


            //RBM FEB 2024
            //PRUEBAS
            //string LigaServidor = "http://207.248.253.106/siancentralpruebas/CL_Autorizaciones.aspx?";

            //DESARROLLO
            //string LigaServidor = "http://localhost:55399/CL_Autorizaciones.aspx?";

            //PRODUCTIVO
            string LigaServidor = "http://40.84.229.61/siancentral/CL_Autorizaciones.aspx?";

            SB.Replace("{AUT_PARAMESTROS}", LigaServidor + "SolFolio=" + SolFolio.ToString() + "&Id_Cd=" + Id_Cd.ToString() + "&TipoCompra=" + TipoCompra + "&TipoSolicitud=" + TipoSolicitud.ToString());
            SB.Replace("{TITULO}", Titulo);
            SB.Replace("{FOLIO}", SolFolio.ToString());
            SB.Replace("{CENTRO}", Id_Cd.ToString() + " - " + Cd_Nombre);
            SB.Replace("{GENERADO_POR}", Emp_Nombre);
            SB.Replace("{TIPO_COMPRA}", TipoCompra);

            if (TipoSolicitud == 1 || TipoSolicitud == 2 || TipoSolicitud == 4)
            {

                SB.Replace("{CODIGO_PRODUCTO}", CodigoProducto);
                SB.Replace("{CODIGO_PADRE}", CodigoProductoPadre);
                SB.Replace("{PROVEEDOR}", ProveedorLocal);

            }


            if (TipoSolicitud == 3)
            {
                SB.Replace("{CODIGO_PRODUCTO}", CodigoProducto);
                SB.Replace("{CODIGO_PADRE}", "0");
                SB.Replace("{PROVEEDOR}", ProveedorNombre);
            }

            if (TipoSolicitud == 1 || TipoSolicitud == 2 || TipoSolicitud == 3 || TipoSolicitud == 4)
            {
                SB.Replace("{PROVEEDORLOCAL}", ProveedorNombre);
                SB.Replace("{CODIGOPRODUCTOPROVEEDOR}", CodigoProductoProv);
                SB.Replace("{DESCRIPCIONPRODUCTOPROVEEDOR}", DescripcionProductoProv);
                SB.Replace("{PRESENTACIONPRODUCTOPROVEEDOR}", PresentacionProductoProv);
                SB.Replace("{DESCRIPCION_PROD}", Prd_Descripcion);
                SB.Replace("{TIPO_PROD}", TipoProducto);
                SB.Replace("{FAMILIA}", Familia);
                SB.Replace("{SUBFAMILIA}", SubFamilia);
                SB.Replace("{VIGENCIA}", Vigencia);
                SB.Replace("{MOTIVO_DESABASTO}", MotivoDesabasto);
            }

            // 
            SB.Replace("{CORREO_AUTORIZADOR}", LstAutorizadores[0].Responsable + " (" + LstAutorizadores[0].Correo + ")");

            // - - - - - - - - - - - - - - - - - - 
            // PRECIOS           
            long lCodProducto = 0;
            long.TryParse(CodigoProducto, out lCodProducto);

            List<eCL_ProductoPrecios> lstPrecios = new List<eCL_ProductoPrecios>();
            eCL_ProductoPrecios objPrec = new eCL_ProductoPrecios();

            CD_CatProducto CD_Prod_Precs = new CD_CatProducto();
            lstPrecios = CD_Prod_Precs.spSel_ProductoPrecios_CL_ver2(Sesion.Id_Emp, Sesion.Id_Cd, lCodProducto, Sesion.Emp_Cnx);

            StringBuilder SB_pecios = new StringBuilder();

            SB_pecios.Append("<table class='tbl_prec'>");
            SB_pecios.Append("<tr>");
            SB_pecios.Append("<th class='bg_t_color1'>Tipo de precio</th>");
            SB_pecios.Append("<th class='bg_t_color1'>Precio</th>");
            SB_pecios.Append("<tr>");

            int BG_Color = 0;
            double PrecioAAA = 0;
            double PrecioCL = 0;

            foreach (eCL_ProductoPrecios Prec in lstPrecios)
            {
                if (Prec.Id_Pre == 1)
                {
                    PrecioAAA = Prec.Prd_Pesos;
                }

                if (Prec.Id_Pre == 4)
                {
                    PrecioCL = Prec.Prd_Pesos;
                }

                if (BG_Color == 1)
                {
                    SB_pecios.Append("<tr>");
                    SB_pecios.Append("<td class='bg_col1'>" + Prec.Prd_Descripcion + "</td>");
                    SB_pecios.Append("<td class='bg_col2'>$" + Prec.Prd_Pesos.ToString() + "</td>");
                    SB_pecios.Append("<tr>");
                    BG_Color = 0;
                }
                else
                {
                    SB_pecios.Append("<tr>");
                    SB_pecios.Append("<td class='bg_col1'>" + Prec.Prd_Descripcion + "</td>");
                    SB_pecios.Append("<td class='bg_col2'>$" + Prec.Prd_Pesos.ToString() + "</td>");
                    SB_pecios.Append("<tr>");
                    BG_Color = 1;
                }
            }
            SB_pecios.Append("</table>");
            string SB_pecios_;
            SB_pecios_ = SB_pecios.ToString();
            SB.Replace("{LISTA_PRECIOS}", SB_pecios_);

            string MensajePrecioAAA = "";
            if (PrecioCL > PrecioAAA)
            {
                MensajePrecioAAA = "Importante: el costo del código de compra local es mayor al precio AAA del código Key.";
            }

            SB.Replace("{MENSAJEPRECIOAAA}", MensajePrecioAAA);



            // - - - - - - - - - - - - - - - - - - 
            // LISTADO DE CLIENTE             
            StringBuilder SB_ClientesExc = new StringBuilder();
            List<eListaGenerica> lstClienteExc = new List<eListaGenerica>();

            CD_ComprasLocales CD_CE = new CD_ComprasLocales();
            lstClienteExc = CD_CE.CL_SelClienteExclusivo(KeyArray_ClienteExclusivos, Sesion.Emp_Cnx);

            SB_ClientesExc.Append("<table class='tbl_prec'>");
            SB_ClientesExc.Append("<tr>");
            SB_ClientesExc.Append("<th class='bg_t_color1'>Tipo de Cte.</th>");
            SB_ClientesExc.Append("<th class='bg_t_color1'>No Cte.</th>");
            SB_ClientesExc.Append("<th class='bg_t_color1'>Nombre</th>");
            SB_ClientesExc.Append("<tr>");

            BG_Color = 0;

            if (lstClienteExc != null)
            {
                foreach (eListaGenerica Cliente in lstClienteExc)
                {
                    if (BG_Color == 1)
                    {
                        SB_ClientesExc.Append("<tr>");
                        SB_ClientesExc.Append("<td class='bg_col1'>" + Cliente.TipoCliente + "</td>");
                        SB_ClientesExc.Append("<td class='bg_col1'>" + Cliente.Id + "</td>");
                        SB_ClientesExc.Append("<td class='bg_col1'>" + Cliente.Descripcion + "</td>");
                        SB_ClientesExc.Append("<tr>");
                        BG_Color = 0;
                    }
                    else
                    {
                        SB_ClientesExc.Append("<tr>");
                        SB_ClientesExc.Append("<td class='bg_col1'>" + Cliente.TipoCliente + "</td>");
                        SB_ClientesExc.Append("<td class='bg_col1'>" + Cliente.Id + "</td>");
                        SB_ClientesExc.Append("<td class='bg_col1'>" + Cliente.Descripcion + "</td>");
                        SB_ClientesExc.Append("<tr>");
                        BG_Color = 1;
                    }
                }
            }
            SB_ClientesExc.Append("</table>");

            string SB_ClienteExc_;
            SB_ClienteExc_ = SB_ClientesExc.ToString();
            SB.Replace("{CLIENTES_EXCLUSIVOS}", SB_ClienteExc_);

            // ENIVIO DE CORREO 

            ConfiguracionGlobal configuracion = new ConfiguracionGlobal();
            configuracion.Id_Cd = Sesion.Id_Cd_Ver;
            configuracion.Id_Emp = Sesion.Id_Emp;
            CN_Configuracion cn_configuracion = new CN_Configuracion();
            cn_configuracion.Consulta(ref configuracion, Sesion.Emp_Cnx);

            string body = SB.ToString();
            AlternateView vistaHtml = AlternateView.CreateAlternateViewFromString(body, null, MediaTypeNames.Text.Html);

            //this.RespaldoCorreo(hfNumSolicitudAbasto.Value, body, correo);
            SmtpClient sm = new SmtpClient(configuracion.Mail_Servidor, Convert.ToInt32(2525));
            sm.Credentials = new NetworkCredential(configuracion.Mail_Usuario, configuracion.Mail_Contraseña);
            sm.EnableSsl = false;
            MailMessage m = new MailMessage();
            //  string[] eVirtual = configuracion.Mail_EVirtual.Split(',');
            m.From = new MailAddress(configuracion.Mail_Remitente);

            //string correo = "ing.rborquez@gmail.com, raul.borquez@gibraltar.com.mx,dianela.morales@key.com.mx,servicios.informatica@gibraltar.com.mx";
            string correo = "";
            // CORREO de AUTORIZADORES 

            // Modificar Lista de Correos en Produccion 
            this.CorreosAutorizadorxMotivoxApp(ref correo, TipoSolicitud, IdAplicacion);


            string[] eVirtual2 = correo.Split(',');
            int reng = 1;

            reng = 0;


            foreach (string core in eVirtual2)
            {
                if (core != " ")
                {
                    if (reng == 0)
                    {
                        m.To.Add(new MailAddress(core));
                        reng = 1;
                    }
                    else
                    {
                        m.CC.Add(new MailAddress(core));
                    }
                }
            }

            m.CC.Add(new MailAddress("raul.borquez@gibraltar.com.mx"));

            m.Subject = "Nueva Solicitud de Compra Local: " + SolFolio.ToString();
            m.IsBodyHtml = true;
            try
            {
                //LinkedResource logo = new LinkedResource(MapPath(@"Imagenes/logo.jpg"), MediaTypeNames.Image.Jpeg);
                //logo.ContentId = "companylogo";
                //vistaHtml.LinkedResources.Add(logo);
            }
            catch (Exception)
            {
            }
            m.AlternateViews.Add(vistaHtml);

            int CorreoEnviado = 0;
            try
            {
                sm.Send(m);
                CorreoEnviado = 1;

            }
            catch (Exception exx)
            {
                CorreoEnviado = -1;
            }

            this.RespaldoCorreo(SolFolio.ToString(), body, correo);

        }

        // cmbCategoria - Tipo Solicitud
        private void CorreosAutorizadorxMotivoxApp(ref string Mails, int cmbCategoria, int Aplikacion)
        {
            int Motivo2 = cmbCategoria;
            CN_ComprasLocales cn_Listadocompralocal = new CN_ComprasLocales();
            //int Motivo2 = Convert.ToInt32(this.cmbCategorias.SelectedValue);
            int aplica = Aplikacion;
            cn_Listadocompralocal.CorreosAutorizadorxMotivoxApp(ref Mails, Motivo2, aplica, Sesion.Id_Emp, Sesion.Id_Cd, Sesion.Emp_Cnx);
        }

        // 5Abr2022 Autorizadores de Correo CL
        public List<eCL_ResponsableAutorizador> CorreosAutorizador_xMotivoxApp(ref int Verificador, int cmbCategoria, int Aplikacion)
        {
            int Motivo2 = cmbCategoria;
            CN_ComprasLocales cn_Listadocompralocal = new CN_ComprasLocales();
            int aplica = Aplikacion;
            return cn_Listadocompralocal.spCatCorreosAutoCompraLocal_CorreoxApp(ref Verificador, Motivo2, aplica, Sesion.Id_Emp, Sesion.Id_Cd, Sesion.Emp_Cnx);
        }

        // 5Abr2022 RFH
        public List<eCL_ResponsableAutorizador> spCatCorreosAutoCompraLocal_CorreoxApp(
            ref int Verificador, int motivo, int aplicacion, int Emp, int Cd, string Conexion)
        {
            CD_ComprasLocales claseCapaDatos = new CD_ComprasLocales();
            return claseCapaDatos.spCatCorreosAutoCompraLocal_CorreoxApp(ref Verificador, motivo, aplicacion, Emp, Cd, Conexion);
        }

        // ENE31-2020 RFH  Compras Locales 

        private void RespaldoCorreo(string Solicitud, string BodyMail, string Rem)
        {
            CN_ComprasLocales cn_Listadocompralocal = new CN_ComprasLocales();
            cn_Listadocompralocal.RespaldoDeCorreo(Solicitud, BodyMail, Rem, Sesion.Id_Emp, Sesion.Id_Cd, Sesion.Emp_Cnx);
        }

        public void GrabaDatosProductoSAT(int solicitud, long producto, string CUnidad, string CProducto, string Conexion)
        {
            try
            {

                CD_ComprasLocales claseComprasLocalesgrd = new CD_ComprasLocales();
                claseComprasLocalesgrd.GrabaDatosProductoSAT(solicitud, producto, CUnidad, CProducto, Conexion);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ProductosClonados(int solicitud, long productooriginal, long productoclon, string Conexion)
        {
            try
            {

                CD_ComprasLocales claseComprasLocalesgrd = new CD_ComprasLocales();
                claseComprasLocalesgrd.ProductosClonados(solicitud, productooriginal, productoclon, Conexion);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string NuevoCodigoProducto(int idEmp, int idCd, string Categoria, string Proveedor, int Producto, string Conexion)
        {
            try
            {
                string maximo = "";
                CD_ComprasLocales claseComprasLocalesgrd = new CD_ComprasLocales();
                claseComprasLocalesgrd.NuevoCodigoProducto(idEmp, idCd, Categoria, Proveedor, Producto, Conexion, ref maximo);
                return maximo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // Compras Locales
        // ENE21-2020 RFH 

        public string NuevoCodigoProducto_ver2(
            int idEmp, int idCd, string Categoria, string Proveedor, Int64 Producto, string Conexion)
        {
            string maximo = "";
            CD_ComprasLocales claseComprasLocalesgrd = new CD_ComprasLocales();
            claseComprasLocalesgrd.NuevoCodigoProducto_ver2(idEmp, idCd, Categoria, Proveedor, Producto, Conexion, ref maximo);
            return maximo;
        }

        public void CorreosAutorizadorxMotivoxApp(ref string Correo, int motivo, int aplicacion, int Emp, int Cd, string Conexion)
        {
            CD_ComprasLocales claseCapaDatos = new CD_ComprasLocales();
            claseCapaDatos.CorreosAutorizadorxMotivoxApp(ref Correo, motivo, aplicacion, Emp, Cd, Conexion);
        }


        public void RespaldoDeCorreo(string Solicitud, string BodyMail, string Remitente, int Emp, int Cd, string Conexion)
        {
            try
            {
                CD_ComprasLocales claseCapaDatos = new CD_ComprasLocales();
                claseCapaDatos.RespaldoDeCorreo(Solicitud, BodyMail, Remitente, Emp, Cd, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AutorizaSolicitud(string Solicitud, int Emp, int CDI, int Usr, string Vigencia, string Conexion)
        {
            try
            {
                CD_ComprasLocales CD = new CD_ComprasLocales();
                CD.AutorizaSolicitud(Solicitud, Emp, CDI, Usr, Vigencia, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AutorizaSolicitudEditar(string Solicitud, int Emp, int CDI, int Usr, string Vigencia, string Conexion)
        {
            try
            {
                CD_ComprasLocales CD = new CD_ComprasLocales();
                CD.AutorizaSolicitudEditar(Solicitud, Emp, CDI, Usr, Vigencia, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void ValidaSolicitud(string Solicitud, int cdi, string Conexion)
        {
            try
            {
                CD_ComprasLocales claseCapaDatos = new CD_ComprasLocales();
                claseCapaDatos.ValidaSolicitud(Solicitud, cdi, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CorreosAutorizadorxMotivo(ref string Correo, int motivo, int Emp, int Cd, string Conexion)
        {
            try
            {
                CD_ComprasLocales claseCapaDatos = new CD_ComprasLocales();
                claseCapaDatos.CorreosAutorizadorxMotivo(ref Correo, motivo, Emp, Cd, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CatCorreosCL(ref DataTable dt, int Emp, int Cd, string Conexion)
        {
            try
            {
                CD_ComprasLocales claseCapaDatos = new CD_ComprasLocales();
                claseCapaDatos.CatCorreosCL(ref dt, Emp, Cd, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CatMotivoCL(ref DataTable dt, string Conexion)
        {
            try
            {
                CD_ComprasLocales claseCapaDatos = new CD_ComprasLocales();
                claseCapaDatos.CatMotivoCL(ref dt, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CatDesConsulta(ref DataTable dt, string Conexion)
        {
            try
            {
                CD_ComprasLocales claseCapaDatos = new CD_ComprasLocales();
                claseCapaDatos.CatDesConsulta(ref dt, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaClienteExclusivos(ref DataTable dt, string Conexion, long Prod)
        {
            try
            {
                CD_ComprasLocales claseCapaDatos = new CD_ComprasLocales();
                claseCapaDatos.ConsultaClienteExclusivos(ref dt, Conexion, Prod);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CatCorreoGraba(string Empre, int CDI, int idConfigu, int Motivo, string correo, int aplicacion, int secuancia, string Conexion)
        {
            try
            {
                CD_ComprasLocales claseCapaDatos = new CD_ComprasLocales();
                claseCapaDatos.CatCorreoGraba(Empre, CDI, idConfigu, Motivo, correo, aplicacion, secuancia, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CatMotivoGraba(string Id, string descip, string AAA, bool aplica, string Conexion)
        {
            try
            {
                CD_ComprasLocales claseCapaDatos = new CD_ComprasLocales();
                claseCapaDatos.CatMotivoGraba(Id, descip, AAA, aplica, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CatDesAgrega(string causa, string Conexion)
        {
            try
            {
                CD_ComprasLocales claseCapaDatos = new CD_ComprasLocales();
                claseCapaDatos.CatDesAgrega(causa, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CatDesDesactiva(int IdCausa, string Conexion)
        {
            try
            {
                CD_ComprasLocales claseCapaDatos = new CD_ComprasLocales();
                claseCapaDatos.CatDesDesactiva(IdCausa, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CatDesElimina(int IdCausa, string Conexion)
        {
            try
            {
                CD_ComprasLocales claseCapaDatos = new CD_ComprasLocales();
                claseCapaDatos.CatDesElimina(IdCausa, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultarSolicitud(int Solicitud, string Conexion, ref long Producto)
        {
            try
            {
                CD_ComprasLocales claseCapaDatos = new CD_ComprasLocales();
                claseCapaDatos.ConsultarSolicitud(Solicitud, Conexion, ref Producto);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CatEliminaCorreoNotifica(int Empresa, int CDI, int Motivo, int Aplica, int secuencia, string Conexion)
        {
            try
            {
                CD_ComprasLocales claseCapaDatos = new CD_ComprasLocales();
                claseCapaDatos.CatEliminaCorreoNotifica(Empresa, CDI, Motivo, Aplica, secuencia, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaExistenciaProductoCompraLocal(int Empres, int CDI, long Producto, string Conexion, ref int verifi)
        {
            try
            {
                CD_ComprasLocales claseCapaDatos = new CD_ComprasLocales();
                claseCapaDatos.ConsultaExistenciaProductoCompraLocal(Empres, CDI, Producto, Conexion, ref verifi);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // Cambios para Ajustes de Costos EMC Sept-2019

        public void EvaluaCambioDeCosto(int Id_Cd, long Id_Prd, double Es_Costo, string Conexion, ref int iOkk)
        {
            try
            {
                CD_ComprasLocales claseComprasLocalesgrd = new CD_ComprasLocales();
                claseComprasLocalesgrd.EvaluaCambioDeCosto(Id_Cd, Id_Prd, Es_Costo, Conexion, ref iOkk);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AplicaCambioDeCostoCompraLocal(int Id_Cd, long Id_Prd, double Es_Costo, string Conexion, ref int iOkk)
        {
            try
            {
                CD_ComprasLocales claseComprasLocalesgrd = new CD_ComprasLocales();
                claseComprasLocalesgrd.AplicaCambioDeCostoCompraLocal(Id_Cd, Id_Prd, Es_Costo, Conexion, ref iOkk);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //-------------------------------------------------------------------------------------------

        public void ListaSucursales(string Conexion, ref List<Embudo> List)
        {
            try
            {
                CD_ComprasLocales claseCapaDatos = new CD_ComprasLocales();
                claseCapaDatos.ListaSucursales(Conexion, ref List);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //         

        protected Sesion Sesion
        {
            get
            {
                if (HttpContext.Current.Session != null)
                {
                    return (Sesion)HttpContext.Current.Session["Sesion" + HttpContext.Current.Session.SessionID];
                }
                return null;
            }
        }

        public void ActualizarCompraLocal(int Id_Emp, int Id_Cd, int Id_Comp, string Id_Prd, string Prd_FechaInicio, string Prd_FechaFin, string Prd_ClaveUnidad, string Prd_ClaveProdServ, string Emp_Cnx, ref int Res, ref CompraLocal CL)
        {
            CD_ComprasLocales CD = new CD_ComprasLocales();
            CD.ActualizarCompraLocal(Id_Emp, Id_Cd, Id_Comp, Id_Prd, Prd_FechaInicio, Prd_FechaFin, Prd_ClaveUnidad, Prd_ClaveProdServ, Emp_Cnx, ref Res, ref CL);
        }

        public void CL_UpdatePreciosCompraLocal(int Id_Emp, int Id_Cd, string Id_Prd, int Id_Pre, string Prd_FechaInicio, string Prd_FechaFin, float Prd_Pesos, string Emp_Cnx)
        {
            CD_ComprasLocales CD = new CD_ComprasLocales();
            CD.CL_UpdatePreciosCompraLocal(Id_Emp, Id_Cd, Id_Prd, Id_Pre, Prd_FechaInicio, Prd_FechaFin, Prd_Pesos, Emp_Cnx);
        }

        public void Correo_SolicitudesEditadas(
            int TipoSolicitud,
            int SolFolio,
            int Id_Cd,
            string Cd_Nombre,
            int Id_U,
            string Emp_Nombre,
            int IdPrvoveedor,
            string ProveedorNombre,
            string CodigoProducto,
            string CodigoProductoPadre,
            string Prd_Descripcion,
            string TipoProducto, //12
            string Familia,
            string SubFamilia,
            string Vigencia,
            string MotivoDesabasto,
            long KeyArray_ClienteExclusivos,
            int IdAplicacion,
            List<eCL_ResponsableAutorizador> LstAutorizadores,
            string Conexion,
            //Nuevos
            string ProveedorLocal,
            string CodigoProductoProv,
            string DescripcionProductoProv,
            string PresentacionProductoProv

            )
        {
            string TipoCompra = "";
            string Titulo = "";
            //int famk = 0;
            int Verificador = 0;

            //List<eCL_ResponsableAutorizador> LstAutorizadores = new List<eCL_ResponsableAutorizador>();
            //LstAutorizadores = this.CorreosAutorizador_xMotivoxApp(ref Verificador, TipoSolicitud, IdAplicacion);

            switch (TipoSolicitud)
            {
                case 1: // Por Desabasto
                    TipoCompra = "OPCIÓN 1: Activacion de código por falta de producto";
                    Titulo = "Se ha colocado una nueva solicitud de <b>MODIFICACIÓN DE COMPRA LOCAL</b> con los siguientes datos:";
                    break;
                case 2: // Abasto Local
                    TipoCompra = "OPCIÓN 2: Código central con abasto local";
                    Titulo = "Se ha colocado una nueva solicitud de <b>MODIFICACIÓN DE COMPRA LOCAL</b> con los siguientes datos:";
                    break;
                case 3: // Solicitud cliente
                    TipoCompra = "OPCIÓN 3: Solicitud del cliente";
                    Titulo = "Se ha colocado una nueva solicitud de <b>MODIFICACIÓN DE COMPRA LOCAL</b> con los siguientes datos:";
                    break;
                case 4: // Compra por estrategia
                    TipoCompra = "OPCIÓN 4: Compra por estrategia";
                    Titulo = "Se ha colocado una nueva solicitud de <b>MODIFICACIÓN DE COMPRA LOCAL</b> con los siguientes datos:";
                    break;
            }

            StringBuilder SB = new StringBuilder();

            string Tmp = string.Empty;

            string s_temp_file = "";
            switch (TipoSolicitud)
            {
                case 1:
                    // Por Desabasto
                    s_temp_file = HttpContext.Current.Server.MapPath("~/CL/CL_EmailTemplate.htm");
                    break;
                case 2:
                    // Abasto Local
                    s_temp_file = HttpContext.Current.Server.MapPath("~/CL/CL_EmailTemplate_CodigoCentralAbastoLocal.htm");
                    break;
                case 3:
                    // Solicitud cliente
                    s_temp_file = HttpContext.Current.Server.MapPath("~/CL/CL_EmailTemplate_SolCliente.htm");
                    break;
                case 4:
                    // compra por Estrategia
                    s_temp_file = HttpContext.Current.Server.MapPath("~/CL/CL_EmailTemplate.htm");
                    break;
            }

            //using (StreamReader reader = new StreamReader(Server.MapPath("~/CL_EmailTemplate.htm")))
            using (StreamReader reader = new StreamReader(s_temp_file))
            {
                Tmp = reader.ReadToEnd();
                SB.Append(Tmp);
            }


            ////RBM FEB 2024
            ////PRUEBAS
            ////string LigaServidor = "http://207.248.253.106/siancentralpruebas/CL_Autorizaciones.aspx?";

            ////DESARROLLO
            ////string LigaServidor = "http://localhost:58896/CL_Autorizaciones.aspx?";

            ////PRODUCTIVO
            string LigaServidor = "http://40.84.229.61/siancentral/CL_Autorizaciones.aspx?";

            SB.Replace("{AUT_PARAMESTROS}", LigaServidor + "SolFolio=" + SolFolio.ToString() + "&Id_Cd=" + Id_Cd.ToString() + "&TipoCompra=" + TipoCompra + "&TipoSolicitud=" + TipoSolicitud.ToString());
            SB.Replace("{TITULO}", Titulo);
            SB.Replace("{FOLIO}", SolFolio.ToString());
            SB.Replace("{CENTRO}", Id_Cd.ToString() + " - " + Cd_Nombre);
            SB.Replace("{GENERADO_POR}", Emp_Nombre);
            SB.Replace("{TIPO_COMPRA}", TipoCompra);

            if (TipoSolicitud == 1 || TipoSolicitud == 2 || TipoSolicitud == 4)
            {

                SB.Replace("{CODIGO_PRODUCTO}", CodigoProducto);
                SB.Replace("{CODIGO_PADRE}", CodigoProductoPadre);
                SB.Replace("{PROVEEDOR}", ProveedorLocal);

            }


            if (TipoSolicitud == 3)
            {
                SB.Replace("{CODIGO_PRODUCTO}", CodigoProducto);
                SB.Replace("{CODIGO_PADRE}", "0");
                SB.Replace("{PROVEEDOR}", ProveedorNombre);
            }

            if (TipoSolicitud == 1 || TipoSolicitud == 2 || TipoSolicitud == 3 || TipoSolicitud == 4)
            {
                SB.Replace("{PROVEEDORLOCAL}", ProveedorNombre);
                SB.Replace("{CODIGOPRODUCTOPROVEEDOR}", CodigoProductoProv);
                SB.Replace("{DESCRIPCIONPRODUCTOPROVEEDOR}", DescripcionProductoProv);
                SB.Replace("{PRESENTACIONPRODUCTOPROVEEDOR}", PresentacionProductoProv);
                SB.Replace("{DESCRIPCION_PROD}", Prd_Descripcion);
                SB.Replace("{TIPO_PROD}", TipoProducto);
                SB.Replace("{FAMILIA}", Familia);
                SB.Replace("{SUBFAMILIA}", SubFamilia);
                SB.Replace("{VIGENCIA}", Vigencia);
                SB.Replace("{MOTIVO_DESABASTO}", MotivoDesabasto);
            }

            // 
            SB.Replace("{CORREO_AUTORIZADOR}", LstAutorizadores[0].Responsable + " (" + LstAutorizadores[0].Correo + ")");

            // - - - - - - - - - - - - - - - - - - 
            // PRECIOS           
            long lCodProducto = 0;
            long.TryParse(CodigoProducto, out lCodProducto);

            List<eCL_ProductoPrecios> lstPrecios = new List<eCL_ProductoPrecios>();
            eCL_ProductoPrecios objPrec = new eCL_ProductoPrecios();

            CD_CatProducto CD_Prod_Precs = new CD_CatProducto();
            lstPrecios = CD_Prod_Precs.spSel_ProductoPrecios_CL_ver2(Sesion.Id_Emp, Sesion.Id_Cd, lCodProducto, Sesion.Emp_Cnx);

            StringBuilder SB_pecios = new StringBuilder();

            SB_pecios.Append("<table class='tbl_prec'>");
            SB_pecios.Append("<tr>");
            SB_pecios.Append("<th class='bg_t_color1'>Tipo de precio</th>");
            SB_pecios.Append("<th class='bg_t_color1'>Precio</th>");
            SB_pecios.Append("<tr>");

            int BG_Color = 0;
            double PrecioAAA = 0;
            double PrecioCL = 0;

            foreach (eCL_ProductoPrecios Prec in lstPrecios)
            {
                if (Prec.Id_Pre == 1)
                {
                    PrecioAAA = Prec.Prd_Pesos;
                }

                if (Prec.Id_Pre == 4)
                {
                    PrecioCL = Prec.Prd_Pesos;
                }

                if (BG_Color == 1)
                {
                    SB_pecios.Append("<tr>");
                    SB_pecios.Append("<td class='bg_col1'>" + Prec.Prd_Descripcion + "</td>");
                    SB_pecios.Append("<td class='bg_col2'>$" + Prec.Prd_Pesos.ToString() + "</td>");
                    SB_pecios.Append("<tr>");
                    BG_Color = 0;
                }
                else
                {
                    SB_pecios.Append("<tr>");
                    SB_pecios.Append("<td class='bg_col1'>" + Prec.Prd_Descripcion + "</td>");
                    SB_pecios.Append("<td class='bg_col2'>$" + Prec.Prd_Pesos.ToString() + "</td>");
                    SB_pecios.Append("<tr>");
                    BG_Color = 1;
                }
            }
            SB_pecios.Append("</table>");
            string SB_pecios_;
            SB_pecios_ = SB_pecios.ToString();
            SB.Replace("{LISTA_PRECIOS}", SB_pecios_);

            string MensajePrecioAAA = "";
            if (PrecioCL > PrecioAAA)
            {
                MensajePrecioAAA = "Importante: el costo del código de compra local es mayor al precio AAA del código Key.";
            }

            SB.Replace("{MENSAJEPRECIOAAA}", MensajePrecioAAA);



            // - - - - - - - - - - - - - - - - - - 
            // LISTADO DE CLIENTE             
            StringBuilder SB_ClientesExc = new StringBuilder();
            List<eListaGenerica> lstClienteExc = new List<eListaGenerica>();

            CD_ComprasLocales CD_CE = new CD_ComprasLocales();
            lstClienteExc = CD_CE.CL_SelClienteExclusivo(KeyArray_ClienteExclusivos, Sesion.Emp_Cnx);

            CD_CE.CL_InsertClienteExclusivo_UpdateSol(KeyArray_ClienteExclusivos.ToString(), SolFolio, Sesion.Emp_Cnx);

            SB_ClientesExc.Append("<table class='tbl_prec'>");
            SB_ClientesExc.Append("<tr>");
            SB_ClientesExc.Append("<th class='bg_t_color1'>Tipo de Cte.</th>");
            SB_ClientesExc.Append("<th class='bg_t_color1'>No Cte.</th>");
            SB_ClientesExc.Append("<th class='bg_t_color1'>Nombre</th>");
            SB_ClientesExc.Append("<tr>");

            BG_Color = 0;

            if (lstClienteExc != null)
            {
                foreach (eListaGenerica Cliente in lstClienteExc)
                {
                    if (BG_Color == 1)
                    {
                        SB_ClientesExc.Append("<tr>");
                        SB_ClientesExc.Append("<td class='bg_col1'>" + Cliente.TipoCliente + "</td>");
                        SB_ClientesExc.Append("<td class='bg_col1'>" + Cliente.Id + "</td>");
                        SB_ClientesExc.Append("<td class='bg_col1'>" + Cliente.Descripcion + "</td>");
                        SB_ClientesExc.Append("<tr>");
                        BG_Color = 0;
                    }
                    else
                    {
                        SB_ClientesExc.Append("<tr>");
                        SB_ClientesExc.Append("<td class='bg_col1'>" + Cliente.TipoCliente + "</td>");
                        SB_ClientesExc.Append("<td class='bg_col1'>" + Cliente.Id + "</td>");
                        SB_ClientesExc.Append("<td class='bg_col1'>" + Cliente.Descripcion + "</td>");
                        SB_ClientesExc.Append("<tr>");
                        BG_Color = 1;
                    }
                }
            }
            SB_ClientesExc.Append("</table>");

            string SB_ClienteExc_;
            SB_ClienteExc_ = SB_ClientesExc.ToString();
            SB.Replace("{CLIENTES_EXCLUSIVOS}", SB_ClienteExc_);

            // ENIVIO DE CORREO 

            ConfiguracionGlobal configuracion = new ConfiguracionGlobal();
            configuracion.Id_Cd = Sesion.Id_Cd_Ver;
            configuracion.Id_Emp = Sesion.Id_Emp;
            CN_Configuracion cn_configuracion = new CN_Configuracion();
            cn_configuracion.Consulta(ref configuracion, Sesion.Emp_Cnx);

            string body = SB.ToString();
            AlternateView vistaHtml = AlternateView.CreateAlternateViewFromString(body, null, MediaTypeNames.Text.Html);

            //this.RespaldoCorreo(hfNumSolicitudAbasto.Value, body, correo);
            SmtpClient sm = new SmtpClient(configuracion.Mail_Servidor, Convert.ToInt32(2525));
            sm.Credentials = new NetworkCredential(configuracion.Mail_Usuario, configuracion.Mail_Contraseña);
            sm.EnableSsl = false;
            MailMessage m = new MailMessage();
            //  string[] eVirtual = configuracion.Mail_EVirtual.Split(',');
            m.From = new MailAddress(configuracion.Mail_Remitente);

            //string correo = "ing.rborquez@gmail.com, raul.borquez@gibraltar.com.mx,dianela.morales@key.com.mx,servicios.informatica@gibraltar.com.mx";
            string correo = ""; //"ing.rborquez@gmail.com, raul.borquez@gibraltar.com.mx,servicios.informatica@gibraltar.com.mx";
            // CORREO de AUTORIZADORES 
            // Modificar Lista de Correos en Produccion 
            this.CorreosAutorizadorxMotivoxApp(ref correo, TipoSolicitud, IdAplicacion);


            string[] eVirtual2 = correo.Split(',');
            int reng = 1;

            reng = 0;


            foreach (string core in eVirtual2)
            {
                if (core != " ")
                {
                    if (reng == 0)
                    {
                        m.To.Add(new MailAddress(core));
                        reng = 1;
                    }
                    else
                    {
                        m.CC.Add(new MailAddress(core));
                    }
                }
            }

            //m.Bcc.Add(new MailAddress("dianela.morales@key.com.mx"));
            m.CC.Add(new MailAddress("raul.borquez@gibraltar.com.mx"));
            m.Subject = "Nueva Solicitud de Modificación Compra Local: " + SolFolio.ToString();
            m.IsBodyHtml = true;
            try
            {
                //LinkedResource logo = new LinkedResource(MapPath(@"Imagenes/logo.jpg"), MediaTypeNames.Image.Jpeg);
                //logo.ContentId = "companylogo";
                //vistaHtml.LinkedResources.Add(logo);
            }
            catch (Exception)
            {
            }
            m.AlternateViews.Add(vistaHtml);

            int CorreoEnviado = 0;
            try
            {
                sm.Send(m);
                CorreoEnviado = 1;

            }
            catch (Exception exx)
            {
                CorreoEnviado = -1;
            }

            this.RespaldoCorreo(SolFolio.ToString(), body, correo);

        }

        public DataTable ComboMotivos()
        {
            CD_ComprasLocales cn = new CD_ComprasLocales();
            return cn.ComboMotivos();
        }


        public void LlenaCombo(int var, string Conexion, string sp, ref List<Comun> lista)
        {
            try
            {
                CD_ComprasLocales CD = new CD_ComprasLocales();
                CD.LlenaCombo(var, Conexion, sp, ref lista);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}