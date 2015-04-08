<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TaxandExpense.aspx.cs" Inherits="JRBuilding.Pages.Buildings.TaxandExpense" UnobtrusiveValidationMode="None"%>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<!DOCTYPE html>
<html class="bg-black">
    <head runat="server">
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
        <%--<ajax:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajax:ToolkitScriptManager>--%>
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:Label ID="lblerror" runat="server"></asp:Label>
    <section class="content bg-black" id="sectiondata" runat="server">
        <div class="row">
                    <div class="col-md-12">
                                    <p>
                                        <%--<asp:Button ID="btnback" class="btn btn-default margin" runat="server" Text="Back" OnClick="btnback_Click"/>--%>
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
                                    <asp:Label ID="headertxt" runat="server" class="box-title" Text="Tax and Expenses"></asp:Label>
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
                                        <div class="row">
                                            <div class="form-group col-md-6">
                                                <asp:Label ID="lblfromdate" runat="server" Text="Start Date"></asp:Label>
                                                <asp:TextBox ID="txtfromdate" runat="server" class="form-control" placeholder="Select Start Date (dd/mm/yyyy)"></asp:TextBox>
                                                <%--<ajax:CalendarExtender ID="fromCalendarExtender" runat="server" TargetControlID="txtfromdate" Format="dd-MMM-yy">
                                                </ajax:CalendarExtender> --%>
                                                <asp:RegularExpressionValidator runat="server" ControlToValidate="txtfromdate" ValidationExpression="^(?:(?:31(\/|-|\.)(?:0?[13578]|1[02]|(?:Jan|Mar|May|Jul|Aug|Oct|Dec)))\1|(?:(?:29|30)(\/|-|\.)(?:0?[1,3-9]|1[0-2]|(?:Jan|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec))\2))(?:(?:1[6-9]|[2-9]\d)?\d{2})$|^(?:29(\/|-|\.)(?:0?2|(?:Feb))\3(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00))))$|^(?:0?[1-9]|1\d|2[0-8])(\/|-|\.)(?:(?:0?[1-9]|(?:Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep))|(?:1[0-2]|(?:Oct|Nov|Dec)))\4(?:(?:1[6-9]|[2-9]\d)?\d{2})$" ErrorMessage="Invalid date format." ValidationGroup="Group1" ForeColor="Red" />
                                                <asp:RequiredFieldValidator ID="reqfromdate" runat="server" ErrorMessage="*" ControlToValidate="txtfromdate" ForeColor="Red" ValidationGroup="tax"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="form-group col-md-6">
                                                <asp:Label ID="lbltodate" runat="server" Text="End Date"></asp:Label>
                                                <asp:TextBox ID="txttodate" runat="server" class="form-control" placeholder="Select End Date (dd/mm/yyyy)"></asp:TextBox>
                                                <%--<ajax:CalendarExtender ID="toCalendarExtender" runat="server" TargetControlID="txttodate" Format="dd-MMM-yy">
                                                </ajax:CalendarExtender>--%>
                                                <asp:RegularExpressionValidator runat="server" ControlToValidate="txttodate" ValidationExpression="(((0|1)[1-9]|2[1-9]|3[0-1])\/(0[1-9]|1[0-2])\/((19|20)\d\d))$" ErrorMessage="Invalid date format." ValidationGroup="Group1" ForeColor="Red" />
                                                <asp:RequiredFieldValidator ID="reqtodate" runat="server" ErrorMessage="*" ControlToValidate="txttodate" ForeColor="Red" ValidationGroup="tax"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="form-group col-md-6">
                                                <asp:Label ID="lblgstnum" runat="server" Text="GST #"></asp:Label>
                                                <asp:TextBox ID="txtgstnum" runat="server" class="form-control" placeholder="GST #"></asp:TextBox> 
                                                <asp:RequiredFieldValidator ID="reqgstnum" runat="server" ErrorMessage="*" ControlToValidate="txtgstnum" ForeColor="Red" ValidationGroup="tax"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="form-group col-md-6">
                                               <asp:Label ID="lblqstnum" runat="server" Text="QST #"></asp:Label>
                                                <asp:TextBox ID="txtqstnum" runat="server" class="form-control" placeholder="QST #"></asp:TextBox> 
                                                <asp:RequiredFieldValidator ID="reqqstnum" runat="server" ErrorMessage="*" ControlToValidate="txtqstnum" ForeColor="Red" ValidationGroup="tax"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="form-group col-md-6">
                                                <asp:Label ID="lbltax1name" runat="server" Text="Tax 1 Description "></asp:Label>
                                                <asp:TextBox ID="txttax1name" runat="server" class="form-control" placeholder="Enter Tax 1 Description"></asp:TextBox> 
                                            </div>
                                            <div class="form-group col-md-6">
                                               <asp:Label ID="lbltax1value" runat="server" Text="Tax 1 Value"></asp:Label>
                                                <asp:TextBox ID="txttax1value" runat="server" class="form-control" placeholder="Enter Tax 1 Value"></asp:TextBox> 
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="form-group col-md-6">
                                                <asp:Label ID="lbltax2name" runat="server" Text="Tax 2 Description "></asp:Label>
                                                <asp:TextBox ID="txttax2name" runat="server" class="form-control" placeholder="Enter Tax 2 Description"></asp:TextBox> 
                                            </div>
                                            <div class="form-group col-md-6">
                                               <asp:Label ID="lbltax2value" runat="server" Text="Tax 2 Value"></asp:Label>
                                                <asp:TextBox ID="txttax2value" runat="server" class="form-control" placeholder="Enter Tax 2 Value"></asp:TextBox> 
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="form-group col-md-6">
                                                <asp:Label ID="lbltax3name" runat="server" Text="Tax 3 Description "></asp:Label>
                                                <asp:TextBox ID="txttax3name" runat="server" class="form-control" placeholder="Enter Tax 3 Description"></asp:TextBox> 
                                            </div>
                                            <div class="form-group col-md-6">
                                               <asp:Label ID="lbltax3value" runat="server" Text="Tax 3 Value"></asp:Label>
                                                <asp:TextBox ID="txttax3value" runat="server" class="form-control" placeholder="Enter Tax 3 Value"></asp:TextBox> 
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="form-group col-md-6">
                                                <asp:Label ID="lbltax4name" runat="server" Text="Tax 4 Description "></asp:Label>
                                                <asp:TextBox ID="txttax4name" runat="server" class="form-control" placeholder="Enter Tax 4 Description"></asp:TextBox> 
                                            </div>
                                            <div class="form-group col-md-6">
                                               <asp:Label ID="lbltax4value" runat="server" Text="Tax 4 Value"></asp:Label>
                                                <asp:TextBox ID="txttax4value" runat="server" class="form-control" placeholder="Enter Tax 4 Value"></asp:TextBox> 
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="form-group col-md-6">
                                                <asp:Label ID="lbltax5name" runat="server" Text="Tax 5 Description "></asp:Label>
                                                <asp:TextBox ID="txttax5name" runat="server" class="form-control" placeholder="Enter Tax 5 Description"></asp:TextBox> 
                                            </div>
                                            <div class="form-group col-md-6">
                                               <asp:Label ID="lbltax5value" runat="server" Text="Tax 5 Value"></asp:Label>
                                                <asp:TextBox ID="txttax5value" runat="server" class="form-control" placeholder="Enter Tax 5 Value"></asp:TextBox> 
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="form-group col-md-6">
                                                <asp:Label ID="lbltax6name" runat="server" Text="Tax 6 Description "></asp:Label>
                                                <asp:TextBox ID="txttax6name" runat="server" class="form-control" placeholder="Enter Tax 6 Description"></asp:TextBox> 
                                            </div>
                                            <div class="form-group col-md-6">
                                               <asp:Label ID="lbltax6value" runat="server" Text="Tax 6 Value"></asp:Label>
                                                <asp:TextBox ID="txttax6value" runat="server" class="form-control" placeholder="Enter Tax 6 Value"></asp:TextBox> 
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="form-group col-md-6">
                                                <asp:Label ID="lbltax7name" runat="server" Text="Tax 7 Description "></asp:Label>
                                                <asp:TextBox ID="txttax7name" runat="server" class="form-control" placeholder="Enter Tax 7 Description"></asp:TextBox> 
                                            </div>
                                            <div class="form-group col-md-6">
                                               <asp:Label ID="lbltax7value" runat="server" Text="Tax 7 Value"></asp:Label>
                                                <asp:TextBox ID="txttax7value" runat="server" class="form-control" placeholder="Enter Tax 7 Value"></asp:TextBox> 
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="form-group col-md-6">
                                                <asp:Label ID="lbltax8name" runat="server" Text="Tax 8 Description "></asp:Label>
                                                <asp:TextBox ID="txttax8name" runat="server" class="form-control" placeholder="Enter Tax 8 Description"></asp:TextBox> 
                                            </div>
                                            <div class="form-group col-md-6">
                                               <asp:Label ID="lbltax8value" runat="server" Text="Tax 8 Value"></asp:Label>
                                                <asp:TextBox ID="txttax8value" runat="server" class="form-control" placeholder="Enter Tax 8 Value"></asp:TextBox> 
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="form-group col-md-6">
                                                <asp:Label ID="lbltax9name" runat="server" Text="Tax 9 Description "></asp:Label>
                                                <asp:TextBox ID="txttax9name" runat="server" class="form-control" placeholder="Enter Tax 9 Description"></asp:TextBox> 
                                            </div>
                                            <div class="form-group col-md-6">
                                               <asp:Label ID="lbltax9value" runat="server" Text="Tax 9 Value"></asp:Label>
                                                <asp:TextBox ID="txttax9value" runat="server" class="form-control" placeholder="Enter Tax 9 Value"></asp:TextBox> 
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="form-group col-md-6">
                                                <asp:Label ID="lbltax10name" runat="server" Text="Tax 10 Description "></asp:Label>
                                                <asp:TextBox ID="txttax10name" runat="server" class="form-control" placeholder="Enter Tax 10 Description"></asp:TextBox> 
                                            </div>
                                            <div class="form-group col-md-6">
                                               <asp:Label ID="lbltax10value" runat="server" Text="Tax 10 Value"></asp:Label>
                                                <asp:TextBox ID="txttax10value" runat="server" class="form-control" placeholder="Enter Tax 10 Value"></asp:TextBox> 
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="form-group col-md-6">
                                                <asp:Label ID="lbltax11name" runat="server" Text="Tax 11 Description "></asp:Label>
                                                <asp:TextBox ID="txttax11name" runat="server" class="form-control" placeholder="Enter Tax 11 Description"></asp:TextBox> 
                                            </div>
                                            <div class="form-group col-md-6">
                                               <asp:Label ID="lbltax11value" runat="server" Text="Tax 11 Value"></asp:Label>
                                                <asp:TextBox ID="txttax11value" runat="server" class="form-control" placeholder="Enter Tax 11 Value"></asp:TextBox> 
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="form-group col-md-6">
                                                <asp:Label ID="lbltax12name" runat="server" Text="Tax 12 Description "></asp:Label>
                                                <asp:TextBox ID="txttax12name" runat="server" class="form-control" placeholder="Enter Tax 12 Description"></asp:TextBox> 
                                            </div>
                                            <div class="form-group col-md-6">
                                               <asp:Label ID="lbltax12value" runat="server" Text="Tax 12 Value"></asp:Label>
                                                <asp:TextBox ID="txttax12value" runat="server" class="form-control" placeholder="Enter Tax 12 Value"></asp:TextBox> 
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="form-group col-md-6">
                                                <asp:Label ID="lbltax13name" runat="server" Text="Tax 13 Description "></asp:Label>
                                                <asp:TextBox ID="txttax13name" runat="server" class="form-control" placeholder="Enter Tax 13 Description"></asp:TextBox> 
                                            </div>
                                            <div class="form-group col-md-6">
                                               <asp:Label ID="lbltax13value" runat="server" Text="Tax 13 Value"></asp:Label>
                                                <asp:TextBox ID="txttax13value" runat="server" class="form-control" placeholder="Enter Tax 13 Value"></asp:TextBox> 
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="form-group col-md-6">
                                                <asp:Label ID="lbltax14name" runat="server" Text="Tax 14 Description "></asp:Label>
                                                <asp:TextBox ID="txttax14name" runat="server" class="form-control" placeholder="Enter Tax 14 Description"></asp:TextBox> 
                                            </div>
                                            <div class="form-group col-md-6">
                                               <asp:Label ID="lbltax14value" runat="server" Text="Tax 14 Value"></asp:Label>
                                                <asp:TextBox ID="txttax14value" runat="server" class="form-control" placeholder="Enter Tax 14 Value"></asp:TextBox> 
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="form-group col-md-6">
                                                <asp:Label ID="lbltax15name" runat="server" Text="Tax 15 Description "></asp:Label>
                                                <asp:TextBox ID="txttax15name" runat="server" class="form-control" placeholder="Enter Tax 15 Description"></asp:TextBox> 
                                            </div>
                                            <div class="form-group col-md-6">
                                               <asp:Label ID="lbltax15value" runat="server" Text="Tax 15 Value"></asp:Label>
                                                <asp:TextBox ID="txttax15value" runat="server" class="form-control" placeholder="Enter Tax 15 Value"></asp:TextBox> 
                                            </div>
                                        </div>

                                       <%--<div class="row">
                                            <div class="form-group col-md-6">
                                                <asp:Label ID="lbladminfees" runat="server" Text="Admin Fees"></asp:Label>
                                                <asp:TextBox ID="txtadminfees" runat="server" class="form-control" placeholder="Enter Admin Fees"></asp:TextBox> 
                                            </div>
                                            <div class="form-group col-md-6">
                                               <asp:Label ID="lblrepairs" runat="server" Text="Repairs and Maintenance"></asp:Label>
                                                <asp:TextBox ID="txtrepairs" runat="server" class="form-control" placeholder="Enter Repairs and Maintenance"></asp:TextBox> 
                                            </div>
                                        </div>--%>
                                        <div class="row">
                                            <div class="form-group col-md-6">
                                                <asp:Label ID="lblgstper" runat="server" Text="GST %"></asp:Label>
                                                <asp:TextBox ID="txtgstper" runat="server" class="form-control" placeholder="GST %"></asp:TextBox> 
                                                <asp:RequiredFieldValidator ID="reqgstper" runat="server" ErrorMessage="*" ControlToValidate="txtgstper" ForeColor="Red" ValidationGroup="tax"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="form-group col-md-6">
                                               <asp:Label ID="lblqstper" runat="server" Text="QST %"></asp:Label>
                                                <asp:TextBox ID="txtqstper" runat="server" class="form-control" placeholder="QST %"></asp:TextBox> 
                                                <asp:RequiredFieldValidator ID="reqqstper" runat="server" ErrorMessage="*" ControlToValidate="txtqstper" ForeColor="Red" ValidationGroup="tax"></asp:RequiredFieldValidator>
                                            </div>
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
                                        <asp:Button ID="btnsubmit" runat="server" Text="Add Tax and Expense" class="btn btn-primary" OnClick="btnsubmit_Click" Visible="false" ValidationGroup="tax"/>
                                        <asp:Button ID="btnupdate" runat="server" Text="Update Tax and Expense" class="btn btn-primary" OnClick="btnupdate_Click" Visible="false" ValidationGroup="tax"/>
                                    </div>
                            </div><!-- /.box -->
                            </div>
                        </div>
                 <div class="row">
                    <div class="col-md-12">
                                    <p>
                                        <asp:Button ID="btnedittax" class="btn bg-orange margin" runat="server" Text="Edit Tax and Expense" OnClick="btnedittax_Click"/>
                                        <asp:Button ID="btndelete" class="btn bg-red margin" runat="server" Text="Delete Tax and Expense" OnClick="btndelete_Click" />
                                    </p>
                     </div>
                </div>
        </section>
    </form>
</body>
</html>
