using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickerSprite : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    internal void Setup(float frequency, float length)
    {
        StartCoroutine(DoFlickering(frequency, length));
    }

    IEnumerator DoFlickering(float frequency, float length)
    {
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();

        float time = 0;
        float freqTimer = 0;
        while (time < length)
        {
            time += Time.deltaTime;
            freqTimer += Time.deltaTime;
            if (freqTimer > frequency)
            {
                freqTimer = 0;
                renderer.enabled = !renderer.enabled;
            }
            yield return null;
        }

        renderer.enabled = true;
        Destroy(this);
    }
}
