using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Controllers.World.Interactable;

using Overworld.Rooms.Interactable;
using Overworld.Rooms.Generation;



namespace Controllers.World.Generation
{
    public class LevelGenerationController : MonoBehaviour
    {
        public GameObject[] rooms; //0 = LR  1 = LRB  2 = LRT  3 = LRTB  4 = R
        public GameObject door, blockObject, enemy, borderRoom;

        public GameObject player;

        private int direction;
        public float moveAmount;
        public int arraySize;

        private float generationDelayTimer;
        public float generationDelayTime = 0.25f;

        private float minX, maxX, minY;
        private bool stopGeneration;
        public bool levelFinished;

        public LayerMask room, block;

        private int downCounter;
        private bool firstMove = true;

        public int difficulty;



        // Start is called before the first frame update
        public void BeginSelf()
        {
            int rStartPos = Random.Range(0, arraySize);
            Vector2 startPos = new Vector2(5 + (rStartPos * moveAmount), 5);
            transform.position = startPos;
            GameObject newRoom = Instantiate(rooms[0], transform.position, Quaternion.identity);
            newRoom.GetComponent<RoomGenerator>().BeginSelf();

            int randX = Random.Range(-2, 2);
            int randY = Random.Range(-2, 2);
            GameObject Entrance = Instantiate(door, new Vector2(transform.position.x + randX, transform.position.y + randY), Quaternion.identity);
            Entrance.GetComponent<DoorController>().type = 0;
            Entrance.GetComponent<DoorController>().BeginSelf();

            direction = Random.Range(1, 5);

            minX = 5;
            maxX = minX + ((arraySize - 1) * moveAmount);
            minY = minX - ((arraySize - 1) * moveAmount);

            while(stopGeneration != true)
            {
                Move();
            }

            /*if (type == 0 && oldPos == newPos && playerSpawned == false)
                {
                    GameObject Player = Instantiate(player, transform.position, Quaternion.identity);
                    playerSpawned = true;
                }*/

            GameObject Player = Instantiate(player, Entrance.transform.position, Quaternion.identity);
            Player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            Player.gameObject.name = "Player";
        }

        /*void Update()
        {
            if (generationDelayTimer <= 0 && stopGeneration == false) //generates a room every generationDelayTime
            {
                Move();
                generationDelayTimer = generationDelayTime;
            }
            else generationDelayTimer -= Time.unscaledDeltaTime;
        }*/

        private void Move()
        {
            if (direction == 1 || direction == 2) //1 and 2 mean go right
            {
                if (transform.position.x < maxX) //checks if in bounds
                {
                    downCounter = 0;
                    Vector2 newPos = new Vector2(transform.position.x + moveAmount, transform.position.y);
                    transform.position = newPos;

                    int rand = Random.Range(0, 4);
                    GameObject newRoom = Instantiate(rooms[rand], transform.position, Quaternion.identity);
                    newRoom.GetComponent<RoomGenerator>().BeginSelf();

                    //Prevents spawner from going back on itself
                    direction = Random.Range(1, 6);
                    if (direction == 3) direction = 2;
                    else if (direction == 4) direction = 5;
                }
                else
                {
                    if (firstMove) direction = 3; //If first move then go left
                    else direction = 5; //if cant go right then go down
                }
            }

            else if (direction == 3 || direction == 4) //3 and 4 mean go left
            {
                if (transform.position.x > minX) //checks if in bounds
                {
                    downCounter = 0;
                    Vector2 newPos = new Vector2(transform.position.x - moveAmount, transform.position.y);
                    transform.position = newPos;

                    int rand = Random.Range(0, 4);
                    GameObject newRoom = Instantiate(rooms[rand], transform.position, Quaternion.identity);
                    newRoom.GetComponent<RoomGenerator>().BeginSelf();

                    //Prevents spawner from going back on itself
                    direction = Random.Range(3, 6);
                }
                else
                {
                    if (firstMove) direction = 1; //If first move then go right
                    else direction = 5; //if cant go left then go down
                }
            }

            else if (direction == 5) // 5 means go down
            {
                downCounter++;

                Collider2D roomDetection = Physics2D.OverlapCircle(transform.position, 1, room);
                if (transform.position.y > minY) //checks if in bounds
                {
                    if (roomDetection.GetComponent<RoomType>().type != 1 && roomDetection.GetComponent<RoomType>().type != 3) // check if the previous room has a bottom exit
                    {
                        if (downCounter >= 2) // check if the generator has moved down twice
                        {
                            roomDetection.GetComponent<RoomType>().DestroyRoom();
                            GameObject newerRoom = Instantiate(rooms[3], transform.position, Quaternion.identity); //Create room 3 as it has all exits open
                            newerRoom.GetComponent<RoomGenerator>().BeginSelf();
                        }
                        else //replace previous room with a room that has an open bottom
                        {
                            roomDetection.GetComponent<RoomType>().DestroyRoom();
                            GameObject newerRoom = Instantiate(rooms[1], transform.position, Quaternion.identity); //Create room 1 as it has bottom open and top closed
                            newerRoom.GetComponent<RoomGenerator>().BeginSelf();
                        }
                    }

                    Vector2 newPos = new Vector2(transform.position.x, transform.position.y - moveAmount);
                    transform.position = newPos;

                    int rand = Random.Range(2, 4);
                    GameObject newRoom = Instantiate(rooms[rand], transform.position, Quaternion.identity);
                    newRoom.GetComponent<RoomGenerator>().BeginSelf();

                    direction = Random.Range(1, 5);
                }
                else
                {
                    int randX = Random.Range(-4, 5);
                    int randY = Random.Range(-4, 0);
                    GameObject Exit = Instantiate(door, new Vector2(transform.position.x + randX, transform.position.y + randY), Quaternion.identity);
                    Exit.GetComponent<DoorController>().type = 1;
                    Exit.GetComponent<DoorController>().BeginSelf();

                    Invoke("Fill", generationDelayTime);
                    stopGeneration = true; //if cant go down then stop generating
                }
            }
            firstMove = false;
        }

