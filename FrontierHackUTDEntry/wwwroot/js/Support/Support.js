$('#chat').keypress(function (e) {
    console.log('test');
    if (e.which == 13) {
        console.log('test 1')
        $.ajax({
            url: 'api/Chatbot/SendMessage',
            method: 'POST',
            data: { userMessage: JSON.stringify(e.currentTarget.value) },
            success: function (response) {
                console.log('test 2');
            }
        }).done(function (response) {
            e.currentTarget.value = '';
            console.log(response);
        }).fail(function (e) {
            console.log(e);
        });
    }
});

console.log($('#chat'));