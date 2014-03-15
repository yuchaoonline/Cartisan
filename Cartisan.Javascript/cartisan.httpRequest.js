(function (_) {
    _.parseJSON = function parseJSON(s, filter) {
        var j;
        function walk(k, v) {
            var i;
            if (v && typeof v === "object") {
                for (i in v) {
                    if (v.hasOwnProperty(i)) {
                        v[i] = walk(i, v[i]);
                    }
                }
            }
            return filter(k, v);
        }

        // 解析通过3个阶段进行。
        // 第一个阶段，通过与此同时表达式检测JSON文本，查找非JSON字符。其中，特别关注“()”和“new”，
        // 因为它们会引起语句的调用，还有“=”，转为它会导致变量的值发生改变。
        // 为安全起见，这里会拒绝所有不希望出现的字符
        if (/^("(\\.|[^"\\\n\r])*?"|[,:{}\[\]0-9.\-+Eaeflnr-u \n\r\t])+?$/.test(s)) {
            // 第2阶段，使用eval函数将JSON文本编译为JavaScript结构。
            // 其中的"{"操作符具有语法上的二义性：取它可以定义一个语句块，也可以表示对象的字面量。
            // 这里将JSON文本用括号括起来是为了消除这种二义性
            try {
                j = eval('(' + s + ')');
            } catch (e) {
                throw new SyntaxError('parseJSON');
            }
        }
        else {
            throw new SyntaxError('parseJSON');
        }

        // 在可选的第3阶段，代码递归地遍历了新生成的结构而且将每个名/值对传递给一个过滤函数，又便进行可能的转换
        if (typeof filter === 'function') {
            j = walk('', j);
        }

        return j;
    };

    // 设置XMLHttpRequest对象的各个不同的部分
    _.getRequestObject = function getRequestObject(url, options) {
        // 初始化请求对象
        var req = false;
        if (window.XmlHttpRequest) {
            req = new window.XmlHttpRequest();
        }
        else if (window.ActiveXObject) {
            req = new window.ActiveXObject('Microsoft.XMLHTTP');
        }

        if (!req) {
            return false;
        }

        // 定义默认的选项
        options = options || {};
        options.method = options.method || 'GET';
        options.send = options.send || null;

        // 为请求的每个阶段定义不同的侦听器
        req.onreadystatechange = function () {
            switch (req.readyState) {
                case 1:
                    // 载入中
                    if (options.loadListener) {
                        options.loadListener.apply(req, arguments);
                    }
                    break;
                case 2:
                    // 载入完成
                    if (options.loadedListener) {
                        options.loadedListener.apply(req, arguments);
                    }
                    break;
                case 3:
                    // 交互
                    if (options.ineractiveListener) {
                        options.ineractiveListener.apply(req, arguments);
                    }
                    break;
                case 4:
                    // 完成
                    // 如果失败则抛出错误
                    try {
                        if (req.status && req.status === 200) {
                            // 针对Content-Type的特殊侦听器
                            // 由于Content-Type状况中可能包含字符集，如：
                            // Content-Type: text/html; charset=ISO-8859-4
                            // 因此通过正则表达式提取出所需的部分
                            var contentType = req.getResponseHeader('Content-Type');
                            var mimeType = contentType.match(/\s*([^;]+)\s*(;|$)/i)[1];
                            switch (mimeType) {
                                case 'text/javascript':
                                case 'application/javascript':
                                    // 响应是Javascript，因此又req.responseText作为回调的参数
                                    if (options.jsResponseListener) {
                                        options.jsResponseListener.call(req, req.responseText);
                                    }
                                    break;
                                case 'application/json':
                                    // 响应是JSON，因此需要用匿名函数对req.responseText进行解析，又返回作为回调参数的JSON对象
                                    if (options.jsonResponseListener) {
                                        try {
                                            var json = parseJSON(req.responseText);
                                        } catch (e) {
                                            var json = false;
                                        }
                                        options.jsonResponseListener.call(req, json);
                                    }
                                    break;
                                case 'text/xml':
                                case 'application/xml':
                                case 'application/xhtml+xml':
                                    // 响应是XML，因此req.responseXML作为回调的参数，此时是Document对象
                                    if (options.xmlResponseListener) {
                                        options.xmlResponseListener.call(req, req.responseXML);
                                    }
                                    break;
                                case 'text/html':
                                    // 响应是HTML，因此又req.responseText作为回调的参数
                                    if (options.htmlResponseListener) {
                                        options.htmlResponseListener.call(req, req.responseText);
                                    }
                                    break;
                            }
                            // 针对响应功能完成侦听器
                            if (options.completeListener) {
                                options.completeListener.apply(req, arguments);
                            }
                        }
                        else {
                            // 响应完成但却存在错误
                            if (options.errorListener) {
                                options.errorListener.apply(req, arguments);
                            }
                        }
                    } catch (e) {
                        // 忽略错误
                    }
                    break;
            }
        };

        // 开启请求
        req.open(options.method, url, true);
        //添加特殊的头部信息又标识请求
        req.setRequestHeader('X-Easier-Ajax-Request', 'AjaxRequest');
        return req;
    };

    // 通过简单地包装getRequestObject()和send()方法发送XMLHttpRequest对象的请求
    _.ajaxRequest = function ajaxRequest(url, options) {
        var req = getRequestObject(url, options);
        return req.send(options.send);
    };

    // XssHttpRequest对象计数器
    var XssHttpRequestCount = 0;

    // XMLHttpRequest对象的一个跨站点<script>标签的实现
    var XssHttpRequest = function () {
        this.requestID = 'XSS_HTTP_REQUEST_' + (++XssHttpRequestCount);
    };

    XssHttpRequest.prototype = {
        url: null,
        scriptObject: null,
        responseJSON: null,
        status: 0,
        readyState: 0,
        timeout: 30000,
        onreadystatechange: function () { },
        setReadyState: function (newReadyState) {
            // 如果比当前状态更新，则只更新就绪状态
            if (this.readyState < newReadyState || newReadyState === 0) {
                this.readyState = newReadyState;
                this.onreadystatechange();
            }
        },
        open: function (url, timeout) {
            this.timeout = timeout || 30000;
            // 将一个名为XSS_HTTP_REQUEST_CALLBACK的特殊变更附加给URL，
            // 其中包含本次请求的回调函数的名称
            this.url = url + ((url.indexOf('?') !== -1) ? '&' : '?') +
                'XSS_HTTP_REQUEST_CALLBACK=' + this.requestID + '_CALLBACK';
            this.setReadyState(0);
        },
        send: function () {
            var requestObject = this;
            // 创建一个载入外部数据的新script对象
            this.scriptObject = document.createElement('script');
            this.scriptObject.setAttribute('id', this.requestID);
            this.scriptObject.setAttribute('type', 'text/javascript');

            // 尚未设置src属性，也先不将其添加到文档……

            // 他那一个在给定的毫秒数之后触发的setTimeOut()方法。
            // 如果在给定时间内脚本没有载入完成，则取消载入
            var timeoutWatcher = setTimeout(function () {
                // 脚本晚于我们假定的停止时间之后载入的情况下
                // 通过一个空方法来重新为window方法赋值
                window[requestObject.requestID + '_CALLBACK'] = function () { };

                // 移除脚本又防止进一步载入
                requestObject.scriptObject.parentNode.removeChild(requestObject.scriptObject);

                // 将状态设置为错误
                requestObject.status = 2;
                requestObject.statusText = 'Timeout after ' + requestObject.timeout + ' milliseconds.';

                // 更新就绪状态
                requestObject.setReadyState(2);
                requestObject.setReadyState(3);
                requestObject.setReadyState(4);
            }, this.timeout);

            // 在window对象中创建一个与请求中的回调方法匹配的方法
            // 在调用时负责处理请求的其它部分
            window[this.requestID + '_CALLBACK'] = function (JSON) {
                // 当脚本载入时将执行这个方法，同时预期的JSON对象

                // 在请求载入成功时清除timeoutWatcher方法
                clearTimeout(timeoutWatcher);

                // 更新就绪状态
                requestObject.setReadyState(2);
                requestObject.setReadyState(3);

                // 将状态设置为成功
                requestObject.responseJSON = JSON;
                requestObject.status = 1;
                requestObject.statusText = 'Loaded.';

                // 更新就绪状态
                requestObject.setReadyState(4);
            }

            // 设置初始就绪状态
            this.setReadyState(1);

            // 现在再设置src属性并将其添加到文档头部。这样会载入脚本
            this.scriptObject.setAttribute('src', this.url);
            var head = document.getElementsByTagName('head')[0];
            head.appendChild(this.scriptObject);
        }
    };

    _.XssHttpRequest = XssHttpRequest;

    // 设置XssHttpRequest对象的不同部分
    _.getXssRequestObject = function getXssRequestObject(url, options) {
        var req = new XssHttpRequest();

        options = options || {};
        // 默认中断时间为30秒
        options.timeout = options.timeout || 30000;
        req.onreadystatechange = function () {
            switch (req.readyState) {
                case 1:
                    // 载入中
                    if (options.loadListener) {
                        options.loadListener.apply(req, arguments);
                    }
                    break;
                case 2:
                    // 载入完成
                    if (options.loadedListener) {
                        options.loadedListener.apply(req, arguments);
                    }
                    break;
                case 3:
                    // 交互
                    if (options.interactiveListener) {
                        options.interactiveListener.apply(req, arguments);
                    }
                    break;
                case 4:
                    // 完成
                    if (req.status === 1) {
                        if (options.completeListener) {
                            options.completeListener.apply(req, arguments);
                        }
                    }
                    else {
                        if (options.errorListener) {
                            options.errorListener.apply(req, arguments);
                        }
                    }
                    break;
            }
        };
        req.open(url, options.timeout);

        return req;
    };

    // 发送XssHttpRequest请求
    _.xssRequest = function xssRequest(url, options) {
        var req = getXssRequestObject(url, options);
        return req.send(null);
    };
})(cartisan);