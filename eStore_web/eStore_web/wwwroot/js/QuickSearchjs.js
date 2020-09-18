
    // https://stackoverflow.com/questions/1909441/how-to-delay-the-keyup-handler-until-the-user-stops-typing
    function delay(callback, ms) {
        var timer = 0;
        return function () {

            var context = this, args = arguments;
    clearTimeout(timer);
            timer = setTimeout(function () {
        callback.apply(context, args);
    }, ms || 0);
};
}

    $(document).ready(function () {



        $("#search").focusout(function () {

            setTimeout(function () {
                $(".SearchContent").slideUp();
                $("#search").val("");

            }, 200);


        });
    $("#search").keyup(delay(function () {

            var searchValue = $("#search").val();
    console.log(searchValue);
            if (searchValue.length >= 3) {

        $(".SearchContent").css("display", "block");
    $(".SearchContent").html("<br><div class='loader'></div><br>");

                if ($.active > 0) {

            $(document).ajaxStop(function () {
                console.log("New request-cancel old one");
            });
        }

                $.get("/Kupac/Search?pretraga=" + searchValue, function (data) {
            $(".SearchContent").slideDown(500);
        $(".SearchContent").html(data);
                    $(".SearchContent").css("display", "none");
                    $(".SearchContent").slideDown(500);
    });

}
}, 500));

});
