<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CambiarPassword.aspx.cs" Inherits="WebAppPortalAD.Paginas.CambiarPassword" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <meta charset="utf-8" />
    <title>Login | Self Service Password</title>

    <meta name="description" content="User login page" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0" />

    <!-- bootstrap & fontawesome -->
    <link rel="stylesheet" href="../Estilos/assets/css/bootstrap.min.css" />
    <link rel="stylesheet" href="../Estilos/assets/font-awesome/4.5.0/css/font-awesome.min.css" />

    <!-- text fonts -->
    <link rel="stylesheet" href="../Estilos/assets/css/fonts.googleapis.com.css" />

    <!-- ace styles -->
    <link rel="stylesheet" href="../Estilos/assets/css/ace.min.css" />

    <!--[if lte IE 9]>
			<link rel="stylesheet" href="assets/css/ace-part2.min.css" />
		<![endif]-->
    <link rel="stylesheet" href="../Estilos/assets/css/ace-rtl.min.css" />

    <style>
        /*css process modal -  processing-modal*/
        .modal-static {
            position: fixed;
            top: 50% !important;
            left: 50% !important;
            margin-top: -100px;
            margin-left: -150px;
            overflow: visible !important;
        }

            .modal-static,
            .modal-static .modal-dialog,
            .modal-static .modal-content {
                width: 300px;
                height: 90px;
            }

                .modal-static .modal-dialog,
                .modal-static .modal-content {
                    padding: 0 !important;
                    margin: 0 !important;
                }

                    .modal-static .modal-content .icon {
                    }

        .modal-text {
            text-align: center;
            font-family: Cambria;
            font-weight: bold;
            font-size: medium;
        }
    </style>

    <%--    <script src="https://cdnjs.cloudflare.com/ajax/libs/sweetalert/1.1.3/sweetalert-dev.js"></script>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/sweetalert/1.1.3/sweetalert.css" />--%>


    <script type="text/javascript"> 
        function ShowPopup() {
            //$("#myModal").modal("show");
            $('#myModal').show();
        }
    </script>

    <script type="text/javascript"> 
        function ShowPopupHide() {
            //$("#myModal").modal("none");
            $('#myModal').hide();
        }
    </script>

    <script type="text/javascript"> 
        function ShowPopup1() {
            //$("#myModal").modal("show");
            $('#myModalRecuperar').show();
        }
    </script>

    <script type="text/javascript"> 
        function ShowPopupHide1() {
            //$("#myModal").modal("none");
            $('#myModalRecuperar').hide();
        }
    </script>

    <script type="text/javascript"> 
        function pregunta() {
            //$("#myModal2").modal("show");
            $('#myModal2').show();
        }
    </script>

    <link href="../Estilos/sweetalert/css/sweetalert.css" rel="stylesheet" />
    <link href="../Estilos/assets/css/jquery-ui.css" rel="stylesheet" />
    <script src="../Estilos/sweetalert/js/sweetalert.min.js"></script>
    <script src="../Estilos/sweetalert/js/sweetalert.init.js"></script>
    <script src="../Estilos/js/Password.js?v=3"></script>

</head>

