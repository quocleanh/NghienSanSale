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
    var _brand = window.location.pathname.split('/').at(-1) || '';
    var _caregory = getUrlParameter('caregory') || '';

    $.ajax({
        url: '/Category/_Filter',
        dataType: "HTML",
        type: "POST",
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify(
            {
                brand: _brand,
                caregory: _caregory,
                page: '1',
                limit: '20'
            }),
        async: true,
        //processData: false,
        cache: true,
        success: function (data) {
            $('#category-copupon').html(data);
            var totalPage = $('#totalPage').val();
            debugger;
            $('.pagination_fg').bootpag({
                total: totalPage,
                page: 1,
                maxVisible: 5
            }).on('page', function (event, num) {
                filter(num, _brand, _caregory);
            });
        },
        error: function (xhr) {

        }
    })

    var filter = function (page, _brand, _caregory) {
        debugger;


        $.ajax({
            url: '/Category/_Filter',
            dataType: "HTML",
            type: "POST",
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify(
                {
                    brand: _brand,
                    caregory: _caregory,
                    page: page,
                    limit: '20'
                }),
            async: true,
            //processData: false,
            cache: true,
            success: function (data) {
                $('#category-copupon').html(data);
                var totalPage = $('#totalPage').val();
                debugger;
                $('.pagination_fg').bootpag({
                    total: totalPage,
                    page: page,
                    maxVisible: 5
                }).on('page', function (event, num) {

                });
            },
            error: function (xhr) {

            }
        })
    }

    $("#filter_categories_month_all").on('change', function () {
        if ($(this).is(':checked')) {
            $('.categories_month').attr("checked", true);
        }
        else {
            $('.categories_month').attr("checked", false);
        }
    })

    $("#btnFilter").click(function (e) {
        debugger;
        e.preventDefault();
        var categorieChecked = $.map($('input[name="filter_categories"]:checked'), function (e) {
            return e.value;
        });
        var categorieMonthChecked = $.map($('input[name="filter_categories_month"]:checked'), function (e) {
            return e.value;
        });
        $.ajax({
            type: "POST",
            url: "/Category/_Filter",
            dataType: "HTML",
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify(
                {
                    brand: _brand,
                    caregory: _caregory,
                    page: '1',
                    limit: '20',
                    categories: categorieChecked,
                    categoriesMonth: categorieMonthChecked
                }),
            async: true,
            //processData: false,
            cache: true,
            success: function (data) {
                $('#category-copupon').html(data);
                var totalPage = $('#totalPage').val();
                $('.pagination_fg').bootpag({
                    total: totalPage,
                    page: 1,
                    maxVisible: 5
                }).on('page', function (event, num) {
                    filter(num, _brand, _caregory);
                });
            },
            error: function (xhr) {

            }
        })
    })

})(jQuery); 
