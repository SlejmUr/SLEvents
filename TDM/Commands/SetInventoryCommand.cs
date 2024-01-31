using System;
using CommandSystem;

namespace TDM.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class SetInventoryCommand : ICommand
    {
        public string Command => "SetInventoryTDM";

        public string[] Aliases => new string[] { "setinvtdm" };

        public string Description => "Set Inventory for TDM Event";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!sender.CheckPermission( PlayerPermissions.GameplayData ))
            {
                response = "You dont have permission to deactivate the event!";
                return false;
            }

            

            response = "Currently not works!";
            return true;
        }
    }
}
