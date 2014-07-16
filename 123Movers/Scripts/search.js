$(document).ready(function () {

    var tableRows = 0;
    tableRows = $('#info tr').length;

    if (tableRows > 1) {
        $("#info").tablesorter();
    }

    $('#btnSearch').click(function () {
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
            $('#info th').removeAttr('class');
            return false;
        }

    });

    function log(str) {
        var self = this,
            settings = {
                hide: 'fadeOut',
                show: 'fadeIn',
                speed: 'slow',
                timeout: 8000
            };

        $('.alert-box').html(str)[settings.show]();

        setTimeout(function () {
            $('.alert-box')[settings.hide]();
        }, settings.timeout);
    }

});