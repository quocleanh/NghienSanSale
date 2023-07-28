var getUrlParameter = function getUrlParameter(sParam) {
    var sPageURL = window.location.search.substring(1),
        sURLVariables = sPageURL.split('&'),
        sParameterName,
        i;

    for (i = 0; i < sURLVariables.length; i++) {
        sParameterName = sURLVariables[i].split('=');

        if (sParameterName[0] === sParam) {
            return sParameterName[1] === undefined ? true : decodeURIComponent(sParameterName[1]);
        }
    }
    return false;
};

(function ($) {
    "use strict";
    var _url = getUrlParameter('url');
    $.ajax({
        url: '/Search/_SearchCoupon?url=' + _url,
        dataType: "HTML", 
        type: "POST",
        contentType: 'application/json; charset=utf-8',
        //data: JSON.stringify({ url: _url }),
        async: true,
        //processData: false,
        cache: true,
        success: function (data) {
            $('#search-copupon').html(data);
        },
        error: function (xhr) {
            debugger;
            alert('error');
        }
    })

    $(document).ready(function () {

    });

   
})(jQuery);