using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightningManager : MonoBehaviour
{
    [SerializeField] GameObject localLightsContainer;
    List<Light2D> allSpotLights = new List<Light2D>();
    // Start is called before the first frame update
    void Start()
    {
        foreach(Transform t in localLightsContainer.transform){
            allSpotLights.Add(t.GetComponent<Light2D>());
        }
    }

    public void BlinkLight(float duration)
    {
        
    }

    private void FlipSomeLightSwitches(int beatNumber)
    {
        foreach(Light2D l in allSpotLights)
        {
            TurnOn(l);
        }

        TurnOff(GetRandomSpotLight);
    }

    private Light2D GetRandomSpotLight => allSpotLights[Random.Range(0, allSpotLights.Count)];


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



    // Update is called once per frame
    void Update()
    {
        
    }
}
