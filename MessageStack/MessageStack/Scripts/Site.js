$(document).ready(function () {

});


//Toggle password visibility
function togglePasswordVisibility() {
    var field = document.getElementById("password-field");
    if (field.type === "password") {
        field.type = "text";
    } else {
        field.type = "password";
    }
}

//Scroll the Chat Box all the way down
$("#chat-box").scrollTop($("#chat-box")[0].scrollHeight);