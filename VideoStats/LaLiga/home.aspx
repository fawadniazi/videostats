<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="home.aspx.cs" Inherits="VideoStats.LaLiga.home" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Bundesliga</title>
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
                    <asp:Button ID="DisplayButton" CssClass="btn btn-primary" runat="server" Text="Display Fixture" OnClick="DisplayButton_Click" />
                </td>
            </tr>
            </table>
    
    </div>
    </form>
</body>
</html>
