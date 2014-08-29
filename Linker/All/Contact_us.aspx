<%@ Page Title="" Language="C#" MasterPageFile="~/Linker.Master" AutoEventWireup="true" CodeBehind="Contact_us.aspx.cs" Inherits="Linker.All.Contact_us" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" language="javascript">
        function validate()
        {
            var v_name = document.getElementById("txt_name").value;
            var v_email = document.getElementById("txt_email").value;
            var v_comment = document.getElementById("txt_comment").value;

            var error = "";

            if (v_name == "" || v_email == "" || v_comment == "")
            {
                alert("All the fields are required.");
                return false;
            }

            var email_pattern = /^[a-zA-Z0-9_.-]+\@[a-zA-Z0-9-]+\.[a-zA-Z0-9]{2,4}$/;
            if (!email_pattern.test(v_email))
            {
                alert("The email is not valid.");
                return false;
            }

            return true;
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <center>
        <asp:Label ID="message" runat="server" Text=""></asp:Label>
    </center>

    <table width="80%" cellspacing="5">
        <tr>
            <td align="right" style="width: 170px;">Section:</td>
            <td style="width: 70px;">
                <asp:DropDownList ID="cb_section" 
                    runat="server" 
                    AutoPostBack="True" 
                    OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" 
                    onKeyUp="this.blur();">
                    <asp:ListItem>Help</asp:ListItem>
                    <asp:ListItem>Critics</asp:ListItem>
                    <asp:ListItem>Other:</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td style="min-width: 500px;">
                <asp:TextBox ID="txt_section" 
                    runat="server" 
                    Width="100%" 
                    ClientIDMode="Static" 
                    MaxLength="50" 
                    Text="Help" 
                    Style="margin-left: -5px;" 
                    Visible="False">
                </asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right">Email:</td>
            <td style="padding-right: 5px" colspan="2">
                <asp:TextBox ID="txt_email" 
                    runat="server" 
                    Width="100%" 
                    ClientIDMode="Static" 
                    MaxLength="254">
                </asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right">Name:</td>
            <td style="padding-right: 5px" colspan="2">
                <asp:TextBox ID="txt_name" 
                    runat="server" 
                    Width="100%" 
                    ClientIDMode="Static" 
                    MaxLength="100">
                </asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right">Comment:</td>
            <td style="padding-right: 5px" colspan="2">
                <asp:TextBox ID="txt_comment" 
                    runat="server" 
                    ClientIDMode="Static" 
                    MaxLength="8000" 
                    TextMode="MultiLine"
                    Style="min-width: 100%; max-width: 900px; width: 100%; height: 200px; margin-left: 1px;" >
                </asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td colspan="2">
                <asp:Button ID="btn_send" 
                    ClientIDMode="Static" 
                    runat="server" 
                    Text="Send" 
                    OnClientClick="return validate()" 
                    OnClick="btn_send_Click" 
                    Style="width: 100%; height: 30px; margin-left: -1px;" />
            </td>
        </tr>
    </table>
</asp:Content>
