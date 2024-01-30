using Exiled.API.Extensions;
using Exiled.API.Features;
using Exiled.API.Features.Pickups;
using Random = UnityEngine.Random;
using System.Linq;
using UnityEngine;

namespace Realistic096
{
    internal class LootSpawner
    {
        static ItemType GetRandomPickup()
        {
            int number = Random.Range(0, 10);
            return number switch
            {
                0 => ItemType.GrenadeFlash,
                1 => ItemType.KeycardChaosInsurgency,
                2 => ItemType.KeycardFacilityManager,
                3 => ItemType.KeycardGuard,
                4 => ItemType.KeycardMTFOperative,
                5 => ItemType.KeycardJanitor,
                6 => ItemType.Ammo556x45,
                7 => ItemType.Ammo9x19,
                8 => ItemType.ArmorLight,
                9 => ItemType.ArmorHeavy,
                _ => ItemType.Adrenaline
            };
        }
        public static void SpawnLoot()
        {
            bool FunnyKeycardSpawned = false;
            for (int i = 0; i <= 10; i++)
            {
                var room = Room.List.Where(a => a.Players.ToList().Count <= 0 && a.Zone == Exiled.API.Enums.ZoneType.HeavyContainment).GetRandomValue();
                for (int j = 0; j <= 3; j++)
                {
                    var randomDoor = room.Doors.GetRandomValue();
                    Pickup pickup = Pickup.CreateAndSpawn(GetRandomPickup(), randomDoor.Position + (randomDoor.Rotation * Vector3.back), Quaternion.Euler(0, 0, 0));
                    pickup.GameObject.GetComponent<Rigidbody>().velocity = new Vector3(Random.Range(-100, 100), Random.Range(-100, 100), Random.Range(-100, 100));
                    if (pickup is KeycardPickup keycardPickup && pickup.Type == ItemType.KeycardJanitor)
                    {
                        int chance = Random.Range(0, 100);
                        if (chance == 66 && !FunnyKeycardSpawned)
                        {
                            // Funny keycard
                            keycardPickup.Permissions |= Exiled.API.Enums.KeycardPermissions.ContainmentLevelThree;
                            keycardPickup.Permissions |= Exiled.API.Enums.KeycardPermissions.ArmoryLevelOne;
                            FunnyKeycardSpawned = true;
                        }
                    }
                    if (pickup is AmmoPickup ammo)
                    {
                        ammo.Ammo = (ushort)Random.Range(15, 30);
                    }
                }
            }
        }
    }
}
