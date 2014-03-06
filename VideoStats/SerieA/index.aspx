<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="VideoStats.SerieA.index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
 <title></title>
          <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <!-- Bootstrap -->
    <link href="../css/bootstrap.min.css" rel="stylesheet">
</head>
<body>
    <div class="navbar navbar-inverse navbar-fixed-top" role="navigation">
      <div class="container">
        <div class="navbar-header">
          <button type="button" class="navbar-toggle" data-toggle="collapse" data-target=".navbar-collapse">
            <span class="sr-only">Toggle navigation</span>
            <span class="icon-bar"></span>
            <span class="icon-bar"></span>
            <span class="icon-bar"></span>
          </button>
          <a class="navbar-brand" href="#">BallBall Video Stats</a>
        </div>
        <div class="navbar-collapse collapse">
          <%--<form class="navbar-form navbar-right" role="form">
            <div class="form-group">
              <input type="text" placeholder="Email" class="form-control">
            </div>
            <div class="form-group">
              <input type="password" placeholder="Password" class="form-control">
            </div>
            <button type="submit" class="btn btn-success">Sign in</button>
          </form>--%>
        </div><!--/.navbar-collapse -->
      </div>
    </div>
    <br />
    <form id="form1" runat="server">
    <div class="container">
        <table class="table table-hover">
            <tr>
                <th>#</th>
                <th>Action</th>
            </tr>
            <tr>
                <td>1</td>
                <td>
        <asp:Button ID="Button1" CssClass="btn btn-primary" runat="server" Text="Show Serie A Fixtures" OnClick="Button1_Click" />
    </td>
            </tr>
            </table>
    </div>
        <asp:GridView ID="GridView1" runat="server" CssClass="table table-hover">
        </asp:GridView>
    </form>
</body>
</html>
