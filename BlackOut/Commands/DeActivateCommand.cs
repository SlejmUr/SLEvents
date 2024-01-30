using System;
using CommandSystem;

namespace BlackOut.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class DeActivateCommand : ICommand
    {
        public string Command => "DeActivateBlackOut";

        public string[] Aliases => new string[] { "debo" };

        public string Description => "DeActivate BlackOut Event";

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
