using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MovableBarrier : Toggleable
{
    public Transform BarrierSprite;
    public Vector3 OffLocation, OnLocation;
    [SerializeField] float moveDuration;
    
    public override void HandleStateChange()
    {
        BarrierSprite.DOLocalMove(IsToggledOn ? OnLocation : OffLocation, moveDuration);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position + OffLocation, .1f);
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position + OnLocation, .1f);
    }

    private void OnValidate()
    {
        BarrierSprite.localPosition = IsToggledOn ? OnLocation : OffLocation;
    }
}
