using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressLevel : GameManager
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;

        GoToNextLevel();
    }
}
