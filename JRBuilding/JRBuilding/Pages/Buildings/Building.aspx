<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Building.aspx.cs" Inherits="JRBuilding.Pages.Buildings.Building" UnobtrusiveValidationMode="None"%>

<!DOCTYPE html>
<html class="bg-black">
    <head>
        <meta charset="UTF-8">
        <title>JRBuildings | Buildings</title>
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
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
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
                                    <asp:Label ID="headertxt" runat="server" class="box-title">Create New Building</asp:Label>
                                   <%-- <h3 id="headertxt" class="box-title">Create New Building</h3>--%>
                                </div><!-- /.box-header -->
                                <!-- form start -->
                                
                                    <div class="box-body">
                                        <div class="form-group">
                                            <asp:Label ID="lblname" runat="server" Text="Building Name"></asp:Label>
                                            <asp:TextBox ID="txtname" runat="server" class="form-control" placeholder="Enter Building Name"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="reqname" runat="server" ErrorMessage="*" ControlToValidate="txtname" ForeColor="Red" ValidationGroup="building"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group">
                                            <asp:Label ID="lbladdress" runat="server" Text="Building Location"></asp:Label>
                                            <asp:TextBox ID="txtaddress" runat="server" class="form-control" placeholder="Enter Building Location" TextMode="MultiLine"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="reqaddress" runat="server" ErrorMessage="*" ControlToValidate="txtaddress" ForeColor="Red" ValidationGroup="building"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group">
                                            <asp:Label ID="lblpicture" runat="server" Text="Building Picture"></asp:Label>
                                            <asp:FileUpload ID="imgupload" runat="server" />
                                        </div>
                                    </div>
                                    <div class="box-footer">
                                        <asp:Button ID="btnsubmit" runat="server" Text="Add Building" class="btn btn-primary" OnClick="btnsubmit_Click" ValidationGroup="building"/>
                                        <asp:Button ID="btnupdate" runat="server" Text="Update Building" class="btn btn-primary" OnClick="btnupdate_Click" ValidationGroup="building"/>
                                    </div>
                            </div><!-- /.box -->
                            </div>
                        </div>
                <div class="row">
                    <div class="col-md-12">
                                    <p>
                                        <asp:Button ID="btnaddlocals" class="btn bg-green margin" runat="server" Text="Add Civics" OnClick="btnaddlocals_Click" Visible="false"/>
                                        <asp:Button ID="btnedit" class="btn bg-orange margin" runat="server" Text="Edit Building Info." OnClick="btnedit_Click" />
                                        <asp:Button ID="btndelete" class="btn bg-maroon margin" runat="server" Text="Delete Building Info." OnClick="btndelete_Click" />
                                        <asp:Button ID="btntax" class="btn bg-olive margin" runat="server" Text="Tax and Expense" OnClick="btntax_Click" Visible="false"/>
                                    </p>
                     </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>--%>
                        <asp:GridView ID="GridView1" runat="server"  Width = "100%"
                        AutoGenerateColumns = "False" Font-Names = "Arial" AllowSorting="True"
                        Font-Size = "11pt" AlternatingRowStyle-BackColor = "#CBD8DF" 
                        HeaderStyle-BackColor = "Blue" AllowPaging ="True"  ShowFooter = "True" 
                        OnPageIndexChanging = "OnPaging" onrowediting="EditCustomer"
                        onrowdatabound="RowDataBound"
                        onrowupdating="UpdateCustomer"  onrowcancelingedit="CancelEdit" CellPadding="4" ForeColor="#333333" GridLines="None" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" >
                        <Columns>
                            <asp:CommandField SelectText="See Details..." ShowSelectButton="True" />
                        <asp:TemplateField ItemStyle-Width = "100px" Visible="false" HeaderText = "Local Id">
                            <ItemTemplate>
                                <asp:Label ID="lbllocalid" runat="server"
                                Text='<%# Eval("local_id")%>'></asp:Label>
                            </ItemTemplate>
                        <ItemStyle Width="30px"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-Width = "150px"  HeaderText = "Civic Address">
                            <ItemTemplate>
                                <asp:Label ID="lbladdress" runat="server"
                                    Text='<%# Eval("local_address")%>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtaddress" runat="server"
                                    Text='<%# Eval("local_address")%>'></asp:TextBox>
                            </EditItemTemplate>
                        <ItemStyle Width="150px"></ItemStyle>
                        </asp:TemplateField>
                            <asp:TemplateField ItemStyle-Width = "150px" Visible="false"  HeaderText = "Building">
                            <ItemTemplate>
                                <asp:Label ID="lblbuilding" runat="server"
                                    Text='<%# Eval("Building_Id")%>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtbuilding" runat="server"
                                    Text='<%# Eval("Building_Id")%>'></asp:TextBox>
                            </EditItemTemplate> 
                        <ItemStyle Width="150px"></ItemStyle>
                        </asp:TemplateField>
                            <asp:TemplateField ItemStyle-Width = "150px" Visible="true"  HeaderText = "Is Suits ? ">
                            <ItemTemplate>
                                <asp:Label ID="lblissuits" runat="server"
                                    Text='<%# Eval("Is_Suits")%>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:DropDownList ID="drpissuits" runat="server" >
                                                <asp:ListItem>Yes</asp:ListItem>
                                                <asp:ListItem>No</asp:ListItem>
                                            </asp:DropDownList>
                                <%--<asp:TextBox ID="txtissuits" runat="server"
                                    Text='<%# Eval("Is_Suits")%>'></asp:TextBox>--%>
                            </EditItemTemplate>
                        <ItemStyle Width="150px"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkRemove" runat="server"
                                    CommandArgument = '<%# Eval("local_id")%>'
                                 OnClientClick = "return confirm('Do you want to delete?')"
                                Text = "Delete" OnClick = "DeleteCustomer"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:CommandField  ShowEditButton="True" />
                        </Columns>
                        <AlternatingRowStyle BackColor="White"  />
                            <EditRowStyle BackColor="#2461BF" />
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />

                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White"></HeaderStyle>
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#EFF3FB" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <SortedAscendingCellStyle BackColor="#F5F7FB" />
                            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                            <SortedDescendingCellStyle BackColor="#E9EBEF" />
                            <SortedDescendingHeaderStyle BackColor="#4870BE" />
                        </asp:GridView>
                        <%--</ContentTemplate>
                        <Triggers>
                        <asp:AsyncPostBackTrigger ControlID = "GridView1" />
                        </Triggers>
                        </asp:UpdatePanel>--%>
                     </div>
                </div>
        </section>
    </form>
</body>
</html>
