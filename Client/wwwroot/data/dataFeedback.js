
//create data
$("#form-feedback-customer").submit(function (event) {

    /* stop form from submitting normally */
    event.preventDefault();
    //get datetime
    let current = new Date();
    let cDate = current.getFullYear() + '-' + (current.getMonth() + 1) + '-' + current.getDate();
    let cTime = current.getHours() + ":" + current.getMinutes() + ":" + current.getSeconds();
    let dateTime = cDate + ' ' + cTime;

    var data_input = new Object();
    data_input.ScheduleInterviewId = $("#scheduleInterviewId").val();
    data_input.FeedbackMessage = $("#inputFeedback").val();
    data_input.UpdatedAt = dateTime;
    data_input.CreatedAt = dateTime;

    

    console.log(data_input);

    if ($("#inputFeedback").val() == '') {
        console.log("please insert");
        document.getElementById(`inputFeedback`).className="form-control is-invalid";
        //$(`#messageFeedback`).html(``);
    } else {
        document.getElementById(`inputFeedback`).className="form-control invalid";
        

        $.ajax({
            url: '/interview/feedback',
            method: 'POST',
            dataType: 'json',
            contentType: 'application/x-www-form-urlencoded',
            data: data_input,
            success: function (response) {

                var obj = JSON.parse(response);

                console.log(obj);

                //sweet alert message success
                Swal.fire({
                    position: 'center',
                    icon: 'success',
                    title: `${obj.message}`,
                    showConfirmButton: false,
                    timer: 1500
                })

                $('#inputFeedback').val('');

            },
            error: function (xhr, status, error) {
                var err = eval(xhr.responseJSON);
                console.log(err);
            }
        })
    }
   
});
