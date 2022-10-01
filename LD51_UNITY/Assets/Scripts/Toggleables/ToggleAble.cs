using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Toggleable : MonoBehaviour
{
    public bool IsToggledOn;

    public void Toggle()
    {
        SetState(!IsToggledOn);
    }

    public void SetState(bool newState)
    {
        IsToggledOn = newState;
        HandleStateChange();
    }

    public abstract void HandleStateChange();
}
