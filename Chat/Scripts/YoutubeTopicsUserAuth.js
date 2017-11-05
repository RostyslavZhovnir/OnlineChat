

$(document).ready(function () {


    $('#table #linksource:contains("youtube.com/watch")').each(function (i) {

        var txt = $(this).text().trim();
        var yt_id = txt.split('?v=')[1];




        //var currentRow = $(this).closest("tr");
        //currentRow.find("td:eq(0)").replaceWith('<iframe class="youtube-frame" width="370" height="210" allowfullscreen src="http://www.youtube.com/embed/' + yt_id + '"></iframe>');

        //var currentRow = $(this).closest("tr");
        var currentRow = $(this).closest("tr");
        currentRow.find("td:eq(0)").empty();
        currentRow.find("td:eq(0)").css({ "border-top": "none" });
        currentRow.find("td:eq(0)").append('<iframe class="youtube-frame" width="370" height="210" allowfullscreen src="http://www.youtube.com/embed/' + yt_id + '"></iframe>');

       
    })
});