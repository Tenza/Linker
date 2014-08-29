<%@ Page Title="" Language="C#" MasterPageFile="~/Linker.Master" AutoEventWireup="true" CodeBehind="Manager.aspx.cs" Inherits="Linker.User.Manager" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript" src="highslide/highslide.js"></script>
    <script type="text/javascript" src="highslide/highslide.config.js" charset="utf-8"></script>
    <link rel="stylesheet" type="text/css" href="highslide/highslide.css" />
    <!--[if lt IE 7]>
    <link rel="stylesheet" type="text/css" href="highslide/highslide-ie6.css" />
    <![endif]-->

    <script type="text/javascript" language="javascript">
        function validate()
        {

            var v_option = document.getElementById("txt_option").value;

            if (v_option == "Images")
            {

                var v_link = document.getElementById("txt_link_images").value;
                var v_description = document.getElementById("txt_description_images").value;

                if (v_link == "" || v_description == "")
                {
                    alert("All the fields are required.");
                    return false;
                }

            }
            else if (v_option == "Videos")
            {

                var v_link = document.getElementById("txt_link_videos").value;
                var v_description = document.getElementById("txt_description_videos").value;

                if (v_link == "" || v_description == "")
                {
                    alert("All the fields are required.");
                    return false;
                }
            }

            return true;
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <asp:DropDownList ID="cb_section" 
        runat="server" 
        AutoPostBack="True" 
        OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" 
        onKeyUp="this.blur();">
        <asp:ListItem>Choose a section:</asp:ListItem>
        <asp:ListItem>Images</asp:ListItem>
        <asp:ListItem>Videos</asp:ListItem>
    </asp:DropDownList>
    <asp:HiddenField ID="txt_option" runat="server" ClientIDMode="Static" />

    <br />
    <center>
        <asp:Label ID="message" runat="server" Text=""></asp:Label>
    </center>
    <br />

    <% if (txt_option.Value == "Images") { %>

    <fieldset>
        <legend>&nbsp;&nbsp;&nbsp;Add an Image&nbsp;&nbsp;&nbsp;</legend>
        <table width="80%" cellspacing="5">
            <tr>
                <td align="right" style="width: 180px; min-width: 70px;">Link:</td>
                <td style="padding-right: 5px">
                    <asp:TextBox ID="txt_link_images" 
                        runat="server" 
                        ClientIDMode="Static" 
                        Width="100%" 
                        MaxLength="2500">
                    </asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right">Description:</td>
                <td style="padding-right: 5px">
                    <asp:TextBox ID="txt_description_images" 
                        runat="server" 
                        ClientIDMode="Static" 
                        TextMode="MultiLine" 
                        MaxLength="8000" 
                        Style="min-width: 100%; max-width: 865px; width: 100%; height: 150px;">
                    </asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right">Permission:</td>
                <td style="padding-right: 5px">
                    <asp:DropDownList ID="cb_permission_images" runat="server" Width="100%">
                        <asp:ListItem>Private</asp:ListItem>
                        <asp:ListItem>Public</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>
                    <asp:Button ID="btn_send_images" 
                        Style="width: 100%; height: 30px; margin-left: -1px;" 
                        runat="server" 
                        Text="Send" 
                        OnClientClick="return validate()" 
                        OnClick="btn_send_images_click" />
                </td>
            </tr>
        </table>
    </fieldset>

    <fieldset>
        <legend>&nbsp;&nbsp;&nbsp;Image Manegment&nbsp;&nbsp;&nbsp;</legend>
        <center>
            <%=load_images()%>
        </center>
    </fieldset>

    <%  } else if (txt_option.Value == "Videos") { %>

    <fieldset>
        <legend>&nbsp;&nbsp;&nbsp;Add a Video&nbsp;&nbsp;&nbsp;</legend>
        <table width="80%" cellspacing="5">
            <tr>
                <td align="right" style="width: 180px; min-width: 70px;">YouTube Link:</td>
                <td style="padding-right: 5px">
                    <asp:TextBox ID="txt_link_videos" runat="server" ClientIDMode="Static" Width="100%" MaxLength="2500"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right">Description:</td>
                <td style="padding-right: 5px">
                    <asp:TextBox ID="txt_description_videos" 
                        runat="server" 
                        ClientIDMode="Static" 
                        TextMode="MultiLine" 
                        MaxLength="8000" 
                        Style="min-width: 100%; max-width: 865px; width: 100%; height: 150px;">
                    </asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right">Permission:</td>
                <td style="padding-right: 5px">
                    <asp:DropDownList ID="cb_permission_videos" runat="server" Width="100%">
                        <asp:ListItem>Private</asp:ListItem>
                        <asp:ListItem>Public</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>
                    <asp:Button ID="btn_send_videos" 
                        Style="width: 100%; height: 30px; margin-left: -1px;" 
                        runat="server" 
                        Text="Send" 
                        OnClientClick="return validate()" 
                        OnClick="btn_send_videos_click" />
                </td>
            </tr>
        </table>
    </fieldset>

    <fieldset>
        <legend>&nbsp;&nbsp;&nbsp;Video Manegment&nbsp;&nbsp;&nbsp;</legend>
        <center>
            <%=load_videos()%>
        </center>
    </fieldset>

    <%  } %>
</asp:Content>
