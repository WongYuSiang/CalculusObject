<%@ Page Title="題目篩選" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ExamSelectedDatabase.aspx.cs" Inherits="CalculusObject.WebPage.QuestionDatabase_Page"  MaintainScrollPositionOnPostback="true"%>
<%@ Register assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI.DataVisualization.Charting" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .style3
        {
            width: 100%;
            margin-right: 0px;
        }
        .style7
        {            background-color: #FFFFCC;
        }
        .新增樣式1
        {
            border-color: #000000;
            border-style: dotted;
        }
        .style8
        {
            width: 20%;
        }
        .style9
        {
            width: 512px;
        }
        .style10
        {
            width: 570px;
            margin-left: 40px;
            background-color: #FFCCFF;
        }
        .auto-style1 {
            width: 100%;
            
        }
        .auto-style2 {
            width: 105px;
            background-color: #33CCCC;
        }
        .auto-style3 {
            background-color: #66CCFF;
            height: 27px;

        }
        .auto-style4 {
            width: 105px;
            background-color: #66CCFF;
            height: 27px;
        }
        .auto-style5 {
            background-color: #3399FF;
        }
        .auto-style6 {
            width: 105px;
            background-color: #3399FF;
        }
        .auto-style7 {
            background-color: #33CCCC;
        }
        .float-style {
            position:fixed; top:20%; left:5%;
            z-index:0;
            width:200px;
        }
        .float-style1 {
            position:fixed; top:10%;
            background-color: #FFFF99;
            width:400px;
        }
        .auto-style8 {
            font-size: medium;
            
        }
        .auto-style9 {
            width: 200px;
            height: 500px;
        }
        .auto-style10 {
            width: 400px;
            height: 479px;
            z-index:99;
        }
        .auto-style11 {
            background-color: #FFFFCC;
            height: 479px;
        }
        .auto-style12 {
            width: 100%;
            margin-right: 0px;
            font-size: x-small;
            height: 722px;
        }
        .auto-style14 {
            font-size: small;
        }
        .auto-style15 {
            text-decoration: underline;
        }
        .auto-style13 {
            font-size: x-small;
        }
        .auto-style16 {
            font-size: small;
            text-decoration: underline;
        }
        .auto-style18 {
            width: 183px;
        }
        .auto-style19 {
            font-size: small;
        }
        /*自動隱藏超出長寬的圖片*/
.post  {
overflow: visible;
width:200px;
height: 600px;
z-index:-1;
}

/*轉場的時間*/
.post img {
transition: all 0.5s ease 0s;
}

