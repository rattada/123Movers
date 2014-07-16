﻿$(function () {
    var message = $.trim($('#message').val());
    if (typeof message === 'undefined' || message.length <= 0) { } else { alert(message); }

    var orgMax = $('#maxMoveWeight option:selected').val();
    var orgMin = $('#minMoveWeight option:selected').val();


    $('#savemoveweight').click(function () {
        var serviceId = $('#services').val().trim();
        var max = $('#maxMoveWeight option:selected').val();
        var min = $('#minMoveWeight option:selected').val();

        if (serviceId == '' || serviceId == undefined) {
            alert('Please Select Service Type');
            $("#services").focus();
            return false;
        } else {
            if ($("#minMoveWeight").val() <= 0) {
                alert('Please Select Min Move Weight Value');
                $("#minMoveWeight").focus();
                return false;
            }
            if ($("#maxMoveWeight").val() <= 0) {
                alert('Please Select Max Move Weight Value');
                $("#maxMoveWeight").focus();
                return false;
            }
            if (parseInt(min) >= parseInt(max)) {
                alert('Min Move Weight Should be Less Than Max Move Weight');
                $('#minMoveWeight').val(orgMin).attr("selected", "selected");
                $('#maxMoveWeight').val(orgMax).attr("selected", "selected");
                return false;
            } else {
                orgMax = $('#maxMoveWeight option:selected').val();
                orgMin = $('#minMoveWeight option:selected').val();
            }
        }

        var ModelData =
           {
               ServiceId: serviceId,
               MinMoveWeightSeq: orgMin,
               MaxMoveWeightSeq: orgMax

           };
        $.ajax({
            url: '/MoveWeight/MoveWeight',
            type: "POST",
            data: ModelData,
            cache: false,
            success: function (data) {
                alert("MoveWeight  data successfully saved");
            },
            error: function (xhr, ajaxOptions, thrownError) {

            }
        });



    });
    $('#services').on("change", function () {
        var serviceId = $('#services option:selected').val();
        if (serviceId != '') {
            $.ajax({
                url: '/MoveWeight/GetMoveWeight',
                type: "GET",
                data: { 'ServiceId': serviceId },
                cache: false,
                success: function (data) {
                    var strigifyJson = JSON.stringify(data);
                    var json = $.parseJSON(strigifyJson);
                    if (json.length > 0) {
                        $('#minMoveWeight').val(json[0][2]).attr("selected", "selected");
                        $('#maxMoveWeight').val(json[0][3]).attr("selected", "selected");

                        orgMax = $('#maxMoveWeight option:selected').val();
                        orgMin = $('#minMoveWeight option:selected').val();
                    }
                    else {
                        orgMin = '';
                        orgMax = '';
                        $("#minMoveWeight option:first").attr('selected', 'selected');
                        $("#maxMoveWeight option:first").attr('selected', 'selected');
                    }
                },
                error: function (xhr, ajaxOptions, thrownError) {

                }
            });
        }
        else {
            $("#minMoveWeight option:first").attr('selected', 'selected');
            $("#maxMoveWeight option:first").attr('selected', 'selected');
        }
    });
});