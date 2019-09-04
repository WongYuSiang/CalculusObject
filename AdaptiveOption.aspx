<%@ Page Title="適性化練習" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdaptiveOption.aspx.cs" Inherits="CalculusObject.WebPage.AdaptiveOption" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .style3
        {
            width: 100%;
        }
        .style4
        {
            background-color: #FFFFFF;
            }
        .style5
        {
            border-style: groove;
            width: 101px;
            color: #333333;
            background-color: #CCFFFF;
        }
        .style7
        {
            border-style: groove;
            background-color: #FFFFCC;
        }
        .style9
        {
            color: #333333;
            background-color: #CCFFFF;
            }
        .style10
        {
            color: #000000;
            background-color: #FFFFCC;
        }
        .style11
        {
            font-size: x-large;
        }
        .style12
        {
            color: #000000;
            background-color: #FFFFCC;
            width: 154px;
        }
        .style13
        {
            background-color: #FFFFFF;
            width: 154px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="Label1" runat="server" Text="請選擇想要練習的章節以及章節落點" 
        style="font-weight: 700; font-size: large"></asp:Label>
<br />
    <table class="style3">
        <tr>
            <td class="style9">
                <strong>
<asp:Label ID="Label2" runat="server" Text="★章節" CssClass="style11"></asp:Label>
                </strong>
            </td>
            <td class="style10">
                <br />
                <strong>
<asp:Label ID="Label3" runat="server" Text="★章節學習方針" CssClass="style11"></asp:Label>
                <br />
<asp:Label ID="Label_Nodata" runat="server" Text="請先選擇章節" style="color: #FF0000"></asp:Label>
                </strong>
                <br />
            </td>
            <td class="style12">
                <asp:Button ID="Button_Enter" runat="server" onclick="Button_Enter_Click" 
                    Text="開始考試" style="font-size: medium" Height="53px" Width="148px" />
            </td>
            <td class="style10" rowspan="3">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style5" valign="top">
                <asp:Button ID="Button_all" runat="server" onclick="Button_all_Click" 
                    Text="全部選取" style="font-size: medium" Width="138px" />
<asp:CheckBoxList ID="CheckBoxList1" runat="server" DataSourceID="SqlDataSource1" 
                    DataTextField="Chapter" DataValueField="Chapter" 
                    style="background-color: #CCFFFF; font-size: large;" CssClass="bold" 
                    onselectedindexchanged="CheckBoxList1_SelectedIndexChanged" 
                    AutoPostBack="True" Width="138px">
    <asp:ListItem>全部</asp:ListItem>
</asp:CheckBoxList>
            </td>
            <td class="style7" colspan="2" valign="top">
                <strong>
                <asp:Button ID="Button_all_AllAbility" runat="server" 
                    onclick="Button_all_AllAbility_Click" Text="全部選取" Width="174px" 
                    style="font-size: medium" />
                </strong>
                <asp:CheckBoxList ID="CheckBoxList_AllAbility" runat="server" 
                    AutoPostBack="True" 
                    onselectedindexchanged="CheckBoxList_AllAbility_SelectedIndexChanged" 
                    style="background-color: #FFFFCC; font-size: small; font-weight: 700;">
                </asp:CheckBoxList>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
        SelectCommand="SELECT DISTINCT [Chapter] FROM [ExerciseData]"></asp:SqlDataSource>
            </td>
        </tr>
        <tr>
            <td class="style4" style="font-weight: 700; " colspan="2">
    <asp:Label ID="Label_TEXT" runat="server" Text="章節:" 
                    style="color: #000000; font-size: large;"></asp:Label>
            </td>
            <td class="style13" style="font-weight: 700; ">
                &nbsp;</td>
        </tr>
        </table>
    <br />
    <br />
            <br />
            </asp:Content>
