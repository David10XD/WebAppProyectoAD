
var estado = 0;
var tabla2;
var tabla3;

function MensajeIncorrecto(resultado) {
    sweetAlert("Error", resultado, "error");
}

function MensajeCorrecto(resultado) {
    sweetAlert("Exito", resultado, "success");
}

function alerta(resultado) {
    sweetAlert("Advertencia", resultado, "error");
}

function addRowDTUsuarios(data2) {
    tabla2 = $("#tbl_usuarios").DataTable({
        "aaSorting": [[0, 'desc']],
        "bSort": true,
        "bDestroy": true,
        "aoColumns": [
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            { "bSortable": false }
        ],
        select: {
            style: 'multi'
        }
    });

    //tabla = $("#tbl_menus").DataTable();
    tabla2.fnClearTable();

    for (var i = 0; i < data2.length; i++) {

        tabla2.fnAddData([
            data2[i].UserName,
            data2[i].JobTitle,
            data2[i].Email,
            data2[i].DisplayName,
            data2[i].Company,
            data2[i].Deparment,
            data2[i].Phone,
            data2[i].Estado,
            '<button type="button" value="Eliminar" title="Editar Usuarios" class="btn btn-success btn-Editar  btn-xs"><i class="fa fa-pencil" aria-hidden="true"></i></button>&nbsp;|&nbsp;<button type="button" value="Actualizar" title="Inactivar usuario" class="btn btn-danger btn-Eliminar  btn-xs"><i class="fa fa-times" aria-hidden="true"></i></button>&nbsp;|<button type="button" value="Actualizar" title="Activar usuario" class="btn btn-primary btn-Activar  btn-xs"><i class="fa fa-check" aria-hidden="true"></i></button>'
        ]);
    }
} 



$(document).on('click', '.btn-Editar', function (e) {
    e.preventDefault();

    var row = $(this).parent().parent()[0];
    var datarow1 = tabla2.fnGetData(row);
    fillModalData(datarow1);
});

$(document).on('click', '.btn-Eliminar', function (e) {
    e.preventDefault();

    var row = $(this).parent().parent()[0];
    var datarow1 = tabla2.fnGetData(row);
    fillModalDataEliminar(datarow1);
});

$(document).on('click', '.btn-Activar', function (e) {
    e.preventDefault();

    var row = $(this).parent().parent()[0];
    var datarow1 = tabla2.fnGetData(row);
    fillModalDataActivar(datarow1);
});



function fillModalDataEliminar(data2) {

    var obj = JSON.stringify({
        usuario: data2[0],
        tipo: "1"
    });

    var md = $("#processing-modal");
    md.modal('show');

    $.ajax({
        type: "POST",
        url: "./RegistrarUsuario.aspx/ActivarInactivarUsuario",
        data: obj,
        contentType: 'application/json; charset=utf-8',
        error: function (xhr, ajaxOptions, thrownError) {
            console.log(xhr.status + " \n" + xhr.responseText, "\n" + thrownError);
            md.modal('hide');
        },
        success: function (data) {
            if (data.d) {
                md.modal('hide');
                MensajeCorrecto(data.d);
            } else {
                md.modal('hide');
                MensajeIncorrecto(data.d);
            }
        }
    });

}

function fillModalDataActivar(data2) {

    var obj = JSON.stringify({
        usuario: data2[0],
        tipo: "2"
    });

    var md = $("#processing-modal");
    md.modal('show');

    $.ajax({
        type: "POST",
        url: "./RegistrarUsuario.aspx/ActivarInactivarUsuario",
        data: obj,
        contentType: 'application/json; charset=utf-8',
        error: function (xhr, ajaxOptions, thrownError) {
            console.log(xhr.status + " \n" + xhr.responseText, "\n" + thrownError);
            md.modal('hide');
        },
        success: function (data) {
            if (data.d) {
                md.modal('hide');
                MensajeCorrecto(data.d);
            } else {
                md.modal('hide');
                MensajeIncorrecto(data.d);
            }
        }
    });

}


