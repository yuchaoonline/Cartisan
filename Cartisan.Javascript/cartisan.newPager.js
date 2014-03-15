(function ($) {
    youQiu.Pager = function (options) {
        this.pageInfo = {};

        this.placeHolder = options.placeHolder || "pagerHolder";
        this.buttons = options.buttons || 8;
        this.getPageData = options.getPageData || function () { };

    };

    youQiu.Pager.prototype = {
        SetPageInfo: function (pageInfo) {
            this.pageInfo.pageIndex = pageInfo.PageIndex;
            this.pageInfo.pageSize = pageInfo.PageSize;
            this.pageInfo.total = pageInfo.Total;
            this.pageInfo.pageCount = pageInfo.PageCount;

            if (this.pageInfo.pageIndex > this.pageInfo.pageCount) {
                this.pageInfo.pageIndex -= 1;
                this.getPageData();
                return;
            }

            var html = '<ul class="fR">';

            var start = this.pageInfo.pageIndex % this.buttons == 0 ?
                (this.pageInfo.pageIndex - this.buttons + 1) :
                (this.pageInfo.pageIndex - (this.pageInfo.pageIndex % this.buttons) + 1);
            var end = start + this.buttons - 1;
            end = end > this.pageInfo.pageCount ? this.pageInfo.pageCount : end;

            if (start != 1) {
                html += '<li><a style="cursor:pointer" pageIndex="' + (start - 1) + '">...</a></li>';
            }


            for (var page = start; page <= end; page++) {
                if (page == this.pageInfo.pageIndex) {
                    html += '<li class="active"><a style="cursor:pointer" pageIndex="' + page + '">' + page + '</a></li>';
                }
                else {
                    html += '<li><a style="cursor:pointer" pageIndex="' + page + '">' + page + '</a></li>';
                }
            }

            if (end < this.pageInfo.pageCount) {
                html += '<li><a style="cursor:pointer" pageIndex="' + (end + 1) + '">...</a></li>';
            }

            html += '</ul>';

            $('#' + this.placeHolder).empty().addClass('pagination').addClass('clearfix').html(html);

            var pager = this;
            $('#' + this.placeHolder + ' a').on('click', function (event) {
                event.preventDefault();
                var pageIndex = $(this).attr('pageIndex');

                if (pageIndex == pager.pageInfo.pageIndex) {
                    return;
                }

                pager.pageInfo.pageIndex = pageIndex;
                pager.getPageData();
            });
        },
        getPageIndex: function () {
            return this.pageInfo.pageIndex;
        },
        getPageCount: function () {
            return this.pageInfo.pageCount;
        },
        getTotal: function () {
            return this.pageInfo.total;
        },
        getPageSize: function () {
            return this.pageInfo.pageSize;
        },
        initializeData: function () {
            this.pageInfo.pageIndex = 1;
            this.getPageData();
        },
        refresh: function () {
            this.getPageData();
        }
    };


})(jQuery);