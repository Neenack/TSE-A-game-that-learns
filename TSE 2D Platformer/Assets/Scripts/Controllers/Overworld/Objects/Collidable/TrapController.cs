using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Controllers.World.Collidable
{
    public class TrapController : MonoBehaviour
    {
        public LayerMask blockLayer;

        // Start is called before the first frame update
        public void BeginSelf()
        {
            Collider2D blockDetector = Physics2D.OverlapCircle(new Vector2(transform.position.x, transform.position.y - 1), 0.1f, blockLayer);
            if (blockDetector == null) //If no block below then destroy
            {
                Destroy(this.gameObject);
            }

            blockDetector = Physics2D.OverlapCircle(new Vector2(transform.position.x, transform.position.y + 1), 0.1f, blockLayer);
            if (blockDetector != null) //IF block directly above then destroy
            {
                Destroy(this.gameObject);
            }

            blockDetector = Physics2D.OverlapCircle(transform.position, 0.1f, blockLayer);
            if (blockDetector != null) //IF block directly above then destroy
            {
                Destroy(this.gameObject);
            }
        }
    }
}