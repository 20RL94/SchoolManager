function loadForms() {
    $.getJSON("/home/getforms", function (data) {
        $.each(data, function (index, item) {
            $("#selectForms").append('<option value="' + item.FormId + '">' + item.Name + '</option>')
        });
    });
}


function loadPupils(formid) {
    $.getJSON("/home/getpupils?formid="+formid, function (data) {
        $.each(data, function (index, item) {
            $("<tr><th>" + item.Lastname +
                "</th><th>" + item.Firstname +
                "</th><th>" + item.Birthday +
                "</th><th>" + item.Sex +
                "</th></tr>").appendTo($("table tbody"))
        });
    })
}


$(function () {
    loadForms();
    $("#selectForms").change(function () {
        loadPupils($(this).val());
    });
});