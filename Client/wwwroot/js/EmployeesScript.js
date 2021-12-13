var exported = [0, 1, 2, 3, 4, 5, 6]

$(document).ready(function () {
    table = $("#Employeetable").DataTable({
        pageLength: 5,
        lengthMenu: [[5, 10, 20, -1], [5, 10, 20, 'All']],
        responsive: true,
        dom: 'lBfrtip',
        buttons: [
            {
                extend: 'copy',
                exportOptions: {
                    columns: exported,
                }
            },
            {
                extend: 'csv',
                exportOptions: {
                    columns: exported
                }
            },
            {
                extend: 'excel',
                title: 'List Employees',
                messageTop: 'Employee List of X Comppany.',
                exportOptions: {
                    columns: exported,
                    title: 'List Employees',
                }
            },
            {
                extend: 'pdf',
                title: 'List Employees',
                orientation: 'landscape',
                text: 'PDF',
                className: 'buttonHide',
                fileName: 'Data',
                autoFilter: true,
                messageTop: 'Employee List of X Comppany.',
                exportOptions: {
                    columns: exported
                }
            },
            {
                extend: 'print',
                title: 'List Employees',
                messageTop: 'Employee List of X Comppany.',
                exportOptions: {
                    columns: exported,
                }
            }
        ],
        "ajax": {
            "url": "https://localhost:44345/API/Employees",
            "dataSrc": "result"
        },
        "columns": [

            {
                "data": null,
                "render": function (data, type, row, meta) {
                    return meta.row + meta.settings._iDisplayStart + 1;
                }
            },
            {
                "data": "nik",
            },
            {
                "data": null,
                "render": function (data, type, row) {
                    return `${row["firstName"]} ${row["lastName"]}`;
                }
            },
            { "data": "email" },
            {
                "data": null,
                "render": function (data, type, row) {
                    if (row['gender'] === 0) {
                        return `Male`;
                    } else {
                        return `Female`;
                    }
                }
            },
            {
                "data": null,
                "render": function (data, type, row) {
                    return dateConversion(row["birthDate"]);
                }
            },
            { "data": "phone" },
            {
                "data": null,
                "render": function (data, type, row) {
                    return `<button type="button" class="btn btn-primary" data-toggle="modal" 
                    onclick="getData('${row['nik']}')" data-placement="top" title="Detail" data-target="#DetailModal" >
                    <i class="fas fa-info-circle"></i> 
                    </button>
                    <button type="button" class="btn btn-danger" data-toggle="modal" onclick="Delete('${row['nik']}')" data-placement="top" title="Delete">
                    <i class="fas fa-trash-alt"></i> 
                    </button>
                    <button type="button" class="btn btn-info" data-toggle="modal" 
                    onclick="getDataUpdate('${row['nik']}')" title="Edit" data-target="#UpdateModals">
                    <i class="fas fa-edit"></i>
                    </button>`;
                }
            }
        ],
    });
});

function getData(nik) {
    $.ajax({
        url: "https://localhost:44345/API/Employees/" + nik,
        success: function (result) {
            var data = result.result
            console.log(data.firstName)
            var text = ""
            text =
                `<tr>
                <td> Name </td>
                <td> : </td>
                <td> ${data.firstName} ${data.lastName}</td>
            </tr>
            <tr>
                <td> NIK </td>
                <td> : </td>
                <td>${nik}</td>
            </tr>
            <tr>
                <td> Gender </td>
                <td> : </td>
                <td>${data.gender}</td>
            </tr>`
            $(".data-employ").html(text);
        },
        error: function (error) {
            console.log(error)
        }
    })
}

function dateConversion(dates) {
    var date = new Date(dates)
    var newDate = ((date.getMonth() > 8) ? (date.getMonth() + 1) : ('0' + (date.getMonth() + 1))) + '/' + ((date.getDate() > 9) ? date.getDate() : ('0' + date.getDate())) + '/' + date.getFullYear()
    return newDate
}

function Insert() {
    var obj = new Object();
    obj.nik = $("#nik").val();
    obj.firstName = $("#firstName").val();
    obj.lastName = $("#lastName").val();
    obj.email = $("#email").val();
    obj.phone = $("#phone").val();

    console.log(obj)

    $.ajax({
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        url: "https://localhost:44345/api/Employees/",
        type: "Post",
        'data': JSON.stringify(obj),
        'dataType': 'json',
        success: function (result) {
            Swal.fire(
                'Good job!',
                'Data berhasil di Submit!',
                'success'
            ),

                setTimeout(function () { $('#CreateModal').modal('hide'); }, 2000),
                table.ajax.reload(),
                setTimeout(function () { $("#createAccount")[0].reset(); }, 4000)
        },
        error: function (error) {
            Swal.fire({
                icon: 'error',
                title: 'Oops...',
                text: 'Submit Gagal!'
            })
        }
    })
}

