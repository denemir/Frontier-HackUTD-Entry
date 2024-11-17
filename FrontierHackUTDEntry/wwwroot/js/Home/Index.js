$('#account-id-box').keypress(function (e) {
    if (e.which == 13) {
        console.log(e.currentTarget.value)
    }
});

$.ajax({
    url: 'api/Customers/GetAllCustomers',
    method: 'GET',
    success: function (response) {
        console.log('test');
    }
}).done(function (response) {
    console.log('test');
    console.log(response);
}).fail(function () {
        console.log("fail");
});