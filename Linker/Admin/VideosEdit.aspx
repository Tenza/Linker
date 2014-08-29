<%@ Page Title="" Language="C#" MasterPageFile="~/Linker.Master" AutoEventWireup="true" CodeBehind="VideosEdit.aspx.cs" Inherits="Linker.Admin.VideosEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">

    <asp:Label ID="message" runat="server" Text=""></asp:Label><br />

    <% if (querystring == null)
       { %>

    <center>
        <asp:ListView ID="ListView1" runat="server">
            <ItemTemplate>
                <tr id="Tr2" runat="server">
                    <td style="padding: 10px;">
                        <asp:TextBox ID="lbl_username" 
                            runat="server" 
                            Text='<%# Eval("username") %>' 
                            ReadOnly="True" 
                            TextMode="MultiLine" 
                            BorderStyle="None" 
                            BackColor="Transparent" 
                            Style="width: 100px; max-width: 100px; color: White; overflow: hidden;">
                        </asp:TextBox>
                    </td>
                    <td style="padding: 10px;">
                        <asp:TextBox ID="lbl_link" 
                            runat="server" 
                            Text='<%# Eval("link") %>' 
                            ReadOnly="True" 
                            TextMode="MultiLine" 
                            BorderStyle="None"
                            BackColor="Transparent" 
                            Style="width: 100px; max-width: 100px; color: White; overflow: hidden;">
                        </asp:TextBox>
                    </td>
                    <td style="padding: 10px;">
                        <asp:TextBox ID="lbl_description" 
                            runat="server" 
                            Text='<%# Eval("description") %>' 
                            ReadOnly="True" 
                            TextMode="MultiLine" 
                            BorderStyle="None" 
                            BackColor="Transparent" 
                            Style="width: 150px; max-width: 150px; color: White; overflow: hidden;">
                        </asp:TextBox>
                    </td>
                    <td style="padding: 10px;">
                        <asp:TextBox ID="lbl_permission" 
                            runat="server" 
                            Text='<%# Eval("permission") %>' 
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
                        <asp:TextBox ID="lbl_visits" 
                            runat="server" 
                            Text='<%# Eval("visits") %>' 
                            ReadOnly="True" 
                            TextMode="MultiLine" 
                            BorderStyle="None" 
                            BackColor="Transparent" 
                            Style="width: 100px; max-width: 100px; color: White; overflow: hidden;">
                        </asp:TextBox>
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
                            OnCommand="delete_image" 
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
                        <th id="Th2" runat="server" style="padding: 10px;">Link</th>
                        <th id="Th3" runat="server" style="padding: 10px;">Description</th>
                        <th id="Th9" runat="server" style="padding: 10px;">Permission</th>
                        <th id="Th10" runat="server" style="padding: 10px;">Date</th>
                        <th id="Th4" runat="server" style="padding: 10px;">Visits</th>
                        <th id="Th5" runat="server" style="padding: 10px;">Edit</th>
                        <th id="Th6" runat="server" style="padding: 10px;">Delete</th>
                    </tr>
                    <tr id="itemPlaceholder" runat="server"></tr>
                </table>
            </LayoutTemplate>
        </asp:ListView>
    </center>

    <%  }
       else
       { %>

    <%-- EDIT functionality missing--%>

    <%  } %>
</asp:Content>
