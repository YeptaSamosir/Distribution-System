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
                            columns: [0, 1, 2, 3, 4, 5, 6]
                        },
                        className: "btn-sm btn-outline-success",
                    },
                    {
                        extend: "excel",
                        exportOptions: {
                            columns: [0, 1, 2, 3, 4, 5, 6]
                        },
                        className: "btn-sm btn-outline-success",
                    },
                    {
                        extend: "pdfHtml5",
                        exportOptions: {
                            columns: [0, 1, 2, 3, 4, 5, 6]
                        },
                        className: "btn-sm btn-outline-success",
                    },
                ],
                responsive: true,
                order: [[1, 'asc']],
                columnDefs: [{
                    searchable: false,
                    orderable: false,
                    targets: [0, 7]
                }],
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
                        data: "customerName",
                    },
                    {
                        data: "candidate.name",
                    },
                    {
                        data: "company.name",
                    },
                    {
                        data: "jobTitle",
                    },
                    {
                        render: function (data, type, row, meta) {
                            scheduleDate = row['startInterview'];

                            if (scheduleDate == "0001-01-01T00:00:00") {
                                return `-`;
                            } else {
                                return moment(scheduleDate).format('ddd, DD MMMM YYYY HH:mm');
                                //return scheduleDate;
                            }
                        },
                    },
                    {
                        data: "status.name",
                        render: function (data, type, row, meta) {
                            var user = (row['followingBy'] == 'candidate')? 'customer' : 'candidate';

                            if (row['statusId'] == "ITV-WD") {
                                return data + " from " + row['followingBy'];
                            }
                            if (row['statusId'] == "ITV-WC") {
                                return data + " from " + user;
                            }

                            return data;
                          
                        },
                    },
                    {

                        render: function (data, type, row, meta) {
                            if (row["statusId"] == "ITV-AC") {
                                return `
                                <div class="float-right">
                                    <button type="button" class="btn btn-success btn-sm" data-toggle="modal" data-target="#modalOnboard"  onclick="modalOnboard('${row["scheduleInterviewId"]}')">
                                        Create Onboard
                                    </button>
                                </div>
                                `;
                            }

                            if (row["statusId"] == "ITV-DN" || row["statusId"] == "ITV-CN"){
                                return `
                                <div class="float-right">
                                    <button type="button" class="btn btn-danger btn-sm"  onclick="deleteModalScheduleInterview('${row["scheduleInterviewId"]}')">
                                        Delete
                                    </button>
                                </div>
                                `;
                            }
                            
                            return `
                            <div class="float-right">
                                <button type="button" class="btn btn-danger btn-sm" disabled>
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

$('#inputOnboardate').daterangepicker({
    singleDatePicker: true,
    showDropdowns: true,
}, function (start, end, label) {
    console.log("A new date selection was made: " + start.format('YYYY-MM-DD'));
    $('#dateStart').val(`${start.format('YYYY-MM-DD')}`);
});

//modal onboard
modalOnboard = (id) => {
    $.ajax({
        url: `/admin/scheduleinterview/get/${id}`,
    }).done((result) => {
        //console.log(result);
        document.getElementById('form-create-onbaord').reset();

        //set value
        $('#inputCandidateId').val(`${result.candidateId}`);
        $('#inputCompanyId').val(`${result.companyId}`);
        $('#inputJobTitle').val(`${result.jobTitle}`);
        $('#candidateName').html(`${result.candidate.name}`);
        $('#candidateEmail').html(`${result.candidate.email}`);
        $('#companyName').html(`${result.company.name}`);
        $('#jobTitle').html(`${result.jobTitle}`);

    }).fail((result) => {
        console.log(result);
    });
}

//create onboard
$("#form-create-onbaord").submit(function (event) {


    /* stop form from submitting normally */
    event.preventDefault();

    var dataInput = new Object();
    dataInput.CandidateId = $("#inputCandidateId").val();
    dataInput.CompanyId = $("#inputCompanyId").val();
    dataInput.JobTitle = $("#inputJobTitle").val();
    dataInput.DateStart = $("#dateStart").val();
    dataInput.StatusId = "ONB-OG";
   
    console.log(dataInput);

    if (dataInput.DateStart == "" || dataInput.DateStart == "") {
        document.getElementById(`inputOnboardate`).className = "form-control is-invalid";
        $(`#messageOnboardate`).html(`Onboard date cannot null`);
    } else {
        document.getElementById(`inputOnboardate`).className = "form-control";

        $.ajax({
            url: '/admin/Onboard/create',
            method: 'POST',
            dataType: 'json',
            contentType: 'application/x-www-form-urlencoded',
            data: dataInput,
            success: function (response) {

                console.log(response);

                var obj = JSON.parse(response);

                console.log(obj);

                if (obj.errors != undefined) {
                    checkValidation(obj.errors.DateStart, "inputOnboardate", "messageOnboardate");
                } else {

                    //idmodal di hide
                    document.getElementById("modalOnboard").className = "modal fade";
                    $('.modal-backdrop').remove();


                    //sweet alert message success
                    Swal.fire({
                        position: 'center',
                        icon: 'success',
                        title: `${obj.message}`,
                        showConfirmButton: false,
                        timer: 1500
                    })

                    
                }

            },
            error: function (xhr, status, error) {
                var err = eval(xhr.responseJSON);
                console.log(err);
            }
        })
    }
});

//delete 
deleteModalScheduleInterview = (id) => {

    console.log(id);

    Swal.fire({
        title: 'Are you sure?',
        text: `You won't be able to revert this!`,
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
                        'Your file has been deleted.',
                        'success'
                    )

                    //reload only datatable
                    $('#datatable-candidate').DataTable().ajax.reload();
                },
            })
        }
    })
}
