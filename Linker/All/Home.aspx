<%@ Page Title="" Language="C#" MasterPageFile="~/Linker.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="Linker.All.Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">

    <center>
        <img src="../images/home.png" alt="Home" /></center>

    <% if (is_logged)
       { %>
    <center>
        <asp:Label ID="message" runat="server"></asp:Label>
    </center>

    <br />
    
    <asp:Login ID="LoginUser" runat="server" EnableViewState="false" RenderOuterTable="false" DestinationPageUrl="~/All/Home.aspx" OnAuthenticate="Login1_Authenticate">
        <LayoutTemplate>
            <asp:ValidationSummary ID="LoginUserValidationSummary" 
                runat="server" 
                ValidationGroup="LoginUserValidationGroup" 
                CssClass="failureNotification" 
                Style="padding: 5px; margin-left: 260px;" />

            <fieldset style="width: 420px; padding: 5px; margin-left: 260px;">
                <legend>&nbsp;&nbsp;&nbsp;Account Information&nbsp;&nbsp;&nbsp;</legend>
                <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName" Style="margin-left: 5px;">Username:</asp:Label>
                <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password" Style="margin-left: 105px;">Password:&nbsp;</asp:Label><br />

                <asp:TextBox ID="UserName" runat="server" MaxLength="50"></asp:TextBox>
                <asp:RequiredFieldValidator ID="UserNameRequired" 
                    runat="server" 
                    ControlToValidate="UserName" 
                    ErrorMessage="UserName is required." 
                    ValidationGroup="LoginUserValidationGroup" 
                    CssClass="failureNotification">*</asp:RequiredFieldValidator>

                <asp:TextBox ID="Password" runat="server" TextMode="Password" MaxLength="50"></asp:TextBox>
                <asp:RequiredFieldValidator ID="PasswordRequired"
                     runat="server" 
                    ControlToValidate="Password" 
                    ErrorMessage="Password is required." 
                    ValidationGroup="LoginUserValidationGroup" 
                    CssClass="failureNotification">*</asp:RequiredFieldValidator>

                <asp:CheckBox ID="RememberMe" runat="server" />
                <asp:Label ID="RememberMeLabel" runat="server" AssociatedControlID="RememberMe">Keep logged.</asp:Label><br />

                <asp:Button ID="LoginButton" 
                    runat="server" 
                    CommandName="Login" 
                    Text="Log In" 
                    ValidationGroup="LoginUserValidationGroup" 
                    Style="height: 25px; width: 100px; margin-left: -1px; margin-top: 3px;" />
                <br />
            </fieldset>

        </LayoutTemplate>
    </asp:Login>
    <%  } %>
</asp:Content>
