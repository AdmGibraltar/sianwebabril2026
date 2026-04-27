using System;


/// <summary>
/// Summary description for Leads
/// </summary>
/// 

namespace CapaEntidad
{

    public class Leads
    {
        public int? Id_Emp { get; set; }
        public int? IdLeads { get; set; }
        public string NombreEmpresa { get; set; }
        public string NumeroEmpleados { get; set; }
        public string Estado { get; set; }


        public int Activo { get; set; }
        public int? IdGiroEmpresa { get; set; }
        public string ProductoInteres { get; set; }
        public int Id_Cd { get; set; }
        public string NomCDI { get; set; }
        public string GiroEmpresa { get; set; }
        public int? IdUsuario { get; set; }
        public string Otro { get; set; }
        public int? Idrepresentante { get; set; }
        public DateTime? FechaAlta { get; set; }
        public DateTime? FechaUltMod { get; set; }
        public DateTime? FechaAsignaRep { get; set; }
        public int? IdUsuarioGerente { get; set; }
        public int? TipoFiltro { get; set; }
        public string Filtro { get; set; }
        public int? IdMedioComunicacion { get; set; }
        public string MedioComunicacion { get; set; }

        public int? MesInicial { get; set; }
        public int? MesFinal { get; set; }
        public int? AnioInicial { get; set; }
        public int? AnioFinal { get; set; }
        public string Ciudad { get; set; }
        public string Comentarios { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public string NombreContacto { get; set; }

        public DateTime? FechaInicial { get; set; }
        public DateTime? FechaFinal { get; set; }

        //para las gráficas 
        public int? Id { get; set; }
        public string Descripcion { get; set; }
        public int? ValorCantidad { get; set; }
        public double ValorMonto { get; set; }

        //jfcv color control de cambios 2 mzo 
        public string PresentarEstatus { get; set; }
        public string ColorEstatus { get; set; }
        public string Color { get; set; }
        public string HistorialLeads { get; set; }
        public string NomEstatus { get; set; }
        public string MotivoCanceladoGerente { get; set; }
        public string MotivoCanceladoRik { get; set; }

        public Leads()
        {
            this.Id_Emp = null;
            this.IdLeads = null;
            this.NombreEmpresa = "";
            this.NumeroEmpleados = "";
            this.Estado = "";

            this.Activo = 1;
            this.IdGiroEmpresa = null;
            this.ProductoInteres = null;
            this.Id_Cd = -1;
            this.NomCDI = null;
            this.GiroEmpresa = null;
            this.IdUsuario = null;
            this.Otro = null;
            this.Idrepresentante = null;
            this.FechaAlta = null;
            this.FechaUltMod = null;
            this.FechaAsignaRep = null;
            this.IdUsuarioGerente = null;
            this.TipoFiltro = null;
            this.Filtro = null;
            this.IdMedioComunicacion = null;
            this.MedioComunicacion = null;

            this.MesInicial = null;
            this.MesFinal = null;
            this.AnioInicial = null;
            this.AnioFinal = null;
            this.Ciudad = null;
            this.Comentarios = null;
            this.Telefono = null;
            this.Correo = null;
            this.FechaInicial = null;
            this.FechaFinal = null;
            this.NombreContacto = null;

            //para las gráficas 
            this.Id = null;
            this.Descripcion = null;
            this.ValorCantidad = null;
            this.ValorMonto = 0;

            //jfcv color control de cambios 2 mzo 
            this.PresentarEstatus = null;
            this.ColorEstatus = null;
            this.Color = null;
            this.HistorialLeads = null;
            this.NomEstatus = null;
            this.MotivoCanceladoGerente = null;
            this.MotivoCanceladoRik = null;


        }

        public Leads(

                int? Id_emp,
                 int? IdLeads,
                string NombreEmpresa,
                string NumeroEmpleados,
                string Estado,

                int Activo,
                int? IdGiroEmpresa,
                string ProductoInteres,
                int Id_Cd,
                string NomCDI,
                string GiroEmpresa,
                int? IdUsuario,
                string Otro,
                int? Idrepresentante,
                DateTime? FechaAlta,
                DateTime? FechaUltMod,
                DateTime? FechaAsignaRep,
                int? IdUsuarioGerente,
                int? TipoFiltro,
                string Filtro,
                int? IdMedioComunicacion,
                string MedioComunicacion,

                int? MesInicial,
                int? MesFinal,
                int? AnioInicial,
                int? AnioFinal,
                string Ciudad,
                string Comentarios,
                string Telefono,
                string Correo,
                DateTime? FechaInicial,
                DateTime? FechaFinal,
                string NombreContacto,

             //para las gráficas 
             int? Id,
            string Descripcion,
            int? ValorCantidad,
            double ValorMonto,
            //jfcv color control de cambios 2 mzo 
            string PresentarEstatus,
            string ColorEstatus,
            string Color,
            string HistorialLeads,
            string NomEstatus,
            string MotivoCanceladoGerente,
            string MotivoCanceladoRik
            )
        {
            this.Id_Emp = null;
            this.IdLeads = null;
            this.NombreEmpresa = "";
            this.NumeroEmpleados = "";
            this.Estado = "";

            this.Activo = 1;
            this.IdGiroEmpresa = null;
            this.ProductoInteres = null;
            this.Id_Cd = -1;
            this.NomCDI = null;
            this.GiroEmpresa = null;
            this.IdUsuario = null;
            this.Otro = null;
            this.Idrepresentante = null;
            this.FechaAlta = null;
            this.FechaUltMod = null;
            this.FechaAsignaRep = null;
            this.IdUsuarioGerente = null;
            this.TipoFiltro = null;
            this.Filtro = null;
            this.IdMedioComunicacion = null;
            this.MedioComunicacion = null;

            this.MesInicial = null;
            this.MesFinal = null;
            this.AnioInicial = null;
            this.AnioFinal = null;

            this.Ciudad = null;
            this.Comentarios = null;
            this.Telefono = null;
            this.Correo = null;
            this.FechaInicial = null;
            this.FechaFinal = null;
            this.NombreContacto = null;
            //para las gráficas 
            this.Id = null;
            this.Descripcion = null;
            this.ValorCantidad = null;
            this.ValorMonto = 0;
            //jfcv color control de cambios 2 mzo 
            this.PresentarEstatus = null;
            this.ColorEstatus = null;
            this.Color = null;
            this.HistorialLeads = null;
            this.NomEstatus = null;
            this.MotivoCanceladoGerente = null;
            this.MotivoCanceladoRik = null;
        }
    }

}