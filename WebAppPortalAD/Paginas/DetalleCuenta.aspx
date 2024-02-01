<%@ Page Title="" Language="C#" MasterPageFile="~/Paginas/Index.Master" AutoEventWireup="true" CodeBehind="DetalleCuenta.aspx.cs" Inherits="WebAppPortalAD.Paginas.DetalleCuenta" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="accordion" class="accordion-style1 panel-group">
          <div class="panel panel-default">
            <div class="panel-heading">
                <h4 class="panel-title">
                    <a class="accordion-toggle" data-toggle="collapse" data-parent="#accordion" href="#collapseOne">
                        <i class="ace-icon fa fa-angle-down bigger-110" data-icon-hide="ace-icon fa fa-angle-down" data-icon-show="ace-icon fa fa-angle-right"></i>
                        <asp:Label ID="Label1" runat="server" Text="Label"  CssClass="bigger-110"></asp:Label>
                    </a>
                </h4>
            </div>
            <div class="widget-box" id="collapseOne">
                <div class="widget-body">
                    <asp:Label ID="Label2" runat="server" Text="Label"  CssClass="bigger-110"></asp:Label>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
