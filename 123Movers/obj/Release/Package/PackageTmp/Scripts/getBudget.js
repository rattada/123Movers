$(function () {
    $('#ExpiredInfo').dataTable({ "sPaginationType": "full_numbers" });
    $('.renewBudget').click(function () {
        var serviceId = $(this).attr('data-id');
        $.ajax({
            url: '/Budget/RenewBudget',
            type: "POST",
            data: { 'ServiceId': serviceId },
            dataType: "json",
            cache: false,
            success: function (data) {
                if (data.success) {
                    alert('Successfully Renewed The Budget');
                    location.reload();
                }
            },
            error: function (xhr, ajaxOptions, thrownError) {
            }
        });
    });

    $('.editBudget').click(function () {
        $("body").mask('Loading...')
    });
});