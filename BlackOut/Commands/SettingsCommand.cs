using CommandSystem;
using System;
using System.Linq;

namespace BlackOut.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class SettingsCommand : ICommand
    {
        public string Command => "SettingsBlackOut";

        public string[] Aliases => new string[] { "sbo" };

        public string Description => "Settings for BlackOut Event";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!sender.CheckPermission(PlayerPermissions.GameplayData))
            {
                response = "You dont have permission to activate the event!";
                return false;
            }

            if (arguments.Contains("list"))
            {
                response = "Example Arguments:\n Lotus or Fallout or JDUFF (can be lowercase)";
                return false;
            }
            bool IsSet = false;
            if (arguments.Count == 1 && !arguments.Contains("list"))
            {
                foreach (var item in arguments)
                {
                    var arg = item.ToLower();
                    if (arg == "lotus")
                    {
                        Main.Instance.Config.Lotus_Round = !Main.Instance.Config.Lotus_Round;
                        IsSet = true;
                    }
                    if (arg == "fallout")
                    {
                        Main.Instance.Config.Fallout_Round = !Main.Instance.Config.Fallout_Round;
                        IsSet = true;
                    }
                    if (arg == "jduff")
                    {
                        Main.Instance.Config.JDuff_Round = !Main.Instance.Config.JDuff_Round;
                        IsSet = true;
                    }
                }

            }
            if (IsSet)
            {
                response = "Settings Applied!";
                return true;
            }
            response = "Example Arguments:\n Lotus or Fallout or JDUFF (can be lowercase)\n list";
            return false;
        }
    }
}
