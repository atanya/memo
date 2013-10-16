superMemo.EditCard = {
    
    viewModel: {},

    initEditForm: function () {
        $("#translationInput").autocomplete({
            source: function (request, response) {
                var res = "";
                var rus = "йцукенгшщзхъфывапролджэячсмитьбюЙЦУКЕНГШЩЗХЪФЫВАПРОЛДЖЭЯЧСМИТЬБЮ.,";
                var eng = "qwertyuiop[]asdfghjkl;'zxcvbnm,.QWERTYUIOP{}ASDFGHJKL:\"ZXCVBNM<>/?";
                for (var i = 0; i < request.term.length; i++) {
                    var pos = eng.indexOf(request.term[i]);
                    res += pos === -1 ? request.term[i] : rus[pos];
                }
                response([res]);
            },
            select: function (event, ui) {
                $(this).val(ui.item.value).change();
            }
        });
    },
    
    wordKeyPress: function(vm, e) {
        if (e.which == 13) {
            if (!vm.translation()) {
                $('#translationInput').focus();
            } else {
                superMemo.EditCard.saveCup();
            }
        }
        return true;
    },

    translationKeyPress: function (vm, e) {
        if (e.which == 13) {
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
        superMemo.EditCard.applyBindings(ko.mapping.fromJS(response));
        
    },

    onLoadFailureCallback: function (response) {
        alert("Fail");
    },
    
    loadCard: function (id) {
        if (id) {
            superMemo.proxy.loadCard(id, superMemo.EditCard.onLoadCardSuccessCallback, superMemo.EditCard.onLoadFailureCallback);
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