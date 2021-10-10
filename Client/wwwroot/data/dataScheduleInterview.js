function init_DataTables() {
    console.log("run_datatables");

    if (typeof $.fn.DataTable === "undefined") {
        return;
    }
    console.log("init_DataTables");

    var handleDataTableButtons = function () {
        if ($("#datatable-interview-schedule").length) {
            var t = $("#datatable-interview-schedule").DataTable({
                dom: "Blfrtip",
                buttons: [
                    {
                        extend: "csv",
                        exportOptions: {
                            columns: [0, 1, 2]
                        },
                        className: "btn-sm",
                    },
                    {
                        extend: "excel",
                        exportOptions: {
                            columns: [0, 1, 2]
                        },
                        className: "btn-sm",
                    },
                    {
                        extend: "pdfHtml5",
                        exportOptions: {
                            columns: [0, 1, 2]
                        },
                        className: "btn-sm",
                    },
                ],
                responsive: true,
                ajax: {
                    url: "/admin/scheduleinterview/get",
                    datatype: "json",
                    dataSrc: "",
                },
                columns: [
                    {
                        render: function (data, type, row, meta) {
                            return meta.row + meta.settings._iDisplayStart + 1;
                        }
                    },
                    {
                        render: function (data, type, row, meta) {
                            scheduleDate = row['startInterview'];

                            if (scheduleDate == null) {
                                return ``;
                            } else {
                                return scheduleDate;
                            }
                        },
                    },
                    {
                        data: "company.name",
                    },
                    {
                        data: "customerName",
                    },
                    {
                        data: "candidate.name",
                    },
                    {
                        data: "jobTitle",
                    },
                    {
                        data: "status.name",
                    },
                    {

                        render: function (data, type, row, meta) {

                            return `
                                <div class="float-right">
                                    <button type="button" class="btn btn-success btn-sm" data-toggle="modal" data-target="#modalEdit"  onclick="editModalScheduleInterview('${row["scheduleInterviewId"]}')">
                                        Edit
                                    </button>
                                    <button type="button" class="btn btn-danger btn-sm"  onclick="deleteModalScheduleInterview('${row["scheduleInterviewId"]}')">
                                        Delete
                                    </button>
                                </div>
                                `;
                        },
                    },
                ],
            });
            t.on("order.dt search.dt", function () {
                t.column(0, { search: "applied", order: "applied" })
                    .nodes()
                    .each(function (cell, i) {
                        cell.innerHTML = i + 1;
                    });
            }).draw();
        }
    };

    TableManageButtons = (function () {
        "use strict";
        return {
            init: function () {
                handleDataTableButtons();
            },
        };
    })();

    $("#datatable").dataTable();

    $("#datatable-keytable").DataTable({
        keys: true,
    });

    $("#datatable-responsive").DataTable();

    $("#datatable-scroller").DataTable({
        ajax: "js/datatables/json/scroller-demo.json",
        deferRender: true,
        scrollY: 380,
        scrollCollapse: true,
        scroller: true,
    });

    $("#datatable-fixed-header").DataTable({
        fixedHeader: true,
    });

    var $datatable = $("#datatable-checkbox");

    $datatable.dataTable({
        order: [[1, "asc"]],
        columnDefs: [{ orderable: false, targets: [0] }],
    });
    $datatable.on("draw.dt", function () {
        $("checkbox input").iCheck({
            checkboxClass: "icheckbox_flat-green",
        });
    });

    TableManageButtons.init();
}

function checkValidation(errorMsg, elementById, elementMsg) {
    if (errorMsg != undefined) {
        document.getElementById(`${elementById}`).className = "form-control is-invalid";
        $(`#${elementMsg}`).html(` ${errorMsg}`);
    } else {
        document.getElementById(`${elementById}`).className = "form-control is-valid";
    }
}

