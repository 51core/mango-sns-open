/*
 图片上传组件(又拍云专用)
 */
tinymce.PluginManager.add('imageupload', function (editor) {

    function selectLocalImages() {
        var dom = editor.dom;
        var input_f = $('<input type="file" name="file" accept="image/jpg,image/jpeg,image/png,image/gif" multiple="multiple">');
        input_f.on('change', function () {
            //上传到又拍云
            var file = input_f[0].files[0];
            console.log(file);
            $.ajax({
                type: 'post',
                url: '/File/UPYun',
                data: 'fileName=' + file.name,
                success: function (result) {
                    if (result != '') {
                        var json = JSON.parse(result);
                        var form = new FormData();
                        form.append('file', file);
                        form.append('authorization', json.Signature);
                        form.append('policy', json.Policy);
                        //上传文件
                        $.ajax({
                            type: 'post',
                            url: editor.settings.upload_image_url,
                            data: form,
                            processData: false,
                            contentType: false,
                            success: function (res) {
                                editor.selection.setContent(dom.createHTML('img', { src: editor.settings.file_server_url + json.Path }));
                            },
                            error: function (ex) {
                                cfg.error(ex);
                            }
                        });
                    }
                    else {
                        alert("文件上传失败");
                    }
                }
            });
        });
        input_f.click();
    }

    editor.addCommand("mceUploadImageEditor", selectLocalImages);

    editor.ui.registry.addButton('imageupload', {
        icon: 'image',
        tooltip: '图片上传',
        onAction: selectLocalImages
    });

    editor.ui.registry.addMenuItem('imageupload', {
        icon: 'image',
        text: '图片上传',
        context: 'tools',
        onAction: selectLocalImages
    });
});