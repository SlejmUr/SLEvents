using System.Linq;
using Exiled.API.Features;
using UnityEngine;

namespace Tiny
{
    internal class Eventers
    {
        public void OnRoundStart()
        {
            if (!Main.Instance.Config.EventEnabled)
                return;


            foreach (var item in Player.List.Where(x=>x.IsHuman))
            {
                item.Scale = Vector3.one * 0.7f;
            }
        }
    }
}
