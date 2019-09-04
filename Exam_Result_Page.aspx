<%@ Page Title="測驗結果" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Exam_Result_Page.aspx.cs" Inherits="CalculusObject.WebPage.Exam_Result_Page1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .style1
        {
            font-size: xx-large;
            text-align: center;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <p class="style1">
        <strong>考試已經完畢了<br />
        <asp:Label ID="Label_score" runat="server" Text="分數"></asp:Label>
        <br />
        <asp:Label ID="Label_say" runat="server" Text="分數"></asp:Label>
        </strong>
    </p>
    </asp:Content>
