var cartisan = cartisan || { };

(function (_, $) {
    // 直接给字符串原型添加格式化方法
    String.prototype.format = function() {
        var args = $.makeArray(arguments);

        return this.replace( /\{(\d+)\}/g , function(m, i) {
            return args[i];
        });
    };
    
    // 仿jQuery.Validate中的format方法
    _.format = function(source, params) {
        if (arguments.length === 1) {
            return function() {
                var args = $.makeArray(arguments);
                args.unshift(source);
                return _.format.apply(this, args);
            };
        }
        if (arguments.length>2 && params.constructor!=Array) {
            params = $.makeArray(arguments).slice(1);
        }
        if (params.constructor!=Array) {
            params = [params];
        }
        $.each(params, function(i, n) {
            source = source.replace(new RegExp("\\{" + i + "\\}", "g"), n);
        });
        return source;
    };

    Date.prototype.format = function(format) {
        /*
         * eg:format="YYYY-MM-dd hh:mm:ss";
         */
        var o = {
            "M+": this.getMonth() + 1,  //month
            "d+": this.getDate(),     //day
            "h+": this.getHours(),    //hour
            "m+": this.getMinutes(),  //minute
            "s+": this.getSeconds(), //second
            "q+": Math.floor((this.getMonth() + 3) / 3),  //quarter
            "S": this.getMilliseconds() //millisecond
        };

        if( /(y+)/ .test(format)) {
            format = format.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
        }

        for(var k in o) {
            if(new RegExp("(" + k + ")").test(format)) {
                format = format.replace(RegExp.$1, RegExp.$1.length == 1 ? o[k] : ("00" + o[k]).substr(("" + o[k]).length));
            }
        }
        return format;
    };

    _.scanDate = function scanDate(obj, dateParser) {
        dateParser = dateParser || jsonDateParser;
        for(var key in obj) {
            obj[key] = dateParser(obj[key]);
            if(typeof obj[key] === 'object') {
                scanDate(obj[key], dateParser);
            }
        }
    };

    _.jsonDateParser = function jsonDateParser(value) {
        if(typeof value === 'string') {
            var match = /-?\d+/.exec(value);
            if(match) {
                return new Date(parseInt(match[0]));
            }
        }
        return value;
    };

    _.alert = function (options, okCallback) {
        options = options || { };
        if (typeof options === 'string') {
            options = { content: options };
        }
        options.title = options.title || "提醒";
        options.content = options.content || "";
        options.width = options.width || 300;
        options.ok = okCallback || options.ok || (function() {});
        
        $('#popWindow').html(options.content).dialog({
            title: options.title,
            width: options.width,
            resizable:false,
            buttons: {
                '确定': function() {
                    options.ok();
                    $(this).dialog('close');
                }
            }
        });
        $('.ui-dialog').removeClass('ui-widget-content');
        $('.ui-dialog-titlebar').removeClass('ui-widget-header');
        $('.ui-dialog-content').removeClass('ui-widget-content');
        $('.ui-dialog-buttonpane').removeClass('ui-widget-content');
        $('#popWindow').dialog('open');
    };

    _.confirm = function(options, okCallback, cancelCallback) {
        options = options || { };
        if (typeof options === 'string') {
            options = { content: options };
        }
        options.title = options.title || "提醒";
        options.content = options.content || "";
        options.width = options.width || 300;
        options.ok = okCallback || options.ok || (function() {});
        options.cancel = cancelCallback || options.cancel || (function() {});

        $('#popWindow').html(options.content).dialog({
                title: options.title,
                width: options.width,
                resizable:false,
                buttons: {
                    '确定': function() {
                        options.ok();
                        $(this).dialog('close');
                    },
                    '取消': function() {
                        options.cancel();
                        $(this).dialog('close');
                    }
                }
            });
        $('.ui-dialog').removeClass('ui-widget-content');
        $('.ui-dialog-titlebar').removeClass('ui-widget-header');
        $('.ui-dialog-content').removeClass('ui-widget-content');
        $('.ui-dialog-buttonpane').removeClass('ui-widget-content');
        $('#popWindow').dialog('open');
    };

    _.warpValidateOption = function(option) {
        option.errorElement = 'p';
        option.errorClass = 'unchecked';
        option.validClass = 'checked';
        option.errorPlacement = function(error, element) {
            if(element.parent().find('p[for="'+element.attr('id')+'"]')!=null) {
                element.parent().find('p[for="'+element.attr('id')+'"]').remove();
            }
            error.attr('style', 'clear:both');
            error.appendTo(element.parent());
        };


        option.success = function(label) {
            label.removeClass('unchecked').addClass('checked');
            label.removeAttr('style');
        };

        return option;
    };
    
    // 指定的函数会被立即执行，并且在指定的间隔内不会执行第二次调用，等过了指定间隔后执行第二次调用。而且如果在指定的时间间隔内出现多次重复调用，只会执行一次。
    // 函数节流
    _.throttle = function(func, wait) {
        var context, args, timeout, throttling, more;
        // 
        var whenDone = _.debounce(function() {
            more = throttling = false;
        }, wait);
        return function() {
            context = this;
            args = arguments;
            var later = function() {
                timeout = null;
                if(more) {
                    func.apply(context, args);
                }
                whenDone();
            };
            if(!timeout) {
                timeout = setTimeout(later, wait);
            }
            if(throttling) {
                more = true;
            }
            else {
                func.apply(context, args);
            }
            whenDone();
            throttling = true;
        };
    };

    // 当第一次调用的时间间隔内，出现第二次调用，便取消第一次调用，并以第二次指定的时间间隔重新计时。
    _.debounce = function(func, wait) {
        // 定时函数句柄
        var timeout;
        return function() {
            // 执行函数的上下文（即谁来执行定期函数的）
            var context = this;
            var args = arguments;
            // 到达指定时间时，真正执行的函数
            var later = function() {
                timeout = null;
                // 以用户执行函数时的上下文及参数来执行用户指定的函数
                func.apply(context, args);
            };
            // 清除未完成的定时器
            clearTimeout(timeout);
            // 创建定时器
            timeout = setTimeout(later, wait);
        };
    };

    _.HTMLEncode = function(str) {
        var s = "";
        if(str.length == 0) {
            return "";
        }
        s = str.replace( /&/g , "&gt;");
        s = s.replace( /</g , "&lt;");
        s = s.replace( />/g , "&gt;");
        s = s.replace( /    /g , "&nbsp;");
        s = s.replace( /\'/g , "&#39;");
        s = s.replace( /\"/g , "&quot;");
        s = s.replace( /\n/g , "<br>");
        return s;
    };

    _.HTMLDecode = function(str) {
        var s = "";
        if(str.length == 0) {
            return "";
        }
        s = str.replace( /&gt;/g , "&");
        s = s.replace( /&lt;/g , "<");
        s = s.replace( /&gt;/g , ">");
        s = s.replace( /&nbsp;/g , "    ");
        s = s.replace( /&#39;/g , "\'");
        s = s.replace( /&quot;/g , "\"");
        s = s.replace( /<br>/g , "\n");
        return s;
    };
    
    _.param = function(data) {
        var s = [];
        var add = function(key, value) {
            value = $.isFunction(value) ? value() : value;
            s[s.length] = encodeURIComponent(key) + '=' + encodeURIComponent(value);
        };

        if($.isArray(data) || (data.jquery && !$.isPlainObject(data))) {
            $.each(data, function() {
                add(this.name, this.value);
            });
        }
        else {
            for(var prefix in data) {
                buildParams(prefix, data[prefix], add);
            }
        }
            
        return s.join( "&" ).replace( /%20/g, "+" );
    };

    function buildParams(prefix, obj, add) {
        if($.isArray(obj)) {
            $.each(obj, function(index, item) {
                buildParams(prefix + '[' + index + ']', item, add);
            });
        }
        else if(obj!=null && typeof obj==='object') {
            for(var name in obj) {
                buildParams(prefix + '.' + name, obj[name], add);
            }
        }
        else {
            add(prefix, obj);
        }
    }
})(cartisan, jQuery);

//
//var youqiu = youqiu || {};
//youqiu.data = youqiu.data || {};    // 用于存放临时的数据或者对象
//
///**************************
// * 屏蔽右键
// **************************/
//$(document).bind('contextmenu', function () {
//    // return false;
//});
//
///**************************
// * 禁止复制
// **************************/
//$(document).bind('selectstart', function () {
//    // return false;
//});
//
///**************************
// * 命名空间
// **************************/
//youqiu.ns = function () {
//    var o = {}, args;
//    for (var i = 0; i < arguments.length; i++) {
//        args = arguments[i].split('.');
//        o = window[args[0]] = window[args[0]] || {};
//        for (var j = 0; j < args.slice(1).length; j++) {
//            o = o[args[j + 1]] = o[args[j + 1]] || {};
//        }
//    }
//
//    return o;
//};
//
///**************************
// * 去除字符串空格
// **************************/
//youqiu.trim = function (str) {
//    return str.replace(/(^\s*)|(\s*$)/g, '');
//};
//youqiu.ltrim = function (str) {
//    return str.replace(/(^\s*)/g, '');
//};
//youqiu.rtrim = function (str) {
//    return str.replace(/(\s*$)/g, '');
//};
//
///**************************
// * 判断是否以指定字符串开始、结束
// **************************/
//youqiu.startWidth = function (source, str) {
//    return new RegExp('^' + str).test(source);
//};
//youqiu.endWidth = function (source, str) {
//    return new RegExp(str + '$').test(source);
//};
//
///**************************
// * 将form表单元素的值序列化成对象
// **************************/
//youqiu.serializeObject = function (form) {
//    var o = {};
//    $.each(form.serializeArray(), function (index) {
//        if (this['value'] != undefined && this['value'].length > 0) {
//            if (o[this['name']]) {
//                o[this[name]] = o[this['name']] instanceof Array ? o[this['name']].push(this['value']) : [o[this['name']], this['value']];
//            }
//            else {
//                o[this['name']] = this['value'];
//            }
//        }
//    });
//
//    return o;
//};
//
///**************************
// * 字符串格式化
// **************************/
//youqiu.formatString = function (str) {
//    for (var i = 0; i < arguments.length - 1; i++) {
//        str = str.replace('{' + i + '}', arguments[i + 1]);
//    }
//    return str;
//};
//
//youqiu.cloneObject = function (obj) {
//    if (!obj) {
//        return obj;
//    }
//    var o = obj.constructor === Array ? [] : {};
//    for (var i in obj) {
//        if (obj.hasOwnProperty(i)) {
//            o[i] = typeof obj[i] === "object" ? youqiu.cloneObject(obj[i]) : obj[i];
//        }
//    }
//    return o;
//};
//
//
///**
// * Create a cookie with the given key and value and other optional parameters.
// * 
// * @example sy.cookie('the_cookie', 'the_value');
// * @desc Set the value of a cookie.
// * @example sy.cookie('the_cookie', 'the_value', { expires: 7, path: '/', domain: 'jquery.com', secure: true });
// * @desc Create a cookie with all available options.
// * @example sy.cookie('the_cookie', 'the_value');
// * @desc Create a session cookie.
// * @example sy.cookie('the_cookie', null);
// * @desc Delete a cookie by passing null as value. Keep in mind that you have to use the same path and domain used when the cookie was set.
// * 
// * @param String
// *            key The key of the cookie.
// * @param String
// *            value The value of the cookie.
// * @param Object
// *            options An object literal containing key/value pairs to provide optional cookie attributes.
// * @option Number|Date expires Either an integer specifying the expiration date from now on in days or a Date object. If a negative value is specified (e.g. a date in the past), the cookie will be deleted. If set to null or omitted, the cookie will be a session cookie and will not be retained when the the browser exits.
// * @option String path The value of the path atribute of the cookie (default: path of page that created the cookie).
// * @option String domain The value of the domain attribute of the cookie (default: domain of page that created the cookie).
// * @option Boolean secure If true, the secure attribute of the cookie will be set and the cookie transmission will require a secure protocol (like HTTPS).
// * @type undefined
// * 
// * @name sy.cookie
// * @cat Plugins/Cookie
// * @author Klaus Hartl/klaus.hartl@stilbuero.de
// * 
// * Get the value of a cookie with the given key.
// * 
// * @example sy.cookie('the_cookie');
// * @desc Get the value of a cookie.
// * 
// * @param String
// *            key The key of the cookie.
// * @return The value of the cookie.
// * @type String
// * 
// * @name sy.cookie
// * @cat Plugins/Cookie
// * @author Klaus Hartl/klaus.hartl@stilbuero.de
// */
//youqiu.cookie = function (key, value, options) {
//    if (arguments.length > 1 && (value === null || typeof value !== "object")) {
//        options = $.extend({}, options);
//        if (value === null) {
//            options.expires = -1;
//        }
//        if (typeof options.expires === 'number') {
//            var days = options.expires, t = options.expires = new Date();
//            t.setDate(t.getDate() + days);
//        }
//        return (document.cookie = [encodeURIComponent(key), '=', options.raw ? String(value) : encodeURIComponent(String(value)), options.expires ? '; expires=' + options.expires.toUTCString() : '', options.path ? '; path=' + options.path : '', options.domain ? '; domain=' + options.domain : '', options.secure ? '; secure' : ''].join(''));
//    }
//    options = value || {};
//    var result, decode = options.raw ? function (s) {
//        return s;
//    } : decodeURIComponent;
//    return (result = new RegExp('(?:^|; )' + encodeURIComponent(key) + '=([^;]*)').exec(document.cookie)) ? decode(result[1]) : null;
//};
//
//youqiu.param = function (data) {
//    var s = [];
//    var add = function (key, value) {
//        value = $.isFunction(value) ? value() : value;
//        s[s.length] = encodeURIComponent(key) + '=' + encodeURIComponent(value);
//    };
//
//    if ($.isArray(data) || (data.jquery && !$.isPlainObject(data))) {
//        $.each(data, function () {
//            add(this.name, this.value);
//        });
//    }
//    else {
//        for (var prefix in data) {
//            buildParams(prefix, data[prefix], add);
//        }
//    }
//
//    return s.join("&").replace(/%20/g, "+");
//};
//
//function buildParams(prefix, obj, add) {
//    if ($.isArray(obj)) {
//        $.each(obj, function (index, item) {
//            buildParams(prefix + '[' + index + ']', item, add);
//        });
//    }
//    else if (obj != null && typeof obj === 'object') {
//        for (var name in obj) {
//            buildParams(prefix + '.' + name, obj[name], add);
//        }
//    }
//    else {
//        add(prefix, obj);
//    }
//}
//
///**
// * 改变jQuery的AJAX默认属性和方法
// * 
// */
////$.ajaxSetup({
////    type: 'POST',
////    error: function (XMLHttpRequest, textStatus, errorThrown) {
////        try {
////            parent.$.messager.progress('close');
////            parent.$.messager.alert('错误', XMLHttpRequest.responseText);
////        } catch (e) {
////            alert(XMLHttpRequest.responseText);
////        }
////    }
////});
//
//
///**************************
// * 浏览器相关信息
// **************************/
//var browser = {
//    appCodeName: navigator.appCodeName,     // 浏览器代码名称
//    appName: navigator.appName,     // 浏览器名称
//    appVersion: navigator.appVersion,       // 浏览器的平台和版本信息
//    cookieEnabled: navigator.cookieEnabled,     // 浏览器中是否启用cookie
//    platform: navigator.platform,       // 运行浏览器的操作系统平台
//    userAgent: navigator.userAgent,     // 由客户机发送服务器的user-agent头部值
//    isIe: false,
//    isVersion: '',
//    isChrome: false,
//    isFirefox: false
//};
//
//if (browser.userAgent.indexOf('MSIE') > -1) {
//    bowser.isIe = true;
//    if (browser.userAgent.indexOf('MSIE 10') > -1) {
//        border.ieVersion = 10;
//    }
//    else if (browser.userAgent.indexOf('MSIE 9') > -1) {
//        border.ieVersion = 9;
//    }
//    else if (browser.userAgent.indexOf('MSIE 8') > -1) {
//        border.ieVersion = 8;
//    }
//    else if (browser.userAgent.indexOf('MSIE 7') > -1) {
//        border.ieVersion = 7;
//    }
//    else if (browser.userAgent.indexOf('MSIE 6') > -1) {
//        border.ieVersion = 6;
//    }
//}
//else if (browser.userAgent.indexOf('Chrome') > -1) {
//    browser.isChrome = true;
//}
//else if (browser.userAgent.indexOf('Firefox') > -1) {
//    browser.isFirefox = true;
//}