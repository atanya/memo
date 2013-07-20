$(function () {
    var saveCup = function () {
        var word = $("#wordInput").val();
        var translaton = $("#translationInput").val();
        superMemo.proxy.saveCup(word, translaton, function(result) {
            alert("success");
        }, function(result) {
            alert("failure");
        });
    };
    $("#saveButton").click(saveCup);
})