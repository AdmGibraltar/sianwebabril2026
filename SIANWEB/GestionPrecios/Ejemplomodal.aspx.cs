using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using System.Web.Services;

namespace SIANWEB.GestionPrecios
{
    public partial class Ejemplomodal : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static string GetProducts()
        {
            var products = new List<Product>
        {
            new Product { Id = 1, Name = "Producto A", Category = "Categoría 1", Price = 100.50m },
            new Product { Id = 2, Name = "Producto B", Category = "Categoría 2", Price = 200.00m },
            new Product { Id = 3, Name = "Producto C", Category = "Categoría 3", Price = 150.75m }
        };

            return new JavaScriptSerializer().Serialize(products);
        }
    }

    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
    }

}