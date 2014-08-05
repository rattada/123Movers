$(function () {
    $("#btnRadius").prop("disabled", true);
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
        var service = $.trim($("#ddlServiceID").val());
        var category = $.trim($("#ddllessorgreate").val());
        var type = $.trim($("#ddlorigordest").val());
        var radius = $.trim($("#txtradius").val());
        var zipcode = $.trim($("#txtzipcode").val());
        if (service == "") {
            alert('Please select Service type');
            $("#ddlServiceID").focus();
            return false;
        }
        else if (category == "") {
            alert('Please select Category');
            $("#ddllessorgreate").focus();
            return false;
        }
        else if (type == "") {
            alert('Please select type');
            $("#ddlorigordest").focus();
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
            service: service,
            zipcode: zipcode,
            radius: radius,
            category: category,
            type: type
        };
        AddZipCodesByRadius(RadiusData);
    });

    $('#btnGetRadius').on("click", function () {
        var service = $.trim($("#ddlServiceID").val());
        var category = $.trim($("#ddllessorgreate").val());
        var radius = $.trim($("#txtradius").val());
        var zipcode = $.trim($("#txtzipcode").val());
        if (service == "") {
            alert('Please select Service type');
            $("#ddlServiceID").focus();
            return false;
        }
        else if (category == "") {
            alert('Please select Category');
            $("#ddllessorgreate").focus();
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
            service: service,
            zipcode: zipcode,
            radius: radius,
            category: category
        };
        GetZipCodesByRadius(RadiusData);
    });
});
function AddZipCodesByRadius(RadiusData) {
    $.ajax({
        url: '/Radius/AddZipCodesByRadius',
        type: 'POST',
        data: RadiusData,
        dataType: "json",
        cache: false,
        success: function (data) {

            if (data.success) {
                alert("Record(s) saved successfully");
                $("input:text").val('');
                $('select').prop('selectedIndex', 0);
            }
            else {

                alert("You are trying to Insert duplicate Record(s)");
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
        }
    });
};
function GetZipCodesByRadius(RadiusData) {
    alert('ASD');
    $.ajax({
        url: '/Radius/GetZipCodesByRadius',
        type: 'GET',
        data: RadiusData,
        dataType: "json",
        cache: false,
        success: function (data) {
            var strigifyJson = JSON.stringify(data);
            var json = $.parseJSON(strigifyJson);
            var table1;
            var table2;
            var table;
            if (json.length > 0) {
                jQuery.each(json, function (i, val) {
                    table2 += "<tr><td class='text-center'>" + val[0] + "</td><td class='text-center'>" + val[1] + "</td><td class='text-center'>" + val[2] + "</td><td class='text-center'>" + val[3] + "</td></tr>";
                });
                table1 = "<thead><tr><th class='header text-center'>Origin Zip</th> <th class='header text-center'>AreaCode</th><th class='header text-center'>ZipCode</th><th class='header text-center'>Distance</th></tr></thead>";
                table = table1 + table2;
                $('#tblRadius').html(table);
                $("#btnRadius").prop("disabled", false);
            }
            else {

                $('#tblRadius').html("<tr><td class='text-center'><b></b>No record(s) found with above Combination.Try with different Combination.</td></tr>");
                $("#btnRadius").prop("disabled", true);
            }

        },
        error: function (xhr, ajaxOptions, thrownError) {
        }
    });
};
