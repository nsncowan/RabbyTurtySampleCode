// Throw
// push object
// no jump

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtyController : MonoBehavior
{
  public float moveSpeed;
  public Rigidbody2D rigidBody;
  public float pushForce;


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

    if(rigidBody.velocity.x < 0)
    {
      theSR.flipX = true;
    }
    else if(rigidBody.velocity.x > 0)
    {
      theSR.flipX = false;
    }

    anim.SetFloat("moveSpeed", Mathf.Abs(rigidBody.velocity.x));
  }

  private void OnControllerColliderHit(ControllerColliderHit hit)
  {
    Rigidbody rb = hit.collider.attachedRigidbody;
    if (rb != null && !rb.isKinematic)
    {
      rb.velocity = hit.moveDirection * pushForce;
    }
  }

}