function addRowDTUsuarioUO(data2) {
    tabla3 = $("#tbl_unidad").DataTable({
        "aaSorting": [[0, 'desc']],
        "bSort": true,
        "bDestroy": true,
        "aoColumns": [
            null,
            null,
            null,
            null,
            { "bSortable": false }
        ],
        select: {
            style: 'multi'
        }
    });

    //tabla = $("#tbl_menus").DataTable();
    tabla3.fnClearTable();

    for (var i = 0; i < data2.length; i++) {

        tabla3.fnAddData([
            data2[i].id,
            data2[i].Nombre,
            data2[i].Unidad,
            data2[i].Estado,
            '<button type="button"title="Editar Usuarios" class="btn btn-success btn-EditarUO  btn-xs"><i class="fa fa-floppy-o" aria-hidden="true"></i></button>'
        ]);
    }
}

$(document).on('click', '.btn-EditarUO', function (e) {
    e.preventDefault();

    var row = $(this).parent().parent()[0];
    var datarow1 = tabla3.fnGetData(row);
    actualizarUnidad(datarow1);
});

// cargar datos en el modal
function fillModalData(data) {

    var obj0 = data[0];
    var obj1 = data[1];
    var obj2 = data[2];

    $("#formUsuario2").val(data[0]);
    $("#formTelefono2").val(data[6]);
    $("#formEmail2").val(data[2]);
    $("#formCompania2").val(data[4]);
    $("#formDepartamento2").val(data[5]);
    $("#formTitulo2").val(data[1]);

    $('.nav-tabs a[href="#Actualiza"]').tab('show');
}

function guardarUsuario() {
    var res = "";

    var p1 = $("#formUsuario").val();
    var p2 = $("#formPrimerNombre").val();
    var p3 = $("#formSegundoNombre").val();
    var p4 = $("#formPrimerApellido").val();
    var p5 = $("#formSegundoApellido").val();
    var p6 = $("#formTelefono").val();
    var p7 = $("#formEmail").val();
    var p8 = $("#formfield3").val();
    var p9 = $("#formCompania").val();
    var p10 = $("#formDepartamento").val();
    var p11 = $("#formTitulo").val();

    var str = p1 + ";" + p2 + ";" + p3 + ";" + p4 + ";" + p5 + ";" + p6 + ";" + p7 + ";" + p8 + ";" + p9 + ";" + p10 +";" + p11;

    var obj = JSON.stringify({
        preguntas: str
    });

    $.ajax({
        type: "POST",
        url: "./RegistrarUsuario.aspx/GuardarUsuarios",
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

                $("#formUsuario").val("");
                $("#formPrimerNombre").val("");
                $("#formSegundoNombre").val(""); 
                $("#formPrimerApellido").val("");
                $("#formSegundoApellido").val(""); 
                $("#formTelefono").val("");
                $("#formEmail").val("");
                $("#formfield3").val("");

                MensajeCorrecto(data.d);
                ListaUsuario();
                $('.nav-tabs a[href="#Lista"]').tab('show');
            } else {
                MensajeIncorrecto(data.d);
            }
        }
    });

}

function actualizarUnidad(datos) {

    var str1 = datos[0];
    var str2 = datos[3];

    var obj = JSON.stringify({
        IdUO: str1,
        Estado: str2
    });

    $.ajax({
        type: "POST",
        url: "./RegistrarUsuario.aspx/ActualizarUnidad",
        data: obj,
        contentType: 'application/json; charset=utf-8',
        error: function (xhr, ajaxOptions, thrownError) {
            console.log(xhr.status + " \n" + xhr.responseText, "\n" + thrownError);
        },
        success: function (data) {
            if (data.d) {
                MensajeCorrecto(data.d);
                ListaUO();
                CargarUnidad();
            } else {
                MensajeIncorrecto(data.d);
            }
        }
    });

}


