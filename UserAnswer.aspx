<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UserAnswer.aspx.cs" Inherits="CalculusObject.WebPage.UserAnswer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <!---->
    <link rel="stylesheet" href="/Styles/jquery-ui.css"><link />
    <script type="text/javascript" src="/Scripts/jquery-1.12.4.js"></script>
    <script type="text/javascript" src="/Scripts/jquery-ui.js"></script>
    <script type="text/javascript" >
        $(function () {
            $("#datepicker").datepicker({ dateFormat: "yy/mm/dd" });
            $("#datepicker1").datepicker({ dateFormat: "yy/mm/dd" });
        });
    </script>
    <!---->
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<h1 ID="test1h1">
    
    </h1></br>    
        
        </br>
        <!--Date: <input type="text" ID="datepicker">-->
        選擇開始日期：<asp:TextBox ID="datepicker1" Enabled="false" runat="server" 
                oninit="datepicker1_Load" ></asp:TextBox>
        <asp:Button ID="ButtonDatepicker1" runat="server" Text="..." onclick="Datepicker1" />
        選擇結束日期：<asp:TextBox ID="datepicker2" Enabled="false" runat="server" ></asp:TextBox>
        <asp:Button ID="ButtonDatepicker2" runat="server" Text="..." onclick="Datepicker1" />
        <br />
        
        <asp:Button ID="Button1" runat="server" Text="計算難易度" onclick="Button1_Click" />
        </br>
        <asp:Label ID="Calendar1Label" runat="server" Text="" Style="color:#ff0000;"></asp:Label>
        <asp:Calendar ID="Calendar1" runat="server" Visible="False" onselectionchanged="CalendarChange"></asp:Calendar>
        </br>
        <asp:Label ID="Calendar1Labe2" runat="server" Text="" Style="color:#ff0000;"></asp:Label>
        <asp:Calendar ID="Calendar2" runat="server" Visible="False" onselectionchanged="CalendarChange"></asp:Calendar>
        <!--<asp:Label ID="Label" runat="server" Text="總人數"></asp:Label>
        <asp:DropDownList ID="DropDownList1" runat="server">
            <asp:ListItem>>=</asp:ListItem>
            <asp:ListItem><=</asp:ListItem>
        </asp:DropDownList>
        <asp:TextBox ID="TextBox_ID" runat="server" Height="23px"></asp:TextBox>
        <asp:Button ID="Button2" runat="server" Text="尋找" 
        Style="margin-right:50px; height: 21px;" onclick="Button2_Click" />-->
        <asp:Button ID="DateButton" runat="server" Text="收尋" onclick="DateButtonClick" />
        </br>
            
            <asp:Label ID="Label1" runat="server" Text="" Style="color:#ff0000;"></asp:Label>
    
    </br>
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"
        DataSourceID="SqlDataSource1" Width="80%" style="text-align:center;" BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="2">
        <Columns>
            <asp:BoundField DataField="ID" HeaderText="題目編號" SortExpression="ID" />
            <asp:BoundField DataField="OriginDiff" HeaderText="原難易度" SortExpression="OriginDiff" />
            <asp:BoundField DataField="NewDiff" HeaderText="新難易度" SortExpression="NewDiff" />
            <asp:BoundField DataField="DoneTimes" HeaderText="作答人數" SortExpression="DoneTimes" />
            <asp:BoundField DataField="Date" HeaderText="更新日期" SortExpression="Date" />
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
        SelectCommand="SELECT [ID], [OriginDiff], [NewDiff], [Date], [DoneTimes] FROM [HistoryAbilityChanges]">
    </asp:SqlDataSource>
</asp:Content>
