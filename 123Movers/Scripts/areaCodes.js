
$(function () {
    var serviceId;
    var ServiceType;
    var k = 0;
    if ($('#serviceid').val() == 1000) {
        serviceId =1000;
        ServiceType = 'long';
        $('#ServiceTab a:last').tab('show') // Select last 
        $('li.cslocal').hide();
    }
    else if ($('#serviceid').val() == 1009) {

        serviceId = 1009;
        ServiceType = 'local';
        $('#ServiceTab a:first').tab('show') // Select first 
        $('li.cslong').hide();
    }
    else {
        serviceId = 1009;
        ServiceType = 'Both';
        $('#ServiceTab a:first').tab('show') // Default first 

    }

    $("#saveprice").attr('disabled', 'disabled');

    $("#body").on("keypress", "input", function (event) {
        var key = event.which;

        if (!(key >= 48 && key <= 57))
            event.preventDefault();
        else
            $("#saveprice").removeAttr('disabled');
    });

    GetAvailableAreas(serviceId, ServiceType);
    GetSelectedAreas(serviceId, ServiceType);



    function GetAvailableAreas(serviceId, ServiceType) {
        $.ajax({
            url: '/AreaCode/GetAvailableAreas',
            type: "GET",
            data: {'serviceId': serviceId },
            dataType: "json",
            cache: false,
            success: function (data) {
                var strigifyJson = JSON.stringify(data);
                var json = $.parseJSON(strigifyJson);
                var table;
                var options = '<option value=""></option>';
                jQuery.each(json, function (i, val) {
                    if (i == 0) {
                        options = '<option value="' + val[0] + '">' + val[1] + ' - ' + val[0] + '</option>';
                    } else {
                        options += '<option value="' + val[0] + '">' + val[1] + ' - ' + val[0] + '</option>';
                    }
                });

                if (json.length > 0) {
                    $('#areaCode').html(options);
                }


            },
            error: function (xhr, ajaxOptions, thrownError) {

            }
        });
    }

    function GetSelectedAreas(serviceId, ServiceType) {

        $.ajax({
            url: '/AreaCode/GetCompanyAreasWithPrices',
            type: "GET",
            data: { 'serviceId': serviceId },
            dataType: "json",
            cache: false,
            success: function (data) {
                var strigifyJson = JSON.stringify(data);
                var json = $.parseJSON(strigifyJson);


                var table;
                var table1 = "<div class ='col-md-6' ><table class='table table-striped' id ='table1' ><thead><tr><th class='col-md-2'>" + 'Area Code' + "</th><th class='col-md-2'>" + 'Price' + "</th></tr> </thead><tbody>";
                var options = '<option value=""></option>';
                jQuery.each(json, function (i, val) {
                    if (i == 0) {
                        options = '<option value="' + val[0] + '">' + val[1] + ' - ' + val[0] + '</option>';
                    } else {
                        options += '<option value="' + val[0] + '">' + val[1] + ' - ' + val[0] + '</option>';
                    }
                    if (val[3] != null) {
                        $('#txtDefaultPrice').val(val[3].toString().slice(0, -2));
                    }

                    if (i < Math.round(json.length / 2)) {
                        var value = val[2].toString().slice(0, -2);
                        table1 += "<tr><td>" + val[1] + " ---> " + val[0] + "</td><td><input type='text' id='txt" + val[0] + "' value ='" + value + "' class='form-control'/><input type='hidden' value='" + val[0] + "' class='areacode' /> </td></tr>";
                    }

                });
                table1 += "</tbody></table></div>";

                var table2 = "<div class ='col-md-6' ><table class='table table-striped' id ='table2'><thead><tr><th class='col-md-2'>" + 'Area Code' + "</th><th class='col-md-2'>" + 'Price' + "</th></tr> </thead><tbody>";
                jQuery.each(json, function (i, val) {
                    if (i >= Math.round(json.length / 2) && i < json.length) {
                        var value = val[2].toString().slice(0, -2);
                        table2 += "<tr><td>" + val[1] + " ---> " + val[0] + "</td><td><input type='text' id='txt" + val[0] + "' value ='" + value + "' class='form-control' /><input type='hidden' value='" + val[0] + "' class='areacode' /> </td></tr>";
                    }
                });

                table2 += "</tbody></table></div>";
                table = table1 + table2;

                $('#selectedareas').html(table);

                if (json.length > 0) {
                    $('#areasSelected').html(options);
                    $("#txtDefaultPrice").removeAttr("disabled");
                }
                else {
                    $("#txtDefaultPrice").attr("disabled", "disabled");
                    $("#txtDefaultPrice").val('');
                    $('#areasSelected').html('')
                }


            },
            error: function (xhr, ajaxOptions, thrownError) {

            }
        });
    }

    $('#saveprice').on("click", function () {
        var defaultprice = $.trim($('#txtDefaultPrice').val());
        var decimal = /^(\d*\.?\d*)$/;
        var areacodes = [];

        $("#table1 tbody tr").each(function () {
            var areaprice = $(this).closest('tr').find('.form-control').val();
            var areacode = $(this).closest('tr').find('.areacode').val();
            if (areaprice.length > 0) {
                if (!areaprice.match(decimal)) {
                    log('Please Enter a Valid Numeric Price :' + areacode + '-' + areaprice);
                    return false;
                }

                areacodes.push(areacode + '-' + 'NO-' + areaprice);
            }
            else {
                if (defaultprice.length > 0) {
                    if (!defaultprice.match(decimal)) {
                        log('Please Enter a Valid Default Price');
                        return false;
                    }
                    areacodes.push(areacode + '-' + 'YES-' + defaultprice);

                }
            }

        });

        $("#table2 tbody tr").each(function () {
            var areaprice = $(this).closest('tr').find('.form-control').val();
            var areacode = $(this).closest('tr').find('.areacode').val();
            if (areaprice.length > 0) {
                if (!areaprice.match(decimal)) {
                    log('Please Enter a Valid Numeric Price :' + areacode + '-' + areaprice);
                    return false;
                }

                areacodes.push(areacode + '-' + 'NO-' + areaprice);
            }
            if (defaultprice.length > 0) {
                if (!defaultprice.match(decimal)) {
                    log('Please Enter a Valid Default Price');
                    return false;
                }

                areacodes.push(areacode + '-' + 'YES-' + defaultprice);

            }

        });

        var rows = $("#table1 tbody tr").length + $("#table2 tbody tr").length;

        if (areacodes.length == 0) {
            log('Please enter the price values');
            return false;
        }
        if (areacodes.length != rows && defaultprice.length == 0) {
            log('Please enter default price');
            return false;
        }

        var jsareacodes = JSON.stringify(areacodes);

        $.ajax({
            url: '/AreaCode/AddCompanyPricePerLead',
            type: "POST",
            data: {  'serviceId': serviceId, areaCodes: jsareacodes },
            success: function (data) {
                if (data.success === true) {
                    log('Prices Saved Suceessfully');
                    $("#saveprice").attr('disabled', 'disabled');
                } else {
                    log("Error :" + data.message);
                }

            },
            error: function (xhr, ajaxOptions, thrownError) {
                var errMessage = JSON.parse(xhr.responseText).customErrorMessage;
                log('Error occurred while saving the prices' + errMessage);
            }
        });

    });


    $('#add').click(function () {

        var Service = $("#ServiceTab > li.active >a").text();

        var selected = [];
        $('#areaCode :selected').each(function (i, el) {
            selected[i] = $(this).val();
        });

        if (selected.length == 0) {

            log("Please Select any Option to add");
            return false;
        }
        var data_to_send = JSON.stringify(selected);
        $.ajax({
            url: '/AreaCode/AddAreaCodes',
            type: "POST",
            data: { 'serviceId': serviceId, 'areaCodes': data_to_send },
            success: function (data) {

                if (Service == "Local") {
                    serviceId = 1009;
                    GetAvailableAreas(serviceId, 'local');
                    GetSelectedAreas(serviceId, 'local');
                    $('#ServiceTab a:first').tab('show');

                }
                else {

                    serviceId = parseInt(1000);
                    GetAvailableAreas(serviceId, 'long');
                    GetSelectedAreas(serviceId, 'long');
                    $('#ServiceTab a:last').tab('show')

                }
                log("Area Codes added Successfully");
                k = 1;

            },
            error: function (xhr, ajaxOptions, thrownError) {

            }
        });


    });


    $('#remove').click(function () {
        var Service = $("#ServiceTab > li.active >a").text();
        var selected = [];
        $('#areasSelected :selected').each(function (i, el) {
            selected[i] = $(this).val();
        });

        if (selected.length == 0) {
            log("Please Select any Option to Remove")
            return false;
        }
        var data_to_send = JSON.stringify(selected);
        $.ajax({
            url: '/AreaCode/DeleteAreaCodes',
            type: "POST",
            data: {'serviceId': serviceId, areaCodes: data_to_send },
            success: function (data) {

                if (Service == "Local") {

                    serviceId = 1009;
                    GetAvailableAreas(serviceId, 'local');
                    GetSelectedAreas(serviceId, 'local');
                    $('#ServiceTab a:first').tab('show')


                }
                else {

                    serviceId = parseInt(1000);
                    GetAvailableAreas(serviceId, 'long');
                    GetSelectedAreas(serviceId, 'long');
                    $('#ServiceTab a:last').tab('show')

                }
                log("Area Codes removed Successfully");
                k = 1;
            },
            error: function (xhr, ajaxOptions, thrownError) {

            }
        });

    });

    $('#accordion span').parent().click(function () {

        if ($('#accordion span').hasClass('glyphicon glyphicon-plus')) {
            $('#accordion span').removeClass("glyphicon glyphicon-plus");
            $('#accordion span').addClass("glyphicon glyphicon-minus");
        }
        else {

            $('#accordion span').removeClass("glyphicon glyphicon-minus");
            $('#accordion span').addClass("glyphicon glyphicon-plus");
        }

    });
    if (k == 0) {
        $('#ServiceTab > li:first').click(function () {
            serviceId = 1009;
            GetAvailableAreas(serviceId, 'local');
            GetSelectedAreas(serviceId, 'local');
            $('#ServiceTab a:first').tab('show')
        });

        $('#ServiceTab > li:last').click(function () {

            serviceId = 1000;
            GetAvailableAreas(serviceId, 'long');
            GetSelectedAreas(serviceId, 'long');
            $('#ServiceTab a:last').tab('show')
        });
    }

    $("#saveprice").attr('disabled', 'disabled');

    $('#body').on('change keyup keydown', 'input, textarea', function (e) {
        $("#saveprice").removeAttr('disabled');
    });

    function log(str) {
        var self = this,
            settings = {
                hide: 'fadeOut',
                show: 'fadeIn',
                speed: 'fast',
                timeout: 2000
            };

        $('.alert-box').html(str)[settings.show]();

        setTimeout(function () {
            $('.alert-box')[settings.hide]();
        }, settings.timeout);
    }
});