function LoginEmployee() {
    var obj = new Object();
    var valueRe = $("#inputEmail").val();
    if (validateEmail(valueRe) == true) {
        obj.Email = valueRe;
    } else {
        obj.Phone = valueRe;
    }

    obj.Password = $("#inputPassword").val();
    console.log(obj);
    $.ajax({
        url: "https://localhost:44372/API/accounts/Login",
        type: "POST",
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        dataType: 'json',
        data: JSON.stringify(obj),
        success: function (response) {
            console.log(response);
            if (response == null) {
                console.log(response);
                Swal.fire(
                    'Opps!',
                    'Email / Phone / Password kurang tepat.',
                    'error'
                )
            } else {
                console.log(response);
                Swal.fire({
                    tittle: 'Yeay!! Login Berhasil',
                    html:
                        'Mohon Tunggu <br>' +
                        '<strong></strong> detik <br>' +
                        'Direct to Dashboard Employee',
                    icon: 'success',
                    timer: 13000,
                    showConfirmButton: false,
                    allowOutsideClick: false,
                    didOpen: () => {
                        timerInterval = setInterval(() => {
                            Swal.getHtmlContainer().querySelector('strong')
                                .textContent = (Swal.getTimerLeft() / 1000)
                                    .toFixed(0)

                        }, 100)
                    },
                    willClose: () => {
                        clearInterval(timerInterval)
                        window.location.href = 'https://localhost:44303/Employees';
                    }

                })
            }
        },
        error: function (response) {
            console.log("error : " + JSON.stringify(response));
            Swal.fire(
                'Opps!',
                'Sepertinya terjadi kesalahan, periksa kembali!',
                'error'
            )
        }
    });
}

function validateEmail(input) {
    if (/^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/.test(input)) {
        return (true)
    }
    return (false)
}

function Swal() {
    Swal.fire({
        icon: 'Success',
        title: 'Yeaayy',
        text: 'Login Berhasil',
        //footer: '<a href="">Why do I have this issue?</a>'
    })
}