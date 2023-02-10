using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;

    public bool onGround;
    bool isJumping;

    float jumpTimeCounter;
    public float jumpTime = 0.35f;

    float jumpForce = 6;
    float speed = 5;

    public LayerMask ground;
    public Animator anim;
    bool facingRight = true;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (!Cam.PlayMode)
            return;

        float moveLR = Input.GetAxisRaw("Horizontal") * speed;

        //animate right
        if (moveLR > 0)
        {
            anim.SetBool("MoveLeft", false);
            if (!facingRight)
            {
                anim.SetTrigger("TurnRight");
                facingRight = true;
            }
            anim.SetBool("MoveRight", true);
        }

        //animate left
        if (moveLR < 0)
        {
            anim.SetBool("MoveRight", false);
            if (facingRight)
            {
                anim.SetTrigger("TurnLeft");
                facingRight = false;
            }
            anim.SetBool("MoveLeft", true);
        }

        //dont animate
        if (moveLR == 0)
        {
            anim.SetBool("MoveRight", false);
            anim.SetBool("MoveLeft", false);
        }

        //move
        rb.velocity = new Vector3(moveLR, rb.velocity.y, rb.velocity.z);
    }

    private void Update()
    {
        if (!Cam.PlayMode)
            return;

        //initial jump
        onGround = Physics.CheckSphere(transform.position + new Vector3(0, -0.5f, 0), 0.5f, ground);
        if (onGround && (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow)))
        {
            rb.velocity = Vector3.up * jumpForce;
            jumpTimeCounter = jumpTime;
            isJumping = true;
        }

        //continue jumping further when held
        if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.UpArrow)) && isJumping)
        {
            if (jumpTimeCounter > 0)
            {
                rb.velocity = Vector3.up * jumpForce;
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }

        //stop jumping
        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.UpArrow))
        {
            isJumping = false;
        }
    }
}
