using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAreaScript : MonoBehaviour
{
    public GameObject callScript;
    CreateGround createground;

    // Start is called before the first frame update
    void Start()
    {
        createground = callScript.GetComponent<CreateGround>();
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "player")
        {
            //createground.MakeGround();
            transform.position = createground.firstpos.transform.position;
        }
        
    }
}
