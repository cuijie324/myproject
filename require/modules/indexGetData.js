
define(["zepto"], function ($) {

    function getData1() {
        $("#getData").html("我正在从后台获取数据……");
    }

    function getData2() {
        $("#getData2").html("22222222222222222……");
    }

    return {
        getData1: getData1,
        getData2: getData2
    }

});