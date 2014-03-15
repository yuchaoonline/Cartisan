(function ($) {
    youQiu.Pager = function (options) {
        //this.init = false;
        this.pageInfo = {
            pageIndex: 1
        };

        var placeHolder = options.placeHolder || "pagerHolder";
        this.getPageData = options.getPageData || function () {
        };

        this.pageSelect = $('<select style="width: 60px; padding: 2px; float: right;" name="c4"></select>');
        this.btnTop = $('<span class="pagehome"><a>首页</a></span>');
        this.btnPrev = $('<span class="pageup"><a>上一页</a></span>');
        this.btnNext = $('<span class="pagedown"><a>下一页</a></span>');
        this.btnEnd = $('<span class="pagehome"><a>尾页</a></span>');

        this.btnTop.on('click', this.topClick());
        this.btnPrev.on('click', this.prevClick());
        this.btnNext.on('click', this.nextClick());
        this.btnEnd.on('click', this.endClick());
        this.pageSelect.on('change', this.selectChange());

        $('#' + placeHolder).addClass('page_box')
            .append(this.pageSelect)
            .append(this.btnEnd)
            .append(this.btnNext)
            .append(this.btnPrev)
            .append(this.btnTop);
    };

    youQiu.Pager.prototype = {
        topClick: function () {
            var pager = this;
            return function () {
                if (!$(this).attr('disabled')) {
                    pager.pageInfo.pageIndex = 1;
                    pager.getPageData();
                }
            };
        },
        prevClick: function () {
            var pager = this;
            return function () {
                if (!$(this).attr('disabled')) {
                    pager.pageInfo.pageIndex -= 1;
                    pager.getPageData();
                }
            };
        },
        nextClick: function () {
            var pager = this;
            return function () {
                if (!$(this).attr('disabled')) {
                    pager.pageInfo.pageIndex += 1;
                    pager.getPageData();
                }
            };
        },
        endClick: function () {
            var pager = this;
            return function () {
                if (!$(this).attr('disabled')) {
                    pager.pageInfo.pageIndex = pager.pageInfo.pageCount;
                    pager.getPageData();
                }
            };
        },
        selectChange: function () {
            var pager = this;
            return function () {
                pager.pageInfo.pageIndex = $(this).val();
                pager.getPageData();
            };
        },
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

            this.pageSelect.html('');
            var pageList = [];
            for (var i = 1; i <= this.pageInfo.pageCount; i++) {
                pageList.push('<option value="' + i + '">' + i + '/' + this.pageInfo.pageCount + '</option>');
            }
            this.pageSelect.html(pageList.join('')).val(this.pageInfo.pageIndex);

            this.btnTop.removeAttr('disabled', 'disabled');
            this.btnPrev.removeAttr('disabled', 'disabled');
            this.btnNext.removeAttr('disabled', 'disabled');
            this.btnEnd.removeAttr('disabled', 'disabled');
            if (this.pageInfo.pageIndex == 1) {
                this.btnTop.attr('disabled', 'disabled');
                this.btnPrev.attr('disabled', 'disabled');
            }
            if (this.pageInfo.pageIndex == this.pageInfo.pageCount) {
                this.btnNext.attr('disabled', 'disabled');
                this.btnEnd.attr('disabled', 'disabled');
            }
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
            //            if (!this.init) {
            //                this.getPageData();
            //            }
        },
        refresh: function () {
            this.getPageData();
        }
    };


})(jQuery);