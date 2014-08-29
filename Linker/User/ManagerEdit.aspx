<%@ Page Title="" Language="C#" MasterPageFile="~/Linker.Master" AutoEventWireup="true" CodeBehind="ManagerEdit.aspx.cs" Inherits="Linker.User.ManagerEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" language="javascript">
        function validate()
        {
            var v_description = document.getElementById("txt_description").value;

            if (v_description == "")
            {
                alert("All the fields are required.");
                return false;
            }

            return true;
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">

    <center>
        <% if (qs_sec == "images"){ %>

                <img src='<%= load_img() %>' alt="" style="max-height: 500px; max-width: 500px;" />

        <%  }else if (qs_sec == "videos"){ %>

                <iframe width="600" height="335" src="https://www.youtube-nocookie.com/embed/<%= load_img() %>?rel=0&amp;hd=1" frameborder="0" allowfullscreen></iframe>

        <%  } %>
        <br />
    </center>

    <table width="80%" cellspacing="5">
        <tr>
            <td align="right">Description:</td>
            <td style="padding-right: 5px">
                <asp:TextBox ID="txt_description" 
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
                <asp:DropDownList ID="cb_permission" runat="server" Width="100%">
                    <asp:ListItem>Private</asp:ListItem>
                    <asp:ListItem>Public</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>
                <asp:Button ID="btn_edit" 
                    Style="width: 50%; height: 30px; margin-left: -1px;" 
                    runat="server" 
                    Text="Edit" 
                    OnClientClick="return validate()" 
                    OnClick="btn_edit_click" />
                <asp:Button ID="btn_delete" 
                    Style="width: 50%; height: 30px; margin-left: -1px;" 
                    runat="server" 
                    Text="Delete" 
                    OnClientClick="return validate()" 
                    OnClick="btn_delete_click" />
            </td>
        </tr>
    </table>
</asp:Content>
