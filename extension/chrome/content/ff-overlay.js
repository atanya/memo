

var mysupermemo = {
    onLoad: function() {
        // initialization code
        this.initialized = true;
        this.strings = document.getElementById("mysupermemo-strings");
    },

    onMenuItemCommand: function(e) {
        var promptService = Components.classes["@mozilla.org/embedcomp/prompt-service;1"]
                                      .getService(Components.interfaces.nsIPromptService);
        promptService.alert(window, "перевод",
                                    "перевод");
    },
    showFirefoxContextMenu : function(event) {
        // show or hide the menuitem based on what the context menu is on
        //document.getElementById("context-mysupermemo").hidden = gContextMenu.onImage;
  
       
  
        // var promptService = Components.classes["@mozilla.org/embedcomp/prompt-service;1"]
        // .getService(Components.interfaces.nsIPromptService);
        // promptService.alert(window, "перевод",
        // "перевод");
								
								
    },
    onToolbarButtonCommand: function(e) {
        // just reuse the function above.  you can change this, obviously!
        mysupermemo.onMenuItemCommand(e);
    }
};

window.addEventListener("load", function () { mysupermemo.onLoad(); }, false);


mysupermemo.onFirefoxLoad = function(myevent) {
  /*document.getElementById("contentAreaContextMenu")
          .addEventListener("popupshowing", function (e) {
    mysupermemo.showFirefoxContextMenu(e);
  }, false);*/
      
    var selectedWord = content.getSelection();

    //$.get('http://192.168.0.10/api/main/' + selectedWord,
    //        function (data) {
    //            alert(data);
    //        }
    //   );

    var urlWIthValue = 'http://192.168.0.10/api/main/' + selectedWord.toString().trim();

    //var Request = require("sdk/request").Request;
    //alert(urlWIthValue);

    //// Be a good consumer and check for rate limiting before doing more.
    //Request({
    //    url: urlWIthValue,
    //    contentType: "application/jsonp",
    //    onComplete: function (response) {
    //        alert('ura!');
    //        mysupermemo.showFirefoxContextMenu(response, selectedWord.anchorNode.parentElement);
    //    }
    //}).get();

        $.ajax({
            url: urlWIthValue,
            type: "GET",
            dataType: "jsonp",
            contentType: "application/json",
            beforeSend: setHeader,
            success: function (data) {
                alert("success");
                mysupermemo.showFirefoxContextMenu(data, selectedWord.anchorNode.parentElement);
            },
            error: function (result) {
                alert('fail');
            }
        });
        function setHeader(xhr) {

            //xhr.setRequestHeader('Accept', 'text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8');
            //xhr.setRequestHeader('Accept-Encoding','gzip, deflate');
            //xhr.setRequestHeader('Accept-Language','en-US,en;q=0.5');
            //xhr.setRequestHeader('Cache-Control','max-age=0');
            //xhr.setRequestHeader('Connection','keep-alive');
            //xhr.setRequestHeader('Host','192.168.0.10');
            //xhr.setRequestHeader('User-Agent','Mozilla/5.0 (Windows NT 6.1; WOW64; rv:21.0) Gecko/20100101 Firefox/21.0');

        }
  
};

mysupermemo.showFirefoxContextMenu = function (data, element) {
   // show or hide the menuitem based on what the context menu is on
    //document.getElementById("context-mysupermemo").hidden = gContextMenu.onImage;
    //alert(element);
    $(element).append($(
        "<div id=\"memoDialog\" onclick=\"$('#memoDialog').hide()\" style=\"left: 427px; top: 1003px; display: block; background: none repeat scroll 0 0 #FFFFFF !important;   border: 1px solid #CCCCCC !important;    border-radius: 3px 3px 3px 3px !important;    box-shadow: 0 2px 4px rgba(0, 0, 0, 0.18) !important;    left: -999px;    overflow: hidden;    padding: 7px 0 0 !important;    top: -999px;    width: 440px !important;    z-index: 999999999 !important;\" >" + data.data + "</div>"));
 };

window.addEventListener("dblclick", function () { mysupermemo.onFirefoxLoad(); }, false);