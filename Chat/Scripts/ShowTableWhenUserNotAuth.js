$(document).ready(function () {
    $('.table').hide();
    $('.hideArticles').hide();

    $('.seeArticles').click(function () {
        $('.table').show("fast")
        $('.hideArticles').show()
    });

    $('.hideArticles').click(function () {
        $('.table').hide("fast")
        $('.hideArticles').hide();
    });
});