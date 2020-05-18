$(function () {
    // Sidebar toggle behavior
    $('#sidebarCollapse').on('click', function () {
        $('#sidebar, #content').toggleClass('active');
    });
});

$(document).on("click", ".browse", function () {
    var file = $(this).parents().find(".file");
    file.trigger("click");
});

$(document).on('input[type="file"]').change(function (e) {
    var fileName = e.target.files[0].name;
    $("#file").val(fileName);
    var reader = new FileReader();
    reader.onload = function (a) {
        // get loaded data and render thumbnail.
        document.getElementById("preview").src = a.target.result;
    };
    // read the image file as a data URL.
    reader.readAsDataURL(e.target.files[0]);
});
