using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
  public float moveSpeed;
  public Rigidbody2D rigidBody;
  public float jumpForce;
  private bool isGrounded;
  public Transform groundCheckPoint;
  public LayerMask whatIsGround;
  private bool canDoubleJump;

  private Animator anim;
  private SpriteRenderer theSR;

  // Start is called before the first frame update
  void Start()
  {
    anim = GetComponent<Animator>();
    theSR = GetComponent<SpriteRenderer>();
  }

  void Update()
  {
    rigidBody.velocity = new Vector2(moveSpeed * Input.GetAxis("Horizontal"), rigidBody.velocity.y);
    isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, .2f, whatIsGround);

    if(isGrounded)
    {
      canDoubleJump = true;
      // reset drag
      GetComponent<Rigidbody2D>().drag = 0;
    }
    if(Input.GetButtonDown("Jump"))
    {
      if(isGrounded)
      {
        rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpForce);
      }
      else
      {
        if(canDoubleJump)
        {
          rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpForce);
          // increase drag for Gliding
          GetComponent<Rigidbody2D>().drag += 20;
          canDoubleJump = false;
        }
      }
    }

// ========================switches direction player is facing based on movement=============================================
    if(rigidBody.velocity.x < 0)
    {
      theSR.flipX = true;
    }
    else if(rigidBody.velocity.x > 0)
    {
      theSR.flipX = false;
    }

// ================enables running animation when moving horizontally=====================================================
    anim.SetFloat("moveSpeed", Mathf.Abs(rigidBody.velocity.x));
    anim.SetBool("isGrounded", isGrounded);
  }

// =============tethers player position to platform once you land on platform========================================================
  private void OnCollisionEnter2D(Collision2D other)
  {
    if (other.gameObject.tag == "Platform")
    {
      transform.parent = other.transform;
    }
  }
// ============removes connection to platform once you jump off=========================================================
  private void OnCollisionExit2D(Collision2D other)
  {
    if (other.gameObject.tag == "Platform")
    {
      transform.parent = null;
    }
  }
}
