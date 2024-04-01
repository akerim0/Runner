using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateGround : MonoBehaviour
{
    Camera cam;
    [Header("Inscribed")]
    public float groundOffset = 2.12f;
    public Transform GROUND_PARENT;
    public GameObject NormalGround, RollGround, JumpGround;
    public GameObject Coins;
    public Transform groundlastpos;
    public GameObject firstpos;

    [Header("Dynamic")]
    Vector3 lastposition;

    // Start is called before the first frame update
    void Start()
    {
        lastposition = groundlastpos.transform.position;
        
    }

    private void Update()
    {
        Vector3 groundPosInCam = Camera.main.WorldToViewportPoint(groundlastpos.position);
        Debug.Log("Ground pos In camera" + groundPosInCam);
        if(groundPosInCam.y <= 1.0f)
        {
            //float size = groundlastpos.gameObject.GetComponent<Renderer>().bounds.size.y;
            GameObject newGround = Instantiate(NormalGround,GROUND_PARENT);
            newGround.transform.position = new Vector3(groundlastpos.position.x, groundlastpos.position.y, groundlastpos.position.z + groundOffset);
            groundlastpos = newGround.transform;
        }

    }
   
    void GenerateCoins(float z)
    {
        int randnum = Random.Range(0,10);
        if (randnum == 5)
        {
            Instantiate(Coins, new Vector3(-0.386f, 0.211f, z), Quaternion.identity);
            Instantiate(Coins, new Vector3(0.02f, 0.211f, z), Quaternion.identity);
            Instantiate(Coins, new Vector3(0.386f, 0.211f, z), Quaternion.identity);
        }
        else if (randnum >= 1 && randnum < 4)
        {
            Instantiate(Coins, new Vector3(-0.386f, 0.211f, z), Quaternion.identity);
        }
        else if (randnum >= 6 && randnum <= 8)
        {
            Instantiate(Coins, new Vector3(0.386f, 0.211f, z), Quaternion.identity);
        }

    }
}
