$(function () {
    var serviceId;
    var serviceType;
    var k = 0;
    $(".TitleStyle").text("Area Codes");
    if ($('#serviceid').val() == 1000) {
        serviceId = 1000;
        serviceType = 'long';
        $('#ServiceTab a:last').tab('show'); // Select last 
        $('li.cslocal').hide();
    }
    else if ($('#serviceid').val() == 1009) {
        serviceId = 1009;
        serviceType = 'local';
        $('#ServiceTab a:first').tab('show'); // Select first 
        $('li.cslong').hide();
    }
    else {
        serviceId = 1009;
        serviceType = 'Both';
        $('#ServiceTab a:first').tab('show'); // Default first 
    }
    $("#saveprice").attr('disabled', 'disabled');

    $("#body").on("keypress", "input", function (event) {
        if ((event.which != 46 || $(this).val().indexOf('.') != -1) && (event.which < 48 || event.which > 57)) {
            event.preventDefault();
        }
        else {
            $("#saveprice").removeAttr('disabled');
        }
    });

    getAvailableAreas(serviceId);
    getSelectedAreas(serviceId);
    function getAvailableAreas(service) {
        $.ajax({
            url: '/AreaCode/GetAvailableAreas',
            type: "GET",
            data: { 'serviceId': service },
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
                else {
                    $('#areaCode').html('');
                }
            },
            error: function (xhr, ajaxOptions, thrownError) {
            }
        });
    }
    function getSelectedAreas(service) {
        $.ajax({
            url: '/AreaCode/GetCompanyAreasWithPrices',
            type: "GET",
            data: { 'serviceId': service },
            dataType: "json",
            cache: false,
            success: function (data) {
                var strigifyJson = JSON.stringify(data);
                var json = $.parseJSON(strigifyJson);
                $('#txtDefaultPrice').val('');
                var table;
                var table1 = "<div class ='col-md-6' ><table class='table table-hover table-striped' id ='table1' ><thead><tr><th class='col-md-2 text-center'>" + 'Area Code' + "</th><th class='col-md-2 text-center'>" + 'Price' + "</th></tr> </thead><tbody>";
                var options = '<option value=""></option>';
                jQuery.each(json, function (i, val) {
                    if (i == 0) {
                        options = '<option value="' + val[0] + '">' + val[1] + ' - ' + val[0] + '</option>';
                    } else {
                        options += '<option value="' + val[0] + '">' + val[1] + ' - ' + val[0] + '</option>';
                    }
                    if (val[3] != null && val[3] != '') {
                        $('#txtDefaultPrice').val(val[3].toString().slice(0, -2));
                    }
                    if (i < Math.round(json.length / 2)) {
                        var value = val[2].toString().slice(0, -2);
                        table1 += "<tr><td>" + val[1] + " ---> " + val[0] + "</td><td><input type='text' id='txt" + val[0] + "' value ='" + value + "' class='form-control'/><input type='hidden' value='" + val[0] + "' class='areacode' /> </td></tr>";
                    }
                });
                table1 += "</tbody></table></div>";

                var table2 = "<div class ='col-md-6' ><table class='table table-hover table-striped' id ='table2'><thead><tr><th class='col-md-2 text-center'>" + 'Area Code' + "</th><th class='col-md-2 text-center'>" + 'Price' + "</th></tr> </thead><tbody>";
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
                    $('#areasSelected').html('');
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
                    alert('Please Enter a Valid Numeric Price :' + areacode + '-' + areaprice);
                    return false;
                }
                areacodes.push(areacode + '-' + 'NO-' + areaprice);
            }
            else {
                if (defaultprice.length > 0) {
                    if (!defaultprice.match(decimal)) {
                        alert('Please Enter a Valid Default Price');
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
                    alert('Please Enter a Valid Numeric Price :' + areacode + '-' + areaprice);
                    return false;
                }
                areacodes.push(areacode + '-' + 'NO-' + areaprice);
            }
            if (defaultprice.length > 0) {
                if (!defaultprice.match(decimal)) {
                    alert('Please Enter a Valid Default Price');
                    return false;
                }
                areacodes.push(areacode + '-' + 'YES-' + defaultprice);
            }
        });
        var rows = $("#table1 tbody tr").length + $("#table2 tbody tr").length;
        if (areacodes.length == 0) {
            alert('Please enter the price values');
            return false;
        }
        if (areacodes.length != rows && defaultprice.length == 0) {
            alert('Please enter default price');
            return false;
        }
        var jsareacodes = JSON.stringify(areacodes);
        $.ajax({
            url: '/AreaCode/AddCompanyPricePerLead',
            type: "POST",
            data: {  'serviceId': serviceId, areaCodes: jsareacodes },
            success: function (data) {
                if (data.success === true) {
                    alert('Prices Saved Suceessfully');
                    $("#saveprice").attr('disabled', 'disabled');
                } else {
                    alert("Error :" + data.message);
                }
            },
            error: function (xhr, ajaxOptions, thrownError) {
                var errMessage = JSON.parse(xhr.responseText).customErrorMessage;
                alert('Error occurred while saving the prices' + errMessage);
            }
        });
    });
    $('#add').click(function () {
        var service = $("#ServiceTab > li.active >a").text();
        var selected = [];
        $('#areaCode :selected').each(function (i, el) {
            selected[i] = $(this).val();
        });
        if (selected.length == 0) {
            alert("Please select any Option to Add");
            return false;
        }
        var dataToSend = JSON.stringify(selected);
        $.ajax({
            url: '/AreaCode/AddAreaCodes',
            type: "POST",
            data: { 'serviceId': serviceId, 'areaCodes': dataToSend },
            success: function (data) {
                if (data.success) {
                    if (service == "Local") {
                        serviceId = 1009;
                        getAvailableAreas(serviceId, 'local');
                        getSelectedAreas(serviceId, 'local');
                        $('#ServiceTab a:first').tab('show');
                    }
                    else {
                        serviceId = parseInt(1000);
                        getAvailableAreas(serviceId, 'long');
                        getSelectedAreas(serviceId, 'long');
                        $('#ServiceTab a:last').tab('show');
                    }
                    alert("Area Code(s) added Successfully");
                    k = 1;
                }
                else {
                    alert("Error occured while saving.Please go to Search");
                    return false;
                }
            },
            error: function (xhr, ajaxOptions, thrownError) {
            }
        });
    });
    $('#remove').click(function () {
        var service = $("#ServiceTab > li.active >a").text();
        var selected = [];
        $('#areasSelected :selected').each(function (i, el) {
            selected[i] = $(this).val();
        });
        if (selected.length == 0) {
            alert("Please select any Option to Remove");
            return false;
        }
        var dataToSend = JSON.stringify(selected);
        $.ajax({
            url: '/AreaCode/DeleteAreaCodes',
            type: "POST",
            data: { 'serviceId': serviceId, areaCodes: dataToSend },
            success: function (data) {
                if (service == "Local") {
                    serviceId = 1009;
                    getAvailableAreas(serviceId, 'local');
                    getSelectedAreas(serviceId, 'local');
                    $('#ServiceTab a:first').tab('show');
                }
                else {
                    serviceId = parseInt(1000);
                    getAvailableAreas(serviceId, 'long');
                    getSelectedAreas(serviceId, 'long');
                    $('#ServiceTab a:last').tab('show');
                }
                alert("Area Code(s) removed Successfully");
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
            getAvailableAreas(serviceId, 'local');
            getSelectedAreas(serviceId, 'local');
            $('#ServiceTab a:first').tab('show');
        });
        $('#ServiceTab > li:last').click(function () {
            serviceId = 1000;
            getAvailableAreas(serviceId, 'long');
            getSelectedAreas(serviceId, 'long');
            $('#ServiceTab a:last').tab('show');
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