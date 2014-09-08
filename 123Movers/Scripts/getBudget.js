$(function () {
 
    $('#ExpiredInfo').dataTable({ "sPaginationType": "full_numbers" });

    $('.renewBudget').click(function () {
        debugger;
        var serviceId = $(this).attr('data-id');
        var companyId = $(this).attr('data-cid');
        $.ajax({
            url: '/Budget/RenewBudget',
            type: "POST",
            data: { 'CompanyId': companyId, 'ServiceId' : serviceId},
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