/*判斷滑鼠的動作放大圖片還有增加透明度*/
.post img:hover {
-moz-transform: scale(2.2);
-ms-transform: scale(2.2);
-o-transform: scale(2.2);
-webkit-transform: scale(2.2);
transform: scale(2.2);
transform-origin: 0 0;
opacity:1;

}
        .auto-style20 {
            font-size: xx-small;
        }
        
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Panel ID="Panel1" runat="server" CssClass="float-style">
                    <asp:Label ID="Label2" runat="server" 
                    style="font-size: medium; font-weight: 700; text-align: center; color: #000000; background-color: #99FFFD;" 
                    Text="滑鼠移至圖片會放大" Width="200px"></asp:Label>
                    <br />
                    <div class="post">
                    <asp:Label ID="Label7" runat="server" 
                    style="font-size: medium; font-weight: 700; text-align: center; color: #000000; background-color: #CCCCCC;" 
                    Text="檢視題目" Width="200px"></asp:Label>
                    <br />
                    <asp:Image ID="Image_Q" runat="server" Height="35px" style="text-align: left" 
                    Width="200px" ImageUrl="~/Images/Adaptive/C01010001Q.PNG" 
                    BorderStyle="Dotted" />
                    <br />
                    <asp:Label ID="Label9" runat="server" 
                    style="font-size: medium; font-weight: 700; text-align: center; color: #000000; background-color: #CCCCCC;" 
                    Text="正解" Width="200px"></asp:Label>
                    <br />
                    <asp:Image ID="Image_A" runat="server" Height="35px" style="text-align: left" 
                    Width="200px" ImageUrl="~/Images/Adaptive/C01010001A.PNG" 
                    BorderStyle="Dotted" />
                    <br />
                    <asp:Label ID="Label8" runat="server" 
                    style="font-size: medium; font-weight: 700; text-align: center; color: #000000; background-color: #CCCCCC;" 
                    Text="其他選項" Width="200px"></asp:Label>
                    <br />
                    <asp:Image ID="Image_B" runat="server" Height="35px" style="text-align: left" 
                    Width="200px" ImageUrl="~/Images/Adaptive/C01010001B.png" 
                    BorderStyle="Dotted" />
                    <br />
                    <asp:Image ID="Image_C" runat="server" Height="35px" style="text-align: left" 
                    Width="200px" ImageUrl="~/Images/Adaptive/C01010001C.png" 
                    BorderStyle="Dotted" />
                    <br />
                    <asp:Image ID="Image_D" runat="server" Height="35px" style="text-align: left" 
                    Width="200px" ImageUrl="~/Images/Adaptive/C01010001D.png" 
                    BorderStyle="Dotted" />
                        </div>
                </asp:Panel>
    &nbsp;<table class="auto-style12">
        <tr>
            
            <td class="auto-style9" valign="top">
                
            <td class="auto-style10" valign="top">
                <table class="auto-style1">
                    <tr>
                        <td class="auto-style4">
                <asp:Label ID="Label14" runat="server" 
                    style="font-size: medium; text-align: left; color: #000000; font-weight: 700; text-decoration: underline; background-color: #FFFFFF;" 
                    Text="章節選擇"></asp:Label>
                        </td>
                        <td class="auto-style3">
                <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" 
                    DataSourceID="GetChapter" DataTextField="Chapter" DataValueField="Chapter" 
                    onselectedindexchanged="DropDownList1_SelectedIndexChanged1" 
                    style="text-align: left; font-weight: 700; " Width="233px" CssClass="auto-style8">
                    <asp:ListItem Selected="True" Value="0">請選擇章節</asp:ListItem>
                </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style6">
                <asp:Label ID="Label16" runat="server" 
                    style="font-size: medium; text-align: left; color: #000000; font-weight: 700; text-decoration: underline; background-color: #FFFFFF;" 
                    Text="題目分類"></asp:Label>
                        </td>
                        <td class="auto-style5">
                            <strong>
                            <asp:CheckBoxList ID="CheckBoxList_AT" runat="server" AutoPostBack="True" CssClass="auto-style8" OnSelectedIndexChanged="CheckBoxList1_SelectedIndexChanged1" RepeatDirection="Horizontal">
                                <asp:ListItem Value="1">學生練習題目</asp:ListItem>
                                <asp:ListItem Value="2">考試專用</asp:ListItem>
                            </asp:CheckBoxList>
                            </strong>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style6">
                <asp:Label ID="Label13" runat="server" 
                    style="font-size: medium; text-align: left; color: #000000; font-weight: 700; text-decoration: underline; background-color: #FFFFFF;" 
                    Text="難易度篩選"></asp:Label>
                        </td>
                        <td class="auto-style5">
                <asp:CheckBoxList ID="CheckBoxList_Difficulty" runat="server" 
                    onselectedindexchanged="CheckBoxList1_SelectedIndexChanged" 
                    style="font-weight: 700; color: #000000;" AutoPostBack="True" DataTextField="Difficulty" 
                    DataValueField="Difficulty" Height="26px" RepeatDirection="Horizontal" CssClass="auto-style8">
                    <asp:ListItem>1</asp:ListItem>
                    <asp:ListItem>2</asp:ListItem>
                    <asp:ListItem>3</asp:ListItem>
                </asp:CheckBoxList>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style2">
                <asp:Label ID="Label12" runat="server" 
                    style="font-size: medium; text-align: left; color: #000000; font-weight: 700; text-decoration: underline; background-color: #FFFFFF;" 
                    Text="能力篩選"></asp:Label>
                        </td>
                        <td class="auto-style7">
                            <strong>
                <asp:CheckBoxList ID="CheckBoxList_SkillUnit" runat="server" 
                    onselectedindexchanged="CheckBoxList1_SelectedIndexChanged" 
                    style="color: #000000;" AutoPostBack="True" 
                    Height="26px" CssClass="auto-style20">
                    <asp:ListItem Value="0">全部</asp:ListItem>
                </asp:CheckBoxList>
                            </strong>
                        </td>
                    </tr>
                </table>
                <br />
                <asp:SqlDataSource ID="GetChapter" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
                    SelectCommand="SELECT DISTINCT [Chapter] FROM [ExerciseData]">
                </asp:SqlDataSource>
                <asp:Button ID="Button_Y" runat="server" OnClick="Button_Y_Click" Text="題目全選" />
                <asp:Button ID="Button_N" runat="server" OnClick="Button_N_Click" Text="選取取消" />
                <asp:Label ID="Label_TEXT" runat="server" style="color: #FF0000"></asp:Label>
                <br />
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
        DataSourceID="SqlDataSource1" CellPadding="2" ForeColor="Black" 
        GridLines="None" 
        style="font-weight: 700; margin-right: 22px; text-align: left;" 
                    onrowdatabound="GridView1_RowDataBound" onselectedindexchanged="GridView1_SelectedIndexChanged" 
                    Width="400px" ShowFooter="True" EmptyDataText="依指定條件查詢，未找到相符的資料。" 
                    ShowHeaderWhenEmpty="True" 
                    BackColor="LightGoldenrodYellow" BorderColor="Tan" BorderWidth="1px" OnPreRender="GridView1_PreRender" CssClass="auto-style13">
        <AlternatingRowStyle BackColor="PaleGoldenrod" />
        <Columns>
            <asp:TemplateField ShowHeader="False">
                <ItemTemplate>
                    <asp:Button ID="Button1" runat="server" CausesValidation="False" 
                        CommandName="Select" Text="顯示" />
                    <asp:CheckBox ID="CheckBoxInTo" runat="server" Text="選取匯入" />
                    <asp:Label ID="Label_sureGet" runat="server" Enabled="False" ForeColor="Red" Text="已經選取題目" Visible="False"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="編號" SortExpression="題目編號">
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("ID") %>'></asp:TextBox>
                </EditItemTemplate>
            </asp:TemplateField>
           
            <asp:BoundField DataField="Difficulty" HeaderText="難易度" SortExpression="Difficulty" />
            <asp:BoundField DataField="SkillsUnit" HeaderText="技能分析" 
                SortExpression="SkillsUnit" />
            <asp:BoundField DataField="Chapter" HeaderText="章節" SortExpression="Chapter" />
        </Columns>
        <FooterStyle BackColor="Tan" />
        <HeaderStyle BackColor="Tan" Font-Bold="True" />
        <PagerStyle BackColor="PaleGoldenrod" ForeColor="DarkSlateBlue" 
            HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />

