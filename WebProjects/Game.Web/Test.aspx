<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Test.aspx.cs" Inherits="Game.Web.Test" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" method="post" action="/WS/Account.ashx?action=UploadFace" enctype="multipart/form-data">
        <input type="file" name="f1"/>
        <input type="text" name="UserID" value="134"/>
        <input type="text" name="ClientIP" value="192.16.0.1"/>
        <input type="text" name="MachineID" value="XXXXXX"/>
        <input type="submit" value="提交" />
    </form>
</body>
</html>
