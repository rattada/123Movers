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

    //$('#btnfilter').click(function () {
    //    $("body").mask('Loading...');
    //    $.ajax({
    //        url: '/Budget/GetBudgetFilterInfo',
    //        type: 'GET',
    //        data: { 'serviceId': ServiceId },
    //        dataType: 'Json',
    //        cache: false,
    //        success: function (data) {
    //            var strigifyJson = JSON.stringify(data);
    //            var json = $.parseJSON(strigifyJson);
    //            var temp;
    //            var table = "<table class='table table-bordered table-striped table-hover' id ='tblFilterInfo'> <thead><tr><th class='header text-center'>Service</th> <th class='header text-center'>Area Code</th><th class='header text-center'>Forced</th><th class='header text-center'>Destination Area</th><th class='header text-center'>Destination Zip</th><th class='header text-center'>Distance</th><th class='header text-center'>Weight</th><th class='header text-center'>Origin Zip</th><th class='header text-center'>Specific Origin Dest. By Area</th><th class='header text-center'>Specific Origin Dest. By State</th></tr></thead><tbody>";
    //            if (json.length > 0) {
    //                jQuery.each(json, function (i, val) {
    //                    var ServiceId = (val[1] == 1009) ? "Local" : "Long";

    //                    if (val[3] == 0) {
    //                        val[3] = "No"
    //                    }
    //                    else {
    //                        val[3] = "Yes"
    //                    }

    //                    if (val[4] == 0) {
    //                        val[4] = "No"
    //                    }
    //                    else {
    //                        val[4] = "Yes"
    //                    }
    //                    if (val[5] == 0) {
    //                        val[5] = "No"
    //                    } else {
    //                        val[5] = "Yes"
    //                    }
    //                    if (val[6] == 0) {
    //                        val[6] = "No"
    //                    } else {
    //                        val[6] = "Yes"
    //                    }
    //                    if (val[7] == 0) {
    //                        val[7] = "No"
    //                    } else {
    //                        val[7] = "Yes"
    //                    }
    //                    if (val[8] == 0) {
    //                        val[8] = "No"
    //                    } else {
    //                        val[8] = "Yes"
    //                    }
    //                    if (val[9] == 0) {
    //                        val[9] = "No"
    //                    } else {
    //                        val[9] = "Yes"
    //                    }
    //                    if (val[10] == 0) {
    //                        val[10] = "No"
    //                    }
    //                    else {

    //                        val[10] = "Yes"
    //                    }

    //                    table += "<tr><td class='text-center'>" + ServiceId + "</td><td class='text-center'>" + val[2] + "</td><td class='text-center'>" + val[3] + "</td><td class='text-center'>" + val[4] + "</td><td class='text-center'>" + val[5] + "</td><td class='text-center'>" + val[6] + "</td><td class='text-center'>" + val[7] + "</td><td class='text-center'>" + val[8] + "</td><td class='text-center'>" + val[9] + "</td><td class='text-center'>" + val[10] + "</td></tr>";
    //                });
    //                table += "</tbody></table>";


    //                $('#divfilter').html(table);



    //                //$('#tblFilterInfo').dataTable({ "sPaginationType": "full_numbers" });
    //                $("body").unmask();
    //            }
    //        },
    //        error: function (xhr, ajaxOptions, thrownError) {
    //        }
    //    });
    //});
});
