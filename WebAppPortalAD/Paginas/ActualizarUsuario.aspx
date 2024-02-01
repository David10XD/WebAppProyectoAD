<%@ Page Title="" Language="C#" MasterPageFile="~/Paginas/Index.Master" AutoEventWireup="true" CodeBehind="ActualizarUsuario.aspx.cs" Inherits="WebAppPortalAD.Paginas.ActualizarUsuario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Estilos/sweetalert/css/sweetalert.css" rel="stylesheet" />
    <script src="../Estilos/sweetalert/js/sweetalert.min.js"></script>
    <script src="../Estilos/sweetalert/js/sweetalert.init.js"></script>
    <script src="../Estilos/assets/js/jquery.min.js"></script>
    <script src="../Estilos/js/Preguntas.js?v=11"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="accordion" class="accordion-style1 panel-group">
        <div class="panel panel-default">
            <div class="tabbable" id="Principal" style="display: block">
                <ul class="nav nav-tabs padding-12 tab-color-blue background-blue" id="myTab3">
                    <li id="tab1">
                        <a data-toggle="tab" href="#Lista">Actualizar o Cambiar Contraseña</a>
                    </li>
                    <li id="tab2" class="active">
                        <a data-toggle="tab" href="#Registro">preguntas de seguridad</a>
                    </li>
                </ul>
                <div class="tab-content">
                    <div id="Lista" class="tab-pane">
                        <div class="form-horizontal">
                            <br />
                            <div class="form-group" style="display: none">
                                <label class="col-sm-3 control-label no-padding-right" for="form-field-1">Contraseña se caducara</label>
                                <div class="col-sm-9">
                                    <input runat="server" type="text" id="formfieldFecha" placeholder="Username" disabled class="col-xs-10 col-sm-5" />
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-3 control-label no-padding-right" for="form-field-1">Usuario</label>
                                <div class="col-sm-9">
                                    <input runat="server" type="text" id="formfield1" placeholder="Username" class="col-xs-10 col-sm-5" />
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-3 control-label no-padding-right" for="form-field-3">Nueva Contraseña</label>
                                <div class="col-sm-9">
                                    <input runat="server" type="password" id="formfield3" placeholder="Nueva Contraseña" class="col-xs-10 col-sm-5" />
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-3 control-label no-padding-right" for="form-field-4">Confirmar Contraseña</label>
                                <div class="col-sm-9">
                                    <input runat="server" type="password" id="formfield4" placeholder="Confirmar Contraseña" class="col-xs-10 col-sm-5" />
                                </div>
                            </div>
                            <div class="clearfix form-actions">
                                <div class="col-md-offset-3 col-md-9">
                                    <asp:Button ID="btn_guardar" runat="server" Text="Guardar" CssClass="btn btn-info" OnClick="btn_guardar_Click"></asp:Button>
                                    &nbsp; &nbsp; &nbsp;
                                    <asp:Button ID="btn_cancelar" runat="server" Text="Cancelar" CssClass="btn" OnClick="btn_cancelar_Click"></asp:Button>
                                </div>
                            </div>
                            <div class="social-or-login center">
                                <asp:Label ID="Label1" runat="server" Text="Label" Visible="false" CssClass="bigger-110"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <div id="Registro" class="tab-pane in active">
                        <div class="form-horizontal" style="display: block;" id="Preguntas">
                            <div style="display: none;">
                                <div class="form-group">
                                    <button type="button" class="btn btn-primary" id="btnDetalle2" onclick="CargarPreguntas()" hidden>Carga Preguntas</button>
                                </div>
                            </div>
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <label class="col-sm-2 control-label no-padding-right" for="form-field-1">Preguntas de seguridad </label>
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-2 control-label no-padding-right" for="form-field-1">Pregunta :</label>
                                    <div class="col-sm-9">
                                        <select id="IdPreguentas1" class="form-control">
                                            <option value="0">Seleccione una Pregunta</option>
                                        </select>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-2 control-label no-padding-right" for="form-field-1"></label>
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="TextBox1" runat="server" Text="" CssClass="form-control" Width="0px" Height="0px" BorderWidth="0px"></asp:TextBox>
                                        <input type="password" id="txtRespuesta1" placeholder="Respuesta" class="col-xs-10 col-sm-15" />
                                    </div>
                                    <div class="col-sm-4">
                                        <input type="password" id="txtRespuestaConfirmacion1" placeholder="Confirmar respuesta" class="col-xs-10 col-sm-15" />
                                    </div>
                                </div>
                                <br />
                                <div class="form-group">
                                    <label class="col-sm-2 control-label no-padding-right" for="form-field-1">Pregunta :</label>
                                    <div class="col-sm-9">
                                        <select id="IdPreguentas2" class="form-control">
                                            <option value="0">Seleccione una Pregunta</option>
                                        </select>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-2 control-label no-padding-right" for="form-field-1"></label>
                                    <div class="col-sm-4">

                                        <asp:TextBox ID="TextBox2" runat="server" Text="" CssClass="form-control" Width="0px" Height="0px" BorderWidth="0px"></asp:TextBox>
                                        <input type="password" id="txtRespuesta2" placeholder="Respuesta" class="col-xs-10 col-sm-15" />
                                    </div>
                                    <div class="col-sm-4">
                                        <input type="password" id="txtRespuestaConfirmacion2" placeholder="Confirmar respuesta" class="col-xs-10 col-sm-15" />
                                    </div>
                                </div>

                                <div class="clearfix form-actions">
                                    <div class="col-md-offset-5 col-md-9">
                                        <button style="display: block;" type="button" class="btn btn-primary" id="btn_guardar2" onclick="guardarPregunta()" hidden>Guardar</button>
                                        <button style="display: none;" type="button" class="btn btn-primary" id="btn_modificar" onclick="modificarPregunta()" hidden>Modificar</button>
                                    </div>
                                </div>
                                <div class="social-or-login center">
                                    <asp:Label ID="Label4" runat="server" Text="Label" Visible="false" CssClass="bigger-110"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="form-horizontal" style="display: none;" id="Respuestas">
                            <div class="form-group">
                                <label class="col-sm-5 control-label no-padding-right" for="form-field-1">Preguntas y Respuestas de seguridad </label>
                                <button type="button" class="btn btn-primary" id="btnDetalle" onclick="VerPregunta()" hidden>Ver mis preguntas de seguridad</button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>


    <%--    <div class="widget-box">
        <div class="widget-header">
            <h4 class="smaller">Actualizar o Cambiar Contraseña</h4>
        </div>
        <div class="widget-body">
            <div class="widget-main">
                <div class="col-xs-12">
                    <div class="form-horizontal">
                        <br />
                        <div class="form-group">
                            <label class="col-sm-3 control-label no-padding-right" for="form-field-1">Contraseña se caducara</label>
                            <div class="col-sm-9">
                                <input runat="server" type="text" id="formfieldFecha" placeholder="Username" disabled class="col-xs-10 col-sm-5" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-sm-3 control-label no-padding-right" for="form-field-1">Usuario</label>
                            <div class="col-sm-9">
                                <input runat="server" type="text" id="formfield1" placeholder="Username" class="col-xs-10 col-sm-5" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-sm-3 control-label no-padding-right" for="form-field-3">Nueva Contraseña</label>
                            <div class="col-sm-9">
                                <input runat="server" type="password" id="formfield3" placeholder="Nueva Contraseña" class="col-xs-10 col-sm-5" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-sm-3 control-label no-padding-right" for="form-field-4">Confirmar Contraseña</label>
                            <div class="col-sm-9">
                                <input runat="server" type="password" id="formfield4" placeholder="Confirmar Contraseña" class="col-xs-10 col-sm-5" />
                            </div>
                        </div>
                        <div class="clearfix form-actions">
                            <div class="col-md-offset-3 col-md-9">
                                <asp:Button ID="btn_guardar" runat="server" Text="Guardar" CssClass="btn btn-info" OnClick="btn_guardar_Click"></asp:Button>
                                &nbsp; &nbsp; &nbsp;
                                    <asp:Button ID="btn_cancelar" runat="server" Text="Cancelar" CssClass="btn" OnClick="btn_cancelar_Click"></asp:Button>
                            </div>
                        </div>
                        <div class="social-or-login center">
                            <asp:Label ID="Label1" runat="server" Text="Label" Visible="false" CssClass="bigger-110"></asp:Label>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="col-sm-12">
        <div class="widget-box">
            <div class="widget-header">
                <h4 class="smaller">Métodos de verificación</h4>
            </div>
            <div class="widget-body">
                <div class="widget-main">
                    <div class="col-xs-12" style="display: block;" id="Preguntas">
                        <div class="form-horizontal">
                            <div class="form-group">
                                <button type="button" class="btn btn-primary" id="btnDetalle2" onclick="CargarPreguntas()" hidden>Carga Preguntas</button>
                            </div>
                        </div>
                        <div class="form-horizontal">
                            <div class="form-group">
                                <label class="col-sm-2 control-label no-padding-right" for="form-field-1">Preguntas de seguridad </label>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-2 control-label no-padding-right" for="form-field-1">Pregunta :</label>
                                <div class="col-sm-9">
                                    <select id="IdPreguentas1" class="form-control" onchange="#">
                                        <option value="0">Seleccione una Pregunta</option>
                                    </select>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-2 control-label no-padding-right" for="form-field-1"></label>
                                <div class="col-sm-4">
                                    <asp:TextBox ID="TextBox1" runat="server" Text="" CssClass="form-control" Width="0px" Height="0px" BorderWidth="0px"></asp:TextBox>
                                    <input type="password" id="txtRespuesta1" placeholder="Respuesta" class="col-xs-10 col-sm-15" />
                                </div>
                                <div class="col-sm-4">
                                    <input type="password" id="txtRespuestaConfirmacion1" placeholder="Confirmar respuesta" class="col-xs-10 col-sm-15" />
                                </div>
                            </div>
                            <br />
                            <div class="form-group">
                                <label class="col-sm-2 control-label no-padding-right" for="form-field-1">Pregunta :</label>
                                <div class="col-sm-9">
                                    <select id="IdPreguentas2" class="form-control" onchange="#">
                                        <option value="0">Seleccione una Pregunta</option>
                                    </select>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-2 control-label no-padding-right" for="form-field-1"></label>
                                <div class="col-sm-4">
                                    <asp:TextBox ID="TextBox2" runat="server" Text="" CssClass="form-control" Width="0px" Height="0px" BorderWidth="0px"></asp:TextBox>
                                    <input type="password" id="txtRespuesta2" placeholder="Respuesta" class="col-xs-10 col-sm-15" />
                                </div>
                                <div class="col-sm-4">
                                    <input type="password" id="txtRespuestaConfirmacion2" placeholder="Confirmar respuesta" class="col-xs-10 col-sm-15" />
                                </div>
                            </div>

                            <div class="clearfix form-actions">
                                <div class="col-md-offset-5 col-md-9">
                                    <button style="display: block;" type="button" class="btn btn-primary" id="btn_guardar2" onclick="guardarPregunta()" hidden>Guardar</button>
                                    <button style="display: none;" type="button" class="btn btn-primary" id="btn_modificar" onclick="modificarPregunta()" hidden>Modificar</button>
                                </div>
                            </div>
                            <div class="social-or-login center">
                                <asp:Label ID="Label2" runat="server" Text="Label" Visible="false" CssClass="bigger-110"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <div class="col-xs-12" style="display: none;" id="Respuestas">
                        <div class="form-group">
                            <label class="col-sm-2 control-label no-padding-right" for="form-field-1">Preguntas y Respuestas de seguridad </label>
                            <button type="button" class="btn btn-primary" id="btnDetalle" onclick="VerPregunta()" hidden>Ver mis preguntas de seguridad</button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>--%>


    <%--<div id="accordion" class="accordion-style1 panel-group">
        <div class="panel panel-default">
            <div class="panel-heading">
                <h4 class="panel-title">
                    <a class="accordion-toggle" data-toggle="collapse" data-parent="#accordion" href="#collapseOne">
                        <i class="ace-icon fa fa-angle-down bigger-110" data-icon-hide="ace-icon fa fa-angle-down" data-icon-show="ace-icon fa fa-angle-right"></i>
                        <asp:Label ID="Label2" runat="server" Text="Actualizar o Cambiar Contraseña" CssClass="bigger-110"></asp:Label>
                    </a>
                </h4>
            </div>

            <div class="panel-collapse collapse" id="collapseOne">
                <div class="panel-body">
                    <div class="col-xs-12">
                        <div class="form-horizontal">
                            <br />
                            <div class="form-group">
                                <label class="col-sm-3 control-label no-padding-right" for="form-field-1">Contraseña se caducara</label>
                                <div class="col-sm-9">
                                    <input runat="server" type="text" id="formfieldFecha" placeholder="Username" disabled class="col-xs-10 col-sm-5" />
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-3 control-label no-padding-right" for="form-field-1">Usuario</label>
                                <div class="col-sm-9">
                                    <input runat="server" type="text" id="formfield1" placeholder="Username" class="col-xs-10 col-sm-5" />
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-3 control-label no-padding-right" for="form-field-3">Nueva Contraseña</label>
                                <div class="col-sm-9">
                                    <input runat="server" type="password" id="formfield3" placeholder="Nueva Contraseña" class="col-xs-10 col-sm-5" />
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-3 control-label no-padding-right" for="form-field-4">Confirmar Contraseña</label>
                                <div class="col-sm-9">
                                    <input runat="server" type="password" id="formfield4" placeholder="Confirmar Contraseña" class="col-xs-10 col-sm-5" />
                                </div>
                            </div>
                            <div class="clearfix form-actions">
                                <div class="col-md-offset-3 col-md-9">
                                    <asp:Button ID="btn_guardar" runat="server" Text="Guardar" CssClass="btn btn-info" OnClick="btn_guardar_Click"></asp:Button>
                                    &nbsp; &nbsp; &nbsp;
                                    <asp:Button ID="btn_cancelar" runat="server" Text="Cancelar" CssClass="btn" OnClick="btn_cancelar_Click"></asp:Button>
                                </div>
                            </div>
                            <div class="social-or-login center">
                                <asp:Label ID="Label1" runat="server" Text="Label" Visible="false" CssClass="bigger-110"></asp:Label>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="panel panel-default">
            <div class="panel-heading">
                <h4 class="panel-title">
                    <a class="accordion-toggle collapsed" data-toggle="collapse" data-parent="#accordion" href="#collapseTwo">
                        <i class="ace-icon fa fa-angle-right bigger-110" data-icon-hide="ace-icon fa fa-angle-down" data-icon-show="ace-icon fa fa-angle-right"></i>
                        &nbsp;Métodos de verificación 
                    </a>
                </h4>
            </div>

            <div class="panel-collapse collapse in" id="collapseTwo">
                <div class="panel-body">
                    <div class="col-xs-12" style="display: block;" id="Preguntas">
                        <div class="form-horizontal">
                            <div class="form-group">
                                <button type="button" class="btn btn-primary" id="btnDetalle2" onclick="CargarPreguntas()" hidden>Carga Preguntas</button>
                            </div>
                        </div>
                        <div class="form-horizontal">
                            <div class="form-group">
                                <label class="col-sm-2 control-label no-padding-right" for="form-field-1">Preguntas de seguridad </label>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-2 control-label no-padding-right" for="form-field-1">Pregunta :</label>
                                <div class="col-sm-9">
                                    <select id="IdPreguentas1" class="form-control" onchange="#">
                                        <option value="0">Seleccione una Pregunta</option>
                                    </select>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-2 control-label no-padding-right" for="form-field-1"></label>
                                <div class="col-sm-4">
                                    <asp:TextBox ID="txtId1" runat="server" Text="" CssClass="form-control" Width="0px" Height="0px" BorderWidth="0px"></asp:TextBox>
                                    <input type="password" id="txtRespuesta1" placeholder="Respuesta" class="col-xs-10 col-sm-15" />
                                </div>
                                <div class="col-sm-4">
                                    <input type="password" id="txtRespuestaConfirmacion1" placeholder="Confirmar respuesta" class="col-xs-10 col-sm-15" />
                                </div>
                            </div>
                            <br />
                            <div class="form-group">
                                <label class="col-sm-2 control-label no-padding-right" for="form-field-1">Pregunta :</label>
                                <div class="col-sm-9">
                                    <select id="IdPreguentas2" class="form-control" onchange="#">
                                        <option value="0">Seleccione una Pregunta</option>
                                    </select>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-2 control-label no-padding-right" for="form-field-1"></label>
                                <div class="col-sm-4">

                                    <asp:TextBox ID="txtId2" runat="server" Text="" CssClass="form-control" Width="0px" Height="0px" BorderWidth="0px"></asp:TextBox>
                                    <input type="password" id="txtRespuesta2" placeholder="Respuesta" class="col-xs-10 col-sm-15" />
                                </div>
                                <div class="col-sm-4">
                                    <input type="password" id="txtRespuestaConfirmacion2" placeholder="Confirmar respuesta" class="col-xs-10 col-sm-15" />
                                </div>
                            </div>

                            <div class="clearfix form-actions">
                                <div class="col-md-offset-5 col-md-9">
                                    <button style="display: block;" type="button" class="btn btn-primary" id="btn_guardar2" onclick="guardarPregunta()" hidden>Guardar</button>
                                    <button style="display: none;" type="button" class="btn btn-primary" id="btn_modificar" onclick="modificarPregunta()" hidden>Modificar</button>
                                </div>
                            </div>
                            <div class="social-or-login center">
                                <asp:Label ID="Label3" runat="server" Text="Label" Visible="false" CssClass="bigger-110"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <div class="col-xs-12" style="display: none;" id="Respuestas">
                        <div class="form-group">
                            <label class="col-sm-2 control-label no-padding-right" for="form-field-1">Preguntas y Respuestas de seguridad </label>
                            <button type="button" class="btn btn-primary" id="btnDetalle" onclick="VerPregunta()" hidden>Ver mis preguntas de seguridad</button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>--%>
</asp:Content>
