$('#chat').keypress(function (e) {
    if (e.which == 13) {
        $.ajax({
            url: '/api/Chatbot/SendMessage',
            contentType: "application/x-www-form-urlencoded",
            method: 'POST',
            data: { userMessage: e.currentTarget.value },
        }).done(function (response) {
            $('#chat').val('');
        });
    }
});