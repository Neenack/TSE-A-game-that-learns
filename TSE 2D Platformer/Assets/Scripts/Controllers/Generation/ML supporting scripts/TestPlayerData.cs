using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace K_Means_Plus_Plus
{
    internal class TestPlayerData
    {
        internal static readonly PlayerData BadPlayer = new PlayerData
        {

            //Increase with skill
            NearMissesWithEnemy = 0,
            NearMissesWithProjectiles = 0,
            EnemiesKilled = 2,
            ItemsPickedUp = 1,
            RoomsExplored = 13,
            InputsOverTime = 0,
            EnemiesAvoided = 2,
            EnemiesKilledWithBombs = 0,
            RopesUsed = 1,
            ZeroRoomsEntered = 0,
            Collectibles = 0,


            //Decrease with skill
            Deaths = 4,
            Time = 440,
            AngryBobDeaths = 1,
            ScreamerDeaths = 0,
            JumperDeaths = 0,
            DeathByTrap1 = 2,
            DeathByTrap2 = 0,
            DeathByTrap3 = 1,
            WallCollisions = 26,
            Jumps = 45,
            TimeIdle = 22,
            LongestTimeInOneRoom = 69
        };

        internal static readonly PlayerData AveragePlayer = new PlayerData
        {
            //Increase with skill
            NearMissesWithEnemy = 1,
            NearMissesWithProjectiles = 0,
            EnemiesKilled = 3,
            ItemsPickedUp = 2,
            RoomsExplored = 16,
            InputsOverTime = 0,
            EnemiesAvoided = 2,
            EnemiesKilledWithBombs = 2,
            RopesUsed = 1,
            ZeroRoomsEntered = 0,
            Collectibles = 1,


            //Decrease with skill
            Deaths = 1,
            Time = 356,
            AngryBobDeaths = 1,
            ScreamerDeaths = 0,
            JumperDeaths = 0,
            DeathByTrap1 = 0,
            DeathByTrap2 = 0,
            DeathByTrap3 = 0,
            WallCollisions = 16,
            Jumps = 35,
            TimeIdle = 22,
            LongestTimeInOneRoom = 49
        };

        internal static readonly PlayerData GoodPlayer = new PlayerData
        {
            //Increase with skill
            NearMissesWithEnemy = 2,
            NearMissesWithProjectiles = 0,
            EnemiesKilled = 4,
            ItemsPickedUp = 4,
            RoomsExplored = 14,
            InputsOverTime = 0,
            EnemiesAvoided = 6,
            EnemiesKilledWithBombs = 3,
            RopesUsed = 2,
            ZeroRoomsEntered = 0,
            Collectibles = 3,


            //Decrease with skill
            Deaths = 0,
            Time = 220,
            AngryBobDeaths = 0,
            ScreamerDeaths = 0,
            JumperDeaths = 0,
            DeathByTrap1 = 0,
            DeathByTrap2 = 0,
            DeathByTrap3 = 0,
            WallCollisions = 11,
            Jumps = 33,
            TimeIdle = 6,
            LongestTimeInOneRoom = 33
        };

    }
}