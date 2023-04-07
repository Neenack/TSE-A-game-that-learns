using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

using Delegates.Utility;


public class DoorController : MonoBehaviour
{
    private GameObject levelGen;
    public GameObject player;
    public LayerMask blockLayer;
    public int type; //0 = entrance   1 = exit
    private bool playerSpawned;

    // Start is called before the first frame update
    void Start()
    {
        levelGen = GameObject.FindGameObjectWithTag("LevelGenerator");
        playerSpawned = false;
    }

    private void Update()
    {
        if (levelGen.GetComponent<LevelGeneration>().levelFinished == true)
        {
            if (Physics2D.OverlapCircle(transform.position, 0.1f, blockLayer) != null)
            {
                Vector3 newPos = transform.position - new Vector3(0, 1, 0);
                transform.position = newPos;
            }
            else
            {
                if (Physics2D.OverlapCircle(transform.position - new Vector3(0, 1, 0), 0.1f, blockLayer) == null)
                {
                    Vector3 newPos = transform.position - new Vector3(0, 1, 0);
                    transform.position = newPos;
                }
                else
                {
                    if (playerSpawned == false && type == 0)
                    {
                        GameObject spawnedPlayer = Instantiate(player, transform.position, Quaternion.identity);
                        playerSpawned = true;

                        spawnedPlayer.transform.SetParent(GameObject.Find("PlayerHolder").transform, true);

                        if(GenerationDelegates.onSpawningPlayer != null)
                        {
                            GenerationDelegates.onSpawningPlayer();
                        }
                    }
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && type == 1)
        {
            GameObject currentPlayer = GameObject.FindGameObjectWithTag("Player");

            //Destroy(currentPlayer);

            if(GenerationDelegates.onDestroyingPlayer != null)
            {
                GenerationDelegates.onDestroyingPlayer();
            }

            levelGen.GetComponent<LevelGeneration>().StartGeneration();
        }
    }
}
