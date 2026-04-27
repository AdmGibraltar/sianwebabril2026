using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using CapaEntidad;
using System.Data;

namespace CapaDatos
{
    public class CD_CrmInforme1
    {

        // 20 NOV 2018 RFH  
        List<string> Parametros = new List<string>();
        List<object> Valores = new List<object>();

        public int Parametro(string Parametro, object Valor)
        {
            Parametros.Add(Parametro);
            Valores.Add(Valor);
            return 0;
        }

        // CRM 1
        // Para la version del CRM 1
        //
        public void spCRM_ControlPromocion_LimpiezaAplicacion(int Id_Emp, int Id_Cd, int Id_U, int Id_Rik, string periodo, int IntConsulta, string monto1, string monto2, bool Pnuevo, ref List<CapaEntidad.Informe1> Lista, string Conexion)
        {

            try
            {
                SqlDataReader dr = null;

                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                Parametro("@Id_Emp", Id_Emp);
                Parametro("@Id_Cd", Id_Cd);
                Parametro("@Id_U", Id_U);
                Parametro("@Id_Rik", Id_Rik == -1 ? (object)null : Id_Rik);
                Parametro("@Periodo", periodo);
                Parametro("@IntConsulta", IntConsulta);
                Parametro("@Monto1", !string.IsNullOrEmpty(monto1) ? Convert.ToDouble(monto1) : (object)null);
                Parametro("@Monto2", !string.IsNullOrEmpty(monto2) ? Convert.ToDouble(monto2) : (object)null);
                Parametro("@Nuevo", Pnuevo);

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCRM_ControlPromocion_LimpiezaAplicacion", ref dr, Parametros.ToArray(), Valores.ToArray());

                CapaEntidad.Informe1 a;
                while (dr.Read())
                {
                    a = new CapaEntidad.Informe1();

                    a.Proyecto = dr.GetValue(dr.GetOrdinal("Proyecto")).ToString();
                    a.Cliente = dr.GetValue(dr.GetOrdinal("Cliente")).ToString();
                    a.Area = dr.GetValue(dr.GetOrdinal("Area")).ToString();
                    a.Solucion = dr.GetValue(dr.GetOrdinal("Solucion")).ToString();
                    a.Aplicacion = dr.GetValue(dr.GetOrdinal("Aplicacion")).ToString();
                    a.Productos = dr.GetValue(dr.GetOrdinal("Productos")).ToString();

                    string sVTeorico = dr.GetValue(dr.GetOrdinal("VTeorico")).ToString();
                    sVTeorico = sVTeorico.Replace("$", "");
                    sVTeorico = sVTeorico.Replace(",", "");
                    sVTeorico = sVTeorico.Replace(" ", "");

                    decimal dVTeorico = 0;
                    decimal.TryParse(sVTeorico, out dVTeorico);
                    a.VTeorico = dVTeorico;
                    a.Analisis = dr.GetValue(dr.GetOrdinal("Analisis")).ToString();
                    a.Presentacion = dr.GetValue(dr.GetOrdinal("Presentacion")).ToString();
                    a.Negociacion = dr.GetValue(dr.GetOrdinal("Negociacion")).ToString();
                    a.Cierre = dr.GetValue(dr.GetOrdinal("Cierre")).ToString();
                    a.Cancelacion = dr.GetValue(dr.GetOrdinal("Cancelacion")).ToString();

                    string MontoProyecto = dr.GetValue(dr.GetOrdinal("MontoProyecto")).ToString();
                    MontoProyecto = MontoProyecto.Replace("$", "");
                    MontoProyecto = MontoProyecto.Replace(",", "");
                    MontoProyecto = MontoProyecto.Replace(" ", "");

                    decimal dMontoProyecto = 0;
                    decimal.TryParse(MontoProyecto, out dMontoProyecto);
                    a.MontoProyecto = dMontoProyecto;

                    a.Comentarios = dr.GetValue(dr.GetOrdinal("Comentarios")).ToString();
                    a.FechaModificacion = dr.GetValue(dr.GetOrdinal("FechaModificacion")).ToString();
                    a.Estatus = dr.GetValue(dr.GetOrdinal("Estatus")).ToString();
                    a.ClienteSIANID = dr.GetValue(dr.GetOrdinal("ClienteSIANID")).ToString();
                    a.OportunidadID = dr.GetValue(dr.GetOrdinal("OportunidadID")).ToString();
                    a.Rik = dr.GetValue(dr.GetOrdinal("Rik")).ToString();
                    a.Nombre = dr.GetValue(dr.GetOrdinal("Nombre")).ToString();
                    a.Causa = dr.GetValue(dr.GetOrdinal("Causa")).ToString();
                    Lista.Add(a);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // CRM 2 
        public void spCRM_ControlPromocion_LimpiezaAplicacion2(int Id_Emp, int Id_Cd, int Id_U, int Id_Rik, string periodo, int IntConsulta, string monto1, string monto2, bool Pnuevo, ref List<CapaEntidad.Informe1> Lista, string Conexion)
        {

            try
            {
                SqlDataReader dr = null;

                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                Parametro("@Id_Emp", Id_Emp);
                Parametro("@Id_Cd", Id_Cd);
                Parametro("@Id_U", Id_U);
                Parametro("@Id_Rik", Id_Rik);
                Parametro("@Periodo", periodo);
                Parametro("@IntConsulta", IntConsulta);
                Parametro("@Monto1", monto1);
                Parametro("@Monto2", monto2);
                Parametro("@Nuevo", Pnuevo);

                /*
                string[] Parametros = { 
                                          "@Id_Emp" 
                                          ,"@Id_Cd"
                                          ,"@Id_U"
                                          ,"@Id_Rik"
                                          ,"@Periodo"
                                          ,"@IntConsulta"
                                          ,"@Monto1"
                                          ,"@Monto2"
                                          ,"@Nuevo"
                                      };
                object[] Valores = { 
                                       Id_Emp
                                       ,Id_Cd
                                       ,Id_U
                                       ,Id_Rik // Id_Rik == -1 ? (object)null: Id_Rik -- No cambiara el valor ya que el sp manejara el el valor.
                                       ,periodo
                                       ,IntConsulta
                                       //,!string.IsNullOrEmpty(monto1)? Convert.ToDouble(monto1) : (object)null
                                       //,!string.IsNullOrEmpty(monto2)? Convert.ToDouble(monto2) : (object)null
                                       // el sp manejara el monto
                                       ,monto1
                                       ,monto2
                                       ,Pnuevo
                                   };
                */

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCRM_ControlPromocion_LimpiezaAplicacion2", ref dr, Parametros.ToArray(), Valores.ToArray());

                while (dr.Read())
                {
                    CapaEntidad.Informe1 EInforme = new CapaEntidad.Informe1();

                    EInforme.Proyecto = dr.GetValue(dr.GetOrdinal("Proyecto")).ToString();
                    EInforme.Cliente = dr.GetValue(dr.GetOrdinal("Cliente")).ToString();
                    EInforme.TipoCliente = dr.GetValue(dr.GetOrdinal("TipoCliente")).ToString(); //JFCV
                    EInforme.Area = dr.GetValue(dr.GetOrdinal("Area")).ToString();
                    EInforme.Solucion = dr.GetValue(dr.GetOrdinal("Solucion")).ToString();
                    EInforme.Aplicacion = dr.GetValue(dr.GetOrdinal("Aplicacion")).ToString();
                    //EInform.Productos = dr.GetValue(dr.GetOrdinal("Productos")).ToString();

                    string sVTeorico = dr.GetValue(dr.GetOrdinal("VTeorico")).ToString();
                    sVTeorico = sVTeorico.Replace("$", "");
                    sVTeorico = sVTeorico.Replace(",", "");
                    sVTeorico = sVTeorico.Replace(" ", "");
                    decimal dVTeorico = 0;
                    decimal.TryParse(sVTeorico, out dVTeorico);

                    EInforme.VTeorico = dVTeorico;
                    EInforme.Analisis = dr.GetValue(dr.GetOrdinal("Analisis")).ToString();
                    EInforme.Presentacion = dr.GetValue(dr.GetOrdinal("Presentacion")).ToString();
                    EInforme.Negociacion = dr.GetValue(dr.GetOrdinal("Negociacion")).ToString();
                    EInforme.Cierre = dr.GetValue(dr.GetOrdinal("Cierre")).ToString();
                    EInforme.Cancelacion = dr.GetValue(dr.GetOrdinal("Cancelacion")).ToString();

                    string MontoProyecto = dr.GetValue(dr.GetOrdinal("MontoProyecto")).ToString();
                    MontoProyecto = MontoProyecto.Replace("$", "");
                    MontoProyecto = MontoProyecto.Replace(",", "");
                    MontoProyecto = MontoProyecto.Replace(" ", "");

                    decimal dMontoProyecto = 0;
                    decimal.TryParse(MontoProyecto, out dMontoProyecto);
                    EInforme.MontoProyecto = dMontoProyecto;

                    EInforme.Comentarios = dr.GetValue(dr.GetOrdinal("Comentarios")).ToString();
                    EInforme.FechaCreacion = dr.GetValue(dr.GetOrdinal("FechaCreacion")).ToString();
                    EInforme.FechaModificacion = dr.GetValue(dr.GetOrdinal("FechaModificacion")).ToString();
                    EInforme.Estatus = dr.GetValue(dr.GetOrdinal("Estatus")).ToString();
                    EInforme.ClienteSIANID = dr.GetValue(dr.GetOrdinal("ClienteSIANID")).ToString();
                    EInforme.OportunidadID = dr.GetValue(dr.GetOrdinal("OportunidadID")).ToString();
                    EInforme.Rik = dr.GetValue(dr.GetOrdinal("Rik")).ToString();
                    EInforme.Nombre = dr.GetValue(dr.GetOrdinal("Nombre")).ToString();
                    EInforme.Causa = dr.GetValue(dr.GetOrdinal("Causa")).ToString();

                    string sVPO = dr.GetValue(dr.GetOrdinal("VPO")).ToString();
                    sVPO = sVPO.Replace("$", "");
                    sVPO = sVPO.Replace(",", "");
                    sVPO = sVPO.Replace(" ", "");
                    decimal dVPO = 0;
                    decimal.TryParse(sVPO, out dVPO);
                    EInforme.VPO = dVPO;

                    EInforme.Productos = dr.GetValue(dr.GetOrdinal("Productos")).ToString();
                    EInforme.TipoVenta = dr.GetValue(dr.GetOrdinal("TipoVenta")).ToString();
                    //a.Vap_Estatus2 = dr.GetValue(dr.GetOrdinal("Vap_Estatus2")).ToString();

                    Lista.Add(EInforme);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        //
        // CRM 2 
        //
        public void spCRM_ControlPromocion_LimpiezaAplicacion2_BySegmento(
            int Id_Emp, int Id_Cd, int Id_U, int Id_Rik, string periodo, int IntConsulta, string monto1, string monto2, bool Pnuevo,
            int Id_Uen, int Id_Seg, int Id_Area, int Id_Sol, int Id_Apl,
            ref List<CapaEntidad.Informe1> Lista, string Conexion)
        {

            try
            {
                SqlDataReader dr = null;

                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                Parametro("@Id_Emp", Id_Emp);
                Parametro("@Id_Cd", Id_Cd);
                Parametro("@Id_U", Id_U);
                Parametro("@Id_Rik", Id_Rik);
                Parametro("@Periodo", periodo);
                Parametro("@IntConsulta", IntConsulta);
                Parametro("@Monto1", monto1);
                Parametro("@Monto2", monto2);
                Parametro("@Nuevo", Pnuevo);
                Parametro("@Id_Uen", Id_Uen);
                Parametro("@Id_Seg", Id_Seg);
                Parametro("@Id_Area", Id_Area);
                Parametro("@Id_Sol", Id_Sol);
                Parametro("@Id_Apl", Id_Apl);

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCRM_ControlPromocion_LimpiezaAplicacion2_BySegmento", ref dr, Parametros.ToArray(), Valores.ToArray());

                while (dr.Read())
                {
                    CapaEntidad.Informe1 obj = new CapaEntidad.Informe1();

                    obj.Proyecto = dr.GetValue(dr.GetOrdinal("Proyecto")).ToString();
                    obj.TipoCliente = dr.GetValue(dr.GetOrdinal("TipoCliente")).ToString();
                    obj.Cliente = dr.GetValue(dr.GetOrdinal("Cliente")).ToString();

                    obj.UEN_Nombre = dr.GetValue(dr.GetOrdinal("Seg_UEN_Nombre")).ToString();
                    obj.Segmento_Nombre = dr.GetValue(dr.GetOrdinal("Seg_Segmento_Nombre")).ToString();
                    obj.Area = dr.GetValue(dr.GetOrdinal("Area")).ToString();
                    obj.Solucion = dr.GetValue(dr.GetOrdinal("Solucion")).ToString();
                    obj.Sol_Nombre = dr.GetValue(dr.GetOrdinal("Seg_Solucion_Nombre")).ToString();
                    obj.Aplicacion = dr.GetValue(dr.GetOrdinal("Seg_Aplicacion_Nombre")).ToString();
                    //EInform.Productos = dr.GetValue(dr.GetOrdinal("Productos")).ToString();

                    string sVTeorico = dr.GetValue(dr.GetOrdinal("VTeorico")).ToString();
                    sVTeorico = sVTeorico.Replace("$", "");
                    sVTeorico = sVTeorico.Replace(",", "");
                    sVTeorico = sVTeorico.Replace(" ", "");
                    decimal dVTeorico = 0;
                    decimal.TryParse(sVTeorico, out dVTeorico);

                    obj.VTeorico = dVTeorico;
                    obj.Analisis = dr.GetValue(dr.GetOrdinal("Analisis")).ToString();
                    obj.Presentacion = dr.GetValue(dr.GetOrdinal("Presentacion")).ToString();
                    obj.Negociacion = dr.GetValue(dr.GetOrdinal("Negociacion")).ToString();
                    obj.Cierre = dr.GetValue(dr.GetOrdinal("Cierre")).ToString();
                    obj.Cancelacion = dr.GetValue(dr.GetOrdinal("Cancelacion")).ToString();

                    string MontoProyecto = dr.GetValue(dr.GetOrdinal("MontoProyecto")).ToString();
                    MontoProyecto = MontoProyecto.Replace("$", "");
                    MontoProyecto = MontoProyecto.Replace(",", "");
                    MontoProyecto = MontoProyecto.Replace(" ", "");

                    decimal dMontoProyecto = 0;
                    decimal.TryParse(MontoProyecto, out dMontoProyecto);
                    obj.MontoProyecto = dMontoProyecto;

                    obj.Comentarios = dr.GetValue(dr.GetOrdinal("Comentarios")).ToString();
                    obj.FechaCreacion = dr.GetValue(dr.GetOrdinal("FechaCreacion")).ToString();
                    obj.FechaModificacion = dr.GetValue(dr.GetOrdinal("FechaModificacion")).ToString();
                    obj.Estatus = dr.GetValue(dr.GetOrdinal("Estatus")).ToString();
                    obj.ClienteSIANID = dr.GetValue(dr.GetOrdinal("ClienteSIANID")).ToString();
                    obj.OportunidadID = dr.GetValue(dr.GetOrdinal("OportunidadID")).ToString();
                    obj.Rik = dr.GetValue(dr.GetOrdinal("Rik")).ToString();
                    obj.Nombre = dr.GetValue(dr.GetOrdinal("Nombre")).ToString();
                    obj.Causa = dr.GetValue(dr.GetOrdinal("Causa")).ToString();

                    string sVPO = dr.GetValue(dr.GetOrdinal("VPO")).ToString();
                    sVPO = sVPO.Replace("$", "");
                    sVPO = sVPO.Replace(",", "");
                    sVPO = sVPO.Replace(" ", "");
                    decimal dVPO = 0;
                    decimal.TryParse(sVPO, out dVPO);
                    obj.VPO = dVPO;

                    obj.Productos = dr.GetValue(dr.GetOrdinal("Productos")).ToString();
                    obj.TipoVenta = dr.GetValue(dr.GetOrdinal("TipoVenta")).ToString();
                    //a.Vap_Estatus2 = dr.GetValue(dr.GetOrdinal("Vap_Estatus2")).ToString();

                    Lista.Add(obj);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void spCRM_ControlEntrada(int Id_Emp, int Id_Cd, int Id_Rik, string periodo, ref List<CapaEntidad.Informe2> Lista, string Conexion)
        {

            try
            {
                SqlDataReader dr = null;

                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = {
                                          "@Id_Emp"
                                          ,"@Id_Cd"
                                          ,"@Id_Rik"
                                          ,"@Periodo"
                                      };
                object[] Valores = {
                                       Id_Emp
                                       ,Id_Cd
                                       ,Id_Rik
                                       ,periodo
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCRM_ControlEntrada", ref dr, Parametros, Valores);

                CapaEntidad.Informe2 a;
                while (dr.Read())
                {
                    a = new CapaEntidad.Informe2();

                    a.Fecha = dr.GetValue(dr.GetOrdinal("Fecha")).ToString();
                    a.Zona = dr.GetValue(dr.GetOrdinal("Zona")).ToString();
                    a.ZonaID = dr.GetValue(dr.GetOrdinal("ZonaID")).ToString();
                    a.UsuarioID = dr.GetValue(dr.GetOrdinal("UsuarioID")).ToString();
                    a.Representante = dr.GetValue(dr.GetOrdinal("Representante")).ToString();

                    Lista.Add(a);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void spCRM_ControlPromocion_DIINumero(int Id_Emp, int Id_Cd, int Id_Rik, string Periodo, ref List<CapaEntidad.Informe3> Lista, string Conexion)
        {
            try
            {
                SqlDataReader dr = null;

                //SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = {
                                          "@Id_Emp"
                                          ,"@Id_Cd"
                                          ,"@Id_Rik"
                                          ,"@Periodo"
                                      };
                object[] Valores = {
                                       Id_Emp
                                       ,Id_Cd
                                       ,Id_Rik
                                       ,Periodo
                                   };


                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCRM_ControlPromocion_DIINumero", ref dr, Parametros, Valores);

                CapaEntidad.Informe3 a;
                while (dr.Read())
                {
                    a = new CapaEntidad.Informe3();

                    a.ZonaID = dr.GetValue(dr.GetOrdinal("ZonaID")).ToString();
                    a.UsuarioId = dr.GetValue(dr.GetOrdinal("UsuarioId")).ToString();

                    string sT = dr.GetValue(dr.GetOrdinal("Monto")).ToString();
                    decimal dT = 0;
                    decimal.TryParse(sT, out dT);
                    a.Monto = dT;

                    sT = dr.GetValue(dr.GetOrdinal("A")).ToString();
                    decimal.TryParse(sT, out dT);
                    a.A = dT;

                    sT = dr.GetValue(dr.GetOrdinal("P")).ToString();
                    decimal.TryParse(sT, out dT);
                    a.P = dT;

                    sT = dr.GetValue(dr.GetOrdinal("N")).ToString();
                    decimal.TryParse(sT, out dT);
                    a.N = dT;

                    sT = dr.GetValue(dr.GetOrdinal("C")).ToString();
                    decimal.TryParse(sT, out dT);
                    a.C = dT;

                    sT = dr.GetValue(dr.GetOrdinal("X")).ToString();
                    decimal.TryParse(sT, out dT);
                    a.X = dT;

                    a.Representante = dr.GetValue(dr.GetOrdinal("Representante")).ToString();
                    a.Zona = dr.GetValue(dr.GetOrdinal("Zona")).ToString();

                    Lista.Add(a);
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