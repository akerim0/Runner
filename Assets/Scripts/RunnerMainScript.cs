using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GroundType { normalG,jumpG,slideG }
public class RunnerMainScript : MonoBehaviour
{
    Camera cam;
    [Header("Inscribed")]    
    public Transform GROUND_PARENT;
    public GameObject NormalGround, SlideGround, JumpGround;
    public Transform groundlastpos;
    public GameObject Coin;
    public Transform COINS_PARENT;

    [Tooltip("Ground")]
    [SerializeField] private float ZgroundOffset = 2.109f;
    [SerializeField] private float YgroundOffset = 0.45f;
    public float coinGenerationDelay = 1.0f;
    GroundType lastGrType = GroundType.normalG;

    [Tooltip("Coins Positions")]
    [SerializeField] float left = -0.278f;
    [SerializeField] float center = 0.092f;
    [SerializeField] float right = 0.458f;


    [Header("Dynamic")]
    GroundType groundType = GroundType.normalG; 
    Vector3 lastposition;
    int previousCoinsDisp = -1;
    float coinGenerationEndTime;
    bool canGenerateCoins = true;
    

    // Start is called before the first frame update
    void Awake()
    {
        lastposition = groundlastpos.transform.position;

    }

    private void FixedUpdate()
    {
        Vector3 groundPosInCam = Camera.main.WorldToViewportPoint(groundlastpos.position);
       // Debug.Log("Ground pos In camera" + groundPosInCam);
        if (groundPosInCam.y < 0.4f) GenerateGround();
       
    }

    void GenerateGround()
    {
        int rand = Random.Range(0, 20);

        switch (rand)
        {
            case 0:
                groundType = GroundType.jumpG;
                break;
            case 1:
                groundType = GroundType.slideG;
                break;
            default:
                groundType = GroundType.normalG;
                break;
        }

        //Check Grounds behind it
        if (groundType == GroundType.jumpG)
        {
            if (lastGrType == GroundType.normalG)
            {
                Vector3 currG = groundlastpos.position;
                for (int i = 0; i < 3; i++)
                {
                    if (currG.y != 0)
                    {
                        groundType = GroundType.normalG;
                        break;
                    }
                    currG.z -= ZgroundOffset;
                }
            }
            else if (lastGrType == GroundType.slideG)
            {
                groundType = GroundType.normalG;
            }

        }
        else if (groundType == GroundType.slideG)
        {
            if (lastGrType == GroundType.slideG || lastGrType == GroundType.jumpG)
            {
                groundType = GroundType.normalG;
            }
        }

        float xPos = 0f;
        float zPos = groundlastpos.position.z + ZgroundOffset;
        float yPos = 0;
        GameObject newGround;

        if (groundType == GroundType.jumpG)
        {
            yPos = YgroundOffset;
            int randNum = Random.Range(1, 10);

            for(int y = 0; y < randNum; y++)
            {
                newGround = Instantiate(JumpGround, GROUND_PARENT);
                newGround.transform.position = new Vector3(xPos, yPos, groundlastpos.position.z + ZgroundOffset);
                groundlastpos = newGround.transform;
                GenerateCoins(groundlastpos);
            }

        }

        else if(groundType == GroundType.slideG)
        {
            newGround = Instantiate(SlideGround, GROUND_PARENT);
            newGround.transform.position = new Vector3(xPos, yPos, zPos);
            groundlastpos = newGround.transform;
            GenerateCoins(groundlastpos);
        }
        else 
        {
            newGround = Instantiate(NormalGround, GROUND_PARENT);
            newGround.transform.position = new Vector3(xPos, yPos, zPos);
            groundlastpos = newGround.transform;
            GenerateCoins(groundlastpos);
        }

        lastGrType = groundType;
               
    }
    void GenerateCoins(Transform groundPos)
    {
        //int numOfCoins = Random.Range(4, 8);
        //float groundSize = 0;
        int coinsDisp = Random.Range(1, 15);
        Transform CoinSlotR = groundPos.Find("CoinsSlotR");
        Transform CoinSlotL = groundPos.Find("CoinsSlotL");
        Transform CoinSlotC = groundPos.Find("CoinsSlotC");

        CoinSlotR.gameObject.SetActive(false);
        CoinSlotL.gameObject.SetActive(false);
        CoinSlotC.gameObject.SetActive(false);

        float lastCoinPos = 0.9116f;
        switch (coinsDisp) {

            case 1:
                CoinSlotR.gameObject.SetActive(true);
                InstantiateCoins(CoinSlotR);
                break;
            case 2:
                CoinSlotC.gameObject.SetActive(true);
                InstantiateCoins(CoinSlotC);
                break;
            case 3:
                CoinSlotL.gameObject.SetActive(true);
                InstantiateCoins(CoinSlotL);
                break;
            case 4:
                CoinSlotR.gameObject.SetActive(true);
                InstantiateCoins(CoinSlotR);
                CoinSlotC.gameObject.SetActive(true);
                InstantiateCoins(CoinSlotC);
                break;
            case 5:
                CoinSlotL.gameObject.SetActive(true);
                InstantiateCoins(CoinSlotL);
                CoinSlotC.gameObject.SetActive(true);
                InstantiateCoins(CoinSlotC);
                break;
            case 6:
                CoinSlotL.gameObject.SetActive(true);
                InstantiateCoins(CoinSlotL);
                CoinSlotR.gameObject.SetActive(true);
                InstantiateCoins(CoinSlotR);
                break;
            case 7:
                CoinSlotL.gameObject.SetActive(true);
                InstantiateCoins(CoinSlotL);
                CoinSlotR.gameObject.SetActive(true);
                InstantiateCoins(CoinSlotR);
                CoinSlotC.gameObject.SetActive(true);
                InstantiateCoins(CoinSlotC);
                break;
            default:
                break;
        }

    }
    void InstantiateCoins(Transform CoinSlot)
    {
        float lastCoinPos = 0.9116f;
        for (int i = 1; i <= 5; i++)
        {
            GameObject newCoin = Instantiate(Coin, CoinSlot);
            newCoin.transform.localPosition = new Vector3(0, 0.09f, lastCoinPos);
            newCoin.transform.localScale = new Vector3(17, 5.283019f, 14);
            lastCoinPos += -0.4558f;
        }
    }
}
