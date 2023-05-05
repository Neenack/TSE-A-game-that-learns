using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombThrowing : MonoBehaviour
{
    public GameObject bombPrefab;
    public float throwSpeed = 10f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = Camera.main.nearClipPlane; // Set the z-position to the near clip plane of the camera
            Vector3 throwDirection = Camera.main.ScreenToWorldPoint(mousePos) - transform.position;
            throwDirection.Normalize(); // Make sure the direction is a unit vector

            GameObject newBomb = Instantiate(bombPrefab, transform.position, Quaternion.identity);
            Rigidbody2D bombRb = newBomb.GetComponent<Rigidbody2D>();
            bombRb.velocity = throwDirection * throwSpeed;
        }
    }
}
