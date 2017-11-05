﻿﻿$(function () {

    $(".dropbtn").on("click", function () {
        $(".dropdown-content").toggle();
    });


    $(".table").on('click', '#joinGroup,#titles,#titlesImage', function () {
        var previousGrp = $('#grp').val();
        if (previousGrp != null) {
            chat.server.leaveGroup(previousGrp);
        }
        $('.message').val('');
        // get the current row
        var currentRow = $(this).closest("tr");

        var col1 = currentRow.find("td:eq(0)").text(); // get current row 1st TD value

        var data = col1.trim();



        $(function () {
            //Slide down to chatroom
            $('html,body').animate({
                scrollTop: $("#groupName").offset().top
            },
                'slow');
            //end Slide down
            chat.server.joinGroup(data);
            $('#inputForm').show();
            $('#grp').val(data);
            $('#chatroom').empty();
            chat.server.showHistory(data);
            //$('#chatroom').append('You Joined group' + ' ( ' + data + ' ) ' + '<br>' + new Date().toLocaleTimeString() + ' ');
            $('#groupName').html('<p>Вы в чате под именем :</p> ' + data);
        });
    });

    //Like group select
    $(".table").on('click', '#like', function () {
        // get the current row
        var currentRow = $(this).closest("tr");

        var col1 = currentRow.find("td:eq(0)").text(); // get current row 1st TD value

        var data = col1.trim();


        //if ($(this).text()=='Like') {
        //    $(this).text("Unlike");
        //} else {
        //    $(this).text("Like");
        //}





        chat.server.likes(data);
    });

    $('#chatBody').hide();
    $('#inputForm').hide();

    //$('#loginBlock').show();
    // Auto generated hub link
    var chat = $.connection.chatHub;


    //New Message Function
    chat.client.addMessage = function (name, message) {





        $('#chatroom').prepend('<div id ="userName"><p><b>' + htmlEncode(name)
            + '</b>' + ' : ' + htmlEncode(message) + ' <br> ' + '</p>' + '<p class ="small" id= "chatTime">' + new Date().toLocaleString() + '</p></div>');




        //$('.message').val('');



    };


    //New Message Function
    chat.client.AddLike = function (plusOrMinus, groupName) {

        $('table tr').each(function () {

            var x = $(this).find('td').eq(0).text();
            var y = x.trim();
            if (y == groupName) {

                //var el = parseInt($(this).find('td').eq(1).text());


                if (plusOrMinus == false) {
                    var el = parseInt($(this).find('td').eq(3).text());
                    //var div = $('#likecounter').text(el - 1);
                    //$(this).append('<td id="likecounter">' + div.text() + '</td>');
                    //$('#likecounter').text(el - 1);
                    //$(this).text(el + 1);
                    //$('.likes').remove();
                    var x = el - 1;
                    //$(this).append('<td class="likes">' + x + '</td>');
                    $(this).find('td').eq(3).text(x);
                    //$('td:eq(1)').append('<td class="likes">' + x + '</td>');
                    el = 0;

                }
                if (plusOrMinus == true) {
                    var el = parseInt($(this).find('td').eq(3).text());
                    //$('.likes').remove();
                    var y = el + 1;
                    $(this).find('td').eq(3).text(y);
                    //$(this).append('<td class="likes">' + y + '</td>');
                    //$('td:eq(1)').text(y);
                    el = 0;
                }










                //$('table tr').each(function () {
                //    if ($(this).find('td').eq(0).text() == groupName) {

                //        $('.likes').remove();

                //        $(this).append('<p class="likes">' + countTotal + '</p>');





                //var el = parseInt($('#likecounter').text());


                //if (plusOrMinus == false) {
                //    $('#likecounter').text(el - 1);
                //}
                //if (plusOrMinus == true) {
                //    $('#likecounter').text(el + 1);
                //}

            }
        }




        )
    };




    //Users Online
    chat.client.userGroupOnline = function (name, message) {



        $('#chatroom').append('<p id="userJoin">  <font size="3" color="red">&#10004</font>  ' + htmlEncode(name)
            + htmlEncode(message) + ' <br> </p>');





    };



    //Message Histry
    chat.client.addMessageHistory = function (name, message, date) {

        //$('#chatroom').append('<div id="historyUserName"><p>' + htmlEncode(name) +'</p></div><div>'
        //    + htmlEncode(message) + ' <br>' + '<p class ="small" id="chatTime">' + htmlEncode(date) + '</p></div>');

        //$('#chatroom').append('<div id ="userName"><p><b>' + htmlEncode(name)
        //    + ' : ' + '</b>' + htmlEncode(message) + ' <br> ' + '</p>' + '<p class ="small" id= "chatTime">' + new Date().toLocaleString() + '</p></div>');


        $('#chatroom').append('<span class="historyUserName"><p><b>' + htmlEncode(name)
            + '</b></span><span class="msg">' + ' : ' + htmlEncode(message) + ' <br> ' + '</p>' + '<p class ="small" id= "chatTime">' + htmlEncode(date) + '</p></span>');






    };


    // Clear Message History
    $('#clearHistory').click(function () {



        $('#chatroom').empty()
    });

    //Show Message History


    $('#showHistory').click(function () {

        //Slide down to chatroom
        $('html,body').animate({ scrollTop: $("#chatroom").offset().top }, 'slow');

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

    // Play new message click
    chat.client.playMusic = function () {
        $('#audio').get(0).play();

    };

    // Notification
    chat.client.showNotification = function (Group, Date, Name) {

        $("#notification").empty();

        $("#notification").fadeIn("slow").append('<span class="dismiss"><a title="Скрыть это уведомление">x</a></span>' + '<p>Новость </p>');



        $("#notification p").click(function () {
            $('html,body').animate({ scrollTop: $("#notificationfull").offset().top }, 'slow');
            $(".dropdown").hide();
            $(".usersonline").hide();
            $("#notification").hide();
            $("#notification").empty();


            $("#notificationfull").empty();
            $("#notificationfull").show();
            $("#notificationfull").append('<span class="dismiss"><a title="Скрыть это уведомление">x</a></span>').hide();
            $("#notificationfull").fadeIn("slow").append('<p><b><font color="#fb5e58">' + Name + '</font></b>' + ' ' + '<b> добавил(ла) новое сообщение в </b>' + '<p> ' + Group + '</p>' + '<p><small>' + Date + '</small></p>' + '</p>');

        });



        $("#notificationfull").click(function () {


            $("#notificationfull").empty();
            $("#notificationfull").hide();
            $(".dropdown").show();
            $(".usersonline").show();

        });

        $(".dismiss").click(function () {

            $("#notification").empty();
            $("#notification").hide();


            $("#notificationfull").hide();
            $("#notificationfull").empty();


        });

    };






    //Title Message Function
    chat.client.addHeader = function (name) {
        setInterval(function () {

            var title = document.title;

            document.title = (title == "Новое сообщение от" ? htmlEncode(name) : "Новое сообщение от");
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
        $('#header').html('Привет, ' + userName);


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
        $('#chatusers>p:contains("' + userName + '")').remove();


    }

    // Open connection
    $.connection.hub.start().done(function () {
        //Join grop
        //$(function () {
        //    chat.server.joinGroup(data);
        //});
        //$("#chatusers").append('<p><b>' + "Users Online"+'</b></p>');
        $('#sendmessage').click(function () {

            // Call send method from hub
            var date = new Date().toLocaleString();
            //var newD = new Date();

            chat.server.send($('#username').val(), $('.message').val(), $('#grp').val(), date);

            $('.message').val('');



        });
        $('#messagebox').keypress(function (e) {
            if (e.which == 13) {//Enter key pressed
                $('#sendmessage').click();//Trigger search button click event
            }
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

        $("#chatusers").append('<p><font size="1" color="green">&#x25CF</font>' + name + '</p>');
    }


}

$(document).ready(function () {

    $(document).on("click", "span.historyUserName>p>b, #userName>p>b", function () {

        var z = $(this).text();

        $('.message').val('@' + z + ' ');
        $('html,body').animate({ scrollTop: $("#chatBody").offset().top }, 'slow');

    });
    //$("#chatTime").click(function () {
    //    alert("Handler for .click() called.");
    //});

    //$(".historyUserName").on('click', '#joinGroup', function () {
    //}


})