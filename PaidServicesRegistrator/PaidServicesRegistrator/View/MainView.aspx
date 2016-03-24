<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MainView.aspx.cs" Inherits="PaidServicesRegistrator.View.MainView" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Pay For Service Using</title>
    <link href="../Content/css/bootstrap/bootstrap.css" type="text/css" rel="stylesheet" />
    <link href="../Content/css/site.css" type="text/css" rel="stylesheet" />
    <script src="../Scripts/bootstrap/bootstrap.min.js"></script>
    <script src="../Scripts/jquery-1.11.3.min.js"></script>
</head>
<body>
    <%-- header --%>
    <div class="navbar navbar-inverse navbar-fixed-top">
        <div class="container">

            <div class="header">Pay For Service without any trouble!</div>
        </div>
    </div>
    <%-- main form --%>

    <form id="mainForm" runat="server">
        <div class="text-center" style="margin-bottom: 30px;">
            <div class="container">
                <div class="row">
                    <div class="col-sm-6 col-md-4 col-md-offset-4">
                        <div class="account-wall" style="padding: 15px; width: 85%;">
                            <asp:TextBox class="form-control"
                                name="login"
                                ID="LoginTextBox"
                                title="Valid characters: a-z, A-Z, 0-9, _, -, . (Min length: 3)"
                                runat="server"
                                placeholder="Login"
                                pattern="^[a-zA-Z0-9.-_]{3,}$" />

                            <div title="Service Name">
                            <asp:DropDownList class="form-control"
                                ID="ServiceNameDropDownList"
                                name="serviceName"
                                runat="server" />
                            </div>
                            
                            <div title="Token Type">
                            <asp:DropDownList class="form-control"
                                ID="TokenTypeDropDownList"
                                name="tokenType"
                                runat="server"
                                AppendDataBoundItems="True" />
                            </div>
                            <button class="btn btn-lg btn-warning btn-block" type="submit" style="margin-top: 10px">
                                Sign in</button>
                        </div>
                        <div style="width: 85%;">
                            <asp:Image ImageUrl="~/Resources/money.png" runat="server" Height="60%" Width="60%" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>

    <%-- footer --%>

    <div id="footerOptions" class="navbar navbar-inverse navbar-fixed-bottom" role="navigation">
        <div class="left-side-footer">
            <h5>Payment Center</h5>
        </div>


        <div class="right-side-footer">
            <h5>Copyright &copy; Minchuk Sergei, Darya Kolyadko BSU FAMCS <%: DateTime.Now.Year %></h5>
        </div>
    </div>
</body>
</html>
