<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register1.aspx.cs" Inherits="JRBuilding.Pages.Register" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>JRBuildings | Registration</title>
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
        .auto-style2 {
            width: 216px;
        }
        .auto-style3 {
            width: 686px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        
        <table cellpadding="0" cellspacing="0" class="auto-style1">
            <tr>
                <td class="auto-style2">
                    <asp:Label ID="lblusername" runat="server" Text="Username ( Email Address )"></asp:Label>
                </td>
                <td class="auto-style3">
                    <asp:TextBox ID="txtusername" runat="server" ValidationGroup="Register" placeholder="Enter User Name"></asp:TextBox>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style2">
                    <asp:Label ID="lblpassword" runat="server" Text="Password"></asp:Label>
                </td>
                <td class="auto-style3">
                    <asp:TextBox ID="txtpassword" runat="server" ValidationGroup="Register" placeholder="Enter Password"></asp:TextBox>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style2">
                    <asp:Label ID="lblconfirm" runat="server" Text="Confirm Password" ></asp:Label>
                </td>
                <td class="auto-style3">
                    <asp:TextBox ID="txtconfirm" runat="server" ValidationGroup="Register" placeholder="Confirm Password"></asp:TextBox>
                </td>
                <td>
                    <br />
                </td>
            </tr>
            <tr>
                <td class="auto-style2">
                    <asp:Label ID="lblfname" runat="server" Text="First Name"></asp:Label>
                </td>
                <td class="auto-style3">
                    <asp:TextBox ID="txtfname" runat="server" ValidationGroup="Register" placeholder="Enter First Name"></asp:TextBox>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style2">
                    <asp:Label ID="lbllname" runat="server" Text="Last Name"></asp:Label>
                </td>
                <td class="auto-style3">
                    <asp:TextBox ID="txtlname" runat="server" ValidationGroup="Register" placeholder="Enter Last Name"></asp:TextBox>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style2">
                    <asp:Label ID="lblrole" runat="server" Text="Role"></asp:Label>
                </td>
                <td class="auto-style3">
                    <asp:DropDownList ID="drprole" runat="server" DataSourceID="SqlDataSource1" DataTextField="Desc" DataValueField="RoleId">
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:JRBConnectionString %>" SelectCommand="SELECT [RoleId], [Desc] FROM [Roles]"></asp:SqlDataSource>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
        </table>
        
    </div>
    </form>
</body>
</html>
