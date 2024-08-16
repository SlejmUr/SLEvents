using System;
using System.Collections.Generic;
using System.Linq;
using CommandSystem;
using Exiled.API.Features;
using Exiled.Permissions.Extensions;

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
            if (!sender.CheckPermission("tdm_event"))
            {
                response = "You dont have permission to deactivate the event!";
                return false;
            }

            var argList = arguments.ToList();
            if (argList.Count != 2)
            {
                response = "Argument must use as:\nsetinvtdm TeamNumber Id\n Where the TeamNumber either 1 or 2\nId is the User Id from admin panel. (example: [2] SlejmUr , here we use the 2)";
                return false;
            }

            var user = Player.List.Where(x=>x.Id == int.Parse(argList[1])).First();
            if (int.TryParse(argList[0], out int result))
            {
                if (result == 1)
                {
                    List<ItemType> itemTypes = new List<ItemType>();
                    var list = user.Inventory.UserInventory.Items.Values.ToList();
                    foreach (var item in list)
                    {
                        itemTypes.Add(item.ItemTypeId);
                        if (Main.Instance.Config.Debug)
                        {
                            Log.Debug(item.ItemTypeId.ToString() + " added to Team1 Inventory");
                        }
                    }

                    Main.Instance.Config.Team1Items = itemTypes;
                    response = "Team 1 Items set!";
                    return true;
                }
                if (result == 2)
                {
                    List<ItemType> itemTypes = new List<ItemType>();
                    var list = user.Inventory.UserInventory.Items.Values.ToList();
                    foreach (var item in list)
                    {
                        itemTypes.Add(item.ItemTypeId);
                        if (Main.Instance.Config.Debug)
                        {
                            Log.Debug(item.ItemTypeId.ToString() + " added to Team2 Inventory");
                        }
                    }

                    Main.Instance.Config.Team2Items = itemTypes;
                    response = "Team 2 Items set!";
                    return true;
                }
            }


            response = "Something off!";
            return false;
        }
    }
}
