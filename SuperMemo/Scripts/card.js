$(function () {
    var saveCup = function () {
        showMessage("");
        var word = $("#wordInput").val();
        var translaton = $("#translationInput").val();
        if (!checkValidation(word, translaton)) {
            return;
        }
        superMemo.proxy.saveCup(word, translaton, function(result) {
            showErrorMessage("Card saved");
            $("#wordInput").val("");
            $("#translationInput").val("");
        }, function(result) {
            alert("failure");
        });
    };

    var checkValidation = function (word, translation) {
        showMessage("#wordValidation", "");
        showMessage("#translationValidation", "");
        if (!word) {
            showMessage("#wordValidation", "Required");
        }
        if (!translation) {
            showMessage("#translationValidation", "Required");
        }
        return !!word && !!translation;
    };

    var showErrorMessage = function(message) {
        showMessage("#messageLabel", message);
    };

    var showMessage = function(control, message) {
        $(control).text(message);
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
    $("#saveButton").click(saveCup);
})