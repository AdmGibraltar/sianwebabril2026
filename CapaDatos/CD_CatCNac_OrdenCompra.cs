using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using CapaModelo_CC.CuentasCoorporativas;
using CapaEntidad;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class CD_CatCNac_OrdenCompra
    {


        public void ConsultarPedidosOC(int cliNum_Ini, int cliNum_Fin, int id_Ter_Ini, int id_Ter_Fin, int anio_Ini, int anio_Fin, string nomCliente, string Estatus, ref List<catcnac_PedidosOc> lista, string Conexion)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                SqlDataReader dr = null;
                string[] Parametros = {  "@CliNum_Ini",
                                        "@CliNum_Fin",
                                        "@Id_Ter_Ini",
                                        "@Id_Ter_Fin",
                                        "@Anio_Ini",
                                        "@Anio_Fin",
                                        "@Nom_Cliente",
                                        "@Estatus" };
                object[] Valores = { cliNum_Ini,
                                    cliNum_Fin,
                                    id_Ter_Ini,
                                    id_Ter_Fin,
                                    anio_Ini,
                                    anio_Fin,
                                    nomCliente,
                                    Estatus };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatCNac_ConsultarPedidosOC", ref dr, Parametros, Valores);
                catcnac_PedidosOc registro;
                while (dr.Read())
                {
                    registro = new catcnac_PedidosOc();
                    registro.Id = dr.IsDBNull(dr.GetOrdinal("Id")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id")));
                    registro.Id_OC = dr.IsDBNull(dr.GetOrdinal("Id_OC")) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("Id_OC")));
                    registro.Remision = dr.IsDBNull(dr.GetOrdinal("Remision")) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("Remision")));
                    registro.Id_Emp = dr.IsDBNull(dr.GetOrdinal("Id_Emp")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Emp")));
                    registro.Id_Cd = dr.IsDBNull(dr.GetOrdinal("Id_Cd")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cd")));
                    registro.Id_Cte = dr.IsDBNull(dr.GetOrdinal("Id_Cte")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cte")));
                    registro.VentaInstalada = dr.IsDBNull(dr.GetOrdinal("VentaInstalada")) ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("VentaInstalada")));
                    registro.Vigencia = dr.IsDBNull(dr.GetOrdinal("Vigencia")) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("Vigencia")));
                    registro.Estatus = dr.IsDBNull(dr.GetOrdinal("estatus")) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("estatus")));

                    registro.Cte_NomComercial = dr.IsDBNull(dr.GetOrdinal("Cte_NomComercial")) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("Cte_NomComercial")));
                    registro.Id_Ter = dr.IsDBNull(dr.GetOrdinal("Id_Ter")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Ter")));
                    registro.Nombre = dr.IsDBNull(dr.GetOrdinal("Nombre")) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("Nombre")));
                    registro.FechaAlta = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("FechaAlta")));
                    registro.Id_Ped = dr.IsDBNull(dr.GetOrdinal("Id_Ped")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Ped")));
                    registro.ID_sol = dr.IsDBNull(dr.GetOrdinal("id_sol")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("id_sol")));
                    registro.EstatusSOl = dr.IsDBNull(dr.GetOrdinal("EstatusSol")) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("EstatusSol")));

                    if (registro.Estatus == "P")
                    {
                        if (registro.EstatusSOl == "N")
                        {
                            registro.EstatusStr = "Pendiente";
                        }
                        else if (registro.EstatusSOl == "P")
                        {
                            registro.EstatusStr = "Pendiente Autorización";
                        }
                        else if (registro.EstatusSOl == "R")
                        {
                            registro.EstatusStr = "Rechazado";
                        }
                        else if (registro.EstatusSOl == "A")
                        {
                            registro.EstatusStr = "Autorizado";
                        }
                    }
                    else
                    {
                        registro.EstatusStr = "Captado";
                    }
                    lista.Add(registro);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public spCatCNac_ConsultaPedido_OC_Result ConsultaPedidoOC_Captacion(int idOC)
        {
            sianwebmty_CCEntities model = new sianwebmty_CCEntities();
            var res = model.spCatCNac_ConsultaPedido_OC(idOC).FirstOrDefault();
            return res;
        }


        public List<spCatCNac_OrdenCompra_Detalle_Result> ConsultarPedidoOrden_Detalle(string idOC, int Id_Cte, int Id_Cd)
        {
            sianwebmty_CCEntities model = new sianwebmty_CCEntities();
            var res = model.spCatCNac_OrdenCompra_Detalle(idOC, Id_Cte, Id_Cd).ToList();
            return res;
        }


        public List<spCatCNac_Remisiones80Ped_Result> ConsultarRemisionesPedido(int idOC)
        {
            sianwebmty_CCEntities model = new sianwebmty_CCEntities();
            var res = model.spCatCNac_Remisiones80Ped(idOC).ToList();

            return res;
        }




        public spCatCNac_ValidaPedidoMov80_Result ValidaRemMov80(int id_ped)
        {
            sianwebmty_CCEntities model = new sianwebmty_CCEntities();
            var res = model.spCatCNac_ValidaPedidoMov80(id_ped).FirstOrDefault();
            return res;
        }


        public spCatCNac_ValidaClienteOC80_Result ValidaClienteOC80(int id_emp, int id_cd, int id_ped)
        {
            sianwebmty_CCEntities model = new sianwebmty_CCEntities();
            var res = model.spCatCNac_ValidaClienteOC80(id_emp, id_cd, id_ped).FirstOrDefault();
            return res;
        }


    }

}