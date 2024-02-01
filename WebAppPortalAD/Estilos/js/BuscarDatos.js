const valor = 3;

function borrarPregutas1() {

    $('#IdPreguentas1').empty();
}

function borrarPregutas2() {

    $('#IdPreguentas2').empty();
}

function MensajeIncorrecto(resultado) {
    sweetAlert("Error", resultado, "error");
}

function MensajeCorrecto(resultado) {
    sweetAlert("Exito", resultado, "success");
}

function RecuperarContrasenia() {
    var r1 = $("#txtUsuario2").val();
    var r2 = $("#txtPassword2").val();
    var r3 = $("#txtPassword3").val();

    var obj = JSON.stringify({
        usuario: r1,
        cadena1: r2,
        cadena2: r3
    });

    var md = $("#processing-modal");
    md.modal('show');

    $.ajax({
        type: "POST",
        /*url: "./Paginas/Login.aspx/RecuperarContrasenia",*/
        url: "./Login.aspx/RecuperarContrasenia",
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

function alerta(resultado) {
    sweetAlert("Advertencia", resultado, "error");
}

function ConsultarRespuesta() {

        if ($("#Respuesta").val() == "") {
            alerta("Debe ingresar la respuesta");
        }
        else {
            var v1 = document.getElementById("IdPreguentas1");
            var selectedoResponsable = v1.options[v1.selectedIndex].text;
            var sl1 = v1.selectedIndex;
            if (sl1 == "0") {
                alerta("Debe seleccionar una pregunta");
            }
            else {
                var obj = JSON.stringify({
                    Usuario: $("#usuario").val(),
                    IdPreguntas: sl1,
                    pregunta: selectedoResponsable,
                    Tipo: 1
                });

                $.ajax({
                    type: "POST",
                    /*url: "./Paginas/Login.aspx/ConsultarPreguntas",*/
                    url: "./Login.aspx/ConsultarPreguntas",
                    data: obj,
                    dataType: "json",
                    contentType: 'application/json; charset=utf-8',
                    error: function (xhr, ajaxOptions, thrownError) {
                        console.log(xhr.status + " \n" + xhr.responseText, "\n" + thrownError);
                    },
                    success: function (data) {
                        var datos = data.d;
                        $(datos).each(function () {
                            var valor = $("#Respuesta").val();
                            if (valor == this.Respuestas) {
                                document.getElementById("btnConsultar").style.display = "none";
                                document.getElementById("btnDesbloquear").style.display = "block";
                            }
                            else {
                                alerta("la respuesta es incorrecta");
                            }
                        });
                    }
                });
            }
        }
}

function ConsultarRespuesta2() {
    if ($("#Respuesta2").val() == "") {
        alerta("Debe ingresar la respuesta");
    }
    else {
        var v1 = document.getElementById("IdPreguentas2");
        var selectedoResponsable = v1.options[v1.selectedIndex].text;
        var sl1 = v1.selectedIndex;
        if (sl1 == "0") {
            alerta("Debe seleccionar una pregunta");
        }
        else {
            var obj = JSON.stringify({
                Usuario: $("#usuario").val(),
                IdPreguntas: sl1,
                pregunta: selectedoResponsable,
                Tipo: 1
            });

            $.ajax({
                type: "POST",
                /*url: "./Paginas/Login.aspx/ConsultarRespuesta",*/
                url: "./Login.aspx/ConsultarRespuesta",
                data: obj,
                dataType: "json",
                contentType: 'application/json; charset=utf-8',
                error: function (xhr, ajaxOptions, thrownError) {
                    console.log(xhr.status + " \n" + xhr.responseText, "\n" + thrownError);
                },
                success: function (data) {
                    var datos = data.d;
                    $(datos).each(function () {
                        var valor = $("#Respuesta2").val();
                        if (valor == this.Respuestas) {
                            document.getElementById("proceso1").style.display = "block";
                            document.getElementById("proceso2").style.display = "none";
                            $("#txtUsuario2").val($("#usuario").val());
                        }
                        else {
                            alerta("la respuesta es incorrecta");
                        }
                    });
                }
            });
        }
    }
}

function BuscarUsuario() {
    if ($("#usuario").val() == "") {
        alerta("Debe ingresar el usuario..");
    }
    else {

        $("#NomUsuario").val($("#usuario").val());

        var obj = JSON.stringify({
            Usuario: $("#usuario").val(),
            IdPreguntas: 0,
            pregunta:'',
            Tipo: 0
        });

        borrarPregutas1();

        $.ajax({
            type: "POST",
            /*url: "./Paginas/Login.aspx/ConsultarPreguntas",*/
            url: "./Login.aspx/ConsultarPreguntas",
            data: obj,
            dataType: "json",
            contentType: 'application/json; charset=utf-8',
            error: function (xhr, ajaxOptions, thrownError) {
                console.log(xhr.status + " \n" + xhr.responseText, "\n" + thrownError);
            },
            success: function (data) {
                var datos = data.d;

                $(datos).each(function () {
                    var option = $(document.createElement('option'));
                    option.text(this.Descripcion);
                    option.val(this.IdPreguntas);

                    $("#IdPreguentas1").append(option);
                });
            }

        });


         jQuery(function ($) {
            $(document).on('click', '.toolbar a[data-target]', function (e) {
                e.preventDefault();
                var target = $(this).data('target');
                $('.widget-box.visible').removeClass('visible');//hide others
                $(target).addClass('visible');//show target
            });
        });
    }
}

function CambiarContrasenia() {

        if ($("#usuario").val() == "") {
            alerta("Debe ingresar el usuario..");
        }
        else {

            $("#NomUsuario").val($("#usuario").val());

            var obj = JSON.stringify({
                Usuario: $("#usuario").val(),
                IdPreguntas: 0,
                pregunta: '',
                Tipo: 0
            });

            borrarPregutas2();

            $.ajax({
                type: "POST",
                /*url: "./Paginas/Login.aspx/ConsultarPreguntas",*/
                url: "./Login.aspx/ConsultarPreguntas",
                data: obj,
                dataType: "json",
                contentType: 'application/json; charset=utf-8',
                error: function (xhr, ajaxOptions, thrownError) {
                    console.log(xhr.status + " \n" + xhr.responseText, "\n" + thrownError);
                },
                success: function (data) {
                    var datos = data.d;

                    $(datos).each(function () {
                        var option = $(document.createElement('option'));
                        option.text(this.Descripcion);
                        option.val(this.IdPreguntas);

                        $("#IdPreguentas2").append(option);
                    });
                }

            });


            jQuery(function ($) {
                $(document).on('click', '.toolbar a[data-target]', function (e) {
                    e.preventDefault();
                    var target = $(this).data('target');
                    $('.widget-box.visible').removeClass('visible');//hide others
                    $(target).addClass('visible');//show target
                });
            });
        }

}

function desbloquerUsuario() {
    var obj = JSON.stringify({
        usuario: $("#usuario").val()
    });

    $.ajax({
        type: "POST",
        /*url: "./Paginas/Login.aspx/desbloquerUsuario",*/
        url: "./Login.aspx/desbloquerUsuario",
        data: obj,
        dataType: "json",
        contentType: 'application/json; charset=utf-8',
        error: function (xhr, ajaxOptions, thrownError) {
            console.log(xhr.status + " \n" + xhr.responseText, "\n" + thrownError);
        },
        success: function (data) {
            var separador = ";";
            var query = data.d;
            var arrayDeCadenas = query.split(separador);
            if (arrayDeCadenas[0] == 1) {
                alerta(arrayDeCadenas[1]);
                document.getElementById("btnSalir").style.display = "block";
                document.getElementById("btnDesbloquear").style.display = "none";
            }
        }

    });
}

function Salir() {
    window.location.href = "Login.aspx";
}

function sendDataAjax() {
   /* BuscarUsuario();*/
}

// Llamando a la funcion de ajax al cargar el documento
sendDataAjax();