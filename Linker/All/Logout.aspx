<%@ Page Title="" Language="C#" MasterPageFile="~/Linker.Master" AutoEventWireup="true" CodeBehind="Logout.aspx.cs" Inherits="Linker.All.Logout" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta http-equiv="refresh" content="1; url=/All/Home.aspx" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <center>
        <span style="font-family:Courier New, Arial;  font-size:large">We hope to see you soon, 
            <asp:Label ID="username" runat="server" Text=""></asp:Label> 
        </span>
    </center>
</asp:Content>
