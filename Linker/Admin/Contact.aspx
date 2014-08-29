<%@ Page Title="" Language="C#" MasterPageFile="~/Linker.Master" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="Linker.Admin.Contact" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" language="javascript">
        function validate()
        {
            var v_account = document.getElementById("txt_account").value;
            var v_password = document.getElementById("txt_password").value;
            var v_to = document.getElementById("txt_to").value;
            var v_subject = document.getElementById("txt_subject").value;
            var v_message = document.getElementById("txt_message").value;

            var error = "";

            if (v_account == "" || v_password == "" || v_to == "" || v_subject == "" || v_message == "")
            {
                alert("All the fields are required.");
                return false;
            }

            var email_pattern = /^[a-zA-Z0-9_.-]+\@[a-zA-Z0-9-]+\.[a-zA-Z0-9]{2,4}$/;
            if (!email_pattern.test(v_account))
            {
                error += "Your email is not valid.\n";
            }

            var email_pattern = /^[a-zA-Z0-9_.-]+\@[a-zA-Z0-9-]+\.[a-zA-Z0-9]{2,4}$/;
            if (!email_pattern.test(v_to))
            {
                error += "Destination email is not valid.\n";
            }

            if (v_account.indexOf("@gmail.com") == -1)
            {
                error += "Only supports Gmail accounts.\n";
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
    <% if (!querystring)
       { %>
    <center>
        <asp:ListView ID="ListView1" runat="server">
            <ItemTemplate>
                <tr id="Tr2" runat="server">
                    <td style="padding: 10px;">
                        <asp:TextBox ID="txt_email"
                            runat="server"
                            Text='<%# Eval("email") %>'
                            ReadOnly="True"
                            TextMode="MultiLine"
                            BorderStyle="None"
                            BackColor="Transparent"
                            Style="width: 100px; max-width: 100px; color: White; overflow: hidden;">
                        </asp:TextBox>
                    </td>
                    <td style="padding: 10px;">
                        <asp:TextBox ID="lbl_name"
                            runat="server"
                            Text='<%# Eval("name") %>'
                            ReadOnly="True"
                            TextMode="MultiLine"
                            BorderStyle="None"
                            BackColor="Transparent"
                            Style="width: 100px; max-width: 100px; color: White; overflow: hidden;">
                        </asp:TextBox>
                    </td>
                    <td style="padding: 10px;">
                        <asp:TextBox ID="txt_comment"
                            runat="server"
                            ClientIDMode="Static"
                            MaxLength="8000"
                            Style="width: 300px; max-width: 300px; height: 100px;"
                            TextMode="MultiLine"
                            Text='<%# Eval("comment") %>'>
                        </asp:TextBox>
                    </td>
                    <td style="padding: 10px;">
                        <asp:TextBox ID="lbl_section"
                            runat="server"
                            Text='<%# Eval("section") %>'
                            ReadOnly="True"
                            TextMode="MultiLine"
                            BorderStyle="None"
                            BackColor="Transparent"
                            Style="width: 100px; max-width: 100px; color: White; overflow: hidden;">
                        </asp:TextBox>
                    </td>
                    <td style="padding: 10px;">
                        <asp:Label ID="lbl_date"
                            runat="server"
                            Text='<%# Eval("date") %>'
                            Width="65px" />
                    </td>
                    <td style="padding: 10px;">
                        <a href="/Admin/Contact.aspx?email=<%# Eval("email") %>&section=<%# Eval("section") %>">
                            <img src="../images/email.png" alt="Edit" />
                        </a>
                    </td>
                    <td style="padding: 10px;">
                        <asp:ImageButton ID="btn_delete"
                            ImageUrl="../images/delete.png"
                            runat="server"
                            OnCommand="delete_item"
                            CommandArgument='<%# Eval("id") %>'
                            Text="Delete"
                            AutoPostBack="True" />
                    </td>
                </tr>
            </ItemTemplate>
            <LayoutTemplate>
                <table id="itemPlaceholderContainer" runat="server" border="1" cellspacing="3">
                    <tr id="Tr1" runat="server">
                        <th id="Th2" runat="server" style="padding: 10px;">Email</th>
                        <th id="Th3" runat="server" style="padding: 10px;">Name</th>
                        <th id="Th4" runat="server" style="padding: 10px;">Comment</th>
                        <th id="Th7" runat="server" style="padding: 10px;">Section</th>
                        <th id="Th5" runat="server" style="padding: 10px;">Date</th>
                        <th id="Th9" runat="server" style="padding: 10px;">Reply</th>
                        <th id="Th10" runat="server" style="padding: 10px;">Delete</th>
                    </tr>
                    <tr id="itemPlaceholder" runat="server"></tr>
                </table>
            </LayoutTemplate>
        </asp:ListView>
    </center>

    <%  }
       else
       { %>

    <center>
        <asp:Label ID="message" runat="server" Text=""></asp:Label></center>
    <table width="80%" cellspacing="5">
        <tr>
            <td align="right" style="width: 170px; min-width: 80px;">Gmail account:</td>
            <td style="padding-right: 5px">
                <asp:TextBox ID="txt_account"
                    runat="server"
                    ClientIDMode="Static"
                    Width="100%"
                    MaxLength="254">
                </asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right">Password:</td>
            <td style="padding-right: 5px">
                <asp:TextBox ID="txt_password"
                    runat="server"
                    ClientIDMode="Static"
                    TextMode="Password"
                    Width="100%"
                    MaxLength="50">
                </asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right">To:</td>
            <td style="padding-right: 5px">
                <asp:TextBox ID="txt_to"
                    runat="server"
                    ClientIDMode="Static"
                    Width="100%"
                    MaxLength="254">
                </asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right">Subject:</td>
            <td style="padding-right: 5px">
                <asp:TextBox ID="txt_subject"
                    runat="server"
                    ClientIDMode="Static"
                    Width="100%"
                    MaxLength="50">
                </asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right">Message:</td>
            <td style="padding-right: 3px">
                <asp:TextBox ID="txt_message"
                    runat="server"
                    ClientIDMode="Static"
                    TextMode="MultiLine"
                    MaxLength="8000"
                    Style="min-width: 100%; max-width: 865px; width: 100%; height: 200px;">
                </asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>
                <asp:Button ID="btn_send"
                    Style="width: 100%; height: 30px; margin-left: -1px;"
                    runat="server"
                    Text="Send"
                    OnClientClick="return validate()"
                    OnClick="btn_send_click" />
            </td>
        </tr>
    </table>
    <%  } %>
</asp:Content>
