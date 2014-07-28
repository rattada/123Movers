$(function () {
    var serviceId = $('#ddlServiceID').val();
    if (serviceId != '') {
        GetAvailableAreas(serviceId);
    } else { $('#ddlareaCode').attr("disabled", true); }

    function GetextAvailableZipCodes(serviceid, areaCode) {
        $.ajax({
            url: '/DestinationZipCode/GetAvailableDestinationZipCodes',
            type: "GET",
            data: { 'serviceId': serviceid, 'areaCode': areaCode },
            dataType: "json",
            cache: false,
            success: function (data) {
                var strigifyJson = JSON.stringify(data);
                var json = $.parseJSON(strigifyJson);
                //var k;
                //if (json.length > 0) {

                //    var values = $.map($('#destZipCodes option'), function (e) { return e.value; });
                //    alert(values);
                //    alert(json.length);

                //    $.each(values, function (i, outer) {
                //        $.each(json, function (j, inner) {
                //            if (inner == outer) {
                //                return false;
                //            }
                //            else {
                //                k =  k + "," + inner;
                //            }
                //        });

                //    });
                //    alert(k.length);
                //    alert(k);
                //    return false;
                //}
                var options = '<option value=""></option>';
                $.each(json, function (i, val) {
                    if (i == 0) {
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
    function GetAvailableZipCodes(serviceid,areaCode) {
        $.ajax({
            url: '/DestinationZipCode/GetCompanyAreasDestinationZipCodes',
            type: "GET",
            data: { 'serviceId': serviceid, 'areaCode': areaCode },
            dataType: "json",
            cache: false,
            success: function (data) {
                var strigifyJson = JSON.stringify(data);
                var json = $.parseJSON(strigifyJson);
                var table;
                var options = '<option value=""></option>';
                $.each(json, function (i, val) {
                    if (i == 0) {
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
                url: '/DestinationZipCode/GetAvailableDestinationZipCodes',
                type: "GET",
                data: { 'serviceId': serviceid, 'areaCode': areaCode },
                dataType: "json",
                cache: false,
                success: function (data) {
                    var strigifyJson = JSON.stringify(data);
                    var json = $.parseJSON(strigifyJson);
                    var table;
                    var options = '<option value=""></option>';
                    $.each(json, function (i, val) {
                        if (i == 0) {
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
            url: '/SpecificOriginAreaCodes/GetAvailSpcfcOriginDestAreas',
            type: "GET",
            data: { 'serviceId': serviceId },
            dataType: "json",
            cache: false,
            success: function (data) {
                var strigifyJson = JSON.stringify(data);
                var json = $.parseJSON(strigifyJson);
                var table;
                var options = '<option value=""></option>';
                $.each(json, function (i, val) {
                    if (i == 0) {
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
                    $('#ddlexstngareaCode').attr("disabled", false);
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
                var table;
                var options = '<option value=""></option>';
                $.each(json, function (i, val) {
                    if (i == 0) {
                        options = '<option value="' + val[0] + '">' + val[1] + ' - ' + val[0] + '</option>';
                    } else {
                        options += '<option value="' + val[0] + '">' + val[1] + ' - ' + val[0] + '</option>';
                    }
                });
                if (json.length > 0) {
                    $('#existingAreaCodes').html(options);
                }
                else {
                    $('#existingAreaCodes').html('');
                }
            },
            error: function (xhr, ajaxOptions, thrownError) {
            }
        });
        //DestinationAreaCodes
        $.ajax({
            url: '/SpecificOriginAreaCodes/GetCompanySpcfcOriginDestAreas',
            type: "GET",
            data: { 'serviceId': serviceId, "spcfcareacode": spfcareacode, "originAreaCode": true },
            dataType: "json",
            cache: false,
            success: function (data) {
                var strigifyJson = JSON.stringify(data);
                var json = $.parseJSON(strigifyJson);
                var options = '<option value=""></option>';
                $.each(json, function (i, val) {
                    if (i == 0) {
                        options = '<option value="' + val[0] + '">' + val[1] + ' - ' + val[0] + '</option>';
                    } else {
                        options += '<option value="' + val[0] + '">' + val[1] + ' - ' + val[0] + '</option>';
                    }
                });
                if (json.length > 0) {
                    $('#destAreaCodes').html(options);
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
        $('#existingAreaCodes :selected').each(function (i, el) {
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
            $('#existingAreaCodes').focus();
            return false;
        }
        var data_to_send = JSON.stringify(selected);
        $.ajax({
            url: '/SpecificOriginAreaCodes/AddCompanySpcfcOriginDestAreaCodes',
            type: "POST",
            data: { 'serviceId': serviceId, "spcfcareacode": originAreaCode, 'areaCodes': data_to_send },
            success: function (data) {
                GetSelectedAreas(serviceId, originAreaCode);
                alert("Area Code(s) added Successfully");
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
                alert("Area Code(s) removed Successfully");
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
        }
    });
    $("body").parent().on("change", "#ddldestareaCode", function () {
        var serviceId = $('#ddlServiceID').val();
        var destAreaCode = $('#ddldestareaCode').val();
        if (destAreaCode != '') {
            GetAvailableZipCodes(serviceId, destAreaCode,true);
        }
        else {
            $('#destZipCodes').html('');
        }
    });
    $("body").parent().on("change", "#ddlexstngareaCode", function () {
        var serviceId = $('#ddlServiceID').val();
        var destAreaCode = $('#ddlexstngareaCode').val();
        if (destAreaCode != '') {
            GetextAvailableZipCodes(serviceId, destAreaCode);
        }
        else {
            $('#existingAreaCodes').html('');
        }
    });
});
