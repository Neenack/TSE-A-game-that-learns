using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneration : MonoBehaviour
{
    public GameObject[] rooms; //0 = LR  1 = LRB  2 = LRT  3 = LRTB  4 = R
    public GameObject door, blockObject, enemy;

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
    void Start()
    {
        int rStartPos = Random.Range(0, arraySize);
        Vector2 startPos = new Vector2(5 + (rStartPos * moveAmount), 5);
        transform.position = startPos;
        Instantiate(rooms[0], transform.position, Quaternion.identity);

        int randX = Random.Range(-2, 2);
        int randY = Random.Range(-2, 2);
        GameObject Entrance = Instantiate(door, new Vector2(transform.position.x + randX, transform.position.y + randY), Quaternion.identity);
        Entrance.GetComponent<DoorController>().type = 0;

        direction = Random.Range(1, 5);

        minX = 5;
        maxX = minX + ((arraySize - 1) * moveAmount);
        minY = minX - ((arraySize - 1) * moveAmount);
    }

    void Update()
    {
        if (generationDelayTimer <= 0 && stopGeneration == false) //generates a room every generationDelayTime
        {
            Move();
            generationDelayTimer = generationDelayTime;
        }
        else generationDelayTimer -= Time.deltaTime;
    }

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
                Instantiate(rooms[rand], transform.position, Quaternion.identity);

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
                Instantiate(rooms[rand], transform.position, Quaternion.identity);

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
                        Instantiate(rooms[3], transform.position, Quaternion.identity); //Create room 3 as it has all exits open
                    }
                    else //replace previous room with a room that has an open bottom
                    {
                        roomDetection.GetComponent<RoomType>().DestroyRoom();
                        Instantiate(rooms[1], transform.position, Quaternion.identity); //Create room 1 as it has bottom open and top closed
                    }
                }

                Vector2 newPos = new Vector2(transform.position.x, transform.position.y - moveAmount);
                transform.position = newPos;

                int rand = Random.Range(2, 4);
                Instantiate(rooms[rand], transform.position, Quaternion.identity);

                direction = Random.Range(1, 5);
            }
            else
            {
                int randX = Random.Range(-4, 5);
                int randY = Random.Range(-4, 0);
                GameObject Exit = Instantiate(door, new Vector2(transform.position.x + randX, transform.position.y + randY), Quaternion.identity);
                Exit.GetComponent<DoorController>().type = 1;

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
                    Instantiate(rooms[rand], positionCheck, Quaternion.identity);
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

            Border(topBorder);
            Border(bottomBorder);
            Border(leftBorder);
            Border(rightBorder);

            levelFinished = true;
        }
    }

    private void Border(Vector2 position)
    {
        Collider2D blockDetector = Physics2D.OverlapCircle(position, 0.2f, block);
        if (blockDetector == null)
        {
            GameObject borderBlock = Instantiate(blockObject, position, Quaternion.identity);
            borderBlock.transform.parent = transform;
        }
    }
}
