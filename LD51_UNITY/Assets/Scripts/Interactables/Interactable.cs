using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public abstract class Interactable : MonoBehaviour
{
    [SerializeField] public SpriteRenderer spriteRenderer;
    public abstract void Interact();

    public void ShowUsable(bool usable)
    {
        if(spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
        spriteRenderer.material.color = usable ? Color.green : Color.white;
    }
}
