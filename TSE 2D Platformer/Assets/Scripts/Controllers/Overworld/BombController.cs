using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour
{
    public Sprite redBomb;
    public SpriteRenderer sr;
    public GameObject explosion;

    public float explosionRadius = 3f;
    public float explosionTimer;
    bool hasExploded = false;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("Red", explosionTimer - 1);
        Invoke("Explode", explosionTimer);
    }

    void Red()
    {
        sr.sprite = redBomb;
    }

    // Update is called once per frame
    void Explode()
    {
        if (!hasExploded)
        {
            hasExploded = true;

            GameObject newExplosion = Instantiate(explosion, transform.position, Quaternion.identity);

            // Destroy blocks within explosion radius
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
            foreach (Collider2D collider in colliders)
            {
                if (collider.gameObject.layer != LayerMask.NameToLayer("Background") && collider.gameObject.tag != "Player"
                    && collider.gameObject.layer != LayerMask.NameToLayer("Room"))
                {
                    Destroy(collider.gameObject);
                }
            }

            // Destroy bomb object
            Destroy(gameObject);
        }
    }
}
