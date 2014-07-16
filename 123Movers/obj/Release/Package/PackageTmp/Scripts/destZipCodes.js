$(function () {
    var areacode;
    var serviceid;

    $(".CallFunc").click(function () {
        areacode = $(this).data("param1");
        serviceid = $(this).data("param2");
        GetZipCodes(areaCode, serviceId);
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
    $('#close').click(function () {

        location.reload();
    });
})(jQuery);
function GetZipCodes(areaCode, serviceId) {
    serviceid = serviceId;
    areacode = areaCode
    $.ajax({
        url: '/DestinationZipCode/GetAvailableDestinationZipCodes',
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
                    options = '<option value="' + val[0] + '">' + val[0] + '</option>';
                } else {
                    options += '<option value="' + val[0] + '">' + val[0] + '</option>';
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
        url: '/DestinationZipCode/GetCompanyAreasDestinationZipCodes',
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
                    options = '<option value="' + val[0] + '">' + val[0] + '</option>';
                } else {
                    options += '<option value="' + val[0] + '">' + val[0] + '</option>';
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