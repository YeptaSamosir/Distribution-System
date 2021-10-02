/*Login*/

$("#form-login").submit(function (event) {

    /* stop form from submitting normally */
    event.preventDefault();


    var data_input = new Object();
    data_input.Username = $("#input-username").val();
    data_input.Password = $("#input-password").val();


    console.log(data_input);

    $.ajax({
        url: '/master/base/account/login',
        method: 'POST',
        dataType: 'json',
        contentType: 'application/x-www-form-urlencoded',
        data: data_input,
        success: function (data) {


            var obj = JSON.parse(data);

            console.log(obj);

            if (obj.errors != undefined) {

                document.getElementById("inputUniversity").className = "form-control is-invalid";
                $("#messageUniv").html(` ${obj.errors.Name}`);
            } else {
                //idmodal di hide
                $('#add').hide();
                $('.modal-backdrop').remove();


                //sweet alert message success
                Swal.fire({
                    position: 'center',
                    icon: 'success',
                    title: `${obj.message}`,
                    showConfirmButton: false,
                    timer: 1500
                })

                //reload only datatable
                $('#myTable').DataTable().ajax.reload();

            }
        },
        error: function (xhr, status, error) {
            var err = eval(xhr.responseJSON);

        }
    })
});