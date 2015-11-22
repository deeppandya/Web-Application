<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="Home.aspx.cs" Inherits="Android_Security.Home" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Home</title>
    <link href="StyleSheet/layout.css" rel="stylesheet" type="text/css" />
    <link href="StyleSheet/base.css" rel="stylesheet" type="text/css" />
    <link href="StyleSheet/skeleton.css" rel="stylesheet" type="text/css" />
    <link href="StyleSheet/VerticalMenu.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.4.4/jquery.min.js"></script>
    
    <script type ="text/javascript">
    
     $(function() {
            $('a.framesLinks').click(function() {
                var frame1 = document.getElementById('frame1');
                var loadingDisplay = document.getElementById('loading').style;
                loadingDisplay.display = 'block';
                if (frame1.onload == null) {
                    frame1.onload = function() {
                        loadingDisplay.display = 'none'
                    };
                    if (window.attachEvent) {
                        frame1.attachEvent('onload', frame1.onload);
                    }
                }
                return true;
            });

        });
    
    </script>
    

    <script type="text/javascript" src="http://maps.googleapis.com/maps/api/js?key=AIzaSyBpTUI3cyvzgXTDQWZ_2TBR__d78xh_Fg4&sensor=true"> 
    </script>
   
   <style type="text/css">
   .locationpanel
   {
   	margin-bottom: 1%;
   
   }
   
   .imagePanel
   {
   	border:solid 3px #cfcfcf;
   	height:300px;
   	width:300px;
   	margin-left:38%;
   	margin-top:8%;
   	position:absolute;
   	}
   .infoclass
   {
   	background-color:Transparent !important;
   	
   	font-family:"HelveticaNeue", "Helvetica Neue", Helvetica;
   	color:Black;
   	font-weight:bold;
   	
   	}
   	.logoutbutton
   	{
   		float:right;
   		}
       .style1
       {
           width: 70px;
       }
       .style2
       {
           width: 140px;
       }
       .line
       {
       	margin-left:2%;
       	margin-right:2%;
       	margin-top:8%;
        border:1px solid #cfcfcf;
       	}
       	
#nav
{
/*list-style-type:none;*/
position:absolute;
margin:0;
margin-left:30%;
margin-top:10.2%;
padding:2px;
padding-top:6px;
padding-bottom:6px;
-moz-border-radius: 15px;
border-top-left-radius: 10px;
border-bottom-left-radius: 10px;
-webkit-border-radius-left:15px;

}
#nav li
{
list-style:none;
float:left;
font-size:10px;
display :inline ;
}

/*#nav li a 
{
display:block;
padding: 3px 8px;
font-weight:bold;
color:#FFFFFF;
background-color:#98bf21;
text-align:center;
padding:6px;
text-decoration:none;
text-transform:uppercase;
	}*/
#nav li > ul 
{
display:none;
width: 20em;
	}
a:link,a:visited
{
font-weight:bold;
color:#FFFFFF;
background-color: #1763aa;
text-align:center;
padding:9px;
text-decoration:none;
text-transform:uppercase;
}
a:hover,a:active
{
	background-color:#98bf21;
}

#nav li:hover > li
{
	float:none;
	
        /* The fade effect, created using an opacity transition */
			-webkit-transition: opacity .3s ease-in;
			-moz-transition: opacity .3s ease-in;
			-o-transition: opacity .3s ease-in;
			-ms-transition: opacity .3s ease-in;
			
	}
     #nav li:hover > ul
     {
        display:block;
        position:absolute;
        margin-top:0.5%;
        width:100%;
        margin-left:-10%;
        
        /* The fade effect, created using an opacity transition */
			-webkit-transition: opacity .8s ease-in;
			-moz-transition: opacity .8s ease-in;
			-o-transition: opacity .8s ease-in;
			-ms-transition: opacity .8s ease-in;
     	}
     	#nav li:hover >ul li a
     	{
     		font-size:9px;
     		
     		}
     	
       	#btnSignOut
       	{
       		font-weight:bold;
            color:#FFFFFF;
            background-color: #98bf21;
            text-align:center;
            padding-bottom:2px;
            padding-top:2px;
            font-size:9px;
            float:right;
       		}
       		#btnSignOut:hover
       		{
       		    background-color:#7A991A;	
       			}
       			
       	#locationHistoryPanel
       	{
       		margin-top:31%;
            margin-right:1%;
            margin-left:1%;
       		}
   </style>
    
  
