<%@ Page Title="建立大量資料" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CreateAccount_Page.aspx.cs" Inherits="CalculusObject.WebPage.CreateAccount_Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .style4
        {
            width: 100%;
        }
        .style5
        {
           
            background-color: #CCFFFF;
        }
        .style6
        {
            font-weight: bold;
            border-bottom-style: solid;
            background-color: #FFFFCC;
        }
        .style7
        {
            color: #000000;
        }
        .style8
        {
            width: 91px;
            color: #696969;
            font-weight: bold;
            border-bottom-style: solid;
            background-color: #FFFFCC;
        }
        .style9
        {
            width: 217px;
        }
        .auto-style5 {
            width: 200px;
            background-color: #66CCFF;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <table class="style4">
        <tr>
            <td dir="ltr" valign="top" class="auto-style5">
                學生明細<br />
                <br />
                班級:<asp:CheckBoxList ID="CheckBoxList_C" runat="server" AutoPostBack="True" DataSourceID="SqlDataSource_class" DataTextField="ClassName" DataValueField="ID" OnSelectedIndexChanged="CheckBoxList_C_SelectedIndexChanged1">
                </asp:CheckBoxList>
                <asp:SqlDataSource ID="SqlDataSource_class" runat="server" ConnectionString="<%$ ConnectionStrings:CCMATHConnectionString %>" SelectCommand="SELECT [ClassName], [ID] FROM [Class] WHERE ([GroupID] = @GroupID)">
                    <SelectParameters>
                        <asp:SessionParameter DefaultValue="0000000000" Name="GroupID" SessionField="CCMATHGroups" Type="String" />
                    </SelectParameters>
                </asp:SqlDataSource>
                <br />
                學生明細<asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" DataSourceID="SqlDataSource_P" ForeColor="Black" GridLines="Vertical" OnDataBound="GridView3_DataBound" Width="300px" EmptyDataText="此班級目前匯入同學">
                    <AlternatingRowStyle BackColor="#CCCCCC" />
                    <Columns>
                        <asp:BoundField DataField="StudentID" HeaderText="學號" SortExpression="StudentID" />
                        <asp:BoundField DataField="Name" HeaderText="姓名" SortExpression="Name" />
                        <asp:BoundField DataField="Class" HeaderText="班級" SortExpression="Class" />
                        <asp:BoundField DataField="Purview" SortExpression="Purview" />
                    </Columns>
                    <FooterStyle BackColor="#CCCCCC" />
                    <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                    <SortedAscendingHeaderStyle BackColor="#808080" />
                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                    <SortedDescendingHeaderStyle BackColor="#383838" />
                </asp:GridView>
                <asp:SqlDataSource ID="SqlDataSource_P" runat="server" ConnectionString="<%$ ConnectionStrings:CCMATHConnectionString %>" SelectCommand="SELECT [StudentID], [Name], [Class], [Purview] FROM [PersonalInformation] WHERE ([ClassID] = @ClassID)">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="CheckBoxList_C" DefaultValue="00000000000" Name="ClassID" PropertyName="SelectedValue" Type="String" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </td>
            <td dir="ltr" valign="top" class="style5">
                <asp:Label ID="Label5" runat="server" Text="群組表"></asp:Label>
                <br />
                <asp:GridView ID="GridView_Groups" runat="server" AllowSorting="True" 
                    AutoGenerateColumns="False" DataKeyNames="ID" 
                    DataSourceID="SqlDataSource_Groups" 
                   
                    style="font-weight: 700; color: #000000; background-color: #CCFF99" CellPadding="4" ForeColor="#333333" GridLines="None">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:CommandField ButtonType="Button" ShowSelectButton="True" />
                        <asp:BoundField DataField="ID" HeaderText="開課代碼" ReadOnly="True" 
                            SortExpression="ID" />
                        <asp:BoundField DataField="School" HeaderText="學校" SortExpression="School" />
                        <asp:BoundField DataField="teacher" HeaderText="老師" SortExpression="teacher" />
                    </Columns>
                    <EditRowStyle BackColor="#2461BF" />
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#EFF3FB" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <SortedAscendingCellStyle BackColor="#F5F7FB" />
                    <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                    <SortedDescendingCellStyle BackColor="#E9EBEF" />
                    <SortedDescendingHeaderStyle BackColor="#4870BE" />
                </asp:GridView>
                <br />
                開課對應表<asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" DataKeyNames="ID" DataSourceID="SqlDataSource_classID" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Vertical">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:BoundField DataField="GroupID" HeaderText="群組代號" SortExpression="GroupID" />
                        <asp:BoundField DataField="ID" HeaderText="課程代號" ReadOnly="True" SortExpression="ID" />
                        <asp:BoundField DataField="ClassName" HeaderText="開課名稱" SortExpression="ClassName" />
                    </Columns>
                    <FooterStyle BackColor="#CCCC99" />
                    <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                    <RowStyle BackColor="#F7F7DE" />
                    <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                    <SortedAscendingCellStyle BackColor="#FBFBF2" />
                    <SortedAscendingHeaderStyle BackColor="#848384" />
                    <SortedDescendingCellStyle BackColor="#EAEAD3" />
                    <SortedDescendingHeaderStyle BackColor="#575357" />
                </asp:GridView>
                <asp:SqlDataSource ID="SqlDataSource_classID" runat="server" ConnectionString="<%$ ConnectionStrings:CCMATHConnectionString %>" SelectCommand="SELECT [ID], [GroupID], [ClassName] FROM [Class] WHERE ([GroupID] = @GroupID)">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="GridView_Groups" DefaultValue="0000000000" Name="GroupID" PropertyName="SelectedValue" Type="String" />
                    </SelectParameters>
                </asp:SqlDataSource>
                <br />
                <table class="style4">
                    <tr>
                        <td>
                <strong><span class="style7">
                <asp:Label ID="Label4" runat="server" Text="新增帳號設定"></asp:Label>
                </span></strong>
                        </td>
                    </tr>
                    <tr>
                        <td><strong><span class="style7">
                1.請先選權限<asp:RadioButtonList ID="RadioButtonList_purview" runat="server" 
                    DataSourceID="SqlDataSource_Purview" DataTextField="Description" DataValueField="Name" OnSelectedIndexChanged="RadioButtonList_purview_SelectedIndexChanged">
                </asp:RadioButtonList>
                </span></strong>
                        </td>
                    </tr>
                    <tr>
                        <td><strong><span class="style7">
                2.依照格式新增帳號打入下方</span></strong></td>
                    </tr>
                    <tr>
                        <td><strong><span class="style7">
                <asp:Label ID="Label3" runat="server" Text="格式設定:學號,姓名,班級,群組代號,課程代號"></asp:Label>
                            <br />
                <asp:Label ID="Label6" runat="server" Text="例如:12345678,王小明,棒棒糖班,TESTTEST,TESTTEST"></asp:Label>
                </span></strong>
                        </td>
                    </tr>
                    <tr>
                        <td>
                <asp:TextBox ID="TextBox_AccountData" runat="server" Height="247px" 
                    Width="388px" style="font-weight: 700; text-align: left" 
                    TextMode="MultiLine"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                </table>
                <br />
                <strong><span class="style7">
                <br />
                <br />
                </span></strong>
                <br />
                <asp:SqlDataSource ID="SqlDataSource_Groups" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:CCMATHConnectionString %>" 
                    
                    SelectCommand="SELECT [ID], [Begin_time], [End_time], [School], [teacher] FROM [Groups] WHERE (([ID] = @ID) AND ([Usestatus] = @Usestatus))">
                    <SelectParameters>
                        <asp:SessionParameter Name="ID" SessionField="CCMATHGroups" Type="String" />
                        <asp:Parameter DefaultValue="1" Name="Usestatus" Type="Int32" />
                    </SelectParameters>
                </asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_Purview" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:CCMATHConnectionString %>" 
                    SelectCommand="SELECT [Name], [Description] FROM [Purview] WHERE (([Name] &lt;&gt; @Name) AND ([Name] &lt;&gt; @Name2))">
                    <SelectParameters>
                        <asp:Parameter DefaultValue="root" Name="Name" Type="String" />
                        <asp:Parameter DefaultValue="teacher" Name="Name2" Type="String" />
                    </SelectParameters>
                </asp:SqlDataSource>
                <br />
                <asp:Button ID="AddButton" runat="server" onclick="AddButton_Click" Text="匯入帳號" 
                    Width="141px" />
                <br />
                <asp:Label ID="Label_error" runat="server" 
                    style="font-weight: 700; color: #FF0000" Text="請輸入匯入資料"></asp:Label>
            </td>
        </tr>
        </table>
    <br />
</asp:Content>
