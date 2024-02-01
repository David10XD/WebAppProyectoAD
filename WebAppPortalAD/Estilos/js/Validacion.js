function ValidarEstado() {
    var str = "";
    var obj = JSON.stringify({
        respuesta: str
    });

    $.ajax({
        type: "POST",
        url: "./DesbloquerUsuario.aspx/ValidarEstado",
        data: obj,
        contentType: 'application/json; charset=utf-8',
        error: function (xhr, ajaxOptions, thrownError) {
            console.log(xhr.status + " \n" + xhr.responseText, "\n" + thrownError);
        },
        success: function (data) {
            if (data.d) {
                var resul = data.d;
                if (resul == "512") {
                    document.getElementById("ContentPlaceHolder1_switchfield1").checked = true;
                }

            } 
        }
    });

}

function sendDataAjax() {
    ValidarEstado();
}

// Llamando a la funcion de ajax al cargar el documento
sendDataAjax();