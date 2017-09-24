$(function () {

    $(".table").on('click', '#btnSelect', function () {
        // get the current row
        var currentRow = $(this).closest("tr");

        var col1 = currentRow.find("td:eq(0)").text(); // get current row 1st TD value

        var data = col1;

        alert(data);

        $(function () {
            chat.server.joinGroup(data);
            $('#inputForm').show();
            $('#grp').val(data);
            $('#chatroom').empty();
            $('#chatroom').append('You Joined group'+' ( '+ data + ' ) ' +'  '+ new Date().toLocaleTimeString() + '</p>');
        });
    });

    $('#chatBody').hide();
    $('#inputForm').hide();

    //$('#loginBlock').show();
    // Auto generated hub link
    var chat = $.connection.chatHub;


    //New Message Function
    chat.client.addMessage = function (name, message) {



        $('#chatroom').append('<p><b>' + htmlEncode(name)
            + '</b>: ' + htmlEncode(message) + '  ' + new Date().toLocaleTimeString() + '</p>');
        $('#message').val("");



    };
    // Clear ErrorMessage

    chat.client.clearError = function () {

        $('#emptymessage').empty();





    };






    //Title Message Function
    chat.client.addHeader = function (name) {
        setInterval(function () {
            document.title = "New message from " + htmlEncode(name);
        }, 3000);
    };


    //Empty Message Error
    chat.client.errorEmptyMessage = function (name, message) {


        $('#emptymessage').css('color', 'red').append('</br>' + htmlEncode(message));

    };

    chat.client.UpdateCounter = function (counter) {
        $('#all').text("Online  " + counter);
    };

    // New User Function
    chat.client.onConnected = function (id, userName, allUsers) {


        //$('#loginBlock').hide();
        $('#chatBody').show();
        // hiden tags set
        $('#hdId').val(id);

        $('#username').val(userName);
        $('#header').html('<h4>Hello ' + userName + '</h4>');


        for (i = 0; i < allUsers.length; i++) {

            AddUser(allUsers[i].ConnectionId, allUsers[i].UserName);
        }


    }

    // Add new user
    chat.client.onNewUserConnected = function (id, name) {

        AddUser(id, name);

    }

    // Delete user
    chat.client.onUserDisconnected = function (id, userName) {

        $('#' + id).remove();
    }

    // Open connection
    $.connection.hub.start().done(function () {
        //Join grop
        //$(function () {
        //    chat.server.joinGroup(data);
        //});

        $('#sendmessage').click(function () {

            // Call send method from hub
            chat.server.send($('#username').val(), $('#message').val(), $('#grp').val());

            $('#message').val("");


        });

        var name = $("#txtUserName").val();
        chat.server.connect(name);

    });
});
// Tag coding
function htmlEncode(value) {
    var encodedValue = $('<div />').text(value).html();
    return encodedValue;
}
//New User
function AddUser(id, name) {

    var userId = $('#hdId').val();

    if (userId != id) {

        $("#chatusers").append('<p id="' + id + '"><b>' + name + '</b></p>');
    }

}