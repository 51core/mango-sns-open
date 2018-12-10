/// <reference path="../../../mango/file/config.js" />
/**
 * User: Jinqn
 * Date: 14-04-08
 * Time: 下午16:34
 * 上传图片对话框逻辑代码,包括tab: 远程图片/上传图片/在线图片/搜索图片
 */

(function () {
    //存储上传的图片列表
    window.onload = function () {
        //初始化上传
        $("#showImageFileUploadBtn").click(function () {
            $('#showInputImageFile').trigger('click');
        });

        var tempImageHtml = null;
        $.FlyUpload({
            url: '',
            id: 'showInputImageFile',
            auto: true,
            preview: function (url) {
                var html = '<img src="/images/loading.gif" alt=""  />';
                html = $(html);
                tempImageHtml = html;
                $("#showImageShareList").append(html);
            },
            success: function (url) {
                $(tempImageHtml).remove();
                $("#showImageShareList").append('<img src="' + commonConfig.domainName + url + '" style="width:120px;height:120px;" class="img-thumbnail" alt="缩略图">');
                parent.plugins['pimage'].arrImage.push(commonConfig.domainName + url);
            }
        });
    };
})();
