
//update
$("#form-reset-password").submit(function(event) {

    /* stop form from submitting normally */
    event.preventDefault();

    if ($("#inputNewPassword").val() == "") {
        document.getElementById("message").innerHTML = "New Password cannot null";
    } else {
        var regex = /^(?=[^a-z]*[a-z])(?=[^A-Z]*[A-Z])(?=\D*\d)[^:&.~\s]{5,20}$/;
        if (!regex.test($("#inputNewPassword").val())) {
            document.getElementById("message").innerHTML = "Must contain uppercase, lowercase letters and numbers";
        } else {
            document.getElementById("message").innerHTML = "";

            var data_input = new Object();
            data_input.EmailAccount = $("#inputEmail").val();
            data_input.NewPassword = $("#inputNewPassword").val();

            console.log(JSON.stringify(data_input));

            $.ajax({
                url: `/Auth/reset-password-account`,
                method: 'POST',
                dataType: 'json',
                contentType: 'application/x-www-form-urlencoded',
                data: data_input,
                success: function (response) {
                  
                    document.getElementById("message").innerHTML = response;

                    window.location.href = "/auth/login";
                }
            });
         }
    }
});
