using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemVaseController : MonoBehaviour
{
    void OnDestroy()
    {
        int rand = Random.Range(1, 3);
        
        if (rand == 1)
        {
            //Give player bomb
        }
        if (rand == 2)
        {
            //Give player rope
        }
    }
}
