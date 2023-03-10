using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    private GameObject levelGen;
    public GameObject player;
    private bool playerSpawned = false;
    public LayerMask blockLayer;
    public int type; //0 = entrance   1 = exit

    // Start is called before the first frame update
    void Start()
    {
        levelGen = GameObject.FindGameObjectWithTag("LevelGenerator");
    }

    // Update is called once per frame
    void Update()
    {
        CollisionCheck();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && type == 1)
        {
            Debug.Log("NEXT LEVEL");
        }
    }

    private void CollisionCheck() //Handles gravity and getting unstuck from blocks
    {
        Vector2 oldPos = transform.position;
        if (levelGen.GetComponent<LevelGeneration>().levelFinished == true)
        {
            Collider2D insideBlock = Physics2D.OverlapCircle(transform.position, 0.1f, blockLayer);
            if (type == 0)
            {
                if (insideBlock != null) transform.position = new Vector2(transform.position.x, transform.position.y - 1);
            }
            else if (type == 1)
            {
                if (insideBlock != null) transform.position = new Vector2(transform.position.x, transform.position.y + 1);
            }


            Collider2D onGround = Physics2D.OverlapCircle(new Vector2(transform.position.x, transform.position.y - 0.5f), 0.1f, blockLayer);
            if (onGround == null)
            {
                transform.position = new Vector2(transform.position.x, transform.position.y - 1);
            }

            Vector2 newPos = transform.position;

            if (type == 0 && oldPos == newPos && playerSpawned == false)
            {
                GameObject Player = Instantiate(player, transform.position, Quaternion.identity);
                playerSpawned = true;
            }
        }
    }
}
