using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using CapaEntidad;
using System.Configuration;
using System.Globalization;

namespace CapaDatos
{
    public class CD_Queja
    {
        public void GuardaQueja(Sesion sesion, Queja queja, DataTable objdtTabla, List<Documento> LstDocumentos, ref int respuesta)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(sesion.Emp_Cnx);
            try
            {
                CapaDatos.StartTrans();
                SqlCommand sqlcmd;
                SqlCommand sqlcmdcen;

                #region Inserta Queja
                string[] ParametrosQueja = {
                                             "@Id_Emp"
                                            ,"@Id_Cd"
                                            ,"@Id_Queja"
                                            ,"@Id_Cliente"
                                            ,"@Nom_Cliente"
                                            ,"@Correo"
                                            ,"@Id_Ctedirecto"
                                            ,"@Nom_Ctedirecto"
                                            ,"@Id_TQueja"
                                            ,"@Id_Prioridad"
                                            ,"@Fec_Evento"
                                            ,"@DondeOcurrio"
                                            ,"@Motivos"
                                            ,"@OtroMotivo"
                                            ,"@Descripcion"
                                            ,"@Embarque"
                                            ,"@FechaEmbarque"
                                            ,"@Guia_Flete"
                                            ,"@Compania_Trans"
                                            ,"@Placas_Unidad"
                                            ,"@Nom_Chofer"
                                            ,"@Fec_Embarque"
                                            ,"@Fec_Arribo"
                                            ,"@Id_Rem"
                                      };
                object[] ValoresQueja = {
                                         sesion.Id_Emp
                                        ,sesion.Id_Cd
                                        ,queja.Id_Queja
                                        ,queja.Id_Cliente
                                        ,queja.Nom_Cliente
                                        ,queja.Correo
                                        ,queja.Id_CteDirecto
                                        ,queja.Nom_CteDirecto
                                        ,queja.Id_TQueja
                                        ,queja.Id_Prioridad
                                        ,queja.Fec_Evento
                                        ,queja.DondeOcurrio
                                        ,queja.Motivos
                                        ,queja.OtroMotivo
                                        ,queja.Descripcion
                                        ,queja.Embarque
                                        ,queja.FechaEmbarque
                                        ,queja.Guia_Flete
                                        ,queja.NomComTransporte
                                        ,queja.Placas
                                        ,queja.NomChofer
                                        ,queja.Fec_Embarque
                                        ,null
                                        ,queja.Id_Rem
                                   };

                sqlcmd = CapaDatos.GenerarSqlCommand("sp_InsertaCapQueja", ref respuesta, ParametrosQueja, ValoresQueja);
                queja.Id_Queja = respuesta;
                if (respuesta < 0)
                {
                    return;
                }
                #endregion

                #region Inserta Productos
                string[] ParametrosProd = new string[]{
                                           "@Id_Emp",
                                           "@Id_Cd",
                                           "@Id_Prd",
                                           "@Descripcion",
                                           "@Presentacion",
                                           "@Cantidad",
                                           "@Uni_Empaque",
                                           "@Lote",
                                           "@Fec_Fabricacion",
                                           "@Fec_Caducidad",
                                           "@Marca",
                                           "@Precio_AAA",
                                           "@Num_Fac",
                                           "@Nom_Prov",
                                           "@Mantener",
                                           "@Rem_Trans",
                                           "@Id_Doc",
                                           "@Costo_Estandar",
                                           "@Nota_Credito",
                                           "@Id_Queja",
                                           "@ConservarProd"
                                           };
                for (int x = 0; x < objdtTabla.Rows.Count; x++)
                {
                    object[] ValoresProd = new object[]{
                                            Convert.ToInt32(objdtTabla.Rows[x][0]),
                                            Convert.ToInt32(objdtTabla.Rows[x][1]),
                                            Convert.ToInt64(objdtTabla.Rows[x][2]),
                                            objdtTabla.Rows[x][3],
                                            objdtTabla.Rows[x][4],
                                            Convert.ToInt32(objdtTabla.Rows[x][5]),
                                            Convert.ToDouble(objdtTabla.Rows[x][6]),
                                            objdtTabla.Rows[x][7],
                                            Convert.ToDateTime(objdtTabla.Rows[x][8]),
                                            Convert.ToDateTime(objdtTabla.Rows[x][9]),
                                            objdtTabla.Rows[x][10],
                                            Convert.ToDouble(objdtTabla.Rows[x][11]),
                                            Convert.ToInt32(objdtTabla.Rows[x][12]),
                                            objdtTabla.Rows[x][13],
                                            1,
                                            0,
                                            0,
                                            Convert.ToDouble(objdtTabla.Rows[x][15]),
                                            0,
                                            queja.Id_Queja,
                                            objdtTabla.Rows[x][14]
                                   };
                    sqlcmd = CapaDatos.GenerarSqlCommand("sp_InsertaCapProductos", ref respuesta, ParametrosProd, ValoresProd);
                }
                #endregion

                #region Inserta documentos
                int Folio = 0;
                string[] Parametros = new string[]{
                                                "@Id_Emp",
                                                "@Id_Cd",
                                                "@Nombre",
                                                "@Formato",
                                                "@Tamaño",
                                                "@Archivo",
                                                "@TipoDoc",
                                                "@Id_Queja"
                                           };
                for (int x = 0; x < LstDocumentos.Count; x++)
                {
                    object[] Valores = new object[]{
                                            sesion.Id_Emp,
                                            sesion.Id_Cd,
                                            LstDocumentos[x].Doc_Nombre,
                                            LstDocumentos[x].Formato,
                                            LstDocumentos[x].Tamano,
                                            LstDocumentos[x].Archivo,
                                            LstDocumentos[x].TipoDoc,
                                            queja.Id_Queja
                                         };
                    sqlcmd = CapaDatos.GenerarSqlCommand("sp_InsertaCapDocumentos", ref Folio, Parametros, Valores);

                    SqlConnection ConexionCentral = new SqlConnection();
                    ConexionCentral.ConnectionString = "Data Source=40.84.229.61;Initial Catalog=siancentral;User ID=sa;Password=4dmK3yQu1m";
                    //ConexionCentral.ConnectionString = "Data Source=207.248.253.106;Initial Catalog=siancentral;User ID=sa;Password=4dmK3yQu1m";
                    ConexionCentral.Open();

                    string Sqlcen = @"Insert into Capdocumentos Values(@Id_Emp, @Id_Cd, @Nombre, @Formato, @Tamano, @Archivo, @TipoDoc, @Id_Queja, @Id_Det)";
                    sqlcmdcen = new SqlCommand(Sqlcen, ConexionCentral);
                    sqlcmdcen.Parameters.AddWithValue("@Id_Emp", sesion.Id_Emp);
                    sqlcmdcen.Parameters.AddWithValue("@Id_Cd", sesion.Id_Cd);
                    sqlcmdcen.Parameters.AddWithValue("@Nombre", LstDocumentos[x].Doc_Nombre);
                    sqlcmdcen.Parameters.AddWithValue("@Formato", LstDocumentos[x].Formato);
                    sqlcmdcen.Parameters.AddWithValue("@Tamano", LstDocumentos[x].Tamano);
                    sqlcmdcen.Parameters.AddWithValue("@Archivo", LstDocumentos[x].Archivo);
                    sqlcmdcen.Parameters.AddWithValue("@TipoDoc", LstDocumentos[x].TipoDoc);
                    sqlcmdcen.Parameters.AddWithValue("@Id_Queja", queja.Id_Queja);
                    sqlcmdcen.Parameters.AddWithValue("@Id_Det", Folio);

                    sqlcmdcen.ExecuteNonQuery();
                    ConexionCentral.Close();
                }
                respuesta = queja.Id_Queja;
                #endregion

                #region Actualiza Comentarios Remisión
                if (queja.Id_Rem != 0)
                {
                    string[] ParametrosRem = new string[]{
                                                "@Id_Emp",
                                                "@Id_Cd",
                                                "@Id_Rem",
                                                "@Rem_Nota"
                                           };

                    object[] ValoresRem = new object[]{
                                            sesion.Id_Emp,
                                            sesion.Id_Cd,
                                            queja.Id_Rem,
                                            "Esta remisión fue creada automáticamente y pertenece a la queja #" + queja.Id_Queja
                                         };
                    sqlcmd = CapaDatos.GenerarSqlCommand("sp_ActualizaRemision", ref Folio, ParametrosRem, ValoresRem);
                }
                #endregion

                CapaDatos.CommitTrans();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                CapaDatos.RollBackTrans();
                #region Eliminar Remision
                CapaDatos.CD_Datos CD = new CapaDatos.CD_Datos(sesion.Emp_Cnx);
                int resultado = 0;
                SqlCommand sql;
                CD.StartTrans();
                string[] Parametros = new string[]{
                                                "@Id_Emp",
                                                "@Id_Cd",
                                                "@Id_Rem"
                                           };

                object[] Valores = new object[]{
                                            sesion.Id_Emp,
                                            sesion.Id_Cd,
                                            queja.Id_Rem
                                         };
                sql = CD.GenerarSqlCommand("sp_EliminarRemision", ref resultado, Parametros, Valores);

                CD.CommitTrans();
                CD.LimpiarSqlcommand(ref sql);

                #endregion
                throw ex;
            }
        }

