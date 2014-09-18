$(document).ready(function () {
    $('.TitleStyle').text('Move Distance');
    var message = $.trim($('#message').val());
    if (typeof message === 'undefined' || message.length <= 0) { } else { alert(message); }
    if ($("#MinMoveDistance").val() == 0) {
        $("#MinMoveDistance").val("");
    }
    if ($("#MaxMoveDistance").val() == 0) {
        $("#MaxMoveDistance").val("");
    }
    var orgMinDistance = $.trim($("#MinMoveDistance").val());
    var orgMaxDistance = $.trim($("#MaxMoveDistance").val());

    $("body").parent().on("keypress", "input:text", function (event) {
        var key = event.which;

        if (!(key >= 48 && key <= 57))
            event.preventDefault();
    });
    $("body").parent().on('click', "#saveDistance", function () {
        var serviceId = $('#ddlServiceID').val();
        var minDistance = $.trim($("#MinMoveDistance").val());
        var maxDistance = $.trim($("#MaxMoveDistance").val());
        var msg = 'Please enter atleast one Distance';
        if (serviceId == '' || serviceId == undefined) {
            alert('Please select Service Type');
            $("#ddlServiceID").focus();
            return false;
        }

        if (serviceId != null && serviceId != '') {

            if (minDistance == "" && maxDistance == "") {
                alert(msg);
                $("#MinMoveDistance").val(orgMinDistance);
                $("#MaxMoveDistance").val(orgMaxDistance);
                return false;
            }

            if (minDistance == 0 && maxDistance == 0) {
                alert(msg);
                return false;
            }
        }
        minDistance = parseInt(minDistance);

        maxDistance = parseInt(maxDistance);


        if (minDistance >= maxDistance) {
            alert('Min Move Distance should be less than Max Move Distance');
            $("#MinMoveDistance").val(orgMinDistance);
            $("#MaxMoveDistance").val(orgMaxDistance);
            $("#MinMoveDistance").focus();
            return false;
        }
        else {
            orgMinDistance = $("#MinMoveDistance").val();
            orgMaxDistance = $("#MaxMoveDistance").val();
        }
        var modelData =
            {  
                ServiceId: serviceId,
                MinMoveDistance: orgMinDistance,
                MaxMoveDistance: orgMaxDistance
            };
        $.ajax({
            url: '/MoveDistance/MoveDistance',
            type: "POST",
            data: modelData,
            cache: false,
            success: function (data) {
                if (data.success)
                    alert("Distance data successfully saved");
                else {
                    alert("There were no area code(s) saved to this Service");
                    $("#MinMoveDistance").val('');
                    $("#MaxMoveDistance").val('');
                }
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
                data: {'ServiceId': serviceId },
                cache: false,
                success: function (data) {
                    var strigifyJson = JSON.stringify(data);
                    var json = $.parseJSON(strigifyJson);
                    if (json != undefined) {
                        $("#MinMoveDistance").val(json.MinMoveDistance);
                        $("#MaxMoveDistance").val(json.MaxMoveDistance);

                        orgMinDistance = $("#MinMoveDistance").val();
                        orgMaxDistance = $("#MaxMoveDistance").val();
                    }
                    else {
                        orgMinDistance = '';
                        orgMaxDistance = '';
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
});