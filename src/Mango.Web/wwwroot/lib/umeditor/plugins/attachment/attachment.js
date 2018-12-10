var result = {
    title: '',
    url: '',
    password:''
}
window.onload = function () {
    parent.plugins['attachment'].DataObj = result;
}
//监听附件标题输入事件
function MonitorTitleInput() {
    result.title = $("#Title").val();
    parent.plugins['attachment'].DataObj = result;
}
//监听附件下载地址输入事件
function MonitorLinkInput() {
    result.url = $("#LinkUrl").val();
    parent.plugins['attachment'].DataObj = result;
}
//监听附件提取密码输入事件
function MonitorPasswordInput() {
    result.password = $("#Password").val();
    parent.plugins['attachment'].DataObj = result;
}