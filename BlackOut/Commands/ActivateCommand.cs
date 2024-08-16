using System;
using CommandSystem;
using Exiled.Permissions.Extensions;

namespace BlackOut.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class ActivateCommand : ICommand
    {
        public string Command => "ActivateBlackOut";

        public string[] Aliases => new string[] { "abo" };

        public string Description => "Activate BlackOut Event";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!sender.CheckPermission("blackout_event"))
            {
                response = "You dont have permission to activate the event!";
                return false;
            }

            Main.Instance.Config.EventEnabled = true;

            response = "Event Activated!";
            return true;
        }
    }
}
