using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testScript : MonoBehaviour
{
    Animator _animator;
    CharacterController charcontroller;
    Vector3 physicvector;
    [SerializeField] private float velocity = 6f;
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        charcontroller = GetComponent<CharacterController>();
        charcontroller.detectCollisions = true;
    }

    // Update is called once per frame
    void Update()
    {
        physicvector = Vector3.zero;
        //if (!charcontroller.isGrounded && )
        //    physicvector += Physics.gravity;

        charcontroller.Move(new Vector3(Input.GetAxis("Horizontal"), physicvector.y, Input.GetAxis("Vertical")) * velocity * Time.deltaTime);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _animator.SetBool("isRunning", true);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            _animator.SetBool("isRolling", true);
            
        }
        if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Rolling") && _animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
        {
            _animator.SetBool("isRolling",false);
        }

            //else
            //{
            //    _animator.SetBool("isJumping", false);
            //}
        }
    IEnumerator waitfor(float secs)
    {
        yield return new WaitForSeconds(secs);
        _animator.SetBool("isSliding", false);

    }
    //private void OnControllerColliderHit(ControllerColliderHit hit)
    //{
    //    // Debug.Log("Collision>..");
    //    if (hit.gameObject.tag == ("coin"))
    //    {
    //        Destroy(hit.gameObject);
    //        Debug.Log("Collision !!");
    //    }
    //}
    //private void OnTriggerEnter(Collider other)
    //{
    //    Destroy(other.gameObject);
    //    if (other.gameObject.tag == "coin")
    //    {
    //        Destroy(other.gameObject);
    //    }
    //}
    //void OnCollisionEnter(Collision collision)
    //{
    //    Debug.Log("Collision>..");
    //}
}
