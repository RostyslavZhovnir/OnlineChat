$(document).ready(function () {
    $('.table').show();
    $('.hideArticles').show();

    //$('.seeArticles').click(function () {
    //    $('.table').show("fast")
    //    $('.hideArticles').show()
    //});

    $('.hideArticles').click(function () {
        $('.table').hide("fast")
        $('.hideArticles').hide();
    });
});