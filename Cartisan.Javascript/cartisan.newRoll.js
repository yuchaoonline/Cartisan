(function ($) {
    var obj = null;
    var time;
    var t;
    $.fn.hkRoll = function (options) {
        obj = this;
        var defaults = {
            images: [],
            heigth:308,
            isOpenUrl: false,   //是否打开新页面
            time: 3000
        };

        $(obj).css('height', options.heigth);

        options = $.extend(defaults, options);

        var imageListHtml = '';
        $.each(options.images, function (index, image) {
            imageListHtml += '<li><img style="cursor: pointer" src="' + image.imageUrl + '"/></li>';
        });
        $(obj).append('<ul>' + imageListHtml + '</ul>');

        var tipListHtml = '';
        $.each(options.images, function (index, image) {
            tipListHtml += '<li><a style="cursor: pointer">' + image.title + '</a></li>';
        });
        $(obj).append('<ul class="change_baner">' + tipListHtml + '</ul>');

        time = options.time;
        refrsh();

        $(obj).find('ul:last li').hover(function () {
            clearTimeout(t);
            $(obj).find('ul:first li:visible').stop().css('opacity', '');
            var index = $(this).index();
            showImg(index);
        }, function () {
            clearTimeout(t);
            t = setTimeout(refrsh, time);
        }).bind('click', function (event) {
            event.preventDefault();
            var index = $(this).index();
            if (options.images[index].redictUrl) {
                if (options.images[index].isOpenUrl) {
                    window.open(options.images[index].redictUrl);
                }
                else {
                    location.href = options.images[index].redictUrl;
                }
            }
        });

        $(obj).find('ul:first li').hover(function () {
            clearTimeout(t);
        }, function () {
            clearTimeout(t);
            t = setTimeout(refrsh, time);
        });

        $(obj).find('ul:first li').live('click', function (event) {
            event.preventDefault();
            var index = $(this).index();
            if (options.images[index].redictUrl) {
                if (options.images[index].isOpenUrl) {
                    window.open(options.images[index].redictUrl);
                }
                else {
                    location.href = options.images[index].redictUrl;
                }
            }
        });
    };

    var showImg = function (index) {
        $(obj).find('ul:first li').hide();
        $(obj).find('ul:first li:eq(' + index + ')').fadeIn(2000);
        $(obj).find('ul:last li a').removeClass('select');
        $(obj).find('ul:last li:eq(' + index + ') a').addClass('select');
    };

    var refrsh = function () {
        var index = $(obj).find('ul:first li:visible').index();
        if (index === -1) {
            $(obj).find('ul:first li:first').show();
            $(obj).find('ul:last li:first').addClass('select');
        }
        else {
            if (index !== $(obj).find('ul:first li').length - 1) {
                index = index + 1;
            }
            else {
                index = 0;
            }
            showImg(index);
        }
        t = setTimeout(refrsh, time);
    };
})(jQuery);