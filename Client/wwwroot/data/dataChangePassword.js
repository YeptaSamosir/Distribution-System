

function checkValidation(errorMsg, elementById, elementMsg) {
    if (errorMsg != undefined) {
        document.getElementById(`${elementById}`).className = "form-control is-invalid";
        $(`#${elementMsg}`).html(`${errorMsg}`);
    } else {
        document.getElementById(`${elementById}`).className = "form-control is-valid";
    }
}


//update
$("#form-update-change-password").submit(function (event) {

    /* stop form from submitting normally */
    event.preventDefault();

    //get datetime
    let current = new Date();
    let cDate = current.getFullYear() + '-' + (current.getMonth() + 1) + '-' + current.getDate();
    let cTime = current.getHours() + ":" + current.getMinutes() + ":" + current.getSeconds();
    let dateTime = cDate + ' ' + cTime;

    var data_input = new Object();
    data_input.Email = $("#accountEmail").val();
    data_input.CurrentPassword = $("#inputPassword").val();
    data_input.NewPassword = $("#inputNewPassword").val();
    data_input.ConfirmPassword = $("#inputConfirmPassword").val();
    data_input.UpdatedAt = dateTime;


    console.log(data_input);

    $.ajax({
        url: `/admin/setting/change-password`,
        method: 'PUT',
        dataType: 'json',
        contentType: 'application/x-www-form-urlencoded',
        data: data_input,
        success: function (response) {

            var obj = JSON.parse(response);

            console.log(obj);

            if (obj.errors != undefined) {
                checkValidation(obj.errors.CurrentPassword, "inputPassword", "messagePassword");
                checkValidation(obj.errors.NewPassword, "inputNewPassword", "messageNewPassword");
                checkValidation(obj.errors.ConfirmPassword, "inputConfirmPassword", "messageConfirmPassword");
            } else {


                //sweet alert message success
                Swal.fire({
                    position: 'center',
                    icon: 'success',
                    title: `${obj.message}`,
                    showConfirmButton: false,
                    timer: 1500
                });

                //reset validation form
                document.getElementById("inputPassword").className = "form-control";
                document.getElementById("inputNewPassword").className = "form-control";
                document.getElementById("inputConfirmPassword").className = "form-control";

                //reset form
                document.getElementById("form-update-change-password").reset();
            }

        },
        error: function (xhr, status, error) {
           // var err = eval(xhr.responseJSON);
          /*  console.log(xhr);
            console.log(status);
            console.log(error);*/
        }
    });
});
