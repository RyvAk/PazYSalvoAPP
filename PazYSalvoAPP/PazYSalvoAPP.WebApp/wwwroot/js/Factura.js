$(document).ready(function () {
    $("#btnVerFacturas").click(async function () {
        const url = $('#facturaContainer').data('listarfacturas');
        const response = await $.ajax({
            url: url,
            method: "GET",
            dataType: "html"
        });

        $("#tblListaDeFacturas").html(response);
        $("#btnVerFacturas").hide();
        $("#btnNuevaFactura").removeClass("visually-hidden"); // visually-hidden
    });

    $("#btnNuevaFactura").click(function () {
        mostrarModal();
    });

    $("#btnActualizarFactura").click(async function () {
        // const url = "@Url.Action("AgregarFacturas", "Factura")";

        const url = $('#facturaContainer').data('actualizarfacturas'); // actualizarfacturas
        const $formulario = $('#frmModalFactura'); // captura contenido form
        var factura = {
            //id: $('#id').val(),
            saldo: $formulario.find('#Saldo').val(),
            clienteid: $formulario.find('#ClienteId').val(),
            servicioadquiridoid: $formulario.find('#ServicioAdquiridoId').val(),
            mediodepagoid: $formulario.find('#MedioDePagoId').val(),
            estadoid: $formulario.find('#EstadoId').val()
        };
        //console.log(factura);
        const response = await $.ajax({
            url: url,
            method: "post",
            dataType: 'json',
            contentType: 'application/json',
            data: JSON.stringify(factura), // Serializa a JSON
            success: function () {
                alert("Factura agregada con éxito");
                $("#modal").modal("hide");
            },
            error: function (jqxhr, textstatus, errorthrown) {

                console.error("error adding invoice:", textstatus, errorthrown);

                alert("error! no se agregó factura");
                $("#modal").modal("hide");
            },
        });

    });

    $(document).on('click', ".btnEditarFactura", function () {
        //debugger;
        //const url = "@Url.Action("EditarFactura", "Factura")";

        const url = $('#facturaContainer').data('editarfacturas');
        const id = $(this).data('id');

        console.log('log');
        //console.log(factura);
        $.ajax({
            url: url,
            method: "get",
            data: id,
            dataType: "html",
            success: function (data) { // Agrega el parámetro 'data' aquí
                $("#modalEditar").html(data);
                $("#modalEditar").modal("show");
            },
            error: function (jqxhr, textstatus, errorthrown) {

                console.error("error adding invoice:", textstatus, errorthrown);

                alert("error!);
                        $("#modal").modal("hide");
            },
        });

    });

});

function mostrarModal() {
    $("#tituloModal").text("Nueva Factura");
    $("#frmModalFactura").trigger("reset");
    $("#modal").modal("show");
}