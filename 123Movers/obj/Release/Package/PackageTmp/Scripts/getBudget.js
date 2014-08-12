$(function () {
    $('#ExpiredInfo').dataTable({ "sPaginationType": "full_numbers" });

    $('#renewBudget').click(function () {

        $.ajax({
            url: '/Budget/RenewBudget',
            type: "POST",
            dataType: "json",
            cache: false,
            success: function (data) {

            },
            error: function (xhr, ajaxOptions, thrownError) {

            }

        });

    });


});