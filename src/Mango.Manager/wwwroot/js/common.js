//公共消息提示方法
function ShowMessage(msg) {
    layer.msg(msg, { time: 1500 });
}
//消息提示,并且跳转到专用地址
function ShowMessage(msg,url) {
    layer.msg(msg, { time: 1500 });
    setTimeout(function () {
        if (url == '') {
            window.location.href = window.location.href;
        }
        else {
            window.location.href = url;
        }
    },1500);
}