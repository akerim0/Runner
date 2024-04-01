using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyGround : MonoBehaviour
{
    Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
        cam = Camera.main;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        float dist = cam.WorldToViewportPoint(this.transform.position).z;
        if (dist < 0)
        {
            Destroy(this.gameObject);
        }
    }
}
