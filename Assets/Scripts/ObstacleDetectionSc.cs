using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleDetectionSc : MonoBehaviour
{
    public GameObject playerGO;
    PlayerScript playersc;
    // Start is called before the first frame update
    void Awake()
    {
        playersc = playerGO.GetComponent<PlayerScript>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        //if (collision.gameObject.tag == "ground")
        //{
        //    Debug.Log("Box collide : Obstacle");
        //    playersc.isRunning = false;
        //}
        Debug.Log("Box collide : Obstacle");
        playersc.isRunning = false;
    }
}
