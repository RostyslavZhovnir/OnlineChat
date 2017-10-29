$(document).ready(function () {
    $('.table').show();
    $('.hideArticles').show();
    $('.seeArticles').hide();

    $('.seeArticles').click(function () {
        $('.table').show("fast")
        $('.hideArticles').show()
        $('.seeArticles').hide();
    });

    $('.hideArticles').click(function () {
        $('.table').hide("fast")
        $('.hideArticles').hide();
        $('.seeArticles').show();
    });
});