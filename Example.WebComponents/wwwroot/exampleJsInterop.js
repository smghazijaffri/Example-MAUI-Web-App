// This is a JavaScript module that is loaded on demand. It can export any number of
// functions, and may import other JavaScript modules if required.

export function showPrompt(message) {
    return prompt(message, 'Type anything here');
}

// wwwroot/js/custom.js

function openLoginModal() {
    $('#loginModal').modal('show');
}

function closeLoginModal() {
    $('#loginModal').modal('hide');
}

window.runningFormatter = function (value, row, index) {
    return index + 1;
};
