<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ErrorReturn.aspx.cs" Inherits="CalculusObject.WebPage.ErrorReturn" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
    .auto-style5 {
        height: 23px;
    }
    .auto-style6 {
        height: 22px;
    }
        .auto-style7 {
            height: 23px;
            width: 72px;
        }
        .auto-style8 {
            width: 72px;
        }
        .auto-style9 {
            height: 22px;
            width: 72px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table class="auto-style1">
    <tr>
        <td class="auto-style7">錯誤回報</td>
        <td class="auto-style5">&nbsp;</td>
    </tr>
    <tr>
        <td class="auto-style7" style="border-style: dotted">
            <asp:Label ID="Label1" runat="server" Text="回報情況"></asp:Label>
        </td>
        <td class="auto-style5" style="border-style: dotted">
            <asp:DropDownList ID="DropDownList_sit" runat="server">
                <asp:ListItem>網頁錯誤</asp:ListItem>
                <asp:ListItem>帳號問題</asp:ListItem>
                <asp:ListItem>資料錯誤</asp:ListItem>
                <asp:ListItem>網頁改善</asp:ListItem>
                <asp:ListItem>其他</asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td class="auto-style9" style="border-style: dotted">
            <asp:Label ID="Label2" runat="server" Text="班級"></asp:Label>
        </td>
        <td class="auto-style6" style="border-style: dotted">
            <asp:TextBox ID="TextBox_class" runat="server"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="auto-style8" style="border-style: dotted">
            <asp:Label ID="Label4" runat="server" Text="學號"></asp:Label>
        </td>
        <td style="border-style: dotted">
            <asp:TextBox ID="TextBox_SID" runat="server"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="auto-style8" style="border-style: dotted">
            <asp:Label ID="Label3" runat="server" Text="姓名"></asp:Label>
        </td>
        <td style="border-style: dotted">
            <asp:TextBox ID="TextBox_Name" runat="server" Width="137px"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="auto-style8" style="border-style: dotted">
            <asp:Label ID="Label5" runat="server" Text="詳細內容"></asp:Label>
        </td>
        <td style="border-style: dotted">
            <asp:TextBox ID="TextBox_text" runat="server" Height="90px" TextMode="MultiLine" Width="253px"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="auto-style8">&nbsp;</td>
        <td>
            <asp:Button ID="Button_Simmit" runat="server" OnClick="Button_Simmit_Click" Text="送出" />
            <asp:Label ID="Label_R" runat="server"></asp:Label>
        </td>
    </tr>
</table>
</asp:Content>
