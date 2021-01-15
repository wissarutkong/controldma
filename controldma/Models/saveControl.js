$(document).ready(function () {

    $("#save_add_valva").on("click", function (e) {
        e.preventDefault();

        $(this).prev().click();

    });

})
