
var Entidad_CompraLocalDet = {
    'Id_Emp': 0,
    'Id_Cd': 0,
    'Id_Comp': 0,
    'Id_CompDet': 0,
    'Id_Prd': '',
    'Det_Costo': 0.0,
    'Det_Estatus': '',
    'Det_FecAut': '',
    'Det_Enfocada': 0,
    'Det_Autorizo': 0
}


var Entidad_CompraLocal = {
    'Id_Emp': 0,
    'Id_Cd': 0,
    'Cd_Nombre': '',
    'Id_Comp': 0,
    'Comp_Solicito': 0,
    'Solicito_Nombre': '',
    'FechaSol': '',
    'FechaAut': '',
    'Comp_Autorizo': 0,
    'Comp_Descuento': 0.0,
    'EstatusAut': '',
    'omentarios': '',
    'TipoSolicitud': '',
    'IdTipoSolicitud': 0,
    'Vigencia': ''
}

var Entidad_ProductoPrecio = {
    '_Id_Emp': 0,
    '_int _Id_Cd': 0,
    '_long _Id_Prd': 0,
    '_Id_Pre': 0,
    '_Prd_Actual': false,
    '_Prd_FechaInicio': '',
    '_Prd_FechaFin': '',
    '_Prd_PreDescripcion': '',
    '_Pre_Descripcion': '',
    '_Prd_Pesos': 0
}

var Entidad_Producto = {
    'Id_Emp': 0,
    'Id_Cd': 0,
    'Id_Prd': 0,
    'Id_Spo': '',
    'Id_Ptp': 0,
    'Id_Cpr': 0,
    'Id_Fam': 0,
    'Id_Sub': 0,
    'Id_Pvd': 0,
    'Prd_Descripcion': '',
    'Prd_Presentacion': '',
    'Prd_InvInicial': 0,
    'Prd_InvFinal': 0,
    'Prd_AgrupadoSpo': 0,
    'Prd_FactorConv': 0.0,
    'Prd_AparatoSisProp': false,
    'Prd_Fisico': 0,
    'Prd_Ordenado': 0,
    'Prd_Asignado': 0,
    'Prd_InvSeg': 0,
    'Prd_TTrans': 0,
    'Prd_TEntre': 0,
    'Prd_Transito': 0,
    'Prd_UniNe': '',
    'Prd_UniNs': '',
    'Prd_Unico': 0,
    'Prd_UniEmp': 0.0,
    'Prd_Colo': false,
    'Prd_Ren': '',
    'Prd_Mes': 0,
    'Prd_CLNomFab': '',
    'Prd_CLIdFab': '', //   txtFcodigo.Text;
    'Prd_CLDesFab': '', //  txtFdescripcion.Text
    'Prd_CLPreFab': '', //txtFpresentacion.Text
    'Prd_CLNomPro': '', // txtPnombre.Text;
    'Prd_CLIdPro': '', //  txtPcodigo.Text;
    'Prd_CLDesPro': '', //  txtPdescripcion.Text;
    'Prd_CLPrePro': '', //  txtPpresentacion.Text;
    'Prd_MaxExistencia': 0,  //txtExistencia.Text': = string.Empty ? 0 : Convert.ToInt32(txtExistencia.Text);
    'Prd_Ubicacion': '', // txtUbicacion.Text;
    'Prd_Contribucion': 0, // txtContribucion.Text': = string.Empty ? 0 : Convert.ToSingle(txtContribucion.Text);
    'Prd_PorUtilidades': '', //  txtPorUtilidades.Text': = string.Empty ? 0 : Convert.ToSingle(txtPorUtilidades.Text);
    'Prd_Nuevo': 0, //  chkProductoNuevo.Checked;
    'Prd_PesConTecnico': 0, //  txtPesos.Text': = string.Empty ? 0 : Convert.ToDouble(txtPesos.Text);
    'Prd_CptSv': '',  //string.Empty;
    'Prd_Activo': 0, //  chkActivo.Checked;
    'Prd_FecAlta': '',  //DateTime.Now;
    'Prd_Minimo': 0, //  txtMinCompra.Text': = string.Empty ? 0 : Convert.ToInt32(txtMinCompra.Text);
    'Prd_PlanAbasto': '', //  txtPlanAbasto.Text;
    //RBM Nov 2023 
    //Campos nuevos
    'Prd_FechaInicio': '',
    'Prd_FechaFin': '',
    'Prd_CodigoProv': 0,
    'Prd_DescripcionProv': '',
    'Prd_PresentacionProv': '',
    'Prd_IdProvLocal': '',
    'Prd_NomProvLocal': '',
    'Prd_NomFamilia': '',
    'Prd_NomSubFamilia': '',
    'Prd_ClaveUnidad': '',
    'Prd_ClaveProdServ': '',

    'ListaProductoPrecios': []
}