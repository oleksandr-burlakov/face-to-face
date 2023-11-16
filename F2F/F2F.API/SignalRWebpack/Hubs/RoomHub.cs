using F2F.BLL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Text.Json;

namespace F2F.API.SignalRWebpack.Hubs
{
    public class UserInfo
    {
        public string userName { get; set; }
        public string connectionId { get; set; }
        public Guid roomId { get; set; }
    }

    public class RoomHub : Hub
    {
        private readonly IClaimService _claimService;
        private readonly IUserService _userService;

        public RoomHub(IClaimService claimService, IUserService userService)
        {
            _claimService = claimService;
            _userService = userService;
        }

        public async Task JoinRoom(string username)
        {
            var userInfo = new UserInfo()
            {
                userName = username,
                connectionId = Context.ConnectionId,
            };
            await Clients.Others.SendAsync("onJoinRoom", JsonSerializer.Serialize(userInfo));
        }

        public async Task InformJoinedUser(string username, string user)
        {
            var userInfo = new UserInfo()
            {
                userName = username,
                connectionId = Context.ConnectionId,
            };
            await Clients
                .Client(user)
                .SendAsync("onInformJoinedUser", JsonSerializer.Serialize(userInfo));
        }

        public async Task SendSignal(string signal, string user, bool isReturn)
        {
            await Clients
                .Client(user)
                .SendAsync("onSendSignal", Context.ConnectionId, signal, isReturn);
        }

        public override async Task OnDisconnectedAsync(System.Exception exception)
        {
            await Clients.All.SendAsync("onUserDisconnect", Context.ConnectionId);
            await base.OnDisconnectedAsync(exception);
        }

        public async Task LeaveRoom(string user)
        {
            await Clients.All.SendAsync("userLeavedRoom", user);
        }
    }
}
