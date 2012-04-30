(function ($) {
    if (!$.outerHTML) {
        $.extend({
            outerHTML: function (ele) {
                var $return = undefined;
                if (ele.length === 1) {
                    $return = ele[0].outerHTML;
                }
                else if (ele.length > 1) {
                    $return = {};
                    ele.each(function (i) {
                        $return[i] = $(this)[0].outerHTML;
                    })
                };
                return $return;
            }
        });
        $.fn.extend({
            outerHTML: function () {
                return $.outerHTML($(this));
            }
        });
    }
})(jQuery);