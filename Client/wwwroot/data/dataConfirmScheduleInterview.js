$('#myDatepicker1').datetimepicker({
    format: 'yyyy-mm-dd hh:ii:ss',
    autoclose: true,
    todayBtn: true,
    minuteStep: 10
});

$('#myDatepicker2').datetimepicker({
    format: 'yyyy-mm-dd hh:ii:ss',
    autoclose: true,
    todayBtn: true,
    minuteStep: 10
});

$('#myDatepicker3').datetimepicker({
    format: 'yyyy-mm-dd hh:ii:ss',
    autoclose: true,
    todayBtn: true,
    minuteStep: 10
});


function checkValidation(errorMsg, elementById, elementMsg) {
    if (errorMsg != undefined) {
        document.getElementById(`${elementById}`).className = "form-control is-invalid";
        $(`#${elementMsg}`).html(` ${errorMsg}`);
    } else {
        document.getElementById(`${elementById}`).className = "form-control is-valid";
    }
}



//create data
$("#form-confirm-schedule").submit(function (event) {

    /* stop form from submitting normally */
    event.preventDefault();
    //get datetime
    let current = new Date();
    let cDate = current.getFullYear() + '-' + (current.getMonth() + 1) + '-' + current.getDate();
    let cTime = current.getHours() + ":" + current.getMinutes() + ":" + current.getSeconds();
    let dateTime = cDate + ' ' + cTime;

    var data_input = new Object();
    data_input.ScheduleInterviewId = $("#scheduleInterviewId").val();
    data_input.DateTimeOne = $("#inputDateTimeOne").val();
    data_input.DateTimeTwo = $("#inputDateTimeTwo").val();
    data_input.DateTimeThree = $("#inputDateTimeThree").val();
    data_input.ScheduleFollowBy = $("#scheduleFollowBy").val();

    console.log(data_input);

    $.ajax({
        url: '/interview/confirmation/create-date-option',
        method: 'POST',
        dataType: 'json',
        contentType: 'application/x-www-form-urlencoded',
        data: data_input,
        success: function (response) {
            console.log(response);
            var obj = JSON.parse(response);

            console.log(obj);
            if (obj.errors != undefined) {
                 checkValidation(obj.errors.DateTimeOne, "inputDateTimeOne", "messageDateTimeOne");
                 checkValidation(obj.errors.DateTimeTwo, "inputDateTimeTwo", "messageDateTimeTwo");
                 checkValidation(obj.errors.DateTimeThree, "inputDateTimeThree", "messageDateTimeThree");
            } else {

                //sweet alert message success
                Swal.fire({
                    position: 'center',
                    icon: 'success',
                    title: `${obj.message}`,
                    showConfirmButton: true,
                })

                var btn = document.getElementById('btn-sub');
                btn.disabled = true;
                btn.innerText = 'Done';

                //reset form
                document.getElementById('form-confirm-schedule').reset();
            }
        },
        error: function (xhr, status, error) {
            var err = eval(xhr.responseJSON);

        }
    })
});
