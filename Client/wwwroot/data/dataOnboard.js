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
                            columns: [1,2,3,4,5]
                        },
                        className: "btn-sm",
                    },
                    {
                        extend: "excel",
                        exportOptions: {
                            columns: [1,2,3,4,5]
                        },
                        className: "btn-sm",
                    },
                    {
                        extend: "pdfHtml5",
                        exportOptions: {
                            columns: [1,2,3,4,5]
                        },
                        className: "btn-sm",
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
                        data: null,
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
                                return dateEnd;
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
                                    <a href="onboard/detail/${row["onboardId"]}"><button type="button" class="btn btn-success btn-sm" data-target="#detail-onboard" onclick="detailOnboard('${row["onboardId"]}')">
                                        Detail
                                    </button></a>
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


//Edit
$(document).ready(function () {
    $('#id_batch').val();
    var id = $('#id_batch').val();
    $.ajax({
        url: `/admin/onboard/get/` + id,
    }).done((result) => {
        console.log(result);

        //set value
        $('#id_batch').val(`${result.onboardId}`);
        $('#nameCandidate').val(`${result.candidate.name}`);
        $('#emailCandidate').val(`${result.candidate.email}`);
        $('#startDate').val(`${result.dateStart}`);
        $('#endDate').val(`${result.dateEnd}`);
        $('#statusCandidate').val(`${result.candidate.status}`);


    }).fail((result) => {
        console.log(result);
    });
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
