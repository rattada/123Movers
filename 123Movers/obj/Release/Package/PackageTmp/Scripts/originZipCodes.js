$(function () {
    $('.TitleStyle').text('Origin Zip Codes');
    var areacode;
    var serviceid;
    $(".OriginAreaCode").click(function () {
        var paramAreaCode = $(this).data("areacode");
        var paramServiceId = $(this).data("serviceid");
        areacode = paramAreaCode;
        serviceid = paramServiceId;
        GetZipCodes(areacode, serviceid);
    });
    $('#add').click(function (e) {
        e.preventDefault();
        var selected = [];
        $('#areaZipCode :selected').each(function (i, el) {
            selected[i] = $(this).val();
            $("#areaZipCode option:selected").remove();
            $("#areaZipSelected").append($(this));
        });
        if (selected.length == 0) {
            alert('Please select any Option to Add');
            return false;
        }
        else {
            var zipcodes = JSON.stringify(selected);
            $.ajax({
                url: '/OriginZipCode/AddCompanyAreaZipCodes',
                type: "POST",
                data: { 'serviceId': serviceid, 'areaCode': areacode, 'zipCodes': zipcodes },
                success: function (data) {
                    if (data.success === true) {
                        var x = $('#areaZipCode > option').length;
                        $('#areaZipCode').scrollTop(x);
                        alert('Zip Code(s) Successfully Added');
                    } else {
                        alert("Error :" + data.message);
                    }
                },
                error: function (xhr, ajaxOptions, thrownError) {
                }
            });
        }
    });
    $('#remove').click(function () {
        var selected = [];
        $('#areaZipSelected :selected').each(function (i, el) {
            selected[i] = $(this).val();
            $("#areaZipSelected option:selected").remove();
            $("#areaZipCode").append($(this));
        });
        if (selected.length == 0) {
            alert('Please select any Option to Remove');
            return false;
        } else {
            var zipcodes = JSON.stringify(selected);
            $.ajax({
                url: '/OriginZipCode/DeleteCompanyAreaZipCodes',
                type: "POST",
                data: { 'serviceId': serviceid, 'areaCode': areacode, 'zipCodes': zipcodes },
                success: function (data) {
                    if (data.success === true) {
                        var x = $('#areaZipSelected > option').length;
                        $('#areaZipSelected').scrollTop(x);

                        alert('Zip Code(s) Successfully Deleted');
                    } else {
                        alert("Error :" + data.message);
                    }
                },
                error: function (xhr, ajaxOptions, thrownError) {
                }
            });
        }
    });
    $('.cross ').click(function () {
        $('#myModal').modal({
            backdrop: null
        });
        location.reload();
    });
});
function GetZipCodes(areaCode, serviceId) {
    serviceid = serviceId;
    areacode = areaCode
    $.ajax({
        url: '/OriginZipCode/GetAvailableZipCodes',
        type: "GET",
        data: { 'serviceId': serviceId, 'areaCode': areaCode },
        dataType: "json",
        cache: false,
        success: function (data) {
            var strigifyJson = JSON.stringify(data);
            var json = $.parseJSON(strigifyJson);
            var table;
            var options = '<option value=""></option>';
            jQuery.each(json, function (i, val) {
                if (i == 0) {
                    options = '<option value="' + val[2] + '">' + val[0] + "-" + val[1] + "-" + val[2] + '</option>';
                } else {
                    options += '<option value="' + val[2] + '">' + val[0] + "-" + val[1] + "-" + val[2] + '</option>';
                }

            });
            if (json.length > 0) {
                $('#areaZipCode').html(options);
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
        }
    });
    $.ajax({
        url: '/OriginZipCode/GetAreaCodeZipCodes',
        type: "GET",
        data: { 'serviceId': serviceId, 'areaCode': areaCode },
        dataType: "json",
        cache: false,
        success: function (data) {
            var strigifyJson = JSON.stringify(data);
            var json = $.parseJSON(strigifyJson);
            var table;
            var options = '<option value=""></option>';
            jQuery.each(json, function (i, val) {
                if (i == 0) {
                    options = '<option value="' + val[2] + '">' + val[0] + "-" + val[1] + "-" + val[2] + '</option>';
                } else {
                    options += '<option value="' + val[2] + '">' + val[0] + "-" + val[1] + "-" + val[2] + '</option>';
                }
            });
            if (json.length > 0) {
                $('#areaZipSelected').html(options);
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
        }
    });
}