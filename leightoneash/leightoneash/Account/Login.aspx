<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Login.aspx.vb" Inherits="leightoneash.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .style1
        {
            width: 103px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <form>
        <h3>
           <font face="Verdana">Logon Page</font>
        </h3>
        <table>
           <tr>
              <td class="style1">Email:</td>
              <td><asp:TextBox id="txtUserName" type="text" runat="server" /></td>
              <td><ASP:RequiredFieldValidator ControlToValidate="txtUserName"
                   Display="Static" ErrorMessage="*" runat="server" 
                   ID="vUserName" /></td>
           </tr>
           <tr>
              <td class="style1">Password:</td>
              <td><asp:TextBox id="txtUserPass" type="password" runat="server" /></td>
              <td><ASP:RequiredFieldValidator ControlToValidate="txtUserPass"
                  Display="Static" ErrorMessage="*" runat="server" 
                  ID="vUserPass" />
              </td>
           </tr>
           
        </table>
           
        <asp:Button type="submit" Value="Logon" runat="server" ID="cmdLogin" Text="Log In" /><p />
        
        <br />           
                Don't have an account yet? <a href="Register.aspx">Register Now!</a>
                <br />
        <asp:Label id="lblMsg" ForeColor="red" Font-Name="Verdana" Font-Size="10" runat="server" />
    </form>
</asp:Content>
