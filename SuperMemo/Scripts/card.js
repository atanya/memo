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
        removeCup: function (func, event) {
            if (confirm("Are you sure?")) {
                var word = $(event.currentTarget).closest("tr").find("td:first-child").text();
                superMemo.proxy.removeCup(word, function(result) {
                    alert("success");
                    location.reload();
                }, function(result) {
                    alert("failure");
                });
            }
        },
        
        updateCup: function(func, event) {
            var word = $(event.currentTarget).closest("tr").find("td:first-child").text();
            window.location = "Edit" + "?word=" + word;
        },
        
        addCup: function() {
            window.location = "Create";
        }
    };
    ko.applyBindings(viewModel, document.getElementById('cardList'));

    $("#saveButton").click(saveCup);
})