var estado = 0;

function MensajeIncorrecto(resultado) {
    sweetAlert("Error", resultado, "error");
}

function MensajeCorrecto(resultado) {
    sweetAlert("Exito", resultado, "success");
}

function alerta(resultado) {
    sweetAlert("Advertencia", resultado, "error");
}


function GuardarApiKey() {
    var res = "";

    var p1 = $("#ApiKey").val();
    var p2 = $("#idEjecucionProceso").val();
    var p3 = $("#idProceso").val();
    var p4 = $("#nombreProceso").val();
    var p5 = "1";

    if (document.getElementById("switchfield1").checked == true) {
        estado = 1;
    }
    else {
        estado = 0;
    }

    var obj = JSON.stringify({
        IdEjecucionproceso: p2,
        IdProceso: p3,
        NombreProceso: p4,
        Estado: estado,
        Tipo: p5,
    });

    var md = $("#processing-modal");
    md.modal('show');

    $.ajax({
        type: "POST",
        url: "./EjecucionRPA.aspx/GuardarAuditoriaProceso",
        data: obj,
        contentType: 'application/json; charset=utf-8',
        error: function (xhr, ajaxOptions, thrownError) {
            console.log(xhr.status + " \n" + xhr.responseText, "\n" + thrownError);
            md.modal('hide');
        },
        success: function (data) {
            if (data.d) {

                $("#ApiKey").val("");
                $("#idEjecucionProceso").val("");
                $("#idProceso").val("");
                $("#nombreProceso").val("");
                document.getElementById("switchfield1").checked = false;
                md.modal('hide');
                MensajeCorrecto(data.d);
            } else {
                md.modal('hide');
                MensajeIncorrecto(data.d);
            }
        }
    });

}