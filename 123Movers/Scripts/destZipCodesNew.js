$(function () {
    var serviceId = $('#ddlServiceID').val();
    if (serviceId != '') {
        GetAvailableAreas(serviceId);
    } else { $('#ddlareaCode').attr("disabled", true); }


    function GetAvailableZipCodes() {
        var serviceId = $('#ddlServiceID').val();
        var extAreaCode = $('#ddlexstngareaCode').val();
        var destAreaCode = $('#ddldestareaCode').val();

        if (extAreaCode == '') { extAreaCode = destAreaCode; }

        $.ajax({
            url: '/DestZipCode/GetCompanyAreasDestinationZipCodes',
            type: "GET",
            data: { 'serviceId': serviceId, 'areaCode': destAreaCode },
            dataType: "json",
            cache: false,
            success: function (data) {
                var strigifyJson = JSON.stringify(data);
                var json = $.parseJSON(strigifyJson);
                var options;
                $.each(json, function (i, val) {
                    if (options == undefined) {
                        options = '<option value="' + val[0] + '">' + val[0] + '</option>';
                    } else {
                        options += '<option value="' + val[0] + '">' + val[0] + '</option>';
                    }
                    
                });
                if (json.length > 0) {
                    $('#destZipCodes').html(options);
                }
                else {
                    $('#destZipCodes').html('');
                }
            },
            error: function (xhr, ajaxOptions, thrownError) {
            }
        });

        $.ajax({
            url: '/DestZipCode/GetAvailableDestinationZipCodes',
            type: "GET",
            data: { 'serviceId': serviceId, 'areaCode': extAreaCode, 'destAreaCode': destAreaCode },
            dataType: "json",
            cache: false,
            success: function (data) {
                var strigifyJson = JSON.stringify(data);
                var json = $.parseJSON(strigifyJson);
                var options;
                $.each(json, function (i, val) {
                    
                    if (options == undefined) {
                        options = '<option value="' + val[0] + '">' + val[0] + '</option>';
                    } else {
                        options += '<option value="' + val[0] + '">' + val[0] + '</option>';
                    }
                });
                if (json.length > 0) {
                    $('#existingZipCodes').html(options);
                }
                else {
                    $('#existingZipCodes').html('');
                }
            },
            error: function (xhr, ajaxOptions, thrownError) {
            }
        });
    }

    function GetAvailableAreas(serviceId) {
        $.ajax({
            url: '/DestZipCode/GetAvailDestAreas',
            type: "GET",
            data: { 'serviceId': serviceId },
            dataType: "json",
            cache: false,
            success: function (data) {
                var strigifyJson = JSON.stringify(data);
                var json = $.parseJSON(strigifyJson);
                var table;
                var options ;
                $.each(json, function (i, val) {
                    if (options == undefined) {
                        options = '<option value="' + val[0] + '">' + val[1] + ' - ' + val[0] + '</option>';
                    } else {
                        options += '<option value="' + val[0] + '">' + val[1] + ' - ' + val[0] + '</option>';
                    }
                });

                if (json.length > 0) {
                    options = '<option value="">' + "--Choose One--" + '</option>' + options;
                    $('#ddldestareaCode').html(options);
                    $('#ddlexstngareaCode').html(options);
                    $('#existingZipCodes').html('');
                    $('#destZipCodes').html('');
                    $('#ddldestareaCode').attr("disabled", false);
                    $('#ddlexstngareaCode').attr("disabled", true);
                }
                else {
                    $('#ddldestareaCode').html('');
                    $('#ddlexstngareaCode').html('');
                    $('#existingZipCodes').html('');
                    $('#destZipCodes').html('');
                    $('#ddldestareaCode').attr("disabled", true);
                    $('#ddlexstngareaCode').attr("disabled", true);
                }
            },
            error: function (xhr, ajaxOptions, thrownError) {
            }
        });
    }

    $('#add').click(function () {
        var serviceId = $('#ddlServiceID').val();
        var destAreaCode = $('#ddldestareaCode').val();
        var selected = [];
        $('#existingZipCodes :selected').each(function (i, el) {
            selected[i] = $(this).val();
        });
        if (serviceId == '') {
            alert("Please Select Service Type");
            $('#ddlServiceID').focus();
            return false;
        }
        if (destAreaCode == '') {
            alert("Please Select Destination AreaCode");
            $('#ddldestareaCode').focus();
            return false;
        }
        if (selected.length == 0) {
            alert("Please Select any Zip Code to Add");
            $('#existingZipCodes').focus();
            return false;
        }
        var data_to_send = JSON.stringify(selected);
        $.ajax({
            url: '/DestZipCode/AddCompanyAreaDestinationZipCodes',
            type: "POST",
            data: { 'serviceId': serviceId, 'areaCode': destAreaCode, 'zipCodes': data_to_send },
            success: function (data) {
                GetAvailableZipCodes();
                alert("Zip Code(s) Added Successfully");
            },
            error: function (xhr, ajaxOptions, thrownError) {
            }
        });
    });
    $('#remove').click(function () {
        var serviceId = $('#ddlServiceID').val();
        var destAreaCode = $('#ddldestareaCode').val();
        var selected = [];
        $('#destZipCodes :selected').each(function (i, el) {
            selected[i] = $(this).val();
        });
        if (selected.length == 0) {
            alert("Please Select any Zip Code to Remove")
            $('#destZipCodes').focus();
            return false;
        }
        var data_to_send = JSON.stringify(selected);
        $.ajax({
            url: '/DestZipCode/DeleteCompanyAreaDestinationZipCodes',
            type: "POST",
            data: { 'serviceId': serviceId, 'areaCode': destAreaCode, 'zipCodes': data_to_send },
            success: function (data) {
                GetAvailableZipCodes();
                alert("Zip Code(s) Removed Successfully");
            },
            error: function (xhr, ajaxOptions, thrownError) {
            }
        });
    });
    $("body").parent().on("change", "#ddlServiceID", function () {
        var serviceId = $('#ddlServiceID').val();
        if (serviceId != '') {
            GetAvailableAreas(serviceId);
        }
        else {
            $('#ddldestareaCode').attr("disabled", true)
            $('#ddlexstngareaCode').attr("disabled", true)
            $('#ddldestareaCode').html('');
            $('#ddlexstngareaCode').html('');
            $('#destAreaCodes').html('');
            $('#existingAreaCodes').html('');
            $('#destZipCodes').html('');
            $('#existingZipCodes').html('');
        }
    });
    $("body").parent().on("change", "#ddldestareaCode", function () {
        var serviceId = $('#ddlServiceID').val();
        var destAreaCode = $('#ddldestareaCode').val();
        if (destAreaCode != '') {
            GetAvailableZipCodes();
            $('#ddlexstngareaCode').attr("disabled", false)
        }
        else {
            $('#ddlexstngareaCode option:first').prop('selected', true);
            $('#ddlexstngareaCode').attr("disabled", true)
            $('#destZipCodes').html('');
            $('#existingZipCodes').html('');
        }
    });
    $("body").parent().on("change", "#ddlexstngareaCode", function () {
        var destAreaCode = $('#ddldestareaCode').val();
        if (destAreaCode == '') {
            alert('Please Select Destincation Area Code')
            $('#ddlexstngareaCode option:first').prop('selected', true);
            $('#destZipCodes').html('');
            $('#existingAreaCodes').html('');
        } else {
            GetAvailableZipCodes();
        }
    });
});