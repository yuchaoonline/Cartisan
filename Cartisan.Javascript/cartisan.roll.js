(function ($) {
    var obj = null;
    var time;
    var t;
    $.fn.hkRoll = function (options) {
        obj = this;
        var defaults = {
            images: [],
            isOpenUrl: false,   //是否打开新页面
            time: 3000
        };

        options = $.extend(defaults, options);

        var imageListHtml = '';
        $.each(options.images, function (index, image) {
            imageListHtml += '<a href="#"><img style="cursor: pointer"  border="0" height="200" width="498" src="' + image.imageUrl + '"/></a>';
        });
        $(obj).append('<div class="rollImg">' + imageListHtml + '</div>');

        $(obj).append('<div class="rollTip"><span style="cursor: pointer"  class="circle circle-P2">1</span><span style="cursor: pointer"  class="circle circle-P1">2</span><span style="cursor: pointer"  class="circle">3</span></div>');

        time = options.time;
        refrsh();

        $(obj).find('.rollTip span').bind('click', function () {
            var index = $(this).index();
            showImg(index);
        });

        $(obj).find('.rollImg a').hover(function () {
            clearTimeout(t);
        }, function () {
            clearTimeout(t);
            t = setTimeout(refrsh, time);
        }).live('click', function (event) {
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
        $(obj).find('.rollImg a').hide();
        $(obj).find('.rollImg a:eq(' + index + ')').fadeIn(2000);
        $(obj).find('.rollTip span').removeClass('circle-chose');
        $(obj).find('.rollTip span:eq(' + index + ')').addClass('circle-chose');
    };

    var refrsh = function () {
        var index = $(obj).find('.rollImg a:visible').index();
        if (index === -1) {
            $(obj).find('.rollImg a:first').show();
            $(obj).find('.rollTip span:first').addClass('circle-chose');
        }
        else {
            if (index !== $(obj).find('.rollImg a').length - 1) {
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