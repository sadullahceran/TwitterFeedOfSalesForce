$(function () {
    $("#salesforceProfile").fadeTo('medium', 0);
    $.get(
            baseUrl + "Feed/ProfileInfo",
            function (data) {
                $("#salesforceProfile").html(data).fadeTo('medium', 1);

            },
            "html"
        );
});

function loadTweets() {
    $("#tweets").fadeTo('medium', 0);
    $.get(
            baseUrl + "Feed/GetTweets",
            function (data) {
                $("#tweets").html(data).fadeTo('medium', 1);
            },
            "html"
        );
}

$(function () {
    loadTweets();
    setInterval(loadTweets, 60 * 1000);
});

$(function () {

    $("form").submit(function (e) {
        e.preventDefault();
        var searchText = $('#txtSearch').val();

        $(".tweet .tweet-text").removeClass("slds-text-color--error");

        $('.tweet').each(function () {
            var textElement = $('.tweet-text', $(this));
            var text = textElement.text();

            var string = "Stackoverflow is the BEST";
            var result = text.match(new RegExp(searchText, "i"));

            if (result) {
                textElement.addClass('slds-text-color--error');

                if (!$(this).is(':visible'))
                    $(this).fadeIn();
            } else {
                $(this).fadeOut();
            }
        });
    });
})


$(function () {

    $("#btnClear").click(function (e) {

        $(".tweet .tweet-text").removeClass("slds-text-color--error");
        $('.tweet:hidden').fadeIn();
    });
})