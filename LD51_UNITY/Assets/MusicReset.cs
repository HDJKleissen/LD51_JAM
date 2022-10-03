using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicReset : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        MusicManager.instance.section = 0;   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
