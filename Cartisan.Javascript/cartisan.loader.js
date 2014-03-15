(function () {
    if (!window.YouQiu) {
        window.YouQiu = {};
    }

    function loadScript(url, callback) {
        var script = document.createElement("script");
        script.type = "text/javascript";
        if (callback) {
            if (script.readyState) {    // IE
                script.onreadystatechange = function () {
                    if (script.readyState === 'loaded' || script.readyState === 'complete') {
                        script.onreadystatechange = null;
                        callback();
                    }
                };
            }
            else {  //Others
                script.onload = function () {
                    callback();
                };
            }
        }

        script.src = url;

        document.getElementsByTagName('head')[0].appendChild(script);
    }
    YouQiu.loadScript = loadScript;

    function loadStyle(url) {
        var link = document.createElement('link');
        link.rel = 'stylesheet';
        link.type = 'text/css';
        link.href = url;
        document.getElementsByTagName('head')[0].appendChild(link);
    }
    YouQiu.loadStyle = loadStyle;
})();