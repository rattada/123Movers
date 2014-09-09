$(function () {
    $('.TitleStyle').text('Radius Information');
    var areaCodes = [];
    var serviceId;
    $("#ddlorigordest").prop("disabled", true);
    //Allow decimal values.
    $("#body").on("keypress", "#txtradius", function (event) {
        if ((event.which != 46 || $(this).val().indexOf('.') != -1) && (event.which < 48 || event.which > 57)) {
            event.preventDefault();
        }
        //Shouldnot allow more than 2 digits after decimal point.
        //if ($(this).val().split(".")[1].length > 1) {
        //    event.preventDefault();
        //}
    });
    //Shouldnot allow decilmal values and Allow only digits.
    $("body").parent().on("keypress", "#txtzipcode", function (event) {
        if (event.which < 48 || event.which > 57) {
            event.preventDefault();
        }
    });
    $('#btnRadius').click(function () {
       // var service = $.trim($("#ddlServiceID").val());
        var lessergreater = $.trim($("#ddllessorgreate").val());
        var type = 'ORIGIN'// $.trim($("#ddlorigordest").val());
        var radius = $.trim($("#txtradius").val());
        var zipcode = $.trim($("#txtzipcode").val());
        if (serviceId == "") {
            alert('Please select Service type');
            $("#ddlServiceID").focus();
            return false;
        }
        else if (radius == "") {
            alert('Please enter Radius');
            $("#txtradius").focus();
            return false;
        }
        else if (zipcode == "") {
            alert('Please enter Zipcode');
            $("#txtzipcode").focus();
            return false;
        }
        var RadiusData =
        {
            service: serviceId,
            zipcode: zipcode,
            radius: radius,
            category: lessergreater,
        };
        AddZipCodesByRadius(RadiusData);
    });

    $('#addAreaCode').on('click', function () {
        var areacode;
        var jareaCodes;

        jareaCodes = JSON.stringify(areaCodes);

        $.ajax({
            url: '/radius/AddAreaCodes',
            data: { 'ServiceId': serviceId, 'AreaCodes': jareaCodes },
            dataType: "json",
            cache: false,
            success: function (data) {
                if (data.success) {
                    $("#addAreaCode").prop("disabled", true);
                    alert('Area Codes Successfully Added');
                }
            }
        });
    });
    $('#btnGetRadius').on("click", function () {
        serviceId = $.trim($("#ddlServiceID").val());
        var lessergreater = $.trim($("#ddllessorgreate").val());
        var radius = $.trim($("#txtradius").val());
        var zipcode = $.trim($("#txtzipcode").val());
        if (serviceId == "") {
            alert('Please select Service type');
            $("#ddlServiceID").focus();
            return false;
        }
        //else if (category == "") {
        //    alert('Please select Category');
        //    $("#ddllessorgreate").focus();
        //    return false;
        //}
        else if (radius == "") {
            alert('Please enter Radius');
            $("#txtradius").focus();
            return false;
        }
        else if (zipcode == "") {
            alert('Please enter Zipcode');
            $("#txtzipcode").focus();
            return false;
        }
        debugger;
        var RadiusData =
        {
            service: serviceId,
            zipcode: zipcode,
            radius: radius,
            category: lessergreater
        };
        GetZipCodesByRadius(RadiusData);
    });
    function AddZipCodesByRadius(RadiusData) {
        $("body").mask('Saving...');
        $.ajax({
            url: '/Radius/AddZipCodesByRadius',
            type: 'POST',
            data: RadiusData,
            dataType: "json",
            cache: false,
            success: function (data) {
                if (data.success) {
                    $("body").unmask();
                    alert("Record(s) saved successfully");
                    $("#btnRadius").prop("disabled", true);
                }
                else {
                    alert("You are trying to Insert duplicate Record(s)");
                }
            },
            error: function (xhr, ajaxOptions, thrownError) {
                $("body").unmask();
            }
        });
    };
    function GetZipCodesByRadius(RadiusData) {
        $("body").mask('Loading...');
        $.ajax({
            url: '/Radius/GetZipCodesByRadius',
            type: 'GET',
            data: RadiusData,
            dataType: "json",
            cache: false,
            success: function (data) {
                var strigifyJson = JSON.stringify(data);
                var json = $.parseJSON(strigifyJson);
                var table = "<table class='table table-bordered table-striped table-hover' id ='tblRadius'> <thead><tr><th class='header text-center'>Origin Zip</th> <th class='header text-center'>AreaCode</th><th class='header text-center'>ZipCode</th><th class='header text-center'>Distance</th></tr></thead><tbody>";
                if (json.length > 0) {
                    jQuery.each(json, function (i, val) {
                        table += "<tr><td class='text-center'>" + val[0] + "</td><td class='text-center areacode'>" + val[1] + "</td><td class='text-center'>" + val[2] + "</td><td class='text-center'>" + val[3] + "</td></tr>";
                        if ($.inArray(val[1], areaCodes) < 0) {
                            areaCodes.push(val[1]);
                        }
                    });
                    table += "</tbody></table>";
                    $('.table-responsive').html(table);
                    $('#tblRadius').dataTable({ "sPaginationType": "full_numbers" });
                    $("#btnRadius").css('display', 'block');
                    $("#ddlorigordest").prop("disabled", false);
                    $("#btnRadius").prop("disabled", false);
                    $("#addAreaCode").prop("disabled", false);
                }
                else {
                    // $('#tblRadius_wrapper').html('');
                    $("#btnRadius").css('display', 'none');
                    $("#ddlorigordest").prop("disabled", true);
                    alert("No record(s) found with above Combination.Please try with another Combination");
                }
                $("body").unmask();
            },
            error: function (xhr, ajaxOptions, thrownError) {
            }
        });
    };
});


