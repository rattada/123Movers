$(function () {

    var serviceId = $('#ddlServiceID').val();
    if (serviceId != '') {
        GetSelectedAreas(serviceId);
    } else { $('#ddlareaCode').attr("disabled", true); }

 
    function GetSelectedAreas(serviceId) {
        $.ajax({
            url: '/DestinationAreaCode/GetCompanyDestAreas',
            type: "GET",
            data: { 'serviceId': serviceId},
            dataType: "json",
            cache: false,
            success: function (data) {
                var strigifyJson = JSON.stringify(data);
                var json = $.parseJSON(strigifyJson);
                var options;
                var destoptions;
                $.each(json, function (i, val) {
                    if (val[2] == 'Table') {
                        if (options == undefined) {
                            options = '<option value="' + val[0] + '">' + val[1] + ' - ' + val[0] + '</option>';
                        } else {
                            options += '<option value="' + val[0] + '">' + val[1] + ' - ' + val[0] + '</option>';
                        }
                    } else {
                        if (destoptions == undefined) {
                            destoptions = '<option value="' + val[0] + '">' + val[1] + ' - ' + val[0] + '</option>';
                        } else {
                            destoptions += '<option value="' + val[0] + '">' + val[1] + ' - ' + val[0] + '</option>';
                        }
                    }
                });

                if (options != undefined) {
                    $('#originAreaCodes').html(options);
                }
                else {
                    $('#originAreaCodes').html('');
                }

                if (destoptions != undefined) {
                    $('#destAreaCodes').html(destoptions);
                }
                else {
                    $('#destAreaCodes').html('');
                }

            },
            error: function (xhr, ajaxOptions, thrownError) {
            }
        });
    }
    $('#add').click(function () {
        var serviceId = $('#ddlServiceID').val();
        var selected = [];
        $('#originAreaCodes :selected').each(function (i, el) {
            selected[i] = $(this).val();
        });
        if (serviceId == '') {
            alert("Please Select Service Type");
            $('#ddlServiceID').focus();
            return false;
        }
        if (selected.length == 0) {

            alert("Please Select any Option to add");
            $('#originAreaCodes').focus();
            return false;
        }
        var data_to_send = JSON.stringify(selected);
        $.ajax({
            url: '/DestinationAreaCode/AddCompanyDestAreaCodes',
            type: "POST",
            data: { 'serviceId': serviceId, 'areaCodes': data_to_send },
            success: function (data) {
                GetSelectedAreas(serviceId);
                alert("Area Codes added Successfully");
            },
            error: function (xhr, ajaxOptions, thrownError) {
            }
        });
    });

    $('#remove').click(function () {
        var serviceId = $('#ddlServiceID').val();
        var selected = [];
        $('#destAreaCodes :selected').each(function (i, el) {
            selected[i] = $(this).val();
        });
        if (selected.length == 0) {
            alert("Please Select any Option to Remove")
            $('#destAreaCodes').focus();
            return false;
        }
        var data_to_send = JSON.stringify(selected);
        $.ajax({
            url: '/DestinationAreaCode/DeleteCompanyDestAreaCodes',
            type: "POST",
            data: { 'serviceId': serviceId, 'areaCodes': data_to_send },
            success: function (data) {
                GetSelectedAreas(serviceId);
                alert("Area Codes removed Successfully");
            },
            error: function (xhr, ajaxOptions, thrownError) {
            }
        });
    });

    $("body").parent().on("change", "#ddlServiceID", function () {
        var serviceId = $('#ddlServiceID').val();
        if (serviceId != '') {
            GetSelectedAreas(serviceId);
            $('#ddlareaCode').attr("disabled", false);
        }
        else {
            $('#ddlareaCode').attr("disabled", true)
            $('#ddlareaCode').html('');
            $('#destAreaCodes').html('');
            $('#originAreaCodes').html('');
        }
    });

});
