require.config({
    paths: {
        jquery: 'jquery-1.11.2.min'
    }
});
 
require(['jquery'], function($) {
    alert($().jquery);
});