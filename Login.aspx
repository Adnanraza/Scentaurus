<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<!DOCTYPE html>
<html>
<head>
    <meta charset="UTF-8">
    <title>Edu-MS - Login</title>

    <base target="_top" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no" />
    <%--
    

    <script src="assets/Login/js/prefixfree.min.js"></script>
    --%>
    <link href="assets/Login/css/style.css" rel="stylesheet" />
    <script src="dist/sweetalert-dev.js"></script>
    <link rel="stylesheet" type="text/css" href="dist/sweetalert.css">
</head>

<body>
    <form id="frm" runat="server">
        <br>
        <div class="login">
            

            <asp:TextBox ID="txtUserName" runat="server" placeholder="Username"></asp:TextBox>

            <asp:RequiredFieldValidator ID="rfv_txtUserName" CssClass="required" runat="server"
                ControlToValidate="txtUserName"
                ErrorMessage="Required" ValidationGroup="Login" ForeColor=""></asp:RequiredFieldValidator>
            <br />
            <asp:TextBox ID="txtPassword" runat="server" placeholder="Password" TextMode="Password"></asp:TextBox>

            <asp:RequiredFieldValidator ID="rfv_txtPassword" CssClass="required" runat="server"
                ControlToValidate="txtPassword"
                ErrorMessage="Required" ValidationGroup="Login" ForeColor=""></asp:RequiredFieldValidator>

            <br />
            <asp:Button ID="btnLogin" runat="server" ValidationGroup="Login" OnClick="Login_Click" Text="Login" />



            <%--        <input type="text" placeholder="username" name="user"><br>
        <input type="password" placeholder="password" name="password"><br>
        <input type="button" value="Login">--%>
        </div>
        <script src='http://cdnjs.cloudflare.com/ajax/libs/jquery/2.1.3/jquery.min.js'></script>

    </form>



</body>
</html>
