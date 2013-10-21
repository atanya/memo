superMemo.User = {
    init: function() {
        $("#changeAlert").hide();
        $("#updateUser").on("click", superMemo.User.update);
    },

    onUpdateSuccessCallback: function(response) {
        $("#changeAlert").show();
    },

    onUpdateCompleteCallback: function(response) {
        window.waiter.hide({ targetId: 'body' });
    },

    update: function() {
        $("#oldContainer").removeClass("has-error");
        $("#newContainer").removeClass("has-error");
        var oldPassword = $("#oldPassword").val();
        var newPassword = $("#newPassword").val();
        if (!oldPassword) {
            $("#oldContainer").addClass("has-error");
        }
        if (!newPassword) {
            $("#newContainer").addClass("has-error");
        }
        if (!oldPassword || !newPassword) {
            return;
        }
        window.waiter.show({ targetId: 'body' });
        superMemo.proxy.updateUser(oldPassword, newPassword, superMemo.User.onUpdateSuccessCallback, superMemo.User.onUpdateCompleteCallback);
    }
};