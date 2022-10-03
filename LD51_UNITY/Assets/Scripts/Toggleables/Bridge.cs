using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bridge : Toggleable
{
    [SerializeField] List<BridgeSegment> bridgeSegmentsInOrder;
    public override void HandleStateChange()
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached("event:/Bridge", gameObject);
       /* // if same sound remove if and else lol
        if (IsToggledOn)
        {
            // SFX: Oneshot bridge opening

        }
        else
        {
            // SFX: Oneshot bridge closing
        }*/
        StartCoroutine(SetBridgeState(!IsToggledOn));
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnValidate()
    {
            
    }

    IEnumerator SetBridgeState(bool open)
    {
        if (open)
        {
            for (int i = 0; i < bridgeSegmentsInOrder.Count; i++)
            {
                bridgeSegmentsInOrder[i].SetBridgeState(open);
                yield return new WaitUntil(() => bridgeSegmentsInOrder[i].IsOpen);
            }
        }
        else
        {
            for (int i = bridgeSegmentsInOrder.Count-1; i >= 0; i--)
            {
                bridgeSegmentsInOrder[i].SetBridgeState(open);
                yield return new WaitUntil(() => bridgeSegmentsInOrder[i].IsClosed);
            }
        }
    }
}