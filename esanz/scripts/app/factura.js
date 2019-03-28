$(document).ready(function () {

    $('#txtfFechaIni').val();
    $('#txtfFechaFin').val();

    loadFormaPago();

    $('#btnBuscar').click(function () {
        loadData();
    });

});


function loadData()
{
    var res = Buscar_validar();
    if (res == false) {
        return false;
    }
    
    var parm = {
        num_pago: $('select[id="ddlfForma_pago"] option:selected').val(),
        fecini: $('#txtfFechaIni').val(),
        fecfin: $('#txtfFechaFin').val(),
        cliente: $('#txtCliente').val()
    };

    $.ajax({
        url: "/Factura/Listar",
        type: "POST",
        data: JSON.stringify(parm),
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            var html = '';
            $.each(result, function (key, item) {
                html += '<tr>';
                html += '<td>' + item.num_factura + '</td>';
                html += '<td>' + item.cliente.pNombre + ' ' + item.cliente.sNombre + ' ' + item.cliente.pApellido + ' ' + item.cliente.sApellido + '</td>';
                html += '<td></td>';
                html += '<td>' + item.modo_pago.nombre + '</td>';
                html += '<td>' + item.total + '</td>';
                html += '<td>';
                var link = $('#btnNuevo').attr("href");
                link = link.replace('0', item.num_factura);
                var ref = '<a href="@">Editar</a> | <a href="#" onclick="DelFactura(' + item.num_factura  + ')">Eliminar</a>'
                ref = ref.replace('@', link);
                html += ref;
                html += '</td>';

                //html += '<td colspan="2"><a href="#" onclick="return verFactura(' + item.num_factura + ')">Editar </a> | <a href="#" onclick="delFactura(' + item.num_factura + ')">Eliminar</a></td>';
                html += '</tr>';
            });
            $('.tbody').html(html);
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

function loadFormaPago() {
    $.ajax({
        url: "/Modo_Pago/Listar",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            $('#ddlfForma_pago').empty();
            $('#ddlfForma_pago').append("<option value='0' selected>--Todos--</option>");
            $.each(result, function (key, item) {
                var valor = item.num_pago;
                var text = item.nombre;
                $('#ddlfForma_pago').append($("<option></option>").val(valor).html(text));
            });
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

function verFactura(num_factura)
{
    debugger;
    var link = $('#btnNuevo').attr("href");
    var url = link.replace('0', num_factura);
    $.get(url);
    //$('#btnNuevo').click();
}

function DelFactura(num_factura)
{
    //var title         = 'Eliminar Factura?';
    //var mensaje       = 'Esta ud. seguro de eliminar la Factura Nro. ' + num_factura + ' !';
    //var titbtnConfirm = 'Eliminar';
    //var titbtnCancel  = 'Cancelar';

    //var result = msgConfirma(title, mensaje, titbtnConfirm, titbtnCancel);
    //if (result)
    //{
    //    $.ajax({
    //        url: "/Factura/Eliminar",
    //        data: "{num_factura:'" + 0 + "'}",
    //        type: "POST",
    //        contentType: "application/json;charset=utf-8",
    //        dataType: "json",
    //        success: function (result) {
    //            msgAlerta(true, '&nbsp;', result.Mensaje);
    //        },
    //        error: function (errormessage) {
    //            alert(errormessage.responseText);
    //        }
    //    });
    //}

    bootbox.confirm({
        title: "Eliminar Factura?",
        message: "Esta ud. seguro de eliminar la Factura Nro. " + num_factura + ' !',
        buttons: {
            confirm: {label: 'Eliminar', className: 'btn-primary'},
            cancel: { label: 'Cancelar', className: 'btn-secondary'}
        },
        callback: function (result) {
            if (result)
            {
                $.ajax({
                    url: "/Factura/Eliminar",
                    data: "{num_factura:'" + num_factura + "'}",
                    type: "POST",
                    contentType: "application/json;charset=utf-8",
                    dataType: "json",
                    success: function (result) {
                        msgAlerta(false, '&nbsp;', result.Mensaje);
                        loadData();
                    },
                    error: function (errormessage) {
                        alert(errormessage.responseText);
                    }
                });
            }
        }
    });
}

function Buscar_validar() {
    var isValid = true;

    if ($('#txtfFechaIni').val().trim() == "") {
        $('#txtfFechaIni').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#txtfFechaIni').css('border-color', 'lightgrey');
    }

    if ($('#txtfFechaFin').val().trim() == "") {
        $('#txtfFechaFin').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#txtfFechaFin').css('border-color', 'lightgrey');
    }

    return isValid;
}