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

    var viewModel = {
        removeCup: function(func, event) {
            var word = $(event.currentTarget).closest("tr").find("td:first-child").text();
            superMemo.proxy.removeCup(word, function(result) {
                alert("success");
                location.reload();
            }, function(result) {
                alert("failure");
            });
        }
    };
    ko.applyBindings(viewModel, document.getElementById('cardList'));

    $("#saveButton").click(saveCup);
    //$("#removeButton").click(removeCup);
})