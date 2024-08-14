using Exiled.API.Extensions;
using Exiled.Events.EventArgs.Player;
using Exiled.Events.EventArgs.Scp096;
using Exiled.API.Features.Roles;
using LightContainmentZoneDecontamination;
using Exiled.API.Enums;
using System.Linq;
using Exiled.API.Features;
using Exiled.API.Features.Doors;
using MEC;

namespace Realistic096
{
    internal class Eventers
    {
        CoroutineHandle _SCP096_TargetReflesh;

        public void OnRoundStart()
        {
            if (!Main.Instance.Config.EventEnabled)
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
            scp096Role.Owner.MaxHealth = 5000;
            scp096Role.Owner.Health = 5000;
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
            if (Main.Instance.Config.SpawnLoot)
                LootSpawner.SpawnLoot();

            // tp 096 to escape door
            var door = Door.List.Where(x => x.Type == DoorType.EscapeSecondary).First();
            scp096Role.Owner.Teleport(door);

            // TP Player who are in light
            var randomHeavy = Room.List.Where(x => x.Zone == ZoneType.HeavyContainment);
            foreach (var player in players.Where(x=> x.CurrentRoom.Zone == ZoneType.LightContainment))
            {
                var tp = randomHeavy.GetRandomValue();
                player.Teleport(tp);
            }

            _SCP096_TargetReflesh = MEC.Timing.CallContinuously(5, () => {

                foreach (var item in Player.List.Where(x => x.IsHuman && x.IsAlive))
                {
                    scp096Role.AddTarget(item);
                }
            });

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
            if (!Main.Instance.Config.EventEnabled)
                return;
            MEC.Timing.CallDelayed(5, () => 
            {
                var players = Player.List.Where(x => x.IsAlive && x.IsHuman).ToList();
                foreach (var item in players)
                {
                    args.Scp096.AddTarget(item);
                }

            });
        }

        public void Player_Hurt(HurtingEventArgs args)
        {
            if (!Main.Instance.Config.EventEnabled)
                return;
            if (args.Player.Role.Type == PlayerRoles.RoleTypeId.Scp096)
                args.DamageHandler.Damage = args.DamageHandler.Damage * 0.01f;
        }

        public void Player_Died(DiedEventArgs args)
        {
            if (!Main.Instance.Config.EventEnabled)
                return;
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

                MEC.Timing.CallDelayed(3, () => {
                    foreach (var player in Player.List)
                    {
                        player.Broadcast(broadcast);
                    }
                    Main.Instance.Config.EventEnabled = false;
                });
                MEC.Timing.KillCoroutines(_SCP096_TargetReflesh);
            }

        }
    }
}
