//定义插件容器
var plugins = new Array();
//注册插件
function registerPlugins() {
    //添加图片插件
    plugins['pimage'] = new function () {
        //注册界面事件
        var result = new Object();
        result.arrImage = new Array();
        //注册指定事件
        UM.registerUI('pimage',
            function (name) {
                var me = this;
                var $btn = $.eduibutton({
                    icon: 'image',
                    click: function () {
                        var layIndex = layer.open({
                            type: 2,
                            title: '图片',
                            maxmin: false,
                            shadeClose: true, //点击遮罩关闭层
                            area: ['700px', '320px'],
                            content: '/lib/umeditor/plugins/image/image.html',
                            btn: ['确定', '取消'],
                            btn1: function (index) {
                                for (var i = 0; i < result.arrImage.length; i++) {
                                    var html = '<img src="' + result.arrImage[i] + '!700xauto" alt="缩略图" />';
                                    me.execCommand('insertHtml', html);
                                }
                                //关闭当前弹窗 并且清空当前数据容器
                                layer.close(layIndex);
                                result.arrImage.splice(0, result.arrImage.length);
                            },
                            btn2: function (index) {
                                //关闭当前弹窗 并且清空当前数据容器
                                layer.close(layIndex);
                                result.arrImage.splice(0, result.arrImage.length);
                            }
                        });
                    },
                    title: '图片'
                });

                me.addListener('selectionchange', function () {
                    //切换为不可编辑时，把自己变灰
                    var state = this.queryCommandState(name);
                    $btn.edui().disabled(state == -1).active(state == 1)
                });
                return $btn;
            }
        );
        return result;
    };
    //添加代码插件
    plugins['code'] = new function () {
        //注册界面事件
        var result = new Object();
        result.CodeObj = {};
        //注册指定事件
        UM.registerUI('code',
            function (name) {
                var me = this;
                var $btn = $.eduibutton({
                    icon: 'source',
                    click: function () {
                        var layIndex = layer.open({
                            type: 2,
                            title: '源代码',
                            maxmin: false,
                            shadeClose: true, //点击遮罩关闭层
                            area: ['620px', '380px'],
                            content: '/lib/umeditor/plugins/code/code.html',
                            btn: ['确定', '取消'],
                            btn1: function (index) {
                                if (result.CodeObj.codeContent != '') {
                                    //把内容插入编辑器
                                    var html = '<pre  class="prettyprint"><code class="language-' + result.CodeObj.codeType + '">' + result.CodeObj.codeContent + '</code></pre>';
                                    me.execCommand('insertHtml', html);
                                }
                                //关闭弹窗并且清空当次内容
                                layer.close(layIndex);
                                result.CodeObj = {};
                                //UM.getEditor('container').setContent(html, true);
                            },
                            btn2: function (index) {
                                //关闭当前弹窗 并且清空当前数据容器
                                layer.close(layIndex);
                                result.CodeObj = {};
                            }
                        });
                    },
                    title: '源代码'
                });

                me.addListener('selectionchange', function () {
                    //切换为不可编辑时，把自己变灰
                    var state = this.queryCommandState(name);
                    $btn.edui().disabled(state == -1).active(state == 1)
                });
                return $btn;
            }
        );
        return result;
    };
    //添加代码插件
    //plugins['attachment'] = new function () {
    //    //注册界面事件
    //    var result = new Object();
    //    result.DataObj = {};
    //    //注册指定事件
    //    UM.registerUI('attachment',
    //        function (name) {
    //            var me = this;
    //            var $btn = $.eduibutton({
    //                icon: 'attachment',
    //                click: function () {
    //                    var layIndex = layer.open({
    //                        type: 2,
    //                        title: '附件',
    //                        maxmin: false,
    //                        shadeClose: true, //点击遮罩关闭层
    //                        area: ['600px', '320px'],
    //                        content: '/lib/umeditor/plugins/attachment/attachment.html',
    //                        btn: ['确定', '取消'],
    //                        btn1: function (index) {
                                
    //                            if (result.DataObj.title != "" && result.DataObj.url != "") {
    //                                var html = '<div class="card" style="margin-bottom:5px;padding:0px;">';
    //                                html += '<div class="col-lg-12" style = "margin-top:10px;padding-left: 0px;">';
    //                                html += '<div class="col-lg-3" style="width:55px;">';
    //                                html += '<img src="/images/download.png" />';
    //                                html += '</div>';
    //                                html += '<div class="col-lg-8">';
    //                                html += '<div class="row">';
    //                                html += result.DataObj.title;
    //                                html += '</div>';
    //                                html += '<div class="row">';
    //                                html += '提取密码:<strong>' + (result.DataObj.password != "" ? result.DataObj.password:"")+'</strong>';
    //                                html += '</div>';
    //                                html += '</div>';
    //                                html += '<div class="col-sm-1">';
    //                                html += '<a href="' + result.DataObj.url + '"  _href="' + result.DataObj.url + '" class="btn btn-link"  target="_blank"> <i class="icon icon-download-alt icon-2x"></i></a>';
    //                                html += '</div>';
    //                                html += '</div>';
    //                                html += '</div>';
    //                                me.execCommand('insertHtml', html);
    //                            }
    //                            //关闭当前弹窗 并且清空当前数据容器
    //                            layer.close(layIndex);
    //                            result.DataObj = {};
    //                        },
    //                        btn2: function (index) {
    //                            //关闭当前弹窗 并且清空当前数据容器
    //                            layer.close(layIndex);
    //                            result.DataObj = {};
    //                        }
    //                    });
    //                },
    //                title: '附件'
    //            });

    //            me.addListener('selectionchange', function () {
    //                //切换为不可编辑时，把自己变灰
    //                var state = this.queryCommandState(name);
    //                $btn.edui().disabled(state == -1).active(state == 1)
    //            });
    //            return $btn;
    //        }
    //    );
    //    return result;
    //};
}

