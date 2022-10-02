using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using DG.Tweening;

public class LightningManager : MonoBehaviour
{
    [SerializeField] GameObject localLightsContainer;
    List<Light2D> allSpotLights = new List<Light2D>();
    [SerializeField] List<Color> LightColors = new List<Color>();


    // Start is called before the first frame update
    void Start()
    {
        //get all the lights in scene attached to container
        foreach (Transform t in localLightsContainer.transform) {
            allSpotLights.Add(t.GetComponent<Light2D>());
        }
    }

    private void FlipSomeLightSwitches(int beatNumber)
    {
        if (beatNumber == 1)
        {
            foreach (Light2D l in allSpotLights)
            {
                TurnOn(l);
            }

            TurnOff(GetRandomSpotLight);
        }

        if(beatNumber == 2 || beatNumber == 4 )
        {
            ChangeLightColor(GetRandomSpotLight);
        }
    }

    private Light2D GetRandomSpotLight => allSpotLights[Random.Range(0, allSpotLights.Count)];
    private Color GetRandomLightColor => LightColors[Random.Range(0, LightColors.Count)];

    private void ChangeLightColor(Light2D light)
    {
        DOTween.To(() => light.color, x => light.color = x, GetRandomLightColor, 0.2f);
    }

    private void TurnOn(Light2D light)
    {
        light.enabled = true;
    }

    private void TurnOff(Light2D light)
    {
        light.enabled = false;
    }

    private void OnEnable()
    {
        MusicManager.BeatUpdated += FlipSomeLightSwitches;
    }

    private void OnDisable()
    {
        MusicManager.BeatUpdated -= FlipSomeLightSwitches;
    }

}
