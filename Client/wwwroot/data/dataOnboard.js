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
                    targets: [0, 8]
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
                        render: function (data, type, row, meta) {
                            dateStart = row['dateStart'];

                            if (dateStart == "0001-01-01T00:00:00") {
                                return `-`;
                            } else {
                                return moment(dateStart).format('ddd, DD MMMM YYYY');
                                //return scheduleDate;
                            }
                        },
                    },
                    {
                        render: function (data, type, row, meta) {
                            dateEnd = row['dateEnd'];

                            if (dateEnd == "0001-01-01T00:00:00") {
                                return `-`;
                            } else {
                                return moment(dateEnd).format('ddd, DD MMMM YYYY');
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
                                <div class="float-right w-100">
                                  <button type="button" class="btn btn-success btn-sm" data-toggle="modal" data-target="#modalEdit"  onclick="editModalOnboard('${row["onboardId"]}')">
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


//Edit
editModalOnboard = (id) => {
    $.ajax({
        url: `/admin/onboard/get/${id}`,
    }).done((result) => {
        //console.log(result);
        console.log(result.status.name);

        //set value
        $('#inputOnboardId').val(`${result.onboardId}`);
        $('#inputStatusId').val(`${result.statusId}`);
        $('#inputCandidateId').val(`${result.candidateId}`);
        $('#inputCompanyId').val(`${result.companyId}`);
        $('#inputDateStart').val(`${result.dateStart}`);

        $('#candidateName').text(`${result.candidate.name}`);
        $('#candidateEmail').text(`${result.candidate.email}`);
        $('#companyName').text(`${result.company.name}`);
        $('#jobTitle').text(`${result.jobTitle}`);
         var dateStart = moment(result.dateStart).format('ddd, DD MMMM YYYY');
        $('#dateStart').text(`${dateStart}`);

        if (result.statusId == "ONB-OG") {
            document.querySelector('#inputStatus').options[1].selected = true;
            $("#dateEndForm").hide();
        } else {
            document.querySelector('#inputStatus').options[0].selected = true;
        }

    }).fail((result) => {
        console.log(result);
    });
}

function showDateEnd() {

    //get datetime
    let current = new Date();
    $('#DateNow').text(moment(current).format('ddd, DD MMMM YYYY'));

    if (document.querySelector('#inputStatus').options[0].selected == true) {
        $("#dateEndForm").show();
    } else {
        $("#dateEndForm").hide();
    }
}

//create onboard
$("#form-edit-onbaord").submit(function (event) {


    /* stop form from submitting normally */
    event.preventDefault();

    var dataInput = new Object();
    dataInput.OnboardId = $("#inputOnboardId").val();
    dataInput.StatusId = $("#inputStatus").val();
    dataInput.CandidateId = $('#inputCandidateId').val();
    dataInput.CompanyId = $('#inputCompanyId').val();
    dataInput.DateStart = $('#inputDateStart').val();

    console.log(dataInput);


    $.ajax({
        url: `/admin/onboard/update`,
        method: 'POST',
        dataType: 'json',
        contentType: 'application/x-www-form-urlencoded',
        data: dataInput,
        success: function (response) {

            console.log(response);

            var obj = JSON.parse(response);

            console.log(obj);

            if (obj.errors == undefined) {
                //idmodal di hide
                document.getElementById("modalEdit").className = "modal fade";
                $('.modal-backdrop').remove();

                Swal.fire({
                    position: 'center',
                    icon: 'success',
                    title: `${obj.message}`,
                    showConfirmButton: false,
                    timer: 1500
                })

                //reload only datatable
                $('#datatable-onboard').DataTable().ajax.reload();
            }
        },
        error: function (xhr, status, error) {
            var err = eval(xhr.responseJSON);
            console.log(err);
        }
    })
  
});
