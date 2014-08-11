$(function () {

    var serviceId = $('#ddlServiceID').val();
    if (serviceId != '') {
        GetAvailableAreas(serviceId);
    } else { $('#ddlareaCode').attr("disabled", true); }

    function GetAvailableAreas(serviceId) {
        $.ajax({
            url: '/SpecificOriginAreaCodes/GetAvailSpcfcOriginDestAreas',
            type: "GET",
            data: { 'serviceId': serviceId },
            dataType: "json",
            cache: false,
            success: function (data) {
                var strigifyJson = JSON.stringify(data);
                var json = $.parseJSON(strigifyJson);
                var options;
                $.each(json, function (i, val) {
                    if (options == undefined) {
                        options = '<option value="' + val[0] + '">' + val[1] + ' - ' + val[0] + '</option>';
                    } else {
                        options += '<option value="' + val[0] + '">' + val[1] + ' - ' + val[0] + '</option>';
                    }
                });

                if (json.length > 0) {
                    options = '<option value="">' + "--Choose One--" + '</option>' + options;
                    $('#ddlareaCode').html(options);
                    $('#destAreaCodes').html('');
                    $('#originAreaCodes').html('');
                    $('#ddlareaCode').attr("disabled", false);
                }
                else {
                    $('#ddlareaCode').html('');
                    $('#destAreaCodes').html('');
                    $('#originAreaCodes').html('');
                    $('#ddlareaCode').attr("disabled", true);
                }
            },
            error: function (xhr, ajaxOptions, thrownError) {
            }
        });
    }
    function GetSelectedAreas(serviceId, spfcareacode) {
        //OriginAreaCodes
        $.ajax({
            url: '/SpecificOriginAreaCodes/GetCompanySpcfcOriginDestAreas',
            type: "GET",
            data: { 'serviceId': serviceId, "spcfcareacode": spfcareacode, "originAreaCode": false },
            dataType: "json",
            cache: false,
            success: function (data) {
                var strigifyJson = JSON.stringify(data);
                var json = $.parseJSON(strigifyJson);
                var options;
                var destoptions;
                var areacode = $.trim($('#ddlareaCode').val());
                $.each(json, function (i, val) {
                    if (val[2] == 'Table')
                    {
                        if (areacode != val[0]) {
                            if (options == undefined) {
                                options = '<option value="' + val[0] + '">' + val[1] + ' - ' + val[0] + '</option>';
                            } else {
                                options += '<option value="' + val[0] + '">' + val[1] + ' - ' + val[0] + '</option>';
                            }
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
        var originAreaCode = $('#ddlareaCode').val();
        var selected = [];
        $('#originAreaCodes :selected').each(function (i, el) {
            selected[i] = $(this).val();
        });
        if (serviceId == '') {
            alert("Please Select Service Type");
            $('#ddlServiceID').focus();
            return false;
        }
        if (originAreaCode == '') {

            alert("Please Select Origin AreaCode");
            $('#ddlareaCode').focus();
            return false;
        }
        if (selected.length == 0) {

            alert("Please Select any Option to add");
            $('#originAreaCodes').focus();
            return false;
        }
        var data_to_send = JSON.stringify(selected);
        $.ajax({
            url: '/SpecificOriginAreaCodes/AddCompanySpcfcOriginDestAreaCodes',
            type: "POST",
            data: { 'serviceId': serviceId, "spcfcareacode": originAreaCode, 'areaCodes': data_to_send },
            success: function (data) {
                GetSelectedAreas(serviceId, originAreaCode);
                alert("Area Codes added Successfully");
            },
            error: function (xhr, ajaxOptions, thrownError) {
            }
        });
    });
    $('#remove').click(function () {
        var serviceId = $('#ddlServiceID').val();
        var originAreaCode = $('#ddlareaCode').val();
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
            url: '/SpecificOriginAreaCodes/DeleteCompanySpcfcOriginDestAreaCodes',
            type: "POST",
            data: { 'serviceId': serviceId, "spcfcareacode": originAreaCode, areaCodes: data_to_send },
            success: function (data) {
                GetSelectedAreas(serviceId, originAreaCode);
                alert("Area Codes removed Successfully");
            },
            error: function (xhr, ajaxOptions, thrownError) {
            }
        });
    });
    $("body").parent().on("change", "#ddlServiceID", function () {
        var serviceId = $('#ddlServiceID').val();
        if (serviceId != '') {
            GetAvailableAreas(serviceId);
            $('#ddlareaCode').attr("disabled", false);
        }
        else {
            $('#ddlareaCode').attr("disabled", true)
            $('#ddlareaCode').html('');
            $('#destAreaCodes').html('');
            $('#originAreaCodes').html('');
        }
    });
    $("body").parent().on("change", "#ddlareaCode", function () {
        var serviceId = $('#ddlServiceID').val();
        var originAreaCode = $('#ddlareaCode').val();
        if (originAreaCode != '') {
            GetSelectedAreas(serviceId, originAreaCode);
        }
        else {
            $('#originAreaCodes').html('');
            $('#destAreaCodes').html('');
        }
    });
});
