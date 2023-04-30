using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // moving
    public float moveSpeed;
    public Rigidbody2D theRB;

    // jumping
    public float jumpForce;

    private bool isGrounded;
    public Transform groundCheckPoint;
    public LayerMask whatIsGround;

    private bool canDoubleJump;

    private Animator anim;
    private SpriteRenderer theSR;


    // ===================================================================
    public float glideSpeed = 20f;
    public float gravityScale = 5;
    public float fallingGravityScale = 1;
    public float linearDrag = 4f;
    // ===================================================================


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        theSR = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // moving velocity
        theRB.velocity = new Vector2(moveSpeed * Input.GetAxis("Horizontal"), theRB.velocity.y);

        // grounded bool
        isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, .2f, whatIsGround);

        // grounded check, cannot double jump
        if (isGrounded)
        {
            canDoubleJump = true;
            GetComponent<Rigidbody2D>().drag = 0;
        }
        // jump button check 
        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded)
            {
                // jump command
                theRB.velocity = new Vector2(theRB.velocity.x, jumpForce);
            }
            // double jump
            else
            {
                if (canDoubleJump)
                {
                    // jump command
                    theRB.velocity = new Vector2(theRB.velocity.x, jumpForce);
                    GetComponent<Rigidbody2D>().drag += 20;
                    // resets canDoubleJump bool after double jump
                    canDoubleJump = false;
                    
                    if (Input.GetButtonUp("Jump") && theRB.velocity.y < 0)
                    {
                        theRB.drag = linearDrag * 5;
                    }
                    canDoubleJump = false;
                }
            }
        }
        if (theRB.velocity.x < 0)
        {
            theSR.flipX = true;
        }
        else if (theRB.velocity.x > 0)
        {
            theSR.flipX = false;
        }
        anim.SetFloat("moveSpeed", Mathf.Abs(theRB.velocity.x));
        anim.SetBool("isGrounded", isGrounded);
    }

     //private void Glide()
     //{
        //theRB.drag += 20f;
        //theRB.velocity = new Vector2(theRB.velocity.x, Mathf.Clamp(theRB.velocity.y, -glideSpeed, float.MaxValue));
     //}
}


  // private void OnCollisionEnter2D(Collision2D other)
  // {
  //   if (other.gameObject.tag == "Platform")
  //   {
  //     transform.parent = other.transform;
  //   }
  // }

  // private void OnCollisionExit2D(Collision2D other)
  // {
  //   if (other.gameObject.tag == "Platform")
  //   {
  //     transform.parent = null;
  //   }
  // }



