using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Controllers.World
{
    public class BlockController : MonoBehaviour
    {
        public Sprite[] blocks;

        // Start is called before the first frame update
        public void BeginSelf()
        {
            int randSprite = Random.Range(0, blocks.Length);

            GetComponent<SpriteRenderer>().sprite = blocks[randSprite];
        }
    }
}