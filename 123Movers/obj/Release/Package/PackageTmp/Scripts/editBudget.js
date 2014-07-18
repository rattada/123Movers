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
                var ID = val[0];
                var value = val[2];

                if (ID == 1 && val[2] == "True") {

                    $('#managearea').removeClass('btn-default');
                    $('#managearea').addClass('btn-warning');
                }
                else
                    if (ID == 2 && val[2] == "True") {
                        $('#destzipcodes').removeClass('btn-default');
                        $('#destzipcodes').addClass('btn-warning');
                    }

                    else if (ID == 3 && val[2] == "True") {
                        $('#distance').removeClass('btn-default');
                        $('#distance').addClass('btn-warning');
                    }
                    else
                        if (ID == 4 && val[2] == "True") {
                            $('#leadlimit').removeClass('btn-default');
                            $('#leadlimit').addClass('btn-warning');
                        }

                        else
                            if (ID == 5 && val[2] == "True") {
                                $('#movewight').removeClass('btn-default');
                                $('#movewight').addClass('btn-warning');
                            }
                            else
                                if (ID == 6 && val[2] == "True") {
                                    $('#geography').removeClass('btn-default');
                                    $('#geography').addClass('btn-warning');

                                }

            });

        },
        error: function (xhr, ajaxOptions, thrownError) {

        }
    });
});