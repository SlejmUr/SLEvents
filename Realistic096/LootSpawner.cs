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
            ItemType itemType = ItemType.Adrenaline;
            /*
            ItemType itemType = number switch
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
            };*/
            switch (number)
            {
                case 0:
                    itemType = ItemType.GrenadeFlash;
                    break;
                case 1:
                    itemType = ItemType.KeycardChaosInsurgency;
                    break;
                case 2:
                    itemType = ItemType.KeycardFacilityManager;
                    break;
                case 3:
                    itemType = ItemType.KeycardGuard;
                    break;
                case 4:
                    itemType = ItemType.KeycardMTFOperative;
                    break;
                case 5:
                    itemType = ItemType.KeycardJanitor;
                    break;
                case 6:
                    itemType = ItemType.Ammo556x45;
                    break;
                case 7:
                    itemType = ItemType.Ammo9x19;
                    break;
                case 8:
                    itemType = ItemType.ArmorLight;
                    break;
                case 9:
                    itemType = ItemType.ArmorHeavy;
                    break;
                default:
                    break;
            }
            return itemType;
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
                    if (pickup.Type == ItemType.KeycardJanitor && pickup is KeycardPickup keycardPickup)
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
