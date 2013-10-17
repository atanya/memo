$(function () {
    var viewModel = {
        wordID: ko.observable(),
        currentWord: ko.observable(),
        currentTrans: ko.observable(),
        totalWords: ko.observable(),
        numberToRepeat: ko.observable(),
        
        editCard: function() {
            window.location = superMemo.urls.editCard + "/" + this.wordID();
        }
    };

    var init = function () {
        bindButtons();
        getNextWord();
        ko.applyBindings(viewModel, document.getElementById('drillPage'));
    };

    var bindButtons = function() {
        $("#againBtn").click(0, sendAnswer);
        $("#badBtn").click(3, sendAnswer);
        $("#goodBtn").click(4, sendAnswer);
        $("#perfectBtn").click(5, sendAnswer);

        $("#answerLink").click(true, showAnswer);
    };

    var sendAnswer = function (event) {
        var wordId = viewModel.wordID();
        superMemo.proxy.sendAnswer(wordId, event.data, function(result) {
            getNextWord();
        }, function(result) {
            alert("failure");
        });
    };

    var showAnswer = function(show) {
        if (show) {
            $("#answerLink").hide();
            $("#transPlace").show();
        } else {
            $("#answerLink").show();
            $("#transPlace").hide();
        }
    };

    var getNextWord = function () {
        showAnswer(false);
        window.waiter.show({ targetId: 'body' });
        superMemo.proxy.getNextWord(function (result) {
            bindData(result);
        }, function (result) {
            alert("failure");
        }, function() {
            window.waiter.hide({ targetId: 'body' });
        });
    };

    var bindData = function (model) {
        viewModel.wordID(model.id);
        viewModel.currentWord(model.word);
        viewModel.currentTrans(model.translation);
        viewModel.totalWords(model.total);
        viewModel.numberToRepeat(model.wordsForToday);
    };
    


    init();
})