var HotelSetupManagement = (function (hotelSetupManagement, $) {
    "use strict";
    hotelSetupManagement.hotelSetup = function () {
        var config = {
            base_url: null,
            file_url: "/",
            img_url: "/img/",
            state: {
                count: null,
                imgSrc: null
            },
            surroundings: {
                scount: null
            },
            feature: {
                fcount: null
            }
        };

        return ({
            renderIndex: function (AccommodationType) {
                $(document).ready(function () {
                    $('#hotelSetupManagement').DataTable({
                        "responsive": true,
                        "processing": true,
                        "serverSide": true,
                        "ajax": {
                            "type": 'POST',
                            "dataType": 'json',
                            "url": "/Admin/HotelSetup/GetHotelAccomodationList",
                            'data': function (json) {
                                return json;
                            }
                        },
                        "lengthMenu":
                            [
                                [25, 50, 100],
                                [25, 50, 100]
                            ],
                        "columns": [
                            { 'data': 'RowNum', "orderable": false },
                            { 'data': 'AccommodationType', "orderable": false },
                            { 'data': 'Action', "orderable": false }
                        ],
                        "columnDefs":
                            [
                                { "className": "text-center", "targets": '_all' }
                            ],
                    });

                    $('#hotelManagement').DataTable({
                        "responsive": true,
                        "processing": true,
                        "serverSide": true,
                        "ajax": {
                            "type": 'POST',
                            "dataType": 'json',
                            "url": "/Admin/HotelSetup/GetGridDetails",
                            'data': function (json) {
                                json.AccommodationType = AccommodationType;
                                return json;
                            }
                        },
                        "lengthMenu":
                            [
                                [25, 50, 100],
                                [25, 50, 100]
                            ],
                        "columns": [
                            { 'data': 'RowNum', "orderable": false },
                            { 'data': 'HotelCode', "orderable": false },
                            { 'data': 'HotelName', "orderable": false },
                            { 'data': 'Location', "orderable": false },
                            { 'data': 'ContactNo', "orderable": false },
                            { 'data': 'Status', "orderable": false },
                            { 'data': 'Action', "orderable": false }
                        ],
                        "columnDefs":
                            [
                                { "className": "text-center", "targets": '_all' }
                            ],
                    });
                   
                });
            },
            renderBookedInform: function () {
                $('#bookedHotelManagement').DataTable({
                    "responsive": true,
                    "processing": true,
                    "serverSide": true,
                    "ajax": {
                        "type": 'POST',
                        "dataType": 'json',
                        "url": "/Admin/HotelSetup/GetBookedHotelList",
                        'data': function (json) {
                            return json;
                        }
                    },
                    "lengthMenu":
                        [
                            [25, 50, 100],
                            [25, 50, 100]
                        ],
                    "columns": [
                        { 'data': 'RowNum', "orderable": false },
                        { 'data': 'HotelCode', "orderable": false },
                        { 'data': 'HotelName', "orderable": false },
                        { 'data': 'BookedBy', "orderable": false },
                        { 'data': 'BookedDate', "orderable": false },
                        { 'data': 'Status', "orderable": false },
                    ],
                    "columnDefs":
                        [
                            { "className": "text-center", "targets": '_all' }
                        ],
                });
                $('#bookedPackageManagement').DataTable({
                    "responsive": true,
                    "processing": true,
                    "serverSide": true,
                    "ajax": {
                        "type": 'POST',
                        "dataType": 'json',
                        "url": "/Admin/HotelSetup/GetBookedPackagelList",
                        'data': function (json) {
                            return json;
                        }
                    },
                    "lengthMenu":
                        [
                            [25, 50, 100],
                            [25, 50, 100]
                        ],
                    "columns": [
                        { 'data': 'RowNum', "orderable": false },
                        { 'data': 'PackageCode', "orderable": false },
                        { 'data': 'PackageName', "orderable": false },
                        { 'data': 'BookedBy', "orderable": false },
                        { 'data': 'BookedDate', "orderable": false },
                        { 'data': 'Status', "orderable": false },
                    ],
                    "columnDefs":
                        [
                            { "className": "text-center", "targets": '_all' }
                        ],
                });
            },
            renderManage: function () {
                $("#HotelImageFile").on('change', function () {
                    var file = this.files[0];
                    if (file) {
                        var reader = new FileReader();
                        reader.onload = function (event) {
                            $("#previewImg").attr("src", event.target.result);
                            $("#previewImg").css("display", "block");
                        }
                        reader.readAsDataURL(file);
                    }
                });
                $("#addAvailabilityRow").click(function () {
                    debugger
                    config.state.count = $('#Availability-items').children().length - 2;
                    var AvailabilityScript = $("#Availability-items-template").html();
                    var AvailabilityTemplate = Handlebars.compile(AvailabilityScript);
                    var AvailabilityEntryHtml = AvailabilityTemplate(config.state);
                    $('#Availability-items').append(AvailabilityEntryHtml);
                });
                $(document).on("click", ".deleteAvailabilityRow", function () {
                    if ($("#Availability-items").children().length > 1) {
                        $(this).closest("tr").remove();
                        viewModel.resetFormIndex($('tr.entryItem'));
                        viewModel.sumAmounts("#Availability-items");
                    }
                    else {
                        Swal.fire({
                            title: "Invalid Request",
                            text: "At least one entry is required.",
                            type: 'info',
                            confirmButtonColor: '#3085d6',
                            confirmButtonText: 'Ok'
                        });
                    }
                });

                $("#addSurroundingsRow").click(function () {
                    config.surroundings.scount = $('#Surroundings-items').children().length - 2;
                    var SurroundingsScript = $("#Surroundings-items-template").html();
                    var SurroundingsTemplate = Handlebars.compile(SurroundingsScript);
                    var SurroundingsEntryHtml = SurroundingsTemplate(config.state);
                    $('#Surroundings-items').append(SurroundingsEntryHtml);
                });
                $(document).on("click", ".deleteSurroundingsRow", function () {
                    if ($("#Surroundings-items").children().length > 1) {
                        $(this).closest("tr").remove();
                        viewModel.resetFormIndex($('tr.entryItem'));
                        viewModel.sumAmounts("#Surroundings-items");
                    }
                    else {
                        Swal.fire({
                            title: "Invalid Request",
                            text: "At least one entry is required.",
                            type: 'info',
                            confirmButtonColor: '#3085d6',
                            confirmButtonText: 'Ok'
                        });
                    }
                });
                $("#addRow").click(function () {
                    config.feature.fcount = $('#Feature-items').children().length - 2;
                    var FeatureScript = $("#Feature-items-template").html();
                    var FeatureTemplate = Handlebars.compile(FeatureScript);
                    var FeatureEntryHtml = FeatureTemplate(config.state);
                    $('#Feature-items').append(FeatureEntryHtml);
                });
                $(document).on("click", ".deleteRow", function () {
                    if ($("#Feature-items").children().length > 1) {
                        $(this).closest("tr").remove();
                        viewModel.resetFormIndex($('tr.entryItem'));
                        viewModel.sumAmounts("#Feature-items");
                    }
                    else {
                        Swal.fire({
                            title: "Invalid Request",
                            text: "At least one entry is required.",
                            type: 'info',
                            confirmButtonColor: '#3085d6',
                            confirmButtonText: 'Ok'
                        });
                    }
                });

            },
            renderGalary: function () {
                $("#addRow").click(function () {
                    config.state.count = $('#Feature-items').children().length;
                    var FeatureScript = $("#Feature-items-template").html();
                    var FeatureTemplate = Handlebars.compile(FeatureScript);
                    var FeatureEntryHtml = FeatureTemplate(config.state);
                    $('#Feature-items').append(FeatureEntryHtml);
                });
                $(document).on("click", ".deleteRow", function () {
                    if ($("#Feature-items").children().length > 1) {
                        $(this).closest("tr").remove();
                        viewModel.resetFormIndex($('tr.entryItem'));
                        viewModel.sumAmounts("#Feature-items");
                    }
                    else {
                        Swal.fire({
                            title: "Invalid Request",
                            text: "At least one entry is required.",
                            type: 'info',
                            confirmButtonColor: '#3085d6',
                            confirmButtonText: 'Ok'
                        });
                    }
                });
            }
        });
    };
    return hotelSetupManagement;
}(HotelSetupManagement || {}, jQuery));