function OnHomePageLoad() {
    LoadPackageSection();
}
function LoadPackageSection() {
    $("#divCategroy").empty();
    $.ajax({
        type: "Get",
        url: '/Home/GetCategorySection',
        data: null,
        datatype: "json",
        success: function (response) {
            $('#divCategroy').html(response);
        },
        complete: function () {
        }
    });
}
function LoadSubPackageSection(packageId) {
    $("#exampleModal").empty();
    $.ajax({
        type: "Get",
        url: '/Home/GetSubPackageSection?packageId=' + packageId + '',
        data: null,
        datatype: "json",
        success: function (response) {
            $('#exampleModal').html(response);
        },
        complete: function () {
            $('#exampleModal').modal()
        }
    });
}
$(document).ready(function () {
    var names_arr = ['about', 'contact', 'service', 'terms', 'privacy', 'orderdetail'];
    var url = window.location.href;
    for (var i = 0; i < names_arr.length; i++) {
        var name = names_arr[i];
        if (url.toLowerCase().indexOf(name) != -1) {
            $(".navbar-toggler").addClass("black-color");
            break;
        }
    }
});