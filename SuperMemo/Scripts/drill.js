$(function () {
    var viewModel = {
        currentWord: ko.observable(),
        currentTrans: ko.observable(),
        totalWords: ko.observable(),
        numberToRepeat: ko.observable()
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

    var sendAnswer = function(event) {
        getNextWord(event.data);
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

    var getNextWord = function(answer) {
        superMemo.proxy.getNextWord(answer, function (result) {
            bindData(result);
        }, function (result) {
            alert("failure");
        });
    };

    var bindData = function (model) {
        viewModel.currentWord(model.Word);
        viewModel.currentTrans(model.Translation);
        viewModel.totalWords(model.TotalWords);
        viewModel.numberToRepeat(model.LeftWords);
    };
    


    init();
})