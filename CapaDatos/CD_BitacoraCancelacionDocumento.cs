using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class CD_BitacoraCancelacionDocumento
    {
        public void InsertarBitacora(ref eBitacoraCancelacionDocumento bitacora, string Conexion, ref int verificador)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            try
            {
                CapaDatos.StartTrans();
                //INSERTA CABECERAS
                #region Cabecera nota Credito
                string[] Parametros = {
                                        "@Id_Emp"
                                        ,"@Id_Cd"
                                        ,"@Motivo_Id"
                                        ,"@Documento_Id"
                                        ,"@Usuario_Id"
                                        ,"@TipoDocumento"
                                        ,"@SerieDocumento"
                                        ,"@Motivo"
                                        ,"@Usuario_Autorizo"
                                        ,"@IdBitacora"
                                        ,"@Id_Nca"
                                        ,"@Num_Cliente"
                                        ,"@StatusDev"
                                        ,"@SerieDev"
                                        ,"@FechaDev"
                                        ,"@TipoDev"
                                        ,"@Factura_dev"
                                        ,"@Nca_Subtotal"
                                        ,"@Nca_Iva"
                                        ,"@Nca_Total"
                                        ,"@Ncr_Estatus"
                                        ,"@Sucursal"
                                        ,"@CorreoSolicitud"
                                      };
                object[] Valores = {
                                        bitacora.Id_Emp
                                        ,bitacora.Id_Cd
                                        ,bitacora.Motivo_Id
                                        ,bitacora.Documento_Id
                                        ,bitacora.Usuario_Id
                                        ,bitacora.TipoDocumento
                                        ,bitacora.SerieDocumento
                                        ,bitacora.Motivo
                                        ,0,0
                                        ,bitacora.Id_Nca
                                        ,bitacora.Num_Cliente
                                        ,bitacora.StatusDev
                                        ,bitacora.SerieDev
                                        ,bitacora.FechaDev < System.Data.SqlTypes.SqlDateTime.MinValue.Value ? (DateTime?) null:             bitacora.FechaDev
                                        ,bitacora.TipoDev
                                        ,bitacora.Factura_dev
                                        ,bitacora.Nca_Subtotal
                                        ,bitacora.Nca_Iva
                                        ,bitacora.Nca_Total
                                        ,bitacora.Ncr_Estatus
                                        ,bitacora.Sucursal
                                        ,bitacora.CorreoSolicitud
                                   };


                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spBitacoraCancelacionDoc_Insertar", ref verificador, Parametros, Valores);
                bitacora.Id = verificador;
                #endregion
                CapaDatos.CommitTrans();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                CapaDatos.RollBackTrans();
                throw ex;
            }
        }

        public void ConsultaBitacora_Buscar(eBitacoraCancelacionDocumento notaCredito, ref List<eBitacoraCancelacionDocumento> listaNotaCredito, string Conexion)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = {
                                          "@Id_Emp"
                                          ,"@Id_Cd"
                                      };
                object[] Valores = {
                                       notaCredito.Id_Emp
                                       ,notaCredito.Id_Cd
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spBitacoraCancelacionDoc_Buscar", ref dr, Parametros, Valores);
                listaNotaCredito = new List<eBitacoraCancelacionDocumento>();
                while (dr.Read())
                {
                    notaCredito = new eBitacoraCancelacionDocumento();
                    //notaCredito.Id_Emp = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Emp")));
                    //notaCredito.Id_Cd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cd")));
                    notaCredito.Motivo_Id = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Motivo_Id")));
                    notaCredito.Documento_Id = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Documento_Id")));
                    notaCredito.Usuario_Id = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Usuario_Id")));
                    notaCredito.TipoDocumento = dr.IsDBNull(dr.GetOrdinal("TipoDocumento")) ? string.Empty : dr.GetValue(dr.GetOrdinal("TipoDocumento")).ToString();
                    notaCredito.SerieDocumento = dr.GetValue(dr.GetOrdinal("SerieDocumento")).ToString();
                    notaCredito.Fecha = dr.GetValue(dr.GetOrdinal("Fecha")).ToString();
                    notaCredito.Motivo = dr.GetValue(dr.GetOrdinal("Motivo")).ToString();
                    notaCredito.UsuarioCancelacion = dr.GetValue(dr.GetOrdinal("UsuarioCancelacion")).ToString();
                    notaCredito.FechaDocumento = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("FechaDocumento")));
                    notaCredito.EstatusBitacora = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("EstatusBitacora")));

                    //notaCredito.Ncr_Subtotal = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Ncr_Subtotal")));
                    //notaCredito.Ncr_Iva = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Ncr_ImporteIVA")));
                    //notaCredito.Ncr_Total = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Ncr_Total")));
                    //notaCredito.Ncr_Pagado = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Ncr_Pagado")));
                    //notaCredito.Ncr_Saldo = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Ncr_Saldo")));

                    //notaCredito.Ncr_Estatus = dr.IsDBNull(dr.GetOrdinal("Ncr_Estatus")) ? string.Empty : dr.GetValue(dr.GetOrdinal("Ncr_Estatus")).ToString();
                    //notaCredito.Ncr_EstatusStr = dr.IsDBNull(dr.GetOrdinal("Ncr_EstatusStr")) ? string.Empty : dr.GetValue(dr.GetOrdinal("Ncr_EstatusStr")).ToString();
                    //notaCredito.PDF = dr.IsDBNull(dr.GetOrdinal("PDF")) ? false : Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("PDF")).ToString());
                    //notaCredito.NcrXML = dr.IsDBNull(dr.GetOrdinal("NcrXML")) ? false : Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("NcrXML")).ToString());
                    //notaCredito.Id_NcrSerie = dr["Id_NcrSerie"].ToString();
                    //notaCredito.Ncr_FolioFiscal = dr["Ncr_FolioFiscal"].ToString();

                    listaNotaCredito.Add(notaCredito);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}