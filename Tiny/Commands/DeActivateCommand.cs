using System;
using CommandSystem;

namespace Tiny.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class DeActivateCommand : ICommand
    {
        public string Command => "DeActivateTiny";

        public string[] Aliases => new string[] { "detiny" };

        public string Description => "DeActivate Tiny Event";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!sender.CheckPermission( PlayerPermissions.GameplayData ))
            {
                response = "You dont have permission to deactivate the event!";
                return false;
            }

            Main.Instance.Config.EventEnabled = false;

            response = "Event Deactivated";
            return true;
        }
    }
}
