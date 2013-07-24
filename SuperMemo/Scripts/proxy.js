window.superMemo.proxy = {
    sendAjaxRequest : function(url, data, onSuccessCallback, onFailureCallback) {
        var jsonRequest = JSON.stringify(data);
        $.ajax({
            url: url,
            type: 'POST',
            data: jsonRequest,
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            success: onSuccessCallback,
            error: onFailureCallback
        });
    },
    saveCup: function (word, translation, onSuccessCallback, onFailureCallback) {
        var data = {
            Word: word,
            Translation: translation
        };
        this.sendAjaxRequest(window.superMemo.urls.saveCup, data, onSuccessCallback, onFailureCallback);
    },
    removeCup: function (word, onSuccessCallback, onFailureCallback) {
        var data = {
            Word: word
        };
        this.sendAjaxRequest(window.superMemo.urls.removeCup, data, onSuccessCallback, onFailureCallback);
    }
};