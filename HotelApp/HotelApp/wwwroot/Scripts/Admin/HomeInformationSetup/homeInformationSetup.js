var HomeInformationSetupManagement = (function (homeInformationSetupManagement, $) {
    "use strict";
    homeInformationSetupManagement.homeInformationSetup = function () {
        var config = {
            base_url: null,
            file_url: "/",
            img_url: "/img/",
            state: {
            }
        };


        return ({
            renderIndex: function () {
                $(document).ready(function () {
                    $('#facilitySetupManagement').DataTable({
                        "responsive": true,
                        "processing": true,
                        "serverSide": true,
                        "searchDelay": 1500,
                        "ajax": {
                            "type": 'POST',
                            "dataType": 'json',
                            "url": "/Claim/GetRequiredDetailList",
                            "contentType": 'application/json; charset=utf-8',
                            'data': function (data) {
                                return data = JSON.stringify(data);
                            }
                        },
                        lengthMenu:
                            [
                                [25, 50, 100],
                                ['25', '50', '100']
                            ],
                        "language": {
                            "search": "",
                            "searchPlaceholder": "Search..."
                        },
                        columns: [
                            { 'data': 'RowNum', "orderable": true },
                            { 'data': 'ClaimId', "orderable": true },
                            { 'data': 'Action', "orderable": false },
                        ],
                        "columnDefs":
                            [
                                { "className": "text-center", "targets": '_all' }
                            ]
                    });
                });
            },
        });
    };

    homeInformationSetupManagement.homefacilitySetup = function () {
        var config = {
            base_url: null,
            file_url: "/",
            img_url: "/img/",
            state: {
            }
        };
        return ({
            renderIndex: function () {
                $(document).ready(function () {
                    $('#facilitySetupManagement').DataTable({
                        "responsive": true,
                        "processing": true,
                        "serverSide": true,
                        "searchDelay": 1500,
                        "ajax": {
                            "type": 'POST',
                            "dataType": 'json',
                            "url": "/Claim/GetRequiredDetailList",
                            "contentType": 'application/json; charset=utf-8',
                            'data': function (data) {
                                return data = JSON.stringify(data);
                            }
                        },
                        lengthMenu:
                            [
                                [25, 50, 100],
                                ['25', '50', '100']
                            ],
                        "language": {
                            "search": "",
                            "searchPlaceholder": "Search..."
                        },
                        columns: [
                            { 'data': 'RowNum', "orderable": true },
                            { 'data': 'ClaimId', "orderable": true },
                            { 'data': 'Action', "orderable": false },
                        ],
                        "columnDefs":
                            [
                                { "className": "text-center", "targets": '_all' }
                            ]
                    });
                });
            },
        });
    };
    return homeInformationSetupManagement;
}(HomeInformationSetupManagement || {}, jQuery));