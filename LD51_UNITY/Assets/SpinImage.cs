using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinImage : MonoBehaviour
{
    public RectTransform rTransform;
    public float speed = 5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rTransform.rotation = Quaternion.Euler(new Vector3(0, 0, -Time.time * speed));
        speed += 1;
    }
}
