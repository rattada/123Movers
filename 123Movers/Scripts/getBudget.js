$(function () {
    $('#ExpiredInfo').dataTable({ "sPaginationType": "full_numbers" });

    $('#activeRenew').click(function () {
        var serviceId = $('#activeRenew').attr('data-id');
        $.ajax({
            url: '/Budget/RenewBudget',
            type: "POST",
            data: {'ServiceId' : serviceId},
            dataType: "json",
            cache: false,
            success: function (data) {
                debugger;
                if (data.success) {
                    alert('Renewed The Budget');
                    $('#activeRenew').attr('disabled', true)
                }
            },
            error: function (xhr, ajaxOptions, thrownError) {

            }

        });

    });


});