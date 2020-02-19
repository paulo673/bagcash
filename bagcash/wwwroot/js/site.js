// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).ready(function () {
    $('.table-click>tbody tr').click(function (e) {
        e.preventDefault();

        var url = $(this).find('td a').attr('href');
        var target = $(this).find('td a').attr('target');

        if (url !== undefined) {
            if (target === "_blank") {
                window.open(url, '_blank');
            } else {
                window.location.href = url;
            }
        }
    });
});