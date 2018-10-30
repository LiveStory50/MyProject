<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="MyProject.WebForm1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
           <a class="thisa">点击</a>
           
        </div>
    </form>
    <script src="js/jquery-1.12.4.js"></script>
    <script>
        $(function () {
            $(".thisa").click(function () {
                 $.ajax({
                     url: 'Handler1.ashx',
                     type: 'POST',
                     data: {
                         "action": "toExcel"
                     }
                 }).done(function (res) {
                     if (res) {
                         alert("删除成功")
                         location.reload();
                     }
                     else {
                         alert("删除失败")
                         location.reload();
                     }


                 })
            })
        })
    </script>
</body>
</html>
