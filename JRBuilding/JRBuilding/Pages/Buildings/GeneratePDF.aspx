<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GeneratePDF.aspx.cs" Inherits="JRBuilding.Pages.Buildings.GeneratePDF" %>

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
        <div class="row">
                    <div class="col-md-12">
                                    <p>
                                        <asp:Button ID="btnback" class="btn btn-default margin" runat="server" Text="Back" OnClick="btnback_Click"/>
                                    </p>
                     </div>
                </div>
                    <div class="row">
                        <div class="col-md-12">
                                        <p>
                                            <asp:Button ID="btnenglish" class="btn bg-green margin" runat="server" Text="English Version" OnClick="btnenglish_Click"/>
                                            <asp:Button ID="btnfrench" class="btn bg-purple margin" runat="server" Text="French Version" OnClick="btnfrench_Click" />
                                        </p>
                         </div>
                    </div>
        <asp:Panel ID="pnldata" runat="server" Visible="false">
            <div class="row">
                        <!-- left column -->
                        <div class="col-md-12">
                            <!-- general form elements -->
                            <div class="box box-primary">
                                <div class="box-header">
                                    <%--<h3 id="headertxt" class="box-title">Create New Civic Address</h3>--%>
                                    <asp:Label ID="headertxt" runat="server" class="box-title" Text="Generate PDF"></asp:Label>
                                </div><!-- /.box-header -->
                                <!-- form start -->
                                
                                    <div class="box-body">
                                        <div class="form-group">
                                            <asp:Label ID="lblleasedata" runat="server" Text="Lease Data "></asp:Label>
                                            <%--<input type="password" class="form-control" id="exampleInputPassword1" placeholder="Password">--%>
                                            <asp:TextBox ID="txtleasedata" runat="server" class="form-control" TextMode="MultiLine" Width="100%" Height="500px"></asp:TextBox>
                                        </div>
                                    </div><!-- /.box-body -->

                                    <div class="box-body">
                                        <div class="form-group">
                                            <asp:Label ID="lblscheduleb" runat="server" Text="Schedule B "></asp:Label>
                                            <%--<input type="password" class="form-control" id="exampleInputPassword1" placeholder="Password">--%>
                                            <asp:TextBox ID="txtscheduleb" runat="server" class="form-control" TextMode="MultiLine" Width="100%" Height="500px"></asp:TextBox>
                                        </div>
                                    </div><!-- /.box-body -->

                                    <div class="box-body">
                                        <div class="form-group">
                                            <asp:Label ID="lblschedulec" runat="server" Text="Schedule C "></asp:Label>
                                            <%--<input type="password" class="form-control" id="exampleInputPassword1" placeholder="Password">--%>
                                            <asp:TextBox ID="txtschedulec" runat="server" class="form-control" TextMode="MultiLine" Width="100%" Height="500px"></asp:TextBox>
                                        </div>
                                    </div><!-- /.box-body -->

                                    <div class="box-footer">
                                        <%--<button type="submit" class="btn btn-primary">Submit</button>--%>
                                        <asp:Button ID="btnsubmit" runat="server" Text="Generate Lease" class="btn btn-primary" OnClick="btnsubmit_Click"/>
                                    </div>
                            </div><!-- /.box -->
                            </div>
                        </div>
        </asp:Panel>
                    
        </section>
    </form>
</body>
</html>
