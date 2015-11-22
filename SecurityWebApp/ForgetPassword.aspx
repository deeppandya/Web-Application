<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ForgetPassword.aspx.cs" Inherits="Android_Security.ForgetPassword" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <link href="StyleSheet/base.css" rel="stylesheet" type="text/css" />
    <link href="StyleSheet/layout.css" rel="stylesheet" type="text/css" />
    <link href="StyleSheet/skeleton.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.4.4/jquery.min.js"></script>
    
    <script type="text/javascript">
    function clearEmail(obj)
            {
                obj.value="";
            }
            
    function setText(obj)
    {
        obj.value = "Please Enter Your Email Id";
    }
    </script>

</head>
<body>
    <form id="form1"  runat="server" autocomplete="off">
    <div>
    <div class="container">
		
		<div class="form-bg" style="height:300px">		   
		        <headerdiv>
				    <header>Forgot Password?</header>   
				     <asp:Panel ID="forgetPanel" runat="server" Visible ="true"> 				     			
				        <p><asp:TextBox ID="txtEmailId" runat="server"  ></asp:TextBox></p>			        
				     </asp:Panel>	
				     <asp:Panel ID="QuestionPanel" runat="server" Visible ="false">
				        <p><asp:TextBox ID="txtQuestion" runat="server" Enabled="false"></asp:TextBox></p> 				
				        <p><asp:TextBox ID="txtAnswer" runat="server"></asp:TextBox></p>	
				     </asp:Panel>	
				     <asp:Panel ID="btnPanel" runat="server" Visible="true">
				        <asp:Button ID="btnSubmit" CssClass="button" runat="server" OnClick="btnSubmit_Click" Visible="true" />	
				     </asp:Panel>
				     <asp:Panel ID="msgPanel" runat="server" Visible="false">
				        <p><asp:Label ID="msgLabel" runat="server" ></asp:Label></p>	
				     </asp:Panel>
                </headerdiv>
		</div>

	</div>
    </div>
    </form>
</body>
</html>
