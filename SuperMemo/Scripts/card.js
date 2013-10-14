superMemo.Card = {
    saveCup: function() {
        superMemo.Card.showMessage("");
        var word = $("#wordInput").val();
        var translaton = $("#translationInput").val();
        var id = $("#WordID").val();
        if (!superMemo.Card.checkValidation(word, translaton)) {
            return;
        }
        superMemo.proxy.saveCup(id, word, translaton, function(result) {
            superMemo.Card.showErrorMessage("Card saved");
            $("#wordInput").val("");
            $("#translationInput").val("");
        }, function(result) {
            alert("failure");
        });
    },

    checkValidation: function(word, translation) {
        superMemo.Card.showMessage("#wordValidation", "");
        superMemo.Card.showMessage("#translationValidation", "");
        if (!word) {
            superMemo.Card.showMessage("#wordValidation", "Required");
        }
        if (!translation) {
            superMemo.Card.showMessage("#translationValidation", "Required");
        }
        return !!word && !!translation;
    },

    showErrorMessage: function(message) {
        superMemo.Card.showMessage("#messageLabel", message);
    },

    showMessage: function(control, message) {
        $(control).text(message);
    },

    viewModel: {
        cards: [],
        
        getCardID: function(targetElementInRow) {
            return $(targetElementInRow).closest("tr").attr("data_id");
        },

        removeCup: function(func, event) {
            if (confirm("Are you sure?")) {
                var id = superMemo.Card.viewModel.getCardID(event.currentTarget);
                superMemo.proxy.removeCup(id, function(result) {
                    location.reload();
                }, function(result) {
                    alert("failure");
                });
            }
        },

        updateCup: function(func, event) {
            var id = superMemo.Card.viewModel.getCardID(event.currentTarget);
            window.location = "Card/Edit/" + id;
        }/*,

        addCup: function() {
            window.location = "Create";
        }*/
    },

    onLoadSuccessCallback: function(response) {
        superMemo.Card.viewModel.cards = response;
        ko.applyBindings(superMemo.Card.viewModel, document.getElementById('cardList'));
    },

    onLoadCardSuccessCallback: function (response) {
        $("#wordInput").val(response.word);
        $("#translationInput").val(response.translation);
    },

    onLoadFailureCallback: function(response) {
        alert("Fail");
    },

    load: function() {
        superMemo.proxy.loadList(superMemo.Card.onLoadSuccessCallback, superMemo.Card.onLoadFailureCallback);
    },

    loadCard: function (id) {
        superMemo.proxy.loadCard(id, superMemo.Card.onLoadCardSuccessCallback, superMemo.Card.onLoadFailureCallback);
    }
};
superMemo.Card.load();