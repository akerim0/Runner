using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

enum PlayerState { idle,running,jumping,sliding,falling};
enum PlayerPos { left, center , right};
public class PlayerScript : MonoBehaviour
{
    [Header("Inscribed")]
    [SerializeField] float playerSpeed = 4f;
    [SerializeField] float jumpOffset = 0.6f;
    [SerializeField] float jumpDistance = 2f;
    [SerializeField] float easing = 0.5f;
    [SerializeField] float left = -0.278f;
    [SerializeField] float center = 0.092f;
    [SerializeField] float right = 0.458f;

    [Header("BoxCast Fields")]
    public float centerX = 0;
    public float centerY = 0.43f;
    public float centerZ = -0.55f;
    public float halfExtX =0.13f;
    public float halfExtY = 0.11f;
    public float halfExtZ = 0.45f;
    public float maxDist = 0.5f;

    //0, 0.09706477f, 0.1409425f) ,new Vector3(0.5f, 1.19413f/2, 0.7181157f/2
    [Header("Dynamic")]

    [SerializeField] PlayerState state = PlayerState.idle;
    [SerializeField] PlayerPos Xpos = PlayerPos.center;
    [SerializeField] bool GameOver = false;
    [SerializeField] bool GameStarted = false;
    [SerializeField] bool isGrounded = false;

    float yOffset = 0;
    bool isJumpPressed = false;
    public bool isRunning = true;
    Animator animator;
    Rigidbody rb;
    CharacterController charcontrol;
    Vector3 gravityvector;

    private void Awake()
    {
        transform.position = new Vector3(center, 0.092f, 0);
    }
    // Start is called before the first frame update
    void Start()
    {
        charcontrol = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space) && GameOver == false)
        {
            animator.SetBool("run", true);
            GameStarted = true;
        }
        if (GameOver && GameStarted) {
            animator.SetBool("run", false);
            animator.SetBool("GameOver", true);
            animator.SetTrigger("gameOver");
            return;
        }
        
        
        Move();
        if (this.animator.GetCurrentAnimatorStateInfo(0).IsName("Running"))
        {
            Debug.Log("Running ");
            state = PlayerState.running;
        }
        else if (this.animator.GetCurrentAnimatorStateInfo(0).IsName("Sliding"))
        {
            Debug.Log("Slising ");
            state = PlayerState.sliding;
        }
        else if (this.animator.GetCurrentAnimatorStateInfo(0).IsName("Jumping"))
        {
            Debug.Log("Jumping ");
            state = PlayerState.jumping;
        }

        //if (GameOver) animator.SetBool("GameOver", true);
    }
    private void FixedUpdate()
    {

        //if(Physics.Raycast(new Vector3(transform.position.x, transform.position.y + 0.3f, transform.position.z), Vector3.forward, out RaycastHit hitInfo, 0.1f)) { 
        //    if(hitInfo.transform.gameObject.tag == "ground")
        //    {
        //        Debug.Log("There is an abstacle");
        //        GameOver =  true;
        //    }

        //}
        if (Physics.BoxCast(new Vector3(transform.position.x + centerX, transform.position.y + centerY, transform.position.z + centerZ), new Vector3(halfExtX, halfExtY, halfExtZ), Vector3.forward, out RaycastHit info, Quaternion.identity, maxDist))
        {
            if (info.distance < 0.5f)
            {
                if ((info.transform.tag == "obstacle" || info.transform.tag == "ground") && state != PlayerState.sliding)
                {
                    GameOver = true;
                }
            }
            else
            {
                Debug.Log("Obstacle HIT ! " + info.transform.name);
            }
            
           //else if(info.transform.name == "ground")
        }
        
        if (Physics.Raycast(transform.position,transform.TransformDirection( Vector3.down),out RaycastHit hit, 5f))
        {
            if (hit.distance < 0.1f)
            {
                isGrounded = true;
                Debug.Log("player is Grounded");
            }
            else
            {
                isGrounded = false;
                Debug.Log("Player is Jumping");
            }    
        }
        if(!GameOver && GameStarted)
        {
            transform.position += Vector3.forward * playerSpeed * Time.fixedDeltaTime;
        }

        if (isJumpPressed && state == PlayerState.jumping)
        {
            Vector3 direction = Vector3.up * CalculateJumpVerticalSpeed();
            rb.AddForce(direction, ForceMode.VelocityChange);

            isJumpPressed = false;
        }
        
        //rb.AddForce(Vector3.forward * playerSpeed,ForceMode.Acceleration);
        //rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, rb.velocity.z * playerSpeed);
    }
  
    float CalculateJumpVerticalSpeed()
    {
        // Calculate jump velocity based on desired jump height and gravity
        return Mathf.Sqrt(2 * jumpOffset * Mathf.Abs(Physics.gravity.y));
    }
    private void Move()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (Xpos == PlayerPos.center) Xpos = PlayerPos.right;
            else if (Xpos == PlayerPos.left) Xpos = PlayerPos.center;

        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            if (Xpos == PlayerPos.center) Xpos = PlayerPos.left;
            else if (Xpos == PlayerPos.right) Xpos = PlayerPos.center;

        }

        switch (Xpos)
        {
            case PlayerPos.right:
                transform.position = Vector3.Lerp(transform.position, new Vector3(right, transform.position.y, transform.position.z), easing);
                break;
            case PlayerPos.left:
                transform.position = Vector3.Lerp(transform.position, new Vector3(left, transform.position.y, transform.position.z), easing);
                break;
            case PlayerPos.center:
                transform.position = Vector3.Lerp(transform.position, new Vector3(center, transform.position.y, transform.position.z), easing);
                break;
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            animator.SetTrigger("jump");
            isJumpPressed = true;
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            animator.SetTrigger("slide");
        }
        

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position, Vector3.down*0.05f);
        //Gizmos.DrawCube()
        Gizmos.DrawWireCube(new Vector3(transform.position.x + centerX ,transform.position.y + centerY, transform.position.z + centerZ), new Vector3(halfExtX, halfExtY, halfExtZ) * 2);
        
    }

}
