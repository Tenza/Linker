<%@ Page Title="" Language="C#" MasterPageFile="~/Linker.Master" AutoEventWireup="true" CodeBehind="CommentsEdit.aspx.cs" Inherits="Linker.Admin.CommentsEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" language="javascript">
        function validate()
        {
            var v_comment = document.getElementById("txt_edit_comment").value;

            if (v_comment == "")
            {
                alert("You cannot edit with an empty comment.");
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
                            BackColor="Transparent"
                            Style="width: 150px; max-width: 150px; color: White; overflow: hidden;">
                        </asp:TextBox>
                    </td>
                    <td style="padding: 10px;">
                        <asp:TextBox ID="txt_comment"
                            runat="server"
                            Text='<%# Eval("comment") %>'
                            ReadOnly="True"
                            TextMode="MultiLine"
                            BorderStyle="None"
                            BackColor="Transparent"
                            Style="width: 500px; max-width: 500px; color: White; overflow: hidden;">
                        </asp:TextBox>
                    </td>
                    <td style="padding: 10px;">
                        <asp:Label ID="lbl_date"
                            runat="server"
                            Text='<%# Eval("date") %>'
                            Width="65px" />
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
                            OnCommand="delete_comment"
                            CommandArgument='<%# Eval("id") %>'
                            Text="Delete"
                            AutoPostBack="True" />
                    </td>
                </tr>
            </ItemTemplate>
            <LayoutTemplate>
                <table id="itemPlaceholderContainer" runat="server" border="1" cellspacing="3">
                    <tr id="Tr1" runat="server">
                        <th id="Th1" runat="server" style="padding: 10px;">Username</th>
                        <th id="Th2" runat="server" style="padding: 10px;">Commment</th>
                        <th id="Th3" runat="server" style="padding: 10px;">Date</th>
                        <th id="Th9" runat="server" style="padding: 10px;">Edit</th>
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
        <table width="55%" cellspacing="5">
            <tr>
                <td style="padding-right: 5px">Comment:<br />
                    <asp:TextBox ID="txt_edit_comment"
                        runat="server"
                        ClientIDMode="Static"
                        TextMode="MultiLine"
                        MaxLength="8000"
                        Style="width: 100%; min-width: 100%; max-width: 865px; height: 150px;">
                    </asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btn_edit_comment"
                        runat="server"
                        Text="Edit Comment"
                        OnClientClick="return validate()"
                        OnClick="btn_edit_click"
                        Style="width: 100%; height: 30px; margin-left: -1px;" />
                </td>
            </tr>
        </table>
    </center>
    <%  } %>
</asp:Content>
