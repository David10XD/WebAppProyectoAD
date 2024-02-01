var id1 = 0;
var id2 = 0;

function borrarPregutas() {

    $('#IdPreguentas2').empty();
    $('#IdPreguentas1').empty();
}

function MensajeIncorrecto(resultado) {
    sweetAlert("Error", resultado, "error");
}

function MensajeCorrecto(resultado) {
    sweetAlert("Exito", resultado, "success");
}

function alerta() {
    sweetAlert("Advertencia", "Las respuesta debe ser iguales", "error");
}

function VerPregunta() {

    $.ajax({
        type: "POST",
        url: "./ActualizarUsuario.aspx/consultapreguntas",
        data: {},
        dataType: "json",
        contentType: 'application/json; charset=utf-8',
        error: function (xhr, ajaxOptions, thrownError) {
            console.log(xhr.status + " \n" + xhr.responseText, "\n" + thrownError);
        },
        success: function (data) {
            var datos = data.d;    
          
            var combo1 = document.getElementById("IdPreguentas1");
            combo1.selectedIndex = datos[0].IdPreguntas;
            $("#txtRespuesta1").val(datos[0].Respuestas);
            $("#txtRespuestaConfirmacion1").val(datos[0].Respuestas);
            $("#ContentPlaceHolder1_txtId1").val(datos[0].IdProceso);
            id1 = datos[0].IdProceso;

            var combo2 = document.getElementById("IdPreguentas2");
            combo2.selectedIndex = datos[1].IdPreguntas;;
            $("#txtRespuesta2").val(datos[1].Respuestas);
            $("#txtRespuestaConfirmacion2").val(datos[1].Respuestas);
            $("#ContentPlaceHolder1_txtId2").val(datos[1].IdProceso);
            id2 = datos[1].IdProceso;

            document.getElementById("Respuestas").style.display = "none";
            document.getElementById("Preguntas").style.display = "block";

            document.getElementById("btn_guardar2").style.display = "none";
            document.getElementById("btn_modificar").style.display = "block";
        }

    });

}

function ExisteRespuestas() {

    $.ajax({
        type: "POST",
        url: "./ActualizarUsuario.aspx/Existepreguntas",
        data: {},
        dataType: "json",
        contentType: 'application/json; charset=utf-8',
        error: function (xhr, ajaxOptions, thrownError) {
            console.log(xhr.status + " \n" + xhr.responseText, "\n" + thrownError);
        },
        success: function (data) {
            var datos = data.d;

            if (datos.length > 0) {
                //document.getElementById("Respuestas").style.display = "block";
                //document.getElementById("Preguntas").style.display = "none";
                $("#Respuestas").show();
                $("#Preguntas").hide();
            }

        }

    });

}

function RecorreJSONSelect(div, selectValues) {

    $(div).empty();

    $.each(selectValues, function (key, value) {
        //
        // Despliegue de datos
        //
        $(div)
            .append($("<option></option>")
                .attr("value", value.Id)
                .text(value.Valor));
    });

    return;
}

function CargarPreguntas() {

    //borrarPregutas();

    $.ajax({
        type: "POST",
        url: "./ActualizarUsuario.aspx/ConsultarPreguntasCombos",
        data: {},
        dataType: "json",
        contentType: 'application/json; charset=utf-8',
        error: function (xhr, ajaxOptions, thrownError) {
            console.log(xhr.status + " \n" + xhr.responseText, "\n" + thrownError);
        },
        success: function (data) {
            var datos = JSON.parse(data.d);

            if (data.d) {

                RecorreJSONSelect("#IdPreguentas1", datos);
                RecorreJSONSelect("#IdPreguentas2", datos);

                //$(datos).each(function () {
                //    var option = $(document.createElement('option'));

                //    option.text(this.Descripcion);
                //    option.val(this.IdPreguntas);

                //    $("#IdPreguentas1").append(option);
                //});

                //$(datos).each(function () {
                //    var option = $(document.createElement('option'));

                //    option.text(this.Descripcion);
                //    option.val(this.IdPreguntas);

                //    $("#IdPreguentas2").append(option);
                //});

                //
            }
            else {
                document.getElementById("btnDetalle2").style.display = "block";
            }
        }

    });

}