        private void Fill() //Loops through every space in the array and fills in any rooms that are free
        {
            for(int i = 0; i < arraySize; i++)
            {
                for (int j = 0; j < arraySize; j++)
                {
                    Vector2 positionCheck = new Vector2((i * moveAmount) + 5, (-j * moveAmount) + 5);
                    Collider2D roomDetection = Physics2D.OverlapCircle(positionCheck, 1, room);
                    if (roomDetection == null)
                    {
                        int rand = Random.Range(0, rooms.Length);
                        GameObject newRoom = Instantiate(rooms[rand], positionCheck, Quaternion.identity);
                        newRoom.GetComponent<RoomGenerator>().BeginSelf();
                    }
                }
            }
            Invoke("CreateBorders", generationDelayTime);
        }


        private void CreateBorders()
        {
            for (int i = 0; i <= (moveAmount * arraySize) - 1; i++)
            {
                Vector2 topBorder = new Vector2(i, moveAmount - 1);
                Vector2 bottomBorder = new Vector2(i, -moveAmount * (arraySize - 1));
                Vector2 leftBorder = new Vector2(-1, i - (moveAmount * (arraySize - 1)));
                Vector2 rightBorder = new Vector2((moveAmount * arraySize), i - (moveAmount * (arraySize - 1)));

                Border(topBorder, blockObject);
                Border(bottomBorder, blockObject);
                Border(leftBorder, blockObject);
                Border(rightBorder, blockObject);

                levelFinished = true;
            }

            for (int i = 0; i <= arraySize; i++)
            {
                Vector2 topBlockBorder = new Vector2((moveAmount * (-0.5f + i)) + 1, moveAmount * 1.5f);
                Vector2 bottomBlockBorder = new Vector2((moveAmount * (-0.5f + i + 1)) - 1, -moveAmount * (arraySize - 0.5f));
                Vector2 leftBlockBorder = new Vector2(-moveAmount * 0.5f - 1, (moveAmount / 2) + (i * -moveAmount));
                Vector2 rightBlockBorder = new Vector2((moveAmount * arraySize) + (moveAmount / 2) + 1, (moveAmount * 1.5f) - (i * moveAmount));

                Border(topBlockBorder, borderRoom);
                Border(bottomBlockBorder, borderRoom);
                Border(leftBlockBorder, borderRoom);
                Border(rightBlockBorder, borderRoom);
            }
        }

        private void Border(Vector2 position, GameObject border)
        {
            Collider2D blockDetector = Physics2D.OverlapCircle(position, 0.2f, block);
            if (blockDetector == null)
            {
                GameObject borderBlock = Instantiate(border, position, Quaternion.identity);
                borderBlock.transform.parent = transform;   
                if(borderBlock.GetComponent<RoomTilerController>() != null)
                {
                    borderBlock.GetComponent<RoomTilerController>().BeginSelf();
                }
            }
        }
        
    }
}