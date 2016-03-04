function loadForms() {
    $.getJSON("/home/getforms", function (data) {
        $.each(data, function (index, item) {
            $("#selectForms").append('<option value="' + item.FormId + '">' + item.Name + '</option>')
        });
    });
}


function toDateString(date) {
    new Date(parseInt(date.substr(6))).toLocaleDateString();
}

function loadPupils(formid) {
    $("#tablePupils tbody").empty();
    $.getJSON("/home/getpupils?formid=" + formid, function (data) {
        $.each(data, function (index, item) {
            $("<tr><td>" + item.Lastname +
                "</td><td>" + item.Firstname +
                "</td><td>" + item.Birthday +
                "</td><td>" + item.Sex +
                "</td></tr>")
            .appendTo($("#tablePupils tbody"))
            .css("cursor", "pointer")
            .click(function () {
                $("#tablePupils").css("display", "none")
                $("#editPupil").css("display", "")

                $("#lastname").val(item.Lastname)
                $("#firstname").val(item.Firstname)
                $("#birthday").val(item.Birthday)
                $("#sex").val(item.Sex)
                $("#matrikelno").val(item.MatrikelNo)
                $("#formid").val(item.FormId)
            });

        });
    });
}


$(function () {
    loadForms();
    $("#selectForms").change(function () {
        loadPupils($(this).val());
    });

    $("#backButton").click(function(){
        $("#editPupil").css("display", "none")
        $("#tablePupils").css("display", "")
    });
});