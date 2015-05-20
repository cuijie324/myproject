(function () {
    var takePicture = document.querySelector("#take-picture"),
        showPicture = document.querySelector("#show-picture");

    var uploadImage = document.querySelector("#uploadImage");

    uploadImage.onclick = function () {
        alert("here");
        takePicture.click();
    };

    if (takePicture && showPicture) {
        // Set events
        takePicture.onchange = function (event) {
            // Get a reference to the taken picture or chosen file
            var files = event.target.files,
                file;
            if (files && files.length > 0) {
                file = files[0];
                try {
                    var $this = this;
                    $this.filename = file.name;
                    var options = {};
                    options.width = 100;

                    //压缩图片，也可以传入图片路径：lrz('../demo.jpg', ...
                    lrz(file, options, function(results) {
                        // 你需要的数据都在这里，可以以字符串的形式传送base64给服务端转存为图片。
                        //上传图片
                        results = null;
                        var response = {};
                        response.Data = "group1/M00/01/46/CgoBAVVZomChf5Y9AAAfr9PtSB8341.jpg";
                        var ele = $("#commentImage").find("img[src='../images/no-pic.png']").first();
                        ele.data("img", response.Data);
                        var url = response.Data;
                        var dotIndex = url.lastIndexOf(".");
                        var newUrl = url.substring(0, dotIndex) + "=180x180." + url.substring(dotIndex + 1, url.length);
                        ele.attr("src", "http://img01.chatu.com/" + newUrl).next().show();


                        //// Get window.URL object
                        //var URL = window.URL || window.webkitURL;

                        //// Create ObjectURL
                        //var imgURL = URL.createObjectURL(file);

                        //// Set img src to ObjectURL
                        //showPicture.src = imgURL;

                        //// Revoke ObjectURL after imagehas loaded
                        //showPicture.onload = function () {
                        //    URL.revokeObjectURL(imgURL);
                        //};
                    });
                } catch(e) {
                    try {
                        // Fallback if createObjectURL is not supported
                        var fileReader = new FileReader();
                        fileReader.onload = function(event) {
                            showPicture.src = event.target.result;
                        };
                        fileReader.readAsDataURL(file);
                    } catch(e) {
                        // Display error message
                        var error = document.querySelector("#error");
                        if (error) {
                            error.innerHTML = "Neither createObjectURL or FileReader are supported";
                        }
                    }
                }
            }
    };
}
})();
