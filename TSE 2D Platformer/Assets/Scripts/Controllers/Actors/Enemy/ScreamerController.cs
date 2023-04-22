using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreamerController : MonoBehaviour
{
    public float screamInterval = 2f;
    public GameObject screamerBolt;

    public LayerMask blockLayer;
    private GameObject player;

    private float screamTimer = 0f;

    void Start()
    {
    }

    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {

            // If the jump timer has expired, jump in a random direction
            if (screamTimer <= 0f)
            {
                //Scream
                GameObject newBolt = Instantiate(screamerBolt, transform.position, Quaternion.identity);
                newBolt.GetComponent<ScreamerBoltController>().SetDirection((player.transform.position - transform.position).normalized, player);
                screamTimer = screamInterval;
            }
            else
            {
                screamTimer -= Time.deltaTime;
            }

            transform.localScale = (player.transform.position.x < transform.position.x ? new Vector3(1, 1, 1) : new Vector3(-1, 1, 1));
        }
    }
}
