$(function () {

    $('#tblLeadLimit tbody tr').each(function () {
        $(this).find(".classCheckVal:input[type=text]").each(function () {

            if ($(this).val() != 0 && $(this).val() != '')
                $(this).attr("disabled", false);


        });
    });

    $("#saveleads").attr('disabled', true);

    $("body").on("keypress", "input", function (event) {
        var key = event.which;

        if (!(key >= 48 && key <= 57))
            event.preventDefault();
        else
            $("#saveleads").removeAttr('disabled');
    });

    $("#saveleads").click(function () {
        var count = 0;
        var required = "";
        var Array = [];
        var values = "";
        var LeadLimitData = [];
        var Filleddata = [];

        $('#tblLeadLimit tbody tr').each(function () {

            //Checking for Only checked checkboxes

            $(this).find('.chkSelectClass:checkbox:checked').each(function () {


                var row = $(this).closest('tr');
                var columns = row.find('td');

                $.each(columns, function (i, item) {
                    if (i > 2) {
                        values = values + "," + $(this).closest('td').find("input:text").val().trim();

                    }
                    else {
                        if (i == 0)
                            values = item.textContent;
                        else
                            values = values + "," + item.textContent.trim();
                    }



                });
                //saving lsit of checked item data in a Array.
                Array.push(values);
            });
        });1009


        //looping through the Array  and storing in a Array list 
        $.each(Array, function (i, item) {

            required = item.split(",")


            if (required[2] == 'Default') {
                required[2] = null;

            }


            if (required[1] == 'Local') {
                required[1] =1009;

            }
            else if (required[1] == 'Long') {
                required[1] =1000;

            }
            else {
                required[1] = null;
            }


            LeadLimitData =
           [{
               AreaCodes: required[2],
               ServiceId: required[1],
               LeadFrequency: required[3],
               DailyLeadLimit: required[4],
               MonthlyLeadLimit: required[5],
               TotalLeadLimit: required[6]
           }]

            Filleddata.push(LeadLimitData);

        });

        //Saving the  entire data in DB with Ajax call

        if (Filleddata.length != 0) {
            $.ajax({

                url: '/LeadLimit/LeadLimit',
                cache: false,
                type: 'POST',
                dataType: "json",
                data: JSON.stringify(Filleddata),// Comverting the list items to JSON format(Because POST method accepts  only JSON data)
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    alert('Successfully Updated:');
                    location.reload();
                },
                error: function () {
                    alert('Service call failed');
                }
            });

        }
        else {
            alert("Please Select any ");
            return false;

        }

    });


    $("body #tblLeadLimit ").parent().on('change', ".chkInnerClass", function () {

        if ($(this).prop('checked') == true) {
    
            $(this).closest("td").find("input[type=text]").prop("disabled", false);
        }
        else {
            $(this).closest("td").find("input[type=text]").prop("value", "");
            $(this).closest("td").find("input[type=text]").prop("disabled", true);
        }
    });


    $("#rdbtnboth").on("change", function () {
        if ($(this).prop('checked') == true) {
            $("#txtFrequencyLocal").attr("disabled", true);
            $("#txtFrequencyLong").attr("disabled", true);
            $("#rdbtnLocal").prop("checked", false);
            $("#rdbtnLong").prop("checked", false);
        };
    });

    $("#rdbtnLocal,#rdbtnLong").on("change", function () {
        $("#rdbtnboth").prop("checked", false);
        $("#txtFrequencyboth").attr("disabled", true);

    });

    $("body #tblLeadLimit ").parent().on('change', ".chkSelectClass", function () {
        if ($(this).prop('checked') == true) {
            //Enable the frequency texboxes
            $(this).closest('tr').find(".freqEnable").attr("disabled", false);
        }
        else {
            $(this).closest('tr').find(".freqEnable").attr("disabled", true);
        }


    });



});