function guardarPregunta() {

    var v1 = document.getElementById("IdPreguentas1");
    var sl1 = v1.selectedIndex;

    var v2 = document.getElementById("IdPreguentas2");
    var sl2 = v2.selectedIndex;

    var p1 = $("#txtRespuesta1").val();
    var p2 = $("#txtRespuesta2").val();

    var p1r = $("#txtRespuestaConfirmacion1").val();
    var p2r = $("#txtRespuestaConfirmacion2").val();

    if (p1 == p1r && p2 == p2r) {
        var str = sl1 + ";" + p1 + "-" + sl2 + ";" + p2;

        var obj = JSON.stringify({
            preguntas: str
        });

        $.ajax({
            type: "POST",
            url: "./ActualizarUsuario.aspx/GuardarPreguntas",
            data: obj,
            contentType: 'application/json; charset=utf-8',
            error: function (xhr, ajaxOptions, thrownError) {
                console.log(xhr.status + " \n" + xhr.responseText, "\n" + thrownError);
            },
            success: function (data) {
                if (data.d) {
                    $("#txtRespuesta1").val("");
                    $("#txtRespuestaConfirmacion1").val("");
                    $("#txtRespuesta2").val("");
                    $("#txtRespuestaConfirmacion2").val("");
                    var combo1 = document.getElementById("IdPreguentas1");
                    combo1.selectedIndex = 0;
                    var combo2 = document.getElementById("IdPreguentas2");
                    combo2.selectedIndex = 0;
                    sendDataAjax();
                    document.getElementById("Respuestas").style.display = "block";
                    document.getElementById("Preguntas").style.display = "none";
                    MensajeCorrecto(data.d);
                    //window.location.href = "./Login.aspx";
                } else {
                    MensajeIncorrecto(data.d);
                }
            }
        });
    }
    else
    {
        alerta();
    }

}

function modificarPregunta() {

    var v1 = document.getElementById("IdPreguentas1");
    var sl1 = v1.selectedIndex;

    var v2 = document.getElementById("IdPreguentas2");
    var sl2 = v2.selectedIndex;

    var p1 = $("#txtRespuesta1").val();
    var p2 = $("#txtRespuesta2").val();

    var p1r = $("#txtRespuestaConfirmacion1").val();
    var p2r = $("#txtRespuestaConfirmacion2").val();

    //var id1 = $("#ContentPlaceHolder1_txtId1").val();
    //var id2 = $("#ContentPlaceHolder1_txtId2").val();

    if (p1 == p1r && p2 == p2r) {

        var str = sl1 + ";" + p1 + ";" + id1 + "-" + sl2 + ";" + p2 + ";" + id2;

        var obj = JSON.stringify({
            preguntas: str
        });

        $.ajax({
            type: "POST",
            url: "./ActualizarUsuario.aspx/ActualizarPreguntas",
            data: obj,
            contentType: 'application/json; charset=utf-8',
            error: function (xhr, ajaxOptions, thrownError) {
                console.log(xhr.status + " \n" + xhr.responseText, "\n" + thrownError);
            },
            success: function (data) {
                if (data.d) {

                    $("#txtRespuesta1").val("");
                    $("#txtRespuestaConfirmacion1").val("");
                    $("#txtRespuesta2").val("");
                    $("#txtRespuestaConfirmacion2").val("");

                    $("#ContentPlaceHolder1_txtId1").val("");
                    $("#ContentPlaceHolder1_txtId2").val("");

                    var combo1 = document.getElementById("IdPreguentas1");
                    combo1.selectedIndex = 0;
                    var combo2 = document.getElementById("IdPreguentas2");
                    combo2.selectedIndex = 0;
                    sendDataAjax();
                    document.getElementById("Respuestas").style.display = "block";
                    document.getElementById("Preguntas").style.display = "none";
                    MensajeCorrecto(data.d);
                } else {
                    MensajeIncorrecto(data.d);
                }
            }
        });
    }
    else {
        alerta();
    }
}

function sendDataAjax() {
    ExisteRespuestas();
    CargarPreguntas();
}

// Llamando a la funcion de ajax al cargar el documento
sendDataAjax();