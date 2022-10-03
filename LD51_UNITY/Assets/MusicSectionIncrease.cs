using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicSectionIncrease : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            MusicManager.instance.section++;
            if (MusicManager.instance.section > 4)
            {
                MusicManager.instance.section = 4;
            }
            Destroy(gameObject);
        }
    }
}