<SortedAscendingCellStyle BackColor="#FAFAE7"></SortedAscendingCellStyle>

<SortedAscendingHeaderStyle BackColor="#DAC09E"></SortedAscendingHeaderStyle>

<SortedDescendingCellStyle BackColor="#E1DB9C"></SortedDescendingCellStyle>

<SortedDescendingHeaderStyle BackColor="#C2A47B"></SortedDescendingHeaderStyle>
    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:CCMATHConnectionString %>" 
        
        SelectCommand="SELECT [ID], [Difficulty], [SkillsUnit], [Chapter], [AdpterOrTest] FROM [ExerciseData] WHERE ([AdpterOrTest] = @AdpterOrTest)">
        <SelectParameters>
            <asp:ControlParameter ControlID="CheckBoxList_AT" DefaultValue="0" Name="AdpterOrTest" PropertyName="SelectedValue" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
                <br />
            </td>
            <td class="auto-style11" valign="top">
                <br />
                <asp:SqlDataSource ID="SqlDataSource4" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
                    
                    
                    
                    SelectCommand="SELECT DISTINCT [doNumber] FROM [SelectedData] WHERE (([Way] = @Way) AND ([TextID] = @TextID))">
                    <SelectParameters>
                        <asp:Parameter DefaultValue="1" Name="Way" Type="Int32" />
                        <asp:ControlParameter ControlID="GridView_testDetail" Name="TextID" 
                            PropertyName="SelectedValue" Type="String" DefaultValue="123" />
                    </SelectParameters>
                </asp:SqlDataSource>
                &nbsp;&nbsp;&nbsp;&nbsp;
                <br />
                <asp:SqlDataSource ID="SqlDataSource3" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
                    
                    
                    
                    SelectCommand="SELECT DISTINCT [doNumber] FROM [SelectedData] WHERE (([Way] = @Way) AND ([TextID] = @TextID))">
                    <SelectParameters>
                        <asp:Parameter DefaultValue="2" Name="Way" Type="Int32" />
                        <asp:ControlParameter ControlID="GridView_testDetail" Name="TextID" 
                            PropertyName="SelectedValue" Type="String" DefaultValue="123" />
                    </SelectParameters>
                </asp:SqlDataSource>
                <asp:Panel ID="Panel2" runat="server" CssClass="float-style1">
                    <span class="auto-style14"><strong>依照步驟完成考卷設定:<br class="auto-style15" /> <span class="auto-style15">Step1.請先&quot;點取&quot;考卷</span></strong></span><br /> 
                    <asp:GridView ID="GridView_testDetail" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource_test" OnSelectedIndexChanged="GridView_testDetail_SelectedIndexChanged" BackColor="White" BorderColor="White" BorderStyle="Ridge" BorderWidth="2px" CellPadding="3" CellSpacing="1" GridLines="None" Height="176px" DataKeyNames="TestID" OnRowCreated="GridView_testDetail_RowCreated" AllowPaging="True" PageSize="5" CssClass="auto-style20" Width="500px">
                        <Columns>
                            <asp:CommandField ButtonType="Button" ShowSelectButton="True" />
                            <asp:BoundField DataField="TestID" HeaderText="序號" SortExpression="TestID">
                            <ItemStyle Width="0px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="TestContent" HeaderText="範圍" SortExpression="TestContent" />
                            <asp:BoundField DataField="Begin_time" HeaderText="開始" SortExpression="Begin_time" />
                            <asp:BoundField DataField="End_time" HeaderText="結束" SortExpression="End_time" />
                            <asp:BoundField DataField="Groups" HeaderText="群組" SortExpression="Groups" Visible="False" />
                            <asp:BoundField DataField="RandCount" HeaderText="隨機" SortExpression="RandCount" />
                            <asp:BoundField DataField="MustCount" SortExpression="MustCount" HeaderText="必考" />
                            <asp:BoundField DataField="TotalCount" HeaderText="總題數" SortExpression="TotalCount" />
                        </Columns>
                        <FooterStyle BackColor="#C6C3C6" ForeColor="Black" />
                        <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#E7E7FF" />
                        <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
                        <RowStyle BackColor="#DEDFDE" ForeColor="Black" />
                        <SelectedRowStyle BackColor="#9471DE" Font-Bold="True" ForeColor="White" />
                        <SortedAscendingCellStyle BackColor="#F1F1F1" />
                        <SortedAscendingHeaderStyle BackColor="#594B9C" />
                        <SortedDescendingCellStyle BackColor="#CAC9C9" />
                        <SortedDescendingHeaderStyle BackColor="#33276A" />
                    </asp:GridView>
                    <asp:SqlDataSource ID="SqlDataSource_test" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:CCMATHConnectionString %>" 
                        SelectCommand="SELECT [TestID], [TestContent], [Begin_time], [End_time], [RandCount], [TotalCount], [MustCount], [Groups] FROM [TestTimeData] WHERE ([Groups] = @Groups)">
                        <SelectParameters>
                            <asp:SessionParameter DefaultValue="000000000" Name="Groups" SessionField="CCMATHGroups" Type="String" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                    <asp:Button ID="ButtonAddTime" runat="server" Height="22px" onclick="ButtonAddTime_Click" style="margin-top: 0px" Text="新增考試時間" Width="300px" />
                    <br />
                    <table class="auto-style1">
                        <tr>
                            <td colspan="2">
                                <asp:Label ID="Label5" runat="server" 
                    style="text-align: left; color: #000000; font-weight: 700;" 
                    Text="測驗代碼:" CssClass="auto-style13"></asp:Label>
                                <span class="auto-style13">
                                <asp:Label ID="Label_getContent" runat="server"></asp:Label>
                                (<asp:Label ID="Label_getID" runat="server"></asp:Label>
                                </span>)</td>
                        </tr>
                        <tr>
                            <td colspan="2" class="auto-style16">
                                <strong>Step2.從左邊題目勾選並將題目匯入到下方</strong></td>
                        </tr>
                        <tr>
                            <td class="auto-style18">
                                <asp:Label ID="Label3" runat="server" style="font-weight: 700; color: #000000" Text="基本題目" Width="150px"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label4" runat="server" style="font-weight: 700; color: #000000" Text="隨機選題題目" Width="150px"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top" class="auto-style18">
                                <asp:ListBox ID="ListBox1" runat="server" Height="75px" Width="150px" Rows="5" 
                    style="margin-left: 3px" DataSourceID="SqlDataSource4" 
                    DataTextField="doNumber" DataValueField="doNumber" 
                    onselectedindexchanged="ListBox1_SelectedIndexChanged" 
                    onprerender="ListBox_PreRender" CssClass="auto-style13"></asp:ListBox>
                                <br />
                                <asp:Button ID="Button2" runat="server" Text="選入至基本題目" Width="152px" 
                    Height="24px" style="font-weight: 700" 
                    onclick="Button2_Click" CssClass="auto-style13" />
                                &nbsp;<asp:Button ID="ButtonDelete1" runat="server" Text="刪除" 
                    onclick="ButtonDelete1_Click" Height="24px" />
                                <br />
                                <asp:Label ID="Label_textS" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:ListBox ID="ListBox2" runat="server" Height="75px" Width="150px" 
                    DataSourceID="SqlDataSource3" DataTextField="doNumber" 
                    DataValueField="doNumber" 
                    onselectedindexchanged="ListBox2_SelectedIndexChanged" 
                    onprerender="ListBox_PreRender" CssClass="auto-style13"></asp:ListBox>
                                <br />
                                <asp:Button ID="Button4" runat="server" Text="選入隨機選題題目" Width="157px" 
                    Height="24px" style="font-weight: 700" 
                    onclick="Button4_Click" CssClass="auto-style13" />
                                &nbsp;&nbsp;&nbsp;
                                <br />
                                <asp:Button ID="ButtonDelete2" runat="server" Text="刪除" 
                    onclick="Button5_Click" Height="24px" />
                                <br />
                                <asp:Label ID="Label15" runat="server" Text="隨機出題個數" CssClass="auto-style19"></asp:Label>
                                <asp:TextBox ID="TextBox2" runat="server" Width="72px"></asp:TextBox>
                               
                                <br />
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Label ID="Label_CountError" runat="server" style="color: #FF0000" Text="[▲]目前設定'亂數題數'小於'挑選題目'可能造成題數減少" Visible="False"></asp:Label>
                                <asp:Button ID="Button_Sure" runat="server" Text="儲存題目" Width="300px" OnClick="Button_Sure_Click" />
                                <br />
                                <strong><span class="auto-style16">Step3.按上方儲存題目即完成匯入動作</span></strong><br />
                                <br />
                                <asp:Label ID="Label_Exception" runat="server" ForeColor="Red" 
                    style="font-weight: 700"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <br />
                &nbsp;
                <br />
               
                <br />
            &nbsp;<br />
                <br />
                <br />
            </td>
        </tr>
        </table>
    
    </asp:Content>
