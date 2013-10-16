﻿superMemo.Card = {
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
        }
    },

    onLoadSuccessCallback: function(response) {
        superMemo.Card.viewModel.cards = response;
        ko.applyBindings(superMemo.Card.viewModel, document.getElementById('cardList'));
    },

    onLoadFailureCallback: function(response) {
        alert("Fail");
    },

    load: function() {
        superMemo.proxy.loadList(superMemo.Card.onLoadSuccessCallback, superMemo.Card.onLoadFailureCallback);
    }
};
superMemo.Card.load();