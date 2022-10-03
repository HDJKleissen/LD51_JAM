using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetMusicSection : MonoBehaviour
{
    public int SectionNum;
    // Start is called before the first frame update
    void Start()
    {
        MusicManager.instance.SetMusicSection(SectionNum);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
