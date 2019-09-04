<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="QuestionSituation.aspx.cs" Inherits="CalculusObject.WebPage.QuestionSituation" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .auto-style5 {
            width: 119px;
        }
        .auto-style6 {
            width: 119px;
            height: 21px;
        }
        .auto-style7 {
            height: 21px;
        }
        .auto-style8 {
            height: 21px;
            width: 302px;
        }
        .auto-style9 {
            width: 302px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table class="auto-style1">
        <tr>
            <td class="auto-style6">選擇考試:</td>
            <td class="auto-style8">
                <asp:SqlDataSource ID="SqlDataSource_test" runat="server" ConnectionString="<%$ ConnectionStrings:CCMATHConnectionString %>" SelectCommand="SELECT [TestContent], [TestID] FROM [TestTimeData] WHERE ([Groups] = @Groups)">
                    <SelectParameters>
                        <asp:SessionParameter DefaultValue="0000000000" Name="Groups" SessionField="CCMATHGroups" Type="String" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </td>
            <td class="auto-style7">&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style5" valign="top">
                <asp:RadioButtonList ID="RadioButtonList2" runat="server" AutoPostBack="True" DataSourceID="SqlDataSource_test" DataTextField="TestContent" DataValueField="TestID" OnSelectedIndexChanged="RadioButtonList2_SelectedIndexChanged">
                </asp:RadioButtonList>
            </td>
            <td class="auto-style9" valign="top">
                <asp:GridView ID="GridView_S" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource_score" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical" OnPreRender="GridView_S_PreRender" Width="300px">
                    <AlternatingRowStyle BackColor="#CCCCCC" />
                    <Columns>
                        <asp:BoundField DataField="StudentID" HeaderText="學號" SortExpression="StudentID" />
                        <asp:BoundField DataField="StudentName" HeaderText="姓名" SortExpression="StudentName" />
                        <asp:BoundField DataField="TestContent" HeaderText="考試項目" SortExpression="TestContent" />
                        <asp:BoundField DataField="Score" HeaderText="成績" SortExpression="Score" />
                    </Columns>
                    <FooterStyle BackColor="#CCCCCC" />
                    <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                    <SortedAscendingHeaderStyle BackColor="Gray" />
                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                    <SortedDescendingHeaderStyle BackColor="#383838" />
                </asp:GridView>
                <asp:SqlDataSource ID="SqlDataSource_score" runat="server" ConnectionString="<%$ ConnectionStrings:CCMATHConnectionString %>" SelectCommand="SELECT [StudentID], [StudentName] ,[TestContent], [Score] FROM [ExamsList] WHERE ([TestID] = @TestID)">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="RadioButtonList2" DefaultValue="0000000000" Name="TestID" PropertyName="SelectedValue" Type="String" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </td>
            <td valign="top">
                <asp:Button ID="Button_out" runat="server" Text="輸出成績" Width="117px" />
            </td>
        </tr>
    </table>
</asp:Content>