</head>
<body>
    <form id="form1" runat="server" autocomplete="off">
         <asp:ScriptManager ID="ScriptManager1" runat="server" />
         <table border="3" style="background-color:#ffffff;float:left; margin-top:2%;margin-left:2%; position:absolute;-moz-border-radius: 15px;border-top-left-radius: 10px;
	    border-bottom-left-radius: 10px;-webkit-border-radius-left:15px; ">
            <tr>
                <td class="style1" >    
                    <asp:Label ID="lblModelNo" runat="server" Text="Model No : " Font-Size="12px" CssClass="infoclass" ></asp:Label>
                </td>
                <td class="style2">
                     <asp:Label ID="txtModelNo" runat="server" Text="" Font-Size="12px" BackColor="Transparent" Font-Bold="true" CssClass="infoclass"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style1">
                    <asp:Label ID="lblIMEINo" runat="server" Text="IMEI : " BackColor="Transparent" Font-Size="12px" CssClass="infoclass"></asp:Label>
                </td>
                <td class="style2">
                    <asp:Label ID="txtIMEI" runat="server" Text="" Font-Size="12px" BackColor="Transparent" Font-Bold="true" CssClass="infoclass"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style1">
                    <asp:Label ID="lblPhoneNumber" runat="server" BackColor="Transparent" Font-Size="12px" Text="Phone No : " CssClass="infoclass"></asp:Label>
                </td>
                <td class="style2">
                    <asp:Label ID="txtPhoneNumber" runat="server" BackColor="Transparent" Font-Size="12px" Text="" Font-Bold="true" CssClass="infoclass"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style1">
                    <asp:Label ID="lblServiceProvider" runat="server" BackColor="Transparent" Font-Size="12px" Text="Provider : " CssClass="infoclass"></asp:Label>
                </td>
                <td class="style2">
                    <asp:Label ID="txtServiceProvider" runat="server" BackColor="Transparent" Font-Size="12px" Text="" Font-Bold="true" CssClass="infoclass"></asp:Label>
                </td>

            </tr>
            </table>
         
         
    <div id="tilebar" runat="server" style=" position:absolute; margin-left:35%; margin-top:0.5%;">   
    <asp:Image ID="imgLogo" runat = "server" ImageUrl="~/images/FootPrintsLOGOFinalEdited2.png" Visible="true" />
    </div>


    <!--<div id="titleDiv" runat="server" class="form-bg" style="width:20%; height:25px ; float:right; margin-top:-1%;">-->
 <table style="background-color:#ffffff;float:right; margin-top:2%;margin-left:80%; position:absolute;-moz-border-radius: 15px;border-top-left-radius: 10px;
	    border-bottom-left-radius: 10px;-webkit-border-radius-left:15px; ">
	                <tr>
                <td >
                    <asp:Label ID="lblUserId" runat="server" Text = "User ID : "  BackColor="Transparent" Font-Size="12px" CssClass="infoclass"></asp:Label>
                </td>
                <td>
                &nbsp;
                </td>
                <td>
                    <asp:Label ID="txtUserId" runat="server" Text = " "  BackColor="Transparent" Font-Size="12px" CssClass="infoclass"></asp:Label>
                </td>
            </tr>
            <tr><td>&nbsp;</td></tr>
            <tr><td>&nbsp;</td></tr>
           
            <tr >
                <td colspan="3" style ="margin-top:5%" >
                    <asp:LinkButton ID = "btnSignOut" runat="server" OnClick="btnSubmit_Click" Width=50%>Sign Out</asp:LinkButton>
                </td>
            </tr>
        </table>
    <!--</div>-->
    <div >
            <ul id="nav">
                <li><asp:LinkButton ID="trackPhone" runat ="server">Mobile Tracking</asp:LinkButton>
                 <ul style="z-index:10;">
                        <li><asp:LinkButton ID = "startTracking" runat="server" OnClick="StartTracking">Start Tracking</asp:LinkButton></li>
                        <li><asp:LinkButton ID = "stopTracking" runat="server" OnClick="StopTracking">Stop Tracking</asp:LinkButton></li>
                        <li><asp:LinkButton ID = "locationHistory" runat="server" OnClick="LocationHistory_Click">Location History</asp:LinkButton></li>
                    </ul>
                </li>
