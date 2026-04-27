
/// <summary>
/// Summary description for BootstrapChartDataItem
/// </summary>
public class BootstrapChartDataItem
{
    public BootstrapChartDataItem(string nom_Empleado, decimal venta1, decimal real1, decimal venta2, decimal real2, decimal venta3, decimal real3, decimal venta4, decimal real4,
        decimal venta5, decimal real5, decimal venta6, decimal real6, decimal venta7, decimal real7, decimal venta8, decimal real8,
        decimal venta9, decimal real9, decimal venta10, decimal real10, decimal venta11, decimal real11, decimal venta12, decimal real12,
        string TipoVenta

        )
    {
        Nom_Empleado = nom_Empleado;
        Venta1 = venta1;
        Venta2 = venta2;
        Venta3 = venta3;
        Venta4 = venta4;
        Venta5 = venta5;
        Venta6 = venta6;
        Venta7 = venta7;
        Venta8 = venta8;
        Venta9 = venta9;
        Venta10 = venta10;
        Venta11 = venta11;
        Venta12 = venta12;

        Real1 = real1;
        Real2 = real2;
        Real3 = real3;
        Real4 = real4;
        Real5 = real5;
        Real6 = real6;
        Real7 = real7;
        Real8 = real8;
        Real9 = real9;
        Real10 = real10;
        Real11 = real11;
        Real12 = real12;
    }

    //comentado para pruebas 
    //public BootstrapChartDataItem(string nom_Empleado, decimal venta1, decimal real1, decimal venta2, decimal real2, decimal venta3, decimal real3, decimal venta4, decimal real4,
    //   decimal venta5, decimal real5, decimal venta6, decimal real6 

    //   )
    //{
    //    Nom_Empleado = nom_Empleado;
    //    Venta1 = venta1;
    //    Venta2 = venta2;
    //    Venta3 = venta3;
    //    Venta4 = venta4;
    //    Venta5 = venta5;
    //    Venta6 = venta6;


    //    Real1 = real1;
    //    Real2 = real2;
    //    Real3 = real3;
    //    Real4 = real4;
    //    Real5 = real5;
    //    Real6 = real6;

    //}

    public BootstrapChartDataItem(string nom_Empleado, string tipoventa, decimal venta1, decimal venta2

       )
    {
        Nom_Empleado = nom_Empleado;
        Venta1 = venta1;
        Venta2 = venta2;

        TipoVenta = tipoventa;


    }

    public string Nom_Empleado { get; set; }
    public string TipoVenta { get; set; }

    public decimal Venta1 { get; set; }
    public decimal Venta2 { get; set; }
    public decimal Venta3 { get; set; }
    public decimal Venta4 { get; set; }
    public decimal Venta5 { get; set; }
    public decimal Venta6 { get; set; }
    public decimal Venta7 { get; set; }
    public decimal Venta8 { get; set; }
    public decimal Venta9 { get; set; }
    public decimal Venta10 { get; set; }
    public decimal Venta11 { get; set; }
    public decimal Venta12 { get; set; }

    public decimal Real1 { get; set; }
    public decimal Real2 { get; set; }
    public decimal Real3 { get; set; }
    public decimal Real4 { get; set; }
    public decimal Real5 { get; set; }
    public decimal Real6 { get; set; }
    public decimal Real7 { get; set; }
    public decimal Real8 { get; set; }
    public decimal Real9 { get; set; }
    public decimal Real10 { get; set; }
    public decimal Real11 { get; set; }
    public decimal Real12 { get; set; }

}