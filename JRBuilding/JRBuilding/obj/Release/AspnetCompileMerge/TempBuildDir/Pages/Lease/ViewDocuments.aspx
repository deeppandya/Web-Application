<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewDocuments.aspx.cs" Inherits="JRBuilding.Pages.Lease.ViewDocuments" %>

<!DOCTYPE html>
<html class="bg-black">
    <head>
        <meta charset="UTF-8">
        <title>JRBuildings | View Documents</title>
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
    <div id="divcontent" runat="server">
        <asp:TreeView ID='TreeView1' runat='server' ShowLines='True' OnSelectedNodeChanged="TreeView1_SelectedNodeChanged"></asp:TreeView>
        <%--<asp:TreeView ID='TreeView1' runat='server' ShowLines='True'>
	<Nodes>
		<asp:TreeNode Text='997 Decarie'>
			<asp:TreeNode Text='995 Decarie'>
				<asp:TreeNode Text='103'>
					<asp:TreeNode Text='5_1_201511190122_.pdf'>
					</asp:TreeNode>
					<asp:TreeNode Text='5_1_201512190101_.pdf'> 
					</asp:TreeNode>
					<asp:TreeNode Text='5_1_201541230108_.pdf'> 
					</asp:TreeNode>
					<asp:TreeNode Text='5_1_201544230101_.pdf'> 
					</asp:TreeNode>
					<asp:TreeNode Text='5_1_201549230122_.pdf'>
					</asp:TreeNode>
				</asp:TreeNode>
				<asp:TreeNode Text='104'> 
				</asp:TreeNode>
				<asp:TreeNode Text='105'> 
				</asp:TreeNode>
				<asp:TreeNode Text='106'> 
				</asp:TreeNode>
				<asp:TreeNode Text='107'> 
				</asp:TreeNode> 
			</asp:TreeNode>
			<asp:TreeNode Text='4530 cote des neiges'>
				<asp:TreeNode Text='100'>
					<asp:TreeNode Text='15_2_201527190146_.pdf'> 
					</asp:TreeNode>
					<asp:TreeNode Text='15_2_201545230132_.pdf'> 
					</asp:TreeNode>
					<asp:TreeNode Text='15_2_201546230121_.pdf'> 
					</asp:TreeNode>
					<asp:TreeNode Text='15_2_201547230104_.pdf'> 
					</asp:TreeNode>
					<asp:TreeNode Text='15_2_201547230136_.pdf'> 
					</asp:TreeNode>
					<asp:TreeNode Text='15_2_201548230127_.pdf'> 
					</asp:TreeNode> 
				</asp:TreeNode> 
			</asp:TreeNode>
			<asp:TreeNode Text='3570 ridgewood Avenue'>
				<asp:TreeNode Text='100'>
					<asp:TreeNode Text='16_3_201546190157_.pdf'> 
					</asp:TreeNode> 
				</asp:TreeNode>
				<asp:TreeNode Text='203'> 
				</asp:TreeNode> 
			</asp:TreeNode> 
		</asp:TreeNode>
		<asp:TreeNode Text='720 Decarie'>
			<asp:TreeNode Text='718 Boul DecarieSaint-Laurent QCH4L3L5'>
				<asp:TreeNode Text='130'> 
				</asp:TreeNode>
				<asp:TreeNode Text='140'> 
				</asp:TreeNode> 
			</asp:TreeNode>
			<asp:TreeNode Text='720 Boul DecarieSaint-LaurentH4L3L5'>
				<asp:TreeNode Text='100'> 
				</asp:TreeNode>
				<asp:TreeNode Text='110'> 
				</asp:TreeNode>
				<asp:TreeNode Text='120'> 
				</asp:TreeNode> 
			</asp:TreeNode> 
		</asp:TreeNode>
	</Nodes>
</asp:TreeView>--%>
    </div>

        <!-- jQuery 2.0.2 -->
        <script src="http://ajax.googleapis.com/ajax/libs/jquery/2.0.2/jquery.min.js"></script>
        <!-- Bootstrap -->
        <script src="../../js/bootstrap.min.js" type="text/javascript"></script>        

    </form>
</body>
</html>

