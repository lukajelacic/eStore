$(document).ready(function () { 


    $(".close").click(function () {
        
        $(".mReportWindowBg").css("display", "none");
    });
    $(".mReportBtn").click(function () {

        $(".mReportWindowBg").css("display", "block");

        var IgraID = $($(this).siblings()[5]).html();
        var KupacID = $($(this).siblings()[6]).html();
        var text = $("#text").val();
        var mReportBtn = $(this);
        console.log(IgraID);
        console.log(KupacID);
        console.log(text);

        $("#report").click(function () {
            $.get("/Profile/RequestRefund?KupacID=" + KupacID + "&IgraID=" + IgraID + "&text=" + text, function (data) {


                
                mReportBtn.css("display", "none");
                $(".mReportWindowBg").css("display", "none");
                

            });
           
        });
 
       
        
    });

});
