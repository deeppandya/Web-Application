<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AssignSuites.aspx.cs" Inherits="JRBuilding.Pages.Buildings.AssignSuits" UnobtrusiveValidationMode="None"%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<!DOCTYPE html>
<html class="bg-black">
    <head runat="server">
        <meta charset="UTF-8">
        <title>JRBuildings | Assign Suits</title>
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
								<div class="box-body">
											<div class="form-group">
												<asp:Label ID="lblbuildingname" runat="server" Text="Building Name"></asp:Label><br/>
												<asp:DropDownList ID="drpbuilding" runat="server"></asp:DropDownList>
											</div>
								</div>
								<div class="box box-primary">
									<div class="box-header">
										
										<asp:Label ID="headertxt1" runat="server" class="box-title" Text="First Tenant Information"></asp:Label>
									</div>
									<div class="box-body">
										  <div class="row">
													<div class="form-group col-md-4">
														<asp:Label ID="lblname1" runat="server" Text="Name"></asp:Label>
														<asp:TextBox ID="txtname1" runat="server" class="form-control" placeholder="Enter Name"></asp:TextBox>
													 <asp:RequiredFieldValidator ID="reqname1" runat="server" ErrorMessage="*" ControlToValidate="txtname1" ForeColor="Red" ValidationGroup="assign"></asp:RequiredFieldValidator>
													</div>
													<div class="form-group col-md-4">
														<asp:Label ID="lblphnum1" runat="server" Text="Phone Number"></asp:Label>
														<asp:TextBox ID="txtphnum1" runat="server" class="form-control" placeholder="Enter Phone Number"></asp:TextBox>
													<asp:RequiredFieldValidator ID="reqphnum1" runat="server" ErrorMessage="*" ControlToValidate="txtphnum1" ForeColor="Red" ValidationGroup="assign"></asp:RequiredFieldValidator>
													</div>
													<div class="form-group col-md-4">
														<asp:Label ID="lblcellnum1" runat="server" Text="Cell Number"></asp:Label>
														<asp:TextBox ID="txtcellnum1" runat="server" class="form-control" placeholder="Enter Cell Number"></asp:TextBox>
													
													</div>
											 </div>
											 <div class="row">
													<div class="form-group col-md-6">
														<asp:Label ID="lblemail1" runat="server" Text="Email Address"></asp:Label>
														<asp:TextBox ID="txtemail1" runat="server" class="form-control" placeholder="Enter Email Address"></asp:TextBox> 
                                                        <asp:RequiredFieldValidator ID="reqemail1" runat="server" ErrorMessage="*" ControlToValidate="txtemail1" ForeColor="Red" ValidationGroup="assign"></asp:RequiredFieldValidator>
													</div>
													<div class="form-group col-md-6">
													   <asp:Label ID="lblaltemail1" runat="server" Text="Alternative Email Address"></asp:Label>
														<asp:TextBox ID="txtaltemail1" runat="server" class="form-control" placeholder="Enter Alternative Email Address"></asp:TextBox> 
													</div>
											 </div>
											 <div class="row">
												<div class="form-group col-md-4">
													<asp:Label ID="lbladdress1" runat="server" Text="Home Address"></asp:Label>
													<asp:TextBox ID="txtaddress1" runat="server" class="form-control" placeholder="Enter Home Address" TextMode="MultiLine"></asp:TextBox> 
                                                    <asp:RequiredFieldValidator ID="reqaddress1" runat="server" ErrorMessage="*" ControlToValidate="txtaddress1" ForeColor="Red" ValidationGroup="assign"></asp:RequiredFieldValidator>
												</div>
												<div class="form-group col-md-4">
												   <asp:Label ID="lblduration1" runat="server" Text="Duration At address"></asp:Label>
													<asp:TextBox ID="txtduration1" runat="server" class="form-control" placeholder="Enter Duration (In Years)"></asp:TextBox> 
                                                    <asp:RequiredFieldValidator ID="reqduration1" runat="server" ErrorMessage="*" ControlToValidate="txtduration1" ForeColor="Red" ValidationGroup="assign"></asp:RequiredFieldValidator>
												</div>
												<div class="form-group col-md-4">
													<asp:Label ID="lblrental1" runat="server" Text="Rental Amount"></asp:Label>
													<asp:TextBox ID="txtrental1" runat="server" class="form-control" placeholder="Enter Rental Amount"></asp:TextBox> 
                                                    <asp:RequiredFieldValidator ID="reqrental1" runat="server" ErrorMessage="*" ControlToValidate="txtrental1" ForeColor="Red" ValidationGroup="assign"></asp:RequiredFieldValidator>
												</div>
											</div>
											<div class="row">
												<div class="form-group col-md-12">
													<asp:Label ID="lblpurpose1" runat="server" Text="Rental for which purpose"></asp:Label>
													<asp:TextBox ID="txtpurpose1" runat="server" class="form-control" placeholder="Enter Rental for which purpose" TextMode="MultiLine"></asp:TextBox> 
                                                    <asp:RequiredFieldValidator ID="reqpurpose1" runat="server" ErrorMessage="*" ControlToValidate="txtpurpose1" ForeColor="Red" ValidationGroup="assign"></asp:RequiredFieldValidator>
												</div>
											</div>
											<div class="row">
												<div class="form-group col-md-8">
													<asp:Label ID="lblsocial1" runat="server" Text="Social Insurance Number"></asp:Label>
													<asp:TextBox ID="txtsocial1" runat="server" class="form-control" placeholder="Enter Social Insurance Number"></asp:TextBox> 
                                                    <asp:RequiredFieldValidator ID="reqsocial1" runat="server" ErrorMessage="*" ControlToValidate="txtsocial1" ForeColor="Red" ValidationGroup="assign"></asp:RequiredFieldValidator>
												</div>
												<div class="form-group col-md-4">
												   <asp:Label ID="lbldob1" runat="server" Text="Date of Birth"></asp:Label>
													<asp:TextBox ID="txtdob1" runat="server" class="form-control" placeholder="Enter Date of Birth  (dd/mm/yyyy)"></asp:TextBox> 
                                                    <asp:RegularExpressionValidator runat="server" ControlToValidate="txtdob1" ValidationExpression="(((0|1)[1-9]|2[1-9]|3[0-1])\/(0[1-9]|1[0-2])\/((19|20)\d\d))$" ErrorMessage="Invalid date format." ValidationGroup="Group1" ForeColor="Red" />
													<%--<ajax:CalendarExtender ID="dob1CalendarExtender" runat="server" TargetControlID="txtdob1" Format="dd-MMM-yy">
													</ajax:CalendarExtender>--%>  
												</div>
											</div>   
									</div><!-- /.box-body -->
								</div>
								<div class="box box-primary">
									<div class="box-header">
										<%--<h3 id="headertxt" class="box-title">Create New Civic Address</h3>--%>
										<asp:Label ID="headertxt2" runat="server" class="box-title" Text="Second Tenant Information"></asp:Label>
									</div>
									<div class="box-body">
										<div class="row">
												<div class="form-group col-md-4">
													<asp:Label ID="lblname2" runat="server" Text="Name"></asp:Label>
													<asp:TextBox ID="txtname2" runat="server" class="form-control" placeholder="Enter Name"></asp:TextBox>
													 
												</div>
												<div class="form-group col-md-4">
													<asp:Label ID="lblphnum2" runat="server" Text="Phone Number"></asp:Label>
													<asp:TextBox ID="txtphnum2" runat="server" class="form-control" placeholder="Enter Phone Number"></asp:TextBox>
													
												</div>
												<div class="form-group col-md-4">
													<asp:Label ID="lblcellnum2" runat="server" Text="Cell Number"></asp:Label>
													<asp:TextBox ID="txtcellnum2" runat="server" class="form-control" placeholder="Enter Cell Number"></asp:TextBox>
													
												</div>
										</div>
										<div class="row">
											<div class="form-group col-md-6">
													<asp:Label ID="lblemail2" runat="server" Text="Email Address"></asp:Label>
													<asp:TextBox ID="txtemail2" runat="server" class="form-control" placeholder="Enter Email Address"></asp:TextBox> 
											</div>
											<div class="form-group col-md-6">
												   <asp:Label ID="lblaltemail2" runat="server" Text="Alternative Email Address"></asp:Label>
													<asp:TextBox ID="txtaltemail2" runat="server" class="form-control" placeholder="Enter Alternative Email Address"></asp:TextBox> 
											</div>
										</div>
										<div class="row">
											<div class="form-group col-md-4">
												<asp:Label ID="lbladdress2" runat="server" Text="Home Address"></asp:Label>
												<asp:TextBox ID="txtaddress2" runat="server" class="form-control" placeholder="Enter Home Address" TextMode="MultiLine"></asp:TextBox> 
											</div>
											<div class="form-group col-md-4">
											   <asp:Label ID="lblduration2" runat="server" Text="Duration At address"></asp:Label>
												<asp:TextBox ID="txtduration2" runat="server" class="form-control" placeholder="Enter Duration"></asp:TextBox> 
											</div>
											<div class="form-group col-md-4">
												<asp:Label ID="lblrental2" runat="server" Text="Rental Amount"></asp:Label>
												<asp:TextBox ID="txtrental2" runat="server" class="form-control" placeholder="Enter Rental Amount"></asp:TextBox> 
											</div>
										</div>
										<div class="row">
											<div class="form-group col-md-12">
												<asp:Label ID="lblpurpose2" runat="server" Text="Rental for which purpose"></asp:Label>
												<asp:TextBox ID="txtpurpose2" runat="server" class="form-control" placeholder="Enter Rental for which purpose" TextMode="MultiLine"></asp:TextBox> 
											</div>
										</div>
										<div class="row">
											<div class="form-group col-md-8">
												<asp:Label ID="lblsocial2" runat="server" Text="Social Insurance Number"></asp:Label>
												<asp:TextBox ID="txtsocial2" runat="server" class="form-control" placeholder="Enter Social Insurance Number"></asp:TextBox> 
											</div>
											<div class="form-group col-md-4">
											   <asp:Label ID="lbldob2" runat="server" Text="Date of Birth"></asp:Label>
												<asp:TextBox ID="txtdob2" runat="server" class="form-control" placeholder="Enter Date of Birth  (dd/mm/yyyy)"></asp:TextBox> 
                                                <asp:RegularExpressionValidator runat="server" ControlToValidate="txtdob2" ValidationExpression="(((0|1)[1-9]|2[1-9]|3[0-1])\/(0[1-9]|1[0-2])\/((19|20)\d\d))$" ErrorMessage="Invalid date format." ValidationGroup="Group1" ForeColor="Red" />
												<%--<ajax:CalendarExtender ID="dob2CalendarExtender" runat="server" TargetControlID="txtdob2" Format="dd-MMM-yy">
												</ajax:CalendarExtender>--%>  
											</div>
										</div>
									</div><!-- /.box-body -->
								</div>
                                <div class="box box-primary">
									<div class="box-header">
										<%--<h3 id="headertxt" class="box-title">Create New Civic Address</h3>--%>
										<asp:Label ID="bankinfoheadertxt" runat="server" class="box-title" Text="Banking Information"></asp:Label>
									</div><!-- /.box-header -->
                                <!-- form start -->
                                
                                    <div class="box-body">
                                        <div class="row">
                                            <div class="form-group col-md-6">
                                                <asp:Label ID="lblbankname" runat="server" Text="Bank Name"></asp:Label>
                                                <asp:TextBox ID="txtbankname" runat="server" class="form-control" placeholder="Enter Bank Name"></asp:TextBox> 
                                            </div>
                                            <div class="form-group col-md-6">
                                               <asp:Label ID="lblbankph" runat="server" Text="Contact Number"></asp:Label>
                                                <asp:TextBox ID="txtbankph" runat="server" class="form-control" placeholder="Enter Contact Number"></asp:TextBox> 
                                            </div>
                                        </div>
                                        
										<div class="row">
											<div class="form-group col-md-8">
												<asp:Label ID="lblbankaddress" runat="server" Text="Bank Address"></asp:Label>
												<asp:TextBox ID="txtbankaddress" runat="server" class="form-control" placeholder="Enter Bank Address" TextMode="MultiLine"></asp:TextBox> 
											</div>
											<div class="form-group col-md-4">
											   <asp:Label ID="lblaccountnumber" runat="server" Text="Account Number"></asp:Label>
												<asp:TextBox ID="txtaccountnumber" runat="server" class="form-control" placeholder="Enter Account Number"></asp:TextBox>   
											</div>
										</div>

                                    </div><!-- /.box-body -->
								</div>

                                <div class="box box-primary">
									<div class="box-header">
										<%--<h3 id="headertxt" class="box-title">Create New Civic Address</h3>--%>
										<asp:Label ID="empheadertxt" runat="server" class="box-title" Text="Employer References"></asp:Label>
									</div><!-- /.box-header -->
                                <!-- form start -->
                                
                                    <div class="box-body">
                                        <div class="row">
                                            <div class="form-group col-md-6">
                                                <asp:Label ID="lblcompanyname" runat="server" Text="Company Name"></asp:Label>
                                                <asp:TextBox ID="txtcompanyname" runat="server" class="form-control" placeholder="Enter Company Name"></asp:TextBox> 
                                            </div>
                                            <div class="form-group col-md-6">
                                               <asp:Label ID="lblcontactname" runat="server" Text="Contact Name"></asp:Label>
                                                <asp:TextBox ID="txtcontactname" runat="server" class="form-control" placeholder="Enter Contact Name"></asp:TextBox> 
                                            </div>
                                        </div>
                                        
										<div class="row">
											<div class="form-group col-md-8">
												<asp:Label ID="lblempaddress" runat="server" Text="Address"></asp:Label>
												<asp:TextBox ID="txtempaddress" runat="server" class="form-control" placeholder="Enter Address" TextMode="MultiLine"></asp:TextBox> 
											</div>
											<div class="form-group col-md-4">
											   <asp:Label ID="lblempphnumber" runat="server" Text="Phone Number"></asp:Label>
												<asp:TextBox ID="txtempphnumber" runat="server" class="form-control" placeholder="Enter Phone Number"></asp:TextBox>   
											</div>
										</div>

                                    </div><!-- /.box-body -->
								</div>
                                <div class="box box-primary">
									<div class="box-header">
										<%--<h3 id="headertxt" class="box-title">Create New Civic Address</h3>--%>
										<asp:Label ID="rentalheadertxt" runat="server" class="box-title" Text="Commercial Rental History"></asp:Label>
									</div><!-- /.box-header -->
                                <!-- form start -->
                                
                                    <div class="box-body">
                                        <div class="row">
                                            <div class="form-group col-md-4">
                                                <asp:Label ID="lbllanlordname" runat="server" Text="Landlord Name"></asp:Label>
                                                <asp:TextBox ID="txtlandlordname" runat="server" class="form-control" placeholder="Enter Landlord Name"></asp:TextBox> 
                                            </div>
                                            <div class="form-group col-md-4">
                                               <asp:Label ID="lblrentalphnumber" runat="server" Text="Phone Number"></asp:Label>
                                                <asp:TextBox ID="txtrentalphnumber" runat="server" class="form-control" placeholder="Enter Phone Number"></asp:TextBox> 
                                            </div>
                                            <div class="form-group col-md-4">
                                            <asp:Label ID="lblrentaladdress" runat="server" Text="Address"></asp:Label>
                                            <asp:TextBox ID="txtrentaladdress" runat="server" class="form-control" placeholder="Enter Address" TextMode="MultiLine"></asp:TextBox> 
                                            </div>
                                        </div>
                                        
                                        
										<div class="row">
											<div class="form-group col-md-4">
											   <asp:Label ID="lblrentaldatein" runat="server" Text="Date In"></asp:Label>
												<asp:TextBox ID="txtrentaldatein" runat="server" class="form-control" placeholder="Enter Date In  (dd/mm/yyyy)"></asp:TextBox>   
                                                <asp:RegularExpressionValidator runat="server" ControlToValidate="txtrentaldatein" ValidationExpression="(((0|1)[1-9]|2[1-9]|3[0-1])\/(0[1-9]|1[0-2])\/((19|20)\d\d))$" ErrorMessage="Invalid date format." ValidationGroup="Group1" ForeColor="Red" />
												<%--<ajax:CalendarExtender ID="dateinCalendarExtender" runat="server" TargetControlID="txtrentaldatein" Format="dd-MMM-yy">
												</ajax:CalendarExtender>--%>
											</div>
											<div class="form-group col-md-4">
											   <asp:Label ID="lblrentaldateout" runat="server" Text="Date Out"></asp:Label>
												<asp:TextBox ID="txtrentaldateout" runat="server" class="form-control" placeholder="Enter Date Out  (dd/mm/yyyy)"></asp:TextBox>   
                                                <asp:RegularExpressionValidator runat="server" ControlToValidate="txtrentaldateout" ValidationExpression="(((0|1)[1-9]|2[1-9]|3[0-1])\/(0[1-9]|1[0-2])\/((19|20)\d\d))$" ErrorMessage="Invalid date format." ValidationGroup="Group1" ForeColor="Red" />
												<%--<ajax:CalendarExtender ID="dateoutCalendarExtender" runat="server" TargetControlID="txtrentaldateout" Format="dd-MMM-yy">
												</ajax:CalendarExtender>--%>
											</div>
											<div class="form-group col-md-4">
												<asp:Label ID="lblrentalreason" runat="server" Text="Reason For Moving"></asp:Label>
												<asp:TextBox ID="txtrentalreason" runat="server" class="form-control" placeholder="Enter Reason for Moving" TextMode="MultiLine"></asp:TextBox> 
											</div>
										</div>

                                    </div><!-- /.box-body -->
								</div>
                                <div class="box box-primary">
									<div class="box-header">
										<%--<h3 id="headertxt" class="box-title">Create New Civic Address</h3>--%>
										<asp:Label ID="localheadertxt" runat="server" class="box-title" Text="Local Requested"></asp:Label>
									</div><!-- /.box-header -->
                                <!-- form start -->
                                
                                    <div class="box-body">
                                        <div class="row">
                                            <div class="form-group col-md-4">
                                                <asp:Label ID="lbllocalnumber" runat="server" Text="Local Number"></asp:Label><br />
                                                <asp:DropDownList ID="drplocal" runat="server"></asp:DropDownList>
                                            </div>
                                            <div class="form-group col-md-4">
                                               <asp:Label ID="lbllocalstartdate" runat="server" Text="Start Date"></asp:Label>
                                                <asp:TextBox ID="txtlocalstartdate" runat="server" class="form-control" placeholder="Enter Start Date (dd/mm/yyyy)"></asp:TextBox>   
                                                <asp:RegularExpressionValidator runat="server" ControlToValidate="txtlocalstartdate" ValidationExpression="(((0|1)[1-9]|2[1-9]|3[0-1])\/(0[1-9]|1[0-2])\/((19|20)\d\d))$" ErrorMessage="Invalid date format." ValidationGroup="Group1" ForeColor="Red" />
                                               <%-- <ajax:CalendarExtender ID="localstartdateCalendarExtender" runat="server" TargetControlID="txtlocalstartdate" Format="dd-MMM-yy">
                                                </ajax:CalendarExtender>--%>
                                                <asp:RequiredFieldValidator ID="reqlocalstartdate" runat="server" ErrorMessage="*" ControlToValidate="txtlocalstartdate" ForeColor="Red" ValidationGroup="assign"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="form-group col-md-4">
                                               <asp:Label ID="lbllocalduration" runat="server" Text="Duration"></asp:Label>
                                                <asp:TextBox ID="txtlocalduration" runat="server" class="form-control" placeholder="Enter Duration"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="reqlocalduration" runat="server" ErrorMessage="*" ControlToValidate="txtlocalduration" ForeColor="Red" ValidationGroup="assign"></asp:RequiredFieldValidator> 
                                            </div>
                                        </div> 
										<div class="row">
											<div class="form-group col-md-4">
											   <asp:Label ID="lbllocalamount" runat="server" Text="Rental Amount"></asp:Label>
												<asp:TextBox ID="txtlocalamount" runat="server" class="form-control" placeholder="Enter Rental Amount"></asp:TextBox> 
                                                <asp:RequiredFieldValidator ID="reqlocalamount" runat="server" ErrorMessage="*" ControlToValidate="txtlocalamount" ForeColor="Red" ValidationGroup="assign"></asp:RequiredFieldValidator>  
											</div>
											<div class="form-group col-md-4">
												<asp:Label ID="lbllocaldeposite" runat="server" Text="Deposite Left (first and last months taxes included)"></asp:Label>
												<asp:TextBox ID="txtlocaldeposite" runat="server" class="form-control" placeholder="Enter Deposite Left"></asp:TextBox> 
                                                <asp:RequiredFieldValidator ID="reqlocaldeposite" runat="server" ErrorMessage="*" ControlToValidate="txtlocaldeposite" ForeColor="Red" ValidationGroup="assign"></asp:RequiredFieldValidator>
											</div>
                                            <div class="form-group col-md-4">
												<asp:Label ID="lblrentalincrease" runat="server" Text="Rental Increase %"></asp:Label>
												<asp:TextBox ID="txtrentalincrease" runat="server" class="form-control" placeholder="Enter Rental Increase %"></asp:TextBox> 
											</div>
										</div>
                                        <div class="row">
											<div class="form-group col-md-4">
                                                <asp:Label ID="lblcurrent" runat="server" Text="Is Current ?"></asp:Label><br />
                                                <asp:DropDownList ID="drpcurrent" runat="server">
                                                    <asp:ListItem>Yes</asp:ListItem>
                                                    <asp:ListItem>No</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="form-group col-md-4">
                                               <asp:Label ID="lblannualper" runat="server" Text="Annual %"></asp:Label>
                                                <asp:TextBox ID="txtannualper" runat="server" class="form-control" placeholder="Enter Annual %"></asp:TextBox>   
                                                <asp:RequiredFieldValidator ID="reqannualper" runat="server" ErrorMessage="*" ControlToValidate="txtannualper" ForeColor="Red" ValidationGroup="assign"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="form-group col-md-4">
                                               <asp:Label ID="lbloptions" runat="server" Text="Duration"></asp:Label>
                                                <asp:TextBox ID="txtoptions" runat="server" class="form-control" placeholder="Enter Options" TextMode="MultiLine"></asp:TextBox>
                                            </div>
										</div>
                                    </div><!-- /.box-body -->
								</div>
                                <div class="box-footer">
                                    <asp:Button ID="btnsubmit" runat="server" Text="Add Tenant Information" class="btn btn-primary" OnClick="btnsubmit_Click" ValidationGroup="assign"/>
                                    <asp:Button ID="btnupdate" runat="server" Text="Update Tenant Information" class="btn btn-primary" OnClick="btnupdate_Click" ValidationGroup="assign"/>
                                </div>
                         </div><!-- /.box -->
                    </div>
					<div class="row">
						<div class="col-md-12">
										<p>
											<asp:Button ID="btnedit" class="btn bg-orange margin" runat="server" Text="Edit Tenant Information" OnClick="btnedit_Click"/>
                                            <asp:Button ID="btndelete" runat="server" Text="Delete Tenant Information" class="btn bg-red margin"  ValidationGroup="suites" OnClick="btndelete_Click"/>     
                                            <asp:Button ID="btnupload" runat="server" Text="Upload Documents" class="btn bg-green margin"  ValidationGroup="suites" OnClick="btnupload_Click" />     
                                            <asp:Button ID="btnlaese" runat="server" Text="Generate Lease" class="btn bg-teal margin"  ValidationGroup="suites" OnClick="btnlaese_Click" />     
										</p>
						 </div>
					</div>
        </section>
    </form>
</body>
</html>