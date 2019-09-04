<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="StudentDoSituation.aspx.cs" Inherits="CalculusObject.WebPage.StudentSituation" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .float-style {
            width: 300px;
        }
        .float-style2 {
            width: 300px;
        }
        .auto-style2 {
            width: 100%;
        }
        .auto-style3 {
            margin-bottom: 0px;
        }
        .auto-style4 {
            background-color: #CCFFFF;
            width:405px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table class="auto-style1">
        <tr>
            <td valign="top" class="auto-style4">
                &nbsp;</td>
            <td valign="top" >
                &nbsp;</td>
        </tr>
        <tr>
            <td valign="top" class="auto-style4">
                學生歷程資料</td>
            <td valign="top" >
                &nbsp;</td>
        </tr>
        <tr>
            <td valign="top" class="auto-style4">
                <strong>
                <asp:Label ID="Label1" runat="server" Text="請輸入查詢學號"></asp:Label>
                <br />
                <asp:TextBox ID="TextBox_stID" runat="server" OnTextChanged="TextBox_stID_TextChanged"></asp:TextBox>
                <asp:Button ID="Button_GetID" runat="server" Text="確定" OnClick="Button_GetID_Click" style="height: 27px" />
                <br />
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource_student" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" BackColor="White" BorderColor="#336666" BorderStyle="Double" BorderWidth="3px" CellPadding="4" CssClass="auto-style4" EmptyDataText="沒有找到搜尋學生請重輸入" GridLines="Horizontal" Width="400px">
                    <Columns>
                        <asp:CommandField ButtonType="Button" ShowSelectButton="True" />
                        <asp:BoundField DataField="StudentID" HeaderText="StudentID" SortExpression="StudentID" />
                        <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
                        <asp:BoundField DataField="Class" HeaderText="Class" SortExpression="Class" />
                        <asp:BoundField DataField="Groups" HeaderText="Groups" SortExpression="Groups" />
                    </Columns>
                    <FooterStyle BackColor="White" ForeColor="#333333" />
                    <HeaderStyle BackColor="#336666" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#336666" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="White" ForeColor="#333333" />
                    <SelectedRowStyle BackColor="#339966" Font-Bold="True" ForeColor="White" />
                    <SortedAscendingCellStyle BackColor="#F7F7F7" />
                    <SortedAscendingHeaderStyle BackColor="#487575" />
                    <SortedDescendingCellStyle BackColor="#E5E5E5" />
                    <SortedDescendingHeaderStyle BackColor="#275353" />
                </asp:GridView>
                </strong>
                <asp:SqlDataSource ID="SqlDataSource_student" runat="server" ConnectionString="<%$ ConnectionStrings:CCMATHConnectionString %>" SelectCommand="SELECT [StudentID], [Name], [Class], [Groups] FROM [PersonalInformation]"></asp:SqlDataSource>
            </td>
            <td valign="top" >
                <table class="auto-style2">
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="Label_C" runat="server" Text="題目"></asp:Label>
                            <br />
                            <asp:Image ID="Image_Q" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Image ID="Image_A" runat="server" />
                            <asp:Label ID="Label_C0" runat="server" Text="題目"></asp:Label>
                        </td>
                        <td>
                            <asp:Image ID="Image_B" runat="server" CssClass="auto-style3" />
                            <asp:Label ID="Label_C1" runat="server" Text="題目"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Image ID="Image_C" runat="server" />
                            <asp:Label ID="Label_C2" runat="server" Text="題目"></asp:Label>
                        </td>
                        <td valign="top">
                            <asp:Image ID="Image_D" runat="server" />
                            <asp:Label ID="Label_C3" runat="server" Text="題目"></asp:Label>
                        </td>
                    </tr>
                </table>
                <asp:Label ID="Label2" runat="server" Text="學生歷年作答題目"></asp:Label>
                <br />
                <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource_studentDoing" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" EmptyDataText="此學生目前沒有歷年紀錄" OnSelectedIndexChanged="GridView2_SelectedIndexChanged">
                    <Columns>
                        <asp:CommandField ShowSelectButton="True" />
                        <asp:BoundField DataField="QuestionID" HeaderText="QuestionID" SortExpression="QuestionID" />
                        <asp:BoundField DataField="Difficulty" HeaderText="Difficulty" SortExpression="Difficulty" />
                        <asp:BoundField DataField="SkillsUnit" HeaderText="SkillsUnit" SortExpression="SkillsUnit" />
                        <asp:BoundField DataField="Reply" HeaderText="Reply" SortExpression="Reply" />
                        <asp:BoundField DataField="SaveOption" HeaderText="SaveOption" SortExpression="SaveOption" />
                        <asp:BoundField DataField="TestType" HeaderText="TestType" SortExpression="TestType" />
                        <asp:BoundField DataField="StudentID" HeaderText="StudentID" SortExpression="StudentID" />
                        <asp:BoundField DataField="StartTime" HeaderText="StartTime" SortExpression="StartTime" />
                    </Columns>
                    <FooterStyle BackColor="White" ForeColor="#000066" />
                    <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                    <RowStyle ForeColor="#000066" />
                    <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                    <SortedAscendingHeaderStyle BackColor="#007DBB" />
                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                    <SortedDescendingHeaderStyle BackColor="#00547E" />
                </asp:GridView>
                <asp:SqlDataSource ID="SqlDataSource_studentDoing" runat="server" ConnectionString="<%$ ConnectionStrings:CCMATHConnectionString %>" SelectCommand="SELECT DISTINCT [QuestionID], [Difficulty], [SkillsUnit], [Reply], [SaveOption], [TestType], [StudentID], [StartTime] FROM [FloatData]"></asp:SqlDataSource>
            </td>
        </tr>
        <tr>
            <td valign="top" class="auto-style4">
                &nbsp;</td>
            <td valign="top" >
                &nbsp;</td>
        </tr>
        <tr>
            <td valign="top" class="auto-style4">
                <strong>
                <asp:Label ID="Label_AB" runat="server" Text="能力分析"></asp:Label>
                
                <br />
                
                </strong>
                
            </td>
            <td style="float-style" valign="top">&nbsp;</td>
        </tr>
    </table>
</asp:Content>
