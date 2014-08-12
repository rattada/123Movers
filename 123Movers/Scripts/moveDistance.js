(function ($) {
    var message = $.trim($('#message').val());
    if (typeof message === 'undefined' || message.length <= 0) { } else { alert(message); }
    var orgMinWeight = $.trim($("#MinMoveDistance").val());
    var orgMaxWeight = $.trim($("#MaxMoveDistance").val());

    $("body").parent().on("keypress", "input:text", function (event) {
        var key = event.which;

        if (!(key >= 48 && key <= 57))
            event.preventDefault();
    });
    $("body").parent().on('click', "#saveDistance", function () {
        var serviceId = $('#ddlServiceID').val();
        var minWeight = $.trim($("#MinMoveDistance").val());
        var maxWeight = $.trim($("#MaxMoveDistance").val());

        if (serviceId == '' || serviceId == undefined) {
            alert('Please select Service Type');
            $("#ddlServiceID").focus();
            return false;
        }
        if (serviceId != null && serviceId != '') {
            if ((minWeight.length == 0 && maxWeight.length == 0) || (parseInt(minWeight) == 0 && parseInt(maxWeight) == 0)) {
                alert('Please enter atleast one Distance');
                return false;
            }
        }
        if (parseInt(minWeight) >= parseInt(maxWeight) && parseInt(minWeight) != 0 && parseInt(maxWeight) != 0) {
            alert('Min Move Distance should be less than Max Move Distance');
            $("#MinMoveDistance").val(orgMinWeight);
            $("#MaxMoveDistance").val(orgMaxWeight);
            $("#MinMoveDistance").focus();
            return false;
        }
        else {
            orgMinWeight = $("#MinMoveDistance").val();
            orgMaxWeight = $("#MaxMoveDistance").val();
        }
        var ModelData =
            {
                ServiceId: serviceId,
                MinMoveDistance: orgMinWeight,
                MaxMoveDistance: orgMaxWeight
            };
        $.ajax({
            url: '/MoveDistance/MoveDistance',
            type: "POST",
            data: ModelData,
            cache: false,
            success: function (data) {
                alert("Distance data successfully saved");
            },
            error: function (xhr, ajaxOptions, thrownError) {
            }
        });
    });
    $("body").parent().on("change", "#ddlServiceID", function () {
        var serviceId = $('#ddlServiceID option:selected').val();
        if (serviceId != '') {
            $.ajax({
                url: '/MoveDistance/GetMoveDistance',
                type: "GET",
                data: { 'ServiceId': serviceId },
                cache: false,
                success: function (data) {
                    var strigifyJson = JSON.stringify(data);
                    var json = $.parseJSON(strigifyJson);
                    if (json.length > 0) {
                        $("#MinMoveDistance").val(json[0][1]);
                        $("#MaxMoveDistance").val(json[0][2]);

                        orgMinWeight = $("#MinMoveDistance").val();
                        orgMaxWeight = $("#MaxMoveDistance").val();
                    }
                    else {
                        orgMinWeight = '';
                        orgMaxWeight = '';
                        $("#MinMoveDistance").val('');
                        $("#MaxMoveDistance").val('');
                    }
                },
                error: function (xhr, ajaxOptions, thrownError) {
                }
            });
        } else {
            $("#MinMoveDistance").val('');
            $("#MaxMoveDistance").val('');
        }
    });
}(jQuery));
