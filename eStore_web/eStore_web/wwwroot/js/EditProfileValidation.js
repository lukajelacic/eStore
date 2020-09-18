
    jQuery.extend(jQuery.validator.messages, {
        required: "Polje obavezno.",
    remote: "Please fix this field.",
    email: "Unesite validnu e-mail adresu.",
    url: "Unesite validan linl",
    date: "Unesite validan datum.",
    dateISO: "Please enter a valid date (ISO).",
    number: "Unesite validan broj.",
    digits: "Unesite samo brojeve.",
    creditcard: "Please enter a valid credit card number.",
    equalTo: "Vrijednosti nisu jednake.",
    accept: "Please enter a value with a valid extension.",
        maxlength: jQuery.validator.format("Unesite manje od {0} znakova."),
        minlength: jQuery.validator.format("Unesite vise od {0} znakova."),
        rangelength: jQuery.validator.format("Please enter a value between {0} and {1} characters long."),
        range: jQuery.validator.format("Please enter a value between {0} and {1}."),
        max: jQuery.validator.format("Please enter a value less than or equal to {0}."),
        min: jQuery.validator.format("Please enter a value greater than or equal to {0}.")
});


//https://laracasts.com/discuss/channels/javascript/variable-outside-ajax-request
    jQuery.validator.addMethod("IsAccountNameValid", function () {


        var AccountName = $("#AccountName").val();
    var OsobaID = $("#OsobaID").val();
    OsobaID = parseInt(OsobaID);
    var result;
        $.ajax({
        type: "GET",
    url: "/Profile/IsAccountNameValid?OsobaID=" + OsobaID + " &AccountName=" + AccountName,
    datatype: "json",
    async: false,
            success: function (data) {
        result = data;

    }
});

return result;
}, "Dati naziv racuna je vec iskoristen");



    jQuery.validator.addMethod("IsEmailValid", function () {


        var Email = $("#Email").val();
    var OsobaID = $("#OsobaID").val();
    OsobaID = parseInt(OsobaID);
    var result;
        $.ajax({
        type: "GET",
    url: "/Profile/IsEmailValid?OsobaID=" + OsobaID + "&Email=" + Email,
    datatype: "json",
    async: false,
            success: function (data) {
        result = data;

    }
});
return result;
}, "Dati Email je vec iskoristen");


    jQuery.validator.addMethod("lettersonly", function (value, element) {
        return this.optional(element) || /^[a-z]+$/i.test(value);
}, "Moguc unos samo slova");


    function ukloni(ele) {


        $(ele).parents("#uno").fadeOut();
    console.log(1);
}
    $(document).ready(function () {





        $(".EditForm").validate({

            rules:
            {
                Ime:
                {

                    required: false,
                    maxlength: 50,
                    lettersonly: true

                },
                Prezime:
                {
                    required: false,
                    maxlength: 50,
                    lettersonly: true
                },
                Email:
                {
                    required: true,
                    maxlength: 30,
                    IsEmailValid: true,
                    email: true
                },

                AccountName:
                {
                    required: true,
                    maxlength: 30,
                    IsAccountNameValid: true
                },
                UserName:
                {
                    required: true,
                    minlength: 5,
                    maxlength:25

                }
            },
            submitHandler: function (form) {
                form.submit();
            }
        });






    });
