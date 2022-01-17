﻿$(document).ready(function () {
    getUniversity();
    tableEmployee = $("#table_id").DataTable({
        "ajax": {
            //"url": "https://localhost:44372/API/Employees/GetAllData",
            "url": "/employees/getall",
            "dataType": 'json',
            "dataSrc": ''
        },
        'columns': [
            {
                "data": null,
                "render": function (data, type, row, meta) {
                    return meta.row + meta.settings._iDisplayStart + 1;
                }
            },
            {
                'data': 'nik'
            },
            {
                'data': 'firstName'
            },
            { 'data': 'lastName' },
            {
                'data': null,
                "render": function (data, type, row) {
                    return `${row['firstName']} ${row['lastName']}`;
                }
            },
            { 'data': 'phone' },
            {
                'data': null,
                'render': (data, type, row) => {
                    var dataGet = new Date(row['birthDate']);
                    return dataGet.toLocaleDateString();
                }
            },
            { 'data': 'email' },
            { 'data': 'gender' },
            {
                "data": null,
                "render": function (data, type, row) {
                    return `<button type="button" class="btn btn-primary" onclick="getData(${row["nik"]})" data-toggle="modal" data-target="#employeeDetailModal">
                              <i class="fas fa-search"></i>
                            </button>
                            <button type="button" class="btn btn-primary"  data-toggle="modal" data-target="#EmployeeEditModal">
                              <i class="fas fa-pencil-alt"></i>
                            </button>
                            <button type="button" class="btn btn-primary" onclick="Delete(${row["nik"]})" data-toggle="modal">
                              <i class="fas fa-trash"></i>
                            </button >`;
                }
            }
        ],
        dom: 'Bfrtip',
        buttons: [
            {
                extend: 'copyHtml5',
                text: '<i class="fas fa-copy color1"> Copy</i>',
                className: 'btn btn-outline-secondary',
                exportOptions: {
                    columns: [0, 1, 2, 3, 4, 5, 6, 7]
                }
            },
            {
                extend: 'csv',
                text: '<i class="fas fa-file-csv color2"> CSV</i>',
                className: 'btn btn-outline-secondary',
                exportOptions: {
                    columns: [0, 1, 2, 3, 4, 5, 6, 7]
                }
            },
            {
                extend: 'excelHtml5',
                text: '<i class="fas fa-file-excel color3"> Excel</i>',
                className: 'btn btn-outline-secondary',
                exportOptions: {
                    columns: [0, 1, 2, 3, 4, 5, 6, 7]
                }
            },
            {
                extend: 'pdfHtml5',
                text: '<i class="fas fa-file-pdf color4"> PDF</i>',
                className: 'btn btn-outline-secondary',
                exportOptions: {
                    columns: [0, 1, 2, 3, 4, 5, 6, 7]
                }
            },
            {
                extend: 'print',
                text: '<i class="fas fa-print color5"> Print</i>',
                className: 'btn btn-outline-secondary',
                exportOptions: {
                    columns: [0, 1, 2, 3, 4, 5, 6, 7]
                }
            }
        ]
    });
});

function Insert(callback) {
    //1
    var obj = new Object();

    /*obj.NIK = $("#NIK").val();*/
    obj.FirstName = $("#FirstName").val();
    obj.LastName = $("#LastName").val();
    obj.Phone = $("#Phone").val();
    obj.BirthDate = $("#BirthDate").val();
    obj.Salary = $("#Salary").val();
    obj.Gender = $("#Gender").val();
    obj.UniversityName = $("#UniversityName").val();
    obj.GPA = $("#GPA").val();
    obj.Degree = $("#Degree").val();
    obj.Email = $("#Email").val();
    obj.Password = $("#Password").val();
    obj.UniversityID = $("#UniversityID").val();

    console.log(JSON.stringify(obj));

    $.ajax({
        //url: "https://localhost:44372/API/Employees/Register",
        url: "/employees/register",
        type: "POST",
        
        data: obj,
        success: callback
    }).done((result) => {
        if (result == 200) {

            Swal.fire(
                'Yeayy',
                result.messageResult,
                'success'
            )
            $('#EmployeeInsertModal').modal('hide');
        }
        //tableEmployee.ajax.reload();
        console.log(result);
    }).fail((error) => {
        //alert pemberitahuan jika gagal
        Swal.fire(
            'Opps!',
            'Sepertinya terjadi kesalahan, periksa kembali!',
            'error'
        )
    });

}

