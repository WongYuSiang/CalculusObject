<%@ Page Title="編輯考試" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Exam_Option.aspx.cs" Inherits="CalculusObject.WebPage.Exam_Option" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .style3
        {
            width: 100%;
        }
        .style18
        {
            height: 25px;
            width: 860px;
        }
        .style19
        {
            width: 860px;
        }
        .style14
        {
            width: 102px;
            background-color: #FFCCFF;
        }
        .style15
        {
            color: #000000;
            font-weight: bold;
        }
        .style13
        {
            width: 275px;
            background-color: #FFCCFF;
        }
        .style20
        {
            width: 469px;
        }
        .style10
        {
            width: 102px;
            background-color: #66FFFF;
        }
        .style9
        {
            width: 275px;
            background-color: #66FFFF;
        }
        .style12
        {
            width: 102px;
            background-color: #CCFFFF;
        }
        .style11
        {
            width: 275px;
            background-color: #CCFFFF;
        }
        .style17
        {
            width: 102px;
            background-color: #CCFF99;
        }
        .style16
        {
            width: 275px;
            
        }
        .style8
        {
            border-style: solid;
            background-color: #CCFF99;
        }
        .style21
        {
            color: #000000;
        }
        .style22
        {
            width: 102px;
            background-color: #66FFFF;
            height: 57px;
        }
        .style23
        {
            width: 275px;
            background-color: #66FFFF;
            height: 57px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <br />
    <br />
    <br />
    <table class="style3">
        <tr>
            <td class="style18">
                <asp:GridView ID="GridView1" runat="server" AllowPaging="True" 
                    AutoGenerateColumns="False" DataSourceID="SqlDataSource1" 
                    onselectedindexchanged="GridView1_SelectedIndexChanged" 
                    DataKeyNames="TestID" style="color: #000000; font-weight: 700" 
                    BackColor="#DEBA84" BorderColor="#FFCC99" BorderStyle="None" BorderWidth="1px" 
                    CellPadding="3" CellSpacing="2" EmptyDataText="目前沒有考試紀錄" 
                    AllowSorting="True" onrowcommand="GridView1_RowCommand" OnRowCreated="GridView1_RowCreated">
                    <Columns>
                        <asp:CommandField ButtonType="Button" ShowSelectButton="True" />
                        <asp:BoundField DataField="TestID" HeaderText="考試序號" SortExpression="TestID" 
                            ReadOnly="True" />
                        <asp:BoundField DataField="TestContent" HeaderText="考試內容" 
                            SortExpression="TestContent" />
                        <asp:BoundField DataField="Begin_time" HeaderText="開始時間" 
                            SortExpression="Begin_time" />
                        <asp:BoundField DataField="End_time" HeaderText="結束時間" 
                            SortExpression="End_time" />
                        <asp:BoundField DataField="Class" HeaderText="班級" SortExpression="Class" />
                        <asp:BoundField DataField="TeacherID" HeaderText="老師" 
                            SortExpression="TeacherID" />
                        <asp:BoundField DataField="RandCount" HeaderText="隨機題數" 
                            SortExpression="RandCount" />
                        <asp:BoundField DataField="MustCount" HeaderText="必考題數" 
                            SortExpression="MustCount" />
                        <asp:BoundField DataField="TotalCount" HeaderText="總共題數" 
                            SortExpression="TotalCount" />
                        <asp:BoundField DataField="Groups" HeaderText="開課群組" SortExpression="Groups" />
                        <asp:ButtonField ButtonType="Button" Text="複製考試人員" CommandName="copyP" />
                        <asp:ButtonField ButtonType="Button" Text="複製考試題目" CommandName="copyQ" 
                            Visible="False"/>
                    </Columns>
                    <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
                    <HeaderStyle BackColor="#A55129" Font-Bold="True" ForeColor="White" />
                    <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
                    <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
                    <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
                    <SortedAscendingCellStyle BackColor="#FFF1D4" />
                    <SortedAscendingHeaderStyle BackColor="#B95C30" />
                    <SortedDescendingCellStyle BackColor="#F1E5CE" />
                    <SortedDescendingHeaderStyle BackColor="#93451F" />
                </asp:GridView>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
                    
                    
                    
                    
                    SelectCommand="SELECT * FROM [TestTimeData] WHERE ([Groups] = @Groups)">
                    <SelectParameters>
                        <asp:SessionParameter DefaultValue="0000" Name="Groups" SessionField="CCMATHGroups" Type="String" />
                    </SelectParameters>
                </asp:SqlDataSource>
                <asp:Label ID="Label_copy" runat="server" style="color: #000000" 
                    Text="下方選擇新增或刪減"></asp:Label>
                <br />
                <asp:Button ID="Button_new" runat="server" onclick="Button_new_Click" 
                    Text="新增" />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="Button_Delete" runat="server" OnClientClick="return confirm('確定要刪除此筆資料嗎?');" onclick="Button_Delete_Click" 
                    Text="刪除此考試" />
                <br />
                0<table class="style3">
                    <tr>
                        <td class="style14" valign="top">
                            <asp:Label ID="Label_ID" runat="server" Text="考試編號:" Visible="False" 
                                CssClass="style15"></asp:Label>
                        </td>
                        <td class="style13" valign="top">
                            <asp:DropDownList ID="DropDownList2" runat="server" 
                                DataSourceID="SqlDataSource1" DataTextField="TestID" DataValueField="TestID" 
                                Visible="False">
                            </asp:DropDownList>
                        </td>
                        <td rowspan="8" style="text-align: left; font-weight: 700;" class="style20" valign="top">
                                <asp:Label ID="LabelTime" runat="server" style="color: #000000" Text="Label" 
                                    Visible="False"></asp:Label>
                            <asp:Calendar ID="CalendarTime" runat="server" Height="193px" 
                                style="margin-left: 0px" Visible="False"></asp:Calendar>
                            <asp:Button ID="Button_Begin" runat="server" onclick="Button_Begin_Click" 
                                Text="確定" Visible="False" />
                                <br />
                            <asp:DropDownList ID="DropDownListHour" runat="server" Visible="False" 
                                    style="font-weight: 700">
                                <asp:ListItem>00</asp:ListItem>
                                <asp:ListItem>01</asp:ListItem>
                                <asp:ListItem>02</asp:ListItem>
                                <asp:ListItem>03</asp:ListItem>
                                <asp:ListItem>04</asp:ListItem>
                                <asp:ListItem>05</asp:ListItem>
                                <asp:ListItem>06</asp:ListItem>
                                <asp:ListItem>07</asp:ListItem>
                                <asp:ListItem>08</asp:ListItem>
                                <asp:ListItem>09</asp:ListItem>
                                <asp:ListItem>10</asp:ListItem>
                                <asp:ListItem>11</asp:ListItem>
                                <asp:ListItem>12</asp:ListItem>
                                <asp:ListItem>13</asp:ListItem>
                                <asp:ListItem>14</asp:ListItem>
                                <asp:ListItem>15</asp:ListItem>
                                <asp:ListItem>16</asp:ListItem>
                                <asp:ListItem>17</asp:ListItem>
                                <asp:ListItem>18</asp:ListItem>
                                <asp:ListItem>19</asp:ListItem>
                                <asp:ListItem>20</asp:ListItem>
                                <asp:ListItem>21</asp:ListItem>
                                <asp:ListItem>22</asp:ListItem>
                                <asp:ListItem>23</asp:ListItem>
                            </asp:DropDownList>
                            <asp:Label ID="Label_J" runat="server" Text="："></asp:Label>
                            <asp:DropDownList ID="DropDownListMin" runat="server" Visible="False" 
                                    style="font-weight: 700">
                                <asp:ListItem>00</asp:ListItem>
                                <asp:ListItem>05</asp:ListItem>
                                <asp:ListItem>10</asp:ListItem>
                                <asp:ListItem>15</asp:ListItem>
                                <asp:ListItem>20</asp:ListItem>
                                <asp:ListItem>25</asp:ListItem>
                                <asp:ListItem>30</asp:ListItem>
                                <asp:ListItem>35</asp:ListItem>
                                <asp:ListItem>40</asp:ListItem>
                                <asp:ListItem>45</asp:ListItem>
                                <asp:ListItem>50</asp:ListItem>
                                <asp:ListItem>55</asp:ListItem>
                            </asp:DropDownList>
                            <br />
                            <asp:Button ID="Button_End" runat="server" onclick="Button_End_Click" 
                                Text="確定" Visible="False" />
                                <span class="style21">
                                <strong dir="ltr">
                    <br />
                </strong>
                                </span>
                        </td>
                    </tr>
                    <tr>
                        <td class="style14" valign="top">
                            <asp:Label ID="Label_range" runat="server" Text="考試範圍:" Visible="False" 
                                CssClass="style15"></asp:Label>
                        </td>
                        <td class="style13" valign="top">
                            <asp:TextBox ID="TextBox_Range" runat="server" Width="234px" Visible="False"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style14" valign="top">
                            <asp:Label ID="Label10" runat="server" Text="隨機出題題數" Visible="False" 
                                CssClass="style15"></asp:Label>
                        </td>
                        <td class="style13" valign="top">
                            <asp:TextBox ID="TextBox_RandCount" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style10" valign="top">
                            <asp:Label ID="Label4" runat="server" Text="起始時間" Visible="False" 
                                CssClass="style15"></asp:Label>
                        </td>
                        <td class="style9" valign="top">
                            <asp:Label ID="Label_BEGIN" runat="server" Text="Label" Visible="False" 
                                CssClass="style15"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style22" valign="top">
                            </td>
                        <td class="style23" valign="top">
                            <asp:Button ID="Button3" runat="server" onclick="Button3_Click" Text="編輯" 
                                Visible="False" />
                        </td>
                    </tr>
                    <tr>
                        <td class="style12" valign="top">
                            <asp:Label ID="Label7" runat="server" Text="結束時間" Visible="False" 
                                CssClass="style15"></asp:Label>
                        </td>
                        <td class="style11" valign="top">
                            <asp:Label ID="Label_END" runat="server" Text="Label_END" Visible="False" 
                                style="color: #000000; font-weight: 700"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style12" valign="top">
                            &nbsp;</td>
                        <td class="style11" valign="top">
                            <asp:Button ID="Button4" runat="server" onclick="Button4_Click" Text="編輯" 
                                Visible="False" />
                        </td>
                    </tr>
                    <tr>
                        <td class="style17" valign="top">
                            <br />
                            <span class="style21" dir="ltr">請選擇想要考試的人名單<br />
                            <strong>●請選擇群組:<br />
                            </strong>
                            <asp:Label ID="Label_G" runat="server" Text="Label"></asp:Label>
                            <br />
                            <asp:DropDownList ID="DropDownList_Group" runat="server" AutoPostBack="True" 
                                DataSourceID="SqlDataSource_Group" DataTextField="Name" DataValueField="ID" 
                                onselectedindexchanged="DropDownList_Group_SelectedIndexChanged" 
                                Width="317px" Height="17px">
                            </asp:DropDownList>
                            <asp:SqlDataSource ID="SqlDataSource_Group" runat="server" 
                                ConnectionString="<%$ ConnectionStrings:CCMATHConnectionString %>" 
                                SelectCommand="SELECT [ID], [Name] FROM [Groups] WHERE ([ID] = @ID)">
                                <SelectParameters>
                                    <asp:SessionParameter Name="ID" SessionField="CCMATHGroups" 
                                        Type="String" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                            <strong>
                            <br />
                            ●請選擇班級</strong>:<asp:CheckBoxList ID="CheckBoxList1" runat="server" 
                                AutoPostBack="True" DataSourceID="SqlDataSource_class" DataTextField="ClassName" 
                                DataValueField="ID" 
                                onselectedindexchanged="CheckBoxList1_SelectedIndexChanged">
                            </asp:CheckBoxList>
                            <asp:SqlDataSource ID="SqlDataSource_class" runat="server" 
                                ConnectionString="<%$ ConnectionStrings:CCMATHConnectionString %>" 
                                
                                SelectCommand="SELECT [ClassName], [ID] FROM [Class] WHERE ([GroupID] = @GroupID)">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="DropDownList_Group" DefaultValue="0000000000" Name="GroupID" PropertyName="SelectedValue" Type="String" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                            <br />
                            請選擇應考人員:<br />
                            <br />
                            <asp:Button ID="Button_AllP" runat="server" onclick="Button_AllP_Click" 
                                Text="全部選取" Width="77px" />
                            &nbsp;
                            <asp:Button ID="Button_AllD" runat="server" onclick="Button_AllD_Click" 
                                Text="全部取消" Width="77px" />
                            <br />
                            <br />
                            <asp:GridView ID="GridView_Stust" runat="server" 
                                AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="StudentID" 
                                DataSourceID="SqlDataSource_stust" EmptyDataText="目前沒有資料" 
                                
                                style="font-weight: 700; color: #000000; font-size: medium; margin-top: 0px; background-color: #66CCFF;" 
                                Width="363px" OnSelectedIndexChanged="GridView_Stust_SelectedIndexChanged">
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="CheckBox1" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="StudentID" HeaderText="學號" ReadOnly="True" 
                                        SortExpression="StudentID" />
                                    <asp:BoundField DataField="Name" HeaderText="姓名" SortExpression="Name" />
                                    <asp:BoundField DataField="Class" HeaderText="班級" SortExpression="Class" />
                                </Columns>
                            </asp:GridView>
                            <asp:SqlDataSource ID="SqlDataSource_stust" runat="server" 
                                ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
                                
                                SelectCommand="SELECT [StudentID], [Name], [Class], [Groups] FROM [PersonalInformation] ORDER BY [StudentID] DESC">
                            </asp:SqlDataSource>
                            <asp:Button ID="Button1" runat="server" Text="新增考試時間" onclick="Button1_Click" 
                                Visible="False" />
                            </span>
                            <asp:Button ID="Button_fix" runat="server" onclick="Button_fix_Click" Text="修改" 
                                Visible="False" style="height: 21px" />
                            <br />
                            <br />
                            <asp:Label ID="Label_Exception" runat="server" BorderColor="Red" 
                                BorderStyle="None" ForeColor="Red"></asp:Label>
                            </td>
                        <td class="style16" valign="top">
                            <strong dir="ltr">
                            <span class="style21">
                                考試名單<br />
                            (<asp:Label ID="Label_say" runat="server" Text="Label"></asp:Label>
                            )<asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" 
                        DataSourceID="SqlDataSource_Doing" EmptyDataText="目前沒有資料" 
                        style="color: #000000; font-weight: 700; font-size: x-small;" Width="100px" CellPadding="4" ForeColor="#333333" GridLines="None" PageSize="30">
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        <Columns>
                            <asp:BoundField DataField="StudentID" HeaderText="學號" 
                                SortExpression="StudentID" />
                        </Columns>
                                <EditRowStyle BackColor="#999999" />
                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                    </asp:GridView>
                            <br />
                    <asp:SqlDataSource ID="SqlDataSource_Doing" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:CCMATHConnectionString %>" 
                        SelectCommand="SELECT [StudentID] FROM [ExamsList] WHERE ([TestID] = @TestID)">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="GridView1" DefaultValue="0000000000" Name="TestID" PropertyName="SelectedValue" Type="String" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                            <br />
                            <br />
                                </span>
                </strong></td>
                    </tr>
                    </table>
            </td>
        </tr>
        <tr>
            <td class="style19">
                <br />
            </td>
        </tr>
    </table>
</asp:Content>
