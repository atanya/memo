superMemo.EditCard = {
    
    viewModel: {},

    initEditForm: function () {
        $("#translationInput").autocomplete({
            source: function (request, response) {
                var self = superMemo.EditCard;
                var translitted = self.translitString(request.term);
                if (self.viewModel.word()) {
                    superMemo.proxy.getTranslation(self.viewModel.word(), function(result) { self.processAutocomplete(result.data, response, translitted); }, function(result) { alert(result); });
                } else {
                    response([translitted]);
                }
            },
            minLength: 0,
            select: function (event, ui) {
                $(this).val(ui.item.value).change();
            }
        });
    },
    
    translitString: function(str) {
        var result = "";
        var rus = "йцукенгшщзхъфывапролджэячсмитьбюЙЦУКЕНГШЩЗХЪФЫВАПРОЛДЖЭЯЧСМИТЬБЮ.,";
        var eng = "qwertyuiop[]asdfghjkl;'zxcvbnm,.QWERTYUIOP{}ASDFGHJKL:\"ZXCVBNM<>/?";
        for (var i = 0; i < str.length; i++) {
            var pos = eng.indexOf(str[i]);
            result += pos === -1 ? str[i] : rus[pos];
        }
        return result;
    },
    
    processAutocomplete: function(data, callback, translit) {
        data.splice(0, 0, translit);
        callback(data);
    },
    
    wordKeyPress: function(vm, e) {
        if (e.which == 13) { //on Enter
            if (!vm.translation()) {
                $('#translationInput').focus();
            } else {
                superMemo.EditCard.saveCup();
            }
        }
        return true;
    },

    translationKeyPress: function (vm, e) {
        if (e.which == 13) { //on Enter
            superMemo.EditCard.saveCup();
        }
        return true;
    },

    saveCup: function () {
        superMemo.EditCard.showMessage("");
        var vm = superMemo.EditCard.viewModel;
        if (!superMemo.EditCard.checkValidation(vm.word(), vm.translation())) {
            return;
        }
        superMemo.proxy.saveCup(vm.id(), vm.word(), vm.translation(), function (result) {
            superMemo.EditCard.showErrorMessage("Card saved");
            vm.word("");
            vm.translation("");
        }, function (result) {
            alert("failure");
        });
    },

    checkValidation: function (word, translation) {
        superMemo.EditCard.showMessage("#wordValidation", "");
        superMemo.EditCard.showMessage("#translationValidation", "");
        if (!word) {
            superMemo.EditCard.showMessage("#wordValidation", "Required");
        }
        if (!translation) {
            superMemo.EditCard.showMessage("#translationValidation", "Required");
        }
        return !!word && !!translation;
    },

    showErrorMessage: function (message) {
        superMemo.EditCard.showMessage("#messageLabel", message);
    },

    showMessage: function (control, message) {
        $(control).text(message);
    },
    
    onLoadCardSuccessCallback: function (response) {
        superMemo.EditCard.applyBindings(ko.mapping.fromJS(response.data));
        
    },

    onLoadFailureCallback: function (response) {
        alert("Fail");
    },

    onLoadCompleteCallback: function (response) {
        window.waiter.hide({ targetId: 'body' });
    },
    
    loadCard: function (id) {
        if (id) {
            window.waiter.show({ targetId: 'body' });
            superMemo.proxy.loadCard(id, superMemo.EditCard.onLoadCardSuccessCallback, superMemo.EditCard.onLoadFailureCallback, superMemo.EditCard.onLoadCompleteCallback);
        } else {
            superMemo.EditCard.applyBindings(superMemo.EditCard.defaultViewModel());
        }
        
    },
    
    defaultViewModel: function() {
        return {
            word: ko.observable(""),
            translation: ko.observable(""),
            id: ko.observable("")
        };
    },
    
    applyBindings: function (viewModel) {
        var self = superMemo.EditCard;
        self.viewModel = viewModel;
        self.viewModel.saveCard = self.saveCup;
        self.viewModel.wordKeyPress = self.wordKeyPress;
        ko.applyBindings(superMemo.EditCard.viewModel);
        $("#wordInput").focus();
    }

}