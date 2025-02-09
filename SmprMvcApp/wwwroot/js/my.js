setTimeout(function () {
    var successAlert = document.getElementById('successAlertId');
    if (successAlert) {
        var alert = new bootstrap.Alert(successAlert);
        alert.close();
    }
}, 3000);

setTimeout(function () {
    var errorAlert = document.getElementById('errorAlertId');
    if (errorAlert) {
        var alert = new bootstrap.Alert(errorAlert);
        alert.close();
    }
}, 3000);