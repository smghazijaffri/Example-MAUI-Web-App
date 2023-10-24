// This is a JavaScript module that is loaded on demand. It can export any number of
// functions, and may import other JavaScript modules if required.

export function showPrompt(message) {
    return prompt(message, 'Type anything here');
}

$(document).ready(function () {
    (function ($) {
        "use strict";

        // Spinner
        var spinner = function () {
            setTimeout(function () {
                if ($("#spinner").length > 0) {
                    $("#spinner").removeClass("show");
                }
            }, 1);
        };
        spinner();

        // Sticky Navbar
        $(window).scroll(function () {
            if ($(this).scrollTop() > 40) {
                $(".navbar").addClass("sticky-top");
            } else {
                $(".navbar").removeClass("sticky-top");
            }
        });
    }
});

// wwwroot/js/custom.js

function openLoginModal() {
    $('#loginModal').modal('show');
}

function closeLoginModal() {
    $('#loginModal').modal('hide');
}

