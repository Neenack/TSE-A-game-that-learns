using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using static UnityEditor.PlayerSettings;

using Delegates.Utility;


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

    private void Update()
    {
        if (levelGen.GetComponent<LevelGeneration>().levelFinished == true)
        {
            if (!Physics2D.OverlapCircle(transform.position - new Vector3(0,1,0), 0.1f, blockLayer))
            {
                transform.position -= new Vector3(0,1,0);
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            NewLevel(false);
        }

        int arraySize = levelGen.GetComponent<LevelGeneration>().arraySize;
        float moveAmount = levelGen.GetComponent<LevelGeneration>().moveAmount;
        float lowestY = -moveAmount * (arraySize - 1);
        if (transform.position.y < lowestY)
        {
            NewLevel(false);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && type == 1)
        {
            NewLevel(true);
        }
    }

    private void NewLevel(bool levelFinished)
    {
        GameObject currentPlayer = GameObject.FindGameObjectWithTag("Player");

        //Destroy(currentPlayer);

        if (GenerationDelegates.onDestroyingPlayer != null)
        {
            GenerationDelegates.onDestroyingPlayer();
        }

        GameObject.Find("BackgroundMusic").GetComponent<BackgroundMusicController>().ChangeMusic(levelGen.GetComponent<LevelGeneration>().difficulty);

        levelGen.GetComponent<LevelGeneration>().StartGeneration(levelFinished);
    }
}
