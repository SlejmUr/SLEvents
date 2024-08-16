using System;
using CommandSystem;
using Exiled.Permissions.Extensions;

namespace Realistic096.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class ActivateCommand : ICommand
    {
        public string Command => "ActivateRealistic096";

        public string[] Aliases => new string[] { "ar096", "realistic096", "realshyguy" };

        public string Description => "Activate Realistic SCP-096 Event";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!sender.CheckPermission("realistic096_event"))
            {
                response = "You dont have permission to activate the event!";
                return false;
            }

            Main.Instance.Config.EventEnabled = true;

            response = "Event Activated";
            return true;
        }
    }
}
