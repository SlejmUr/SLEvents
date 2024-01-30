﻿using System;
using CommandSystem;

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
            if (!sender.CheckPermission( PlayerPermissions.GameplayData ))
            {
                response = "You dont have permission to activate the event!";
                return false;
            }

            Main.Instance.Config.IsEnabled = true;

            response = "Event Activated!";
            return true;
        }
    }
}
