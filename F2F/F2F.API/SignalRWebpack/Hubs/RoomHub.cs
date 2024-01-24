using F2F.BLL.Services;
using Microsoft.AspNetCore.Razor.TagHelpers;
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
        private readonly IMeetingParticipantService _meetingParticipantService;
        private readonly IRoomService _roomService;

        public RoomHub(
            IMeetingParticipantService meetingParticipantService,
            IRoomService roomService
        )
        {
            _meetingParticipantService = meetingParticipantService;
            _roomService = roomService;
        }

        public async Task JoinRoom(string username, Guid meetingId)
        {
            var userInfo = new UserInfo()
            {
                userName = username,
                connectionId = Context.ConnectionId,
            };
            var otherParticipants = await _meetingParticipantService.GetByMeetingIdAsync(meetingId);
            await _meetingParticipantService.InsertAsync(
                new DLL.Entities.MeetingParticipant()
                {
                    ParticipantId = Context.ConnectionId,
                    UserName = username,
                    MeetingId = meetingId,
                }
            );
            await Clients
                .Clients(otherParticipants.Select(x => x.ParticipantId))
                .SendAsync("onJoinRoom", JsonSerializer.Serialize(userInfo));
        }

        public async Task InformJoinedUser(string user)
        {
            var myInfo = await _meetingParticipantService.GetByConnectionId(Context.ConnectionId);
            var userInfo = new UserInfo()
            {
                userName = myInfo?.UserName,
                connectionId = Context.ConnectionId,
            };
            await Clients
                .Client(user)
                .SendAsync("onInformJoinedUser", JsonSerializer.Serialize(userInfo));
        }

        public async Task SendSignal(string user)
        {
            await Clients.Client(user).SendAsync("onSendSignal", Context.ConnectionId);
        }

        public override async Task OnDisconnectedAsync(System.Exception exception)
        {
            var result = await _meetingParticipantService.DeleteByParticipantIdAsync(
                Context.ConnectionId
            );
            if (!result.IsAnyoneLeft && result.MeetingId.HasValue)
            {
                await _roomService.CloseRoom(result.MeetingId.Value);
            }
            await Clients.All.SendAsync("onUserDisconnect", Context.ConnectionId);
        }

        public async Task LeaveRoom(string user)
        {
            await Clients.All.SendAsync("userLeavedRoom", user);
        }
    }
}