function ActualizaUsuario() {
    
    if (estado == 1) {

        if ($("#Password1").val() == "" && $("#Password2").val() == "") {
            alerta("Debe ingresar la nueva clave");
            return;
        }
        else {
            if ($("#Password1").val() == $("#Password2").val()) {

            }
            else {
                alerta("Las clave no son iguales ");
                return;
            }
        }
    }

    var res = "";
    var p1 = "";
    var p2 = "";
    var p3 = "";
    var p4 = "";
    var p5 = "";
    var p6 = "";

    if ($("#formUsuario2").val() == "") {
        p1 = " ";
    }
    else {
        p1 = $("#formUsuario2").val();
    }
    if ($("#formCompania2").val() == "") {
        p2 = " ";
    }
    else {
        p2 = $("#formCompania2").val();
    }
    if ($("#formDepartamento2").val() == "") {
        p3 = " ";
    }
    else {
        p3 = $("#formDepartamento2").val();
    }
    if ($("#formTitulo2").val() == "") {
        p4 = " ";
    }
    else {
        p4 = $("#formTitulo2").val();
    }
    if ($("#formEmail2").val() == "") {
        p5 = " ";
    }
    else {
        p5 = $("#formEmail2").val();
    }
    if ($("#formEmail2").val() == "") {
        p6 = " ";
    }
    else {
        p6 = $("#formTelefono2").val()
    }

    var str = p1 + ";" + p2 + ";" + p3 + ";" + p4 + ";" + p5 + ";" + p6 + ";" + estado + ";" + $("#Password1").val();

    var obj = JSON.stringify({
        preguntas: str
    });



    $.ajax({
        type: "POST",
        url: "./RegistrarUsuario.aspx/ActualizarUsuarios",
        data: obj,
        contentType: 'application/json; charset=utf-8',
        error: function (xhr, ajaxOptions, thrownError) {
            console.log(xhr.status + " \n" + xhr.responseText, "\n" + thrownError);
        },
        success: function (data) {
            if (data.d) {
                $("#formUsuario2").val("");
                $("#formCompania").val("");
                $("#formDepartamento").val("");
                $("#formTitulo").val("");
                $("#formTelefono2").val("");
                $("#formEmail2").val("");
                document.getElementById("idContrasenia1").style.display = "none";
                document.getElementById("idContrasenia2").style.display = "none";
                document.getElementById("switchfield1").checked = false;
                $("#Password1").val("");
                $("#Password2").val("");
                MensajeCorrecto(data.d);
                ListaUsuario();
                $('.nav-tabs a[href="#Lista"]').tab('show');
            } else {
                MensajeIncorrecto(data.d);
            }
        }
    });

}

function ListaUsuario() {
    var str = "";
    var obj = JSON.stringify({
        respuesta: str
    });

    var md = $("#processing-modal");
    md.modal('show');

    $.ajax({
        type: "POST",
        url: "./RegistrarUsuario.aspx/ListaUsuarios",
        data: obj,
        contentType: 'application/json; charset=utf-8',
        error: function (xhr, ajaxOptions, thrownError) {
            console.log(xhr.status + " \n" + xhr.responseText, "\n" + thrownError);
            md.modal('hide');
        },
        success: function (data) {
            if (data.d) {
                var query = data.d;
                if (query.length > 0) {

                    if ($("#ContentPlaceHolder1_Id").val() != "") {
                        document.getElementById("ActualizarC").style.display = "block";
                        document.getElementById("combo").style.display = "block";
                        document.getElementById("tab1").style.display = "block";
                        document.getElementById("tab2").style.display = "block";
                        document.getElementById("tab3").style.display = "block";
                        document.getElementById("tab4").style.display = "block";
                    }

                    addRowDTUsuarios(data.d);
                    md.modal('hide');
                }
                else {
                    if (query.length == 0) {
                        document.getElementById("Principal").style.display = "none";
                        document.getElementById("error").style.display = "block";
                    }
                    md.modal('hide');
                }
            }
            else {
                if ($("#ContentPlaceHolder1_Id").val() == "1") {
                    document.getElementById("combo").style.display = "block";
                    document.getElementById("tab2").style.display = "block";
                } 
                md.modal('hide');
            }
        }
    });

}

function ListaUO() {
    var str = "";
    var obj = JSON.stringify({
        respuesta: str
    });

    var md = $("#processing-modal");
    md.modal('show');

    $.ajax({
        type: "POST",
        url: "./RegistrarUsuario.aspx/ListaUnidad",
        data: obj,
        contentType: 'application/json; charset=utf-8',
        error: function (xhr, ajaxOptions, thrownError) {
            console.log(xhr.status + " \n" + xhr.responseText, "\n" + thrownError);
            md.modal('hide');
        },
        success: function (data) {
            if (data.d) {
                var query = data.d;
                if (query.length > 0) {
                    addRowDTUsuarioUO(data.d);
                    md.modal('hide');        
                }
            }
        }
    });

}

function CargarDatos() {
    var v1 = document.getElementById("IdPreguentas1");
    var selectedoResponsable = v1.options[v1.selectedIndex].text;
    var sl1 = v1.value;
    if (sl1 == "0") {
        //alerta("Debe seleccionar un grupo");
    }
    else {
        ListaUsuarioPorContenedor(sl1, selectedoResponsable);
    }
}

