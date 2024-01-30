using CommandSystem;
using Exiled.API.Features;
using System;
using System.Linq;

namespace Realistic096.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class SettingsCommand : ICommand
    {
        public string Command => "SettingsRealistic096";

        public string[] Aliases => new string[] { "sr096", "realistic096settings", "setrealshyguy" };

        public string Description => "Settings for Realistic SCP-096 Event";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!sender.CheckPermission(PlayerPermissions.GameplayData))
            {
                response = "You dont have permission to activate the event!";
                return false;
            }

            if (arguments.Contains("list"))
            {
                response = "Example Arguments:\n 096Count=2\n SpawnLoot=true\n CanOtherSCPPlay=true\n SCPSToDClass=false";
                return false;
            }
            bool IsSet = false;
            if (arguments.Count > 1 && !arguments.Contains("list"))
            {
                foreach (var item in arguments)
                {
                    Log.Info(item);
                    if (item.Contains("="))
                    {
                        var splitted = item.Split('=');
                        var name = splitted[0];
                        var value = splitted[1];
                        if (name == "096Count" && int.TryParse(value, out var result))
                        {
                            // Currently doesnt supported!
                            //Main.Instance.Config.PlayableSCP096Count = result;
                            IsSet = true;
                        }
                        if (name == "SpawnLoot" && bool.TryParse(value, out var bool_result))
                        {
                            // Currently doesnt supported!
                            //Main.Instance.Config.SpawnLoot = bool_result;
                            IsSet = true;
                        }
                        if (name == "CanOtherSCPPlay" && bool.TryParse(value, out bool_result))
                        {
                            Main.Instance.Config.OtherSCPSCanPlay = bool_result;
                            IsSet = true;
                        }
                        if (name == "SCPSToDClass" && bool.TryParse(value, out bool_result))
                        {
                            Main.Instance.Config.AllSCPSToDClass = bool_result;
                            IsSet = true;
                        }
                    }
                }

            }
            if (IsSet)
            {
                response = "Settings Applied!";
                return true;
            }
            response = "Example Arguments:\n 096Count=2\n SpawnLoot=true\n CanOtherSCPPlay=true\n SCPSToDClass=false\n list";
            return false;
        }
    }
}
