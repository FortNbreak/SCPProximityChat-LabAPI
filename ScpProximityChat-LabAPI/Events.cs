using LabApi.Events.Arguments.PlayerEvents;
using LabApi.Events.CustomHandlers;
using LabApi.Features.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScpProximityChat_LabAPI
{
    public class Events : CustomEventsHandler
    {
        public override void OnServerRoundRestarted()
        {
            Handler.ToggledPlayers.Clear();
        }

        public override void OnPlayerChangedRole(PlayerChangedRoleEventArgs ev)
        {
            if (!ScpProximityChat.Instance.Config.SendBroadcastOnRoleChange)
                return;

            if (!ScpProximityChat.Instance.Config.AllowedRoles.Contains(ev.NewRole.RoleTypeId))
                return;

            if (ev.Player.Team == PlayerRoles.Team.SCPs)
                ev.Player.SendBroadcast(ScpProximityChat.Instance.Config.BroadcastMessage, ScpProximityChat.Instance.Config.BroadcastDuration);
        }
    }
}