function Delete(nik) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: "https://localhost:44345/api/Employees/" + nik,
                type: "Delete",
                success: function (result) {
                    Swal.fire(
                        'Deleted!',
                        'Your file has been deleted.',
                        'success'
                    )
                    table.ajax.reload()
                },
                error: function (error) {
                    alert("Delete Fail");
                }
            });
        }
    })
}

function Update() {
    var obj = new Object();
    obj.nik = $("#updatenik").val();
    obj.firstName = $("#updatefirstName").val();
    obj.lastName = $("#updatelastName").val();
    obj.email = $("#updateemail").val();
    obj.phone = $("#updatephone").val();
    obj.salary = $("#updatesalary").val();
    console.log(obj)
    $.ajax({
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        url: "https://localhost:44345/api/Employees/",
        type: "Put",
        'data': JSON.stringify(obj),
        'dataType': 'json',
        success: function (result) {
            Swal.fire(
                'Good job!',
                'Your data has been saved!',
                'success'
            ),

                table.ajax.reload()
        },
        error: function (error) {
            Swal.fire({
                icon: 'error',
                title: 'Oops...',
                text: 'Submit Fail!'
            })
        }
    })
}

function getDataUpdate(nik) {
    $.ajax({
        url: "https://localhost:44345/API/Employees/" + nik,
        success: function (result) {
            console.log(result)
            var data = result.result
            $("#updatenik").attr("value", data.nik)
            $("#updatefirstName").attr("value", data.firstName)
            $("#updatelastName").attr("value", data.lastName)
            $("#updateemail").attr("value", data.email)
            $("#updatephone").attr("value", data.phone)
            $("#updatesalary").attr("value", data.salary)
            $("#updatedateBirth").attr("value", data.birthDate)
            $("#updategender").attr("value", data.gender)
        },
        error: function (error) {
            console.log(error)
        }
    })
}

function resetFrom() {
    $("#createAccount")[0].reset()
}


//< !-- Universities  -- >

$.ajax({
    url: "https://localhost:44345/api/Universities/CountEmployeeUniversity",
    success: function (result) {
        console.log(result)
        var data = result.result
        var options2 = {
            series: data.value,
            labels: data.key,
            chart: {
                width: "100%",
                type: 'donut',
                toolbar: {
                    show: true,
                    offsetX: 0,
                    offsetY: 0,
                    export: {
                        csv: {
                            filename: undefined,
                            columnDelimiter: ',',
                            headerCategory: 'category',
                            headerValue: 'value',
                            dateFormatter(timestamp) {
                                return new Date(timestamp).toDateString()
                            }
                        },
                        svg: {
                            filename: undefined,
                        },
                        png: {
                            filename: undefined,
                        }
                    },
                    autoSelected: 'zoom'
                },
            },
            responsive: [{
                breakpoint: 500,
                options: {
                    chart: {
                        width: "50%",
                    },
                    legend: {
                        position: 'bottom'
                    }
                }
            }]
        };
        var chart2 = new ApexCharts(document.querySelector("#chart2"), options2);
        chart2.render();
    },
    error: function (error) {
        console.log(error)
    }
})

$.ajax({
    url: "https://localhost:44345/api/Employees/",
    success: function (result) {
        var data = result.result
        var male = 0;
        var female = 0;

        $.each(data, function (key, val) {
            if (val.gender == 0) {
                male++
            } female++
        });

        var options3 = {
            series: [male, female],
            labels: ["Male", "Female"],
            chart: {
                width: "90%",
                type: 'donut',
            },
            responsive: [{
                options: {

                    breakpoint: 500,
                    chart: {
                        width: "40%",
                    },
                    legend: {
                        position: 'bottom'
                    }
                }
            }]
        };
        var chart3 = new ApexCharts(document.querySelector("#chart"), options3);
        chart3.render();
    },
    error: function (error) {
        console.log(error)
    }
})

$.ajax({
    url: "https://localhost:44345/api/Universities/CountEmployeeUniversity",
    success: function (result) {
        console.log(result)
        var data = result.result
        var options3 = {
            series: [{
                name: "Employees",
                data: data.value
            }],
            chart: {
                type: 'bar',
                height: 350
            },
            plotOptions: {
                bar: {
                    borderRadius: 4,
                    horizontal: false,
                }
            },
            dataLabels: {
                enabled: false
            },
            xaxis: {
                categories: data.key,
            }
        };

        var chart3 = new ApexCharts(document.querySelector("#chart3"), options3);
        chart3.render();
    },
    error: function (error) {
        console.log(error)
    }
})










//$("#createAccount").validate({
//    rules: {
//        "#nik": {
//            required: true
//        },
//        "#firstName": {
//            required: true
//        },
//        "#lastName": {
//            required: true
//        },
//        "#email": {
//            required: true,
//            email: true
//        },
//        "#phone": {
//            required: true
//        }
//    },
//    errorPlacement: function (error, element) { },
//    highlight: function (element) {
//        console.log(element),
//        $(element).closest('.form-control').addClass('is-invalid');
//    },
//    unhighlight: function (element) {
//        $(element).closest('.form-control').removeClass('is-invalid');
//    },
//});
