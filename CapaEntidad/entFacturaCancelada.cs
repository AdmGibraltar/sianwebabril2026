using System;

public class entFacturaCancelada
{
    public string strRfcReceptor { get; set; }
    public string strRazonSocial { get; set; }
    public string strSerie { get; set; }
    public int intFolio { get; set; }
    public string strFolioFiscal { get; set; }
    public DateTime? dtFechaEmision { get; set; }
    public DateTime? dtFechaSolCanc { get; set; }
    public string strTipoDocumento { get; set; }
    public string strEstadoSAT { get; set; }
    public decimal decSubtotal { get; set; }
    public decimal decIVA { get; set; }
    public decimal decTotal { get; set; }
    public int intFolioRelacionado { get; set; }
    public string strSerieRelacionada { get; set; }
    public string strFolioFiscalRelacionado { get; set; }
    public string strTipoDocumentoRelacionado { get; set; }
    public bool boolEsTotal { get; set; }
}