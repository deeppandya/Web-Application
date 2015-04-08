<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Notifications.aspx.cs" Inherits="JRBuilding.Pages.User.Notifications" %>

<!DOCTYPE html>
<html class="bg-black">
    <head>
        <meta charset="UTF-8">
        <title>JRBuildings | Log in</title>
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
        <style>
        .center {
            margin:0 auto;
            width: 70%;
            vertical-align: middle;
            margin-top:60px;
        }
        </style>
    </head>
<body class="bg-black">
    <form id="form1" runat="server">
        <label class="has-success" runat="server" id="lblmsg"></label>
                        <div class="box box-warning center">
                                <div class="box-header">
                                    <h3 class="box-title">Email Notifications</h3>
                                </div><!-- /.box-header -->
                                <div class="box-body">
                                        <!-- text input -->
                                        <div class="form-group">
                                            <label>To</label>
                                            <asp:TextBox ID="txtto" runat="server" class="form-control"></asp:TextBox>
                                        </div>
                                        <div class="form-group">
                                            <label>Subject</label>
                                            <asp:TextBox ID="txtsubject" runat="server" class="form-control"></asp:TextBox>
                                        </div>
                                        <div class="form-group">
                                            <label>Message Body</label>
                                            <asp:TextBox ID="txtbody" runat="server" class="form-control" TextMode="MultiLine" Height="200px"></asp:TextBox>
                                        </div>
                                </div><!-- /.box-body -->
                                <div class="box-footer">
                                        <%--<button type="submit" class="btn btn-primary">Submit</button>--%>
                                        <asp:Button ID="btnsubmit" runat="server" Text="Send" class="btn btn-primary" OnClick="btnsubmit_Click"/>
                                </div>
                            </div><!-- /.box -->
        <!-- jQuery 2.0.2 -->
        <script src="http://ajax.googleapis.com/ajax/libs/jquery/2.0.2/jquery.min.js"></script>
        <!-- Bootstrap -->
        <script src="../../js/bootstrap.min.js" type="text/javascript"></script>
    </form>
</body>
</html>

