(function ($) {
    $.validator.addMethod("cellphone", function (value, element) {
        return this.optional(element) || /^(13[0-9]|15[0-9]|18[0-9])\d{8}$/i.test(value);
    });

    $.validator.addMethod("emailOrCellphone", function (value, element) {
        return this.optional(element) || /^(13[0-9]|15[0-9]|18[0-9])\d{8}$/i.test(value) ||
            /^((([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+(\.([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+)*)|((\x22)((((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(([\x01-\x08\x0b\x0c\x0e-\x1f\x7f]|\x21|[\x23-\x5b]|[\x5d-\x7e]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(\\([\x01-\x09\x0b\x0c\x0d-\x7f]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))))*(((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(\x22)))@((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))$/i.test(value);
    });

    $.validator.addMethod('charAndNumber', function (value, element) {
        return this.optional(element) || /^([a-zA-Z0-9]+)$/.test(value);
    });

    $.validator.addMethod('chineseAndCharAndNumber', function (value, element) {
        return this.optional(element) || /^([0-9a-zA-Z\u4e00-\u9fa5]+)$/g.test(value);
    });

    $.validator.addMethod('notSpace', function (value, element) {
        return this.optional(element) || /^([\S]+)$/.test(value);
    });

    $.validator.addMethod('chineseAndCharAndNumberUnderscore', function (value, element) {
        return this.optional(element) || /^([\w\u4e00-\u9fa5]+)$/g.test(value);
    });

    $.validator.addMethod('identityCard', function (value, element) {
        return this.optional(element) || /^(\d{15}|\d{17}[\dXx])$/g.test(value);
    });

    $.validator.addMethod('cellPhoneAndMobilePhone', function (value, element) {
        return this.optional(element) || /^(((0\d{2,3})-)(\d{7,8})(-(\d{1,4}))|(\d{7,8})(-(\d{1,4}))|(\d{7,8})|((0\d{2,3})-)(\d{7,8}))?$/g.test(value) || /^(13[0-9]|15[0-9]|18[0-9])\d{8}$/i.test(value);
    });
})(jQuery);