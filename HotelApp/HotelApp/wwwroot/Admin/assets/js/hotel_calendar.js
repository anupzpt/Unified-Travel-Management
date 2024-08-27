var dateFormat = "dd/mm/yy";

Date.prototype.addDays = function (days) {
    this.setDate(this.getDate() + parseInt(days));
    return this;
};
function ShowCalDefault(cal, nextFocus,isCallBack) {
    $(cal).attr('readonly', true);
        $(function () {
            $(cal).datepicker({
                changeMonth: true,
                changeYear: true,
                showOn: "both",
                dateFormat: dateFormat,
                buttonImage: "",
                buttonImageOnly: true,

                onClose: function () {
                    if (nextFocus !=undefined) {
                        $("#" + nextFocus).focus();
                    }
                    if (isCallBack) {
                        CallBackEng();
                    }
                    
                }
            });
        });
}


function ShowNepaliCalendar(cal) {
    $(function () {
        $(cal).nepaliDatePicker({
            npdMonth: true,
            npdYear: true,
            dateFormat: dateFormat,
            npdYearCount: 100
        });
    });
}

function ShowNepEngCalendar(nepTextBox, engTextBox, isCallBack) {
    $(function () {
        $(nepTextBox).nepaliDatePicker({
            npdMonth: true,
            npdYear: true,
            npdYearCount: 50,
            onChange: function () {
                var ad = BS2AD($(nepTextBox).val());
                var dt = ad.split("-");
                $(engTextBox).val(dt[2] + "/" + dt[1] + "/" + dt[0]);
                $(engTextBox).focus();
                if (isCallBack) {
                    CallBack();
                }
            }
        });
    });
}
function ShowNepEngCalendarUptoToday(nepTextBox, engTextBox, nepToday) {
    $(function () {
        $(nepTextBox).nepaliDatePicker({
            npdMonth: true,
            npdYear: true,
            npdYearCount: 50,
            disableAfter: nepToday,
            onChange: function () {
                var ad = BS2AD($(nepTextBox).val());
                var dt = ad.split("-");
                $(engTextBox).val(dt[2] + "/" + dt[1] + "/" + dt[0]);
                $(engTextBox).focus();
                //if (isCallBack) {
                //    CallBack();
                //}
            }
        });
    });
}



function addDays(date, days) {
    var result = new Date(date);
    result.setDate(result.getDate() + days);
    return new Date(result).toLocaleDateString('fr-CA');
}

function MakeAutocomplete(field,flag) {
    $(function () {
        $(field).autocomplete({
            source: function (request, response) {
                $.ajax({
                    url: "/Admin/PackageSetup/LoadAutocomplete",
                    //url: url,
                    type: "POST",
                    dataType: "json",
                    data: { type: flag, param: $(field).val() },
                    success: function (data) {
                        response($.map(data, function (item) {
                            debugger
                            return { label: item.text, value: item.text };
                        }))
                    }
                })
            }
        });
    })
}

function CalFromToday(cal) {
    $(function () {
        $(cal).datepicker({
            changeMonth: true,
            changeYear: true,
            showOn: "both",
            dateFormat: dateFormat,
            buttonImage: "",
            buttonImageOnly: true,
            yearRange: "-90:-18", //18 years or older up until 115yo (oldest person ever, can be sensibly set to something much smaller in most cases)
            maxDate: "+10Y", //Will only allow the selection of dates more than 18 years ago, useful if you need to restrict this
            minDate: "+0",

            onClose: function (x, y) {
                
            }
        });
    });
}
function CalFromYesterdayToToday(cal) {
    $(function () {
        $(cal).datepicker({
            changeMonth: true,
            changeYear: true,
            showOn: "both",
            dateFormat: dateFormat,
            buttonImage: "",
            buttonImageOnly: true,
            yearRange: "-90:-18", //18 years or older up until 115yo (oldest person ever, can be sensibly set to something much smaller in most cases)
            maxDate: "+0", //Will only allow the selection of dates more than 18 years ago, useful if you need to restrict this
            minDate: "-1"
        });
    });
}
function CalUpToToday(cal) {
    $(function () {
        $(cal).datepicker({
            changeMonth: true,
            changeYear: true,
            showOn: "both",
            dateFormat: dateFormat,
            buttonImage: "",
            buttonImageOnly: true,
           // yearRange: "-90:-18", //18 years or older up until 115yo (oldest person ever, can be sensibly set to something much smaller in most cases)
            maxDate: "+0", //Will only allow the selection of dates more than 18 years ago, useful if you need to restrict this
            minDate: "-5Y"

        });
    });
}
function CalDOB(cal,nextFocus) {
    $(function () {
        $(cal).datepicker({
            changeMonth: true,
            changeYear: true,
            dateFormat: dateFormat,
            showOn: "both",
            buttonImage: "",
            buttonImageOnly: true,
            yearRange: "-90:-16", //18 years or older up until 115yo (oldest person ever, can be sensibly set to something much smaller in most cases)
            maxDate: "-16Y", //Will only allow the selection of dates more than 18 years ago, useful if you need to restrict this
            minDate: "-90Y",
            onClose: function () {
                if (nextFocus != undefined) {
                    $("#" + nextFocus).focus();
                }
                CallBackJS(cal);
            }
        });
    });
}
function ShowCalFromTo(calFrom, calTo, nom) {
    if (nom == null || nom == "" || nom == undefined) nom = 1;
    
    $(function () {
        $(calFrom).datepicker({
            changeMonth: true,
            changeYear: true,
            showOn: "both",
            dateFormat: dateFormat,
            buttonImage: "",
            buttonImageOnly: true,
            onSelect: function (selectedDate) {
                $(calTo).datepicker("option", "minDate", selectedDate);
                $(calTo).focus();

            }
        });

        $(calTo).datepicker({
            changeMonth: true,
            changeYear: true,
            showOn: "both",
            dateFormat: dateFormat,
            buttonImage: "",
            buttonImageOnly: true,
            onSelect: function (selectedDate) {
                $(calFrom).datepicker("option", "maxDate", selectedDate);
            }
        });
    });
}

function ShowCalNepaliDate(cal) {
    $(function () {
        $(cal).datepicker({
            changeMonth: true,
            changeYear: true,
            showOn: "both",
            dateFormat: dateFormat,
            buttonImage: "",
            buttonImageOnly: true,
            onClose: function () {
                this.focus();
            }
        });
    });
}