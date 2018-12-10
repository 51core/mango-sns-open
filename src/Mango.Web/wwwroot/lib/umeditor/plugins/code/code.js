var result = {
    codeType: 'csharp',
    codeContent:''
}
window.onload = function () {
    parent.plugins['code'].CodeObj = result;
}
//监听代码内容输入事件
function MonitorContentsInput() {
    result.codeContent =  html2Escape($("#CodeContents").val());
    parent.plugins['code'].CodeObj = result;
}
//监听代码类型选择事件
function MonitorTypeChanage() {
    result.codeType = $("#CodeType").val();
    parent.plugins['code'].CodeObj = result;
}
function html2Escape(sHtml) {
    return sHtml.replace(/[<>&"]/g, function (c) { return { '<': '&lt;', '>': '&gt;', '&': '&amp;', '"': '&quot;' }[c]; });
}
