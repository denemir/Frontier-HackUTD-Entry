$('#account-id-box').keypress(function (e) {
    if (e.which == 13) {
        console.log(e.currentTarget.value)
    }
});