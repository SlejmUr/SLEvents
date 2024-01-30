using Exiled.API.Features;
using System;

namespace Realistic096
{
    internal class Main : Plugin<Config>
    {
        public static Main Instance { get; private set; }
        #region Plugin Info
        public override string Author => "SlejmUr";
        public override string Name => "Realistic096";
        public override string Prefix => "Realistic096";
        public override Version Version => new Version(0,1);
        public Eventers Eventers;
        #endregion

        public override void OnEnabled()
        {;
            Instance = this;
            Eventers = new Eventers();
            Exiled.Events.Handlers.Server.RoundStarted += Eventers.OnRoundStart;
            Exiled.Events.Handlers.Scp096.CalmingDown += Eventers.SCP_096_CalmingDown;
            Exiled.Events.Handlers.Player.Hurting += Eventers.Player_Hurt;
            Exiled.Events.Handlers.Player.Died += Eventers.Player_Died;
            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            Instance = this;
            Eventers = new Eventers();
            Exiled.Events.Handlers.Server.RoundStarted -= Eventers.OnRoundStart;
            Exiled.Events.Handlers.Scp096.CalmingDown -= Eventers.SCP_096_CalmingDown;
            Exiled.Events.Handlers.Player.Hurting -= Eventers.Player_Hurt;
            Exiled.Events.Handlers.Player.Died -= Eventers.Player_Died;
            base.OnDisabled();
        }
    }
}
