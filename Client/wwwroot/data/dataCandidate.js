﻿function init_DataTables() {
    console.log("run_datatables");

    if (typeof $.fn.DataTable === "undefined") {
        return;
    }
    console.log("init_DataTables");

    var handleDataTableButtons = function () {
        if ($("#datatable-candidate").length) {
            var t = $("#datatable-candidate").DataTable({
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
                    url: "/admin/candidate/get",
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
                        data: "name",
                    },
                    {
                        data: "grade",
                    },
                    {
                        render: function (data, type, row, meta) {

                            return `
                                <div class="float-right">
                                    <button type="button" class="btn btn-success btn-sm" data-toggle="modal" data-target="#modalEdit"  onclick="editModalCandidate('${row["candidateId"]}')">
                                        Edit
                                    </button>
                                    <button type="button" class="btn btn-danger btn-sm"  onclick="deleteModalCandidate('${row["candidateId"]}')">
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
$("#form-create-candidate").submit(function (event) {

    /* stop form from submitting normally */
    event.preventDefault();
    //get datetime
    let current = new Date();
    let cDate = current.getFullYear() + '-' + (current.getMonth() + 1) + '-' + current.getDate();
    let cTime = current.getHours() + ":" + current.getMinutes() + ":" + current.getSeconds();
    let dateTime = cDate + ' ' + cTime;

    var data_input = new Object();
    data_input.Name = $("#inputNamaLengkap").val();
    data_input.Grade = $("#inputGrade").val();
    data_input.UpdatedAt = dateTime;
    data_input.CreatedAt = dateTime;

    console.log(data_input);

    $.ajax({
        url: '/admin/candidate/post',
        method: 'POST',
        dataType: 'json',
        contentType: 'application/x-www-form-urlencoded',
        data: data_input,
        success: function (response) {
            console.log(response);
            var obj = JSON.parse(response);

            console.log(obj);
            if (obj.errors != undefined) {
                checkValidation(obj.errors.Name, "inputNamaLengkap", "messageName");
                checkValidation(obj.errors.Grade, "inputGrade", "messageGrade");

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
editModalCandidate = (id) => {
    $.ajax({
        url: `/admin/candidate/get/${id}`,
    }).done((result) => {
        //console.log(result);

        //set value
        $('#candidateId').val(`${result.candidateId}`);
        $('#candidateName').val(`${result.name}`);
        $('#grade').val(`${result.grade}`);
        

    }).fail((result) => {
        console.log(result);
    });
}

//update
$("#form-edit-candidate").submit(function (event) {


    /* stop form from submitting normally */
    event.preventDefault();
    //get datetime
    let current = new Date();
    let cDate = current.getFullYear() + '-' + (current.getMonth() + 1) + '-' + current.getDate();
    let cTime = current.getHours() + ":" + current.getMinutes() + ":" + current.getSeconds();
    let dateTime = cDate + ' ' + cTime;

    var data_input = {
        "CandidateId": $("#candidateId").val(),
        "Name": $("#candidateName").val(),
        "Grade": $("#grade").val(),
        "UpdatedAt": dateTime
    }

    console.log(JSON.stringify(data_input));

    $.ajax({
        url: `/admin/candidate/update`,
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
deleteModalCandidate = (id) => {

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
                url: `/admin/candidate/delete/${id}`,
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