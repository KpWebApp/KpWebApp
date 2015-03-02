$(document).ready(function () {
    newYearSelected();
    var regfrm = document.getElementById("registerForm");
    console.log(regfrm);
    $('.registerForm').bootstrapValidator({
        message: 'This value is not valid',
        feedbackIcons: {
            valid: 'glyphicon glyphicon-ok',
            invalid: 'glyphicon glyphicon-remove',
            validating: 'glyphicon glyphicon-refresh'
        },
        fields: {
            username: {
                message: 'Не правильний логін',
                validators: {
                    notEmpty: {
                        message: 'Логін не може бути порожнім'
                    },
                    stringLength: {
                        min: 2,
                        max: 30,
                        message: 'Логін повинен містити від 2 до 30 символів'
                    },
                    regexp: {
                        regexp: /^[a-zA-Z0-9_]+$/,
                        message: 'Логін може містити тільки літери і цифри'
                    }
                }
            },
            FirstName: {
                message: 'Не дійсне ім\'я',
                validators: {
                    notEmpty: {
                        message: 'Ім\'я не може бути порожнім'
                    },
                    stringLength: {
                        min: 3,
                        max: 30,
                        message: 'Ім\'я повинно містити від 3 до 20 символів'
                    },
                    regexp: {
                        regexp: /^[а-яА-ЯІіЄєЇї]+$/,
                        message: 'Ім\'я може містити тільки літери'
                    }
                }
            },
            LastName: {
                message: 'Не дійсне прізвище',
                validators: {
                    notEmpty: {
                        message: 'Прізвище не може бути порожнім'
                    },
                    stringLength: {
                        min: 2,
                        max: 30,
                        message: 'Прізвище повинно містити від 2 до 20 символів'
                    },
                    regexp: {
                        regexp: /^[а-яА-ЯІіЄєЇї]+$/,
                        message: 'Ім\'я може містити тільки літери'
                    }
                }
            },
            Email: {
                validators: {
                    notEmpty: {
                        message: 'email адреса не може бути порожньою'
                    },
                    emailAddress: {
                        message: 'Ви ввели не вірну email адресу'
                    }
                }
            },
            Password: {
                validators: {
                    notEmpty: {
                        message: 'Введіть пароль'
                    }
                }
            },
            secondPassword: {
                validators: {
                    notEmpty: {
                        message: 'Введіть пароль'
                    },
                    identical: {
                        field: 'Password',
                        message: 'Пароль і підтвердження паролю не співпадають'
                    }
                }
            }
        }
    });
    $('.registerForm').bootstrapValidator({
        message: 'This value is not valid',
        feedbackIcons: {
            valid: 'glyphicon glyphicon-ok',
            invalid: 'glyphicon glyphicon-remove',
            validating: 'glyphicon glyphicon-refresh'
        },
        fields: {
            username: {
                message: 'Не правильний логін',
                validators: {
                    notEmpty: {
                        message: 'Логін не може бути порожнім'
                    },
                    stringLength: {
                        min: 2,
                        max: 30,
                        message: 'Логін повинен містити від 2 до 30 символів'
                    },
                    regexp: {
                        regexp: /^[a-zA-Z0-9_]+$/,
                        message: 'Логін може містити тільки літери і цифри'
                    }
                }
            },
            FirstName: {
                message: 'Не дійсне ім\'я',
                validators: {
                    notEmpty: {
                        message: 'Ім\'я не може бути порожнім'
                    },
                    stringLength: {
                        min: 3,
                        max: 30,
                        message: 'Ім\'я повинно містити від 3 до 20 символів'
                    },
                    regexp: {
                        regexp: /^[а-яА-ЯІіЄєЇї]+$/,
                        message: 'Ім\'я може містити тільки літери'
                    }
                }
            },
            LastName: {
                message: 'Не дійсне прізвище',
                validators: {
                    notEmpty: {
                        message: 'Прізвище не може бути порожнім'
                    },
                    stringLength: {
                        min: 2,
                        max: 30,
                        message: 'Прізвище повинно містити від 2 до 20 символів'
                    },
                    regexp: {
                        regexp: /^[а-яА-ЯІіЄєЇї]+$/,
                        message: 'Ім\'я може містити тільки літери'
                    }
                }
            },
            Email: {
                validators: {
                    notEmpty: {
                        message: 'email адреса не може бути порожньою'
                    },
                    emailAddress: {
                        message: 'Ви ввели не вірну email адресу'
                    }
                }
            },
            Password: {
                validators: {
                    notEmpty: {
                        message: 'Введіть пароль'
                    }
                }
            },
            secondPassword: {
                validators: {
                    notEmpty: {
                        message: 'Введіть пароль'
                    },
                    identical: {
                        field: 'Password',
                        message: 'Пароль і підтвердження паролю не співпадають'
                    }
                }
            }
        }
    });
    $('.changePassword').bootstrapValidator({
        message: 'This value is not valid',
        feedbackIcons: {
            valid: 'glyphicon glyphicon-ok',
            invalid: 'glyphicon glyphicon-remove',
            validating: 'glyphicon glyphicon-refresh'
        },
        fields: {

            NewPassword: {
                validators: {
                    notEmpty: {
                        message: 'Введіть пароль'
                    }
                }
            },
            secondNewPassword: {
                validators: {
                    notEmpty: {
                        message: 'Введіть пароль'
                    },
                    identical: {
                        field: 'Password',
                        message: 'Пароль і підтвердження паролю не співпадають'
                    }
                }
            }
        }
    });
});


