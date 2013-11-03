// The onClicked callback function.



chrome.runtime.onInstalled.addListener(function() {
var onClickHandler =function onClickHandler(info, tab) { 
alert(1);
  if (info.menuItemId == "contextselection") {
  alert(info.toString());
  var selectedWord = 'cup';//window.getSelection().toString().trim();
	alert(selectedWord);
        if (selectedWord) {
            var urlWithValue = 'http://supermemo.apphb.com/api/Translator/' + selectedWord;
			try{
				$.ajax({
					url: urlWithValue,
					type: "GET",
					dataType: "json",
					contentType: "application/json; charset=utf-8",
					success: function (data) {
						alert(data.data);
					},
					error: function (data) {
						alert("request failed");
					}
				});
			}
			catch(exp){
				alert(exp);
			}
        }
	}
};
  var context = "selection";
    var title = "Translate with SuperMemo";
    chrome.contextMenus.create({"title": title, "contexts":[context],
                                         "id": "contextselection"}, onClickHandler);
});
