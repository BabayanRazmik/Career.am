$("#SearchId").on("keyup", function () {
    var job = $(this).val();
    if (job.length >= 2 || job.length == 0) {
        GetSearch();
    }
});


function GetSearch() {
    var jobName = $("#SearchId").val();
    var industryName = $("#IndustryId").val();
    var locationName = $("#locationId").val();
    var typeEmployment = $("#typeEmploymentId").val();
    $.ajax({
        type: "GET",
        url: SearchUrl,
        data:
        {
            search: jobName,
            locationId: industryName,
            IndustryId: locationName,
            TypeEmploymentId: typeEmployment
        },
        success: function (data) {
            $("#ListActiveJobPartial").html(data)
        }
    })
};

$("#IndustryId").on("change", function () {
    GetSearch();
});

$("#locationId").on("change", function() {
    GetSearch();
});

$("#typeEmploymentId").on("change", function () {
    GetSearch();
});