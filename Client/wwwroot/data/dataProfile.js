

function checkValidation(errorMsg, elementById, elementMsg) {
    if (errorMsg != undefined) {
        document.getElementById(`${elementById}`).className = "form-control is-invalid";
        $(`#${elementMsg}`).html(`${errorMsg}`);
    } else {
        document.getElementById(`${elementById}`).className = "form-control is-valid";
    }
}


//update
$("#form-edit-account-profile").submit(function (event) {

    /* stop form from submitting normally */
    event.preventDefault();

    //get datetime
    let current = new Date();
    let cDate = current.getFullYear() + '-' + (current.getMonth() + 1) + '-' + current.getDate();
    let cTime = current.getHours() + ":" + current.getMinutes() + ":" + current.getSeconds();
    let dateTime = cDate + ' ' + cTime;

    var data_input = new Object();
    data_input.AccountId = $("#accountId").val();
    data_input.Password = $("#accountPassword").val();
    data_input.Name = $("#inputName").val();
    data_input.Username = $("#inputUsername").val();
    data_input.Email = $("#inputEmail").val();
    data_input.IsActive = true;
    data_input.UpdatedAt = dateTime;

   
    console.log(JSON.stringify(data_input));

    $.ajax({
        url: `/admin/setting/account/update`,
        method: 'PUT',
        dataType: 'json',
        contentType: 'application/x-www-form-urlencoded',
        data: data_input,
        success: function (response) {

            var obj = JSON.parse(response);

            console.log(obj);

            if (obj.errors != undefined) {
                checkValidation(obj.errors.Name, "inputName", "messageName");
                checkValidation(obj.errors.Username, "inputUsername", "messageUsername");
                checkValidation(obj.errors.Email, "inputEmail", "messageEmail");
            } else {


                //sweet alert message success
                Swal.fire({
                    position: 'center',
                    icon: 'success',
                    title: `${obj.message}`,
                    showConfirmButton: false,
                    timer: 1500
                })

                //reset validation form
                document.getElementById("inputName").className = "form-control";
                document.getElementById("inputUsername").className = "form-control";
                document.getElementById("inputEmail").className = "form-control";

                               
            }

        },
        error: function (xhr, status, error) {
            var err = eval(xhr.responseJSON);
        }
    });
});
