var jQury = $.noConflict();

jQury(function () {
    var serviceId;
    var ServiceType;
    var k = 0;
    serviceId = jQury('#ddlServiceID').val();
    spcfcareacode = jQury('#ddlareaCode').val();
    // GetAvailableAreas(serviceId);
    function GetAvailableAreas(serviceId) {
        jQury.ajax({
            url: '/SpecificOriginAreaCodes/GetAvailSpcfcOriginDestAreas',
            type: "GET",
            data: { 'serviceId': serviceId },
            dataType: "json",
            cache: false,
            success: function (data) {
                var strigifyJson = JSON.stringify(data);
                var json = jQury.parseJSON(strigifyJson);
                var table;
                var options = '<option value=""></option>';
                jQury.each(json, function (i, val) {
                    if (i == 0) {
                        options = '<option value="' + val[0] + '">' + val[1] + ' - ' + val[0] + '</option>';
                    } else {
                        options += '<option value="' + val[0] + '">' + val[1] + ' - ' + val[0] + '</option>';
                    }
                });

                if (json.length > 0) {
                    options = '<option value="">' + "--Choose One--" + '</option>' + options;
                    jQury('#ddlareaCode').html(options);
                    jQury('#areasSelected').html('');
                    jQury('#areaCode').html('');
                }
            },
            error: function (xhr, ajaxOptions, thrownError) {
            }
        });
    }
    function GetSelectedAreas(serviceId, spfcareacode) {
        //OriginAreaCodes
        jQury.ajax({
            url: '/SpecificOriginAreaCodes/GetCompanySpcfcOriginDestAreas',
            type: "GET",
            data: { 'serviceId': serviceId, "spcfcareacode": spfcareacode, "originAreaCode": false },
            dataType: "json",
            cache: false,
            success: function (data) {
                var strigifyJson = JSON.stringify(data);
                var json = jQury.parseJSON(strigifyJson);
                var table;
                var options = '<option value=""></option>';
                jQury.each(json, function (i, val) {
                    if (i == 0) {
                        options = '<option value="' + val[0] + '">' + val[1] + ' - ' + val[0] + '</option>';
                    } else {
                        options += '<option value="' + val[0] + '">' + val[1] + ' - ' + val[0] + '</option>';
                    }
                });
                if (json.length > 0) {
                    jQury('#areaCode').html(options);
                }
                else {
                    jQury('#areaCode').html('');
                }
            },
            error: function (xhr, ajaxOptions, thrownError) {
            }
        });

        //DestinationAreaCodes
        jQury.ajax({
            url: '/SpecificOriginAreaCodes/GetCompanySpcfcOriginDestAreas',
            type: "GET",
            data: { 'serviceId': serviceId, "spcfcareacode": spfcareacode, "originAreaCode": true },
            dataType: "json",
            cache: false,
            success: function (data) {
                var strigifyJson = JSON.stringify(data);
                var json = jQury.parseJSON(strigifyJson);
                var options = '<option value=""></option>';
                jQury.each(json, function (i, val) {
                    if (i == 0) {
                        options = '<option value="' + val[0] + '">' + val[1] + ' - ' + val[0] + '</option>';
                    } else {
                        options += '<option value="' + val[0] + '">' + val[1] + ' - ' + val[0] + '</option>';
                    }
                });
                if (json.length > 0) {
                    jQury('#areasSelected').html(options);
                }
                else {
                    jQury('#areasSelected').html('');
                }
            },
            error: function (xhr, ajaxOptions, thrownError) {
            }
        });
    }
    jQury('#add').click(function () {

        var serviceId = jQury('#ddlServiceID').val();
        var SpcfcAreaCode = jQury('#ddlareaCode').val();

        var selected = [];
        jQury('#areaCode :selected').each(function (i, el) {
            selected[i] = jQury(this).val();
        });

        if (serviceId == '') {
            alert("Please Select Service Type");
            jQury('#ddlServiceID').focus();
            return false;
        }
        if (SpcfcAreaCode == '') {

            alert("Please Select Origin AreaCode");
            jQury('#ddlareaCode').focus();
            return false;
        }
        if (selected.length == 0) {

            alert("Please Select any Option to add");
            jQury('#areaCode').focus();
            return false;
        }
        var areaCodes = JSON.stringify(selected);
        jQury.ajax({
            url: '/SpecificOriginAreaCodes/AddCompanySpcfcOriginDestAreaCodes',
            type: "POST",
            data: { 'serviceId': serviceId, "spcfcareacode": SpcfcAreaCode, 'areaCodes': areaCodes },
            success: function (data) {
                GetSelectedAreas(serviceId, SpcfcAreaCode);
                alert("Area Codes added Successfully");
            },
            error: function (xhr, ajaxOptions, thrownError) {
            }
        });
    });
    jQury('#remove').click(function () {
        var serviceId = jQury('#ddlServiceID').val();
        var SpcfcAreaCode = jQury('#ddlareaCode').val();
        var selected = [];
        jQury('#areasSelected :selected').each(function (i, el) {
            selected[i] = jQury(this).val();
        });
        if (selected.length == 0) {
            alert("Please Select any Option to Remove")
            jQury('#areasSelected').focus();
            return false;
        }
        var areaCodes = JSON.stringify(selected);
        jQury.ajax({
            url: '/SpecificOriginAreaCodes/DeleteCompanySpcfcOriginDestAreaCodes',
            type: "POST",
            data: { 'serviceId': serviceId, "spcfcareacode": SpcfcAreaCode, areaCodes: areaCodes },
            success: function (data) {
                GetSelectedAreas(serviceId, SpcfcAreaCode);
                alert("Area Codes removed Successfully");
            },
            error: function (xhr, ajaxOptions, thrownError) {
            }
        });
    });
    jQury("body").parent().on("change", "#ddlServiceID", function () {
        var serviceId = jQury('#ddlServiceID').val();
        var SpcfcAreaCode = jQury('#ddlareaCode').val();

        if (serviceId != '') {
            GetAvailableAreas(serviceId);
        }
        else {
            jQury('#ddlareaCode').html('');
            jQury('#areasSelected').html('');
            jQury('#areaCode').html('');
        }
    });
    jQury("body").parent().on("change", "#ddlareaCode", function () {
        var serviceId = jQury('#ddlServiceID').val();
        var SpcfcAreaCode = jQury('#ddlareaCode').val();
        if (SpcfcAreaCode != '') {
            GetSelectedAreas(serviceId, SpcfcAreaCode);
        }
        else {
            jQury('#areaCode').html('');
            jQury('#areasSelected').html('');
        }
    });
});