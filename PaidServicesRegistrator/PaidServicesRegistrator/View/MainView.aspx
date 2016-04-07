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
    <form class="form-horizontal" role="form" runat="server">
        <div class="container">
            <div class="row">
                <div class="text-center" style="margin-bottom: 20px;">
                    <div class="col-sm-9 col-md-4 col-md-offset-4">
                        <div id="FormDiv" runat="server" class="account-wall" style="padding: 15px; width: 90%;">
                            <asp:Label runat="server" Text="Choose a service to pay for and
                                a token type. Then generate your token." />
                            <br />
                            <asp:Label runat="server" Text="REMEMBER IT" />
                            <div class="form-group">
                                <label class="control-label col-sm-5">Service name:</label>
                                <div class="col-sm-11">
                                    <asp:DropDownList class="form-control"
                                        ID="ServiceNameDropDownList"
                                        name="serviceName"
                                        runat="server" />
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="control-label col-sm-4">Token type:</label>
                                <div class="col-sm-11">
                                    <asp:DropDownList class="form-control"
                                        ID="TokenTypeDropDownList"
                                        name="tokenType"
                                        runat="server" />
                                </div>
                                <div class="col-sm-11">
                                    <asp:Button CssClass="btn btn-lg btn-warning btn-block" runat="server"
                                        Style="margin-top: 25px" Text="Get token" OnClick="OnGetTokenButtonClick" />
                                </div>
                            </div>
                        </div>
                        <div id="TokenDiv" style="width: 93%; margin-top: 15px; display: none" runat="server">
                            <asp:Label Text="Your token:" runat="server" CssClass="token-text-label"></asp:Label><br />
                            <asp:Label ID="TokenValueLabel" Text="iviu9y0307y-f4yf0huyg8" runat="server" CssClass="token-text"></asp:Label>
                        </div>
                        <div style="width: 85%;">
                            <asp:Image ImageUrl="~/Resources/money.png" runat="server" Height="50%" Width="50%" />
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