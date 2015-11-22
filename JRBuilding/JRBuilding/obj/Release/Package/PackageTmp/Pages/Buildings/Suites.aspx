<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Suites.aspx.cs" Inherits="JRBuilding.Pages.Buildings.Suits" UnobtrusiveValidationMode="None"%>

<!DOCTYPE html>
<html class="bg-black">
    <head>
        <meta charset="UTF-8">
        <title>JRBuildings | Locals</title>
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
       <%-- <div class="row">
                    <div class="col-md-12">
                                    <p>
                                        <asp:Button ID="btnback" class="btn btn-default margin" runat="server" Text="Back" OnClick="btnback_Click"/>
                                    </p>
                     </div>
                </div>--%>
                    <div class="row">
                        <!-- left column -->
                        <div class="col-md-12">
                            <!-- general form elements -->
                            <div class="box box-primary">
                                <div class="box-header">
                                   <asp:Label ID="headertxt" runat="server" class="box-title"></asp:Label>
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
                                            <asp:Label ID="lblname" runat="server" Text="Suit Number"></asp:Label>
                                            <%--<input type="email" class="form-control" id="exampleInputEmail1" placeholder="Enter email">--%>
                                            <asp:TextBox ID="txtname" runat="server" class="form-control" placeholder="Enter Suite Number"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="reqname" runat="server" ErrorMessage="*" ControlToValidate="txtname" ForeColor="Red" ValidationGroup="suites"></asp:RequiredFieldValidator>
                                        </div>
                                        <%--<div class="form-group">
                                            <asp:Label ID="lbllocals" runat="server" Text="Does this building have any locals ?"></asp:Label>
                                            <asp:DropDownList ID="drplocals" runat="server" class="btn btn-default">
                                                <asp:ListItem Selected="True">Yes</asp:ListItem>
                                                <asp:ListItem>No</asp:ListItem>
                                                
                                            </asp:DropDownList>
                                        </div>--%>
                                    </div><!-- /.box-body -->

                                    <div class="box-footer">
                                        <%--<button type="submit" class="btn btn-primary">Submit</button>--%>
                                        <asp:Button ID="btnsubmit" runat="server" Text="Add Suite" class="btn btn-primary" OnClick="btnsubmit_Click" ValidationGroup="suites"/>
                                        <asp:Button ID="btnupdate" runat="server" Text="Update Suite" class="btn btn-primary"  ValidationGroup="suites" OnClick="btnupdate_Click"/>
                                    </div>
                            </div><!-- /.box -->
                            </div>
                        </div>
                        <div class="row">
                                    <div class="col-md-12">
                                        <asp:Button ID="btnedit" runat="server" Text="Edit Suite" class="btn bg-orange margin" ValidationGroup="suites" OnClick="btnedit_Click"/>
                                                        <asp:Button ID="btndelete" runat="server" Text="Delete Suite" class="btn bg-red margin"  ValidationGroup="suites" OnClick="btndelete_Click"/>     
                                     </div>
                                </div>
         <div class="row">
                    <div class="col-md-12">
                        <asp:GridView ID="gridsuites" runat="server" Width="100%" Visible="false">

                        </asp:GridView>        
                     </div>
                </div>
        </section>
    </form>
</body>
</html>
