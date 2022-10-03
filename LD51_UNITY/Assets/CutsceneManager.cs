using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Video;

public class CutsceneManager : MonoBehaviour
{
    public UnityEvent OnVideoFinished;

    [SerializeField]
    private string videoFileName;
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

    // Start is called before the first frame update
    void Start()
    {
        player.url = System.IO.Path.Combine(Application.streamingAssetsPath, videoFileName);
        player.Play();
    }
    private void Player_loopPointReached(VideoPlayer source)
    {
        Debug.Log("video ova");
        OnVideoFinished.Invoke();
    }
}
