using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Overworld.Rooms.Interactable
{
    public class RoomType : MonoBehaviour
    {
        public int type;

        public void DestroyRoom()
        {
            Destroy(gameObject);
        }
    }
}