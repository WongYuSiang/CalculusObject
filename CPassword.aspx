<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CPassword.aspx.cs" Inherits="CalculusObject.Account.CPassword" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">

    .style2
    {
        width: 158px;
    }
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        變更密碼
     
</h2>
<h2>
    <asp:Label ID="Label1" runat="server" Text="使用下面的表單變更您的密碼。 "></asp:Label>
    <br />
    <table class="style1">
        <tr>
            <td class="style2">
                    請輸入舊密碼</td>
            <td>
                <asp:TextBox ID="TextBox_old" runat="server"></asp:TextBox>
            </td>
            <td>
                    &nbsp;</td>
        </tr>
        <tr>
            <td class="style2">
                <table class="style1">
                    <tr>
                        <td>
                                請輸入新密碼</td>
                    </tr>
                </table>
            </td>
            <td>
                <asp:TextBox ID="TextBox_new1" runat="server"></asp:TextBox>
            </td>
            <td>
                    &nbsp;</td>
        </tr>
        <tr>
            <td class="style2">
                    輸入確認新密碼</td>
            <td>
                <asp:TextBox ID="TextBox_new2" runat="server"></asp:TextBox>
            </td>
            <td>
                    &nbsp;</td>
        </tr>
        <tr>
            <td class="style2">
                <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="確認" />
            </td>
            <td>
                <asp:Label ID="Label_ERROR" runat="server" Text="()"></asp:Label>
            </td>
            <td>
                    &nbsp;</td>
        </tr>
        <tr>
            <td class="style2">
                    &nbsp;</td>
            <td>
                    &nbsp;</td>
            <td>
                    &nbsp;</td>
        </tr>
    </table>
</h2>
</asp:Content>
