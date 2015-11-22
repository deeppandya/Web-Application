<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewLease.aspx.cs" Inherits="JRBuilding.Pages.Lease.ViewLease" %>

<!DOCTYPE html>
<html class="bg-black">
    <head>
        <meta charset="UTF-8">
        <title>JRBuildings | Upload Lease</title>
        <meta content='width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no' name='viewport'>
        <!-- bootstrap 3.0.2 -->
        <link href="../../css/bootstrap.min.css" rel="stylesheet" type="text/css" />
        <!-- font Awesome -->
        <link href="../../css/font-awesome.min.css" rel="stylesheet" type="text/css" />
        <!-- Ionicons -->
        <link href="../../css/ionicons.min.css" rel="stylesheet" type="text/css" />
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
    <section class="content bg-black">
        <div class="row">
                    <div class="col-md-12">
                                    <p>
                                        <asp:Button ID="btnback" class="btn btn-default margin" runat="server" Text="Back" OnClick="btnback_Click"/>
                                    </p>
                     </div>
                </div>
                    <div class="row">
                        <!-- left column -->
                        <div class="col-md-12">
                            <!-- general form elements -->
                            <div class="box box-primary">
                                <div class="box-header">
                                    <h3 id="headertxt" class="box-title">View Lease</h3>
                                </div><!-- /.box-header -->
                                <!-- form start -->
                                
                                    <div class="box-body">
                                        <div class="form-group">
                                            <%--<label >Email address</label>--%>
                                            <asp:Label ID="lblbuilding" runat="server" Text="Building Name"></asp:Label></br>
                                            <asp:DropDownList ID="drpbuilding" runat="server"></asp:DropDownList>
                                            <%--<asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:JRBConnectionString %>" SelectCommand="SELECT * FROM [Buildings]"></asp:SqlDataSource>--%>
                                            <%--<input type="email" class="form-control" id="exampleInputEmail1" placeholder="Enter email">--%>
                                            <%--<asp:TextBox ID="txtid" runat="server" class="form-control" placeholder="Enter Building Id"></asp:TextBox>--%>
                                        </div>
                                        <div class="form-group">
                                            <%--<label >Email address</label>--%>
                                            <asp:Label ID="lbllocal" runat="server" Text="Civic Name"></asp:Label></br>
                                            <asp:DropDownList ID="drplocal" runat="server"></asp:DropDownList>
                                            <%--<asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:JRBConnectionString %>" SelectCommand="SELECT * FROM [Buildings]"></asp:SqlDataSource>--%>
                                            <%--<input type="email" class="form-control" id="exampleInputEmail1" placeholder="Enter email">--%>
                                            <%--<asp:TextBox ID="txtid" runat="server" class="form-control" placeholder="Enter Building Id"></asp:TextBox>--%>
                                        </div>
                                        <div class="form-group">
                                            <%--<label >Email address</label>--%>
                                            <asp:Label ID="lbldocumenttype" runat="server" Text="Document Type"></asp:Label></br>
                                            <asp:DropDownList ID="drpdocumenttype" runat="server" OnSelectedIndexChanged="drpdocumenttype_SelectedIndexChanged" AutoPostBack="true">
                                                <asp:ListItem Text="--Choose One--"  Value="0"></asp:ListItem>
                                                 <asp:ListItem Text="Lease" Value="1"></asp:ListItem>
                                                 <asp:ListItem Text="Rental Cheques" Value="2"></asp:ListItem>
                                            </asp:DropDownList>
                                            <%--<asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:JRBConnectionString %>" SelectCommand="SELECT * FROM [Buildings]"></asp:SqlDataSource>--%>
                                            <%--<input type="email" class="form-control" id="exampleInputEmail1" placeholder="Enter email">--%>
                                            <%--<asp:TextBox ID="txtid" runat="server" class="form-control" placeholder="Enter Building Id"></asp:TextBox>--%>
                                        </div>
                                        <div class="form-group">
                                            <%--<label >Email address</label>--%>
                                            <asp:Label ID="lblsuites" runat="server" Text="Suites" Visible="false"></asp:Label></br>
                                            <asp:DropDownList ID="drpsuites" runat="server" Visible="false" OnSelectedIndexChanged="drpsuites_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                            <%--<asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:JRBConnectionString %>" SelectCommand="SELECT * FROM [Buildings]"></asp:SqlDataSource>--%>
                                            <%--<input type="email" class="form-control" id="exampleInputEmail1" placeholder="Enter email">--%>
                                            <%--<asp:TextBox ID="txtid" runat="server" class="form-control" placeholder="Enter Building Id"></asp:TextBox>--%>
                                        </div>
                                        <div class="form-group">
                                            <%--<label >Email address</label>--%>
                                            <asp:Label ID="lbltenant" runat="server" Text="Tenant Name" Visible="false"></asp:Label></br>
                                            <asp:DropDownList ID="drptenant" runat="server" Visible="false" OnSelectedIndexChanged="drptenant_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                                            <%--<asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:JRBConnectionString %>" SelectCommand="SELECT * FROM [Buildings]"></asp:SqlDataSource>--%>
                                            <%--<input type="email" class="form-control" id="exampleInputEmail1" placeholder="Enter email">--%>
                                            <%--<asp:TextBox ID="txtid" runat="server" class="form-control" placeholder="Enter Building Id"></asp:TextBox>--%>
                                        </div>
                                        <div class="form-group">
                                            <%--<label >Email address</label>--%>
                                            <asp:Label ID="lbldate" runat="server" Text="Select Date" Visible="false"></asp:Label></br>
                                            <asp:DropDownList ID="drpdate" runat="server" Visible="false" OnSelectedIndexChanged="drpdate_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                                            <%--<asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:JRBConnectionString %>" SelectCommand="SELECT * FROM [Buildings]"></asp:SqlDataSource>--%>
                                            <%--<input type="email" class="form-control" id="exampleInputEmail1" placeholder="Enter email">--%>
                                            <%--<asp:TextBox ID="txtid" runat="server" class="form-control" placeholder="Enter Building Id"></asp:TextBox>--%>
                                        </div>
                                    </div><!-- /.box-body -->

                                    <div class="box-footer">
                                        <%--<button type="submit" class="btn btn-primary">Submit</button>--%>
                                        <asp:Button ID="btnsubmit" runat="server" Text="VIew Lease" class="btn btn-primary" OnClick="btnsubmit_Click"/>
                                    </div>
                            </div><!-- /.box -->
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <iframe runat="server" id="iframepdf" height="600" width="100%" visible="false"> </iframe>
                            </div>
                        </div>
        </section>
    </form>
</body>
</html>