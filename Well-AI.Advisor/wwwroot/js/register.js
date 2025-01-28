function validCaptcha() {
    var flag = true;
    var message = 'Please check the checkbox';
    if (typeof (grecaptcha) != 'undefined') {
        var response = grecaptcha.getResponse();
        (response.length === 0) ? (message = 'Captcha verification failed') : (message = 'Success!');
    }
    $("#lblMessage").html(message);
    $("#lblMessage").css('color', (message.toLowerCase() == 'success!') ? "green" : "red");
    flag = ((message.toLowerCase() == 'success!') ? true : false);
    if (flag == false) {
        return false;
    }
};