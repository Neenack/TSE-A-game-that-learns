using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Delegates.Utility;



public class LevelGeneration : MonoBehaviour
{
    public GameObject[] rooms; //0 = LR  1 = LRB  2 = LRT  3 = LRTB  4 = R
    public GameObject door, blockObject, borderRoom, bg;
    public GameObject player;

    [SerializeField]
    GameObject borderHolder, roomsHolder, doorsHolder;

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
        StartGeneration();
    }

    public void StartGeneration()
    {
        foreach (Transform child in transform) { GameObject.Destroy(child.gameObject); }
        foreach (Transform child in roomsHolder.transform) { GameObject.Destroy(child.gameObject); }
        foreach (Transform child in borderHolder.transform) { GameObject.Destroy(child.gameObject); }
        foreach (Transform child in doorsHolder.transform) { GameObject.Destroy(child.gameObject); }
        
        if(ZoneDelegates.onZoneCompletion != null) { ZoneDelegates.onZoneCompletion(); }


        int rStartPos = Random.Range(1, arraySize - 1);
        Vector2 startPos = new Vector2(5 + (rStartPos * moveAmount), 5);
        transform.position = startPos;

        SetBackground();

        CreateRoom(rooms[0], transform.position);
        CreateDoor(0);

        if(ZoneDelegates.onZoneTickUpdate != null) ZoneDelegates.onZoneTickUpdate();

        direction = Random.Range(1, 4);

        minX = 5;
        maxX = minX + ((arraySize - 1) * moveAmount);
        minY = minX - ((arraySize - 1) * moveAmount);

        stopGeneration = false;

        StartCoroutine(GenerateRoom());
    }

    IEnumerator GenerateRoom()
    {
        while(!stopGeneration)
        {
            if (generationDelayTimer <= 0 && stopGeneration == false) //generates a room every generationDelayTime
            {
                Move();
                generationDelayTimer = generationDelayTime;
                yield return null;
            }
            else generationDelayTimer -= Time.deltaTime;
            yield return null;
        }


        Transform door0 = GameObject.Find("Entrance").transform;

        GameObject spawnedPlayer = Instantiate(player, door0.position, Quaternion.identity);
        spawnedPlayer.transform.SetParent(GameObject.Find("PlayerHolder").transform, true);

        if(GenerationDelegates.onSpawningPlayer != null) GenerationDelegates.onSpawningPlayer();
        if(ZoneDelegates.onZoneGenerationFinish != null) ZoneDelegates.onZoneGenerationFinish();
    }

    /*void Update()
    {
        
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
                CreateRoom(rooms[rand], transform.position);

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
                CreateRoom(rooms[rand], transform.position);

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
                        CreateRoom(rooms[3], transform.position); //Create room 3 as it has all exits open
                    }
                    else //replace previous room with a room that has an open bottom
                    {
                        roomDetection.GetComponent<RoomType>().DestroyRoom();
                        CreateRoom(rooms[1], transform.position); //Create room 1 as it has bottom open and top closed
                    }
                }

                Vector2 newPos = new Vector2(transform.position.x, transform.position.y - moveAmount);
                transform.position = newPos;

                int rand = Random.Range(2, 4);
                CreateRoom(rooms[rand], transform.position);

                direction = Random.Range(1, 5);
            }
            else
            {
                CreateDoor(1);

                Invoke("Fill", generationDelayTime);
                stopGeneration = true; //if cant go down then stop generating                
            }
        }
        firstMove = false;


        if(ZoneDelegates.onZoneTickUpdate != null)
        {
            ZoneDelegates.onZoneTickUpdate();
        }
    }

    private void SetBackground()
    {
        bg.transform.position = new Vector2(arraySize * moveAmount / 2, -arraySize * moveAmount / 2 + moveAmount);

        SpriteRenderer bgR = bg.GetComponent<SpriteRenderer>();
        bgR.drawMode = SpriteDrawMode.Tiled;
        bgR.size = new Vector2(arraySize * moveAmount + 1, arraySize * moveAmount + 1);

        bg.SetActive(true);
    }

    private void CreateRoom(GameObject room, Vector3 pos)
    {
        GameObject newRoom = Instantiate(room, pos, Quaternion.identity);

        float xPos = (pos.x - 5) / moveAmount;
        if (xPos < 0) xPos = -xPos;
        float yPos = (pos.y - 5) / moveAmount;
        if (yPos < 0) yPos = -yPos;

        //newRoom.transform.parent = roomsHolder.transform;
        newRoom.transform.SetParent(roomsHolder.transform, true);
        newRoom.gameObject.name = "Room [" + yPos + "-" + xPos + "]";
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
                    CreateRoom(rooms[rand], positionCheck);
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

        levelFinished = true;
    }

    private void Border(Vector2 position, GameObject border)
    {
        Collider2D blockDetector = Physics2D.OverlapCircle(position, 0.2f, block);
        if (blockDetector == null)
        {
            GameObject borderBlock = Instantiate(border, position, Quaternion.identity);
            borderBlock.transform.SetParent(borderHolder.transform, true);
            //borderBlock.transform.parent = transform;
        }
    }

    private void CreateDoor(int type)
    {
        Vector3 randPos = new Vector3(Random.Range(-2, 2), 2);
        GameObject Door = Instantiate(door, transform.position + randPos, Quaternion.identity);
        Door.GetComponent<DoorController>().type = type;
        if (type == 0) Door.gameObject.name = "Entrance";
        if (type == 1) Door.gameObject.name = "Exit";
        Door.transform.SetParent(doorsHolder.transform, true);
        //Door.transform.parent = roomsHolder.transform;
    }
    
}