$(function() {
    loadForms();

    $("#selectForms").change(function () {
        loadPupils($(this).val());
    });

    $("#backButton").click(function() {
        $("#tablePupils").css("display", "");
        $("#formPupil").css("display", "none");
        loadPupils($("#selectForms").val());
    });

    $("#saveButton").click(function () {
        $.post("home/savepupil", {
            Firstname: $("#firstname").val(),
            Lastname: $("#lastname").val(),
            Sex: $("#sex").val(),
            Birthday: $("#birthday").val(),
            MatrikelNo: $("#matrikelNo").val(),
            FormName: $("#form").val(),
            PupilId: $("#pupilId").val()
        }, function(data, status) {
            alert("Success");
        });
    });
});

function loadForms() {
    $.getJSON("/home/getforms", function (data) {
        $.each(data, function(index, item) {
            $("select").append('<option value="' + item.FormId + '">' + item.Name + '</option>');
        });
    });
}

function loadPupils(formId) {
    $.getJSON("/home/getpupils", { formId: formId }, function (data) {
        $("tbody").empty();
        $.each(data, function (index, item) {
            var imgScr = item.Sex === "m" ? '<img src="Images/man.png"></img>' : '<img src="Images/woman.png"></img>';
            $('<tr>' + '<input type="hidden" value="' + item.PupilId + '"/>' + '<td>' + item.Lastname + '</td><td>' + item.Firstname + '</td><td>' + aspDateToJsDate(item.Birthday).toLocaleDateString() + '</td><td>' + imgScr + '</td></tr>')
                .appendTo("tbody")
                .css("cursor", "pointer")
                .click(function() {
                    $("#tablePupils").css("display", "none");
                    $("#formPupil").css("display", "");

                    loadPupil($(this).children("input").val());
                });
        });
    });
}

function loadPupil(pupilId) {
    $.getJSON("/home/getpupilbyid", { pupilId: pupilId }, function(data) {
        $("#firstname").val(data[0].Firstname);
        $("#lastname").val(data[0].Lastname);
        $("#sex").val(data[0].Sex);
        $("#birthday").val(aspDateToJsDate(data[0].Birthday).toLocaleDateString());
        $("#matrikelNo").val(data[0].MatrikelNo);
        $("#form").val(data[0].Form);
        $("#pupilId").val(data[0].PupilId);
    });
}

function aspDateToJsDate(aspDate) {
    return new Date(parseInt(aspDate.replace("/Date(", "").replace(")/", ""), 10));
}