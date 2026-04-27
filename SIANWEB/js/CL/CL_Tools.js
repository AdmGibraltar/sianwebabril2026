/*
Key Quimica Dic 2018 
24 Dic 2018 RFH 
*/

function Empty_Disable_Chb(Selector) {
    $(Selector).prop('checked', false);
    $(Selector).attr("disabled", "disabled");
}

function Empty_Disable_Tb(Selector) {
    $(Selector).val('');
    $(Selector).attr("disabled", "disabled");
}

function Disable_Btn(Selector) {
    $(Selector).attr("disabled", "disabled");
}

function Enable_Btn(Selector) {
    $(Selector).removeAttr("disabled");
}

Number.prototype.formatMoney = function (c, d, t) {
    var n = this,
        c = isNaN(c = Math.abs(c)) ? 2 : c,
        d = d == undefined ? "." : d,
        t = t == undefined ? "," : t,
        s = n < 0 ? "-" : "",
        i = String(parseInt(n = Math.abs(Number(n) || 0).toFixed(c))),
        j = (j = i.length) > 3 ? j % 3 : 0;
    return s + (j ? i.substr(0, j) + t : "") + i.substr(j).replace(/(\d{3})(?=\d)/g, "$1" + t) + (c ? d + Math.abs(n - i).toFixed(c).slice(2) : "");
};

function format_YYYYMMDD(Fecha) {
    /*
    var sResult = "";
    sResult = Fecha.substring(3, 5) + "-" + Fecha.substring(0, 2) + "-" + Fecha.substring(6, 10);
    return sResult;
    */

    var sResult = "";
    var YYYY = Fecha.substring(0, 4);
    var MM = Fecha.substring(5, 7);
    var DD = Fecha.substring(8, 10);
    //sResult = YYYY + "/" + MM + "/" + DD;
    sResult = DD + "/" + MM + "/" + YYYY;
    //sResult = Fecha.substring(3, 5) + "-" + Fecha.substring(0, 2) + "-" + Fecha.substring(6, 10);

    return sResult;
}

function format_YYYYMMDD_2(Fecha) {

    var sResult = "";
    var YYYY = Fecha.substring(6, 10);
    var MM = Fecha.substring(3, 5);

    var DD = Fecha.substring(0, 2);

    sResult = YYYY + "-" + MM + "-" + DD;

    return sResult;
}

function CheckBox_ToInt(Selector) {
    var iIsChecked = 0;
    var IsChecked = Selector.is(":checked");

    if (IsChecked) {
        iIsChecked = 1;
    } else {
        iIsChecked = 0;
    }

    return iIsChecked;
}

function ReturnEmpty_WhenZero(Valor) {
    var sReturn = '';

    if (Valor <= 0) {
        sReturn = '';
    } else {
        sReturn = Valor;
    }
    return sReturn;
}

function Modal_Mensaje(IdTipoMensaje, Titulo, Mensaje) {

    var sIcon = '';

    switch (IdTipoMensaje) {
        case 1:  // Exclamacion
            sIcon = '<i class="fa fa-times fa-5x" aria-hidden="true" style="color:red;" ></i>';
            break;
        case 2:  // Exclamacion
            sIcon = '<i class="fa fa-times fa-4" aria-hidden="true" style="color:red;" ></i>';
            break;
    }

    $('#modalAcysMensaje_Icon').html(sIcon);
    $('#modalAcysMensaje_Titulo').html(Titulo);
    $('#modalAcysMensaje_Texto').html(Mensaje);
    $('#modalAcysMensaje').appendTo("body").modal('show');

}