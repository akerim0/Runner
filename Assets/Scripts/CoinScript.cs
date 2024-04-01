using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    [SerializeField] private float spinSpeed = 15f;
   
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, 1,0) * spinSpeed) ;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerScript>() != null)
        {
            Destroy(this.gameObject);
        }
    }
}