function ListaUsuarioPorContenedor(id,id2) {
    var str = id;
    var obj = JSON.stringify({
        respuesta: str,
        respuesta2: id2
    });

    var md = $("#processing-modal");
    md.modal('show');

    $.ajax({
        type: "POST",
        url: "./RegistrarUsuario.aspx/ListaUsuarioPorContenedor",
        data: obj,
        contentType: 'application/json; charset=utf-8',
        error: function (xhr, ajaxOptions, thrownError) {
            console.log(xhr.status + " \n" + xhr.responseText, "\n" + thrownError);
            md.modal('hide');
        },
        success: function (data) {
            if (data.d) {
                addRowDTUsuarios(data.d);
                md.modal('hide');
            } else {
                md.modal('hide');
            }
        }
    });

}

function BuscarUsuario() {
    var v1 = document.getElementById("IdPreguentas1");
    var selectedoResponsable = v1.options[v1.selectedIndex].text;
    var sl1 = v1.selectedIndex;
    BuscarUsuarioPorContenedor($("#formBuscar").val(), selectedoResponsable);
}

function BuscarUsuarioPorContenedor(id, id2) {
    var str = id;
    var obj = JSON.stringify({
        respuesta: str,
        respuesta2: id2
    });

    var md = $("#processing-modal");
    md.modal('show');

    $.ajax({
        type: "POST",
        url: "./RegistrarUsuario.aspx/BuscarUsuarioPorContenedor",
        data: obj,
        contentType: 'application/json; charset=utf-8',
        error: function (xhr, ajaxOptions, thrownError) {
            console.log(xhr.status + " \n" + xhr.responseText, "\n" + thrownError);
            md.modal('hide');
        },
        success: function (data) {
            if (data.d) {
                addRowDTUsuarios(data.d);
                md.modal('hide');
            } else {
                md.modal('hide');
                alerta("No existe el usuario");
            }
        }
    });

}

function ConsultarUsuarios() {
    ListaUsuario();
}

function CambiarEstado() {

    if (document.getElementById("switchfield1").checked == true) {
        document.getElementById("idContrasenia1").style.display = "block";
        document.getElementById("idContrasenia2").style.display = "block";
        estado = 1;
    }
    else if (document.getElementById("switchfield1").checked == false) { 
        document.getElementById("idContrasenia1").style.display = "none";
        document.getElementById("idContrasenia2").style.display = "none";
        estado = 0;
    }
}

function CargarUnidad() {

    //borrarPregutas();

    $.ajax({
        type: "POST",
        url: "./RegistrarUsuario.aspx/ConsultarUnidadCombos",
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
            }
        }
    });

}

/////////////////////

    function combinarNombres() {
        // Obtener los valores de los campos de nombre y apellido
    let valorDominio = $("#ContentPlaceHolder1_TextBox1").val();
    var nombre1 = $("#formPrimerNombre").val().toLowerCase();
    var nombre2 = $("#formSegundoNombre").val().toLowerCase();
    var apellido1 = $("#formPrimerApellido").val().toLowerCase();
    var apellido2 = $("#formSegundoApellido").val().toLowerCase();
    
    var primerasDosLetrasNombre = nombre1.substring(0, 3);
    var numeroAleatorio = Math.floor(Math.random() * 99) + 1;
    //var signos = ['/', '*', '-', '.'];
    //var signoAleatorio = signos[Math.floor(Math.random() * signos.length)];
    // Combinar las letras, números y signos obtenidos
        //var combinacionFinal = primerasDosLetrasNombre  + apellido1 + numeroAleatorio + "@" + valorDominio;
    let combinacionFinal = primerasDosLetrasNombre + apellido1
    // Mostrar la combinación en el tercer campo
        $("#formSeleccionUsuario").val(combinacionFinal);

        let dominioGenerado = combinacionFinal +numeroAleatorio + "@" + valorDominio;
        $("#formEmailDominio").val(dominioGenerado);
    
        $.ajax({
            type: "POST",
        url: "./RegistrarUsuario.aspx/combinacion",
            data: {combinacion: combinacionFinal },
        success: function (data) {
            // Manejar la respuesta del servidor si esq lo hay
            console.log(data);
                    },
        error: function (error) {
            // Manejar errores si esq los hay
            console.error(error);
                    }
                });
        }

/////////////////

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

function sendDataAjax() {
    ListaUsuario();
    ListaUO();
    CargarUnidad();
}

// Llamando a la funcion de ajax al cargar el documento
sendDataAjax();

