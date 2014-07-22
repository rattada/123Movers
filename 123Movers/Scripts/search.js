$(document).ready(function () {
    var tableRows = 0;
    tableRows = $('#info tr').length;

    var Count = $("#CompanyCount").text().split('-');

    if (Count[0] == 0) {
        removeSorting();
    }
    else if (tableRows > 1) {
        $("#info").tablesorter();
    }
    
    $('#btnSearch').click(function () {
        if (CheckForInputs() == false)
            return false;
    });

    function log(str) {
        var self = this,
            settings = {
                hide: 'fadeOut',
                show: 'fadeIn',
                speed: 'fast',
                timeout: 1000
            };

        $('.alert-box').html(str)[settings.show]();

        setTimeout(function () {
            $('.alert-box')[settings.hide]();
        }, settings.timeout);
    }

    $("body").parent().on("keypress", "#CompanyId,#InsertionOrderId", function (event) {
        if ((event.which != 46 || $(this).val().indexOf('.') != -1) && (event.which < 48 || event.which > 57)) {
            event.preventDefault();
        }
    });

    //Getting the records when "Enter" key is Pressed.. 
    $(document).keypress(function (e) {
        if (e.which === 13) {
            if (CheckForInputs() == true) {
                $("form").submit();
            }
        }
    });

    //Check for all TextBox values whether they are empty or not returns boolean value...
    function CheckForInputs() {
        var companyname = $.trim($("#CompanyName").val());
        var companyId = $.trim($("#CompanyId").val());
        var ax = $.trim($("#AX").val());
        var insertionOrderId = $.trim($("#InsertionOrderId").val());
        if (companyname == "" && companyId == "" && ax == "" && insertionOrderId == "") {
            alert('Please enter atleast one value');
            $("#CompanyCount").text("0- Results");
            $("#info").find("tr:gt(0)").remove();
            $("#info tbody").append("<tr><td class='col-md-6 text-center' colspan='10'><b>No Results Found </b></td></tr>");
            $("#info").tablesorter({
                headers: {
                    0: { sorter: false },
                    1: { sorter: false },
                    2: { sorter: false },
                    3: { sorter: false }
                }
            });
            $('#info th').removeClass('header');
            return false;
        }
        else {
            $("#CompanyName").val(companyname);
            $("#AX").val(ax);
            return true;
        }
    }

    //remove Sorting functionality if No resocrds found...
    function removeSorting() {
        $("#info").tablesorter({
            headers: {
                0: { sorter: false },
                1: { sorter: false },
                2: { sorter: false },
                3: { sorter: false }
            }
        });
        $('#info th').removeClass('header');
    }
});