using System;
using CommandSystem;
using Exiled.Permissions.Extensions;

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
            if (!sender.CheckPermission("tiny_event"))
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
