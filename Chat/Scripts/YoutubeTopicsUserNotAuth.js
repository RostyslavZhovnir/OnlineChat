

$(document).ready(function () {


    $('#tablelogoff #linksource:contains("youtube.com/watch")').each(function (i) {

        var txt = $(this).text().trim();


        var yt_id = txt.split('?v=')[1];

        //$(this).replaceWith('<iframe class="youtube-frame" src="http://www.youtube.com/embed/' + yt_id + '"></iframe>');

        $(this).replaceWith('<iframe class="youtube-frame" width="370" height="210" allowfullscreen src="http://www.youtube.com/embed/' + yt_id + '"></iframe>');

       
    })
});