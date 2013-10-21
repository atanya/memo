superMemo.Card = {
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
                });
            }
        },

        updateCup: function(func, event) {
            var id = superMemo.Card.viewModel.getCardID(event.currentTarget);
            window.location = superMemo.urls.editCard + "/" + id;
        }
    },

    onLoadSuccessCallback: function(response) {
        superMemo.Card.viewModel.cards = response.data;
        ko.applyBindings(superMemo.Card.viewModel, document.getElementById('cardList'));
    },

    onLoadCompleteCallback: function (response) {
        window.waiter.hide({ targetId: 'body' });
    },

    load: function () {
        window.waiter.show({ targetId: 'body' });
        superMemo.proxy.loadList(superMemo.Card.onLoadSuccessCallback, superMemo.Card.onLoadCompleteCallback);
    }
};
superMemo.Card.load();