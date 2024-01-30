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
            string whatSet = "";
            if (arguments.Count == 1 && !arguments.Contains("list"))
            {
                foreach (var item in arguments)
                {
                    var arg = item.ToLower();
                    if (arg == "lotus")
                    {
                        Main.Instance.Config.Lotus_Round = !Main.Instance.Config.Lotus_Round;
                        Main.Instance.Config.Fallout_Round = false;
                        Main.Instance.Config.JDuff_Round = false;
                        IsSet = true;
                        if (Main.Instance.Config.Lotus_Round)
                            whatSet = " - Lotus Round";
                        else
                            whatSet = " - Lotus Round Disabled";
                    }
                    if (arg == "fallout")
                    {
                        Main.Instance.Config.Fallout_Round = !Main.Instance.Config.Fallout_Round;
                        Main.Instance.Config.Lotus_Round = false;
                        Main.Instance.Config.JDuff_Round = false;
                        IsSet = true;
                        if (Main.Instance.Config.Fallout_Round)
                            whatSet = " - Fallout Round";
                        else
                            whatSet = " - Fallout Round Disabled";
                    }
                    if (arg == "jduff")
                    {
                        Main.Instance.Config.JDuff_Round = !Main.Instance.Config.JDuff_Round;
                        Main.Instance.Config.Lotus_Round = false;
                        Main.Instance.Config.Fallout_Round = false;
                        IsSet = true;
                        if (Main.Instance.Config.JDuff_Round)
                            whatSet = " - JDUFF Round";
                        else
                            whatSet = " - JDUFF Round Disabled";
                    }
                    if (arg == "default")
                    {
                        Main.Instance.Config.JDuff_Round = false;
                        Main.Instance.Config.Lotus_Round = false;
                        Main.Instance.Config.Fallout_Round = false;
                        IsSet = true;
                        whatSet = " - Default Round";
                    }
                }

            }
            if (IsSet)
            {
                response = "Settings Applied!" + whatSet;
                return true;
            }
            response = "Example Arguments:\n Lotus or Fallout or JDUFF or Default (can be lowercase)\n list";
            return false;
        }
    }
}
