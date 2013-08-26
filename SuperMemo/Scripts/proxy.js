window.superMemo.proxy = {
    sendAjaxRequest : function(url, type, data, onSuccessCallback, onFailureCallback) {
        var jsonRequest = data ? JSON.stringify(data) : '';
        $.ajax({
            url: url,
            type: type,
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
        //this.sendAjaxRequest(window.superMemo.urls.saveCup, 'POST', data, onSuccessCallback, onFailureCallback);
        this.sendAjaxRequest('/SuperMemo/api/main', 'POST', data, onSuccessCallback, onFailureCallback);
    },
    removeCup: function (id, onSuccessCallback, onFailureCallback) {
        //var data = {
        //    id: id
        //};
        //this.sendAjaxRequest(window.superMemo.urls.removeCup, 'POST', data, onSuccessCallback, onFailureCallback);
        this.sendAjaxRequest('/SuperMemo/api/main/' + id, 'DELETE', null, onSuccessCallback, onFailureCallback);
    },
    
    getNextWord: function (onSuccessCallback, onFailureCallback) {
        //this.sendAjaxRequest(window.superMemo.urls.getNextWord, 'POST', data, onSuccessCallback, onFailureCallback);
        this.sendAjaxRequest('/SuperMemo/api/training', 'GET', null, onSuccessCallback, onFailureCallback);
    },
    
    sendAnswer: function (id, answer, onSuccessCallback, onFailureCallback) {
        var data = {
            Answer: answer
        };
        //this.sendAjaxRequest(window.superMemo.urls.getNextWord, 'POST', data, onSuccessCallback, onFailureCallback);
        this.sendAjaxRequest('/SuperMemo/api/training/' + id, 'PUT', answer, onSuccessCallback, onFailureCallback);
    },
    
    loadList: function (onSuccessCallback, onFailureCallback) {
        this.sendAjaxRequest('/SuperMemo/api/main', 'GET', null, onSuccessCallback, onFailureCallback);
    },

    loadCard: function (id, onSuccessCallback, onFailureCallback) {
        this.sendAjaxRequest('/SuperMemo/api/main/' + id, 'GET', null, onSuccessCallback, onFailureCallback);
    }
};