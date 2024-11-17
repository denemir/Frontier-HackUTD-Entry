$.ajax({
    url: 'api/Customers/GetCustomerById',
    method: 'GET',
    data: { id: '000060be4aa292815abc44ab6fe96015b89e83b21c8a63473ee216fd67998a99' },
    success: function (response) {
        console.log('test');
    }
}).done(function (response) {
    console.log('test');
    console.log(response);
}).fail(function () {
    console.log("fail");
});