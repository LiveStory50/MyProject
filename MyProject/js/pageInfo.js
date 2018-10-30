$(function () {
    //点击下一页
    $("#PageNext").click(function(){
        $.ajax({
            url: 'ajax/PageInfor.ashx',
            type: 'POST',
            data: {
                "action": "pageNextCtrlAppraise",
                
            }.done(function(res){

            })

        })
    })



})