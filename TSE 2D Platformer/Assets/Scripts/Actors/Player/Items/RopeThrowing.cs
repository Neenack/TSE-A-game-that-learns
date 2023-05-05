using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeThrowing : MonoBehaviour
{
    public GameObject ropeBall;
    public float throwSpeed = 15f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = Camera.main.nearClipPlane; // Set the z-position to the near clip plane of the camera
            Vector3 throwDirection = Camera.main.ScreenToWorldPoint(mousePos) - transform.position;
            throwDirection.Normalize(); // Make sure the direction is a unit vector

            GameObject newRope = Instantiate(ropeBall, transform.position, Quaternion.identity);
            Rigidbody2D ropeRb = newRope.GetComponent<Rigidbody2D>();
            ropeRb.velocity = throwDirection * throwSpeed;
        }
    }
}