<body class="login-layout">
    <div class="main-container">
        <div class="main-content">
            <div class="row">
                <div class="col-sm-10 col-sm-offset-1">
                    <div class="login-container">
                        <div class="center">
                            <h1>
                                <%-- <i class="ace-icon fa fa-leaf green"></i>--%>
                                <span class="red3">Self Service Password</span>
                                <span class="white" id="id-text2"></span>
                            </h1>
                            <h4 class="light-blue" id="id-company-text"></h4>
                        </div>

                        <div class="space-6"></div>

                        <div class="position-relative">
                            <form runat="server">
                                <div id="login-box" class="login-box visible widget-box no-border">
                                    <div class="widget-body">
                                        <div class="panel-heading">
                                            <div class="text-center">
                                                <a href="#">
                                                    <img src="../Imagenes/ImagenesEmpresas/Logo-web.png" alt="Image" width="340" height="120" class="block-center img-rounded" style="background-color: #372b72">
                                                </a>
                                            </div>
                                        </div>
                                        <div class="widget-main">
                                            <h4 class="header blue lighter bigger" style="text-align: center">Actualizar Contraseña
                                            </h4>
                                            <fieldset>
                                                <label class="block clearfix">
                                                    <span class="block input-icon input-icon-right">
                                                        <input type="password" class="form-control" placeholder="Clave Actual" id="ClaveActual"/>
                                                        <i class="ace-icon fa fa-lock"></i>
                                                    </span>
                                                </label>

                                                <label class="block clearfix">
                                                    <span class="block input-icon input-icon-right">
                                                        <input type="password" class="form-control" placeholder="Nueva Clave" id="NuevaClave"/>
                                                        <i class="ace-icon fa fa-lock"></i>
                                                    </span>
                                                </label>

                                                <label class="block clearfix">
                                                    <span class="block input-icon input-icon-right">
                                                        <input type="password" class="form-control" placeholder="Confirmar Clave" id="ConfirmarClave"/>
                                                        <i class="ace-icon fa fa-lock"></i>
                                                    </span>
                                                </label>
                                                <div class="clearfix">
                                                    <button type="button" class="width-65 pull-right btn btn-sm btn-primary" id="btnRecuperar2" onclick="RecuperarContrasenia()" style="display: block">
                                                        <span class="bigger-110">Guardar</span>
                                                        <i class="ace-icon fa fa-arrow-right icon-on-right"></i>
                                                    </button>
                                                </div>

                                                <div class="space-4"></div>

                                                <div class="modal modal-static fade" id="myModal" role="dialog" aria-hidden="true">
                                                    <div class="modal-dialog">
                                                        <div class="modal-content">
                                                            <div class="modal-body">
                                                                <div class="text-center">
                                                                    <img src="../Estilos/assets/images/avatars/loading.gif" class="icon" />
                                                                    <h5><span class="modal-text">Procesando, Espere por favor... </span></h5>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="modal fade" id="myModal2" tabindex="-1" role="dialog" aria-labelledby="modal-eventLabel" aria-hidden="true">
                                                    <div class="modal-dialog" role="document">
                                                        <div class="modal-content">
                                                            <div class="modal-header">
                                                                <h5 class="modal-title" id="event-title2"></h5>
                                                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                                                    <span aria-hidden="true">&times;</span>
                                                                </button>
                                                            </div>
                                                            <div class="modal-body">
                                                                <div id="event-description2" class="alert alert-danger"></div>
                                                            </div>
                                                            <div class="modal-footer">
                                                                <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                            </fieldset>
                                            <%--</form>--%>
                                            <div class="social-or-login center">
                                                <asp:Label ID="Label1" runat="server" Text="Label" Visible="false" CssClass="bigger-110"></asp:Label>
                                            </div>
                                        </div>
                                        <!-- /.widget-main -->
                                    </div>
                                    <!-- /.widget-body -->
                                </div>
                                <!-- /.login-box -->

                                <div id="forgot-box" class="forgot-box widget-box no-border">
                                    <div class="widget-body">
                                        <div class="widget-main">
                                            <h4 class="header blue light-red">
                                                <i class="ace-icon fa fa-key"></i>
                                                Recuperar Contraseña
                                            </h4>

                                            <div class="space-12"></div>
                                            <fieldset>
                                                <div class="row" style="display: none" id="proceso1">
                                                    <p style="text-align: center">
                                                        Ingrese su usuario y nueva contraseña									
                                                    </p>
                                                    <label class="block clearfix">
                                                        <span class="block input-icon input-icon-right">
                                                            <input type="text" class="form-control" placeholder="Nombre de usuario" id="txtUsuario2" disabled />
                                                            <i class="ace-icon fa fa-user"></i>
                                                        </span>
                                                    </label>

                                                    <label class="block clearfix">
                                                        <span class="block input-icon input-icon-right">
                                                            <input type="password" class="form-control" placeholder="Contraseña Nueva" id="txtPassword2" />
                                                            <i class="ace-icon fa fa-lock"></i>
                                                        </span>
                                                    </label>

                                                    <label class="block clearfix">
                                                        <span class="block input-icon input-icon-right">
                                                            <input type="password" class="form-control" placeholder="Contraseña Confirmar" id="txtPassword3" />
                                                            <i class="ace-icon fa fa-lock"></i>
                                                        </span>
                                                    </label>

                                                    <div class="clearfix">
                                                        <button type="button" class="width-65 pull-right btn btn-sm btn-primary" id="btnRecuperar" onclick="RecuperarContrasenia()" style="display: block">
                                                            <span class="bigger-110">Aceptar</span>
                                                            <i class="ace-icon fa fa-arrow-right icon-on-right"></i>
                                                        </button>
                                                    </div>
                                                    <br />
                                                    <ul>
                                                        <li>La duración mínima de la contraseña es 1 día</li>
                                                        <li>No puede introducir contreseñas anteriores hasta 3 veces de anterioridad</li>
                                                        <li>La Contraseña debe incluir caracteres de complejidad, es decir letras mayúsculas, minúsculas, número y caracteres especiales</li>
                                                        <li>No puede incluir nombres, apellidos o datos personales </li>
                                                    </ul>
                                                    <%--                                                    <label class="block clearfix">
                                                        La duración mínima de la contraseña es 1 día
                                                    </label>
                                                    <label class="block clearfix">
                                                        No puede introducir contreseñas anteriores hasta 3 veces de anterioridad
                                                    </label>
                                                    <label class="block clearfix">
                                                        La Contraseña debe cumplir caracteres de complejidad, es decir letras mayúsculas, minúsculas, número y caracteres especiales
                                                    </label>
                                                    <label class="block clearfix">
                                                        No puede incluir nombres, apellidos o datos personales
                                                    </label>--%>
                                                    <div class="modal modal-static fade" id="myModalRecuperar" role="dialog" aria-hidden="true">
                                                        <div class="modal-dialog">
                                                            <div class="modal-content">
                                                                <div class="modal-body">
                                                                    <div class="text-center">
                                                                        <img src="../Estilos/assets/images/avatars/loading.gif" class="icon" />
                                                                        <h5><span class="modal-text">Procesando, Espere por favor... </span></h5>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row" style="display: block" id="proceso2">
                                                    <label class="block clearfix">
                                                        <span class="block input-icon input-icon-right">
                                                            <select id="IdPreguentas2" class="form-control" onchange="#">
                                                                <option value="0">Seleccione una Pregunta</option>
                                                            </select>
                                                        </span>
                                                    </label>
                                                    <label class="block clearfix">
                                                        <span class="block input-icon input-icon-right">
                                                            <input type="password" class="form-control" placeholder="Respuesta" id="Respuesta2" />
                                                            <i class="ace-icon fa fa-lock"></i>
                                                        </span>
                                                    </label>
                                                    <button type="button" class="width-65 pull-right btn btn-sm btn-primary" id="btnConsultar2" onclick="ConsultarRespuesta2()" style="display: block">
                                                        <span class="bigger-110">Verificar</span>
                                                        <i class="ace-icon fa fa-arrow-right icon-on-right"></i>
                                                    </button>
                                                </div>
                                            </fieldset>
                                            <div class="social-or-login center">
                                                <asp:Label ID="Label2" runat="server" Text="Label" Visible="false" CssClass="bigger-110"></asp:Label>
                                            </div>
                                        </div>
                                        <!-- /.widget-main -->

                                        <div class="toolbar center">
                                            <a href="#" data-target="#login-box" class="back-to-login-link">Atrás para iniciar sesión
											
                                            <i class="ace-icon fa fa-arrow-right"></i>
                                            </a>
                                        </div>
                                    </div>
                                    <!-- /.widget-body -->
                                </div>
                                <!-- /.forgot-box -->

                                <div id="signup-box" class="<%--signup--%>-box widget-box no-border">
                                    <div class="widget-body">
                                        <div class="widget-main">
                                            <h4 class="header green lighter bigger">
                                                <i class="ace-icon fa fa-users blue"></i>
                                                Desbloquear usuario
                                            </h4>

                                            <div class="space-6"></div>
                                            <p>Usuario: </p>
                                            <fieldset>
                                                <label class="block clearfix">
                                                    <span class="block input-icon input-icon-right">
                                                        <input id="NomUsuario" type="text" class="form-control" placeholder="Nombre de usuario" disabled />
                                                        <i class="ace-icon fa fa-user"></i>
                                                    </span>
                                                </label>
                                                <label class="block clearfix">
                                                    <span class="block input-icon input-icon-right">
                                                        <select id="IdPreguentas1" class="form-control" onchange="#">
                                                            <option value="0">Seleccione una Pregunta</option>
                                                        </select>
                                                    </span>
                                                </label>
                                                <label class="block clearfix">
                                                    <span class="block input-icon input-icon-right">
                                                        <input type="password" class="form-control" placeholder="Respuesta" id="Respuesta" />
                                                        <i class="ace-icon fa fa-lock"></i>
                                                    </span>
                                                </label>

                                                <%--<label class="block clearfix">
                                                    <span class="block input-icon input-icon-right">
                                                        <input type="password" class="form-control" placeholder="Repetir Contraseña" />
                                                        <i class="ace-icon fa fa-retweet"></i>
                                                    </span>
                                                </label>--%>
                                                <div class="space-24"></div>

                                                <div class="clearfix">
                                                    <%--      <button type="reset" class="width-30 pull-left btn btn-sm">
                                                        <i class="ace-icon fa fa-refresh"></i>
                                                        <span class="bigger-110">Reiniciar</span>
                                                    </button>--%>
                                                    <button type="button" class="width-65 pull-right btn btn-sm btn-primary" id="btnConsultar" onclick="ConsultarRespuesta()" style="display: block">
                                                        <span class="bigger-110">Verificar</span>
                                                        <i class="ace-icon fa fa-arrow-right icon-on-right"></i>
                                                    </button>
                                                    <button type="button" class="width-65 pull-right btn btn-sm btn-primary" id="btnDesbloquear" onclick="desbloquerUsuario()" style="display: none">
                                                        <span class="bigger-110">Desbloquer</span>
                                                        <i class="ace-icon fa fa-arrow-right icon-on-right"></i>
                                                    </button>
                                                    <button type="button" class="width-65 pull-right btn btn-sm btn-danger" id="btnSalir" onclick="Salir()" style="display: none">
                                                        <span class="bigger-110">Salir</span>
                                                        <i class="ace-icon fa fa-arrow-right icon-on-right"></i>
                                                    </button>
                                                </div>
                                            </fieldset>
                                        </div>
                                        <div class="toolbar center">
                                            <a href="#" data-target="#login-box" class="back-to-login-link">
                                                <i class="ace-icon fa fa-arrow-left"></i>
                                                Atrás para iniciar sesión
                                            </a>
                                        </div>
                                    </div>
                                    <!-- /.widget-body -->
                                </div>
                                <!-- /.signup-box -->
                            </form>
                        </div>
                        <!-- /.position-relative -->
                    </div>
                </div>
                <!-- /.col -->
            </div>
            <!-- /.row -->


            <div class="modal modal-static fade" id="processing-modal" role="dialog" aria-hidden="true">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-body">
                            <div class="text-center">
                                <img src="../Imagenes/ImagenesEmpresas/loading.gif" class="icon" />
                                <h5><span class="modal-text">Procesando, Espere por favor... </span></h5>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </div>
        <!-- /.main-content -->
    </div>
    <!-- /.main-container -->

    <!-- basic scripts -->

    <!--[if !IE]> -->

    <script src="../Estilos/assets/js/jquery-2.1.4.min.js"></script>
    <!-- <![endif]-->

    <!--[if IE]>
