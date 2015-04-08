<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="JRBuilding.Pages.User.Register" UnobtrusiveValidationMode="None"%>

<!DOCTYPE html>
<html class="bg-black">
    <head>
        <meta charset="UTF-8">
        <title>JRBuildings | Registration Page</title>
        <meta content='width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no' name='viewport'>
        <!-- bootstrap 3.0.2 -->
        <link href="../../css/bootstrap.min.css" rel="stylesheet" type="text/css" />
        <!-- font Awesome -->
        <link href="../../css/font-awesome.min.css" rel="stylesheet" type="text/css" />
        <!-- Theme style -->
        <link href="../../css/AdminLTE.css" rel="stylesheet" type="text/css" />

        <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
        <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
        <!--[if lt IE 9]>
          <script src="https://oss.maxcdn.com/libs/html5shiv/3.7.0/html5shiv.js"></script>
          <script src="https://oss.maxcdn.com/libs/respond.js/1.3.0/respond.min.js"></script>
        <![endif]-->
    </head>
<body class="bg-black">
    <form id="form1" runat="server">

        <div class="form-box" id="login-box">
            <div class="header bg-blue">Register User</div>
                <div class="body bg-gray">
                    <div class="form-group">
                        <%--<input type="text" name="name" class="form-control" placeholder="Full name"/>--%>
                        <asp:TextBox ID="txtfname" class="form-control" placeholder="First Name" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="reqfname" runat="server" ErrorMessage="*" ControlToValidate="txtfname" ValidationGroup="register" ForeColor="Red"></asp:RequiredFieldValidator>
                    </div>
                    <div class="form-group">
                        <%--<input type="text" name="userid" class="form-control" placeholder="User ID"/>--%>
                        <asp:TextBox ID="txtlname" class="form-control" placeholder="Last Name" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="reqlname" runat="server" ErrorMessage="*" ControlToValidate="txtlname" ValidationGroup="register" ForeColor="Red"></asp:RequiredFieldValidator>
                    </div>
                    <div class="form-group">
                        <%--<input type="password" name="password" class="form-control" placeholder="Password"/>--%>
                        <asp:TextBox ID="txtusername" class="form-control" placeholder="Email Id" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="requsername" runat="server" ErrorMessage="*" ControlToValidate="txtusername" ValidationGroup="register" ForeColor="Red"></asp:RequiredFieldValidator>
                    </div>
                    <div class="form-group">
                        <%--<input type="password" name="password2" class="form-control" placeholder="Retype password"/>--%>
                        <asp:TextBox ID="txtpassword" class="form-control" placeholder="Password" runat="server" TextMode="Password"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="reqpassword" runat="server" ErrorMessage="*" ControlToValidate="txtpassword" ValidationGroup="register" ForeColor="Red"></asp:RequiredFieldValidator>
                    </div>
                    <div class="form-group">
                        <%--<input type="password" name="password2" class="form-control" placeholder="Retype password"/>--%>
                        <asp:TextBox ID="txtconfirmpassword" class="form-control" placeholder="Confirm Password" runat="server" TextMode="Password"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="reqconfirmpassword" runat="server" ErrorMessage="*" ControlToValidate="txtconfirmpassword" ValidationGroup="register" ForeColor="Red"></asp:RequiredFieldValidator>
                        <%--<asp:CompareValidator ID="cmpconfirmpassword" runat="server" ErrorMessage="Confirm password failed" ValidationGroup="register" ValueToCompare="txtpassword" ControlToValidate="txtconfirmpassword" ForeColor="Red"></asp:CompareValidator>--%>
                    </div>
                    <div class="form-group">
                        <%--<input type="password" name="password2" class="form-control" placeholder="Retype password"/>--%>
                        <asp:DropDownList ID="drprole" runat="server" class="btn btn-default" DataSourceID="SqlDataSource1" DataTextField="Desc" DataValueField="RoleId"></asp:DropDownList>
                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:JRBConnectionString %>" SelectCommand="SELECT [RoleId], [Desc] FROM [Roles]"></asp:SqlDataSource>
                    </div>
                   
                </div>
                <div class="footer">                    

                    <%--<button type="submit" class="btn bg-olive btn-block">Sign me up</button>--%>
                    <asp:Button ID="btnsubmit" class="btn bg-blue btn-block" runat="server" Text="Register" OnClick="btn_submit_click" ValidationGroup="register"/>
<%--                    <a href="login.html" class="text-center">I already have a membership</a>--%>
                </div>

            <%--<div class="margin text-center">
                <span>Register using social networks</span>
                <br/>
                <button class="btn bg-light-blue btn-circle"><i class="fa fa-facebook"></i></button>
                <button class="btn bg-aqua btn-circle"><i class="fa fa-twitter"></i></button>
                <button class="btn bg-red btn-circle"><i class="fa fa-google-plus"></i></button>

            </div>--%>
        </div>


        <!-- jQuery 2.0.2 -->
        <script src="http://ajax.googleapis.com/ajax/libs/jquery/2.0.2/jquery.min.js"></script>
        <!-- Bootstrap -->
        <script src="../../js/bootstrap.min.js" type="text/javascript"></script>

    </form>
</body>
</html>
