<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Consultas.aspx.cs" Inherits="WebAppPortalAD.Paginas.Consultas"  ValidateRequest="false"  %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager2" runat="server" EnablePageMethods="true">
        </asp:ScriptManager>
        <div>
            <asp:TextBox ID="txtDatos" runat="server" Height="243px" TextMode="MultiLine" Width="600px"></asp:TextBox>
            <asp:Button ID="btnRevisar" runat="server" Text="Revisar" OnClick="btnRevisar_Click" />
            <asp:Label ID="lblresultado" runat="server" Text="Label"></asp:Label>
            <asp:Button ID="btnRevisar0" runat="server" Text="Carga" OnClick="btnRevisar0_Click" />
            
        </div>
    </form>
</body>
</html>
