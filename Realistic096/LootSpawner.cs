using Exiled.API.Extensions;
using Exiled.API.Features;
using Exiled.API.Features.Pickups;
using Random = UnityEngine.Random;
using System.Linq;
using UnityEngine;
using System;

namespace Realistic096
{
    internal class LootSpawner
    {
        static ItemType GetRandomPickup()
        {
            int number = Random.Range(0, 11);
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
                    itemType = ItemType.AntiSCP207;
                    break;
                case 2:
                    itemType = ItemType.SCP207;
                    break;
                case 3:
                    itemType = ItemType.Jailbird;
                    break;
                case 4:
                    itemType = ItemType.Coin;
                    break;
                case 5:
                    itemType = ItemType.MicroHID;
                    break;
                case 6:
                    itemType = ItemType.Ammo556x45;
                    break;
                case 7:
                    itemType = ItemType.Ammo9x19;
                    break;
                case 8:
                    itemType = ItemType.Medkit;
                    break;
                case 9:
                    itemType = ItemType.ArmorHeavy;
                    break;
                case 10:
                    itemType = ItemType.SCP500;
                    break;
                default:
                    break;
            }
            return itemType;
        }
        public static void SpawnLoot()
        {
            bool FunnyKeycardSpawned = false;
            int hcz_count = Room.List.Where(x => x.Zone == Exiled.API.Enums.ZoneType.HeavyContainment).Count();
            for (int i = 0; i < hcz_count; i++)
            {
                var room = Room.List.Where(a => a.Players.ToList().Count <= 0 && a.Zone == Exiled.API.Enums.ZoneType.HeavyContainment).GetRandomValue();
                if (room.Type == Exiled.API.Enums.RoomType.Pocket)
                    continue;
                for (int j = 0; j <= 10; j++)
                {
                    try
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
                            ammo.Ammo = (ushort)Random.Range(30, 90);
                        }
                    }
                    catch (Exception ex)
                    {
                        Log.Info(ex.Message + "\n" + ex);
                    }

                }
            }
        }
    }
}
