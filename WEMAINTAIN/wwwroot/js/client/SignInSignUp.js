$(function () {
    $(".Register").click(function () {
        $(".form-signin").toggleClass("form-signin-left");
        $(".form-signup").toggleClass("form-signup-left");
        $(".frame").toggleClass("frame-long");
        $(".signup-inactive").toggleClass("signup-active");
        $(".signin-active").toggleClass("signin-inactive");
        $(".forgot").toggleClass("forgot-left");
        $(this).removeClass("idle").addClass("active");
    });
});

$(function () {
    $(".btn-signup").click(function () {
        $(".nav").toggleClass("nav-up");
        $(".form-signup-left").toggleClass("form-signup-down");
        $(".frame").toggleClass("frame-short");
    });
});

$(function () {
    $(".btn-signin").click(function () {
        //$(".btn-animate").toggleClass("btn-animate-grow");
        $(".frame").removeClass("frame-long");
        $(".frame").toggleClass("frame-short");
        $(".forgot").toggleClass("forgot-fade");
    });
});