function Delete(nik) {
    Swal.fire({
        title: 'Yakin ingin dihapus?',
        text: "Data akan dihapus dari database.",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yakieen dong!',
        cancelButtonText: 'Engga jadi'

    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                //url: "https://localhost:44372/API/Employees/" + nik,
                url: "employees/delete/" +nik,
                type: "DELETE",
                crossDomain: true,
            }).done((result) => {
                Swal.fire(
                    'Yeayy',
                    result.messageResult,
                    'success'
                )
                tableEmployee.ajax.reload();
                //$('.createEmployee').modal('hide');

            }).fail((error) => {

                Swal.fire(
                    'Opps!',
                    'Sepertinya terjadi kesalahan, periksa kembali!',
                    'error'
                )
            })
        }
    })
}
function getNIK(nik, result) {
    var dataNIK = result.filter((g) => {
        return g.nik == nik;
    })
    return dataNIK[0];
}

function getData(nik) {
    $.ajax({
        /*url: "https://localhost:44372/API/Employees/" + nik,*/
        url: "/Employees/getregister/" +nik
        //type: "GET"
    }).done((result) => {
        //$("#listEmployee").html(text);
        console.log(getNIK(nik, result))
        var dataNIK = getNIK(nik, result);
        var text = ''
        text = `<div class = "text-center">
                    <table class= "table bg-light table-hover text-info text-center">
                        <tr>
                            <td>Name</td>
                            <td>:</td>
                            <td>${dataNIK.firstName} ${dataNIK.lastName}</td>
                        </tr>
                        <tr>
                            <td>Birth Date</td>
                            <td>:</td>
                            <td>${dataNIK.birthDate}</td>
                        </tr>
                        <tr>
                            <td>Salary</td>
                            <td>:</td>
                            <td>Rp. ${dataNIK.salary}</td>
                        </tr>
                        <tr>
                            <td>E-mail</td>
                            <td>:</td>
                            <td>${dataNIK.email}</td>
                        </tr>
                        <tr>
                            <td>University</td>
                            <td>:</td>
                            <td>${dataNIK.universityName}</td>
                        </tr
                        <tr>
                            <td>GPA</td>
                            <td>:</td>
                            <td>${dataNIK.gpa}</td>
                        </tr>
                         <tr>
                            <td>Degree</td>
                            <td>:</td>
                            <td>${dataNIK.degree}</td>
                        </tr>
                    </table>
                    </div>`
        $('#EmployeeDetailModal-body').html(text).css('background-color', 'white');
    }).fail((error) => {
        console.log(error);
    });
}

function Update() {
    var obj = new Object();
    obj.nik = $("#NIKedit").val();
    obj.firstName = $("#FirstNameedit").val();
    obj.lastName = $("#LastNameedit").val();
    obj.email = $("#Emailedit").val();
    obj.phone = $("#Phoneedit").val();
    obj.birthDate = $("#BirthDateedit").val();
    obj.salary = $("#Salaryedit").val();
    obj.gender = $("#Genderedit").val();
    console.log(JSON.stringify(obj));

    $.ajax({
        /*headers: {
            /// 'Accept': 'application/json',/
            'Content-Type': 'application/json'
        },*/
        //url: "https://localhost:44372/API/Employees/UpdateRegisterData",
        url:"/employees/Put",
        type: "PUT",
        data: obj
        //headers: {
        //    'Accept': 'application/json',
        //    'Content-Type': 'application/json'
        //},
        //dataType: 'json',
        //data: JSON.stringify(obj),
    }).done((result) => {
        //buat alert pemberitahuan jika success

        /*alert(result.messageResult)*/
        Swal.fire(
            'Yeayy',
            result.messageResult,
            'success'
        )
        //tableEmployee.ajax.reload();
        $('#EmployeeEditModal').modal('hide');

    }).fail((error) => {

        Swal.fire(
            'Opps!',
            'Sepertinya terjadi kesalahan, periksa kembali!',
            'error'
        )
        //alert pemberitahuan jika gagal
    });
}

