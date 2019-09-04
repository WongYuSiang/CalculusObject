<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserExam.aspx.cs" Inherits="CalculusObject.WebPage.WebForm1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <a style="color:Blue;">ID，SkillsUnit，Difficult，StudentData可以重複輸入<span style="color:Red;">以 , 來區分</span></a><br /><br />
    ID:<asp:TextBox ID="TID" runat="server" Height="100px" TextMode="MultiLine"></asp:TextBox>
    SkillsUnit:<asp:TextBox ID="TSkillsUnit" runat="server" Height="100px" TextMode="MultiLine"></asp:TextBox>
    Difficult:<asp:TextBox ID="TDifficult" runat="server" Height="100px" TextMode="MultiLine"></asp:TextBox>
    Chapter:<asp:TextBox ID="TChapter" runat="server" Height="100px" TextMode="MultiLine"></asp:TextBox><br /><br />
    <div>StudentData(StudentID,Correct(0OR1))ex:4a2g0000,1,1,1,0,..........<br />X表示失敗<br />?表示可能已存在<br />
    <asp:TextBox ID="TStudentData" runat="server" Height="300px" TextMode="MultiLine"></asp:TextBox>
    <asp:TextBox ID="TCheck" runat="server" Height="300px" TextMode="MultiLine" ReadOnly="True" ></asp:TextBox><br />
    </div>
    <asp:Button ID="Button2" runat="server" Text="匯入資料" onclick="Button2Click" />
    <asp:Button ID="Button1" runat="server" Text="開始計算" onclick="Button1Click" />
    <asp:Label ID="Label1" runat="server" Text="" Style="color:#ff0000;"></asp:Label>
    </div>
    </form>
</body>
</html>
