(function ($) {
    var obj = null;

    var containerWidth;
    var itemCount;
    var itemWidth;
    var listWidth;
    var moves;
    var maxMoves;
    $.fn.slider = function (options) {
        var defaults = {
            itemWidth: 100,
            showCount: 7
        };

        options = $.extend(defaults, options);

        obj = this;

        containerWidth = $(this).find('.sliderContainer').width();
        itemCount = $(this).find('.sliderContainer>ul>li').length;

        itemWidth = options.itemWidth;

        listWidth = itemWidth * itemCount;

        moves = 0;
        maxMoves = itemCount - options.showCount;

        btnStatusProcess();

        $(this).find('.sliderLeftControl>a').on('click', function (event) {
            event.preventDefault();
            slide('pre');
        });

        $(this).find('.sliderRightControl>a').on('click', function (event) {
            event.preventDefault();
            slide('next');
        });
    };

    var slide = function (direction) {
        if (listWidth > containerWidth) {
            if (direction === 'next') {
                if (moves < maxMoves) {
                    moves++;
                }
            }
            else if (direction === 'pre') {
                if (moves > 0) {
                    moves--;
                }
            }

            $(obj).find('.mainDiv>ul').animate({
                left: ((-itemWidth) * moves) + 'px'
            }, 200);
        }

        btnStatusProcess();
    };

    var btnStatusProcess = function () {
        var rightControl = $(obj).find('.sliderRightControl>a').find('img');
        if (moves >= maxMoves) {
            rightControl.parent().attr('disabled', 'disabled');
            rightControl.attr('src', rightControl.attr('srcDisabled'));
        }
        else {
            rightControl.parent().removeAttr('disabled');
            rightControl.attr('src', rightControl.attr('srcEnabled'));
        }

        var leftControl = $(obj).find('.sliderLeftControl>a').find('img');
        if (moves <= 0) {
            leftControl.parent().attr('disabled', 'disabled');
            leftControl.attr('src', leftControl.attr('srcDisabled'));
        }
        else {
            leftControl.parent().removeAttr('disabled');
            leftControl.attr('src', leftControl.attr('srcEnabled'));
        }
    };
})(jQuery);