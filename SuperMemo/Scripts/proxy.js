window.superMemo.proxy = {
    
    techError: "Technical error",
    
    sendAjaxRequest : function(url, type, data, onSuccessCallback, onFailureCallback, onCompleteCallback) {
        var jsonRequest = data ? JSON.stringify(data) : '';
        $.ajax({
            url: url,
            type: type,
            data: jsonRequest,
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            success: function (response) { superMemo.proxy.onSuccess(response, onSuccessCallback) },
            error: this.onFailure,
            complete: onCompleteCallback,
            statusCode: {
                401: function () { //Unauthorized
                    window.location.href = window.movieService.environment.baseUrl;
                }
            }
        });
    },
    
    onFailure: function() {
        superMemo.proxy.showError(techError);
    },
    
    onSuccess: function (response, onSuccessCallback) {
        if (!response || !response.status) {
            var error = response ? response.data || techError : techError;
            superMemo.proxy.showError(error);
        } else {
            onSuccessCallback(response);
        }
    },
    
    showError: function(message) {
        $("#errorText").text(message);
        $("#errorDiv").show();
    },
    
    saveCup: function (id, word, translation, onSuccessCallback, onFailureCallback) {
        var data = {
            Id: id,
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
    
    getNextWord: function (onSuccessCallback, onCompleteCallback) {
        //this.sendAjaxRequest(window.superMemo.urls.getNextWord, 'POST', data, onSuccessCallback, onFailureCallback);
        this.sendAjaxRequest('/SuperMemo/api/training', 'GET', null, onSuccessCallback, null, onCompleteCallback);
    },
    
    sendAnswer: function (id, answer, onSuccessCallback, onFailureCallback) {
        var data = {
            Answer: answer
        };
        //this.sendAjaxRequest(window.superMemo.urls.getNextWord, 'POST', data, onSuccessCallback, onFailureCallback);
        this.sendAjaxRequest('/SuperMemo/api/training/' + id, 'PUT', answer, onSuccessCallback, onFailureCallback);
    },
    
    loadList: function (onSuccessCallback, onCompleteCallback) {
        this.sendAjaxRequest('/SuperMemo/api/main', 'GET', null, onSuccessCallback, null, onCompleteCallback);
    },

    loadCard: function (id, onSuccessCallback, onCompleteCallback) {
        this.sendAjaxRequest('/SuperMemo/api/main/' + id, 'GET', null, onSuccessCallback, null, onCompleteCallback);
    },
    
    getTranslation: function (word, onSuccessCallback, onFailureCallback) {
        this.sendAjaxRequest('/SuperMemo/api/translator/' + word, 'GET', null, onSuccessCallback, onFailureCallback);
    },
};