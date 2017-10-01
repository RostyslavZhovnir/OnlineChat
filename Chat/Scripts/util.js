$(function () {
    


    $(".table").on('click', '#joinGroup', function () {
        // get the current row
        var currentRow = $(this).closest("tr");

        var col1 = currentRow.find("td:eq(0)").text(); // get current row 1st TD value

        var data = col1.trim();



        $(function () {

            chat.server.joinGroup(data);
            $('#inputForm').show();
            $('#grp').val(data);
            $('#chatroom').empty();
            //$('#chatroom').append('You Joined group' + ' ( ' + data + ' ) ' + '<br>' + new Date().toLocaleTimeString() + ' ');
            $('#groupName').html('You join group: ' + data);
        });
    });

    //Like group select
    $(".table").on('click', '#like', function () {
        // get the current row
        var currentRow = $(this).closest("tr");

        var col1 = currentRow.find("td:eq(0)").text(); // get current row 1st TD value

        var data = col1.trim();

     
        if ($(this).text()=='Like') {
            $(this).text("Unlike");
        } else {
            $(this).text("Like");
        }

     



        chat.server.likes(data);
    });

    $('#chatBody').hide();
    $('#inputForm').hide();

    //$('#loginBlock').show();
    // Auto generated hub link
    var chat = $.connection.chatHub;


    //New Message Function
    chat.client.addMessage = function (name, message) {



        $('#chatroom').append('<p><b id ="userName">' + htmlEncode(name)
            + ':</b> ' + htmlEncode(message) + ' <br> ' + '</p>' + '<p class ="small" id= "chatTime"><i>' + new Date().toLocaleString() + '</i></p>');


        $('#message').val('');


    };

   


    //New Message Function
    chat.client.userGroupOnline = function (name, message) {



        $('#chatroom').append('<p><b>' + htmlEncode(name)
            + htmlEncode(message) + ' <br> </p>');


        $('#message').val('');


    };



    //New Message Function
    chat.client.addMessageHistory = function (name, message, date) {



        $('#chatroom').append('<p><i> ' + htmlEncode(name)
            + htmlEncode(message) + ' <br> </i></p>' + '<p class ="small" id="chatTime"><i>' + htmlEncode(date) + '</i></p>');


        $('#message').val('');


    };


    // Clear Message History
    $('#clearHistory').click(function () {
        $('#chatroom').empty()
    });

    //Show Message History


    $('#showHistory').click(function () {
        var x = $('#grp').val();
        //chat.server.ShowHistory(x);
        //chat.server.joinGroup(x);
        $('#chatroom').empty();
        chat.server.showHistory(x);


    });


    // Clear ErrorMessage
    chat.client.clearError = function () {
        $('#emptymessage').empty();





    };


    ////Show likes in groups
    //chat.client.Likes = function (alreadyCalled, groupName) {


    //};

        
      
           
       

        

    



    //Title Message Function
    chat.client.addHeader = function (name) {
        setInterval(function () {

            var title = document.title;

            document.title = (title == "New message from" ? htmlEncode(name) : "New message from");
        }, 4000);


    };


    //Empty Message Error
    chat.client.errorEmptyMessage = function (name, message) {


        $('#emptymessage').css('color', 'red').append('</br>' + htmlEncode(message));

    };

    //chat.client.UpdateCounter = function (counter) {
    //    $('#all').text("Online  " + counter);
    //};

    // New User Function
    chat.client.onConnected = function (id, userName, allUsers) {


        //$('#loginBlock').hide();
        $('#chatBody').show();
        // hiden tags set
        $('#hdId').val(id);

        $('#username').val(userName);
        $('#header').html('Hello ' + userName);


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
            var date = new Date().toLocaleString();
            //var newD = new Date();
            chat.server.send($('#username').val(), $('#message').val(), $('#grp').val(), date);





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

        $("#chatusers").append('<p><img src="../Images/online.png" />' + '  <b>  ' + name + '</b></p>');
    }

}