/* Salesforce profile div is ajax-loaded to increase performance */
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

// Fetchs tweet from server 
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

// Fetch tweets periodically for every 60 seconds.
$(function () {
    loadTweets();
    setInterval(loadTweets, 60 * 1000);
});

// When search button clicked, every tweet is traversed and given keyword is searched
// @TODO: Only accepts single keyword, multiple combinations may also be searched
$(function() {
    $("#frmSearch").submit(function(e) {
        e.preventDefault();
        var searchText = $('#txtSearch').val();

        // First, remove previously found strings
        $(".tweet .tweet-text").removeClass("slds-text-color--error");


        $('.tweet').each(function() {
            var textElement = $('.tweet-text', $(this));
            var text = textElement.text();

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
});

// Clear button reverses all tweets to original state
$(function () {
    $("#btnClear").click(function (e) {
        $(".tweet .tweet-text").removeClass("slds-text-color--error");
        $('.tweet:hidden').fadeIn();
    });
})