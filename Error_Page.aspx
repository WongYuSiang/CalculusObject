<%@ Page Title="錯誤資訊" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Error_Page.aspx.cs" Inherits="CalculusObject.WebPage.error_page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <p>
        錯誤網頁<br />
        <asp:Label ID="Label_ERROR" runat="server" 
            style="font-weight: 700; color: #FF0000" Text="目前找不到你適合的題目"></asp:Label>
        <br />
        <asp:Button ID="Button_RestartTest" runat="server" 
            onclick="Button_RestartTest_Click" Text="重新選題目" />
    </p>
</asp:Content>
