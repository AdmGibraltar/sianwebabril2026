using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using CapaModelo;
using CapaNegocios;
using CapaDatos;
using CapaNegocios.FlujosDeEstado;

namespace CapaNegocios
{
    /// <summary>
    /// Clase de manejo de operaciones de reglas de negocio concerniente al instrumento de propuestas tecno-económicas
    /// </summary>
    public class CN_CrmPropuestaTecnoEconomica
    {
        /// <summary>
        /// Cambia el estado de la valuación de interés para reflejar que la propuesta asociada a dicha valuación ha sido aceptada. Se desencadena la generación del ACYS a partir de los productos asociados a la valuación.
        /// </summary>
        /// <param name="s">Sesión del operador</param>
        /// <param name="idVal">Identificador de la valuación a la cual se aceptará su propuesta</param>
        /// 
        public Acys Aceptar(Sesion s, int idVal)
        {

            try
            {
                Acys acys = null;
                using (var businessTransaction = CN_FabricaTransaccionNegocios.Default(s))
                {
                    businessTransaction.Begin();
                    //TODO: ejecutar el bloque como una sola transacción
                    CN_CapValuacionProyecto cnCapValuacionProyecto = new CN_CapValuacionProyecto();
                    //Se actualiza la valuación para reflejar que la propuesta asociada ha sido aceptada.
                    cnCapValuacionProyecto.ActualizarAPropuestaAceptada(s, idVal, businessTransaction);

                    CN_CrmValuacionOportunidades cnCrmValuacionOportunidades = new CN_CrmValuacionOportunidades();
                    var valuacion = cnCapValuacionProyecto.Obtener(s, idVal, businessTransaction);
                    var proyectos = cnCrmValuacionOportunidades.ObtenerPorValuacion(s, valuacion.Id_Cte, idVal, businessTransaction);

                    CN_CrmOportunidad cnCrmOportunidad = new CN_CrmOportunidad();
                    CapaDatos.CD_CrmOportunidadesProductos cdCrmOportunidadesProductos = new CapaDatos.CD_CrmOportunidadesProductos();

                    /* ORIGNAL 
                    //Varios ACYS?
                    foreach(var p in proyectos)
                    {
                        //Validar el flujo
                        var proyecto = cnCrmOportunidad.ObtenerPorId(s, p.Id_Op, businessTransaction);
                        CapaNegocios.FlujosDeEstado.CRM.ProyectoStateMachine psm = new FlujosDeEstado.CRM.ProyectoStateMachine(proyecto, s);
                        psm.AlCrearAcys = (a) =>
                        {
                            acys = a;
                        };
                        psm.Transaction = businessTransaction;
                        var productos = cdCrmOportunidadesProductos.ConsultarPorOportunidadYCliente(s.Id_Emp, s.Id_Cd, proyecto.Id_Op, valuacion.Id_Cte, businessTransaction.DataContext);
                        proyecto.CrmOportunidadesProducto = productos;
                        
                        psm.Update();
                    }
                    */

                    foreach (var p in proyectos)
                    {
                        //Validar el flujo
                        var proyecto = cnCrmOportunidad.ObtenerPorId(s, p.Id_Op, businessTransaction);
                        CapaNegocios.FlujosDeEstado.CRM.ProyectoStateMachine psm = new FlujosDeEstado.CRM.ProyectoStateMachine(proyecto, s);
                        psm.AlCrearAcys = (a) =>
                        {
                            acys = a;
                        };
                        psm.Transaction = businessTransaction;

                        var productos = cdCrmOportunidadesProductos.ConsultarPorOportunidadYCliente(s.Id_Emp, s.Id_Cd, proyecto.Id_Op, valuacion.Id_Cte, businessTransaction.DataContext);
                        proyecto.CrmOportunidadesProducto = productos;

                        psm.Update(); //RFH

                    }

                    businessTransaction.Commit();
                }

                return acys;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        protected List<AcysPrd> ObtenerListadoDeProductos(CapValProyecto valuacion, Sesion s, IBusinessTransaction ibt)
        {
            List<AcysPrd> productos = new List<AcysPrd>();

            try
            {
                CN_CapValProyecto cnCapValProyecto = new CN_CapValProyecto();
                var dets = cnCapValProyecto.ObtenerDetalle(s, valuacion.Id_Vap, ibt);

                CN_CatProducto cnCatProducto = new CN_CatProducto();
                foreach (var cvpd in dets)
                {
                    var producto = cnCatProducto.ObtenerPorId(s, cvpd.Id_Prd, ibt);
                    AcysPrd prd = new AcysPrd()
                    {
                        Acs_Doc = string.Empty,
                        Acys_Cantidad = cvpd.Vap_Cantidad,
                        Acys_CantTotal = cvpd.Vap_Cantidad,
                        Acys_FechaFin = DateTime.Now,
                        Acys_FechaInicio = DateTime.Now,
                        Acys_Frecuencia = 1,
                        Acys_Jueves = false,
                        Acys_Lunes = false,
                        Acys_Martes = false,
                        Acys_Miercoles = false,
                        Acys_Sabado = false,
                        Acys_UltACtp = 0,
                        Acys_UltSCtp = 0,
                        Acys_Viernes = false,
                        Id_Prd = cvpd.Id_Prd,
                        Id_TG = 0,
                        Prd_Descripcion = producto.Prd_Descripcion,
                        Prd_Precio = cvpd.Vap_Precio,
                        Prd_Presentacion = producto.Prd_Presentacion,
                        Prd_UniNom = producto.Prd_UniNe
                    };
                    productos.Add(prd);
                }

            }
            catch (Exception ex)
            {
                productos = null;
            }

            return productos;
        }


        public Acys Aceptar_Ver2(Sesion s, int idVal, string conexion)
        {
            int Id_Op = 0;
            int Id_Cte = 0;

            CN_CapValuacionProyecto cnCapValuacionProyecto = new CN_CapValuacionProyecto();

            int verificador = 0;
            Acys ACYS = new Acys();

            try
            {
                CN_CrmValuacionOportunidades cnCrmValuacionOportunidades = new CN_CrmValuacionOportunidades();
                CapaModelo.CapValProyecto valuacion = new CapaModelo.CapValProyecto();
                List<CrmValuacionOportunidade> LstProyectos = new List<CrmValuacionOportunidade>();

                CN_CrmOportunidad cnCrmOportunidad = new CN_CrmOportunidad();
                CapaDatos.CD_CrmOportunidadesProductos cdCrmOportunidadesProductos = new CapaDatos.CD_CrmOportunidadesProductos();

                valuacion = cnCapValuacionProyecto.Obtener(s, idVal);

                CN_CapAcys cnCapAcys = new CN_CapAcys();

                GenerarACYS_Ver2(s, valuacion, ref ACYS);

                // SIEMPRE REGRESARA UN PROYECTO
                LstProyectos = cnCrmValuacionOportunidades.ObtenerPorValuacion_Ver2(s, valuacion.Id_Cte, idVal);
                Id_Cte = valuacion.Id_Cte;
                if (valuacion.Id_Ter.Value == 0)
                {
                    Crm_Proyectos datos = new Crm_Proyectos();
                    datos.Id_Emp = s.Id_Emp;
                    datos.Id_Cd = s.Id_Cd;
                    datos.Id_Cte = valuacion.Id_Cte;
                    datos.Id_Rik = s.Id_Rik;

                    List<Crm_Proyectos> lista = new List<Crm_Proyectos>();
                    CD_Crm_Proyecto CD = new CD_Crm_Proyecto();
                    CD.ObtenerTerrProyectos(datos, ref lista, conexion);

                    if (lista.Count == 0)
                    {
                        ACYS = null;
                        throw new Exception("Info: El Acuerdo no tiene Registrado un Territorio Válido, Favor de Revisar para Continuar.");
                    }
                    else
                    {
                        valuacion.Id_Ter = lista.First().Id_Ter;
                    }
                }

                CapaDatos.CD_CrmOportunidad cdrmOportunidad = new CapaDatos.CD_CrmOportunidad();
                foreach (var Proyecto in LstProyectos)
                {


                    Id_Op = Proyecto.Id_Op;
                    CrmOportunidade proyecto = new CrmOportunidade();
                    proyecto = cnCrmOportunidad.ObtenerPorId(s, Proyecto.Id_Op);

                    // Consulta Los Productos del Proyecto
                    List<AcysPrd> productos = new List<AcysPrd>();
                    productos = this.ObtenerListadoDeProductos(valuacion, s);

                    CapaNegocios.CN_CapAcys CN_CapAcys_ = new CapaNegocios.CN_CapAcys();

                    int Acys_Id_Acs = 0;
                    int Acys_Id_AcsVersion = 0;
                    int Acys_Id_Ter = 0;

                    Acys_Id_Ter = valuacion.Id_Ter.Value;

                    CN_CapAcys_.ClieneTieneACYS_ByCteTer(s, s.Id_Emp, s.Id_Cd, valuacion.Id_Cte, valuacion.Id_Ter.Value,
                        ref Acys_Id_Acs, ref Acys_Id_AcsVersion);

                    if (Acys_Id_Acs > 0)
                    {
                        // TIENE ACYS - añada los productos o actualiza lo existentes

                        CN_CapAcysDet cnCapAcysDet = new CN_CapAcysDet();
                        //var acysDeCliente = cnCapAcys.ObtenerParaCliente(Sesion, valuacion.Id_Cte, valuacion.Id_Ter.Value, Transaction);

                        ACYS.Id_Acs = Acys_Id_Acs;
                        // 3SEP-2020 
                        ACYS.Id_AcsVersion = Acys_Id_AcsVersion;
                        ACYS.Id_Ter = valuacion.Id_Ter.Value;
                        List<CapaEntidad.eCapAcysDet> lstProductosExistentes = new List<CapaEntidad.eCapAcysDet>();

                        // Consulta Producto en Detalle de ACyS
                        lstProductosExistentes = cnCapAcysDet.Consulta_ProductosDeACYS(
                        s.Id_Emp, s.Id_Cd, valuacion.Id_Cte, Acys_Id_Acs, valuacion.Id_Ter.Value, s);

                        CapaEntidad.eCapAcysDet obj = new CapaEntidad.eCapAcysDet();

                        int maxId = 0;
                        // Saca el Ultimo 
                        try
                        {
                            obj = lstProductosExistentes.Last();
                            maxId = obj.Id_AcsDet;
                        }
                        catch (Exception ex)
                        {
                            maxId = 0;
                        }

                        int siguienteId = maxId + 1;
                        int ultimoId = siguienteId + productos.Count;


                        // INSERTA EL DETALLE con SP
                        int iResult = 0;
                        for (int pId = siguienteId, idx = 0; pId < ultimoId; pId++, idx++)
                        {
                            eAcysDet2 _eAcysDet2 = new eAcysDet2();
                            _eAcysDet2.Id_AcsDet = pId;
                            _eAcysDet2.Id_Acs = Acys_Id_Acs;
                            _eAcysDet2.
                                Id_AcsVersion = ACYS.Id_AcsVersion;
                            _eAcysDet2.Id_Prd = productos[idx].Id_Prd;
                            _eAcysDet2.Acs_Cantidad = productos[idx].Acys_Cantidad;
                            _eAcysDet2.Acs_Documento = productos[idx].Acs_Doc;
                            _eAcysDet2.Acs_Sabado = productos[idx].Acys_Sabado;
                            _eAcysDet2.Acs_Viernes = productos[idx].Acys_Viernes;
                            _eAcysDet2.Acs_Jueves = productos[idx].Acys_Jueves;
                            _eAcysDet2.Acs_Miercoles = productos[idx].Acys_Miercoles;
                            _eAcysDet2.Acs_Martes = productos[idx].Acys_Martes;
                            _eAcysDet2.Acs_Lunes = productos[idx].Acys_Lunes;
                            _eAcysDet2.Acs_Frecuencia = productos[idx].Acys_Frecuencia;
                            _eAcysDet2.Acs_Precio = productos[idx].Prd_Precio;
                            _eAcysDet2.Acs_ConsigFechaInicio = productos[idx].Acys_FechaInicio.ToString();
                            _eAcysDet2.Acs_ConsigFechaFin = productos[idx].Acys_FechaFin.ToString();
                            _eAcysDet2.Acs_canTTotal = productos[idx].Acys_CantTotal;
                            _eAcysDet2.Id_TG = productos[idx].Id_TG;
                            _eAcysDet2.Acs_Modalidad = "0";
                            _eAcysDet2.Id_Ter = valuacion.Id_Ter.Value;

                            CD_CapAcysDet CD_AcysDet = new CD_CapAcysDet(s.Emp_Cnx);
                            iResult = CD_AcysDet.InsertUpdateADD(Acys_Id_Acs, ACYS.Id_AcsVersion, _eAcysDet2, s);

                        }
                    }
                    else
                    {
                        ACYS.Acs_Estatus = "A";
                        ACYS.Id_Ter = valuacion.Id_Ter.Value;
                        cnCapAcys.Insertar(ACYS, productos, s.Emp_Cnx, null, ref verificador,
                            new List<Asesoria>(), new List<Producto>(), new List<Producto>(), new List<AcysDatosGarantia>(), "");
                    }
                    CN_CrmProspecto cnCrmProspecto = new CN_CrmProspecto();
                    try
                    {
                        cnCrmProspecto.ConvertirACliente(s, valuacion.Id_Cte);
                    }
                    catch (CN_CrmProspecto.ConvertirAClienteClienteYaEsProspectoException cacpe)
                    {
                        //Si ya es cliente no hay problema, podemos seguir
                    }
                    catch (Exception ex)
                    {
                        //Algo inesperado resultó en una complicación. Arrojar y manejar.
                        throw ex;
                    }

                }

                // Si se completo el proceso actualiza. 

                CN_CrmOportunidad CN_Opor = new CN_CrmOportunidad();
                CN_Opor.CrmOportunidades_UpdateEstatus(s.Id_Emp, s.Id_Cd, idVal, Id_Op, valuacion.Id_Cte,
                s.Id_Rik, "A", 4, 4, s.Emp_Cnx);

            }
            catch (Exception ex)
            {
                //businessTransaction.
                throw ex;
                ACYS = null;
            }

            return ACYS;
        }


        /// <summary>
        /// Genera la estructura lógica de un acys a partir de una valuación.
        /// </summary>
        /// <param name="s">Sesión del usuario en operación</param>
        /// <param name="valuacion">Instancia de la entidad [CapValProyecto]</param>
        /// <returns>Acys. En caso exitoso; null en caso contrario</returns>
        protected void GenerarACYS_Ver2(Sesion s, CapValProyecto valuacion, ref Acys acys)
        {
            //Acys acys = new Acys();
            this.PrepararSeccionGeneralACYS_Ver2(s, valuacion, ref acys);
            PrepararSeccionVisitasACYS_Ver2(ref acys);
            PrepararSeccionContactosACYS_Ver2(ref acys);
            ///return acys;
        }

        /// <summary>
        /// Inicializa la parte lógica del acys que reflaja la sección de contactos.
        /// </summary>
        /// <param name="acys">Instancia de la entidad [CapAcys]</param>
        protected void PrepararSeccionContactosACYS_Ver2(ref Acys acys)
        {
            acys.Acs_Notas = string.Empty;

            acys.Acs_ContactoRepVenta = 0;
            acys.Acs_ContactoRepVentaTel = string.Empty;
            acys.Acs_ContactoRepVentaEmail = string.Empty;

            acys.Acs_ContactoRepServ = 0;
            acys.Acs_ContactoRepServTel = string.Empty;
            acys.Acs_ContactoRepServEmail = string.Empty;


            acys.Acs_ContactoJefServ = 0;
            acys.Acs_ContactoJefServTel = string.Empty;
            acys.Acs_ContactoJefServEmail = string.Empty;


            acys.Acs_ContactoAseServ = 0;
            acys.Acs_ContactoAseServTel = string.Empty;
            acys.Acs_ContactoAseServEmail = string.Empty;

            acys.Acs_ContactoJefOper = 0;
            acys.Acs_ContactoJefOperTel = string.Empty;
            acys.Acs_ContactoJefOperEmail = string.Empty;


            acys.Acs_ContactoCAlmRep = 0;
            acys.Acs_ContactoCAlmRepTel = string.Empty;
            acys.Acs_ContactoCAlmRepEmail = string.Empty;

            acys.Acs_ContactoCServTec = 0;
            acys.Acs_ContactoCServTecTel = string.Empty;
            acys.Acs_ContactoCServTecEmail = string.Empty;

            acys.Acs_ContactoCCreCob = 0;
            acys.Acs_ContactoCCreCobTel = string.Empty;
            acys.Acs_ContactoCCreCobEmail = string.Empty;


            acys.Acs_Contacto2 = string.Empty;
            acys.Acs_Telefono2 = 0;
            acys.Acs_Correo2 = string.Empty;

            acys.Acs_Contacto3 = string.Empty;
            acys.Acs_Telefono3 = 0;
            acys.Acs_Correo3 = string.Empty;

            acys.Acs_Contacto4 = string.Empty;
            acys.Acs_Telefono4 = 0;
            acys.Acs_Correo4 = string.Empty;

            acys.Acs_Contacto5 = string.Empty;
            acys.Acs_Telefono5 = 0;
            acys.Acs_Correo5 = string.Empty;

            acys.Acs_Contacto6 = string.Empty;
            acys.Acs_Telefono6 = 0;
            acys.Acs_Correo6 = string.Empty;

        }

        /// <summary>
        /// Inicializa la parte lógica del acys que refleja la sección de visitas
        /// </summary>
        /// <param name="acys">Instancia de la entidad [CapAcys]</param>
        protected void PrepararSeccionVisitasACYS_Ver2(ref Acys acys)
        {
            acys.Vis_Frecuencia = 0;
            acys.Acs_VisitaOtro = string.Empty;

            acys.Acs_ReqServAsesoria = false;
            acys.Acs_ReqServTecnicoRelleno = false;
            acys.Acs_ReqServMantenimiento = false;

            string Modalidad = "A";
            acys.Acs_Modalidad = Modalidad;

            acys.IdCte_DirEntrega = 0;
        }

        /// <summary>
        /// Inicializa la parte de la estructura lógica del acys que pertenece a la sección general.
        /// </summary>
        /// <param name="s">Sesión del usuario en operación</param>
        /// <param name="valuacion">Instancia de la entidad [CapValProyecto]</param>
        /// <param name="acys">Instancia de la entidad [CapAcys]</param>
        protected void PrepararSeccionGeneralACYS_Ver2(Sesion s, CapValProyecto valuacion, ref Acys acys)
        {
            acys.Id_Emp = s.Id_Emp;
            acys.Id_Cd = s.Id_Cd;
            if (valuacion.Id_Ter == null)
            {
                throw new ValuacionSinTerritorioException();
            }
            CN_CatCliente cnCatCliente = new CN_CatCliente();
            var cliente = cnCatCliente.Obtener(s, valuacion.Id_Cte);
            acys.Id_Ter = valuacion.Id_Ter.Value;
            acys.Id_Rik = s.Id_Rik;
            acys.Id_Cte = valuacion.Id_Cte;
            acys.Cte_Nombre = cliente.Cte_NomComercial;
            acys.Id_AcsVersion = 1;
            acys.Acs_Fecha = DateTime.Now;
            acys.Acs_FechaInicioDocumento = DateTime.Now;
            int year = DateTime.Now.Year;
            DateTime UltimoDiaDelAnio = new DateTime(year, 12, 31);
            acys.Acs_FechaFinDocumento = UltimoDiaDelAnio; // DateTime.Now;



            acys.Acs_Proveedor = "Sin especificar";
            acys.Acs_RutaEntrega = 0;
            acys.Acs_RutaServicio = 0;
            acys.Acs_VigenciaIni = DateTime.Now;
            acys.Acs_Semana = 0;
            acys.Acs_RecPedCorreo = false;

            acys.Acs_RecPedFax = false;
            acys.Acs_RecPedTel = false;
            acys.Acs_RecPedRep = false;
            acys.Acs_RecPedOtroStr = string.Empty;

            acys.Acs_PedidoEncargadoEnviar = string.Empty;
            acys.Acs_PedidoPuesto = string.Empty;
            acys.Acs_PedidoTelefono = string.Empty;
            acys.Acs_PedidoEmail = string.Empty;


            acys.Acs_ReqOrdenCompra = false;
            acys.Acs_RecDocReposicion = false;
            acys.Acs_RecDocFolio = false;
            acys.Acs_RecDocOtro = string.Empty;
            acys.Id_U = s.Id_U;
        }


        /// <summary>
        /// Genera la estructura lógica de un acys a partir de una valuación.
        /// </summary>
        /// <param name="s">Sesión del usuario en operación</param>
        /// <param name="valuacion">Instancia de la entidad [CapValProyecto]</param>
        /// <returns>Acys. En caso exitoso; null en caso contrario</returns>
        protected Acys GenerarACYS(Sesion s, CapValProyecto valuacion)
        {
            Acys acys = new Acys();
            PrepararSeccionGeneralACYS(s, valuacion, acys);
            PrepararSeccionVisitasACYS(acys);
            PrepararSeccionContactosACYS(acys);
            return acys;
        }

        /// <summary>
        /// Inicializa la parte de la estructura lógica del acys que pertenece a la sección general.
        /// </summary>
        /// <param name="s">Sesión del usuario en operación</param>
        /// <param name="valuacion">Instancia de la entidad [CapValProyecto]</param>
        /// <param name="acys">Instancia de la entidad [CapAcys]</param>
        protected void PrepararSeccionGeneralACYS_Ver2(Sesion s, CapValProyecto valuacion, Acys acys)
        {
            acys.Id_Emp = s.Id_Emp;
            acys.Id_Cd = s.Id_Cd;
            if (valuacion.Id_Ter == null)
            {
                throw new ValuacionSinTerritorioException();
            }
            CN_CatCliente cnCatCliente = new CN_CatCliente();
            var cliente = cnCatCliente.Obtener(s, valuacion.Id_Cte);
            acys.Id_Ter = valuacion.Id_Ter.Value;
            acys.Id_Rik = s.Id_Rik;
            acys.Id_Cte = valuacion.Id_Cte;
            acys.Cte_Nombre = cliente.Cte_NomComercial;
            acys.Id_AcsVersion = 1;
            acys.Acs_Fecha = DateTime.Now;
            acys.Acs_FechaInicioDocumento = DateTime.Now;
            int year = DateTime.Now.Year;
            DateTime UltimoDiaDelAnio = new DateTime(year, 12, 31);
            acys.Acs_FechaFinDocumento = UltimoDiaDelAnio; // DateTime.Now;



            acys.Acs_Proveedor = "Sin especificar";
            acys.Acs_RutaEntrega = 0;
            acys.Acs_RutaServicio = 0;
            acys.Acs_VigenciaIni = DateTime.Now;
            acys.Acs_Semana = 0;
            acys.Acs_RecPedCorreo = false;

            acys.Acs_RecPedFax = false;
            acys.Acs_RecPedTel = false;
            acys.Acs_RecPedRep = false;
            acys.Acs_RecPedOtroStr = string.Empty;

            acys.Acs_PedidoEncargadoEnviar = string.Empty;
            acys.Acs_PedidoPuesto = string.Empty;
            acys.Acs_PedidoTelefono = string.Empty;
            acys.Acs_PedidoEmail = string.Empty;


            acys.Acs_ReqOrdenCompra = false;
            acys.Acs_RecDocReposicion = false;
            acys.Acs_RecDocFolio = false;
            acys.Acs_RecDocOtro = string.Empty;
            acys.Id_U = s.Id_U;
        }

        /// <summary>
        /// Inicializa la parte de la estructura lógica del acys que pertenece a la sección general.
        /// </summary>
        /// <param name="s">Sesión del usuario en operación</param>
        /// <param name="valuacion">Instancia de la entidad [CapValProyecto]</param>
        /// <param name="acys">Instancia de la entidad [CapAcys]</param>
        protected void PrepararSeccionGeneralACYS(Sesion s, CapValProyecto valuacion, Acys acys)
        {
            acys.Id_Emp = s.Id_Emp;
            acys.Id_Cd = s.Id_Cd;
            if (valuacion.Id_Ter == null)
            {
                throw new ValuacionSinTerritorioException();
            }
            CN_CatCliente cnCatCliente = new CN_CatCliente();
            var cliente = cnCatCliente.Obtener(s, valuacion.Id_Cte);
            acys.Id_Ter = valuacion.Id_Ter.Value;
            acys.Id_Rik = s.Id_Rik;
            acys.Id_Cte = valuacion.Id_Cte;
            acys.Cte_Nombre = cliente.Cte_NomComercial;
            acys.Id_AcsVersion = 1;
            acys.Acs_Fecha = DateTime.Now;
            acys.Acs_FechaInicioDocumento = DateTime.Now;
            acys.Acs_FechaFinDocumento = DateTime.Now;
            acys.Acs_Proveedor = "Sin especificar";
            acys.Acs_RutaEntrega = 0;
            acys.Acs_RutaServicio = 0;
            acys.Acs_VigenciaIni = DateTime.Now;
            acys.Acs_Semana = 0;
            acys.Acs_RecPedCorreo = false;

            acys.Acs_RecPedFax = false;
            acys.Acs_RecPedTel = false;
            acys.Acs_RecPedRep = false;
            acys.Acs_RecPedOtroStr = string.Empty;

            acys.Acs_PedidoEncargadoEnviar = string.Empty;
            acys.Acs_PedidoPuesto = string.Empty;
            acys.Acs_PedidoTelefono = string.Empty;
            acys.Acs_PedidoEmail = string.Empty;


            acys.Acs_ReqOrdenCompra = false;
            acys.Acs_RecDocReposicion = false;
            acys.Acs_RecDocFolio = false;
            acys.Acs_RecDocOtro = string.Empty;
            acys.Id_U = s.Id_U;
        }

        /// <summary>
        /// Inicializa la parte lógica del acys que refleja la sección de visitas
        /// </summary>
        /// <param name="acys">Instancia de la entidad [CapAcys]</param>
        protected void PrepararSeccionVisitasACYS(Acys acys)
        {
            acys.Vis_Frecuencia = 0;
            acys.Acs_VisitaOtro = string.Empty;

            acys.Acs_ReqServAsesoria = false;
            acys.Acs_ReqServTecnicoRelleno = false;
            acys.Acs_ReqServMantenimiento = false;

            string Modalidad = "A";
            acys.Acs_Modalidad = Modalidad;

            acys.IdCte_DirEntrega = 0;
        }

        /// <summary>
        /// Inicializa la parte lógica del acys que reflaja la sección de contactos.
        /// </summary>
        /// <param name="acys">Instancia de la entidad [CapAcys]</param>
        protected void PrepararSeccionContactosACYS(Acys acys)
        {
            acys.Acs_Notas = string.Empty;

            acys.Acs_ContactoRepVenta = 0;
            acys.Acs_ContactoRepVentaTel = string.Empty;
            acys.Acs_ContactoRepVentaEmail = string.Empty;

            acys.Acs_ContactoRepServ = 0;
            acys.Acs_ContactoRepServTel = string.Empty;
            acys.Acs_ContactoRepServEmail = string.Empty;


            acys.Acs_ContactoJefServ = 0;
            acys.Acs_ContactoJefServTel = string.Empty;
            acys.Acs_ContactoJefServEmail = string.Empty;


            acys.Acs_ContactoAseServ = 0;
            acys.Acs_ContactoAseServTel = string.Empty;
            acys.Acs_ContactoAseServEmail = string.Empty;

            acys.Acs_ContactoJefOper = 0;
            acys.Acs_ContactoJefOperTel = string.Empty;
            acys.Acs_ContactoJefOperEmail = string.Empty;


            acys.Acs_ContactoCAlmRep = 0;
            acys.Acs_ContactoCAlmRepTel = string.Empty;
            acys.Acs_ContactoCAlmRepEmail = string.Empty;

            acys.Acs_ContactoCServTec = 0;
            acys.Acs_ContactoCServTecTel = string.Empty;
            acys.Acs_ContactoCServTecEmail = string.Empty;

            acys.Acs_ContactoCCreCob = 0;
            acys.Acs_ContactoCCreCobTel = string.Empty;
            acys.Acs_ContactoCCreCobEmail = string.Empty;


            acys.Acs_Contacto2 = string.Empty;
            acys.Acs_Telefono2 = 0;
            acys.Acs_Correo2 = string.Empty;

            acys.Acs_Contacto3 = string.Empty;
            acys.Acs_Telefono3 = 0;
            acys.Acs_Correo3 = string.Empty;

            acys.Acs_Contacto4 = string.Empty;
            acys.Acs_Telefono4 = 0;
            acys.Acs_Correo4 = string.Empty;

            acys.Acs_Contacto5 = string.Empty;
            acys.Acs_Telefono5 = 0;
            acys.Acs_Correo5 = string.Empty;

            acys.Acs_Contacto6 = string.Empty;
            acys.Acs_Telefono6 = 0;
            acys.Acs_Correo6 = string.Empty;

        }

        /// <summary>
        /// Regresa el listado de productos asociados a una valuación, preparados para ser asociados a la estructura lógica del ACYS.
        /// </summary>
        /// <param name="valuacion">Instancia de la entidad [CapValProyecto]</param>
        /// <param name="s">Sesión del usuario en operación</param>
        /// <returns>AcysPrd[]. Conjunto de productos asociados a una valuación en una llamada satisfactoria; null de otro modo</returns>
        protected List<AcysPrd> ObtenerListadoDeProductos(CapValProyecto valuacion, Sesion s)
        {
            CN_CapValProyecto cnCapValProyecto = new CN_CapValProyecto();
            var dets = cnCapValProyecto.ObtenerDetalle(s, valuacion.Id_Vap);
            List<AcysPrd> productos = new List<AcysPrd>();
            CN_CatProducto cnCatProducto = new CN_CatProducto();
            CD_CatProducto CN = new CD_CatProducto();
            int Validador = 0;
            foreach (var cvpd in dets)
            {


                var producto = CN.ConsultaProductoById(ref Validador, s.Id_Emp, s.Id_Cd, s.Id_Cd, cvpd.Id_Prd, true, s.Emp_Cnx);


                AcysPrd prd = new AcysPrd()
                {
                    Acs_Doc = string.Empty,
                    Acys_Cantidad = cvpd.Vap_Cantidad,
                    Acys_CantTotal = cvpd.Vap_Cantidad,
                    Acys_FechaFin = DateTime.Now,
                    Acys_FechaInicio = DateTime.Now,
                    Acys_Frecuencia = 1,
                    Acys_Jueves = false,
                    Acys_Lunes = false,
                    Acys_Martes = false,
                    Acys_Miercoles = false,
                    Acys_Sabado = false,
                    Acys_UltACtp = 0,
                    Acys_UltSCtp = 0,
                    Acys_Viernes = false,
                    Id_Prd = cvpd.Id_Prd,
                    Id_TG = 0,
                    Prd_Descripcion = producto.Prd_Descripcion,
                    Prd_Precio = cvpd.Vap_Precio,
                    Prd_Presentacion = producto.Prd_Presentacion,
                    Prd_UniNom = producto.Prd_UniNe
                };
                productos.Add(prd);
            }

            return productos;
        }

        public class ValuacionSinTerritorioException
            : Exception
        {
            public ValuacionSinTerritorioException()
                : base("Generación de ACYS cancelada. La valuación no cuenta con un territorio asociado.")
            {
            }
        }
    }
}