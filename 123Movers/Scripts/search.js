$(document).ready(function () {
    var rowsCount = $('#info tr').length;
    if (rowsCount > 2) {
        $('#info').dataTable({ "sPaginationType": "full_numbers" });
    }
    else {
        $('#info').addClass('thead')
    }
    $('#btnSearch').click(function () {

        if (checkForInputs() == false)
            return false;
        else
            $("body").mask('Searching...');

    });

    $('.btnBudget').click(function () {
        $("body").mask('Loading...');
    });

    $("body").parent().on("keypress", "#CompanyId,#InsertionOrderId", function (event) {
        if ((event.which != 46 || $(this).val().indexOf('.') != -1) && (event.which < 48 || event.which > 57)) {
            event.preventDefault();
        }
    });
    //Getting the records when "Enter" key is Pressed.. 
    $(document).keypress(function (e) {
        if (e.which === 13) {
            if (checkForInputs() == false) {
                $('#info').html('');
                return false;
            }
            else
                $("form").submit();
        }
    });

    //Check for all TextBox values whether they are empty or not returns boolean value...
    function checkForInputs() {
        var companyname = $.trim($("#CompanyName").val());
        var companyId = $.trim($("#CompanyId").val());
        var ax = $.trim($("#AX").val());
        var insertionOrderId = $.trim($("#InsertionOrderId").val());
        if (companyname == "" && companyId == "" && ax == "" && insertionOrderId == "") {
            alert('Please enter atleast one value');
            $('#info').html('');
            $('#info_wrapper').html('');
            return false;
        }
        else {
            $("#CompanyName").val(companyname);
            $("#AX").val(ax);
            return true;
        }

    }
});