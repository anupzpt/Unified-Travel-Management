var PackageSetupManagement = (function (packageSetupManagement, $) {
    "use strict";
    packageSetupManagement.packageSetup = function () {
        var config = {
            base_url: null,
            file_url: "/",
            img_url: "/img/",
            state: {
                count: null
            },
            availability: {
                count: null
            },
            inclusion: {
                count: null
            },
        };

        return ({
            renderIndex: function (PackageType) {
                $(document).ready(function () {
                    $('#packageSetupManagement').DataTable({
                        "responsive": true,
                        "processing": true,
                        "serverSide": true,
                        "ajax": {
                            "type": 'POST',
                            "dataType": 'json',
                            "url": "/Admin/PackageSetup/GetRequiredDetailList",
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
                            { 'data': 'PackageType', "orderable": false },
                            { 'data': 'Action', "orderable": false }
                        ],
                        "columnDefs":
                            [
                                { "className": "text-center", "targets": '_all' }
                            ],
                    });
                    $('#packageInformationManagement').DataTable({
                        "responsive": true,
                        "processing": true,
                        "serverSide": true,
                        "ajax": {
                            "type": 'POST',
                            "dataType": 'json',
                            "url": "/Admin/PackageSetup/GetRequiredPackageDetailList",
                            'data': function (json) {
                                json.PackageType = PackageType;
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

            renderBookingList: function () {
                $(document).ready(function () {
                    $('#packageSetupManagement').DataTable({
                        "responsive": true,
                        "processing": true,
                        "serverSide": true,
                        "ajax": {
                            "type": 'POST',
                            "dataType": 'json',
                            "url": "/Home/GetRequiredDetailList",
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
                            { 'data': 'CodeType', "orderable": false },
                            { 'data': 'Name', "orderable": false },
                            { 'data': 'TotalAmount', "orderable": false },
                            { 'data': 'BookedDate', "orderable": false },
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
            renderManage: function () {
                MakeAutocomplete("#PackageItineraries_0__Accomodation", "Accommodation")

                $("#PackageImageFile").on('change', function () {
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
                $("#addRow").click(function () {
                    config.state.count = $('#Feature-items').children().length - 2;
                    var FeatureScript = $("#Feature-items-template").html();
                    var FeatureTemplate = Handlebars.compile(FeatureScript);
                    var FeatureEntryHtml = FeatureTemplate(config.state);
                    $('#Feature-items').append(FeatureEntryHtml);
                    MakeAutocomplete("#PackageItineraries_" + config.state.count + "__Accomodation", "Accommodation")
                });
                $("#addPackageAvailabilityRow").click(function () {
                    debugger
                    config.availability.count = $('#PackageAvailability-items').children().length;
                    var PackageAvailabilityScript = $("#PackageAvailability-items-template").html();
                    var PackageAvailabilityTemplate = Handlebars.compile(PackageAvailabilityScript);
                    var PackageAvailabilityEntryHtml = PackageAvailabilityTemplate(config.availability);
                    $('#PackageAvailability-items').append(PackageAvailabilityEntryHtml);
                });
                $("#addPackageInclusionExcludeRow").click(function () {
                    config.inclusion.count = $('#PackageInclusionExclude-items').children().length;
                    var PackageInclusionExcludeScript = $("#PackageInclusionExclude-items-template").html();
                    var PackageInclusionExcludeTemplate = Handlebars.compile(PackageInclusionExcludeScript);
                    var PackageInclusionExcludeEntryHtml = PackageInclusionExcludeTemplate(config.inclusion);
                    $('#PackageInclusionExclude-items').append(PackageInclusionExcludeEntryHtml);
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
    return packageSetupManagement;
}(PackageSetupManagement || {}, jQuery));