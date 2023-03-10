using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTiler : MonoBehaviour
{
    public GameObject block, ladder, trap, enemy;
    private GameObject levelGen;
    private int difficulty; //1 - 10

    // Start is called before the first frame update
    void Start()
    {
        levelGen = GameObject.FindGameObjectWithTag("LevelGenerator");
        difficulty = levelGen.GetComponent<LevelGeneration>().difficulty;

        foreach (Transform child in transform)
        {
            if (child.tag == "Tile")
            {
                GameObject newBlock = Instantiate(block, child.position, Quaternion.identity);
                newBlock.transform.parent = transform;
                Destroy(child.gameObject);
            }

            else if (child.tag == "Chance")
            {
                int chance = child.gameObject.GetComponent<ChanceBlock>().chance;
                int rand = Random.Range(1, 101);
                if (rand <= chance)
                {
                    GameObject newBlock = Instantiate(block, child.position, Quaternion.identity);
                    newBlock.transform.parent = transform;
                }
                Destroy(child.gameObject);
            }

            else if (child.tag == "Ladder")
            {
                GameObject newLadder = Instantiate(ladder, child.position, Quaternion.identity);
                newLadder.transform.parent = transform;
            }

            else if (child.tag == "Trap")
            {
                int rand = Random.Range(1, 101);
                if (rand < (difficulty * 10))
                {
                    GameObject newTrap = Instantiate(trap, child.position, Quaternion.identity);
                    newTrap.transform.parent = transform;
                }
                else Destroy(child.gameObject);
            }
        }

        AddEnemies();
    }

    private void AddEnemies()
    {
        if (enemy != null)
        {
            int rand = Random.Range(1, 101);
            if (rand < (difficulty * 10))
            {
                int randX = Random.Range(-4, 5);
                int randY = Random.Range(-4, 5);

                GameObject newEnemy = Instantiate(enemy, new Vector2(transform.position.x + randX, transform.position.y + randY), Quaternion.identity);
            }
        }
    }
}
