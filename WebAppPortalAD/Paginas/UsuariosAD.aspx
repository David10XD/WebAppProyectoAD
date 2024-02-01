<%@ Page Title="" Language="C#" MasterPageFile="~/Paginas/Index.Master" AutoEventWireup="true" CodeBehind="UsuariosAD.aspx.cs" Inherits="WebAppPortalAD.Paginas.UsuariosAD" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Estilos/sweetalert/css/sweetalert.css" rel="stylesheet" />
    <script src="../Estilos/sweetalert/js/sweetalert.min.js"></script>
    <script src="../Estilos/sweetalert/js/sweetalert.init.js"></script>
    <script src="../Estilos/assets/js/jquery.min.js"></script>
    <script src="../Estilos/js/Usuario.js"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="clearfix form-actions">
        <div class="col-md-offset-5 col-md-9">
            <button style="display: block;" type="button" class="btn btn-primary" id="btn_guardar2" onclick="ConsultarUsuarios()" hidden>Consultar</button>
        </div>
    </div>
</asp:Content>
