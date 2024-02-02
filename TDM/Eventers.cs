using System;
using System.Collections.Generic;
using System.Linq;
using Exiled.API.Features;
using Exiled.API.Features.Doors;
using Exiled.API.Features.Items;
using Exiled.Events.EventArgs.Server;
using Exiled.Events.Features;
using UnityEngine;

namespace TDM
{
    internal class Eventers
    {
        public void OnRoundStart()
        {
            if (!Main.Instance.Config.EventEnabled)
                return;

            

            var players = Player.List.Count;
            var team1_count = players / 2;
            var team2_count = players - team1_count;
            var team1 = Player.List.Take(team2_count).ToList();
            var team2 = Player.List.Skip(team1_count).Take(team2_count).ToList();
            Team1Set(team1);
            Team2Set(team2);
        }

        public void Respawn(SelectingRespawnTeamEventArgs args)
        {
            if (!Main.Instance.Config.EventEnabled)
                return;
            // disable respawning
            args.Team = Respawning.SpawnableTeamType.None;

        }

        public void Team1Set(List<Player> players)
        {
            var preferences = Firearm.PlayerPreferences;
            var room1 = Door.List.Where(x => x.Type == Exiled.API.Enums.DoorType.CheckpointEzHczA).First().Room;
            foreach (var player in players)
            {
                player.Role.Set( PlayerRoles.RoleTypeId.NtfSpecialist);
                player.Teleport(room1);
                var myPreference = preferences.Where(x => x.Key == player).First().Value;
                if (Main.Instance.Config.Team1Items.Count != 0)
                {
                    foreach (var teamItem in Main.Instance.Config.Team1Items)
                    {
                        var createdItem = player.AddItem(teamItem);
                        if (createdItem.IsWeapon)
                        {
                            var firearm = createdItem as Firearm;
                            var firearmPref = myPreference[firearm.FirearmType];
                            firearm.AddAttachment(firearmPref);
                            var ammoType = firearm.AmmoType;
                            player.AddAmmo(ammoType, 120);
                        }
                    }
                }
            }
            if (players.Count > 5)
            {
                players.RandomItem().Role.Set(PlayerRoles.RoleTypeId.NtfCaptain);
            }
        }

        public void Team2Set(List<Player> players)
        {
            var preferences = Firearm.PlayerPreferences;
            var room2 = Door.List.Where(x => x.Type == Exiled.API.Enums.DoorType.CheckpointEzHczB).First().Room;
            foreach (var player in players)
            {
                player.Role.Set(PlayerRoles.RoleTypeId.ChaosConscript);
                player.Teleport(room2);
                var myPreference = preferences.Where(x => x.Key == player).First().Value;
                if (Main.Instance.Config.Team2Items.Count != 0)
                {
                    foreach (var teamItem in Main.Instance.Config.Team2Items)
                    {
                        var createdItem = player.AddItem(teamItem);
                        if (createdItem.IsWeapon)
                        {
                            var firearm = createdItem as Firearm;
                            var firearmPref = myPreference[firearm.FirearmType];
                            firearm.AddAttachment(firearmPref);
                            var ammoType = firearm.AmmoType;
                            player.AddAmmo(ammoType, 120);
                        }
                    }
                }
            }
            if (players.Count > 5)
            {
                players.RandomItem().Role.Set(PlayerRoles.RoleTypeId.ChaosRepressor);
            }
        }
    }
}
