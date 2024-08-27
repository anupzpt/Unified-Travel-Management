var BannerSetupManagement = (function (bannerSetupManagement, $) {
    "use strict";
    bannerSetupManagement.bannerSetup = function () {
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
                    $('#bannerSetupManagement').DataTable({
                        "responsive": true,
                        "processing": true,
                        "serverSide": true,
                        "ajax": {
                            "type": 'POST',
                            "dataType": 'json',
                            "url": "/Admin/BannerSetup/GetRequiredDetailList",
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
                            { 'data': 'BannerCode', "orderable": false },
                            { 'data': 'SloganName', "orderable": false },
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
                $("#BannerImageFile").on('change', function () {
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
            }
        });
    };
    return bannerSetupManagement;
}(BannerSetupManagement || {}, jQuery));