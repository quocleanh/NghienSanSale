!function (e, n, t) {
    "use strict";
    var o = "https://fonts.googleapis.com/css?family=Poppins:300,400,500,600,700&display=swap", r = "__3perf_googleFonts_c2536"; function c(e) { (n.head || n.body).appendChild(e) } function a() { var e = n.createElement("link"); e.href = o, e.rel = "stylesheet", c(e) } function f(e) { if (!n.getElementById(r)) { var t = n.createElement("style"); t.id = r, c(t) } n.getElementById(r).innerHTML = e } e.FontFace && e.FontFace.prototype.hasOwnProperty("display") ? (t[r] && f(t[r]), fetch(o).then(function (e) { return e.text() }).then(function (e) {
        return e.replace(/@font-face {/g, "@font-face{font-display:swap;")
    }).then(function (e) { return t[r] = e }).then(f).catch(a)) : a()
}(window, document, localStorage);
(function ($) {
    "use strict"; 
    $('body').delegate('.card-cont .show-code', 'click', function () {
        var primary_link = $(location).attr('href');
        var ex_link = $(this).attr('data-ex-link');
        var code_ = $(this).attr('data-code');
        var id_ = $(this).attr('data-id');
        navigator.clipboard.writeText(code_); 
        $('#' + id_).hide('fast')
        $('#cpc-' + id_).text(code_);
        $('#cpc-' + id_).addClass('active');
        $.toast({
            text: "Sao chép thành công. Hệ thống sẽ chuyến bạn đến trang khuyến mãi", // Text that is to be shown in the toast
            //heading: 'Note', // Optional heading to be shown on the toast
            showHideTransition: 'fade', // fade, slide or plain
            allowToastClose: false, // Boolean value true or false
            hideAfter: 2000, // false to make it sticky or number representing the miliseconds as time after which toast needs to be hidden
            stack: 5, // false if there should be only one toast at a time or a number representing the maximum number of toasts to be shown at a time
            position: 'mid-center', // bottom-left or bottom-right or bottom-center or top-left or top-right or top-center or mid-center or an object representing the left, right, top, bottom values
            bgColor: '#ededed',  // Background color of the toast
            textColor: '#000000',  // Text color of the toast
            textAlign: 'center',  // Text alignment i.e. left, right or center
            loader: true,  // Whether to show loader or not. True by default
            loaderBg: '#9EC600',  // Background color of the toast loader
            beforeShow: function () { navigator.clipboard.writeText(code_); }, // will be triggered before the toast is shown
            afterShown: function () { }, // will be triggered after the toat has been shown
            beforeHide: function () { }, // will be triggered before the toast gets hidden
            afterHidden: function () { window.open(ex_link, '_blank'); }  // will be triggered after the toast has been hidden
        }); 
        return false;
    }); 
})(jQuery);
