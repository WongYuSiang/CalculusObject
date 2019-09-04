<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TeachingMaterials.aspx.cs" Inherits="CalculusObject.WebPage.TeachingMaterials" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .style4
        {
            width: 100%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table class="style4">
        <tr>
            <td valign="top" rowspan="2" style="background-color: #CCFFFF; width: 500px;">
                <asp:DropDownList ID="DropDownList1" runat="server" DataTextField="Chapter" 
                    DataValueField="Chapter" Width="207px" 
                    style="font-size: large; font-weight: 700" AutoPostBack="True" 
                    onselectedindexchanged="DropDownList1_SelectedIndexChanged">
                    <asp:ListItem>1-1</asp:ListItem>
                    <asp:ListItem>2-1</asp:ListItem>
                </asp:DropDownList>
                <br />
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                    DataSourceID="SqlDataSource_video" 
                    onselectedindexchanged="GridView1_SelectedIndexChanged" CellPadding="4" 
                    ForeColor="#333333" GridLines="None" 
                    style="font-weight: 700; font-size: small;" Width="500px" 
                    EmptyDataText="目前這章節沒有影片" onrowcreated="GridView1_RowCreated">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:CommandField ButtonType="Button" ShowSelectButton="True" />
                        <asp:BoundField DataField="MainContent" HeaderText="影片內容" 
                            SortExpression="MainContent" />
                        <asp:BoundField DataField="VideoTag" HeaderText="代碼" 
                            SortExpression="VideoTag" />
                        <asp:BoundField DataField="Cheapter" HeaderText="章節" 
                            SortExpression="Cheapter" />
                        <asp:BoundField DataField="Difficulty" HeaderText="觀看程度" 
                            SortExpression="Difficulty" />
                        <asp:BoundField DataField="SkillsUnit" HeaderText="適合技能" 
                            SortExpression="SkillsUnit" />
                        <asp:TemplateField>
                            <EditItemTemplate>
                                <asp:Label ID="Label_set" runat="server" 
                                    style="font-weight: 700; color: #FF3300" Text="推薦"></asp:Label>
                            </EditItemTemplate>
                            <ItemTemplate>
                                |<asp:Label ID="Label_set" runat="server" 
                                    style="font-weight: 700; color: #FF3300" Text="推薦"></asp:Label>
                                <br />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EditRowStyle BackColor="#7C6F57" />
                    <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#E3EAEB" />
                    <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                    <SortedAscendingCellStyle BackColor="#F8FAFA" />
                    <SortedAscendingHeaderStyle BackColor="#246B61" />
                    <SortedDescendingCellStyle BackColor="#D4DFE1" />
                    <SortedDescendingHeaderStyle BackColor="#15524A" />
                </asp:GridView>
                <asp:SqlDataSource ID="SqlDataSource_video" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:CCMATHConnectionString %>" 
                    
                    SelectCommand="SELECT [MainContent], [VideoTag], [Cheapter], [Difficulty], [SkillsUnit] FROM [Materials] WHERE ([Cheapter] = @Cheapter)">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="DropDownList1" Name="Cheapter" 
                            PropertyName="SelectedValue" Type="String" DefaultValue="00" />
                    </SelectParameters>
                </asp:SqlDataSource>
                <asp:ObjectDataSource ID="ObjectDataSource_View" runat="server">
                </asp:ObjectDataSource>
            </td>
            <td>
                <asp:Label ID="Label_star0" runat="server" 
                    style="font-weight: 700; color: #000000; font-size: small;" 
                    Text="調整觀看大小:    "></asp:Label>
                <asp:Button ID="Button1" runat="server" onclick="Button1_Click" 
                    Text="420x345" />
&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="Button3" runat="server" onclick="Button3_Click" 
                    Text="500x400" />
&nbsp;&nbsp;&nbsp;
                <asp:Button ID="Button2" runat="server" onclick="Button2_Click" 
                    Text="800x600" />
                <br />
<iframe id="VideoHere" runat="server" width="420" height="345" 
                    src="https://www.youtube.com/embed/OqSBJMUbCNA" frameborder="0" name="I1" ></iframe>
                <br />
            </td>
        </tr>
        <tr>
            <td style="background-color: #66CCFF">
                <asp:Label ID="Label_star1" runat="server" 
                    
                    style="font-weight: 700; color: #000000; font-size: x-large; background-color: #CCCCFF;" 
                    Text="請求各位同學給予回應與評分  &lt;(_ _)&gt;"></asp:Label>
                <br />
                <br />
                <asp:Label ID="Label_star" runat="server" 
                    style="font-weight: 700; color: #000000" Text="影片部分看完覺得如何 請評分:"></asp:Label>
                <asp:RadioButtonList ID="RadioButtonList1" runat="server" 
                    RepeatDirection="Horizontal" 
                    style="font-weight: 700; color: #000000; font-size: small; background-color: #CCFFCC;">
                    <asp:ListItem Value="1">覺得太簡單</asp:ListItem>
                    <asp:ListItem Value="2">聽的懂，可以了解</asp:ListItem>
                    <asp:ListItem Value="3">講解的太深奧，不懂</asp:ListItem>
                </asp:RadioButtonList>
                <br />
                <asp:Label ID="Label_Suggest0" runat="server" 
                    style="color: #000000; font-weight: 700" Text="請給予建議與意見"></asp:Label>
                <br />
                <asp:TextBox ID="TextBox_reply" runat="server" Height="118px" 
                    style="font-weight: 700; color: #000000" TextMode="MultiLine" 
                    Width="339px"></asp:TextBox>
                <br />
                <asp:Button ID="Button_send" runat="server" Text="送出意見" 
                    onclick="Button_send_Click" />
                <asp:Label ID="Label_THS" runat="server" 
                    style="color: #FF0000; font-weight: 700" Text="感謝給予回覆ヽ(✿ﾟ▽ﾟ)ノ" 
                    Visible="False"></asp:Label>
            </td>
        </tr>
    </table>
    <br />
</asp:Content>
