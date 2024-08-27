var FacilitySetupManagement = (function (facilitySetupManagement, $) {
    "use strict";
    facilitySetupManagement.facilitySetup = function () {
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
                        "ajax": {
                            "type": 'POST',
                            "dataType": 'json',
                            "url": "/Admin/FacilitySetup/GetRequiredDetailList",
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
                            { 'data': 'CategoryCode', "orderable": false },
                            { 'data': 'CategoryName', "orderable": false },
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

            }
        });
    };
    return facilitySetupManagement;
}(FacilitySetupManagement || {}, jQuery));