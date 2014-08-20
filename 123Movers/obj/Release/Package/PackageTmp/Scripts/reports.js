$(function () {
    $(".datepicker").datepicker();
    $('#btnSubmit').on('click', function () {
        var dateFrom = $('#dateFrom').val();
        var dateTo = $('#dateTo').val();
        if (dateFrom == '' && dateTo == '') { alert('Please Enter From And To Dates'); }
        else if (dateFrom == '') { alert('Please Enter From Date'); }
        else if (dateTo == '') { alert('Please Enter To Date'); }
    });
});