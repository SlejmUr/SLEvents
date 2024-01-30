using Exiled.API.Features;
using System;

namespace Tiny
{
    internal class Main : Plugin<Config>
    {
        public static Main Instance { get; private set; }
        #region Plugin Info
        public override string Author => "SlejmUr";
        public override string Name => "Tiny";
        public override string Prefix => "Tiny";
        public override Version Version => new Version(0,1);
        public Eventers Eventers;
        #endregion

        public override void OnEnabled()
        {;
            Instance = this;
            Eventers = new Eventers();
            Exiled.Events.Handlers.Server.RoundStarted += Eventers.OnRoundStart;
            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            Instance = this;
            Eventers = new Eventers();
            Exiled.Events.Handlers.Server.RoundStarted -= Eventers.OnRoundStart;
            base.OnDisabled();
        }
    }
}
