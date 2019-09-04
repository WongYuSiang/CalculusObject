<%@ Page Title="考試區" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TextOption_Page.aspx.cs" Inherits="CalculusObject.webpage.text_page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div>
    <asp:Label ID="Label_EXAM" runat="server" Text="考試區" 
        style="font-weight: 700; font-size: large"></asp:Label>
    <asp:GridView ID="GridView_EXAM" runat="server" AutoGenerateColumns="False" 
        BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" 
        CellPadding="3" DataKeyNames="TestID" DataSourceID="SqlDataSource_TestList" 
        ForeColor="Black" GridLines="Vertical" Width="918px" 
        onselectedindexchanged="GridView_EXAM_SelectedIndexChanged" 
        onrowdatabound="GridView_EXAM_RowDataBound" 
        onrowcreated="GridView_EXAM_RowCreated" style="text-align: left" 
            EmptyDataText="目前沒有考試">
        <AlternatingRowStyle BackColor="#CCCCCC" />
        <Columns>
            <asp:CommandField ButtonType="Button" SelectText="開始測驗" 
                ShowSelectButton="True" />
            <asp:BoundField DataField="TestID" HeaderText="TestID" ReadOnly="True" 
                SortExpression="TestID" />
            <asp:BoundField DataField="TestContent" HeaderText="考試內容" 
                SortExpression="TestContent" />
            <asp:BoundField DataField="Begin_time" HeaderText="開始時間" 
                SortExpression="Begin_time" />
            <asp:BoundField DataField="End_time" HeaderText="結束時間" 
                SortExpression="End_time" />
            <asp:BoundField DataField="Class" HeaderText="班級" SortExpression="Class" />
            <asp:BoundField DataField="TotalCount" HeaderText="總題數" 
                SortExpression="TotalCount" />
        </Columns>
        <FooterStyle BackColor="#AAAAAA" />
        <HeaderStyle BackColor="Orange" Font-Bold="True" ForeColor="Black" />
        <PagerStyle BackColor="#2323F2" ForeColor="Black" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#CCFF99" Font-Bold="True" ForeColor="White" />
        <SortedAscendingCellStyle BackColor="#FFCCFF" />
        <SortedAscendingHeaderStyle BackColor="#CCFFCC" />
        <SortedDescendingCellStyle BackColor="#FFCCCC" />
        <SortedDescendingHeaderStyle BackColor="#FFFFCC" />
    </asp:GridView>
        <br />
    <asp:Label ID="Label_EXAM0" runat="server" Text="其他" 
        style="font-weight: 700; font-size: large"></asp:Label>
    <asp:GridView ID="GridView_EXAM0" runat="server" AutoGenerateColumns="False" 
        BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" 
        CellPadding="3" DataSourceID="SqlDataSource_TestList0" 
        ForeColor="Black" GridLines="Vertical" Width="918px" 
            style="text-align: left; color: #808080;" EmptyDataText="目前沒有考試">
        <AlternatingRowStyle BackColor="#CCCCCC" />
        <Columns>
            <asp:ButtonField ButtonType="Button" Text="按鈕" />
            <asp:BoundField DataField="TestContent" HeaderText="考試內容" 
                SortExpression="TestContent" />
            <asp:BoundField DataField="Begin_time" HeaderText="開始時間" 
                SortExpression="Begin_time" />
            <asp:BoundField DataField="End_time" HeaderText="結束時間" 
                SortExpression="End_time" />
            <asp:BoundField DataField="Class" HeaderText="班級" SortExpression="Class" />
            <asp:BoundField DataField="TotalCount" HeaderText="總題數" 
                SortExpression="TotalCount" />
        </Columns>
        <FooterStyle BackColor="#AAAAAA" />
        <HeaderStyle BackColor="Orange" Font-Bold="True" ForeColor="Black" />
        <PagerStyle BackColor="#2323F2" ForeColor="Black" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#CCFF99" Font-Bold="True" ForeColor="White" />
        <SortedAscendingCellStyle BackColor="#FFCCFF" />
        <SortedAscendingHeaderStyle BackColor="#CCFFCC" />
        <SortedDescendingCellStyle BackColor="#FFCCCC" />
        <SortedDescendingHeaderStyle BackColor="#FFFFCC" />
    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSource_TestList0" runat="server" 
        ConnectionString="<%$ ConnectionStrings:ConnectionString2 %>" 
        
            SelectCommand="SELECT [TestContent], [Begin_time], [End_time], [Class], [TotalCount] FROM [TestTimeData]">
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlDataSource_TestList" runat="server" 
        ConnectionString="<%$ ConnectionStrings:ConnectionString2 %>" 
        SelectCommand="SELECT [TestID], [TestContent], [Begin_time], [End_time], [Class], [TotalCount] FROM [TestTimeData]">
    </asp:SqlDataSource>
    </div>
</asp:Content>
