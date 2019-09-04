<%@ Page Title="測驗中.." Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdaptiveTest.aspx.cs" Inherits="CalculusObject.WebPage.AdaptiveTestaspx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">

        .style8
        {
        }
        .style10
        {
            border-style: groove;
        }
        .style1
        {
            width: 100%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table class="style1" align="left" dir="ltr">
        <tr>
            <td class="style10" colspan="3">
                <asp:Image ID="Image_Ques" runat="server" Width="74px" Height="57px" />
            </td>
            <td class="style10" dir="rtl">
                <asp:Button ID="Button_Exit" runat="server" onclick="Button_Exit_Click" 
                    Text="離開測驗" Visible="False" Height="36px" style="font-size: medium" 
                    Width="114px" />
            </td>
        </tr>
        <tr>
            <td class="style10" valign="top">
                <asp:RadioButton ID="RadioButton_Option1" runat="server" GroupName="Option" />
                <asp:Image ID="Image_Option1" runat="server" />
            </td>
            <td class="style10" colspan="2" valign="top">
                <asp:RadioButton ID="RadioButton_Option2" runat="server" GroupName="Option" />
                <asp:Image ID="Image_Option2" runat="server" />
            </td>
            <td style="background-color: #D4D0C8">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style10" valign="top">
                <asp:RadioButton ID="RadioButton_Option3" runat="server" GroupName="Option" />
                <asp:Image ID="Image_Option3" runat="server" />
            </td>
            <td class="style10" colspan="2" valign="top">
                <asp:RadioButton ID="RadioButton_Option4" runat="server" GroupName="Option" />
                <asp:Image ID="Image_Option4" runat="server" />
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style8" colspan="2">
                <asp:Button ID="Button_Check" runat="server" onclick="Button_Check_Click" 
                    Text="確認答案" Height="33px" Width="89px" />
        <asp:Button ID="Button_Next" runat="server" Text="下一題" 
            onclick="Button_Next_Click" Visible="False" Height="33px" Width="98px" />
        <asp:Label ID="Label_text" runat="server" Text="_ " ForeColor="#FF3300" 
                    style="font-size: x-large"></asp:Label>
            </td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
    </asp:Content>
