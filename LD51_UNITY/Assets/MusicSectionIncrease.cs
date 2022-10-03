using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicSectionIncrease : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            MusicManager.instance.SetMusicSection(MusicManager.instance.section+1);
            if (MusicManager.instance.section > 4)
            {
                MusicManager.instance.SetMusicSection(4);
            }
            Destroy(gameObject);
        }
    }
}