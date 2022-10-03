using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : Interactable
{
    [SerializeField] bool Active;

    public Sprite OnSprite, OffSprite;

    public List<Toggleable> LinkedToggleables;

    public override void Interact()
    {
        Active = !Active;
        SetCorrectSprite();
        HandleToggleables();
        // SFX: Oneshot lever sound can use Active bool for on/off diff
        FMODUnity.RuntimeManager.PlayOneShotAttached("event:/Lever", gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        if(spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        if(LinkedToggleables.Count == 0)
        {
            LinkedToggleables = new List<Toggleable>(GetComponentsInChildren<Toggleable>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetCorrectSprite()
    {
        spriteRenderer.sprite = Active ? OnSprite : OffSprite;
    }

    void HandleToggleables()
    {
        foreach (Toggleable toggleable in LinkedToggleables)
        {
            toggleable.Toggle();
        }
    }

    private void OnValidate()
    {
        SetCorrectSprite();
        if (LinkedToggleables.Count != GetComponentsInChildren<Toggleable>().Length)
        {
            LinkedToggleables = new List<Toggleable>(GetComponentsInChildren<Toggleable>());
        }
    }
}
