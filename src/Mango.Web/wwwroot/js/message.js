/// <reference path="../lib/signalr/dist/browser/signalr.min.js" />
//处理Signalr连接
var connection = new signalR.HubConnectionBuilder().withUrl("/MessageHub").build();
//绑定消息接收方法
connection.on("ReceiveMessage", function (message) {
    console.log(message);
    $("#msg_push_count").text(message);
});
//开始连接
connection.start().catch(function (err) {
    return console.error(err.toString());
});
