<%@ Page Title="個人檔案" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Personal_Page.aspx.cs" Inherits="CalculusObject.WebPage.Personal_Page" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <script src="../Scripts/jquery-1.4.1.js" type="text/javascript">                          
    </script>
    <script type="text/javascript">
        // 顯示讀取遮罩
        function ShowProgressBar() {
         //   var panel = document.getElementById("PanelAB");
            displayProgress();
            displayMaskFrame();
            setTimeout(function () { HideProgressBar(); }, 3000);
           
        }

        // 隱藏讀取遮罩
        function HideProgressBar() {
            var progress = $('#divProgress');
            var maskFrame = $("#divMaskFrame");
            progress.hide();
            maskFrame.hide();
        }
        // 顯示讀取畫面
        function displayProgress() {
            var w = $(document).width();
            var h = $(window).height();
            var progress = $('#divProgress');
            progress.css({ "z-index": 999999, "top": (h / 2) - (progress.height() / 2), "left": (w / 2) - (progress.width() / 2) });
            progress.show();
        }
        // 顯示遮罩畫面
        function displayMaskFrame() {
            var w = $(window).width();
            var h = $(document).height();
            var maskFrame = $("#divMaskFrame");
            maskFrame.css({ "z-index": 999998, "opacity": 0.7, "width": w, "height": h });
            maskFrame.show();
        }
</script>
    <style type="text/css">
        #Loadingbackground
       {
           position:fixed;
        top:0px;
        bottom:0px;
        right:0px;
        overflow:hidden;
        padding:0;
        margin:0;
        background:url('http://ilearning.csie.stust.edu.tw/CalculusObject/Images/BasicSet/loading.jpg');
         
         }
        .style1
        {            margin-top: 0px;
        }
        .style6
        {
            font-size: xx-small;
            font-family: 細明體;
            background-color: #0066FF;
            text-align: center;
        }
        .style10
        {
            font-size: medium;
            font-family: 細明體;
            background-color: #FFFFCC;
            width: 632px;
            }
        .auto-style1 {
            font-size: xx-small;
            font-family: 細明體;
            background-color: #0066FF;
            text-align: center;
            width: 493px;
        }
    </style>
   
</asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent" >

    <h2 title="個人學習">
        開課班級:<asp:Label ID="Label_C" runat="server" Text="Label"></asp:Label>
        ,班級:<asp:Label ID="Label_SC" runat="server" Text="Label"></asp:Label>
    </h2>
    <h2 title="個人學習">
        <asp:Label ID="Label_name" runat="server" Text="Label_name"></asp:Label>
        個人學習檔</h2>
    
        <table class="style1">
            <tr>
                <td class="auto-style2" valign="top">
                    <br />
                    <br />
                    <br />
                    <asp:Chart ID="Chart_ChapterCount" runat="server" BorderlineColor="Transparent" 
                        onload="Chart1_Load" Palette="Pastel" Width="600px" BackColor="Gray" 
                        BackSecondaryColor="Green" BorderlineDashStyle="Dash" Height="375px">
                        <Series>
                            <asp:Series BackGradientStyle="TopBottom" 
                                BackImageTransparentColor="255, 255, 192" BackSecondaryColor="Brown" 
                                BorderColor="Crimson" BorderWidth="2" Color="Red" LabelForeColor="RoyalBlue" 
                                MarkerBorderColor="LightCyan" Name="Chapter" ShadowColor="AntiqueWhite" 
                                Font="Microsoft Sans Serif, 12pt, style=Bold" IsValueShownAsLabel="True" 
                                IsXValueIndexed="True" Label="章節:#VALX\n做過:#VAL次" LabelBackColor="White" 
                                LabelBorderColor="Goldenrod">
                            </asp:Series>
                            <asp:Series ChartArea="ChartArea1" Name="Correct">
                            </asp:Series>
                        </Series>
                        <ChartAreas>
                            <asp:ChartArea Name="ChartArea1" ShadowColor="Khaki">
                                <AxisY Enabled="True" LineWidth="0" TextOrientation="Stacked" Title="答題次數" 
                                    TitleFont="Microsoft Sans Serif, 10.2pt, style=Bold">
                                </AxisY>
                                <AxisX Enabled="False" LineWidth="0">
                                </AxisX>
                                <AxisX2 Enabled="False" InterlacedColor="WhiteSmoke" 
                                    LabelAutoFitMinFontSize="5" LineColor="Goldenrod" LineWidth="0">
                                </AxisX2>
                                <AxisY2 Enabled="False" LineColor="LawnGreen" LineWidth="0" LogarithmBase="2">
                                </AxisY2>
                            </asp:ChartArea>
                        </ChartAreas>
                        <Titles>
                            <asp:Title Alignment="TopCenter" BackColor="Transparent" 
                                BackGradientStyle="LeftRight" BorderDashStyle="NotSet" DockingOffset="3" 
                                Font="Microsoft Sans Serif, 13.8pt, style=Bold" Name="Title1" 
                                ShadowColor="255, 128, 0" Text="章節作答紀錄">
                            </asp:Title>
                        </Titles>
                    </asp:Chart>
                    <asp:Chart ID="Chart_DoingTime" runat="server" BorderlineColor="Transparent" 
                        onload="Chart1_Load" Palette="Fire" Width="600px" BackColor="Gray" 
                        BackSecondaryColor="Green" BorderlineDashStyle="Dash" 
                        BackImageAlignment="Top" IsSoftShadows="False" Height="375px">
                        <Series>
                            <asp:Series BackGradientStyle="TopBottom" 
                                BackImageTransparentColor="255, 255, 192" BackSecondaryColor="Lime" 
                                BorderColor="DeepPink" BorderWidth="2" Color="DeepSkyBlue" LabelForeColor="ActiveCaptionText" 
                                MarkerBorderColor="LightCyan" Name="week" ShadowColor="AntiqueWhite" 
                                Font="Microsoft Sans Serif, 12pt, style=Bold" IsValueShownAsLabel="True" 
                                IsXValueIndexed="True" Label="#VAL" LabelBackColor="GreenYellow" 
                                ChartType="Line" Legend="Legend1" LabelBorderColor="LawnGreen">
                            </asp:Series>
                        </Series>
                        <ChartAreas>
                            <asp:ChartArea Name="ChartArea1" ShadowColor="Khaki" 
                                IsSameFontSizeForAllAxes="True">
                                <AxisY Enabled="True" LineWidth="0" ArrowStyle="Lines" 
                                    TextOrientation="Stacked" Title="作答次數" 
                                    TitleFont="Microsoft Sans Serif, 10.2pt, style=Bold">
                                </AxisY>
                                <AxisX Enabled="True" LineWidth="2" Interval="1" IsInterlaced="True" 
                                    LineColor="SteelBlue" LogarithmBase="2">
                                    <StripLines>
                                        <asp:StripLine BorderColor="255, 255, 128" />
                                        <asp:StripLine BorderColor="128, 255, 255" />
                                    </StripLines>
                                </AxisX>
                                <AxisX2 Enabled="False" InterlacedColor="WhiteSmoke" 
                                    LabelAutoFitMinFontSize="5" LineColor="Goldenrod" LineWidth="0">
                                </AxisX2>
                                <AxisY2 Enabled="False" LineColor="LawnGreen" LineWidth="0" LogarithmBase="2">
                                </AxisY2>
                            </asp:ChartArea>
                        </ChartAreas>
                        <Titles>
                            <asp:Title Alignment="TopCenter" BackColor="Transparent" 
                                BackGradientStyle="LeftRight" BorderDashStyle="NotSet" DockingOffset="3" 
                                Font="Microsoft Sans Serif, 13.8pt, style=Bold" Name="Title1" 
                                ShadowColor="255, 128, 0" Text="每週練習狀況">
                            </asp:Title>
                        </Titles>
                    </asp:Chart>
                    <br />
                    <br />
                    <br />
                    <asp:Panel ID="Panel1" runat="server" style="background-color: #00CCFF">
                        <asp:Label ID="Label1" runat="server" 
                        style="font-weight: 700; font-size: medium" Text="個人成績分析" Width="600px"></asp:Label>
                        <br />
                        <asp:GridView ID="GridView_Test" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4" DataSourceID="SqlDataSource_TEST" onrowcreated="GridView_Test_RowCreated" onselectedindexchanged="GridView_Test_SelectedIndexChanged" style="font-weight: 700; font-size: medium; background-color: #FFFFCC" Width="600px">
                            <Columns>
                                <asp:CommandField ButtonType="Button" ShowSelectButton="True" />
                                <asp:BoundField DataField="TestContent" HeaderText="考試名稱" SortExpression="TestContent" />
                                <asp:BoundField DataField="Score" HeaderText="成績" SortExpression="Score" />
                                <asp:BoundField DataField="TestID" HeaderText="TestID" SortExpression="TestID" />
                            </Columns>
                            <EmptyDataTemplate>
                                <asp:Label ID="Label3" runat="server" Text='<%# Eval("TestID", "{0}") %>'></asp:Label>
                            </EmptyDataTemplate>
                            <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                            <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" />
                            <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
                            <RowStyle BackColor="White" ForeColor="#003399" />
                            <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                            <SortedAscendingCellStyle BackColor="#EDF6F6" />
                            <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
                            <SortedDescendingCellStyle BackColor="#D6DFDF" />
                            <SortedDescendingHeaderStyle BackColor="#002876" />
                        </asp:GridView>
                    </asp:Panel>
                    <br />
                    <br />
                   


                    
                    <asp:SqlDataSource ID="SqlDataSource_TEST" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
                        
                        
                        SelectCommand="SELECT [Score], [TestContent], [TestID] FROM [ExamsList] WHERE ([Score] &gt;= @Score)">
                        <SelectParameters>
                            <asp:Parameter DefaultValue="0" Name="Score" Type="Int32" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                    <br />
                    <asp:Panel ID="Panel_chart" runat="server">
                        
                    </asp:Panel>
                   <br />
                   <br />
                   <br />                                     
                </td>
                <td class="style10" valign="top">
                    <asp:Label ID="Label_AbilityTitle" runat="server" Text="能力分析" Height="20px" 
                        Width="450px" 
                        
                        
                        
                        style="text-align: center; font-weight: 700; font-size: large; color: #000000; background-color: #0099FF"></asp:Label>
                    <asp:Label ID="Label_AbilityTitle0" runat="server" Text="請選擇章節查看" Height="20px" 
                        Width="450px" 
                        
                        
                        
                        style="text-align: left; font-weight: 700; font-size: large; color: #000000; background-color: #00FFFF"></asp:Label>
                    <br />
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
        
            <asp:CheckBoxList ID="CheckBoxList_Chapter" runat="server" 
                CellSpacing="2" DataSourceID="GetChapter" DataTextField="Chapter" 
                DataValueField="Chapter" 
                onselectedindexchanged="CheckBoxList_Chapter_SelectedIndexChanged" 
                RepeatDirection="Horizontal" RepeatLayout="Flow" 
                style="text-align: left; background-color: #99FFCC; font-weight: 700; color: #000000;" RepeatColumns="6" >
                <asp:ListItem Value="0">全部</asp:ListItem>
            </asp:CheckBoxList>

            <br />

            <asp:Button ID="Button_getAll" runat="server" onclick="Button_getAll_Click" 
                Text="全部選取" UseSubmitBehavior="false" Width="90px" EnableViewState="False" Height="26px" />
             
            <asp:Button ID="Button_NotAll0" runat="server" EnableViewState="False" Height="26px" onclick="Button_NotAll0_Click" Text="全部取消" UseSubmitBehavior="false" Width="90px" Visible="False" />
            <br />
             
            <asp:Button ID="Button_view" runat="server" OnClick="Button_view_Click" Text="查看" OnClientClick="ShowProgressBar();" Width="100%"  />
             
            <asp:SqlDataSource ID="GetChapter" runat="server" 
                ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
                SelectCommand="SELECT DISTINCT [Chapter] FROM [ExerciseData]">
            </asp:SqlDataSource>
            <asp:Panel ID="PanelAB" runat="server" >
            </asp:Panel>
            <br />
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
        
       </ContentTemplate>
       </asp:UpdatePanel>
<div id="divProgress" style="text-align:center; display: none; position: fixed; top: 50%;  left: 50%;" >
    <asp:Image ID="imgLoading" runat="server" ImageUrl="~/Images/loading.gif" />
    <br />
    <font color="#1B3563" size="2px">資料處理中</font>
</div>
<div id="divMaskFrame" style="background-color: #F2F4F7; display: none; left: 0px;
    position: absolute; top: 0px;">
</div>

                    <br />
                </td>
            </tr>
            </table>
        <br />

</asp:Content>
 
 