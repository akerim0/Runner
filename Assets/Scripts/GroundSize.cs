using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSize : MonoBehaviour
{
    static GroundSize S;
    static public float _groundSize;
    Renderer renderer;
    // Start is called before the first frame update
    void Awake()
    {
        renderer = GetComponent<Renderer>();
        _groundSize = renderer.bounds.size.x;
        Debug.Log("Size = " + _groundSize);
        
    }
    //static public float groundSize { get  => S._groundSize ; private set { } }
}
