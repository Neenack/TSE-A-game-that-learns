using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    private GameObject levelGen;
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

    private void CollisionCheck() //Handles gravity and getting unstuck from blocks
    {
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
        }
    }
}
