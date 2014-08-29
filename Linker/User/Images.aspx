<%@ Page Title="" Language="C#" MasterPageFile="~/Linker.Master" AutoEventWireup="true" CodeBehind="Images.aspx.cs" Inherits="Linker.User.Images" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" src="highslide/highslide.js"></script>
    <script type="text/javascript" src="highslide/highslide.config.js" charset="utf-8"></script>
    <link rel="stylesheet" type="text/css" href="highslide/highslide.css" />
    <!--[if lt IE 7]>
    <link rel="stylesheet" type="text/css" href="highslide/highslide-ie6.css" />
    <![endif]-->
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <center>
        <%=load_images()%>
    </center>
</asp:Content>
