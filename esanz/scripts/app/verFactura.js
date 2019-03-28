$(document).ready(function () {
    loadCombos();

    $("#ddlCategoria").change(function () {
        var id = $('#ddlCategoria').val();
        loadProducto(id);
        $('#txtCantidad').val(0);   
    });

    $("#btnNuevoItem").click(function () {
        NuevoItem();        
    });

    $("#btnGrabarFactura").click(function () {
        GrabarFactura();
    });

    $("#btnAnadirItem").click(function () {
        GrabarNuevoItem();
    });
});

function loadCombos() {
    loadCliente();
    loadFormaPago();
    loadCategoria();
    loadProducto(0);
}

function loadCliente() {
    $.ajax({
        url: "/Cliente/Listar",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            $('#ddlCliente').empty();
            $('#ddlCliente').append("<option value='0' selected>--Seleccione--</option>");
            $.each(result, function (key, item) {
                var valor = item.id_cliente;
                var text = item.pNombre + ' ' + item.sNombre + ' ' + item.pApellido + ' ' + item.sApellido;
                $('#ddlCliente').append($("<option></option>").val(valor).html(text));
            });
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
            $('#ddlFormaPago').empty();
            $('#ddlFormaPago').append("<option value='0' selected>--Seleccione--</option>");
            $.each(result, function (key, item) {
                var valor = item.num_pago;
                var text = item.nombre;
                $('#ddlFormaPago').append($("<option></option>").val(valor).html(text));
            });
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

function loadCategoria() {
    $.ajax({
        url: "/Categoria/Listar",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            $('#ddlCategoria').empty();
            $('#ddlCategoria').append("<option value='0' selected>--Seleccione--</option>");
            $.each(result, function (key, item) {
                var valor = item.id_categoria;
                var text = item.nombre;
                $('#ddlCategoria').append($("<option></option>").val(valor).html(text));
            });
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

function loadProducto(id) {
    $.ajax({
        url: "/Producto/Listar",
        type: "POST",
        data: "{id: '" + id + "'}",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            $('#ddlProducto').empty();
            $('#ddlProducto').append("<option value='0' selected>--Seleccione--</option>");
            $.each(result, function (key, item) {
                var valor = item.id_producto;
                var text = item.nombre;
                $('#ddlProducto').append($("<option></option>").val(valor).html(text));
            });
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

function NuevoItem() {
    $("#ddlCategoria").val('0');
    $('#ddlCategoria').css('border-color', 'lightgrey');
    $("#ddlProducto").val('0');
    $('#ddlProducto').css('border-color', 'lightgrey');
    $("#txtCantidad").val('0');
    $('#txtCantidad').css('border-color', 'lightgrey');

    $(".modal").modal("show");
};

function GrabarFactura()
{
    bootbox.confirm({
        title: "Ver Factura?",
        message: "Esta ud. seguro de Grabar la Factura actual!.",
        buttons: {
            confirm: { label: 'Aceptar', className: 'btn-primary' },
            cancel: { label: 'Cancelar', className: 'btn-secondary' }
        },
        callback: function (result) {
            if (result) {
                GrabandoFactura();
            }
        }
    });
}

function GrabandoFactura()
{
    var num_factura = $("#num_factura").val();
    var fecha = $("#fecha").val();
    var id_cliente = $('select[id="id_cliente"] option:selected').val();
    var num_pago = $('select[id="num_pago"] option:selected').val();

    var detalle = $('#tblDetalle').tableToJSON({
        ignoreColumns: [2, 5, 6]
    });

    var factura = {
        "Factura": {
            "num_factura": num_factura,
            "id_cliente": id_cliente,
            "fecha": fecha,
            "num_pago": num_pago,
            //"cliente": {
            //    "id_cliente": 0,
            //    "pNombre": null,
            //    "sNombre": null,
            //    "pApellido": null,
            //    "sApellido": null,
            //    "direccion": null,
            //    "fechaNacimiento": "/Date(-62135578800000)/",
            //    "telefono": null,
            //    "email": null,
            //    "ciudad": null,
            //    "Nombres": null
            //},
            //"modo_pago": {
            //    "num_pago": 0,
            //    "nombre": null
            //},
            "detalle": detalle
            //"detalle": [
            //  {
            //      "num_detalle": 4,
            //      "num_factura": 6,
            //      "id_producto": 1,
            //      "cantidad": 3,
            //      "precio": 5,
            //      "producto": {
            //          "id_producto": 1,
            //          "nombre": "Frejol Canario",
            //          "precio": null,
            //          "stock": null,
            //          "id_categoria": null,
            //          "categoria": {
            //              "id_categoria": 0,
            //              "nombre": null,
            //              "descripcion": null
            //          }
            //      },
            //      "subtotal": 15
            //  },
            //  {
            //      "num_detalle": 5,
            //      "num_factura": 6,
            //      "id_producto": 2,
            //      "cantidad": 2,
            //      "precio": 6,
            //      "producto": {
            //          "id_producto": 2,
            //          "nombre": "Pallar Bebe",
            //          "precio": null,
            //          "stock": null,
            //          "id_categoria": null,
            //          "categoria": {
            //              "id_categoria": 0,
            //              "nombre": null,
            //              "descripcion": null
            //          }
            //      },
            //      "subtotal": 12
            //  }
            //],
        }
    };

    $.ajax({
        url: "/Factura/Grabar",
        data: JSON.stringify(factura),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            debugger;
            $("#num_factura").val(result.Id)
            msgAlerta(false, 'Ver Factura', result.Mensaje);
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

function GrabarNuevoItem()
{
    var res = GrabarNuevoItem_validar();
    if (res == false) {
        return false;
    }

    var id_producto = $('select[id="ddlProducto"] option:selected').val();
    var ds_producto = $('select[id="ddlProducto"] option:selected').text();
    var precio = 0.0;
    var cantidad = $('#txtCantidad').val();
    var subtotal = 0.0;

    $.ajax({
        url: "/Producto/Traer",
        type: "POST",
        data: "{id: '" + id_producto + "'}",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            precio = result.precio;

            var tag = "<tr><td>@Item</td><td>@Id_Producto</td><td>@Producto</td><td>@Precio</td><td>@Cantidad</td><td>@Subtotal</td><td><a href='#' onclick='eliminarItem(this)'>Eliminar</a></td></tr>";
            tag = tag.replace('@Item', '0');
            tag = tag.replace('@Id_Producto', id_producto);
            tag = tag.replace('@Producto', ds_producto);
            tag = tag.replace('@Precio', precio);
            tag = tag.replace('@Cantidad', cantidad);
            subtotal = (parseFloat(precio) * parseFloat(cantidad));
            tag = tag.replace('@Subtotal', subtotal);

            $("#tblDetalle > tbody:last-child").append(tag);

            $(".modal").modal("hide");
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

function GrabarNuevoItem_validar() {
    var isValid = true;

    if ($('select[id="ddlCategoria"] option:selected').val().trim() == "0") {
        $('#ddlCategoria').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#ddlCategoria').css('border-color', 'lightgrey');
    }

    if ($('select[id="ddlProducto"] option:selected').val().trim() == "0") {
        $('#ddlProducto').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#ddlProducto').css('border-color', 'lightgrey');
    }

    if ($('#txtCantidad').val().trim() == "0") {
        $('#txtCantidad').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#txtCantidad').css('border-color', 'lightgrey');
    }
    return isValid;
}

function eliminarItem(obj)
{
    $(obj).parents("tr").remove();
}

