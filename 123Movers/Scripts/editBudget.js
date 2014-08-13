$(function () {
    var ddlservice = $("#services").val();
    var ServiceId = (ddlservice == 1009) ? "1009" : (ddlservice == 1000) ? "1000" : null;
    $.ajax({
        url: '/Budget/GetFilterResult',
        type: "GET",
        data: { 'serviceId': ServiceId },
        dataType: "json",
        cache: false,
        success: function (data) {
            var strigifyJson = JSON.stringify(data);
            var json = $.parseJSON(strigifyJson);

            jQuery.each(json, function (i, val) {
                var id = val[0];
                var value = val[2];

                if (id == 1 && val[2] == "True") {
                    $('#managearea').removeClass('btn-default');
                    $('#managearea').addClass('btn-warning');
                }
                else if (id == 2 && val[2] == "True") {
                    $('#destarea').removeClass('btn-default');
                    $('#destarea').addClass('btn-warning');
                }
                else if (id == 3 && val[2] == "True") {
                    $('#destareazip').removeClass('btn-default');
                    $('#destareazip').addClass('btn-warning');
                }
                else if (id == 4 && val[2] == "True") {
                    $('#destzip').removeClass('btn-default');
                    $('#destzip').addClass('btn-warning');
                }
                else if (id == 5 && val[2] == "True") {
                    $('#distance').removeClass('btn-default');
                    $('#distance').addClass('btn-warning');
                }
                else if (id == 6 && val[2] == "True") {
                    $('#leadlimit').removeClass('btn-default');
                    $('#leadlimit').addClass('btn-warning');
                }
                else if (id == 7 && val[2] == "True") {
                    $('#movewight').removeClass('btn-default');
                    $('#movewight').addClass('btn-warning');
                }

                else if (id == 8 && val[2] == "True") {
                    $('#originzip').removeClass('btn-default');
                    $('#originzip').addClass('btn-warning');
                }
                else if (id == 9 && val[2] == "True") {
                    $('#specificAreas').removeClass('btn-default');
                    $('#specificAreas').addClass('btn-warning');
                }
                else if (id == 10 && val[2] == "True") {
                    $('#specificStates').removeClass('btn-default');
                    $('#specificStates').addClass('btn-warning');
                }
                if ($('#managearea').hasClass('btn-default')) {
                    $('.clsdisable').attr("disabled", "disabled");
                    return false;
                }
            });
        },
        error: function (xhr, ajaxOptions, thrownError) {
        }
    });
    $("#save").attr("disabled", true);
    $('body').on('change keyup keydown', 'input, textarea, select', function (e) {
        $("#save").attr("disabled", false);
    });

    $('#btnfilter').click(function () {
        $.ajax({
            url: '/Budget/GetBudgetFilterInfo',
            type: 'GET',
            data: { 'serviceId': ServiceId },
            dataType: 'Json',
            cache: false,
            success: function (data) {
                var strigifyJson = JSON.stringify(data);
                var json = $.parseJSON(strigifyJson);
               // $('#divfilter').html(json);
            },
            error: function (xhr, ajaxOptions, thrownError) {
            }
        });
    });

});