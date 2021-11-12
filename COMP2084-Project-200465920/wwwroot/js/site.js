// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function lockOptions() {
    var x = document.getElementById("mediaType").value;
    if (x == 'Movie') {
        document.getElementById('season').disabled = true;
        document.getElementById('episode').disabled = true;
        document.getElementById('season').required = false;
        document.getElementById('episode').required = false;
        document.getElementById('season').value = "";
        document.getElementById('episode').value = "";

    }
    else {
        document.getElementById('season').disabled = false;
        document.getElementById('episode').disabled = false;
        document.getElementById('season').required = true;
        document.getElementById('episode').required = true;
    }
}

function confirmDelete() {
    return confirm("Are you sure?");
}

