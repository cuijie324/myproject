
define(["zepto"], function ($) {

    function Hi() {
        this.name = "买好茶上茶途网，世界级名品授权、100%正品保证！";
    }

    Hi.prototype.showMe = function () {

        $("#box").html(this.name);

    };

    return {
        Hi: Hi
    }

});