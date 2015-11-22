<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Android_Security._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
	<meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">	<!-- Force Latest IE rendering engine -->
	<title>Login Form</title>
    
    <link href="StyleSheet/base.css" rel="stylesheet" type="text/css" />
    <link href="StyleSheet/layout.css" rel="stylesheet" type="text/css" />
    <link href="StyleSheet/skeleton.css" rel="stylesheet" type="text/css" />
        <script type="text/javascript">
            function clearEmail(obj)
                    {
                        obj.value="";
                    }
                    
        </script>
    
    <style type="text/css">
    .forgotPassword, forgotPassword:hover
    {
    	margin-left:7%;
    	cursor:pointer;
    	}
    	
    	       	#btnSignOut
       	{
       		font-weight:bold;
            color:#FFFFFF;
            background-color: #98bf21;
            text-align:center;
            padding-bottom:5px;
            padding-top:5px;
            font-size:9px;
            float:right;
            margin-right:8%;
            text-decoration:none;
            text-transform:uppercase;
            width:20%;
            height:20px;
       		}
       		#btnSignOut:hover
       		{
       		    background-color:#7A991A;	
       		    cursor:pointer;
       			}
    
    </style>
</head>
<body>
    <form id="form1" runat="server" autocomplete="off">
    <div>
   <%-- <div class="notice">
		<a href="" class="close">close</a>
		<p class="warn">Whoops! We didn't recognise your username or password. Please try again.</p>
	</div>--%>

    <div id="tilebar" runat="server" style=" position:absolute; margin-left:35%; margin-top:-10%;">   
    <asp:Image ID="imgLogo" runat = "server" ImageUrl="~/images/FootPrintsLOGOFinalEdited2.png" Visible="true" />
    </div>


	<!-- Primary Page Layout -->

	<div class="container">
		
		<div class="form-bg">
		    <headerdiv>
				<header>Login</header>
				<%--<p><input type="text" placeholder="Username"></p>--%>
				<p><asp:TextBox ID="txtUserName" runat="server" ></asp:TextBox></p>
				<p><asp:TextBox ID="txtPassWord" runat="server" TextMode="Password" ></asp:TextBox></p>
				<%--<p><input type="password" placeholder="Password"></p>--%>
				<%--<asp:Button ID="btnSubmit" CssClass="button" runat="server" 
                    onclick="btnSubmit_Click" />--%>
                    <asp:LinkButton ID = "btnSignOut" runat="server" OnClick="btnSubmit_Click">Sign In</asp:LinkButton>
                    <p><asp:HyperLink ID="hypForgotPassword" runat="server" NavigateUrl="~/ForgetPassword.aspx" CssClass="forgotPassword" Font-Bold="true" Text="Forgot Password?" Font-Size="11px" Font-Underline="false"></asp:HyperLink></p>
            </headerdiv>
		</div>
	</div>
    </div>
    </form>
</body>
</html>
