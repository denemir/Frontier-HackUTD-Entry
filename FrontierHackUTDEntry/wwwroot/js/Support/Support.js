var botui = new BotUI('my-botui-app');

$(document).ready(function () {
    botui.message.add({
        content: 'Hello! How can I assist you today?',
        delay: 1000,
        loading: true
    });
});

$('#chat').keypress(function (e) {
    if (e.which == 13) {
        var msg = e.currentTarget.value;
        sendMessage(msg);
        $.ajax({
            url: '/api/Chatbot/SendMessage',
            contentType: "application/x-www-form-urlencoded",
            method: 'POST',
            data: { userMessage: msg },
        }).done(function (response) {
            receiveMessage(response);
        });
    }
});

function sendMessage(input) {
    botui.message.add({
        content: input,
        human: true
    });
    $('#chat').val('');
}

function receiveMessage(response) {
    botui.message.add({
        content: response,
        delay: 1000,
        loading: true
    });
}