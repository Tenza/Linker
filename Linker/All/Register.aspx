<%@ Page Title="" Language="C#" MasterPageFile="~/Linker.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="Linker.All.Register" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" language="javascript">
        function validate()
        {
            var v_username = document.getElementById("txt_username").value;
            var v_email = document.getElementById("txt_email").value;
            var v_password1 = document.getElementById("txt_password1").value;
            var v_password2 = document.getElementById("txt_password2").value;
            var v_name = document.getElementById("txt_name").value;
            var error = "";

            if (v_username == "" || v_email == "" || v_password1 == "" || v_password2 == "" || v_name == "")
            {
                alert("All the fields are required.");
                return false;
            }

            var email_pattern = /^[a-zA-Z0-9_.-]+\@[a-zA-Z0-9-]+\.[a-zA-Z0-9]{2,4}$/;
            if (!email_pattern.test(v_email))
            {
                error += "The email is not valid.\n";
            }

            if (v_password1 != v_password2)
            {
                error += "The passwords don't match.\n";
            }

            if (error != "")
            {
                alert(error);
                return false;
            }

            return true;
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <center>
        <asp:Label ID="message" runat="server" Text=""></asp:Label></center>
    <table width="80%" cellspacing="5">
        <tr>
            <td align="right">Username:</td>
            <td style="padding-right: 5px">
                <asp:TextBox ID="txt_username" 
                    runat="server"
                    Width="100%" 
                    ClientIDMode="Static" 
                    MaxLength="50">
                </asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right">Email:</td>
            <td style="padding-right: 5px">
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
            <td style="padding-right: 5px">
                <asp:TextBox ID="txt_name" 
                    runat="server" 
                    Width="100%" 
                    ClientIDMode="Static" 
                    MaxLength="100">
                </asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right">Password:</td>
            <td style="padding-right: 5px">
                <asp:TextBox ID="txt_password1" 
                    runat="server" 
                    Width="100%" 
                    ClientIDMode="Static" 
                    MaxLength="50" 
                    TextMode="Password">
                </asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right">Password confirmation:</td>
            <td style="padding-right: 5px">
                <asp:TextBox ID="txt_password2" 
                    runat="server" 
                    Width="100%" 
                    ClientIDMode="Static" 
                    MaxLength="50" 
                    TextMode="Password"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>
                <asp:Button ID="btn_register"     
                    runat="server" 
                    Text="Register" 
                    OnClientClick="return validate()" 
                    OnClick="btn_register_click" 
                    Style="width: 100%; height: 30px; margin-left: -1px;" />
            </td>
        </tr>
    </table>
</asp:Content>
