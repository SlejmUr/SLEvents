using System;
using System.Collections.Generic;
using System.Linq;
using Exiled.API.Features;
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
            foreach (var item in players)
            {
                item.Role.Set( PlayerRoles.RoleTypeId.NtfSpecialist);
            }
            if (players.Count > 5)
            {
                players.RandomItem().Role.Set(PlayerRoles.RoleTypeId.NtfCaptain);
            }
        }

        public void Team2Set(List<Player> players)
        {
            foreach (var item in players)
            {
                item.Role.Set(PlayerRoles.RoleTypeId.ChaosConscript);
            }
            if (players.Count > 5)
            {
                players.RandomItem().Role.Set(PlayerRoles.RoleTypeId.ChaosRepressor);
            }
        }
    }
}
