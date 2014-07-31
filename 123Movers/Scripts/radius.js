$(function () {
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
            service: $.trim($("#ddlServiceID").val()),
            zipcode: $.trim($("#txtzipcode").val()),
            radius: $.trim($("#txtradius").val()),
            category: $.trim($("#ddllessorgreate").val()),
            type: $.trim($("#ddlorigordest").val())
        };
        AddZipCodesByRadius(RadiusData);
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
                alert("Record successfully saved");
                $("input:text").val('');
                $('select').prop('selectedIndex', 0);

            },
            error: function (xhr, ajaxOptions, thrownError) {
            }
        });
    };

    
        