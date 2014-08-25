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
                    $('#btnfilter').removeClass('btn-default');
                    $('#btnfilter').addClass('btn-primary');
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
        $("body").mask('Loading...');
        $.ajax({
            url: '/Budget/GetBudgetFilterInfo',
            type: 'GET',
            data: { 'serviceId': ServiceId },
            dataType: 'Json',
            cache: false,
            success: function (data) {
                var strigifyJson = JSON.stringify(data);
                var json = $.parseJSON(strigifyJson);
                var table = "<table class='table table-bordered table-striped table-hover' id ='tblFilterInfo'> <thead><tr><th class='header text-center'>ServiceID</th> <th class='header text-center'>AreaCode</th><th class='header text-center'>IsForce</th><th class='header text-center'>IsDest.AreaCode</th><th class='header text-center'>IsDest.ZipCode</th><th class='header text-center'>IsMoveDistance</th><th class='header text-center'>IsMoveWeight</th><th class='header text-center'>IsOriginZipCode</th><th class='header text-center'>IsSpcfcOriginDestAreacode</th><th class='header text-center'>IsSpcfcOriginDestState</th></tr></thead><tbody>";
                if (json.length > 0) {
                    jQuery.each(json, function (i, val) {
                        var ServiceId = (val[1] == 1009) ? "Local" : "Long";
                        table += "<tr><td class='text-center'>" + ServiceId + "</td><td class='text-center'>" + val[2] + "</td><td class='text-center'>" + val[3] + "</td><td class='text-center'>" + val[4] + "</td><td class='text-center'>" + val[5] + "</td><td class='text-center'>" + val[6] + "</td><td class='text-center'>" + val[7] + "</td><td class='text-center'>" + val[8] + "</td><td class='text-center'>" + val[9] + "</td><td class='text-center'>" + val[10] + "</td></tr>";
                    });
                    table += "</tbody></table>";
                    $('#divfilter').html(table);
                    $('#tblFilterInfo').dataTable({ "sPaginationType": "full_numbers" });
                    $("body").unmask();
                }
            },
            error: function (xhr, ajaxOptions, thrownError) {
            }
        });
    });
});

$(window).load(function () {
    jQuery(document).ready(function ($) {

        $('[data-popup-target]').click(function () {
            $('html').addClass('overlay');
            var activePopup = $(this).attr('data-popup-target');
            $(activePopup).addClass('visible');

        });

        $(document).keyup(function (e) {
            if (e.keyCode == 27 && $('html').hasClass('overlay')) {
                clearPopup();
            }
        });

        $('.popup-exit').click(function () {
            clearPopup();

        });

        $('.popup-overlay').click(function () {
            clearPopup();
        });

        function clearPopup() {
            $('.popup.visible').addClass('transitioning').removeClass('visible');
            $('html').removeClass('overlay');

            setTimeout(function () {
                $('.popup').removeClass('transitioning');
            }, 200);
        }

    });
});