$(function () {
    $("#accordion_tblCompanyAreas").hide();
    var serviceId = $('#ddlServiceID').val();
    if (serviceId != '') {
        GetSelectedAreas(serviceId);
        //GettblCompanyAreas(serviceId);

    } else { $('#ddlareaCode').attr("disabled", true); }

    jQuery.fn.multiselect = function () {
        $(this).each(function () {
            var checkboxes = $(this).find("input:checkbox");
            checkboxes.each(function () {
                var checkbox = $(this);
                // Highlight pre-selected checkboxes
                if (checkbox.prop("checked"))
                    checkbox.parent().addClass("multiselect-on");

                // Highlight checkboxes that the user selects
                checkbox.click(function () {
                    if (checkbox.prop("checked"))
                        checkbox.parent().addClass("multiselect-on");
                    else
                        checkbox.parent().removeClass("multiselect-on");
                });
            });
        });
    };

    //function GettblCompanyAreas(serviceId) {
    //    $.ajax({
    //        url: '/DestinationAreaCode/GetCompanyDestAreas',
    //        type: "GET",
    //        data: { 'serviceId': serviceId },
    //        dataType: "json",
    //        cache: false,
    //        success: function (data) {
    //            var strigifyJson = JSON.stringify(data);
    //            var json = $.parseJSON(strigifyJson);
    //            if (json.length > 0) {
    //                $("#divTblAreas").show();
    //            }
    //            var table=undefined;
    //            jQuery.each(json, function (i, val) {

    //                //if (table == undefined) {
    //                //    table = "<label><input type='checkbox' name='option[]'  id="+val[0] +" />" + val[1] + ' - ' + val[0] + "</label>";
    //                //} else { 
    //                //    table += "<label><input type='checkbox' name='option[]'  id="+val[0] +" />" + val[1] + ' - ' + val[0] + "</label>";
    //                //}
    //            });
    //            //$("#accordion_tblCompanyAreas").show();
    //            //$('#divTblAreas').html(table);
    //            //$(".multiselect").multiselect();
    //        },
    //        error: function (xhr, ajaxOptions, thrownError) {
    //        }
    //    });
    //}
    function GetSelectedAreas(serviceId) {
        $.ajax({
            url: '/DestinationAreaCode/GetCompanyDestAreas',
            type: "GET",
            data: { 'serviceId': serviceId },
            dataType: "json",
            cache: false,
            success: function (data) {
                var strigifyJson = JSON.stringify(data);
                var json = $.parseJSON(strigifyJson);
                var options;
                var destoptions;
                var table;
                $.each(json, function (i, val) {
                    if (val[2] == 'Table') {
                        if (options == undefined) {
                            options = '<option value="' + val[0] + '">' + val[1] + ' - ' + val[0] + '</option>';
                        } else {
                            options += '<option value="' + val[0] + '">' + val[1] + ' - ' + val[0] + '</option>';
                        }
                    } else if (val[2] == 'Table1') {
                        if (destoptions == undefined) {
                            destoptions = '<option value="' + val[0] + '">' + val[1] + ' - ' + val[0] + '</option>';
                        } else {
                            destoptions += '<option value="' + val[0] + '">' + val[1] + ' - ' + val[0] + '</option>';
                        }
                    }
                    else {
                        //if (val[2] == 1) {
                        //    $('#test').prop('checked', true);
                        //}

                        if (table == undefined) {

                            table = "<label><input type='checkbox' name='option[]'  id=" + val[0] + " />" + val[1] + ' - ' + val[0] + "</label>";
                        } else {
                            table += "<label><input type='checkbox' name='option[]'  id=" + val[0] + " />" + val[1] + ' - ' + val[0] + "</label>";
                        }
                        $("#accordion_tblCompanyAreas").show();
                        $('#divTblAreas').html(table);
                        $(".multiselect").multiselect();


                    }
                });
                if (options != undefined) {
                    $('#originAreaCodes').html(options);
                }
                else {
                    $('#originAreaCodes').html('');
                }

                if (destoptions != undefined) {
                    $('#destAreaCodes').html(destoptions);
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
        var selected = [];
        $('#originAreaCodes :selected').each(function (i, el) {
            selected[i] = $(this).val();
        });
        if (serviceId == '') {
            alert("Please select Service Type");
            $('#ddlServiceID').focus();
            return false;
        }
        if (selected.length == 0) {

            alert("Please select any Option to add");
            $('#originAreaCodes').focus();
            return false;
        }
        var data_to_send = JSON.stringify(selected);
        $.ajax({
            url: '/DestinationAreaCode/AddCompanyDestAreaCodes',
            type: "POST",
            data: { 'serviceId': serviceId, 'areaCodes': data_to_send },
            success: function (data) {
                GetSelectedAreas(serviceId);
                alert("Area Code(s) added Successfully");
            },
            error: function (xhr, ajaxOptions, thrownError) {
            }
        });
    });
    $('#remove').click(function () {
        var serviceId = $('#ddlServiceID').val();
        var selected = [];
        if (serviceId == '') {
            alert("Please select Service Type");
            $('#ddlServiceID').focus();
            return false;
        }
        $('#destAreaCodes :selected').each(function (i, el) {
            selected[i] = $(this).val();
        });
        if (selected.length == 0) {
            alert("Please select any Option to Remove")
            $('#destAreaCodes').focus();
            return false;
        }
        var data_to_send = JSON.stringify(selected);
        $.ajax({
            url: '/DestinationAreaCode/DeleteCompanyDestAreaCodes',
            type: "POST",
            data: { 'serviceId': serviceId, 'areaCodes': data_to_send },
            success: function (data) {
                GetSelectedAreas(serviceId);
                alert("Area Code(s) removed Successfully");
            },
            error: function (xhr, ajaxOptions, thrownError) {
            }
        });
    });
    $("body").parent().on("change", "#ddlServiceID", function () {
        var serviceId = $('#ddlServiceID').val();
        if (serviceId != '') {
            GetSelectedAreas(serviceId);
           // GettblCompanyAreas(serviceId);
            $('#ddlareaCode').attr("disabled", false);
        }
        else {
            $('#ddlareaCode').attr("disabled", true)
            $('#ddlareaCode').html('');
            $('#destAreaCodes').html('');
            $('#originAreaCodes').html('');
            $('#divTblAreas').html('');
        }
    });

    $('#saveSettings').click(function () {
        var serviceId = $('#ddlServiceID').val();
        var selected = [];

        if (serviceId == '') {
            alert("Please select Service Type");
            $('#ddlServiceID').focus();
            return false;
        }

        $(".multiselect").each(function (i, item) {

            var checkboxes = $(this).find("input:checkbox");
            checkboxes.each(function () {
                var checkbox = $(this);
                if (checkbox.prop("checked")) {
                    alert(this.id);
                    var areacode = this.id;
                    selected.push(areacode);
                }
            });
        });
        var data_to_send = JSON.stringify(selected);
        $.ajax({
            url: '/DestinationAreaCode/Turn_ON_OFF_CompanyDestAreaCodes',
            type: "POST",
            data: { 'serviceId': serviceId, 'areaCodes': data_to_send },
            success: function (data) {
                alert("Settings saved Successfully");
            },
            error: function (xhr, ajaxOptions, thrownError) {
            }
        });
    });
});
