using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
 
 
/// <summary>
/// Summary description for Aplicacion
/// </summary>

public class CatalogoListaPrecios
{
    public Int64? Id_SubCatalogo { get; set; }
    public string Descripcion_SubCatalogo { get; set; }

    public CatalogoListaPrecios()
    {
        this.Id_SubCatalogo = null;
        this.Descripcion_SubCatalogo = "";

    }

    public CatalogoListaPrecios(

        int Id_SubCatalogo,
        string Descripcion_SubCatalogo

    )
    {
        this.Id_SubCatalogo = null;
        this.Descripcion_SubCatalogo = null;

    }

    public CatalogoListaPrecios(
        int? Id_SubCatalogo,
        string Descripcion_SubCatalogo

    )
    {
        this.Id_SubCatalogo = Id_SubCatalogo;
        this.Descripcion_SubCatalogo = Descripcion_SubCatalogo;
    }


}}


