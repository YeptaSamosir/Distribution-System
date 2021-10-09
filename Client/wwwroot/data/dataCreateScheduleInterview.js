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


//form for candidate
$.ajax({
    url: "/admin/candidate/get",
    dataType: "json",
}).done((response) => {
    //var result = JSON.parse(response);
    console.log(response);
    text = '<option value=""></option>';
    $.each(response, function (key, val) {

        text += `<option value="${val.candidateId}"> ${val.name}</option> `;

    });

    $('#inputCandidate').html(text);

}).fail((result) => {
    console.log(result);
});


function getEmailCandidate() {

    var selectBox = document.getElementById("inputCandidate");
    var getIdCandidate = selectBox.options[selectBox.selectedIndex].value;

    $.ajax({
        url: `/admin/candidate/get/${getIdCandidate}`,
        dataType: "json",
    }).done((response) => {

        console.log(response);
        $('#inputCandidateEmail').val(response.email);

    }).fail((result) => {
        console.log(result);
    });
}


/* AUTOCOMPLETE */
function init_autocomplete() {

    $.ajax({
        url: "/admin/company/get",
    }).done((response) => {
        /*var countries = JSON.parse(response);*/
        var countries = response;
        if (typeof ($.fn.autocomplete) === 'undefined') { return; }
        console.log('init_autocomplete');

        const obj = {};
        for (let i = 0; i < response.length; i++) {
            obj[i] = response[i].name;
        }
        console.log(obj);
          
        var companysArray = $.map(obj, function (value, key) {
            return {
                value: value,
                data: key
            };
        });

        // initialize autocomplete with custom appendTo
        $('#inputCompany').autocomplete({
            lookup: companysArray
        });

    }).fail((result) => {
        console.log(result);
    });

};


function checkValidation(errorMsg, elementById, elementMsg) {
    if (errorMsg != undefined) {
        document.getElementById(`${elementById}`).className = "form-control is-invalid";
        $(`#${elementMsg}`).html(` ${errorMsg}`);
    } else {
        document.getElementById(`${elementById}`).className = "form-control is-valid";
    }
}



//create data
$("#form-create-shedule").submit(function (event) {

    /* stop form from submitting normally */
    event.preventDefault();
    //get datetime
    let current = new Date();
    let cDate = current.getFullYear() + '-' + (current.getMonth() + 1) + '-' + current.getDate();
    let cTime = current.getHours() + ":" + current.getMinutes() + ":" + current.getSeconds();
    let dateTime = cDate + ' ' + cTime;

    var data_input = new Object();
    data_input.CustomerName = $("#inputCustomerName").val();
    data_input.CustomerEmail = $("#inputCustomerEmail").val();
    data_input.CompanyName = $("#inputCompany").val();
    data_input.JobTitle = $("#inputJobTitle").val();
    data_input.CandidateId = $("#inputCandidate").val();
    data_input.CandidateEmail = $("#inputCandidateEmail").val();
    data_input.ScheduleFollowBy = $("#inputScheduleBy").val();
    data_input.Type = $('input[name="inputType"]:checked').val();
    data_input.Location = $("#inputLocation").val();
    data_input.CreatedAt = dateTime;
    data_input.UpdatedAt = dateTime;
    data_input.DateTimeOne = $("#inputDataTimeOne").val();
    data_input.DateTimeTwo = $("#inputDataTimeTwo").val();
    data_input.DateTimeThree = $("#inputDataTimeThree").val();

  

    console.log(data_input);

    $.ajax({
        url: '/admin/scheduleinterview/create',
        method: 'POST',
        dataType: 'json',
        contentType: 'application/x-www-form-urlencoded',
        data: data_input,
        success: function (response) {
            console.log(response);
            var obj = JSON.parse(response);

            console.log(obj);
            if (obj.errors != undefined) {
                checkValidation(obj.errors.CustomerName, "inputCustomerName", "messageCustomerName");
                checkValidation(obj.errors.CustomerEmail, "inputCustomerEmail", "messageCustomerEmail");
                checkValidation(obj.errors.CompanyName, "inputCompany", "messageCompany");
                checkValidation(obj.errors.JobTitle, "inputJobTitle", "messageJobTitle");
                checkValidation(obj.errors.CandidateId, "inputCandidate", "messageCandidate");
                checkValidation(obj.errors.CandidateEmail, "inputCandidateEmail", "messageCandidateEmail");
                checkValidation(obj.errors.ScheduleFollowBy, "inputScheduleBy", "messageScheduleBy");
                checkValidation(obj.errors.Type, "inputType", "messageType");
                checkValidation(obj.errors.Location, "inputLocation", "messageLocation");
                checkValidation(obj.errors.DateTimeOne, "inputDataTimeOne", "messageDataTimeOne");
                checkValidation(obj.errors.DateTimeTwo, "inputDataTimeTwo", "messageDataTimeTwo");
                checkValidation(obj.errors.DateTimeThree, "inputDataTimeThree", "messageDataTimeThree");
            } else {

                //sweet alert message success
                Swal.fire({
                    position: 'center',
                    icon: 'success',
                    title: `${obj.Message}`,
                    showConfirmButton: false,
                    timer: 1500
                })
            }
        },
        error: function (xhr, status, error) {
            var err = eval(xhr.responseJSON);

        }
    })
});
