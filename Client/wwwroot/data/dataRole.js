function init_DataTables() {
    console.log("run_datatables");

    if (typeof $.fn.DataTable === "undefined") {
        return;
    }
    console.log("init_DataTables");

    var handleDataTableButtons = function () {
        if ($("#datatable-role").length) {
            var t = $("#datatable-role").DataTable({
                dom: "Blfrtip",
                buttons: [
                    {
                        extend: "csv",
                        className: "btn-sm",
                    },
                    {
                        extend: "excel",
                        className: "btn-sm",
                    },
                    {
                        extend: "pdfHtml5",
                        className: "btn-sm",
                    },
                ],
                responsive: true,
                ajax: {
                    url: "/admin/role/get",
                    datatype: "json",
                    dataSrc: "",
                },
                columns: [
                    {
                        data: null,
                    },
                    {
                        data: "name",
                    },
                    {
                        render: function (data, type, row, meta) {

                            return `
                                <div class="float-right">
                                    <button type="button" class="btn btn-success btn-sm" data-toggle="modal" data-target="#modalEdit"  onclick="editModalRole('${row["roleId"]}')">
                                        Edit
                                    </button>
                                    <button type="button" class="btn btn-danger btn-sm"  onclick="deleteModalRole('${row["roleId"]}')">
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
$("#form-create-role").submit(function (event) {

    /* stop form from submitting normally */
    event.preventDefault();
    //get datetime
    let current = new Date();
    let cDate = current.getFullYear() + '-' + (current.getMonth() + 1) + '-' + current.getDate();
    let cTime = current.getHours() + ":" + current.getMinutes() + ":" + current.getSeconds();
    let dateTime = cDate + ' ' + cTime;

    var data_input = new Object();
    data_input.RoleId = $("#inputRoleId").val();
    data_input.Name = $("#inputRoleName").val();
    data_input.UpdatedAt = dateTime;
    data_input.CreatedAt = dateTime;

   
    console.log(data_input);

    $.ajax({
        url: '/admin/role/post',
        method: 'POST',
        dataType: 'json',
        contentType: 'application/x-www-form-urlencoded',
        data: data_input,
        success: function (response) {
               
            var obj = JSON.parse(response);

            console.log(obj);

            if (obj.errors != undefined) {
                checkValidation(obj.errors.RoleId, "inputRoleId", "messageRoleId");
                checkValidation(obj.errors.Name, "inputRoleName", "messageRoleName");

            } else {

                //idmodal di hide
                document.getElementById("modalCreate").className = "modal fade";
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
                $('#datatable-role').DataTable().ajax.reload();
            }
            
        },
        error: function (xhr, status, error) {
            var err = eval(xhr.responseJSON);
            console.log(err);
        }
    })
});

//Edit
editModalRole = (id) => {
    $.ajax({
        url: `/admin/role/get/${id}`,
    }).done((result) => {
        //console.log(result);

        //set value
        $('#inputRoleIdEdit').val(`${result.roleId}`);
        $('#inputRoleNameEdit').val(`${result.name}`);
       
    }).fail((result) => {
        console.log(result);
    });
}

//update
$("#form-edit-role").submit(function (event) {


    /* stop form from submitting normally */
    event.preventDefault();
    //get datetime
    let current = new Date();
    let cDate = current.getFullYear() + '-' + (current.getMonth() + 1) + '-' + current.getDate();
    let cTime = current.getHours() + ":" + current.getMinutes() + ":" + current.getSeconds();
    let dateTime = cDate + ' ' + cTime;

    var data_input = {
        "RoleId": $("#inputRoleIdEdit").val(),
        "Name": $("#inputRoleNameEdit").val(),
        "UpdatedAt": dateTime
    }

    console.log(JSON.stringify(data_input));

    $.ajax({
        url: `/admin/role/update`,
        method: 'PUT',
        dataType: 'json',
        contentType: 'application/x-www-form-urlencoded',
        data: data_input,
        success: function (response) {
                    
            var obj = JSON.parse(response);

            console.log(obj);

            if (obj.errors != undefined) {
                checkValidation(obj.errors.Name, "inputRoleNameEdit", "messageRoleNameEdit");
            } else {

                //idmodal di hide
                document.getElementById("modalEdit").className = "modal fade";
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
                $('#datatable-role').DataTable().ajax.reload();
            }
            
        },
        error: function (xhr, status, error) {
            var err = eval(xhr.responseJSON);
        }
    });
});

//delete 
deleteModalRole = (id) => {

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
                url: `/admin/role/delete/${id}`,
                method: 'DELETE',
                contentType: 'application/x-www-form-urlencoded',
                success: function (response) {

                    Swal.fire(
                        'Deleted!',
                        `Data berhasil di hapus`,
                        'success'
                    )

                    //reload only datatable
                    $('#datatable-role').DataTable().ajax.reload();
                },
            })
        }
    })
}
