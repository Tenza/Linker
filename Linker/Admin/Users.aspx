<%@ Page Title="" Language="C#" MasterPageFile="~/Linker.Master" AutoEventWireup="true" CodeBehind="Users.aspx.cs" Inherits="Linker.Admin.Users" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" language="javascript">
        function validate()
        {
            var v_email = document.getElementById("txt_email").value;
            var v_name = document.getElementById("txt_name").value;
            var v_visits = document.getElementById("txt_visits").value;
            var error = "";

            if (v_email == "" || v_name == "" || v_visits == "")
            {
                alert("All the fields are required.");
                return false;
            }

            var email_pattern = /^[a-zA-Z0-9_.-]+\@[a-zA-Z0-9-]+\.[a-zA-Z0-9]{2,4}$/;
            if (!email_pattern.test(v_email))
            {
                error += "The email is not valid.\n";
            }

            if (isNaN(v_visits))
            {
                error += "Visits has to be a number.\n";
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
        <asp:Label ID="message" runat="server" Text=""></asp:Label>
    </center>

    <% if (querystring == null)
       { %>

    <center>
        <asp:ListView ID="ListView1" runat="server">
            <ItemTemplate>
                <tr id="Tr2" runat="server">
                    <td style="padding: 10px;">
                        <asp:TextBox ID="txt_username"
                            runat="server"
                            Text='<%# Eval("username") %>'
                            ReadOnly="True"
                            TextMode="MultiLine"
                            BorderStyle="None"
                            BackColor="Transparent" Style="width: 100px; max-width: 100px; color: White; overflow: hidden;"></asp:TextBox>
                    </td>
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
                        <asp:TextBox ID="txt_name"
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
                        <asp:Label ID="lbl_status"
                            runat="server"
                            Text='<%# Eval("status") %>' />
                    </td>
                    <td style="padding: 10px;">
                        <asp:Label ID="lbl_premission"
                            runat="server"
                            Text='<%# Eval("permission") %>' />
                    </td>
                    <td style="padding: 10px;">
                        <asp:Label ID="lbl_visits"
                            runat="server"
                            Text='<%# Eval("visits") %>' />
                    </td>
                    <td style="padding: 10px;">
                        <asp:Label ID="lbl_role"
                            runat="server"
                            Text='<%# get_role(Eval("username")) %>' />
                    </td>
                    <td style="padding: 10px;">
                        <asp:ImageButton ID="btn_promote"
                            ImageUrl="../images/promote.png"
                            runat="server"
                            OnCommand="promote_user"
                            CommandArgument='<%# Eval("username") %>'
                            Text="Promote" AutoPostBack="True" />
                    </td>
                    <td style="padding: 10px;">
                        <a href="?ID=<%# Eval("id") %>">
                            <img src="../images/edit.png" alt="Edit" />
                        </a>
                    </td>
                    <td style="padding: 10px;">
                        <asp:ImageButton ID="btn_delete"
                            ImageUrl="../images/delete.png"
                            runat="server"
                            OnCommand="delete_user"
                            CommandArgument='<%# Eval("id") + "|" + Eval("status") %>'
                            Text="Delete"
                            AutoPostBack="True" />
                    </td>
                </tr>
            </ItemTemplate>
            <LayoutTemplate>
                <table id="itemPlaceholderContainer" runat="server" border="1" cellspacing="3">
                    <tr id="Tr1" runat="server">
                        <th id="Th1" runat="server" style="padding: 10px;">Username</th>
                        <th id="Th2" runat="server" style="padding: 10px;">Email</th>
                        <th id="Th3" runat="server" style="padding: 10px;">Name</th>
                        <th id="Th4" runat="server" style="padding: 10px;">Status</th>
                        <th id="Th7" runat="server" style="padding: 10px;">Permission</th>
                        <th id="Th5" runat="server" style="padding: 10px;">Visits</th>
                        <th id="Th6" runat="server" style="padding: 10px;">Role</th>
                        <th id="Th8" runat="server" style="padding: 10px;">Promote<br />
                            Demote</th>
                        <th id="Th9" runat="server" style="padding: 10px;">Edit</th>
                        <th id="Th10" runat="server" style="padding: 10px;">Ban<br />
                            Unban</th>
                    </tr>
                    <tr id="itemPlaceholder" runat="server"></tr>
                </table>
            </LayoutTemplate>
        </asp:ListView>
    </center>

    <%  }
       else
       { %>

    <table width="80%" cellspacing="5">
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
            <td align="right">Permission:</td>
            <td style="padding-right: 5px">
                <asp:DropDownList ID="cb_permission" runat="server" Width="101%">
                    <asp:ListItem>Private</asp:ListItem>
                    <asp:ListItem>Public</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td align="right">Visits:</td>
            <td style="padding-right: 5px">
                <asp:TextBox ID="txt_visits"
                    runat="server"
                    Width="100%"
                    ClientIDMode="Static"
                    MaxLength="50">
                </asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>
                <asp:Button ID="btn_edit"
                    runat="server"
                    Text="Edit"
                    OnClientClick="return validate()"
                    OnClick="btn_edit_click"
                    Style="width: 100%; height: 30px; margin-left: -1px;" />
            </td>
        </tr>
    </table>

    <%  } %>
</asp:Content>
