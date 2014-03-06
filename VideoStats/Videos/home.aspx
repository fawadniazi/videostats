<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="home.aspx.cs" Inherits="VideoStats.Videos.home" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Bundesliga</title>
        <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <!-- Bootstrap -->
    <link href="../css/bootstrap.min.css" rel="stylesheet">
    <style type="text/css">
        .auto-style1 {
            width: 47px;
        }
    </style>
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
                    <asp:Button ID="DisplayButton" CssClass="btn btn-primary" runat="server" Text="Get Videos From Ooyala and Insert Into New Video Table" OnClick="DisplayButton_Click" />
                </td>
            </tr>
            <tr>
                <td>2</td>
                <td>
                    <asp:Button ID="Button1" runat="server" CssClass="btn btn-danger" Text="Split Videos by League and Insert into videosby league table" OnClick="Button1_Click" />
                </td>
            </tr>
            <tr>
                <td>3</td>
                <td>
                    <asp:Button ID="Button2" runat="server" CssClass="btn btn-primary" Text="Generate EPL Table" OnClick="Button2_Click" />
                </td>
            </tr>
            <tr>
                <td>4</td>
                <td>
                    <asp:Button ID="Button3" CssClass="btn btn-danger"  runat="server" Text="Generate NON EPL" OnClick="Button3_Click" />
                </td>
            </tr>
            <tr>
                <td>5</td>
                <td>
                    <asp:Button ID="Button4" CssClass="btn btn-danger"  runat="server" Text="SPR Videos with Date" OnClick="Button4_Click" />
                </td>
            </tr>
            <tr>
                <td>6</td>
                <td>
                    <asp:Button ID="Button5" CssClass="btn btn-danger"  runat="server" Text="GBU Videos with Date" OnClick="Button5_Click" />
                </td>
            </tr>
            <tr>
                <td>7</td>
                <td>
                    <asp:Button ID="Button6" CssClass="btn btn-danger"  runat="server" Text="FL1 Videos with Date" OnClick="Button6_Click" />
                </td>
            </tr>
            <tr>
                <td>8</td>
                <td>
                    <asp:Button ID="Button7" CssClass="btn btn-danger"  runat="server" Text="ISA Videos with Date" OnClick="Button7_Click" />
                </td>
            </tr>
            <tr>
                <td>9</td>
                <td>
                    <asp:Button ID="Button8" CssClass="btn btn-danger"  runat="server" Text="FAC Videos with Date" OnClick="Button8_Click" />
                </td>
            </tr>
            <tr>
                <td>10</td>
                <td>
                    <asp:Button ID="Button9" CssClass="btn btn-danger"  runat="server" Text="EPL Videos with Date" OnClick="Button9_Click" />
                </td>
            </tr>
            </table>


           <table class="table table-hover">
            <tr>
                <th class="auto-style1">#</th>
                <th>Action</th>
            </tr>
            <tr>
                <td class="auto-style1">11</td>
                <td>
                      <asp:Button ID="Button10" CssClass="btn btn-danger"  runat="server" Text="SPR Videos with Date" OnClick="Button10_Click"  /></td>
            </tr>
            <tr>
                <td class="auto-style1">12</td>
                <td>
                     <asp:Button ID="Button11" CssClass="btn btn-danger"  runat="server" Text="FL1 Videos with Date" OnClick="Button11_Click" /></td>
            </tr>
            <tr>
                <td class="auto-style1">13</td>
                <td>
                     <asp:Button ID="Button12" CssClass="btn btn-danger"  runat="server" Text="ISA Videos with Date" OnClick="Button12_Click" /></td>
            </tr>
            <tr>
                <td class="auto-style1">14</td>
                <td>
                      <asp:Button ID="Button13" CssClass="btn btn-danger"  runat="server" Text="EPL Videos with Date" OnClick="Button13_Click"  /></td>
            </tr>
            <tr>
                <td class="auto-style1">15</td>
                <td>
                     <asp:Button ID="Button14" CssClass="btn btn-danger"  runat="server" Text="GBU Videos with Date" OnClick="Button14_Click" /></td>
            </tr>
            </table>
    
    </div>
    </form>
</body>
</html>
