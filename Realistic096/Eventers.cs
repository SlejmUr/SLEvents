using Exiled.API.Extensions;
using Exiled.Events.EventArgs.Player;
using Exiled.Events.EventArgs.Scp096;
using Exiled.API.Features.Roles;
using LightContainmentZoneDecontamination;
using Exiled.API.Enums;
using System.Linq;
using Exiled.API.Features;
using Exiled.API.Features.Doors;

namespace Realistic096
{
    internal class Eventers
    {
        public void OnRoundStart()
        {
            if (!Main.Instance.Config.IsEnabled)
                return;

            var lifts = Lift.List;
            foreach (var item in lifts)
            {
                if (item.Type == ElevatorType.Scp049)
                    item.ChangeLock(Interactables.Interobjects.DoorUtils.DoorLockReason.SpecialDoorFeature);
                if (item.Type == ElevatorType.Nuke)
                    item.ChangeLock(Interactables.Interobjects.DoorUtils.DoorLockReason.SpecialDoorFeature);
                if (item.Type == ElevatorType.LczB)
                    item.ChangeLock(Interactables.Interobjects.DoorUtils.DoorLockReason.SpecialDoorFeature);
                if (item.Type == ElevatorType.LczA)
                    item.ChangeLock(Interactables.Interobjects.DoorUtils.DoorLockReason.SpecialDoorFeature);
            }

            var players = Player.List;
            var scp096_where = players.Where(x => x.Role.Type == PlayerRoles.RoleTypeId.Scp096);
            Scp096Role scp096Role;
            if (scp096_where.Any())
            {
                var player = scp096_where.First();
                scp096Role = player.Role.Cast<Scp096Role>();
            }
            else
            {
                var randomplayer = players.GetRandomValue();
                randomplayer.Role.Set(PlayerRoles.RoleTypeId.Scp096);
                scp096Role = randomplayer.Role.Cast<Scp096Role>();
            }
            if (scp096Role == null) //drop error
                return;
            foreach (var player in players)
            {
                if (scp096Role.Owner == player)
                    continue;
                if (player.IsScp && player.Role.Type != PlayerRoles.RoleTypeId.Scp096)
                {
                    if (Main.Instance.Config.OtherSCPSCanPlay)
                    {
                        continue;
                    }
                    else
                    {
                        SCP_ToPlayer(player);
                    }
                }
                scp096Role.AddTarget(player);
            }
            // spawn more loot?

            // tp 096 to escape door
            var door = Door.List.Where(x => x.Type == DoorType.EscapeSecondary).First();
            scp096Role.Owner.Teleport(door);

            // TP Player who are in light
            var randomHeavy = Room.List.Where(x => x.Zone == ZoneType.HeavyContainment);
            foreach (var player in players)
            {
                var tp = randomHeavy.GetRandomValue();
                if (player.CurrentRoom.Zone == ZoneType.LightContainment)
                    player.Teleport(tp);
            }

            // decont
            DecontaminationController.Singleton.DecontaminationOverride = DecontaminationController.DecontaminationStatus.Forced;
        }

        public void SCP_ToPlayer(Player player)
        {
            PlayerRoles.RoleTypeId toRole = PlayerRoles.RoleTypeId.ClassD;
            if (!Main.Instance.Config.AllSCPSToDClass)
            {
                switch (player.Role.Type)
                {
                    case PlayerRoles.RoleTypeId.Scp173:
                        toRole = PlayerRoles.RoleTypeId.FacilityGuard;
                        break;
                    case PlayerRoles.RoleTypeId.Scp106:
                        toRole = PlayerRoles.RoleTypeId.FacilityGuard;
                        break;
                    case PlayerRoles.RoleTypeId.Scp049:
                        toRole = PlayerRoles.RoleTypeId.FacilityGuard;
                        break;
                    case PlayerRoles.RoleTypeId.Scp079:
                        toRole = PlayerRoles.RoleTypeId.FacilityGuard;
                        break;
                    case PlayerRoles.RoleTypeId.Scp0492:
                        toRole = PlayerRoles.RoleTypeId.FacilityGuard;
                        break;
                    case PlayerRoles.RoleTypeId.Scp939:
                        toRole = PlayerRoles.RoleTypeId.FacilityGuard;
                        break;
                    case PlayerRoles.RoleTypeId.Scp3114:
                        toRole = PlayerRoles.RoleTypeId.FacilityGuard;
                        break;
                    default:
                        break;
                }
            }           
            player.Role.Set(toRole);
        }


        public void SCP_096_CalmingDown(CalmingDownEventArgs args)
        {
            if (!Main.Instance.Config.IsEnabled)
                return;
            args.IsAllowed = true;
            args.ShouldClearEnragedTimeLeft = true;
            var players = Player.List.Where(x=>x.IsAlive && x.IsHuman).ToList();
            foreach (var item in players)
            {
                args.Scp096.AddTarget(item);
            }
        }

        public void Player_Hurt(HurtingEventArgs args)
        {
            if (!Main.Instance.Config.IsEnabled)
                return;
            if (args.Player.Role.Type == PlayerRoles.RoleTypeId.Scp096)
                args.IsAllowed = false;
        }

        public void Player_Died(DiedEventArgs args)
        {
            var players = Player.List.Where(x=>x.IsHuman && x.IsAlive).ToList();
            if (players.Count() == 1)
            {
                Exiled.API.Features.Broadcast broadcast = new Exiled.API.Features.Broadcast()
                { 
                    Show = true,
                    Duration = 5,
                    Type = Broadcast.BroadcastFlags.Normal,
                    Content = "Congratulation for " + players[0].DisplayNickname + " winning!"
                };
                foreach (var player in Player.List)
                {
                    player.Broadcast(broadcast);
                }
                Round.EndRound(true);
                Main.Instance.Config.IsEnabled = false;
            }

        }
    }
}
