var firstName = $(".FirstName");
var lastName = $(".LastName");
var uidSerie = $(".UidSerie");
var email = $(".Email");
var birthDate = $(".BirthDate");
var gender = $(".Gender");
var passwordRegister = $(".passwordRegister");
var paswordConfirmRegister = $(".passwordConfirm");
var userName = $(".userNameLogin");
var passwordLogin = $(".passwordLogin");

///validations----------------------------------------------------------------------
function valid() {
    var isValid = false;
    if (firstName.val() != "" && lastName.val() != "" && uidSerie.val() != "" && email.val() != "" && birthDate.val() != "" && gender.val() != "Seleccione su identidad de genero" && passwordRegister.val() != "") {
        isValid = true;
    } else {
        isValid = false;
    }
    return isValid;
}

function validPassword() {
    isValid = false;
    if (passwordRegister.val() == paswordConfirmRegister.val()) {
        isValid = true;
    } else {
        isValid = false;
    }
    return isValid;
}

function validCredentials() {
    isValid = false;
    if (userName.val() != "" && passwordLogin.val() != "") {
        isValid = true;
    } else {
        isValid = false;
    }
    return isValid;
}


///Register-------------------------------------------------------------------------------------------------------------------------------------
$(".registerButton").on("click", function () {
    if (valid()) {
        if (validPassword()) {
            $.ajax({
                url: '/account/SignUp',
                method: 'post',
                data: {
                    newUser: {
                        FirstName: firstName.val(),
                        LastName: lastName.val(),
                        UidSerie: uidSerie.val(),
                        Email: email.val(),
                        BirthDate: birthDate.val(),
                        Gender: gender.val(),
                        Password: passwordRegister.val(),
                    }
                },
                success: function (result) {
                    if (result == 'True') {
                        location.href = window.location.protocol + "//" + window.location.host + "/account/Index";
                    } else if (result == 'False') {
                        $(".userCreated").addClass('show');
                    }
                }
            })
        } else {
            $(".unMatchPasswords").addClass('show');
        }
    } else {
        $(".unValidInputs").addClass('show');
    }
})

///Login--------------------------------------------------------------------------------------------------------------------------------------------
$(".buttonLogin").on('click', function () {
    if (validCredentials()) {
        $.ajax({
            url: '/account/SignIn',
            method: 'post',
            data: {
                User: {
                    userName: userName.val(),
                    Password: passwordLogin.val()
                }
            },
            success: function (result) {
                if (result == "In") {
                    location.href = window.location.protocol + "//" + window.location.host + "/Profile/Index";
                } else if (result == "notPassword") {
                    $(".invalidPassword").addClass('show');
                } else if (result == "notExist") {
                    $(".notExist").addClass('show');
                }
            }
        })
    } else {
        $(".inputsEmpty").addClass('show');
    }
})