//create data
$("#form-create-schedule-interview").submit(function (event) {

    /* stop form from submitting normally */
    event.preventDefault();
    //get datetime
    let current = new Date();
    let cDate = current.getFullYear() + '-' + (current.getMonth() + 1) + '-' + current.getDate();
    let cTime = current.getHours() + ":" + current.getMinutes() + ":" + current.getSeconds();
    let dateTime = cDate + ' ' + cTime;

    var data_input = new Object();
    data_input.ScheduleInterviewId = $("#inputScheduleInterviewID").val();
    data_input.CandidateId = $("#inputCandidateID").val();
    data_input.CompanyId = $("#inputCompanyID").val();
    data_input.StatusId = $("#statusID").val();
    data_input.CustomerName = $("#inputCustomerName").val();
    data_input.JobTitle = $("#inputJobTitle").val();
    data_input.Location = $("#inputLocationInterview").val();
    data_input.StartInterview = $("#inputStartDate").val();
    data_input.EndInterview = $("#inputEndDate").val();
    data_input.UpdatedAt = dateTime;
    data_input.CreatedAt = dateTime;

    console.log(data_input);

    $.ajax({
        url: '/admin/scheduleinterview/post',
        method: 'POST',
        dataType: 'json',
        contentType: 'application/x-www-form-urlencoded',
        data: data_input,
        success: function (response) {
            console.log(response);
            var obj = JSON.parse(response);

            console.log(obj);
            if (obj.errors != undefined) {
                checkValidation(obj.errors.ScheduleInterviewId, "inputScheduleInterviewID", "messageScheduleInterviewID");
                checkValidation(obj.errors.CandidateId, "inputCandidateID", "messageScheduleInterviewID");
                checkValidation(obj.errors.CompanyId, "inputCompanyID", "messageScheduleInterviewID");
                checkValidation(obj.errors.StatusId, "inputStatusID", "messageStatusID");
                checkValidation(obj.errors.CustomerName, "inputCustomerName", "messageCustomerName");
                checkValidation(obj.errors.JobTitle, "inputJobTitle", "messageJobTitle");
                checkValidation(obj.errors.Location, "inputLocationInterview", "messageLocationInterview");
                checkValidation(obj.errors.StartInterview, "inputStartDate", "messageStartDate");
                checkValidation(obj.errors.EndInterview, "inputEndDate", "messageEndDate");

            } else {
                $('#modalCandidate').modal('hide');



                //sweet alert message success
                Swal.fire({
                    position: 'center',
                    icon: 'success',
                    title: `${response}`,
                    showConfirmButton: false,
                    timer: 1500
                })

                //reload only datatable
                $('#datatable-candidate').DataTable().ajax.reload();
            }


        },
        error: function (xhr, status, error) {
            var err = eval(xhr.responseJSON);

        }
    })
});

//Edit
editModalScheduleInterview = (id) => {
    $.ajax({
        url: `/admin/scheduleinterview/get/${id}`,
    }).done((result) => {
        console.log(result);

        //set value
        $('#scheduleInterviewID').val(`${result.scheduleInterviewId}`);
        $('#candidateID').val(`${result.candidateId}`);
        $('#companyID').val(`${result.companyId}`);
        $('#statusID').val(`${result.statusId}`);
        $('#customerName').val(`${result.customerName}`);
        $('#jobTitle').val(`${result.jobTitle}`);
        $('#locationInterview').val(`${result.location}`);
        $('#startDate').val(`${result.startInterview}`);
        $('#endDate').val(`${result.endInterview}`);
        

    }).fail((result) => {
        console.log(result);
    });
}

//update
$("#form-edit-schedule-interview").submit(function (event) {


    /* stop form from submitting normally */
    event.preventDefault();
    //get datetime
    let current = new Date();
    let cDate = current.getFullYear() + '-' + (current.getMonth() + 1) + '-' + current.getDate();
    let cTime = current.getHours() + ":" + current.getMinutes() + ":" + current.getSeconds();
    let dateTime = cDate + ' ' + cTime;

    var data_input = {
        "ScheduleInterviewId": $("#scheduleInterviewID").val(),
        "CandidateId": $("#candidateID").val(),
        "CompanyId": $("#companyID").val(),
        "StatusId": $("#statusID").val(),
        "CustomerName": $("#customerName").val(),
        "JobTitle": $("#jobTitle").val(),
        "Location": $("#locationInterview").val(),
        "StartInterview": $("#startDate").val(),
        "EndInterview": $("#endDate").val(),
        "UpdatedAt": dateTime
    }

    console.log(JSON.stringify(data_input));

    $.ajax({
        url: `/admin/scheduleinterview/update`,
        method: 'PUT',
        dataType: 'json',
        contentType: 'application/x-www-form-urlencoded',
        data: data_input,
        success: function (response) {

            console.log(response);

            //idmodal di hide
            $('#modalEdit').hide();
            $('.modal-backdrop').remove();


            //sweet alert message success
            Swal.fire({
                position: 'center',
                icon: 'success',
                title: `${response}`,
                showConfirmButton: false,
                timer: 1500
            })

            //reload only datatable
            $('#datatable-candidate').DataTable().ajax.reload();


        },
        error: function (xhr, status, error) {
            var err = eval(xhr.responseJSON);
        }
    });
});

//delete 
deleteModalScheduleInterview = (id) => {

    console.log(id);

    Swal.fire({
        title: 'Hapus Data',
        text: `Anda akan menghapus data !`,
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete!'
    }).then((isDelete) => {
        if (isDelete.isConfirmed) {

            $.ajax({
                url: `/admin/scheduleinterview/delete/${id}`,
                method: 'DELETE',
                contentType: 'application/x-www-form-urlencoded',
                success: function (response) {
                    console.log(response);

                    Swal.fire(
                        'Deleted!',
                        'Data berhasil dihapus.',
                        'success'
                    )

                    //reload only datatable
                    $('#datatable-candidate').DataTable().ajax.reload();
                },
            })
        }
    })
}
