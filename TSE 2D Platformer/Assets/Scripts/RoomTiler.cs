using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTiler : MonoBehaviour
{
    public GameObject block, ladder, trap, enemy;
    public LayerMask blockLayer;
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

    private Vector2 randomPos()
    {
        int randX = Random.Range(-4, 5);
        int randY = Random.Range(-4, 5);

        return new Vector3(randX, randY);
    }

    private bool Collision(Vector2 pos)
    {
        if (Physics2D.OverlapCircle(pos, 0.1f, blockLayer) != null) return true;
        else return false;
    }

    private void AddEnemies()
    {
        if (enemy != null)
        {
            int rand = Random.Range(1, 101);
            if (rand < (difficulty * 10))
            {
                /*
                while (Physics2D.OverlapCircle(transform.position + randPos, 0.1f, blockLayer) != null)
                {
                    randPos = randomPos();
                }

                while (Physics2D.OverlapCircle((transform.position + randPos) - new Vector3(0, 1, 0), 0.1f, blockLayer) == null)
                {
                    randPos = randomPos();
                }
                */

                bool legalSpawn = false;
                while (legalSpawn == false)
                {
                    Vector3 randPos = randomPos();
                    if (!Collision(transform.position + randPos) && //Checks if not inside a block
                        Collision(transform.position + randPos - new Vector3(0, 1, 0)) && //Checks its not floating in the air
                        !Collision(transform.position + randPos + new Vector3(0, 1, 0)) && //Checks there is no block directly above it
                        !Collision((transform.position + randPos) + new Vector3(1, 0, 0)) && //Checks nothing to the right
                        Collision((transform.position + randPos) - new Vector3(0, 1, 0))) //Checks nothing to the left
                    {
                        GameObject newEnemy = Instantiate(enemy, transform.position + randPos, Quaternion.identity);
                        newEnemy.transform.parent = transform;
                        legalSpawn = true;
                    }
                }

               //GameObject newEnemy = Instantiate(enemy, transform.position + randPos, Quaternion.identity);
               //newEnemy.transform.parent = transform;
            }
        }
    }

}
