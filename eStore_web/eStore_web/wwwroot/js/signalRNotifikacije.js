

"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/notifikacije").build();

connection.on("GetNotification", function (obj) {
    var x = generateNotification(obj);
    $("#notifikacije").prepend(x);
    incrementNotificationNumber();
});

connection.on("getAllNotifikacije", function (obj) {
    obj.forEach(element => {
        var x = generateNotification(element);
        $("#notifikacije").prepend(x);
        if (!element.otvorena) {

            incrementNotificationNumber();
        }
    });

});

function incrementNotificationNumber() {
    var x = $("#NotificationBrojac").text();
    if (x !== "") {
        var br = parseInt(x);
        var y = br + 1;

        $("#NotificationBrojac").text(y);
        return;
    }
    $("#NotificationBrojac").text("1");

}

function generateNotification(obj) {
    var o = "";
    if (!obj.otvorena) {
        o = "style='background-color:#d6e6ff'";
    }


    var element = '<a class="dropdown-item" href="' + obj.url + '" style="margin:5px; text-align:center">' +
        '<div style = "width:100%">' +
        '<p> ' + obj.poruka + ' </p>' +
        ' <p style="text-align:center;font-size:11px"> ' + obj.user + ' - ' + obj.vrijeme + '</p>' +
        '</div > </a >';
    return element;
}


connection.start().then(function () {
    console.log("connected");
    connection
        .invoke('getNotification', 20);
}).catch(function (err) {
    return console.error(err.toString());
});