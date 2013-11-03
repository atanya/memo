// The onClicked callback function.

function onClickHandler(info, tab) {
  if (info.menuItemId == "contextselection") {
  var selectedWord = info.selectionText? info.selectionText.trim(): null;
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


chrome.contextMenus.create({"title": "Translate with SuperMemo", "contexts":["selection"],
                    "id": "contextselection"});      

chrome.contextMenus.onClicked.addListener(onClickHandler);					
