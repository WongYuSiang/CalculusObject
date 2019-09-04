<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TestQuestion.aspx.cs" Inherits="CalculusObject.WebPage.TestQuestion" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .style2
        {
            border-style: solid;
            border-width: 1px;
            padding: 1px 4px;
        }
        .style3
        {
            width: 511px;
            border-style: solid;
            border-width: 1px;
            padding: 1px 4px;
        }
        .style4
        {
            font-size: large;
        }
        .auto-style5 {
            border-style: solid;
            border-width: 1px;
            padding: 1px 4px;
            
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table class="style1">
        <tr>
            <td colspan="2" class="auto-style5" valign="top">
                <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" 
                    DataSourceID="getTestID" DataTextField="TestContent" DataValueField="TestID" 
                    Height="18px" 
                    onselectedindexchanged="DropDownList1_SelectedIndexChanged" 
                    style="margin-bottom: 0px" Width="606px">
                </asp:DropDownList>
                <asp:SqlDataSource ID="getTestID" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:CCMATHConnectionString %>" 
                    
                    SelectCommand="SELECT [TestID], [TestContent] FROM [TestTimeData] WHERE ([Groups] = @Groups)">
                    <SelectParameters>
                        <asp:SessionParameter DefaultValue="0000000000" Name="Groups" SessionField="CCMATHGroups" Type="String" />
                    </SelectParameters>
                </asp:SqlDataSource>
                <asp:Button ID="Button_text" runat="server" Text="開始搜尋" Width="119px" 
                    onclick="Button_text_Click" />
                <asp:Label ID="Label_group" runat="server" BackColor="#33CCFF" ForeColor="#00CCFF"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style3" valign="top">
                <span class="style4">考卷試題題目:</span><br />
                <br />
                <asp:Panel ID="Panel_onImage" runat="server" 
                    style="border-style: groove; background-color: #FFFFFF" Width="505px">
                </asp:Panel>
            </td>
            <td valign="top">
                <br />
                <span class="style4">全體考卷能力值分析:</span><br />
                <asp:Panel ID="Panel_chart" runat="server">
                </asp:Panel>
                <br />
                <asp:Chart ID="Chart_Score" runat="server" Height="500px" Width="500px">
                    <Series>
                        <asp:Series Name="Series1">
                        </asp:Series>
                    </Series>
                    <ChartAreas>
                        <asp:ChartArea Name="ChartArea1">
                        </asp:ChartArea>
                    </ChartAreas>
                </asp:Chart>
            </td>
        </tr>
        </table>
</asp:Content>
