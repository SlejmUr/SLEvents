using Exiled.API.Features;
using System;

namespace TDM
{
    internal class Main : Plugin<Config>
    {
        public static Main Instance { get; private set; }
        #region Plugin Info
        public override string Author => "SlejmUr";
        public override string Name => "TDM";
        public override string Prefix => "TDM";
        public override Version Version => new Version(0,2);
        public Eventers Eventers;
        #endregion

        public override void OnEnabled()
        {;
            Instance = this;
            Eventers = new Eventers();
            Exiled.Events.Handlers.Server.RoundStarted += Eventers.OnRoundStart;
            Exiled.Events.Handlers.Server.SelectingRespawnTeam += Eventers.Respawn;
            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            Instance = this;
            Eventers = new Eventers();
            Exiled.Events.Handlers.Server.RoundStarted -= Eventers.OnRoundStart;
            Exiled.Events.Handlers.Server.SelectingRespawnTeam -= Eventers.Respawn;
            base.OnDisabled();
        }
    }
}
