
$(function () {

    //加入购物车
    $(".joinshop").click(function () {
        //如何将id加入购物车表
        var id = $(this).attr('name')

        $.ajax({
            type: "GET",
            url: "ajax/ShopAjax.ashx?id=" + id,
            success: function (msg) {
                if (msg == "空") {
                    location.href = 'login.aspx';
                    return;
                }
                else if (msg == "成功") {
                    alert("成功加入购物车")
                }
                else if (msg == "失败") {
                    alert("加入购物车失败")
                }
            }
        });

    });




    
    //购物车中点击－数据库购物车数量减一
    $(".reduce").click(function () {
        //获取name里的值------商品id
        var proid = $(this).attr('name')

        $.ajax({
            type: "GET",
            url: "ajax/ShopCartReduce.ashx?proid=" + proid,
            success: function (msg) {
                if (msg == "小于一") {
                    alert("商品数量不能小于一");
                }
            }
        });

    });

    //购物车中点击 + 数据库购物车数量加一
    $(".add").click(function () {
        //获取name里的值------商品id
        var proid = $(this).attr('name')

        $.ajax({
            type: "GET",
            url: "ajax/ShopCartAdd.ashx?proid=" + proid,
            success: function (msg) {
                if (msg == "大于") {
                    alert("商品数量最多只能为999");
                }
            }
        });

    });

    //当购物车中input失去焦点时候将input内的值赋给数据库
    $(".count-input").blur(function () {
        //获取input里的值------手动添加商品的数量
        var procount = $(this).val();
        var proid = $(this).attr('name');
        if (procount < 2) {
            alert("商品数量最少为1");
        }
        else if (procount > 999) {
            alert("商品数量最多只能为999");
        }
        else if (1 < procount < 999) {
            $.ajax({
                type: "GET",
                url: "ajax/AddProQuantity.ashx?procount=" + procount + "&proid=" + proid,
                success: function (msg) {
                    if (msg == "不足") {
                        alert("库存不足！");
                    }
                }
            });
        }
    });

    //删除
    $(".delete").click(function () {
        //获取商品id
        var proid = $(this).attr('name');
        $.ajax({
            type: "GET",
            url: "ajax/DelShopPro.ashx?proid=" + proid,
            success: function (msg) {
                if (msg == "succes") {
                    alert("删除成功");
                    location.reload();
                }
                else if (msg == "defeated") {
                    alert("删除失败");
                }
            }
        });

    });




    //账户安全----验证旧密码
    $("#oldpwd").blur(function (oldpwd) {
        var oldpwd = $(this).val();

        $.ajax({
            url: 'ajax/CheckPassWord.ashx',
            type: 'POST',
            data: {
                "action": "checkpwd",
                "oldpwd": oldpwd
            }
        }).done(function (res) {
            if (res == "错误") {
                alert("旧密码错误");
                location.reload();
            }
        })
    });
    //账户安全----修改密码
    $("#updat").click(function (newpwd) {
        var newpwd = document.getElementById("newpwd").value;
        var realpwd = document.getElementById("realpwd").value;
        if (newpwd == realpwd) {
            $.ajax({
                url: 'ajax/CheckPassWord.ashx',
                type: 'POST',
                data: {
                    "action": "updatepwd",
                    "newpwd": newpwd
                }
            }).done(function (res) {
                if (res == "succce") {
                    location.href = 'login.aspx';

                }
                else if (res == "defeated") {
                    alert("修改密码失败");
                }
            })
        }
        else {
            alert("两次输入密码不一致");
        }

    })

    ////购物车分页
    ////通过recordCount的Lenth长度来获取购物车数据总条数
    //    var recordCount = document.getElementsByTagName("tbody")[0].getElementsByTagName("tr");
    ////每页显示多少条数据
    //    var pageSize = 6;
    ////总行数除以每页显示数据的条数获得总页数
    //    var pageCount = Math.ceil(recordCount.length / pageSize);
    //    if (recordCount<7) {
    //        //如果总页数少于七条就隐藏分页
    //    }



    ///订单表操作


    //立即支付----修改数据库中statuss的状态参数
    $(".paynow").click(function (appid) {

        var appid = $(this).attr("data-code");

        $.ajax({
            url: 'ajax/CheckPassWord.ashx',
            type: 'POST',
            data: {
                "action": "pays",
                "appid": appid
            }
        }).done(function (res) {
            if (res == "seccess") {
                //$(".member-check").css('display',' block')
                alert("支付成功");
                location.reload();
            }
            else if (res == "defeated") {
                alert("支付失败");
            }
        })
    })

    //删除订单
    //删除订单----——删除主表和从表内容
    $(".member-delete").click(function (appid) {

        var appid = $(this).attr("data-code");

        $.ajax({
            url: 'ajax/CheckPassWord.ashx',
            type: 'POST',
            data: {
                "action": "delorders",
                "appid": appid
            }
        }).done(function (res) {
            if (res == "seccess") {

                alert("成功");
                location.reload();
            }
            else if (res == "defeated") {
                alert("失败");
            }
        })


    })

    //确认收货----修改数据库中statuss的状态参数1改为2 _————存储过程
    $(".receives").click(function (appid) {
        var appid = $(this).attr("data-code");

        $.ajax({
            url: 'ajax/CheckPassWord.ashx',
            type: 'POST',
            data: {
                "action": "receivesYes",
                "appid": appid
            }
        }).done(function (res) {
            if (res == "") {
               
                alert("收货完成,交易成功");
                location.reload();
            }
            else  {
                alert(res);
            }
        })
    })


    //发表评价
    //发表评价----——向评价表插入数据_————存储过程
    $(".publishs").click(function (codeid, contents) {
        var cot = $("#cont").val();//内容

        var codeid = $(this).attr("data-code");

        $.ajax({
            url: 'ajax/CheckPassWord.ashx',
            type: 'POST',
            data: {
                "action": "pubapps",
                "codeid": codeid,
                "contents": cot
            }
        }).done(function (res) {
            if (res == "") {
                alert("评价成功");
            }
            else {
                alert(res);
            }
        })
    })

    //设为默认地址
    //设为默认地址----——修改users表中的counts_————存储过程
    $(".pc-event-d").click(function (counts) {
        var counts = $(this).attr("data-code");
        $.ajax({
            url: 'ajax/CheckPassWord.ashx',
            type: 'POST',
            data: {
                "action": "defultadress",
                "counts": counts
            }
        }).done(function (res) {
            if (res == "seccess") {
                location.reload();
            }

        })
    })



    //修改地址信息
    //修改地址信息---弹框
    $(".不知道").click(function (counts) {

        var counts = $(this).attr("data-code");
        $.ajax({
            url: 'ajax/CheckPassWord.ashx',
            type: 'POST',
            data: {
                "action": "selectadress",
                "counts": counts
            }
        }).done(function (res) {
            var obj = JSON.parse(res)
            $("#alertname").val(obj[0].name);
            $("#alertAdress").val(obj[0].adress);
            $("#alertPhone").val(obj[0].phones);//将电话赋值给文本框
          


            $("#suyes").click(function (name, adress, phone, counts) {
                var coun = obj[0].adID;
            var name = document.getElementById("alertname").value;
            var adress = document.getElementById("alertAdress").value;
            var phone = document.getElementById("alertPhone").value;
            $.ajax({
                url: 'ajax/CheckPassWord.ashx',
                type: 'POST',
                data: {
                    "action": "updateadress",
                    "name": name,
                    "adress": adress,
                    "phone": phone,
                    "counts": coun
                }
            }).done(function (res) {
               
                   // $.ajaxSetup({cache:false})
               

            })
            })


        })


    })

    
    //修改地址信息
    //修改地址信息---
    $(".upcompiles").click(function (name, adress, phone, adid) {
      
        var adid = $("#adid").html();
        var name = document.getElementById('shr').value;
        var adress = document.getElementById('dizhi').value;
        var phone = document.getElementById('shj').value;
        $.ajax({
            url: 'ajax/CheckPassWord.ashx',
            type: 'POST',
            data: {
                "action": "updateadress",
                "name": name,
                "adress": adress,
                "phone": phone,
                "adid": adid,
            }
        }).done(function (res) {
            if (res=="") {
                alert("修改收货地址信息成功")
            }


        })

    })

    //删除地址
    $(".delet").click(function (adid) {
        var adid = $(this).attr("data-code");
        $.ajax({
            url: 'ajax/CheckPassWord.ashx',
            type: 'POST',
            data: {
                "action": "deleteadress",
               
                "adid": adid,
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

    //新增收货地址
    $(".addadress").click(function (counts, addname, addadress, addphone) {
       
       
        var addname = document.getElementById('addname').value;
    
        var addadress = document.getElementById('addadress').value;
        var addphone = document.getElementById('addphone').value;
         var counts = document.getElementById('counts').value;
        $.ajax({
            url: 'ajax/CheckPassWord.ashx',
            type: 'POST',
            data: {
                "action": "addadress",

                "counts": counts,
                "addname": addname,
                "addadress": addadress,
                "addphone": addphone,
            }
        }).done(function (res) {
            if (res) {
                alert("添加成功")
                location.reload();
            }
            else {
                alert("添加失败")
                location.reload();
            }


        })
    })

    ////订单发货修改stutass=1 JS代码写在ctrl-order.aspx前台

    //根据ID删除商品   JS代码写在ctrl？？？.aspx前台
    
})

