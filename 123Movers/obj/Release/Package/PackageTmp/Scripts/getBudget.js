$(function () {
    var companyID = $("#tdCompanyID").text().split(":");
    companyID = companyID[1];
    $('#ExpiredInfo').dataTable({ "sPaginationType": "full_numbers" });

    $('.renewBudget').click(function () {
        var serviceId = $(this).attr('data-id');
        $.ajax({
            url: '/Budget/RenewBudget',
            type: "POST",
            data: { 'companyID': companyID, 'ServiceId': serviceId },
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