<script src="assets/js/jquery-1.11.3.min.js"></script>
<![endif]-->
    <script type="text/javascript">
        if ('ontouchstart' in document.documentElement) document.write("<script src='assets/js/jquery.mobile.custom.min.js'>" + "<" + "/script>");
    </script>

    <!-- inline scripts related to this page -->
    <script type="text/javascript">
        //jQuery(function ($) {
        //    $(document).on('click', '.toolbar a[data-target]', function (e) {
        //        e.preventDefault();
        //        var target = $(this).data('target');
        //        $('.widget-box.visible').removeClass('visible');//hide others
        //        $(target).addClass('visible');//show target
        //    });
        //});



        //you don't need this, just used for changing background
        jQuery(function ($) {
            $('#btn-login-dark').on('click', function (e) {
                $('body').attr('class', 'login-layout');
                $('#id-text2').attr('class', 'white');
                $('#id-company-text').attr('class', 'blue');

                e.preventDefault();
            });
            $('#btn-login-light').on('click', function (e) {
                $('body').attr('class', 'login-layout light-login');
                $('#id-text2').attr('class', 'grey');
                $('#id-company-text').attr('class', 'blue');

                e.preventDefault();
            });
            $('#btn-login-blur').on('click', function (e) {
                $('body').attr('class', 'login-layout blur-login');
                $('#id-text2').attr('class', 'white');
                $('#id-company-text').attr('class', 'light-blue');

                e.preventDefault();
            });

        });
    </script>

    <script src="../Estilos/assets/js/jquery.min.js"></script>
    <script src="../Estilos/assets/js/bootstrap.min.js"></script>

</body>
</html>

