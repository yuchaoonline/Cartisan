(function ($) {
    youQiu.Upload = function (options) {
        var obj = options.content;
        var appUrl = options.appUrl;
        var imageWarehouseUrl = options.imageWarehouseUrl;
        var imageValuesId = options.imageValuesId;

        var imagePanel = $('<div></div>');

        options.images = options.images || [];
        $.each(options.images, function (index, image) {
            imagePanel.append(generateImageHtml(image));
        });

        imagePanel.append('<div class="cleark"></div>');

        var buttonSpan = Math.floor(Math.random() * 1000 + 1);

        $(obj).append(imagePanel).append('<div><span id="' + buttonSpan + '"></span></div><p class="wrongicon"></p>');

        var swfUpload = new SWFUpload({
            //Backend Settings
            upload_url: imageWarehouseUrl + "Upload/UploadImage",
            post_params: {
                //AspNetSessionId: current.sessionId
                type: options.type,
                userId: youQiu.User.UserId || 0
            },

            // File Upload Settings
            file_size_limit: '5 MB',
            file_types: '*.jpg;*.jpeg;*.png;*.bmp',
            file_types_description: 'Web Image Files',
            file_upload_limit: options.limit,
            file_post_name: 'file',
            //prevent_swf_caching: false,


            // Button settings
            button_image_url: appUrl + 'Scripts/swfupload/XPButtonNoText_61x22.png',
            button_placeholder_id: buttonSpan,
            button_width: 63,
            button_height: 22,
            button_text: '<span class="button">上传</span>',
            button_text_style: '.button { width:61px; height:22px; line-height:22px; text-align:center; }',
            button_window_mode: SWFUpload.WINDOW_MODE.TRANSPARENT,

            // Flash Settings
            // flash_url: appUrl + 'Scripts/swfupload/swfupload.swf',
            flash_url: appUrl + 'Scripts/swfupload/swfupload_youqiu.swf',
            flash9_url: appUrl + 'Scripts/swfupload/swfupload_fp9.swf',

            custom_settings: {
                isIE: $.browser.msie,
                thumbnail_height: 800,
                thumbnail_width: 800,
                thumbnail_quality: 100
            },

            // Event Handler Settings

            swfupload_loaded_handler: function () {
                var stats = this.getStats();
                stats.successful_uploads = $(obj).find('.sctp').length;
                swfUpload.setStats(stats);
            },
            file_queued_handler: fileQueue,
            file_queue_error_handler: fileQueueError,
            file_dialog_start_handler: fileDialogStart,
            file_dialog_complete_handler: fileDialogComplete,
            upload_start_handler: uploadStart,
            upload_progress_handler: uploadProgress,
            upload_error_handler: uploadError,
            upload_success_handler: uploadSuccess,
            upload_complete_handler: uploadComplete,

            // Debug Settings
            debug: false
        });

        $(obj).find('.deleteImage').live('click', function (event) {
            event.preventDefault();
            var stats = swfUpload.getStats();
            stats.successful_uploads--;
            swfUpload.setStats(stats);

            var imageUrls = $('#' + imageValuesId);
            var urls = imageUrls.val().split(',');
            var strs = [];
            var fileName = $(this).attr('fileName');
            for (var i = 0; i < urls.length; i++) {
                if (urls[i] !== fileName) {
                    strs[strs.length] = urls[i];
                }
            }
            imageUrls.val(strs.join(','));

            $(this).parent().parent().parent().remove();
        });

        $('.cancelUpload').live('click', function (event) {
            event.preventDefault();
            swfUpload.cancelUpload($(this).attr('fileId'));

            $(this).parent().parent().remove();
        });

        lightBox();

        function uploadError(file, errorCode, message) {

        }

        function uploadProgress(file, bytesLoaded) {

        }

        function fileDialogComplete(numFilesSelected, numFilesQueued) {
            if (numFilesQueued > 0) {
                if (this.support.imageResize) {
                    this.startResizedUpload(this.getQueueFile(0).id, this.customSettings.thumbnail_width, this.customSettings.thumbnail_height, SWFUpload.RESIZE_ENCODING.JPEG, this.customSettings.thumbnail_quality, false);
                }
                else
                    this.startUpload();
            }
        }

        function uploadComplete() {
            if (this.getStats().files_queued > 0) {
                if (this.support.imageResize)
                    this.startResizedUpload(this.getQueueFile(0).id, this.customSettings.thumbnail_width, this.customSettings.thumbnail_height, SWFUpload.RESIZE_ENCODING.JPEG, this.customSettings.thumbnail_quality, false);
                else
                    this.startUpload();
            }
        }

        function fileDialogStart() {
            $(obj).find('.wrongicon').hide();
        }

        function fileQueueError(file, errorCode, message) {
            $(obj).find('.wrongicon').html('最多上传3张图片,每张图片不超过5M');
            $(obj).find('.wrongicon').show();
        }

        function fileQueue(file) {
            var image = $(['<div class="sctp" style="float: left; margin-right: 10px;" id="thumb_' + file.id + '">',
                '<div class="user_img_shangchuang">',
                  '<img src="', appUrl, 'Content/images/base/loading.gif" style="margin: 32px" width="32" height="32" alt=""/>',
                '</div>',
                '<div>处理中…</div>',
              '</div>'].join(''));
            image.insertBefore($(obj).first().find('.cleark')).fadeIn(500);
        }

        function uploadStart(file) {
            $('#thumb_' + file.id).children().last().html('上传中…&nbsp;&nbsp;&nbsp;&nbsp;<a fileId="' + file.id + '" class="cancelUpload">取消</a>');
        }

        function uploadSuccess(file, serverData) {
            var uploadResult = $.parseJSON(serverData);

            var image = {
                fileId: file.id,
                Id: uploadResult.Id,
                OriginalUrl: uploadResult.OriginalUrl,
                ThumbnailUrl: uploadResult.ThumbnailUrl
            };

            $('#thumb_' + file.id).html(generateImageHtml(image));
            var imageUrls = $('#' + imageValuesId);

            imageUrls.val((imageUrls.val() ? (imageUrls.val() + ',') : '') + uploadResult.Id);

            lightBox();
        }

        function lightBox() {
            $(obj).find('.user_img_shangchuang>a').lightBox({
                imageLoading: appUrl + 'Scripts/jquery-lightbox/images/lightbox-ico-loading.gif',
                imageBtnPrev: appUrl + 'Scripts/jquery-lightbox/images/lightbox-btn-prev.gif',
                imageBtnNext: appUrl + 'Scripts/jquery-lightbox/images/lightbox-btn-next.gif',
                imageBtnClose: appUrl + 'Scripts/jquery-lightbox/images/lightbox-btn-close.gif',
                imageBlank: appUrl + 'Scripts/jquery-lightbox/images/lightbox-blank.gif',
                containerBorderSize: 10,
                containerResizeSpeed: 400,
                txtImage: '',
                txtOf: '/'
            });
        }

        function generateImageHtml(image) {
            return $(['<div class="sctp">',
                '<div class="user_img_shangchuang" style="height: 96px">',
                '<a href="', image.OriginalUrl, '">',
                '<img src="', image.ThumbnailUrl, '" width="96" height="96" /></a></div>',
                '<div>',
                '<div class="sccg_gou"  style="float: left; padding-top:8px; ">',
                '<img src="', appUrl, 'Content/images/base/icon_gou.gif" width="16" height="16" alt=""/></div>',
                '<div class="sccg_text"  style="float: left;">',
                '上传成功!</div>',
                '<div class="sccg_ljt"  style="float: left; padding-top:8px; ">',
                '<a href="#" class="deleteImage" fileId="' + (image.fileId || '') + '" fileName="' + image.Id + '">',
                '<img src="', appUrl, 'Content/images/base/icon_ljt.gif" alt="删除" width="16" height="16" /></a></div>',
                '</div>'].join(''));
        }
    };



    $.fn.upload = function (options) {
        var defaults = {
            limit: '3'
        };

        options = $.extend(defaults, options);

        options.content = this;

        return new youQiu.Upload(options);
    };


})(jQuery);