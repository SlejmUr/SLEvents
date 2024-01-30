using Exiled.API.Features;
using System;

namespace BlackOut
{
    internal class Main : Plugin<Config>
    {
        public static Main Instance { get; private set; }
        #region Plugin Info
        public override string Author => "SlejmUr";
        public override string Name => "BlackOut";
        public override string Prefix => "BlackOut";
        public override Version Version => new Version(0,1);
        public Eventers Eventers;
        #endregion

        public override void OnEnabled()
        {;
            Instance = this;
            Eventers = new Eventers();
            Exiled.Events.Handlers.Server.RoundStarted += Eventers.OnRoundStart;
            Exiled.Events.Handlers.Player.Spawned += Eventers.Player_Spawned;
            Exiled.Events.Handlers.Server.RespawningTeam += Eventers.Respawning;
            Exiled.Events.Handlers.Player.PickingUpItem += Eventers.Player_Pickup;
            Exiled.Events.Handlers.Warhead.Starting += Eventers.Warhead_Start;
            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            Instance = this;
            Eventers = new Eventers();
            Exiled.Events.Handlers.Server.RoundStarted -= Eventers.OnRoundStart;
            Exiled.Events.Handlers.Server.RespawningTeam -= Eventers.Respawning;
            Exiled.Events.Handlers.Player.Spawned -= Eventers.Player_Spawned;
            Exiled.Events.Handlers.Player.PickingUpItem -= Eventers.Player_Pickup;
            Exiled.Events.Handlers.Warhead.Starting -= Eventers.Warhead_Start;
            base.OnDisabled();
        }
    }
}
