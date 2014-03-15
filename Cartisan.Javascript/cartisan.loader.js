(function (_) {
    _.loadScript = function(url, callback) {
        var script = document.createElement("script");
        script.type = "text/javascript";
        if(callback) {
            if(script.readyState) { // IE
                script.onreadystatechange = function() {
                    if(script.readyState === 'loaded' || script.readyState === 'complete') {
                        script.onreadystatechange = null;
                        callback();
                    }
                };
            }
            else { //Others
                script.onload = function() {
                    callback();
                };
            }
        }

        script.src = url;

        document.getElementsByTagName('head')[0].appendChild(script);
    };
    
    _.loadStyle = function(url) {
        var link = document.createElement('link');
        link.rel = 'stylesheet';
        link.type = 'text/css';
        link.href = url;
        document.getElementsByTagName('head')[0].appendChild(link);
    };
})(cartisan);