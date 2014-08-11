$(function () {
    var serviceId = $('#ddlServiceID').val();
    if (serviceId != '') {
        GetAvailStates(serviceId);
    } else { $('#ddlState').attr("disabled", true); }
   
    function GetAvailStates(serviceId) {
        $.ajax({
            url: '/SpecificStates/GetAvailStates',
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
                        options = '<option value="' + val[0] + '">' + val[0] + '</option>';
                    } else {
                        options += '<option value="' + val[0] + '">' + val[0] + '</option>';
                    }
                });
                if (json.length > 0) {
                    options = '<option value="">' + "--Choose One--" + '</option>' + options;
                    $('#ddlState').html(options);
                    $('#destStates').html('');
                    $('#originStates').html('');
                    $('#ddlState').attr("disabled", false);
                }
                else {
                    $('#ddlState').html('');
                    $('#destStates').html('');
                    $('#originStates').html('');
                    $('#ddlState').attr("disabled", true);
                }
            },
            error: function (xhr, ajaxOptions, thrownError) {
            }
        });
    }
    function GetCompanySpcfcStates(serviceId, originState) {
        $.ajax({
            url: '/SpecificStates/GetCompanySpcfcOriginDestStates',
            type: "GET",
            data: { 'serviceId': serviceId, "originState": originState, "IsOriginState": false },
            dataType: "json",
            cache: false,
            success: function (data) {
                var strigifyJson = JSON.stringify(data);
                var json = $.parseJSON(strigifyJson);
                var options;
                var destoptions;
                var state = $.trim($('#ddlState').val());
                $.each(json, function (i, val) {
                    if (val[1] == 'Table') {
                        if (state != val[0]) {
                            if (options == undefined) {
                                options = '<option value="' + val[0] + '">' + val[0] + '</option>';
                            } else {
                                options += '<option value="' + val[0] + '">' + val[0] + '</option>';
                            }
                        }
                    } else {
                        if (destoptions == undefined) {
                            destoptions = '<option value="' + val[0] + '">' +  val[0] + '</option>';
                        } else {
                            destoptions += '<option value="' + val[0] + '">' +  val[0] + '</option>';
                        }
                    }
                });

                if (options != undefined) {
                    $('#originStates').html(options);
                }
                else {
                    $('#originStates').html('');
                }

                if (destoptions != undefined) {
                    $('#destStates').html(destoptions);
                }
                else {
                    $('#destStates').html('');
                }
            },
            error: function (xhr, ajaxOptions, thrownError) {
            }
        });
    }

    $('#add').click(function () {
        var serviceId = $('#ddlServiceID').val();
        var Spcfcstate = $('#ddlState').val();
        var selected = [];
        $('#originStates :selected').each(function (i, el) {
            selected[i] = $(this).val();
        });
        if (serviceId == '') {
            alert("Please Select Service Type");
            $('#ddlServiceID').focus();
            return false;
        }
        if (Spcfcstate == '') {
            alert("Please Select Origin originStates");
            $('#ddlState').focus();
            return false;
        }
        if (selected.length == 0) {
            alert("Please Select any Option to add");
            $('#originStates').focus();
            return false;
        }
        var states = JSON.stringify(selected);
        $.ajax({
            url: '/SpecificStates/AddCompanySpcfcOriginDeststates',
            type: "POST",
            data: { 'serviceId': serviceId, "originState": Spcfcstate, 'destStates': states },
            success: function (data) {
                GetCompanySpcfcStates(serviceId, Spcfcstate);
                alert("States added Successfully");
            },
            error: function (xhr, ajaxOptions, thrownError) {
            }
        });
    });
    $('#remove').click(function () {
        var serviceId = $('#ddlServiceID').val();
        var Spcfcstate = $('#ddlState').val();
        var selected = [];
        $('#destStates :selected').each(function (i, el) {
            selected[i] = $(this).val();
        });
        if (selected.length == 0) {
            alert("Please Select any Option to Remove")
            $('#destStates').focus();
            return false;
        }
        var states = JSON.stringify(selected);
        $.ajax({
            url: '/Specificstates/DeleteCompanySpcfcOriginDeststates',
            type: "POST",
            data: { 'serviceId': serviceId, "originState": Spcfcstate, "destStates": states },
            success: function (data) {
                GetCompanySpcfcStates(serviceId, Spcfcstate);
                alert("States removed Successfully");
            },
            error: function (xhr, ajaxOptions, thrownError) {
            }
        });
    });
    $("body").parent().on("change", "#ddlServiceID", function () {
        var serviceId = $('#ddlServiceID').val();
        if (serviceId != '') {
            GetAvailStates(serviceId);
        }
        else {
            $('#ddlState').html('');
            $('#destStates').html('');
            $('#originStates').html('');
            $('#ddlState').attr("disabled", true);
        }
    });
    $("body").parent().on("change", "#ddlState", function () {
        var serviceId = $('#ddlServiceID').val();
        var originState = $('#ddlState').val();
        if (originState != '') {
            GetCompanySpcfcStates(serviceId, originState);
        }
        else {
            $('#originStates').html('');
            $('#destStates').html('');
        }
    });
});
