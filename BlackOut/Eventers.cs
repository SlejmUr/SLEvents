using Exiled.Events.EventArgs.Server;
using Exiled.Events.EventArgs.Player;
using Exiled.API.Features;
using System.Linq;
using Exiled.Events.EventArgs.Warhead;
using System.Collections.Generic;

namespace BlackOut
{
    internal class Eventers
    {
        public void OnRoundStart()
        {
            if (!Main.Instance.Config.EventEnabled)
                return;


            foreach (var item in Room.List)
            {
                item.AreLightsOff = true;
            }


            if (Main.Instance.Config.JDuff_Round)
            {
                var scps = Player.List.Where(x => x.IsScp).ToList();
                foreach (var item in scps)
                {
                    item.Role.Set(PlayerRoles.RoleTypeId.Scp939);
                }

                // spawn loot

                var humans = Player.List.Where(x => x.IsHuman);
                foreach (var item in humans)
                {
                    item.Role.Set(PlayerRoles.RoleTypeId.FacilityGuard);
                }

            }
            if (Main.Instance.Config.Lotus_Round)
            {
                var scps = Player.List.Where(x => x.IsScp).ToList();
                if (scps.Count > 2)
                {
                    var tmp = scps.Take(2).ToList();
                    var other_scp = scps.Skip(2).ToList();
                    foreach (var item in other_scp)
                    {
                        item.Role.Set(PlayerRoles.RoleTypeId.ClassD);
                    }
                    scps = tmp;
                }

                foreach (var item in scps)
                {
                    item.Role.Set(PlayerRoles.RoleTypeId.Scp939);
                }
                var humans = Player.List.Where(x => x.IsHuman);
                foreach (var item in humans)
                {
                    item.Role.Set(PlayerRoles.RoleTypeId.ClassD);
                }
            }
            if (Main.Instance.Config.Skelle_Round)
            {
                var scps = Player.List.Where(x => x.IsScp).ToList();
                foreach (var item in scps)
                {
                    item.Role.Set(PlayerRoles.RoleTypeId.Scp3114);
                }
                if (scps.Count > 2)
                {
                    scps = scps.Skip(2).ToList();
                    foreach (var item in scps)
                    {
                        item.Role.Set(PlayerRoles.RoleTypeId.ClassD);
                    }
                }
            }
            if (Main.Instance.Config.Fallout_Round)
            {
                var scps = Player.List.Where(x => x.IsScp).ToList();
                foreach (var item in scps)
                {
                    if (item.Role.Type == PlayerRoles.RoleTypeId.Scp173 || item.Role.Type == PlayerRoles.RoleTypeId.Scp096 || item.Role.Type == PlayerRoles.RoleTypeId.Scp079)
                        item.Role.Set(Fallout_RoleSet());
                }
                if (scps.Count > 3)
                {
                    scps = scps.Skip(3).ToList();
                    foreach (var item in scps)
                    {
                        item.Role.Set(PlayerRoles.RoleTypeId.ClassD);
                    }
                }
            }
            
        }


        public void Player_Spawned(SpawnedEventArgs args)
        {
            if (!Main.Instance.Config.EventEnabled)
                return;
            if (Main.Instance.Config.Fallout_Round)
            {
                MEC.Timing.CallDelayed(3, () => {
                    if (args.Reason == Exiled.API.Enums.SpawnReason.ForceClass | args.Reason == Exiled.API.Enums.SpawnReason.Respawn | args.Reason == Exiled.API.Enums.SpawnReason.RoundStart | args.Reason == Exiled.API.Enums.SpawnReason.LateJoin)
                    {
                        args.Player.AddItem(ItemType.Lantern);
                    }

                });
            }
            else
            {
                MEC.Timing.CallDelayed(3, () => {
                    if (args.Reason == Exiled.API.Enums.SpawnReason.ForceClass | args.Reason == Exiled.API.Enums.SpawnReason.Respawn | args.Reason == Exiled.API.Enums.SpawnReason.RoundStart | args.Reason == Exiled.API.Enums.SpawnReason.LateJoin)
                    {
                        args.Player.AddItem(ItemType.Flashlight);
                    }

                });
            }

        }

        public void Respawning(RespawningTeamEventArgs args)
        {
            if (!Main.Instance.Config.EventEnabled)
                return;
            if (Main.Instance.Config.JDuff_Round || Main.Instance.Config.Lotus_Round)
            {
                args.IsAllowed = false;
            }
        }
        public void Player_Pickup(PickingUpItemEventArgs args)
        {
            if (!Main.Instance.Config.EventEnabled)
                return;
            if (Main.Instance.Config.Lotus_Round)
            {
                if (IsWeaponOrAmmo(args.Pickup.Type))
                    args.IsAllowed = false;
            }
        }

        public void Warhead_Start(StartingEventArgs args)
        {
            if (!Main.Instance.Config.EventEnabled)
                return;
            if (Main.Instance.Config.Lotus_Round)
            {
                if (args.Player.Role.Team != PlayerRoles.Team.SCPs && !args.IsAuto)
                {

                    Exiled.API.Features.Broadcast broadcast = new Exiled.API.Features.Broadcast()
                    {
                        Show = true,
                        Duration = 5,
                        Type = Broadcast.BroadcastFlags.Normal,
                        Content = "Congratulation for " + args.Player.DisplayNickname + " winning!"
                    };
                    
                    foreach (var player in Player.List)
                    {
                        player.Broadcast(broadcast);
                    };
                    Main.Instance.Config.EventEnabled = false;
                }
            }
        }

        public bool IsWeaponOrAmmo(ItemType itemType)
        {
            switch (itemType)
            {
                case ItemType.GunCOM15:
                    return true;
                case ItemType.MicroHID:
                    return true;
                case ItemType.Ammo12gauge:
                    return true;
                case ItemType.GunE11SR:
                    return true;
                case ItemType.GunCrossvec:
                    return true;
                case ItemType.Ammo556x45:
                    return true;
                case ItemType.GunFSP9:
                    return true;
                case ItemType.GunLogicer:
                    return true;
                case ItemType.GrenadeHE:
                    return true;
                case ItemType.GrenadeFlash:
                    return true;
                case ItemType.Ammo44cal:
                    return true;
                case ItemType.Ammo762x39:
                    return true;
                case ItemType.Ammo9x19:
                    return true;
                case ItemType.GunCOM18:
                    return true;
                case ItemType.GunRevolver:
                    return true;
                case ItemType.GunAK:
                    return true;
                case ItemType.GunShotgun:
                    return true;
                case ItemType.ParticleDisruptor:
                    return true;
                case ItemType.GunCom45:
                    return true;
                case ItemType.Jailbird:
                    return true;
                case ItemType.GunFRMG0:
                    return true;
                case ItemType.GunA7:
                    return true;
                default:
                    break;
            }
            return false;
        }

        public PlayerRoles.RoleTypeId Fallout_RoleSet()
        {
            List<PlayerRoles.RoleTypeId> roles = new List<PlayerRoles.RoleTypeId>()
            {
                PlayerRoles.RoleTypeId.Scp106,
                PlayerRoles.RoleTypeId.Scp939,
                PlayerRoles.RoleTypeId.Scp049
            };
            return roles.RandomItem();
        }
    }
}
