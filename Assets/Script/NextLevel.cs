using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressLevel : GameManager
{
    public AudioSource EndAudio;

    private void OnTriggerEnter2D(Collider2D collision)
    {
       
        if (!collision.CompareTag("Player"))
            return;

        if (collision.CompareTag("Player"))
        {
            if (EndAudio != null)
            {
                GameObject.Instantiate(EndAudio, transform.position, Quaternion.identity);
            }
            GoToNextLevel();
        }
    }
}
