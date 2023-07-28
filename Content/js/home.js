
(function ($) {
    "use strict";
    $.ajax({
        url: '/Home/_Coupons',
        dataType: "HTML",
        type: "POST",
        contentType: 'application/json; charset=utf-8',
        //data: JSON.stringify({ user: { name: 'Rintu', email: 'Rintu@gmial.com' } }),
        async: true,
        //processData: false,
        cache: true,
        success: function (data) {
            $('#home-copupon').html(data);
        },
        error: function (xhr) {
            debugger;
            alert('error');
        }
    })

    $(document).ready(function () {



    });

})(jQuery); 