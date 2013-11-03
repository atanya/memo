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
			 chrome.windows.create({
                url : "superMemoPopup.html?" + data.data,
                type: 'popup',
                focused: true,
				width: 200,
				height: 200
            });
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
/*chrome.runtime.onMessage.addListener(function(request) {
    if (request.type === 'supermemo_translate') {
        chrome.tabs.create({
            url: chrome.extension.getURL('superMemoPopup.html'),
            active: false
        }, function(tab) {
            // After the tab has been created, open a window to inject the tab
            chrome.windows.create({
                tabId: tab.id,
                type: 'popup',
                focused: true,
				width: 200,
				height: 200
            });
        });
    }
});*/