using System;
using CommandSystem;

namespace TDM.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class DeActivateCommand : ICommand
    {
        public string Command => "DeActivateTDM";

        public string[] Aliases => new string[] { "detdm" };

        public string Description => "DeActivate TDM Event";

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
