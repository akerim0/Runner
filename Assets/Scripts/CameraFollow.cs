using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{   
    [Header("Inscribed")]
    [SerializeField] float camSpeed = 4f;
    public Transform playerpos;

    float easing;
    Vector3 diff;

    // Start is called before the first frame update
    void Start()
    {
        diff = playerpos.position - transform.position;
    }

    // Update is called once per frame
    void LateUpdate()

    {
        easing = Time.deltaTime * camSpeed;
        float transfX = transform.position.x;
        float transfY = transform.position.y;
        float transfZ = playerpos.position.z - diff.z;
        transform.position = Vector3.Lerp(transform.position,new Vector3(transfX,transfY,transfZ),easing); 
    }
}
