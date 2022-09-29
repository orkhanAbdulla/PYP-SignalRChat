
var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();
connection.start()

document.getElementById("sendButton").addEventListener("click", function () {
    var message = document.getElementById("messageInput").value;
    connection.invoke("SendMessage", message)
});
connection.on("ReceiveMessage", function (userName,message,date) {
    var li = document.createElement("li");
    li.innerHTML = `${date} ${userName} says: ${message}`;
    document.getElementById("messagesList").appendChild(li);
})
connection.on("UserConnected", function (Id) {
    let elm = document.getElementById(Id)
    elm.classList.remove("bg-offline")
    elm.classList.add("bg-online")
})
connection.on("UserDisConnected", function (Id) {
    let elm = document.getElementById(Id)
    elm.classList.remove("bg-online")
    elm.classList.add("bg-offline")
})

