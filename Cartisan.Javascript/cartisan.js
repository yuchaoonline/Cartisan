var youQiu = { };

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
})(youQiu, jQuery);