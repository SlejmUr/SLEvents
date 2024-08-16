using System;
using CommandSystem;
using Exiled.Permissions.Extensions;

namespace TDM.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class ActivateCommand : ICommand
    {
        public string Command => "ActivateTDM";

        public string[] Aliases => new string[] { "atdm" };

        public string Description => "Activate TDM Event";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!sender.CheckPermission("tdm_event"))
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
