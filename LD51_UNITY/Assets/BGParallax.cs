using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGParallax : MonoBehaviour
{
    Vector2 startPos;
    Vector2 camStartPos;
    Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        startPos = transform.position;
        camStartPos = cam.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = startPos + (camStartPos - new Vector2(cam.transform.position.x, cam.transform.position.y)) * 0.02f;
    }
}
