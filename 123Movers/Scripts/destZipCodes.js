$(function () {
    var areacode;
    var serviceid;
    $(".DestAreaCode").click(function () {
        var paramAreaCode = $(this).data("areacode");
        var paramServiceId = $(this).data("serviceid");
        areacode = paramAreaCode;
        serviceid = paramServiceId;
        GetZipCodes(areacode, serviceid);
    });
    $('#add').click(function () {
        var selected = [];
        $('#areaZipCode :selected').each(function (i, el) {
            selected[i] = $(this).val();
            $("#areaZipCode option:selected").remove();
            $("#areaZipSelected").append($(this));
        });
        if (selected.length == 0) {
            alert('Please Select ZipCode');
            return false;
        }
        else {
            var zipcodes = JSON.stringify(selected);
            $.ajax({
                url: '/DestinationZipCode/AddCompanyAreaDestinationZipCodes',
                type: "POST",
                data: { 'serviceId': serviceid, 'areaCode': areacode, 'zipCodes': zipcodes },
                success: function (data) {
                    if (data.success === true) {
                        var x = $('#areaZipCode > option').length;
                        $('#areaZipCode').scrollTop(x);

                        alert('Zip Codes Successfully Added');
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
            alert('Please Select ZipCode');
            return false;
        } else {
            var zipcodes = JSON.stringify(selected);
            $.ajax({
                url: '/DestinationZipCode/DeleteCompanyAreaDestinationZipCodes',
                type: "POST",
                data: { 'serviceId': serviceid, 'areaCode': areacode, 'zipCodes': zipcodes },
                success: function (data) {
                    if (data.success === true) {
                        var x = $('#areaZipSelected > option').length;
                        $('#areaZipSelected').scrollTop(x);

                        alert('Zip Codes Successfully Deleted');
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
function GetZipCodes(areaCode, serviceid) {
    $.ajax({
        url: '/DestinationZipCode/GetAvailableDestinationZipCodes',
        type: "GET",
        data: { 'serviceId': serviceid, 'areaCode': areaCode },
        dataType: "json",
        cache: false,
        success: function (data) {
            $("body").mask('Loading....')
            var strigifyJson = JSON.stringify(data);
            var json = $.parseJSON(strigifyJson);
            var table;
            //var options = '<select name=areaZipCode" id="areaZipCode" size="20" style="width: 150px" multiple> <option value=""></option>';
            var options = '<option value=""></option>';
            jQuery.each(json, function (i, val) {
                if (i == 0) {
                    //options = '<option value="' + val[0] + '">' + val[0] + '</option>';
                    options = '<option value="' + val[0] + '">' + val[1] + '-' + val[0] + '</option>';
                } else {
                    //options = '<option value="' + val[0] + '">' + val[0] + '</option>';
                    options += '<option value="' + val[0] + '">' + val[1] + '-' + val[0] + '</option>';
                }
            });
            //options += '<select>';
            if (json.length > 0) {
               
               // $('#divzip').html(options);
                $('#areaZipCode').html(options);
                //$('body').unmask();
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
        }
    });

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
            jQuery.each(json, function (i, val) {
                if (i == 0) {
                    options = '<option value="' + val[0] + '">' + val[1] + '-' + val[0] + '</option>';
                } else {
                    options += '<option value="' + val[0] + '">' + val[1] + '-' + val[0] + '</option>';
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