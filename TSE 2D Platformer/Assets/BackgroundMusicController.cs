using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusicController : MonoBehaviour
{
    public AudioSource sound;
    public AudioClip slow, normal, fast;

    public void ChangeMusic(int difficulty)
    {
        if (difficulty <= 5) sound.clip = slow; sound.Play();
        if (difficulty > 5 && difficulty< 9) sound.clip = normal; sound.Play();
        if (difficulty >= 9) sound.clip = fast; sound.Play();
    }
}
