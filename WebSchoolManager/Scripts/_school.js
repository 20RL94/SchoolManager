var actualformid;

function toDateString(date) {
    return new Date(parseInt(date.substr(6))).toLocaleDateString();
}

function loadForms() {
    $.getJSON("/home/getForms", function (data) {
        $.each(data, function (index, item) {
            $("select[id^=selectForms]")
                .append($('<option value="' + item.FormId + '"' + (item.FormId == actualformid ? ' selected' : '') + '>' + item.Name + '</option>'))
        });
    });
}

function showPupilForm(show, pupil) {
    $("#editPupil").dialog("open");
    $("#editPupil #id").val(pupil != null ? pupil.PupilId : 0);
    $("#editPupil #firstname").val(pupil != null ? pupil.FirstName : "");
    $("#editPupil #lastname").val(pupil != null ? pupil.LastName : "");
    $("#editPupil #matrikel").val(pupil != null ? pupil.MatrikelNo : "");
    $("#editPupil #birthday").val(pupil != null ? toDateString(pupil.Birthday) : "");
    $("#editPupil #sex").val(pupil != null ? pupil.Sex : "");
    $("#editPupil #selectFormsPupil").val(pupil != null ? pupil.FormId : actualformid);
   
}

function getPupil() {
    return {
        PupilId: $("#editPupil #id").val(),
        FirstName: $("#editPupil #firstname").val(),
        LastName: $("#editPupil #lastname").val(),
        MatrikelNo: $("#editPupil #matrikel").val(),
        Birthday: $("#editPupil #birthday").val(),
        Sex: $("#editPupil #sex").val(),
        FormId: $("#editPupil #selectFormsPupil").val()
    };
}

function loadPupilTable() {
    $("#tablePupils tbody").empty();
    $.getJSON("/home/getPupilsByForm?formid=" + actualformid, function (data) {
        $.each(data, function (index, item) {
            $('<tr><td>' + item.LastName + '</td><td>' + item.FirstName + '</td><td>' + toDateString(item.Birthday) + '</td><td></td></tr>')
                .appendTo("#tablePupils tbody")
                .css("cursor", "pointer")
                .click(function () {
                    showPupilForm(true, item);
                });
        });
    });
}

$(function () {

    $("#tabs").tabs();
    $("#editPupil").dialog({
        autoOpen: false,
        modal:true,
        width: 600,
        title: "Edit Pupil Information",
        buttons: [
            {
                text:"Save",
                click: function () {
                    $.post("/home/savepupil", getPupil());
                }
            },
            {
                text: "Close",
                click: function () {
                    $(this).dialog("close");
                }
            }
        ]
    });

    $("#birthday").datepicker($.datepicker.regional["de"]);
    $("#search").autocomplete({
        source:function(request,response)
        {
            $.get("/home/GetPupilsByPattern?pattern=" + $("#search").val(), function (data) {
                response($.map(data, function (item) {
                    return { label: item.FirstName + " " + item.LastName, value: item.PupilId };
                }));
            });
        },
        minLength: 2,
        select: function (event, ui) {
            // label(firstname+lastname) will be displayed instead of id in searchbar
            $("#search").val(ui.item.label);
            // prevent the first event of searchbar click-> label:name -> value:id will be put on searchbar
            event.preventDefault();
            $.get("/home/getPupilById", { pupilId: ui.item.value }, function (data) {
                //pop up dialog
                showPupilForm(true, data);
            });
        }
    });

    loadForms();

    $("#selectForms").change(function () {
        actualformid = $(this).val();
        loadPupilTable();
    });

    $("#buttonNew").click(function () {
        showPupilForm(true);
    });

    
});