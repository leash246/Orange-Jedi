<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Register.aspx.vb" Inherits="leightoneash.Account.Register" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
<link type="text/css" rel="Stylesheet" href="../Styles/Site.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h3>
        <font face="Verdana">Registration Page</font>
    </h3>
    <asp:Panel ID="Panel1" runat="server" GroupingText="Account Information" Width="510px" DefaultButton="cmdRegister" >
        <div>
            <span style="text-align:right;"><asp:label runat="server" Width="125px">Name:</asp:label></span>
            <span><asp:TextBox id="txtFName" type="text" runat="server" /></span>
            <span><asp:TextBox id="txtMName" type="text" runat="server" width="25px" /></span>
            <span><asp:TextBox id="txtLName" type="text" runat="server" /></span>
        </div>
        <div>
            <span style="text-align:right;"><asp:label runat="server" Width="125px">Email:</asp:label></span>
            <span><asp:TextBox id="txtEmail" type="text" runat="server" AutoComplete="off" AutoCompleteType="Disabled" width="300px"/>
                <ASP:RequiredFieldValidator ControlToValidate="txtEmail" Display="Static" 
                    ErrorMessage="Email is required" runat="server" ID="vUserName" Width="10px" />
            </span>
        </div>
        <div>
            <span style="text-align:right;"><asp:label runat="server" Width="125px">Confirm Email:</asp:label></span>
            <span><asp:TextBox id="txtEmail2" type="text" runat="server" AutoComplete="off" AutoCompleteType="Disabled" width="300px" />
                <asp:CompareValidator ID="CompareEmail" runat="server" ControlToCompare="txtEmail" ControlToValidate="txtEmail2"
                    CssClass="failureNotification" Display="Dynamic" ErrorMessage="The Confirm Email must match the Email entry."
                    ValidationGroup="EmailValidationGroup" Width="10px">*</asp:CompareValidator>
            </span>
        </div>
        <div>
            <span style="text-align:right;"><asp:label runat="server" Width="125px">Password:</asp:label></span>
            <span><asp:TextBox id="txtUserPass" type="password" runat="server" AutoComplete="off" AutoCompleteType="Disabled" width="300px" />
                <ASP:RequiredFieldValidator ControlToValidate="txtUserPass" Display="Static" 
                    ErrorMessage="Password is required" runat="server" ID="vUserPass" Width="10px" />
            </span>
        </div>
        <div>
            <span style="text-align:right;"><asp:label runat="server" Width="125px">Confirm Password:</asp:label></span>
            <span><asp:TextBox id="txtUserPass2" type="password" runat="server" AutoComplete="off" AutoCompleteType="Disabled" width="300px" />
                <asp:CompareValidator ID="ComparePassword" runat="server" ControlToCompare="txtUserPass" ControlToValidate="txtUserPass2"
                    CssClass="failureNotification" Display="Dynamic" ErrorMessage="The Confirm Password must match the Password entry."
                    ValidationGroup="PasswordValidationGroup" Width="10px">*</asp:CompareValidator>
            </span>
        </div>
    </asp:Panel>
    <asp:Button type="submit" Text="Register" runat="server" ID="cmdRegister" /><p />
    <asp:Label id="lblMsg" ForeColor="red" Font-Name="Verdana" Font-Size="10" runat="server" />
</asp:Content>