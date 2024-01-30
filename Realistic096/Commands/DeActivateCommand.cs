using System;
using CommandSystem;

namespace Realistic096.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class DeActivateCommand : ICommand
    {
        public string Command => "DeActivateRealistic096";

        public string[] Aliases => new string[] { "dr096", "norealistic096", "norealshyguy" };

        public string Description => "DeActivate Realistic SCP-096 Event";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!sender.CheckPermission( PlayerPermissions.GameplayData ))
            {
                response = "You dont have permission to deactivate the event!";
                return false;
            }

            Main.Instance.Config.IsEnabled = false;

            response = "Event Deactivated";
            return true;
        }
    }
}
