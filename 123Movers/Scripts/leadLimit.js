﻿$(function () {
    $(".TitleStyle").text("Lead Limit");
    $('#tblLeadLimit tbody tr').each(function () {
        $(this).find(".classCheckVal:input[type=text]").each(function () {
            if ($(this).val() != 0 && $(this).val() != '')
                $(this).attr("disabled", false);
        });
    });
    $("#saveleads").attr('disabled', true);
    $("body").on("keypress", "input", function (event) {
        var key = event.which;
        if (!(key >= 48 && key <= 57))
            event.preventDefault();
        else
            $("#saveleads").removeAttr('disabled');
    });
    $("#saveleads").click(function () {
        var leadLimitData = [];
        var filleddata = [];
        $('#tblLeadLimit tbody tr').each(function () {
            //Checking for Only checked checkboxes
            $(this).find('.chkSelectClass:checkbox:checked').each(function () {
                var row = $(this).closest('tr');
                var columns = row.find('td');
                $.each(columns, function (i, item) {
                    if (i == 1) {
                        var service = $.trim(item.textContent);
                        if (service == 'Local') ServiceId = 1009;
                        else if (service == 'Long') ServiceId = 1000;
                        else ServiceId = null;
                    }
                    else if (i == 2) {
                        if ($.trim(item.textContent) == 'Default') AreaCodes = null;
                        else AreaCodes = $.trim(item.textContent);
                    }
                    else if (i == 3) {
                        LeadFrequency = $.trim($(this).closest('td').find("input:text").val());
                    }
                    else if (i == 4) {
                        DailyLeadLimit = $.trim($(this).closest('td').find("input:text").val());
                    }
                    else if (i == 5) {
                        MonthlyLeadLimit = $.trim($(this).closest('td').find("input:text").val());
                    }
                    else if (i == 6) {
                        TotalLeadLimit = $.trim($(this).closest('td').find("input:text").val());
                    }
                });
                leadLimitData =
                          [{
                              AreaCodes: AreaCodes,
                              ServiceId: ServiceId,
                              LeadFrequency: LeadFrequency,
                              DailyLeadLimit: DailyLeadLimit,
                              MonthlyLeadLimit: MonthlyLeadLimit,
                              TotalLeadLimit: TotalLeadLimit
                          }]
                filleddata.push(leadLimitData);
            });
        });
        //Saving the  entire data in DB with Ajax call
        var dataToSend = JSON.stringify(filleddata);
        if (dataToSend != 0) {
            $.ajax({
                url: '/LeadLimit/LeadLimit',
                cache: false,
                type: 'POST',
                dataType: "json",
                data: JSON.stringify(filleddata),// Comverting the list items to JSON format(Because POST method accepts  only JSON data)
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    alert('Successfully Updated:');
                    location.reload();
                },
                error: function () {
                    alert('Service call failed');
                }
            });
        }
        else {
            alert("Please Select atleast one lead");
            return false;
        }
    });
    $("body #tblLeadLimit ").parent().on('change', ".chkInnerClass", function () {
        if ($(this).prop('checked') == true) {
            $(this).closest("td").find("input[type=text]").prop("disabled", false);
            if ($(this).closest("td").find("input[type=text]").val() == "0")
                $(this).closest("td").find("input[type=text]").val('');
        }
        else {
            $(this).closest("td").find("input[type=text]").prop("value", "");
            $(this).closest("td").find("input[type=text]").prop("disabled", true);
        }
    });
    $(".chkBoth").on("change", function () {
        if ($(this).prop('checked') == true) {
            $(".chkLocalLong").prop("checked", false);
            $(".txtFrqLocalLong").attr("disabled", true);

        };
    });
    $(".chkLocalLong").on("change", function () {
        $(".chkBoth").prop("checked", false);
        $(".txtFrqBoth").attr("disabled", true);
    });
    $("body #tblLeadLimit ").parent().on('change', ".chkSelectClass", function () {
        if ($(this).prop('checked') == true) {
            //Enable the frequency texboxes
            $(this).closest('tr').find(".freqEnable").attr("disabled", false);
            $("#saveleads").removeAttr('disabled');
        }
        else {
            $(this).closest('tr').find(".freqEnable").attr("disabled", true);
        }
    });
});