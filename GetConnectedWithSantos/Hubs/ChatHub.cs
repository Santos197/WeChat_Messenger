using GetConnectedWithSantos.Dtos;
using GetConnectedWithSantos.Services;
using Microsoft.AspNetCore.Server.HttpSys;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace GetConnectedWithSantos.Hubs
{
    public class ChatHub:Hub
    {
        private readonly ChatService _chatService;
        public ChatHub(ChatService chatService)
        {
            _chatService = chatService;      
        }

        public override async Task OnConnectedAsync()
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, "WeChat");
            await Clients.Caller.SendAsync("UserConnected");
        }
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, "WeChat");
            var user = _chatService.GetUserByConnectionId(Context.ConnectionId);
            _chatService.RemoveUserFromList(user);
            await DisplayOnlineUser();
            await base.OnDisconnectedAsync(exception);

        }

        public async Task AddUserConnectionId(string name)
            {
            _chatService.AddUserConnectionId(name, Context.ConnectionId);
             await DisplayOnlineUser();
        }

        public async Task ReceiveMessage(MessageDto message)
        {
            await Clients.Group("WeChat").SendAsync("NewMessage", message);
        }

        public async Task CreatePrivateChat(MessageDto message)
        {
            string privateGroupName = GetPrivateGroupName(message.From, message.To);
            await Groups.AddToGroupAsync(Context.ConnectionId, privateGroupName);
            var toConnectionId = _chatService.GetConnectionIdByUser(message.To);
            await Groups.AddToGroupAsync(toConnectionId, privateGroupName);
            //opeing private chatbox for other end user
            await Clients.Client(toConnectionId).SendAsync("OpenPrivateChat", message);

        }

        public async Task ReceivePrivateMessage(MessageDto message)
        {
            string privateGroupName = GetPrivateGroupName(message.From, message.To);
            await Clients.Group(privateGroupName).SendAsync("NewPrivateMessage", message);

        }

        public async Task RemovePrivateChat(string from, string to)
        {
            string privateGroupName= GetPrivateGroupName(from, to);
            await Clients.Group(privateGroupName).SendAsync("ClosePrivateChat");

            await Groups.RemoveFromGroupAsync(Context.ConnectionId, privateGroupName);
            var toConnectionId = _chatService.GetConnectionIdByUser(to);
            await Groups.RemoveFromGroupAsync(toConnectionId, privateGroupName);

        }



        private async Task DisplayOnlineUser()
        {
            var onLineUsers = _chatService.GetOnlineUsers();
            await Clients.Groups("WeChat").SendAsync("OnlineUsers", onLineUsers);
        }

        private string GetPrivateGroupName(string from,string to)
        {
            var stringCompare = string.CompareOrdinal(from, to) < 0;
            return stringCompare ? $"{from}={to}" : $"{to}={from}";
        }
    }
}
