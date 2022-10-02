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
    }

    // Start is called before the first frame update
    void Start()
    {
        if(spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
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
            toggleable.SetState(Active);
        }
    }

    private void OnValidate()
    {
        SetCorrectSprite();
    }
}
