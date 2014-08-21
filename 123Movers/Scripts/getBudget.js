$(function () {
    var activeCount = $("#ActiveInfo > tbody > tr").length;
    var expCount = $("#ExpiredInfo > tbody > tr").length;
    var text = $("#ActiveInfo > tbody > tr > td").text().trim();
    var val = 'No record(s) found';
    if (text != val) {
        if (activeCount == 1) {
            $('#ExpiredInfo tr:last').remove();
        }
        else if (activeCount == 2) {
            $('#ExpiredInfo tr:last').remove();
            $('#ExpiredInfo tr:last').remove();
        }
        $('#ExpiredInfo').dataTable({ "sPaginationType": "full_numbers" });
    }

    $('.renewBudget').click(function () {
        var serviceId = $(this).attr('data-id');
        $.ajax({
            url: '/Budget/RenewBudget',
            type: "POST",
            data: {'ServiceId' : serviceId},
            dataType: "json",
            cache: false,
            success: function (data) {
                if (data.success) {
                    alert('Renewed The Budget');
                    location.reload();
                }
            },
            error: function (xhr, ajaxOptions, thrownError) {
            }
        });
    });

    //$('#expireRenew').click(function () {
    //    var serviceId = $('#expireRenew').attr('data-id');
    //    $.ajax({
    //        url: '/Budget/RenewBudget',
    //        type: "POST",
    //        data: { 'ServiceId': serviceId },
    //        dataType: "json",
    //        cache: false,
    //        success: function (data) {
    //            debugger;
    //            if (data.success) {
    //                alert('Renewed The Budget');
    //                $('#expireRenew').attr('disabled', true)
    //            }
    //        },
    //        error: function (xhr, ajaxOptions, thrownError) {
    //        }
    //    });
    //});
});