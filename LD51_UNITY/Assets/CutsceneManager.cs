using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Video;

public class CutsceneManager : MonoBehaviour
{
    public UnityEvent OnVideoFinished;

    public VideoPlayer player;
    // Start is called before the first frame update
    void OnEnable()
    {
        player.loopPointReached += Player_loopPointReached;
    }
    void OnDisable()
    {
        player.loopPointReached -= Player_loopPointReached;
    }

    private void Player_loopPointReached(VideoPlayer source)
    {
        Debug.Log("video ova");
        OnVideoFinished.Invoke();
    }
}
