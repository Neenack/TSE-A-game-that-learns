using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.ML.Data;

namespace K_Means_Plus_Plus
{
        public class PlayerData
        {
            //Increase with skill
            [LoadColumn(0)]
            public float NearMissesWithEnemy;

            [LoadColumn(1)]
            public float NearMissesWithProjectiles;

            [LoadColumn(2)]
            public float EnemiesKilled;

            [LoadColumn(3)]
            public float ItemsPickedUp;

            [LoadColumn(4)]
            public float RoomsExplored;

            [LoadColumn(5)]
            public float InputsOverTime;

            [LoadColumn(6)]
            public float EnemiesAvoided;

            [LoadColumn(7)]
            public float EnemiesKilledWithBombs;

            [LoadColumn(8)]
            public float RopesUsed;

            [LoadColumn(9)]
            public float ZeroRoomsEntered;

            [LoadColumn(10)]
            public float Collectibles;

            //Decrease with skill

            [LoadColumn(11)]
            public float Deaths;

            [LoadColumn(12)]
            public float Time;

            [LoadColumn(13)]
            public float AngryBobDeaths;

            [LoadColumn(14)]
            public float ScreamerDeaths;

            [LoadColumn(15)]
            public float JumperDeaths;

            [LoadColumn(16)]
            public float DeathByTrap1;

            [LoadColumn(17)]
            public float DeathByTrap2;

            [LoadColumn(18)]
            public float DeathByTrap3;

            [LoadColumn(19)]
            public float WallCollisions;

            [LoadColumn(20)]
            public float Jumps;

            [LoadColumn(21)]
            public float TimeIdle;

            [LoadColumn(22)]
            public float LongestTimeInOneRoom;

        }
    }