//$('#EmployeeInsertModal').on('show.bs.modal', function () {
//    console.log("test");
    
//});
function getUniversity() {
    $.ajax({
        url: 'https://localhost:44372/API/Universities'
    }).done((data) => {
        console.log(data);
        var universitySelect = '';
        $.each(data, function (key, val) {
            universitySelect += `<option value='${val.universityID}'>${val.universityName}</option>`
        });
        $("#UniversityID").html(universitySelect);

    }).fail((error) => {
        console.log(error);
    })
}

//function closeModal() {
//    $('#EmployeeInsertModal').modal('hide');
//}

//Form Validation
(function () {
    'use strict';
    window.addEventListener('load', function () {
        var table = $('#tableEmployee').DataTable();
        var forms = document.getElementsByClassName('needs-validation');
        var validation = Array.prototype.filter.call(forms, function (form) {
            form.addEventListener('submit', function (event) {
                if (form.checkValidity() === false) {
                    event.preventDefault();
                    event.stopPropagation();
                }
                else {
                    Insert();
                    /*$('#EmployeeEditModal').modal('hide');*/
                    $('#EmployeeInsertModal').modal('hide');
                    table.ajax.reload();
                }

                form.classList.add('was-validated');
            }, false);
        });
    }, false);
})();

//(function () {
//    'use strict';
//    window.addeventlistener('load', function () {
//        var table = $('#tableemployee').datatable();
//        var forms = document.getelementsbyclassname('needs-validation');
//        var validation = array.prototype.filter.call(forms, function (form) {
//            form.addeventlistener('submit', function (event) {
//                if (form.checkvalidity() === false) {
//                    event.preventdefault();
//                    event.stoppropagation();
//                }
//                else {
//                    update();
//                    $('#employeeeditmodal').modal('hide');
//                   /* $('#employeeinsertmodal').modal('hide');*/
//                    table.ajax.reload();
//                }

//                form.classlist.add('was-validated');
//            }, false);
//        });
//    }, false);
//})();

function validateForm() {
    let x = document.form["EditForm"]["firstName"].value;
    if (x == "") {
        alert("Nama Harus diisi!!");
        return false;
    }
}


//CountEmployeeBySalary
$.ajax({
    url: "https://localhost:44372/API/Employees/CountEmployeeBySalary"
}).done((result) => {
    console.log(result);

    const salary = [];
    const valEmp2 = [];
    $.each(result.result, function (key, val) {
        if (val.salary != 0) {
            salary.push(val.salary);
            valEmp2.push(val.value);
        }
    });

    var optionsSal = {
        series: [{
            name: 'Employee',
            data: generateSalary(salary, valEmp2)
        }],
        chart: {
            height: 350,
            type: 'bar'
        },
        plotOptions: {
            bar: {
                columnWidth: '40%'
            }
        },
        colors: ['#00E396'],
        dataLabels: {
            enabled: false
        },
        legend: {
            show: true,
            showForSingleSeries: true,
            customLegendItems: ['Employees'],
            markers: {
                fillColors: ['#00E396', '#775DD0']
            }
        }
    };

    var chart2 = new ApexCharts(document.querySelector("#CountEmployeeBySalary"), optionsSal);
    chart2.render();
}).fail((error) => {
    console.log(error);
});

function generateSalary(salary, valEmp2) {
    var values = [salary, valEmp2];
    var i = 0;
    var series = [];
    while (i < salary.length) {
        /*series.push([x, values[s][i]]);*/
        series.push([
            values[0][i],
            values[1][i]
        ]);
        i++;
    }
    return series;
}

