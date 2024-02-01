function MensajeIncorrecto(resultado) {
    sweetAlert("Error", resultado, "error");
}

function MensajeCorrecto(resultado) {
    sweetAlert("Exito", resultado, "success");
}

function alerta(resultado) {
    sweetAlert("Advertencia", resultado, "error");
}

function RecuperarContrasenia() {
    var r1 = $("#ClaveActual").val();
    var r2 = $("#NuevaClave").val();
    var r3 = $("#ConfirmarClave").val();

    var obj = JSON.stringify({
        usuario: r1,
        cadena1: r2,
        cadena2: r3
    });

    var md = $("#processing-modal");
    md.modal('show');

    $.ajax({
        type: "POST",
          /*url: "./Paginas/CambiarPassword.aspx/RecuperarContrasenia",*/
        url: "./CambiarPassword.aspx/RecuperarContrasenia",
        data: obj,
        dataType: "json",
        contentType: 'application/json; charset=utf-8',
        error: function (xhr, ajaxOptions, thrownError) {
            console.log(xhr.status + " \n" + xhr.responseText, "\n" + thrownError);
            md.modal('hide');
        },
        success: function (data) {
            var datos = data.d;
            if (datos == "Contraseña actualizada.") {
                MensajeCorrecto(datos);
                md.modal('hide');
                window.location.href = "Login.aspx";
                //window.location.href = "https://credenciales.indoamerica.edu.ec/";             
            }
            else {
                md.modal('hide');
                MensajeIncorrecto(datos);
            }
        }
    });

}