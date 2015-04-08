<!DOCTYPE html>
<html>
<head runat="server">
    <meta charset="UTF-8">
    <title>JRBuildings | Profile</title>
    <meta content='width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no' name='viewport'>
    <!-- bootstrap 3.0.2 -->
    <link href="../../css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <!-- font Awesome -->
    <link href="../../css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <!-- Ionicons -->
    <link href="../../css/ionicons.min.css" rel="stylesheet" type="text/css" />
    <!-- Theme style -->
    <link href="../../css/AdminLTE.css" rel="stylesheet" type="text/css" />

    <link rel="shortcut icon" href="../../img/home.png" >

    <script src="//ajax.googleapis.com/ajax/libs/jquery/1.11.0/jquery.min.js"></script>
    <script>



        function SetFrame(url, action) {
            if (action != '') {
                SetAction(action);
            }
            document.getElementById("iframecontent").src = url;
        }
        function SetAction(action) {
            var value = action;
            document.getElementById('headerid').innerHTML = value;
            document.getElementById('sitemapid').innerHTML = value;
        }
        function SetRole(role)
        {
            document.getElementById("aregister").style.visibility = role;
        }
    </script>
</head>
<body class="skin-blue">
    <!-- header logo: style can be found in header.less -->
    <header class="header">
        <a href="../../index.html" class="logo">
            <!-- Add the class icon to your logo image or logo icon to add the margining -->
            J R Buildings
        </a>
        <!-- Header Navbar: style can be found in header.less -->
        <nav class="navbar navbar-static-top" role="navigation">
            <!-- Sidebar toggle button-->
            <a href="#" class="navbar-btn sidebar-toggle" data-toggle="offcanvas" role="button">
                <span class="sr-only">Toggle navigation</span>
                <span class="icon-bar"></span>
                <span class="icon-bar"></span>
                <span class="icon-bar"></span>
            </a>
            <div class="navbar-right">
                <ul class="nav navbar-nav">
                    <li class="dropdown user user-menu">
                        <a href="#" class="dropdown-toggle" data-toggle="dropdown" runat="server" onclick="SignOutOnclick">
                            <i class="glyphicon glyphicon-user"></i>
                            <span>Sign Out</span>
                        </a>
                    </li>
                </ul>
            </div>
        </nav>
    </header>
    <div class="wrapper row-offcanvas row-offcanvas-left">
        <!-- Left side column. contains the logo and sidebar -->
        <aside class="left-side sidebar-offcanvas">
            <!-- sidebar: style can be found in sidebar.less -->
            <section class="sidebar">
                <ul class="sidebar-menu">
                    <ul class="sidebar-menu">
                        <li>
                            <a href="#" onclick="SetFrame('../Buildings/ViewBuildings.aspx','Home');">
                                <i class="fa fa-home"></i><span>Home</span>
                            </a>
                        </li>
                        <li id="aregister">
                            <a href="#" onclick="SetFrame('Register.aspx','Register User');">
                                <i class="fa fa-user"></i><span>Register User</span>
                            </a>
                        </li>
                        <li id="abuildings">
                            <a href="#" onclick="SetFrame('../Buildings/ViewBuildings.aspx','View Buildings');">
                                <i class="fa fa-building-o"></i><span>View Buildings</span>
                            </a>
                        </li>
                        <li class="treeview">
                            <a href="#">
                                <i class="fa fa-laptop"></i>
                                <span>View Screens</span>
                                <i class="fa fa-angle-left pull-right"></i>
                            </a>
                            <ul class="treeview-menu">
                                <!--<li><a href="#" onclick="SetFrame('../Buildings/ViewScreen.aspx?action=viewbuildings','View Buildings');"><i class="fa fa-angle-double-right"></i>View Buildings</a></li>-->
                                <li><a href="#" onclick="SetFrame('../Buildings/ViewScreen.aspx?action=viewcivic','View Civic Addresses');"><i class="fa fa-angle-double-right"></i>View Civic Addresses</a></li>
                                <li><a href="#" onclick="SetFrame('../Buildings/ViewScreen.aspx?action=viewsuites','View Suites');"><i class="fa fa-angle-double-right"></i>View Suites</a></li>
                                <li><a href="#" onclick="SetFrame('../Buildings/ViewScreen.aspx?action=viewtenants','View Tenants');"><i class="fa fa-angle-double-right"></i>View Tenants</a></li>
                                <li><a href="#" onclick="SetFrame('../Buildings/ViewScreen.aspx?action=viewtax','View Taxes and Expanses');"><i class="fa fa-angle-double-right"></i>View Taxes and Expanses</a></li>
                            </ul>
                        </li>
                        <li class="treeview">
                            <a href="#">
                                <i class="fa fa-edit"></i>
                                <span>Add Screens</span>
                                <i class="fa fa-angle-left pull-right"></i>
                            </a>
                            <ul class="treeview-menu">
                                <!--<li><a href="#" onclick="SetFrame('../Buildings/ViewScreen.aspx?action=viewbuildings','View Buildings');"><i class="fa fa-angle-double-right"></i>View Buildings</a></li>-->
                                <li><a href="#" onclick="SetFrame('../Buildings/ViewScreen.aspx?action=addcivic','Add Civic Addresses');"><i class="fa fa-angle-double-right"></i>View Civic Addresses</a></li>
                                <li><a href="#" onclick="SetFrame('../Buildings/ViewScreen.aspx?action=addsuites','Add Suites');"><i class="fa fa-angle-double-right"></i>View Suites</a></li>
                                <li><a href="#" onclick="SetFrame('../Buildings/ViewScreen.aspx?action=addtenants','Add Tenants');"><i class="fa fa-angle-double-right"></i>View Tenants</a></li>
                                <li><a href="#" onclick="SetFrame('../Buildings/ViewScreen.aspx?action=addtax','Add Taxes and Expanses');"><i class="fa fa-angle-double-right"></i>View Taxes and Expanses</a></li>
                            </ul>
                        </li>
                        <li id="adocuments">
                            <a href="#" onclick="SetFrame('../Buildings/ViewBuildings.aspx','View Documents');">
                                <i class="fa fa-file-text-o"></i><span>View Documents</span>
                            </a>
                        </li>
                        <li class="treeview">
                            <a href="#">
                                <i class="fa fa-bar-chart-o"></i>
                                <span>Reports</span>
                                <i class="fa fa-angle-left pull-right"></i>
                            </a>
                            <ul class="treeview-menu">
                                <li><a href="#" onclick="SetFrame('control.html','General');"><i class="fa fa-angle-double-right"></i>General</a></li>
                                <li><a href="#" onclick="SetFrame('control.html','Icons');"><i class="fa fa-angle-double-right"></i>Icons</a></li>
                            </ul>
                        </li>
                    </ul>
                </ul>
            </section>
            <!-- /.sidebar -->
        </aside>

        <!-- Right side column. Contains the navbar and content of the page -->
        <aside class="right-side">
            <!-- Content Header (Page header) -->
            <section class="content-header">

                <h1 id="headerid">Home
                </h1>

                <ol class="breadcrumb" style="font-size: 18px;">
                    <li><a href="../User/Profile.aspx"><i class="fa fa-home" id="homeheader"></i></a></li>
                    <li class="active" id="sitemapid">Home</li>
                </ol>
            </section>

            <!-- Main content -->
            <section class="content">
                <iframe id="iframecontent" style="min-height: 956px; width: 100%" scrolling="yes" frameborder="0" runat="server"></iframe>
            </section>
            <!-- /.content -->
        </aside>
        <!-- /.right-side -->
    </div>
    <!-- ./wrapper -->

    <script src="//ajax.googleapis.com/ajax/libs/jquery/2.1.1/jquery.min.js"></script>
    <script src="//maxcdn.bootstrapcdn.com/bootstrap/3.2.0/js/bootstrap.min.js" type="text/javascript"></script>
    <!-- AdminLTE App -->
    <script src="../../js/AdminLTE/app.js" type="text/javascript"></script>
    <!-- AdminLTE for demo purposes -->
    <script src="../../js/AdminLTE/demo.js" type="text/javascript"></script>
</body>
</html>
