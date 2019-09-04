<%@ Page Title="考試中.." Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Exam_Page.aspx.cs" Inherits="CalculusObject.webpage.Exam_Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .style4
        {            border-style: groove;
            border-width: 1px;
            padding: 1px 4px;
        }
        .style7
        {
            
            border-right-style: solid;
            border-top-style: solid;
            border-bottom-style: solid;
        }
        .style8
        {
            width : 300px;
            font-weight: 700;
        }
        .style9
        {
            width : 30px;
            border-style: solid;
        }
    .style10
    {
        width : 300px;
        font-weight: 700;
        text-align: left;
        background-color: #CCFFFF;
    }
    .style12
    {
        font-size: large;
        font-weight: bold;
    }
    .style13
    {
        font-size: large;
        font-weight: bold;
    }
        .auto-style6 {
            border-style: groove;
            border-width: 1px;
            padding: 1px 4px;
            font-size: large;
            font-weight: bold;
        }
        .auto-style7 {
            color: #CCFFFF;
        }
        .auto-style8 {
            border-right-style: solid;
            border-top-style: solid;
            border-bottom-style: solid;
            background-color: #FFFFFF;
        }
        .auto-style9 {
            width : 30px;
            border-style: solid;
            background-color: #FFFFFF;
        }
        .auto-style10 {
            width : 30px;
            border-style: solid;
            height: 33px;
        }
        .auto-style11 {
            border-right-style: solid;
            border-top-style: solid;
            border-bottom-style: solid;
            height: 33px;
        }
        .auto-style13 {
            border-style: groove;
            border-width: 1px;
            padding: 1px 4px;
            background-color: #FFFFFF;
        }
        .auto-style16 {
            width: 30px;
            font-weight: 700;
            text-align: left;
            background-color: #CCFFFF;
        }
        .auto-style17 {
            font-weight: 700;
            height: 33px;
            width:100%
        }
        .auto-style18 {
            font-weight: 700;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Panel ID="Panel1" runat="server" Width="920px">
        <strong>
        <br />
        <asp:Panel ID="Panel_button" runat="server" OnPreRender="Panel_button_PreRender" OnInit="Panel_button_Init">
           
        </asp:Panel>
        </strong>
    </asp:Panel>
    <table class="style1">
        <tr>
            <td class="style8">
                <br />
            </td>
        </tr>
    </table>
    <table class="style1" align="center">
        <tr>
            <td class="auto-style13" valign="top" colspan="6">
                <asp:Label ID="Label_number" runat="server" EnableTheming="True" Height="29px" 
                    Text="Label_number" Width="660px" 
                    style="font-weight: 700; font-size: x-large"></asp:Label>
                <br />
                <asp:Image ID="Image_Ques" runat="server" OnPreRender="Image_Ques_PreRender" />
            </td>
        </tr>
        <tr>
            <td class="auto-style10" valign="top">
                </td>
            <td class="auto-style11">
                </td>
            <td rowspan="5">
            </td>
            <td class="auto-style10" valign="top">
                </td>
            <td class="auto-style11">
                </td>
            <td class="auto-style17" colspan="2">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style9" valign="top">
                <asp:RadioButton ID="RadioButton_Option1" runat="server" GroupName="Option" 
                    oncheckedchanged="RadioButton_Option1_CheckedChanged" />
            </td>
            <td class="auto-style8">
                <asp:Image ID="Image_Option1" runat="server" />
            </td>
            <td class="auto-style9" valign="top">
                <asp:RadioButton ID="RadioButton_Option2" runat="server" GroupName="Option" 
                    oncheckedchanged="RadioButton_Option1_CheckedChanged" />
            </td>
            <td class="auto-style8">
                <asp:Image ID="Image_Option2" runat="server" />
            </td>
            <td class="auto-style17" colspan="2">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style9">
                <asp:RadioButton ID="RadioButton_Option3" runat="server" GroupName="Option" 
                    oncheckedchanged="RadioButton_Option1_CheckedChanged" />
            </td>
            <td class="auto-style8">
                <asp:Image ID="Image_Option3" runat="server" />
            </td>
            <td class="auto-style9" valign="top">
                <asp:RadioButton ID="RadioButton_Option4" runat="server" GroupName="Option" 
                    oncheckedchanged="RadioButton_Option1_CheckedChanged" />
            </td>
            <td class="auto-style8">
                <asp:Image ID="Image_Option4" runat="server" />
            </td>
            <td class="auto-style17" colspan="2">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style8" colspan="2">
                &nbsp;</td>
            <td class="style8" valign="top" colspan="2">
                &nbsp;</td>
            <td class="auto-style18" valign="top" colspan="2">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style10" colspan="2">
                <strong>
        <asp:Button ID="Button_Next" runat="server" Text="下一題" 
            onclick="Button_Next_Click" CssClass="auto-style6" />
                </strong>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <strong>
        <asp:Button ID="Button_before" runat="server" Text="上一題" 
            onclick="Button_before_Click" CssClass="auto-style6" />
                </strong>
            </td>
            <td class="auto-style16" valign="top">
                <asp:Label ID="Label_CSID" runat="server" Text="Label" CssClass="auto-style7"></asp:Label>
                <asp:Label ID="Label_CTID" runat="server" Text="Label" CssClass="auto-style7"></asp:Label>
                <asp:Label ID="Label_Count" runat="server" Text="Label_count" CssClass="auto-style7"></asp:Label>
            </td>
            <td class="style10" valign="top">
                &nbsp;</td>
            <td class="style10" valign="top">
                &nbsp;</td>
            <td class="style8">
        <asp:Button ID="Button_Finish" runat="server" Text="完成作答" 
            onclick="Button_Finish_Click" CssClass="style13" />
            </td>
        </tr>
        </table>
    </asp:Content>
