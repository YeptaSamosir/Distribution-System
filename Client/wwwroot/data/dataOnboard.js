function init_DataTables() {
    console.log("run_datatables");

    if (typeof $.fn.DataTable === "undefined") {
        return;
    }
    console.log("init_DataTables");

    var handleDataTableButtons = function () {
        if ($("#datatable-onboard").length) {
            var t = $("#datatable-onboard").DataTable({
                dom: "Blfrtip",
                buttons: [
                    {
                        extend: "csv",
                        exportOptions: {
                            columns: [0,1,2,3,4,5,6,7]
                        },
                        className: "btn-sm btn-outline-success",
                    },
                    {
                        extend: "excel",
                        exportOptions: {
                            columns: [0, 1, 2, 3, 4, 5, 6, 7]
                        },
                        className: "btn-sm btn-outline-success",
                    },
                    {
                        extend: "pdfHtml5",
                        exportOptions: {
                            columns: [0, 1, 2, 3, 4, 5, 6, 7]
                        },
                        className: "btn-sm btn-outline-success",
                    },
                ],
                responsive: true,
                order: [[1, 'asc']],
                columnDefs: [{
                    searchable: false,
                    orderable: false,
                    targets: [0, 6]
                }],
                ajax: {
                    url: "/admin/onboard/get",
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
                        data: "candidate.name",
                    },
                    {
                        data: "candidate.email",
                    },
                    {
                        data: "company.name",
                    },
                    {
                        data: "jobTitle",
                    },
                    {
                        data: "dateStart",
                    },
                    {
                        render: function (data, type, row, meta) {
                            dateEnd = row['dateEnd'];

                            if (dateEnd == "0001-01-01T00:00:00") {
                                return `-`;
                            } else {
                                return moment(dateEnd).format('ddd, DD MMMM YYYY HH:mm');
                                //return dateEnd;
                            }
                        },
                    },
                    {
                        data: "status.name",
                    },
                    {

                        render: function (data, type, row, meta) {
                            return `
                                <div class="float-right">
                                    <button type="button" class="btn btn-success btn-sm" data-target="#modalEdit" onclick="modalOnboard('${row["onboardId"]}')">
                                        Update
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


//modal onboard
modalOnboard = (id) => {
    $.ajax({
        url: `/admin/onboard/get/${id}`,
    }).done((result) => {
        console.log(result);
        document.getElementById('form-edit-onbaord').reset();

        //set value
        $('#inputOnboardId').val(`${result.onboardId}`);
        $('#inputStatusId').val(`${result.statusId}`);
        $('#candidateName').text(`${result.candidate.name}`);
        $('#candidateEmail').text(`${result.candidate.email}`);
        $('#companyName').text(`${result.company.name}`);
        $('#jobTitle').text(`${result.JobTitle}`);

    }).fail((result) => {
        console.log(result);
    });
}

//create onboard
$("#form-edit-onbaord").submit(function (event) {


    /* stop form from submitting normally */
    event.preventDefault();

    var dataInput = new Object();
    dataInput.OnboardId = $("#inputOnboardId").val();
    dataInput.DateEnd = $("#inputDateEnd").val();
    dataInput.StatusId = $("#inputStatusId").val();

    console.log(dataInput);

    if (dataInput.OnboardId == "") {
        document.getElementById(`inputDateEnd`).className = "form-control is-invalid";
        $(`#messageOnboardate`).html(`Onboard date cannot null`);
    } else {
        document.getElementById(`inputDateEnd`).className = "form-control";

       /* $.ajax({
            url: '/admin/Onboard/update',
            method: 'PUT',
            dataType: 'json',
            contentType: 'application/x-www-form-urlencoded',
            data: dataInput,
            success: function (response) {

                console.log(response);

                var obj = JSON.parse(response);

                console.log(obj);

                if (obj.errors != undefined) {
                    checkValidation(obj.errors.DateStart, "inputDateEnd", "messageOnboardate");
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
        })*/
    }
});

//delete 
deleteModalCandidate = (id) => {

    console.log(id);

    Swal.fire({
        title: 'Delete Data',
        text: `You will delete this data ?`,
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
                        `Data deleted successfully`,
                        'success'
                    )

                    //reload only datatable
                    $('#datatable-candidate').DataTable().ajax.reload();
                },
            })
        }
    })
}
