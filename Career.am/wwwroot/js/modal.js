$("#logout").click(function (log) {
    log.preventDefault();
    $("#logoutModal").show();
});

$(".hid").click(function (log) {
    log.preventDefault();
    $("#logoutModal").hide();
});