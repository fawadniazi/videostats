<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="VideoStats.Home" Async="True" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <!-- Bootstrap -->
    <link href="css/bootstrap.min.css" rel="stylesheet">
</head>
<body>
    <form id="form1" runat="server">

        <h1>Videos Data Extracter</h1>

        <table class="table table-hover">
            <tr>
                <th>#</th>
                <th>Action</th>
            </tr>
            <tr>
                <td>1</td>
                <td> <asp:Button ID="Button6" CssClass="btn btn-primary" runat="server" OnClick="Button1_Click" Text="ParseHtml" />
                </td>
            </tr>
            <tr>
                <td>2</td>
                <td> <asp:Button ID="Button7" CssClass="btn btn-primary" runat="server" OnClick="Button2_Click" Text="Read DataBase" />  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;  <asp:Button ID="ReadEPL" CssClass="btn btn-info" runat="server" Text="ReadEPL" OnClick="ReadEPL_Click" />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="Button12" CssClass="btn btn-primary" runat="server" Text="Get Fixture and Videos" OnClick="Button12_Click" />
        </td>
            </tr>
            <tr>
                <td>3</td>
                <td><asp:Button ID="Button8" CssClass="btn btn-danger"  runat="server" OnClick="Button3_Click" Text="InsertDB" />
       </td>
            </tr>
            <tr>
                <td>4</td>
                <td> <asp:Button ID="Button9" CssClass="btn btn-danger" runat="server" OnClick="Button4_Click" Text="InsertEPL" />
        </td>
            </tr>
            <tr>
                <td>5</td>
                <td><asp:Button ID="Button10" CssClass="btn btn-danger" runat="server" OnClick="Button5_Click" Text="InsertNonEPL" />
      </td>
            </tr>
            <tr>
                <td>6</td>
                <td>  <asp:Button ID="Button11" CssClass="btn btn-danger" runat="server" OnClick="InsertError_Click" Text="InsertError" /></td>
            </tr>
            


        </table>

             


    <div>
        <asp:Literal ID="ElapsedTimeLabel" runat="server"></asp:Literal>
        <br />
        <br />
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="ParseHtml" Visible="False" />
&nbsp;&nbsp;&nbsp;
        <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Read DataBase" Visible="False" />
&nbsp;&nbsp;&nbsp;
        <asp:Button ID="Button3" runat="server" OnClick="Button3_Click" Text="InsertDB" Visible="False" />
&nbsp;&nbsp;&nbsp;
        <asp:Button ID="Button4" runat="server" OnClick="Button4_Click" Text="InsertEPL" Visible="False" />
&nbsp;&nbsp;&nbsp;
        <asp:Button ID="Button5" runat="server" OnClick="Button5_Click" Text="InsertNonEPL" Visible="False" />
&nbsp;&nbsp;&nbsp;
        <asp:Button ID="InsertError" runat="server" OnClick="InsertError_Click" Text="InsertError" Visible="False" />
        <br />
        <asp:GridView ID="GridView1" runat="server" CssClass="table table-striped">
        </asp:GridView>
        <br />
    </div>
    </form>

    <!-- jQuery (necessary for Bootstrap's JavaScript plugins) -->
    <script src="https://code.jquery.com/jquery.js"></script>
    <!-- Include all compiled plugins (below), or include individual files as needed -->
    <script src="js/bootstrap.min.js"></script>
</body>
</html>