<%--                <li><asp:LinkButton ID="stopTracking" runat ="server" OnClick="StopTracking">Stop Tracking</asp:LinkButton></li>
--%>                
                <li><asp:LinkButton ID = "messagePhone" runat="server" OnClick="SendMessage">Send a Message</asp:LinkButton></li>
                <li><asp:LinkButton ID = "CaptureImage" runat="server" OnClick="SendPhoto">Take Picture</asp:LinkButton></li>
                <li><asp:LinkButton ID = "ChangeProfile" runat="server">Change Phone Profile</asp:LinkButton>
                    <ul style="z-index:10;">
                        <li><asp:LinkButton ID = "ProfileLoud" runat="server" OnClick="Loud">Change Profile To Loud</asp:LinkButton></li>
                        <li><asp:LinkButton ID = "ProfileVibrate" runat="server" OnClick="Vibrate">Change Profile To Vibrate</asp:LinkButton></li>
                        <li><asp:LinkButton ID = "ProfileSilent" runat="server" OnClick="Silent">Change Profile To Silent</asp:LinkButton></li>
                    </ul>
                </li>
                <li><asp:LinkButton ID = "SoundAlarm" runat="server" OnClick="PlayAlarm">Play Alarm</asp:LinkButton></li>
            </ul>
    </div>
    <br />
    <hr class="line" />
    <!--<div id = 'divDeviceInfo' class="form-bg" style="width:20%; height:5% ; float:left; margin-top:1%;background: -webkit-linear-gradient(top, #eeeeee 41%,#cccccc 100%);">-->
        
   <!-- </div>-->
    

   
   <asp:UpdatePanel ID="mapPanel" UpdateMode="Conditional" ChildrenAsTriggers="true" runat="server">
   <Triggers>
   <asp:AsyncPostBackTrigger  controlid="locationGrid" EventName="RowCommand"  />
   </Triggers>
   <ContentTemplate>    
    
                
                <%--Place holder to fill with javascript by server side code--%>
                <asp:Literal ID="js" runat="server"></asp:Literal>
                <%--Place for google to show your MAP--%>
                <div id ="map_canvas"   style="width:98%; position:absolute; height:400px; border:solid 5px #cfcfcf; margin-top:0%; margin-left:1%; margin-right:1%">
                </div>
                <br />
                
   </ContentTemplate> </asp:UpdatePanel> 
            
        <asp:Panel ID = "imagePanel" CssClass="imagePanel" runat="server" >

        
        </asp:Panel>
        
        <asp:Panel ID ="LoadingPanel" CssClass="imagePanel" runat="server" Visible="false">
            <asp:Image ID="loadingImage" runat="server" ImageUrl="~/images/ajax-loader (2).gif" />
        </asp:Panel>
   <asp:Panel runat="server">
   <table>
   <tr>
   <td>
   </td>
   </tr>
   </table>
   
   </asp:Panel>
   <%--<asp:Panel ID = "locationHistoryPanel" runat="server" Visible = "false" CssClass="locationpanel">--%>
    
   <asp:UpdatePanel ID="locationHistoryPanel" UpdateMode="Always" ChildrenAsTriggers="true" runat="server">
     <Triggers>
            <asp:AsyncPostBackTrigger  controlid="Timer2" eventname="Tick" />
       </Triggers>
       
       <ContentTemplate>     
       <asp:Timer runat="server" id="Timer2" interval="120000" ontick="Timer2_Tick" /> 
            <asp:GridView ID="locationGrid" runat="server" AllowPaging="True" 
                            AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" 
                            GridLines="None"
                            Height="100%" Width="100%" OnRowCommand="locationGrid_OnRowCommand"  >
                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                            <Columns>
                            <asp:BoundField DataField="DT" HeaderText="DateTime"
                                                                 SortExpression="Location">
                                <ControlStyle Width="50px" />
                                <FooterStyle BackColor="#1763AA" />
                                <HeaderStyle HorizontalAlign="Left" BackColor="#1763AA" />
                                </asp:BoundField>
                            <asp:BoundField DataField="LATITUDE" HeaderText="Latitude" 
                                                    InsertVisible="False" 
                                                    ReadOnly="True" SortExpression="ID"  >
                                <FooterStyle BackColor="#1763AA" />
                                <HeaderStyle HorizontalAlign="Left" BackColor="#1763AA" />
                                </asp:BoundField>
                            <asp:BoundField DataField="LONGITUDE" HeaderText="Longtitude" 
                                                                     SortExpression="Name" >
                                    
                                    <FooterStyle BackColor="#1763AA" />
                                    
                                    <HeaderStyle HorizontalAlign="Left" BackColor="#1763AA" />
                                </asp:BoundField>
                                    
                                    </Columns>                           
                            
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            
                            
            </asp:GridView>
            </ContentTemplate>
    </asp:UpdatePanel>
        <%--</asp:Panel>  --%>
            
    </form>
</body>
</html>
