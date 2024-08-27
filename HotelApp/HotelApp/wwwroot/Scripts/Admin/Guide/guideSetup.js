var GuideSetupManagement = (function (guideSetupManagement, $) {
    "use strict";
    guideSetupManagement.guideSetup = function () {
        var config = {
            base_url: null,
            file_url: "/",
            img_url: "/img/",
            state: {
                count: null
            }
        };

        return ({
            renderIndex: function (VechicleType) {
                $(document).ready(function () {
                    $('#guideSetupManagement').DataTable({
                        "responsive": true,
                        "processing": true,
                        "serverSide": true,
                        "ajax": {
                            "type": 'POST',
                            "dataType": 'json',
                            "url": "/Admin/GuideSetup/GetRequiredDetailList",
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
                            { 'data': 'Name', "orderable": false },
                            { 'data': 'SpecializedRegion', "orderable": false },
                            { 'data': 'Experience', "orderable": false },
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
                $("#GuideImageFile").on('change', function () {
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
    return guideSetupManagement;
}(GuideSetupManagement || {}, jQuery));