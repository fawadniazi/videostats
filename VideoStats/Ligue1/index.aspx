<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="VideoStats.Ligue1.index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
 <title></title>
          <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <!-- Bootstrap -->
    <link href="../css/bootstrap.min.css" rel="stylesheet">
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table class="table table-hover">
            <tr>
                <th>#</th>
                <th>Action</th>
            </tr>
            <tr>
                <td>1</td>
                <td>
        <asp:Button ID="Button1" CssClass="btn btn-primary" runat="server" Text="Show Ligue1 Fixtures" OnClick="Button1_Click" />
    </td>
            </tr>
            </table>
    </div>
        <asp:GridView ID="GridView1" runat="server" CssClass="table table-hover">
        </asp:GridView>
    </form>
</body>
</html>
