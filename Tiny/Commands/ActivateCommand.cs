﻿using System;
using CommandSystem;
using Exiled.Permissions.Extensions;

namespace Tiny.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class ActivateCommand : ICommand
    {
        public string Command => "ActivateTiny";

        public string[] Aliases => new string[] { "atiny", "makeeveryonesmall" };

        public string Description => "Activate Tiny Event";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!sender.CheckPermission("tiny_event"))
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
