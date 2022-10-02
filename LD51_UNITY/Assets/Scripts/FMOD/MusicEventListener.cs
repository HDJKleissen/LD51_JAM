using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicEventListener : MonoBehaviour
{
    [SerializeField] private Image image;

    private Color blue = new Color { r = 0.9490196f, g = 0.1333333f, b = 1f, a = 0.6f };
    private Color purple = new Color { r = 1f, g = 0.1607843f, b = 0.4588235f, a = 0.6f };
    private Color red = new Color { r = 1f, g = 0.5647059f, b = 0.1215686f, a = 0.6f };
    private Color yellow = new Color { r = 0.5490196f, g = 0.1176471f, b = 1f, a = 0.6f };


    // Start is called before the first frame update
    void Start()
    {
        MusicManager.BeatUpdated += OnBeat;
        MusicManager.MarkerUpdated += OnMarker;
    }

    private void OnDestroy()
    {
        MusicManager.BeatUpdated -= OnBeat;
        MusicManager.MarkerUpdated -= OnMarker;
    }

    private void OnBeat(int value)
    {
        Debug.Log("Beat: " + value);
        switch (value)
        {
            case 1:
                image.color = blue;
                break;
            case 2:
                image.color = purple;
                break;
            case 3:
                image.color = red;
                break;
            case 4:
                image.color = yellow;
                break;
        }

    }

    private void OnMarker(string bar)
    {
        //Debug.Log("Marker: " + bar);
    }
}
