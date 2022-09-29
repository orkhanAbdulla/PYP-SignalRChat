using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using PYP_SignalRPrictice.DAL;
using PYP_SignalRPrictice.Models;

namespace PYP_SignalRPrictice.HUBs
{
    public class ChatHub:Hub
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly ChatContext _chatContext; 
        public ChatHub(IHttpContextAccessor contextAccessor, ChatContext chatContext) 
        { 
            _contextAccessor = contextAccessor; 
            _chatContext = chatContext;
        }
        public async Task SendMessage(string message)
        {
            string userName = _contextAccessor.HttpContext.Session.GetString("Identity"); 
            User user = _chatContext.Users.FirstOrDefault(x => x.Username.ToLower() == userName.ToLower()); 
            if (user != null) 
            {
                await Clients.All.SendAsync("ReceiveMessage", user.Username, message, DateTime.Now.ToString("dd MM yyyy HH:hh"));
                //if (user.ConnectionId != null) 
                //{ 
                //} 
            }
        }
        public async override Task OnConnectedAsync()
        {
            string userName = _contextAccessor.HttpContext.Session.GetString("Identity");
            User user = _chatContext.Users.FirstOrDefault(x => x.Username.ToLower() == userName.ToLower());
            if (user != null)
            {
                user.ConnectionId = Context.ConnectionId;
                _chatContext.SaveChanges();
                await Clients.All.SendAsync("UserConnected", user.Id);
            }
        }
        public async override Task OnDisconnectedAsync(Exception exception)
        {
            string userName = _contextAccessor.HttpContext.Session.GetString("Identity");
            User user = _chatContext.Users.FirstOrDefault(x => x.Username.ToLower() == userName.ToLower());
            if (user != null)
            {
                user.ConnectionId = null;
                _chatContext.SaveChanges();
                await Clients.All.SendAsync("UserDisConnected", user.Id);
            }
        }
    }
}
