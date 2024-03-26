<%@ Page Title="" Language="C#" MasterPageFile="~/Paginas/Index.Master" AutoEventWireup="true" CodeBehind="RegistrarUsuario.aspx.cs" Inherits="WebAppPortalAD.Paginas.RegistrarUsuario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Estilos/sweetalert/css/sweetalert.css" rel="stylesheet" />
    <link href="../Estilos/assets/css/jquery-ui.css" rel="stylesheet" />
    <script src="../Estilos/sweetalert/js/sweetalert.min.js"></script>
    <script src="../Estilos/sweetalert/js/sweetalert.init.js"></script>

    
    <%--<script src="../Estilos/assets/js/jquery.min.js"></script>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <div id="accordion" class="accordion-style1 panel-group">
        <div class="panel panel-default">
            <div>
                <%--<input type="text" class="form-control" id="idmiembroAd" runat="server" Width="0px" Height="0px" BorderWidth="0px" />--%>
                <asp:TextBox ID="Id" runat="server" Text="" CssClass="form-control" Width="0px" Height="0px" BorderWidth="0px"></asp:TextBox>
                <asp:TextBox ID="TextBox1" runat="server" Text="" CssClass="form-control" Width="0px" Height="0px" BorderWidth="0px"></asp:TextBox>
            </div>
            <div class="tabbable" id="Principal" style="display: block">
                 <ul class="nav nav-tabs padding-12 tab-color-blue background-blue" id="myTab3">
                     <li id="tab1" class="active">
                         <a data-toggle="tab" href="#Lista">Lista de Usuarios</a>
                     </li>
                     <li id="tab2" style="display: none">
                         <a data-toggle="tab" href="#Registro">Registro de Usuarios</a>
                     </li>
                     <li id="tab3">
                         <a data-toggle="tab" href="#Actualiza">Actualizar  Usuarios</a>
                     </li>
                     <li id="tab4" style="display: none">
                         <a data-toggle="tab" href="#Unidad">Unidad Organizativa</a>
                     </li>
                 </ul>
                <div class="tab-content">
                    <div id="Lista" class="tab-pane in active">
                        <div class="row" style="display: none" id="combo">
                            <div class="col-sm-12">
                                <span class="block input-icon input-icon-right">
                                    <select id="IdPreguentas1" class="col-xs-5 col-sm-2" onchange="CargarDatos()">
                                        <option value="0">Seleccione el grupo</option>
                                    </select>
                                    <label class="col-sm-1 control-label no-padding-right" for="form-field-1">Usuario</label>
                                    <input type="text" id="formBuscar" placeholder="Usuario" class="col-xs-2 col-sm-3" />
                                    <button style="display: block; " type="button" class="btn btn-primary" id="btnBuscar" onclick="BuscarUsuario()" hidden>Buscar</button>
                                </span>
                            </div>
                        </div>
                        <br />
                        <div class="box box-primary">
                            <div class="box-header"></div>
                            <div class="box-body">
                                <table id="tbl_usuarios" class="table table-bordered table-hover text-center">
                                    <thead>
                                        <tr>
                                            <th>Usuario</th>
                                            <th>Titulo</th>
                                            <th>Email</th>
                                            <th>Nombre Completo</th>
                                            <th>Compañia</th>
                                            <th>Departamento</th>
                                            <th>Telefono</th>
                                            <th>Estados</th>
                                            <th colspan="2" style="text-align:center">Acciones</th>
                                        </tr>
                                    </thead>
                                    <tbody></tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                    <div id="Registro" class="tab-pane">


                        <!-- PAGE CONTENT BEGINS -->
                        <div class="form-horizontal">
                            <br />
                                <div class="form-group">
                                    <label class="col-sm-3 control-label no-padding-right" for="form-field-1">Primer Nombre</label>
                                    <div class="col-sm-9">
                                        <input type="text" id="formPrimerNombre" placeholder="Primer Nombre" class="col-xs-10 col-sm-5" />
                                    </div>
                                </div>

                                <div class="form-group">
                                     <label class="col-sm-3 control-label no-padding-right" for="form-field-1">Segundo Nombre</label>
                                     <div class="col-sm-9">
                                         <input type="text" id="formSegundoNombre" placeholder="Segundo Nombre" class="col-xs-10 col-sm-5" />
                                </div>
                            </div>

                            <div class="form-group">
                                <label class="col-sm-3 control-label no-padding-right" for="form-field-1">Primer Apellido</label>
                                <div class="col-sm-9">
                                    <input type="text" id="formPrimerApellido" placeholder="Primer Apellido" class="col-xs-10 col-sm-5" />
                                </div>
                            </div>

                            <div class="form-group">
                                <label class ="col-sm-3 control-label no-padding-right" for="form-field-1">Segundo Apellido</label>
                                <div class="col-sm-9">
                                    <input type="text" id="formSegundoApellido" placeholder="Segundo Apellido" class="col-xs-10 col-sm-5" />
                                </div>
                            </div>

                            <div class="form-group">
                                <label  style="margin-top:5mm;"   class="col-sm-3 control-label no-padding-right" for="form-field-1">Nombre Usuario</label>
                                <div class="col-sm-9">
                                    <br />
                                    <input style="margin-right: 2mm; " type ="text" id="formSeleccionUsuario" placeholder="Nombre Usuario" class="col-xs-10 col-sm-5" readonly/>
                                        <button  style="height: 32px;"   type ="button" onclick="combinarNombres()" >Generar</button>
                                </div>
                            </div>


                            <div class="form-group">
                                <label class="col-sm-3 control-label no-padding-right" for="form-field-1">Teléfono</label>
                                <div class="col-sm-9">
                                    <input type="text" id="formTelefono" placeholder="Teléfono" class="col-xs-10 col-sm-5" />
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-3 control-label no-padding-right" for="form-field-1">Email</label>
                                <div class="col-sm-9">
                                    <input type="text" id="formEmailDominio" placeholder="Email" class="col-xs-10 col-sm-5" readonly />
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-3 control-label no-padding-right" for="form-field-1">Compañia</label>
                                <div class="col-sm-9">
                                    <input type="text" id="formCompania" placeholder="Compañia" class="col-xs-10 col-sm-5" />
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-3 control-label no-padding-right" for="form-field-1">Departamento</label>
                                <div class="col-sm-9">
                                    <!--<input type="text" id="formDepartamento" placeholder="Departamento" class="col-xs-10 col-sm-5" />-->
                                     <select id="formDepartamento" class="col-xs-5 col-sm-2">
                                        <option value="0">Seleccione el departamento</option>
                                    </select>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-3 control-label no-padding-right" for="form-field-1">Título</label>
                                <div class="col-sm-9">
                                    <input type="text" id="formTitulo" placeholder="Título" class="col-xs-10 col-sm-5" />
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-3 control-label no-padding-right" for="form-field-3">Nueva Contraseña</label>
                                <div class="col-sm-9">
                                    <input type="password" id="formfield3" placeholder="Contraseña" class="col-xs-10 col-sm-5" />
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-3 control-label no-padding-right" for="form-field-4">Confirmar Contraseña</label>
                                <div class="col-sm-9">
                                    <input type="password" id="formfield4" placeholder="Confirmar Contraseña" class="col-xs-10 col-sm-5" />
                                </div>
                            </div>
                            <div class="clearfix form-actions">
                                <div class="col-md-offset-3 col-md-9">
                                    <button style="display: block;" type="button" class="btn btn-primary" id="btn_guardar2" onclick="guardarUsuario()" hidden>Guardar</button>
                                </div>
                            </div>
                            <div class="social-or-login center">
                                <asp:Label ID="Label1" runat="server" Text="Label" Visible="false" CssClass="bigger-110"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <div id="Actualiza" class="tab-pane">
                        <!-- PAGE CONTENT BEGINS -->
                        <div class="form-horizontal">
                            <br />
                            <div class="form-group">
                                <label class="col-sm-3 control-label no-padding-right" for="form-field-1">Nombre Usuario</label>
                                <div class="col-sm-9">
                                    <input type="text" id="formUsuario2" placeholder="Nombre Usuario" disabled class="col-xs-10 col-sm-5" />
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-3 control-label no-padding-right" for="form-field-1">Teléfono</label>
                                <div class="col-sm-9">
                                    <input type="text" id="formTelefono2" placeholder="Teléfono" class="col-xs-10 col-sm-5" />
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-3 control-label no-padding-right" for="form-field-1">Email</label>
                                <div class="col-sm-9">
                                    <input type="text" id="formEmail2" placeholder="Email" class="col-xs-10 col-sm-5" />
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-3 control-label no-padding-right" for="form-field-1">Compañia</label>
                                <div class="col-sm-9">
                                    <input type="text" id="formCompania2" placeholder="Compañia" class="col-xs-10 col-sm-5" />
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-3 control-label no-padding-right" for="form-field-1">Departamento</label>
                                <div class="col-sm-9">
                                    <input type="text" id="formDepartamento2" placeholder="Departamento" class="col-xs-10 col-sm-5" />
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-3 control-label no-padding-right" for="form-field-1">Título</label>
                                <div class="col-sm-9">
                                    <input type="text" id="formTitulo2" placeholder="Título" class="col-xs-10 col-sm-5" />
                                </div>
                            </div>
                            <div class="form-group" id="ActualizarC" style="display: block">
                                <label class="col-sm-3 control-label no-padding-right" for="form-field-1">Resetear Contraseña</label>
                                <div class="col-sm-9">
                                    <%--<button style="display: block;" type="button" class="btn btn-white btn-primary" id="btnResetear" onclick="CambiarEstado()" hidden>Resetear Contraseña</button>--%>
                                    <label>
                                        <input id="switchfield1" name="switch-field-1" class="ace ace-switch" type="checkbox" onclick="CambiarEstado()" />
                                        <span class="lbl"></span>
                                    </label>
                                </div>
                            </div>
                            <div class="form-group" style="display: none" id="idContrasenia1">
                                <label class="col-sm-3 control-label no-padding-right" for="form-field-3">Nueva Contraseña</label>
                                <div class="col-sm-9">
                                    <input type="password" id="Password1" placeholder="Nueva Contraseña" class="col-xs-10 col-sm-5" />
                                </div>
                            </div>
                            <div class="form-group" style="display: none" id="idContrasenia2">
                                <label class="col-sm-3 control-label no-padding-right" for="form-field-4">Confirmar Contraseña</label>
                                <div class="col-sm-9">
                                    <input type="password" id="Password2" placeholder="Confirmar Contraseña" class="col-xs-10 col-sm-5" />
                                </div>
                            </div>
                            <div class="clearfix form-actions">
                                <div class="col-md-offset-3 col-md-9">
                                    <button style="display: block;" type="button" class="btn btn-primary" id="btn_Actualizar" onclick="ActualizaUsuario()" hidden>Actualizar</button>
                                </div>
                            </div>
                            <div class="social-or-login center">
                                <asp:Label ID="Label3" runat="server" Text="Label" Visible="false" CssClass="bigger-110"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <div id="Unidad" class="tab-pane">
                        <div class="box box-primary">
                            <div class="box-header"></div>
                            <div class="box-body">
                                <table id="tbl_unidad" class="table table-bordered table-hover text-center">
                                    <thead>
                                        <tr>
                                            <th>Codigo</th>
                                            <th>Descripcion</th>
                                            <th>Unidad</th>
                                            <th>Estado</th>
                                            <th>Acciones</th>
                                        </tr>
                                    </thead>
                                    <tbody></tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="tabbable" id="error" style="display: none">
                <div class="col-xs-12">
                    <!-- PAGE CONTENT BEGINS -->
                    <div class="alert alert-block alert-success">
                        <button type="button" class="close" data-dismiss="alert">
                            <i class="ace-icon fa fa-times"></i>
                        </button>
                        <i class="ace-icon fa fa-check green"></i>
                        Comunicarse con Password Manager para revisar la configuración del usuario					
                                                <strong class="green">
                                                    <small></small>
                                                </strong>
                    </div>
                    <!-- PAGE CONTENT ENDS -->
                </div>
            </div>
        </div>
    </div>

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


</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
    <script src="../Estilos/js/Usuario.js?v=55"></script>
</asp:Content>



