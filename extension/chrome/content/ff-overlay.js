var mysupermemo = {
    onLoad: function() {
        // initialization code
        this.initialized = true;
        this.strings = document.getElementById("mysupermemo-strings");
    },
    onMenuItemCommand: function(event) {
        var selectedWord = 'cup';//content.getSelection().toString().trim();
        if (selectedWord) {
            var urlWIthValue = 'http://mysupermemo.com/api/Translator/' + selectedWord;
            alert(urlWIthValue);
            $.ajax({
                url: urlWIthValue,
                type: "GET",
                dataType: "jsonp",
                contentType: "application/json; charset=utf-8",
                beforeSend: setHeader,
                complete: function (xhr, textStatus) {
                    alert('c' + textStatus);
                }
                //success: function (data) {
                //    alert(1);
                //    alert(data);
                //    //mysupermemo.showFirefoxContextMenu(data, selectedWord.anchorNode.parentElement);
                //},
                //error: function (xhr, textStatus, errorThrown) {
                //    alert('request failed');
                //    alert("XMLHttpRequest=" + xhr.responseText + "\ntextStatus=" + textStatus + "\nerrorThrown=" + errorThrown);
                //}
            });
            function setHeader(xhr) {

                xhr.setRequestHeader('Accept', 'text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8');
                xhr.setRequestHeader('Accept-Encoding','gzip, deflate');
                xhr.setRequestHeader('Accept-Language','en-US,en;q=0.5');
                xhr.setRequestHeader('Cache-Control','max-age=0');
                xhr.setRequestHeader('Connection','keep-alive');
                xhr.setRequestHeader('Host','192.168.0.10');
                xhr.setRequestHeader('User-Agent','Mozilla/5.0 (Windows NT 6.1; WOW64; rv:21.0) Gecko/20100101 Firefox/21.0');

            }
        }
       
    },
    showFirefoxContextMenu : function(event) {
        // show or hide the menuitem based on what the context menu is on
        document.getElementById("context-mysupermemo").hidden = gContextMenu.onImage;
         //var promptService = Components.classes["@mozilla.org/embedcomp/prompt-service;1"]
         //.getService(Components.interfaces.nsIPromptService);
         //promptService.alert(window, "перевод",
         //"перевод");
    },
    onToolbarButtonCommand: function(e) {
        // just reuse the function above.  you can change this, obviously!
        mysupermemo.onMenuItemCommand(e);
    }
};

window.addEventListener("load", function () { mysupermemo.onLoad(); }, false);


mysupermemo.onFirefoxLoad = function (myevent) {
    var menu = document.getElementById("contentAreaContextMenu");
    if (menu) {
        menu.addEventListener("popupshowing", function (e) {
            mysupermemo.showFirefoxContextMenu(e);
        }, false);
    }
};

mysupermemo.showFirefoxContextMenu = function (data, element) {
   // show or hide the menuitem based on what the context menu is on
    document.getElementById("context-mysupermemo").hidden = gContextMenu.onImage;
    //alert(element);
    //$(element).append($(
     //   "<div id=\"memoDialog\" onclick=\"$('#memoDialog').hide()\" style=\"left: 427px; top: 1003px; display: block; background: none repeat scroll 0 0 //#FFFFFF !important;   border: 1px solid #CCCCCC !important;    border-radius: 3px 3px 3px 3px !important;    box-shadow: 0 2px 4px rgba(0, 0, 0, //0.18) !important;    left: -999px;    overflow: hidden;    padding: 7px 0 0 !important;    top: -999px;    width: 440px !important;    z-index: //999999999 !important;\" >" + data.data + "</div>"));
 };

    //window.addEventListener("dblclick", function () { mysupermemo.onFirefoxLoad(); }, false);

