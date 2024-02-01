<%@ Page Title="" Language="C#" MasterPageFile="~/Paginas/Index.Master" AutoEventWireup="true" CodeBehind="DesbloquerUsuario.aspx.cs" Inherits="WebAppPortalAD.Paginas.DesbloquerUsuario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../Estilos/js/Validacion.js?v=2"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page-header">
        <h1>Desbloquer Usuario</h1>
    </div>
    <!-- /.page-header -->
    <div class="col-xs-12">
        <!-- PAGE CONTENT BEGINS -->
        <div class="form-horizontal">
            <div class="form-group">
                <label class="col-sm-3 control-label no-padding-right" for="form-field-1">Usuario</label>
                <div class="col-sm-9">
                    <input runat="server" type="text" id="formfield1" placeholder="Username" class="col-xs-10 col-sm-5" />
                </div>
            </div>
            <div class="form-group">
                <label class="col-sm-3 control-label no-padding-right" for="form-field-1">Estado Usuario</label>
                <div class="col-sm-9">
                    <label>
                        <input id="switchfield1" name="switch-field-1" class="ace ace-switch" type="checkbox" runat="server" />
                        <span class="lbl"></span>
                    </label>
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
</asp:Content>