        public void ConsultaQueja(Sesion sesion, ref Queja queja, ref List<Documento> lstDocumentos, ref List<Producto> lstProductos)
        {
            try
            {
                SqlDataReader dr = null;
                Documento documento;
                Producto producto;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(sesion.Emp_Cnx);

                string[] Parametros = { "@Id_Queja" };
                object[] Valores = { queja.Id_Queja };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapQueja_Consulta", ref dr, Parametros, Valores);

                while (dr.Read())
                {

                    queja.Id_Cliente = dr.GetInt32(dr.GetOrdinal("Id_Cliente"));
                    queja.Nom_Cliente = dr.GetString(dr.GetOrdinal("Cte_Nom"));
                    queja.Correo = dr.GetString(dr.GetOrdinal("Correo"));
                    queja.Id_TQueja = dr.GetInt32(dr.GetOrdinal("Id_TQueja"));
                    queja.Descripcion = dr.GetString(dr.GetOrdinal("Descripcion"));
                }
                dr.Close();
                sqlcmd = CapaDatos.GenerarSqlCommand("spCapDocumentos_Consulta", ref dr, Parametros, Valores);
                while (dr.Read())
                {
                    documento = new Documento();
                    documento.Id_Doc = dr.GetInt32(dr.GetOrdinal("ID"));
                    documento.Doc_Nombre = dr.GetString(dr.GetOrdinal("Nombre"));
                    documento.Formato = dr.GetString(dr.GetOrdinal("Tipo"));
                    documento.Tamano = dr.GetInt64(dr.GetOrdinal("Tamano"));
                    documento.TipoDoc = dr.GetString(dr.GetOrdinal("TipoDoc"));
                    documento.Archivo = (byte[])dr.GetValue(dr.GetOrdinal("Archivo"));
                    lstDocumentos.Add(documento);
                }

                dr.Close();
                sqlcmd = CapaDatos.GenerarSqlCommand("spCapProductos_Consulta", ref dr, Parametros, Valores);
                while (dr.Read())
                {
                    producto = new Producto();

                    producto.Id_Prd = dr.GetInt64(dr.GetOrdinal("Id_Prd"));
                    producto.Descripcion = dr.GetString(dr.GetOrdinal("Descripcion"));
                    producto.Cantidad = dr.GetInt32(dr.GetOrdinal("Cantidad"));
                    producto.Presentacion = dr.GetString(dr.GetOrdinal("Presentacion"));
                    producto.Lote = dr.GetString(dr.GetOrdinal("Lote"));
                    producto.Fabricacion = dr.GetDateTime(dr.GetOrdinal("Fec_Fabricacion"));
                    producto.Caducidad = dr.GetDateTime(dr.GetOrdinal("Fec_Fabricacion"));
                    producto.Marca = dr.GetString(dr.GetOrdinal("Marca"));
                    producto.Costo = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Precio_AAA")));
                    producto.Num_Fac = dr.GetInt32(dr.GetOrdinal("Num_Fac"));
                    producto.Nom_Prov = dr.GetString(dr.GetOrdinal("Nom_Prov"));
                    producto.Id_motivo = dr.GetInt32(dr.GetOrdinal("Id_Motivo"));
                    producto.Motivo = dr.GetString(dr.GetOrdinal("Motivo"));
                    producto.ConservarProd = dr.GetBoolean(dr.GetOrdinal("ConservarProd"));

                    lstProductos.Add(producto);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaQuejas(Sesion sesion, string correo, ref List<Queja> LstQuejas, int Id_Queja, int TipoQueja, string Embarque, string Flete, DateTime Fec_Inicio, DateTime Fec_Fin)
        {
            try
            {
                SqlDataReader dr = null;
                Queja queja;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(sesion.Emp_Cnx);

                string[] Parametros = {
                                         "@correo",
                                         "@Id_Queja",
                                         "@TipoQueja",
                                         "@Embarque",
                                         "@Flete",
                                         "@Fec_Ini",
                                         "@Fec_Fin"

                                      };
                object[] Valores = {
                                       correo,
                                       Id_Queja,
                                       TipoQueja,
                                       Embarque,
                                       Flete,
                                       Fec_Inicio == DateTime.MinValue ? (object) null: Fec_Inicio,
                                       Fec_Fin == DateTime.MinValue ? (object) null: Fec_Fin
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spLstQueja_Consulta", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    queja = new Queja();
                    queja.Id_Emp = dr.GetInt32(dr.GetOrdinal("Id_Emp"));
                    queja.Id_Cd = dr.GetInt32(dr.GetOrdinal("Id_Cd"));
                    queja.Id_Queja = dr.GetInt32(dr.GetOrdinal("Id_Queja"));
                    queja.Nom_Cliente = dr.GetString(dr.GetOrdinal("Cte_Nom"));
                    queja.TipoQueja = dr.GetString(dr.GetOrdinal("Tipo_Queja"));
                    queja.Descripcion = dr.GetString(dr.GetOrdinal("Descripcion"));
                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("FechaCreacion")))) queja.FechaCreacion = null; else queja.FechaCreacion = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("FechaCreacion")));
                    queja.Embarque = dr.IsDBNull(dr.GetOrdinal("Embarque")) ? "" : dr.GetValue(dr.GetOrdinal("Embarque")).ToString();
                    queja.Guia_Flete = dr.IsDBNull(dr.GetOrdinal("Guia_Flete")) ? "" : dr.GetValue(dr.GetOrdinal("Guia_Flete")).ToString();
                    LstQuejas.Add(queja);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaQuejas(Sesion sesion, ref Queja queja, ref List<Producto> lstProductos, ref List<Documento> lstDocumentos)
        {
            try
            {
                SqlDataReader dr = null;
                Producto producto;
                Documento documento;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(sesion.Emp_Cnx);

                string[] Parametros = { "@Id_Emp",
                                        "@Id_Cd",
                                        "@Id_Queja"
                                      };
                object[] Valores = {
                                       sesion.Id_Emp,
                                       sesion.Id_Cd,
                                       queja.Id_Queja
                                   };

                //Consulta cabecero de Queja
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapQueja_Consulta", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    queja.Id_Queja = dr.GetInt32(dr.GetOrdinal("Id_Queja"));
                    queja.Id_Cd = dr.GetInt32(dr.GetOrdinal("Id_Cd"));
                    queja.Nom_Cliente = dr.GetString(dr.GetOrdinal("Cte_Nom"));
                    queja.Id_CteDirecto = dr.GetInt32(dr.GetOrdinal("Id_Ctedirecto"));
                    queja.Nom_CteDirecto = dr.GetString(dr.GetOrdinal("Nom_Ctedirecto"));
                    queja.Correo = dr.GetString(dr.GetOrdinal("Correo"));
                    queja.Id_Prioridad = dr.GetInt32(dr.GetOrdinal("Id_Prioridad"));
                    queja.Id_TQueja = dr.GetInt32(dr.GetOrdinal("Id_TQueja"));
                    queja.Embarque = dr.GetString(dr.GetOrdinal("Embarque"));
                    queja.FechaEmbarque = dr.GetDateTime(dr.GetOrdinal("FechaEmbarque"));
                    queja.Guia_Flete = dr.GetString(dr.GetOrdinal("Guia_Flete"));
                    queja.Descripcion = dr.GetString(dr.GetOrdinal("Descripcion"));
                    queja.Id_Estatus = dr.GetInt32(dr.GetOrdinal("Id_Estado"));
                    queja.Fec_Evento = dr.GetDateTime(dr.GetOrdinal("Fec_Evento"));
                    queja.DondeOcurrio = dr.GetInt32(dr.GetOrdinal("DondeOcurrio"));
                    queja.Motivos = dr.GetString(dr.GetOrdinal("Motivos"));
                    queja.OtroMotivo = dr.GetString(dr.GetOrdinal("OtroMotivo"));
                    queja.NomComTransporte = dr.GetString(dr.GetOrdinal("Compania_Trans"));
                    queja.Placas = dr.GetString(dr.GetOrdinal("Placas_Unidad"));
                    queja.NomChofer = dr.GetString(dr.GetOrdinal("Nom_Chofer"));

                    if (dr.IsDBNull(dr.GetOrdinal("Fec_Embarque")))
                        queja.Fec_Embarque = dr.GetDateTime(dr.GetOrdinal("Fec_Embarque"));
                    else
                        queja.Fec_Embarque = Convert.ToDateTime("2000/01/01 12:00:00.000");

                    if (dr.IsDBNull(dr.GetOrdinal("Fec_Arribo")))
                        queja.Fec_Arribo = dr.GetDateTime(dr.GetOrdinal("Fec_Embarque"));
                    else
                        queja.Fec_Arribo = Convert.ToDateTime("2000/01/01 12:00:00.000");

                    //RBM Enero 2021
                    //Se agrega el campo Id_Rem para mostrar en el formulario
                    //Inicio
                    queja.Id_Rem = dr.GetInt32(dr.GetOrdinal("Id_Rem"));
                    //Fin
                }

                //Consulta productos de la queja 
                dr.Close();
                sqlcmd = CapaDatos.GenerarSqlCommand("spCapProductos_Consulta", ref dr, Parametros, Valores);
                while (dr.Read())
                {
                    producto = new Producto();
                    producto.Id_Emp = dr.GetInt32(dr.GetOrdinal("Id_Emp"));
                    producto.Id_Cd = dr.GetInt32(dr.GetOrdinal("Id_Cd"));
                    producto.Id_Prd = dr.GetInt64(dr.GetOrdinal("Id_Prd"));
                    producto.Descripcion = dr.GetString(dr.GetOrdinal("Descripcion"));
                    producto.Presentacion = dr.GetString(dr.GetOrdinal("Presentacion"));
                    producto.Cantidad = dr.GetInt32(dr.GetOrdinal("Cantidad"));
                    producto.Prd_UniEmp = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Uni_Empaque"))) ? 0 : float.Parse(dr.GetValue(dr.GetOrdinal("Uni_Empaque")).ToString());
                    producto.Lote = dr.IsDBNull(dr.GetOrdinal("Lote")) ? "0" : dr.GetValue(dr.GetOrdinal("Lote")).ToString();
                    producto.Fabricacion = dr.GetDateTime(dr.GetOrdinal("Fec_Fabricacion"));
                    producto.Caducidad = dr.GetDateTime(dr.GetOrdinal("Fec_Caducidad"));
                    producto.Marca = dr.IsDBNull(dr.GetOrdinal("Marca")) ? "" : dr.GetValue(dr.GetOrdinal("Marca")).ToString();
                    producto.Costo = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Precio_AAA")));
                    producto.Costoestandar = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Costo_Estandar")));
                    producto.Num_Fac = dr.GetInt32(dr.GetOrdinal("Num_Fac"));
                    producto.Nom_Prov = producto.Marca = dr.IsDBNull(dr.GetOrdinal("Nom_Prov")) ? "" : dr.GetValue(dr.GetOrdinal("Nom_Prov")).ToString();
                    producto.ConservarProd = dr.GetBoolean(dr.GetOrdinal("ConservarProd"));
                    lstProductos.Add(producto);
                }

                //Consulta documentos de la queja 
                dr.Close();
                sqlcmd = CapaDatos.GenerarSqlCommand("spCapDocumentos_Consulta", ref dr, Parametros, Valores);
                while (dr.Read())
                {
                    documento = new Documento();
                    documento.Id_Emp = dr.GetInt32(dr.GetOrdinal("Id_Emp"));
                    documento.Id_Cd = dr.GetInt32(dr.GetOrdinal("Id_Cd"));
                    documento.Id_Doc = dr.GetInt32(dr.GetOrdinal("Id_Doc"));
                    documento.Doc_Nombre = dr.GetString(dr.GetOrdinal("Doc_Nombre"));
                    documento.Formato = dr.GetString(dr.GetOrdinal("Formato"));
                    documento.Tamano = dr.GetInt64(dr.GetOrdinal("Tamano"));
                    documento.TipoDoc = dr.GetString(dr.GetOrdinal("TipoDoc"));
                    documento.Archivo = (byte[])dr.GetValue(dr.GetOrdinal("Archivo"));
                    lstDocumentos.Add(documento);


                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ActualizaQueja(Sesion sesion, ref Queja queja, DataTable objdtTablaProd, ref List<Documento> LstDocumentos, ref int respuesta)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(sesion.Emp_Cnx);
            try
            {
                CapaDatos.StartTrans();
                SqlCommand sqlcmd = new SqlCommand();

                #region Inserta Productos
                string[] ParametrosProd = new string[]{
                                           "@Id_Emp",
                                           "@Id_Cd",
                                           "@Id_Prd",
                                           "@Descripcion",
                                           "@Presentacion",
                                           "@Cantidad",
                                           "@Uni_Empaque",
                                           "@Lote",
                                           "@Fec_Fabricacion",
                                           "@Fec_Caducidad",
                                           "@Marca",
                                           "@Precio_AAA",
                                           "@Num_Fac",
                                           "@Nom_Prov",
                                           "@Mantener",
                                           "@Rem_Trans",
                                           "@Id_Doc",
                                           "@Costo_Estandar",
                                           "@Nota_Credito",
                                           "@Id_Queja",
                                           "@ConservarProd"
                                           };
                for (int x = 0; x < objdtTablaProd.Rows.Count; x++)
                {
                    object[] ValoresProd = new object[]{
                                            Convert.ToInt32(objdtTablaProd.Rows[x][0]),
                                            Convert.ToInt32(objdtTablaProd.Rows[x][1]),
                                            Convert.ToInt64(objdtTablaProd.Rows[x][2]),
                                            objdtTablaProd.Rows[x][3],
                                            0,//objdtTablaProd.Rows[x][4],
                                            Convert.ToInt32(objdtTablaProd.Rows[x][5]),
                                            Convert.ToInt32(objdtTablaProd.Rows[x][6]),
                                            objdtTablaProd.Rows[x][7],
                                            Convert.ToDateTime(objdtTablaProd.Rows[x][8]),
                                            Convert.ToDateTime(objdtTablaProd.Rows[x][9]),
                                            objdtTablaProd.Rows[x][10],
                                            Convert.ToDouble(objdtTablaProd.Rows[x][11]),
                                            Convert.ToInt32(objdtTablaProd.Rows[x][12]),
                                            objdtTablaProd.Rows[x][13],
                                            1,
                                            0,
                                            0,
                                            0,
                                            0,
                                            queja.Id_Queja,
                                            objdtTablaProd.Rows[x][14]
                                   };
                    sqlcmd = CapaDatos.GenerarSqlCommand("sp_InsertaCapProductos", ref respuesta, ParametrosProd, ValoresProd);
                }
                #endregion

                #region Inserta documentos
                int Folio = 0;
                string[] Parametros = new string[]{
                                                "@Id_Emp",
                                                "@Id_Cd",
                                                "@Nombre",
                                                "@Formato",
                                                "@Tamaño",
                                                "@Archivo",
                                                "@TipoDoc",
                                                "@Id_Queja"
                                           };
                for (int x = 0; x < LstDocumentos.Count; x++)
                {
                    object[] Valores = new object[]{
                                            sesion.Id_Emp,
                                            sesion.Id_Cd,
                                            LstDocumentos[x].Doc_Nombre,
                                            LstDocumentos[x].Formato,
                                            LstDocumentos[x].Tamano,
                                            LstDocumentos[x].Archivo,
                                            LstDocumentos[x].TipoDoc,
                                            queja.Id_Queja,
                                         };
                    sqlcmd = CapaDatos.GenerarSqlCommand("sp_InsertaCapDocumentos", ref Folio, Parametros, Valores);

                    SqlConnection ConexionCentral = new SqlConnection();
                    ConexionCentral.ConnectionString = "Data Source=40.84.229.61;Initial Catalog=siancentral;User ID=sa;Password=4dmK3yQu1m";
                    //ConexionCentral.ConnectionString = "Data Source=207.248.253.106;Initial Catalog=siancentral;User ID=sa;Password=4dmK3yQu1m";
                    ConexionCentral.Open();

                    string Sqlcen = @"Insert into Capdocumentos Values(@Id_Emp, @Id_Cd, @Nombre, @Formato, @Tamano, @Archivo, @TipoDoc, @Id_Queja, @Id_Det)";
                    sqlcmd = new SqlCommand(Sqlcen, ConexionCentral);
                    sqlcmd.Parameters.AddWithValue("@Id_Emp", sesion.Id_Emp);
                    sqlcmd.Parameters.AddWithValue("@Id_Cd", sesion.Id_Cd);
                    sqlcmd.Parameters.AddWithValue("@Nombre", LstDocumentos[x].Doc_Nombre);
                    sqlcmd.Parameters.AddWithValue("@Formato", LstDocumentos[x].Formato);
                    sqlcmd.Parameters.AddWithValue("@Tamano", LstDocumentos[x].Tamano);
                    sqlcmd.Parameters.AddWithValue("@Archivo", LstDocumentos[x].Archivo);
                    sqlcmd.Parameters.AddWithValue("@TipoDoc", LstDocumentos[x].TipoDoc);
                    sqlcmd.Parameters.AddWithValue("@Id_Queja", queja.Id_Queja);
                    sqlcmd.Parameters.AddWithValue("@Id_Det", Folio);

                    sqlcmd.ExecuteNonQuery();
                    ConexionCentral.Close();
                }
                respuesta = queja.Id_Queja;
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

        public void ConsultaMotivos(Sesion Sesion, ref DataSet dsMotivos, int Id_tQueja)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Sesion.Emp_Cnx);

                string[] Parametros = { "@Id_tQueja" };
                object[] Valores = { Id_tQueja };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatMotivos_Combo", ref dsMotivos, Parametros, Valores);

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void EliminarDocumento(Sesion sesion, int Id_Doc)
        {
            try
            {
                SqlCommand sqlcmd = new SqlCommand();

                SqlConnection Conexion = new SqlConnection();
                Conexion.ConnectionString = @ConfigurationManager.AppSettings.Get("strConnection");
                Conexion.Open();

                string Sql = @"Delete from Capdocumentos where Id_Emp = @Id_Emp and Id_Cd = @Id_Cd and ID = @Id_Doc";
                sqlcmd = new SqlCommand(Sql, Conexion);
                sqlcmd.Parameters.AddWithValue("@Id_Emp", sesion.Id_Emp);
                sqlcmd.Parameters.AddWithValue("@Id_Cd", sesion.Id_Cd);
                sqlcmd.Parameters.AddWithValue("@Id_Doc", Id_Doc);

                sqlcmd.ExecuteNonQuery();
                Conexion.Close();

                SqlConnection ConexionCentral = new SqlConnection();
                ConexionCentral.ConnectionString = "Data Source=40.84.229.61;Initial Catalog=siancentral;User ID=sa;Password=4dmK3yQu1m";
                //ConexionCentral.ConnectionString = "Data Source=207.248.253.106.253.106;Initial Catalog=siancentral;User ID=sa;Password=4dmK3yQu1m";
                ConexionCentral.Open();

                string Sqlcen = @"Delete from Capdocumentos where Id_Emp = @Id_Emp and Id_Cd = @Id_Cd and Id_Det = @Id_Doc";
                sqlcmd = new SqlCommand(Sqlcen, ConexionCentral);
                sqlcmd.Parameters.AddWithValue("@Id_Emp", sesion.Id_Emp);
                sqlcmd.Parameters.AddWithValue("@Id_Cd", sesion.Id_Cd);
                sqlcmd.Parameters.AddWithValue("@Id_Doc", Id_Doc);

                sqlcmd.ExecuteNonQuery();
                ConexionCentral.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void EliminarProducto(Sesion sesion, int Id_Queja, long Id_Prd)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(sesion.Emp_Cnx);
                int respuesta = 0;
                string[] ParametrosQueja = {
                                               "@Id_Emp",
                                               "@Id_Cd",
                                               "@Id_Queja",
                                               "@Id_Prd"
                                           };
                object[] ValoresQueja = {
                                            sesion.Id_Emp,
                                            sesion.Id_Cd,
                                            Id_Queja,
                                            Id_Prd
                                        };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("SpElimina_Producto", ref respuesta, ParametrosQueja, ValoresQueja);

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}