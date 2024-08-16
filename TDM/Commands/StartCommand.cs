using System;
using CommandSystem;
using Exiled.Permissions.Extensions;

namespace TDM.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class StartCommand : ICommand
    {
        public string Command => "StartTDM";

        public string[] Aliases => new string[] { "stdm" };

        public string Description => "Start TDM Event";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!sender.CheckPermission("tdm_event"))
            {
                response = "You dont have permission to Start the event!";
                return false;
            }

            Eventers.StartRound();

            response = "Event Started!";
            return true;
        }
    }
}
