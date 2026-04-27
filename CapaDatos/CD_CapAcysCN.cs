using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using CapaEntidad;
using System.Data;
using CapaModelo;

namespace CapaDatos
{
    // 14ENE-2020 RFH 

    public class CD_CapAcysCN
    {
        List<string> Parametros = new List<string>();
        List<object> Valores = new List<object>();

        public int Parametro(string Parametro, object Valor)
        {
            Parametros.Add(Parametro);
            Valores.Add(Valor);
            return 0;
        }

        public eAcysCN_Permisos spAcysCN_Permisos(ref int Estatus, ref string Mensaje,
            int Sucursal, int Id_Cte, string Conexion)
        {
            Estatus = 0;
            Mensaje = "";
            eAcysCN_Permisos P = new eAcysCN_Permisos();
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                Parametro("@Sucursal", Sucursal);
                Parametro("@Id_Cte", Id_Cte);

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spAcysCN_Permisos", ref dr, Parametros.ToArray(), Valores.ToArray());

                if (dr.HasRows)
                {
                    dr.Read();
                    P.NombreNodo = dr.GetValue(dr.GetOrdinal("NombreNodo")).ToString();
                    P.Id = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id"))); // Id_Acys
                    P.Id_Matriz = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Matriz")));
                    P.Nombre = dr.GetValue(dr.GetOrdinal("Nombre")).ToString();
                    P.EjecCuenta = dr.GetValue(dr.GetOrdinal("EjecCuenta")).ToString();
                    P.LiderTecnico = dr.GetValue(dr.GetOrdinal("LiderTecnico")).ToString();
                    P.DiasCredito = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("DiasCredito")));
                    P.AcuerdoEcon = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("AcuerdoEcon")));
                    P.DatosFisc = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("DatosFisc")));
                    P.AsigRIK = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("AsigRIK")));
                    P.MOV80 = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("MOV80")));
                    P.NivelAcys = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("NivelAcys")));
                    P.TipoCuenta = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("TipoCuenta")));
                    P.FechaUltimaAct = dr.GetValue(dr.GetOrdinal("FechaUltimaAct")).ToString();
                    P.FechaVencimiento = dr.GetValue(dr.GetOrdinal("FechaVencimiento")).ToString();
                    P.Activo = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Activo")));
                    P.Fecha = dr.GetValue(dr.GetOrdinal("Fecha")).ToString();
                    P.FechaInicio = dr.GetValue(dr.GetOrdinal("FechaInicio")).ToString();
                    P.FechaFin = dr.GetValue(dr.GetOrdinal("FechaFin")).ToString();
                    P.Descripcion = dr.GetValue(dr.GetOrdinal("Descripcion")).ToString();
                }
                Estatus = 1;
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                Mensaje = ex.Message.ToString();
                Estatus = -1;
                P = null;
            }
            return P;
        }

        // Consulta de Producto de Acys de Cuentas Nacionales 

        public List<eAcysDet2> spAcysCN_Productos(ref int Estatus, ref string Mensaje,
            int Sucursal, int Id_Cte, int Id_Acs, string Conexion)
        {
            List<eAcysDet2> Lst = new List<eAcysDet2>();
            Estatus = 0;
            int ordFrecMesIni;
            int ordFrecAnioIni;
            Mensaje = "";
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                Parametro("@Sucursal", Sucursal);
                Parametro("@Id_Cte", Id_Cte);
                Parametro("@Id_Asc", Id_Acs);
                /*Aqui traer el Acys para solo traer los productos que no estan en local*/

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spAcysCN_Productos",
                    ref dr, Parametros.ToArray(), Valores.ToArray());

                while (dr.Read())
                {
                    eAcysDet2 P = new eAcysDet2();

                    P.NombreNodo = dr.GetValue(dr.GetOrdinal("NombreNodo")).ToString();
                    P.Id_Matriz = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Matriz")));
                    P.Id_Acys = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_ACYS")));
                    P.Id_Acs = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_ACYS")));
                    P.Nombre = dr.GetValue(dr.GetOrdinal("Nombre")).ToString();

                    P.Id_Prd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Prd")));
                    P.Prd_Descripcion = dr.GetValue(dr.GetOrdinal("Descripcion")).ToString();

                    P.Id_TG = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_TG")));


                    P.Acs_Precio = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Precio")));
                    P.Acs_Cantidad = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Cantidad")));

                    P.Subtotal = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Subtotal")));

                    P.Acs_Frecuencia = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_Frecuencia")));
                    P.Acs_FrecuenciaTipo = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_FrecuenciaTipo")));
                    P.Acs_FrecuenciaTipo_Nombre = dr.GetValue(dr.GetOrdinal("Acs_FrecuenciaTipo_Nombre")).ToString();

                    P.Acs_Lunes = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Lun")));
                    P.Acs_Martes = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Mar")));
                    P.Acs_Miercoles = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Mie")));
                    P.Acs_Jueves = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Jue")));
                    P.Acs_Viernes = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Vie")));
                    P.Acs_Sabado = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Sab")));

                    P.Acs_Documento = dr.GetValue(dr.GetOrdinal("Documento")).ToString();

                    P.Uni_Descripcion = dr.GetValue(dr.GetOrdinal("Unidad")).ToString();
                    //P.Presentacion = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Presentacion")));

                    P.RequiereOC = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("RequiereOC")));

                    ordFrecMesIni = dr.GetOrdinal("Acs_FrecMesIni");
                    ordFrecAnioIni = dr.GetOrdinal("Acs_FrecAnioIni");

                    P.Acs_FrecMesIni = dr.IsDBNull(ordFrecMesIni) ? 0 : Convert.ToInt32(dr.GetValue(ordFrecMesIni));
                    P.Acs_FrecAnioIni = dr.IsDBNull(ordFrecAnioIni) ? 0 : Convert.ToInt32(dr.GetValue(ordFrecAnioIni));

                    P.Prd_Presentacion = dr.GetValue(dr.GetOrdinal("Presentacion")).ToString();
                    // 8JUL2021-RFH
                    P.PrecioLista = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("PrecioLista")));
                    P.Prd_Activo = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Prd_Activo")));
                    Lst.Add(P);
                }
                Estatus = 1;

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                Mensaje = ex.Message.ToString();
                Estatus = -1;
                Lst = null;
            }
            return Lst;
        }

        //

    }
}