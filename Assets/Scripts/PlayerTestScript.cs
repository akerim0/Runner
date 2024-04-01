using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTestScript : MonoBehaviour
{
    public float jumpForce = 4f;
    public float jumpDistance = 3f;
    public float gravityScale = -15f;
    public float moveSpeed = 4f;
    public bool canMove = true;
    public bool gravityControl = false;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            float horz = Input.GetAxis("Horizontal");
            float vert = Input.GetAxis("Vertical");

             rb.velocity = new Vector3(horz, 0, vert) * moveSpeed;
        }
       
        if (Input.GetKey(KeyCode.Space) && !canMove)
        {
            Debug.Log("Jump pressed");
            Vector3 direction = Vector3.up * CalculateJumpVerticalSpeed() + Vector3.forward * jumpDistance;
            rb.AddForce(direction,ForceMode.Impulse);
        }
        if (gravityControl)
        {
            if (rb.velocity.y < 0) Physics.gravity = new Vector3(0, 1, 0) * gravityScale;
        }
        
    }
    float CalculateJumpVerticalSpeed()
    {
        // Calculate jump velocity based on desired jump height and gravity
        return Mathf.Sqrt(2 * jumpForce * Mathf.Abs(Physics.gravity.y));
    }
}