//function newYearSelected() {
//    $('#graduates').fadeOut();
//    $('#graduates').empty();
//    $('#graduates').append('<th>Ім\'я</th>');
//    $.ajax({
//        url: '/Search/Changed?Year=' + $('#year').val(),
//        type: 'post',
//        dataType: 'json',
//        success: function(data) {            
//            $.each(data, function(key, val) {                
//                if (val.IsRegistred) {
//                    var person = '<tr><td><a href = "/Personal/Profile?username=';
//                    if (val.IsTeacher) {
//                        person += (val.Username + '">' + val.FullName + '</a> <a href = "/Post/TeachersInfoPage?userId= ' + val.UserId + '"><span class ="glyphicon glyphicon-info-sign"></span></a> </td> </tr>');
//                    } else {
//                        person += (val.Username + '">' + val.FullName + '</a></td> </tr>');
//                    }
//                } else {
//                    var person = '<tr><td>';
//                    if (val.IsTeacher) {
//                        person += (val.FullName + '<a href = "/Post/TeachersInfoPage?userId=' + val.UserId + '"><span class ="glyphicon glyphicon-info-sign"></span></a></td> </tr>');
//                    } else {
//                        person += (val.FullName + '</td> </tr>');
//                    }
//                }
//                $("#graduates").append(person);
//            });
//            $('#graduates').fadeIn(2000);
//        },
//    });    
//};
function newYearSelected() {
    $('#graduates').fadeOut();
    $('#graduates').empty();
    $('#graduates').append('<th>Ім\'я</th>');
    $.ajax({
        url: '/Search/Changed?Year=' + $('#year').val(),
        type: 'post',
        dataType: 'json',
        success: function(data) {            
            $.each(data, function(key, val) {                
                if (val.IsRegistred) {
                    var person = '<tr><td><a href = "/Personal/Profile?username=';
                    if (val.IsTeacher) {
                        person += (val.Username + '">' + val.FullName + '</a> <a href = "/Post/TeachersInfoPage?userId= ' + val.UserId + '"><span align="right" class ="glyphicon glyphicon-info-sign"></span></a> </td> </tr>');
                    } else {
                        person += (val.Username + '">' + val.FullName + '</a></td> </tr>');
                    }
                } else {
                    var person = '<tr><td>';
                    if (val.IsTeacher) {
                        person += (val.FullName + '<a href = "/Post/TeachersInfoPage?userId=' + val.UserId + '"><span align="right" class ="glyphicon glyphicon-info-sign"></span></a></td> </tr>');
                    } else {
                        person += (val.FullName + '</td> </tr>');
                    }
                }
                $("#graduates").append(person);
            });
            $('#graduates').fadeIn(2000);
        },
    });    
};