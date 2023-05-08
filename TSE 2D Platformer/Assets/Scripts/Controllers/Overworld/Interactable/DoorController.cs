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
            if (Physics2D.OverlapCircle(transform.position, 0.1f, blockLayer))
            {
                transform.position += new Vector3(Random.Range(-1, 1), Random.Range(-1, 1));
            }
            else
            {
                if (!Physics2D.OverlapCircle(transform.position - new Vector3(0,1,0), 0.1f, blockLayer))
                {
                    transform.position -= new Vector3(0,1,0);
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
