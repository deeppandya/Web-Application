<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="JRBuilding.Pages.User.Login" UnobtrusiveValidationMode="None"%>

<!DOCTYPE html>
<html class="bg-black">
    <head>
        <meta charset="UTF-8">
        <title>JRBuildings | Log in</title>
        <meta content='width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no' name='viewport'>
        <!-- bootstrap 3.0.2 -->
        <link href="../../css/bootstrap.min.css" rel="stylesheet" type="text/css" />
        <!-- font Awesome -->
        <link href="../../css/font-awesome.min.css" rel="stylesheet" type="text/css" />
        <!-- Theme style -->
        <link href="../../css/AdminLTE.css" rel="stylesheet" type="text/css" />

        <link rel="shortcut icon" href="../../img/home.png" >

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
            <div class="header bg-blue">Sign In</div>
                <div class="body bg-gray">
                    <div class="form-group">
                        <%--<input type="text" name="userid" class="form-control" placeholder="User ID"/>--%>
                        <asp:TextBox ID="txtusername" runat="server" class="form-control" placeholder="Email ID"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="requsername" runat="server" ErrorMessage="*" ControlToValidate="txtusername" ForeColor="Red" ValidationGroup="login"></asp:RequiredFieldValidator>
                    </div>
                    <div class="form-group">
                        <%--<input type="password" name="password" class="form-control" placeholder="Password"/>--%>
                        <asp:TextBox ID="txtpassword" runat="server" class="form-control" placeholder="Password" TextMode="Password"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="reqpassword" runat="server" ErrorMessage="*" ControlToValidate="txtpassword" ForeColor="Red" ValidationGroup="login"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="footer">                                                               
                    <%--<button type="submit" class="btn bg-blue btn-block">Sign in</button>--%>  
                    <asp:Button ID="btnlogin" runat="server" class="btn bg-blue btn-block" Text="Sign in" OnClick="btnlogin_Click" ValidationGroup="login"/>
                    <%--<p><a href="#">I forgot my password</a></p>--%>
                    
                    <%--<a href="Register.aspx" class="text-center">Register User</a>--%>
                </div>
        </div>


        <!-- jQuery 2.0.2 -->
        <script src="http://ajax.googleapis.com/ajax/libs/jquery/2.0.2/jquery.min.js"></script>
        <!-- Bootstrap -->
        <script src="../../js/bootstrap.min.js" type="text/javascript"></script>        

    </form>
</body>
</html>