//CountEmployeeByUniversity
$.ajax({
    url: "https://localhost:44372/API/Universities/GetTotalEmployee"
}).done((result) => {
    console.log(result);

    const univName = [];
    const valEmp1 = [];
    const data = []
    $.each(result.result, function (key, val) {
        if (val.universityName != null) {
            univName.push(val.universityName);
            valEmp1.push(val.value);
            data.push(univName, valEmp1);
        }
    });

    var options1 = {
        series: [{
            name: 'Total Employee',
            data: valEmp1
        }],
        goals: [{
            name: 'University'
        }],
        chart: {
            type: 'bar',
            /*height: 350*/
        },
        plotOptions: {
            bar: {
                borderRadius: 4,
                horizontal: true,
            }
        },
        dataLabels: {
            enabled: false
        },
        legend: {
            show: true,
            showForSingleSeries: true,
            customLegendItems: ['Universitas'],
            markers: {
                fillColors: ['#71C7FF', '#775DD0']
            }
        },
        xaxis: {
            categories: univName,
        }
    };


    var chart1 = new ApexCharts(document.querySelector("#CountEmployeeByUniversity"), options1);
    chart1.render();
}).fail((error) => {
    console.log(error);
});
//Test
//var optionsT = {
//    series: [{
//        data: [400, 430, 448, 470, 540, 580, 690, 1100, 1200, 1380]
//    }],
//    chart: {
//        type: 'bar',
//        height: 350
//    },
//    plotOptions: {
//        bar: {
//            borderRadius: 4,
//            horizontal: true,
//        }
//    },
//    dataLabels: {
//        enabled: false
//    },
//    xaxis: {
//        categories: ['South Korea', 'Canada', 'United Kingdom', 'Netherlands', 'Italy', 'France', 'Japan',
//            'United States', 'China', 'Germany'
//        ],
//    }
//};
//var chart3 = new ApexCharts(document.querySelector("#SalaryChart"), optionsT);
//chart3.render();

//Gender
//var optionsG = {
//    chart: {
//        type: "area",
//        height: 300,
//        foreColor: '#999',
//        stacked: true,
//        dropShadow: {
//            enabled: true,
//            enabledSeries: [0],
//            top: -2,
//            left: 2,
//            blur: 5,
//            opacity: 0.06
//        }
//    },
//    colors: ['#00E396', '#0090FF'],
//    stroke: {
//        curve: "smooth",
//        width: 3
//    },
//    dataLabels: {
//        enabled: false
//    },
//    series: [{
//        name: 'Total Views',
//        data: generateDayWiseTimeSeries(0, 18)
//    }, {
//        name: 'Unique Views',
//        data: generateDayWiseTimeSeries(1, 18)
//    }],

//    markers: {
//        size: 0,
//        strokeColor: '#fff',
//        strokeWidth: 3,
//        strokeOpacity: 1,
//        fillOpacity: 1,
//        hover: {
//            size: 6
//        }
//    },

//    xaxis: {
//        type: "dateTime",
//        axisBorder: {
//            show: false
//        },
//        axisTicks: {
//            show: false
//        }
//    },
//    yaxis: {
//        labels: {
//            offsetX: 14,
//            offsetY: -5
//        },
//        tooltip: {
//            enabled: true
//        }
//    },
//    grid: {
//        padding: {
//            left: -5,
//            right: 5
//        }
//    },
//    tooltip: {
//        x: {
//            format: "dd mm yyyy"
//        },
//    },
//    legend: {
//        position: 'top',
//        horizontalAlign: 'left'
//    },
//    fill: {
//        type: "solid",
//        fillOpacity: 0.7
//    }
//};
//var chart = new ApexCharts(document.querySelector("#CountEmployeeByGender"), optionsG);

//chart.render();

//function generateDayWiseTimeSeries(s, count) {
//    var values = [[
//        4, 3, 10, 9, 29, 19, 25, 9, 12, 7, 19, 5, 13, 9, 17, 2, 7, 5
//    ], [
//        2, 3, 8, 7, 22, 16, 23, 7, 11, 5, 12, 5, 10, 4, 15, 2, 6, 2
//    ]];
//    var i = 0;
//    var series = [];
//    var x = new Date("11 Nov 2012").getTime();
//    while (i < count) {
//        series.push([x, values[s][i]]);
//        x += 86400000;
//        i++;
//    }
//    return series;
//}