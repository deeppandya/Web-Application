<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="locals.aspx.cs" Inherits="JRBuilding.Pages.Buildings.locals" UnobtrusiveValidationMode="None"%>

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
                                        <asp:Button ID="btnback" class="btn btn-default margin" runat="server" Text="Back" OnClick="btnback_Click" Visible="false"/>
                                    </p>
                     </div>
                </div>
                    <div class="row">
                        <!-- left column -->
                        <div class="col-md-12">
                            <!-- general form elements -->
                            <div class="box box-primary">
                                <div class="box-header">
                                    <%--<h3 id="headertxt" class="box-title">Create New Civic Address</h3>--%>
                                    <asp:Label ID="headertxt" runat="server" class="box-title"></asp:Label>
                                </div><!-- /.box-header -->
                                <!-- form start -->
                                
                                    <div class="box-body">
                                        <div class="form-group">
                                            <%--<label >Email address</label>--%>
                                            <asp:Label ID="lblbuilding" runat="server" Text="Building Name"></asp:Label><br/>
                                            <asp:DropDownList ID="drpbuilding" runat="server"></asp:DropDownList>
                                            <%--<asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:JRBConnectionString %>" SelectCommand="SELECT * FROM [Buildings]"></asp:SqlDataSource>--%>
                                            <%--<input type="email" class="form-control" id="exampleInputEmail1" placeholder="Enter email">--%>
                                            <%--<asp:TextBox ID="txtid" runat="server" class="form-control" placeholder="Enter Building Id"></asp:TextBox>--%>
                                        </div>

                                        <div class="form-group">
                                            <%--<label >Email address</label>--%>
                                            <asp:Label ID="lblissuits" runat="server" Text="Does this civic address have any suits ?"></asp:Label><br/>
                                            <asp:DropDownList ID="drpissuits" runat="server" OnSelectedIndexChanged="drpissuits_SelectedIndexChanged" >
                                                <asp:ListItem>Yes</asp:ListItem>
                                                <asp:ListItem>No</asp:ListItem>
                                            </asp:DropDownList>
                                            <%--<asp:RequiredFieldValidator ID="reqissuits" runat="server" ErrorMessage="*" ControlToValidate="drpissuits" ForeColor="Red"></asp:RequiredFieldValidator>--%>
                                        </div>
                                        
                                        <div class="form-group">
                                            <asp:Label ID="lbladdress" runat="server" Text="Civic Address"></asp:Label>
                                            <%--<input type="password" class="form-control" id="exampleInputPassword1" placeholder="Password">--%>
                                            <asp:TextBox ID="txtaddress" runat="server" class="form-control" placeholder="Enter Civic Address" TextMode="MultiLine"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="reqaddress" runat="server" ErrorMessage="*" ControlToValidate="txtaddress" ForeColor="Red" ValidationGroup="local"></asp:RequiredFieldValidator>
                                        </div>
                                        <%--<div class="form-group">
                                            <asp:Label ID="lblpicture" runat="server" Text="Civic Building Picture"></asp:Label>
                                            <asp:FileUpload ID="imgupload" runat="server" />
                                        </div>--%>
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
                                        <asp:Button ID="btnsubmit" runat="server" Text="Add Civic Building" class="btn btn-primary" OnClick="btnsubmit_Click" ValidationGroup="local"/>
                                        <asp:Button ID="btnupdate" runat="server" Text="Update Civic Building" class="btn btn-primary" ValidationGroup="local" OnClick="btnupdate_Click"/>
                                    </div>
                            </div><!-- /.box -->
                            </div>
                        </div>
                 <div class="row">
                    <div class="col-md-12">
                                    <p>
                                        <%--<asp:Button ID="btnaddsuits" class="btn bg-green margin" runat="server" Text="Add Suites" OnClick="btnaddsuits_Click" Visible="false"/>
                                        <asp:Button ID="btnassignsuits" class="btn bg-purple margin" runat="server" Text="Assign Suites" OnClick="btnassignsuits_Click" Visible="false" />
                                        <asp:Button ID="btnuploadlease" class="btn bg-navy margin" runat="server" Text="Upload Documents" OnClick="btnuploadlease_Click" Visible="false" />
                                        <asp:Button ID="btnviewlease" class="btn bg-aqua margin" runat="server" Text="View Documents" Visible="false" OnClick="btnviewlease_Click"/>--%>
                                        <asp:Button ID="btnedit" class="btn bg-orange margin" runat="server" Text="Edit Civic Info." OnClick="txtedit_Click" />
                                        <asp:Button ID="btndelete" class="btn bg-maroon margin" runat="server" Text="Delete Civic Info." OnClick="txtdelete_Click" />
                                    </p>
                     </div>
                </div>
        </section>
    </form>
</body>
</html>
