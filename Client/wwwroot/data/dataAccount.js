function init_DataTables() {
    console.log("run_datatables");

    if (typeof $.fn.DataTable === "undefined") {
        return;
    }
    console.log("init_DataTables");

    var handleDataTableButtons = function () {
        if ($("#datatable-account").length) {
            var t = $("#datatable-account").DataTable({
                dom: "Blfrtip",
                buttons: [
                    {
                        extend: "csv",
                        exportOptions: {
                            columns: [1,2,3,4]
                        },
                        className: "btn-sm",
                    },
                    {
                        extend: "excel",
                        exportOptions: {
                            columns: [1, 2, 3, 4]
                        },
                        className: "btn-sm",
                    },
                    {
                        extend: "pdfHtml5",
                        exportOptions: {
                            columns: [1, 2, 3, 4]
                        },
                        className: "btn-sm",
                    },
                ],
                responsive: true,
                order: [[1, 'asc']],
                columnDefs: [{
                    searchable: false,
                    orderable: false,
                    targets: [0, 5]
                }],
                ajax: {
                    url: "/admin/account/get",
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
                        data: "email",
                    },
                    {
                        data: "username",
                    },
                    {
                        render: function (data, type, row, meta) {
                            var b = '';

                            $.each(row['accountRoles'], function (key, val) {
                                b += `<span class="m-1 p-2 badge badge-info">${val.role.name}</span>`;
                            });

                            return b;
                        },
                    },
                    {
                        render: function (data, type, row, meta) {
                            var a = `
                            <div class="float-right">
                                <button type="button" class="btn btn-success btn-sm" data-toggle="modal" data-target="#modalEdit"  onclick="editModalRole('${row["accountId"]}')">
                                    Edit
                                </button>
                      
                                <button type="button" class="btn btn-danger btn-sm"  onclick="deleteModalAccount('${row["accountId"]}')">
                                        Delete
                                </button>
                            </div>
                            `;
                            return a;
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




/*============ */
function hideshow() {
    var password = document.getElementById("inputPassword");
    var slash = document.getElementById("slash");
    var eye = document.getElementById("eye");

    if (password.type === 'password') {
        password.type = "text";
        slash.style.display = "block";
        eye.style.display = "none";
    }
    else {
        password.type = "password";
        slash.style.display = "none";
        eye.style.display = "block";
    }

}

function checkValidation(errorMsg, elementById, elementMsg) {
    if (errorMsg != undefined) {
        document.getElementById(`${elementById}`).className = "form-control is-invalid";
        $(`#${elementMsg}`).html(`${errorMsg}`);
    } else {
        document.getElementById(`${elementById}`).className = "form-control is-valid";
    }
}


//select option role for form create
$.ajax({
    url: "/admin/role/get",
}).done((response) => {
    console.log(response);
    
    $.each(response, function (key, val) {

        $('#inputRole')
            .append(`<input type="checkbox" id="${val.roleId}" class="flat" name="roles[]" value="${val.roleId}">`)
            .append(`<label for="${val.roleId}">${val.name}</label></div>`)
            .append(`<br>`);
    });

    $.each(response, function (key, val) {

        $('#inputRoleEdit')
            .append(`<input type="checkbox" id="${val.roleId}-edit" class="flat" name="rolesEdit[]" value="${val.roleId}">`)
            .append(`<label for="${val.roleId}">${val.name}</label></div>`)
            .append(`<br>`);
    });
       
}).fail((result) => {
    console.log(result);
});


//create data
$("#form-create-account").submit(function (event) {

    /* stop form from submitting normally */
    event.preventDefault();

    var checkboxes = document.getElementsByName('roles[]');
    var roles = [];
    for (var i = 0, n = checkboxes.length; i < n; i++) {
        if (checkboxes[i].checked) {
            roles.push(checkboxes[i].value);
        }
    }

   
    //get datetime
    let current = new Date();
    let cDate = current.getFullYear() + '-' + (current.getMonth() + 1) + '-' + current.getDate();
    let cTime = current.getHours() + ":" + current.getMinutes() + ":" + current.getSeconds();
    let dateTime = cDate + ' ' + cTime;

    var data_input = new Object();
    data_input.Name = $("#inputName").val();
    data_input.Username = $("#inputUsername").val();
    data_input.Email = $("#inputEmail").val();
    data_input.Password = $("#inputPassword").val();
    data_input.IsActive = true;
    data_input.UpdatedAt = dateTime;
    data_input.CreatedAt = dateTime;

    if (roles.length != 0) {
        data_input.Roles = roles;
    }

    
    console.log(data_input);

    $.ajax({
        url: '/admin/account/register',
        method: 'POST',
        dataType: 'json',
        contentType: 'application/x-www-form-urlencoded',
        data: data_input,
        success: function (response) {

            console.log(response);

            var obj = JSON.parse(response);

            console.log(obj);

            if (obj.errors != undefined) {
                checkValidation(obj.errors.Name, "inputName", "messageName");
                checkValidation(obj.errors.Username, "inputUsername", "messageUsername");
                checkValidation(obj.errors.Email, "inputEmail", "messageEmail");
                checkValidation(obj.errors.Password, "inputPassword", "messagePassword");
                if (obj.errors.Roles != undefined) {
                    $('#messageRoles').html(obj.errors.Roles);
                } else {
                    $('#messageRoles').html('');
                }
                
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
                $('#datatable-account').DataTable().ajax.reload();
            }

        },
        error: function (xhr, status, error) {
            var err = eval(xhr.responseJSON);
            console.log(err);
        }
    })
});

//delete 
deleteModalAccount = (id) => {

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
                url: `/admin/account/delete/${id}`,
                method: 'DELETE',
                contentType: 'application/x-www-form-urlencoded',
                success: function (response) {

                    Swal.fire(
                        'Deleted!',
                        `Data berhasil di hapus`,
                        'success'
                    )

                    //reload only datatable
                    $('#datatable-account').DataTable().ajax.reload();
                },
            })
        }
    })
}


//Edit
editModalRole = (id) => {

    $.ajax({
        url: `/admin/account/get/${id}`,
    }).done((result) => {
        //console.log(result);
        document.getElementById('form-edit-account').reset();
        
        $('#terakhirUpdate').html(`<i>${result.updatedAt}</i>`);
        //set value
        $('#inputAccountId').val(`${result.accountId}`);
        $("#inputNameEdit").val(`${result.name}`);
        $("#inputUsernameEdit").val(`${result.username}`);
        $("#inputEmailEdit").val(`${result.email}`);

        $.each(result.accountRoles, function (key, val) {
            document.getElementById(`${val.roleId}-edit`).checked = true;
        });

        if (result.isActive == true) {
            document.querySelector('#inputIsActiveEdit').options[1].selected = true;
        } else {
            document.querySelector('#inputIsActiveEdit').options[0].selected = true;
        }

    }).fail((result) => {
        console.log(result);
    });
}


//update
$("#form-edit-account").submit(function (event) {

    /* stop form from submitting normally */
    event.preventDefault();

    var checkboxes = document.getElementsByName('rolesEdit[]');
    var roles = [];
    for (var i = 0, n = checkboxes.length; i < n; i++) {
        if (checkboxes[i].checked) {
            roles.push(checkboxes[i].value);
        }
    }
      
    //get datetime
    let current = new Date();
    let cDate = current.getFullYear() + '-' + (current.getMonth() + 1) + '-' + current.getDate();
    let cTime = current.getHours() + ":" + current.getMinutes() + ":" + current.getSeconds();
    let dateTime = cDate + ' ' + cTime;
       
    var data_input = new Object();
    data_input.AccountId = $("#inputAccountId").val();
    data_input.Name = $("#inputNameEdit").val();
    data_input.Username = $("#inputUsernameEdit").val();
    data_input.Email = $("#inputEmailEdit").val();
    data_input.IsActive = $('#inputIsActiveEdit').val();
    data_input.UpdatedAt = dateTime;

    if (roles.length != 0) {
        data_input.Roles = roles;
    } 

    console.log(JSON.stringify(data_input));

    $.ajax({
        url: `/admin/account/register/update`,
        method: 'PUT',
        dataType: 'json',
        contentType: 'application/x-www-form-urlencoded',
        data: data_input,
        success: function (response) {

            var obj = JSON.parse(response);

            console.log(obj);

            if (obj.errors != undefined) {
                checkValidation(obj.errors.Name, "inputNameEdit", "messageNameEdit");
                checkValidation(obj.errors.Username, "inputUsernameEdit", "messageUsernameEdit");
                checkValidation(obj.errors.Email, "inputEmailEdit", "messageEmailEdit");
                if (obj.errors.Roles != undefined) {
                    $('#messageRolesEdit').html(obj.errors.Roles);
                } else {
                    $('#messageRolesEdit').html('');
                }
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
                $('#datatable-account').DataTable().ajax.reload();
            }

        },
        error: function (xhr, status, error) {
            var err = eval(xhr.responseJSON);
        }
    });
});
