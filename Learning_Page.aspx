<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Learning_Page.aspx.cs" Inherits="CalculusObject.WebPage.Learning_Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <p>
        休息一下:來查看你的不會的地方<br />
        <br />
        <asp:Button ID="Button_RestartTest" runat="server" 
            onclick="Button_RestartTest_Click" Text="繼續練習" />
    </p>
</asp:Content>
