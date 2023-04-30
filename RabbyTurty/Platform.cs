using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
  [Header("Platform Movement")]
  public Transform[] points; 
  public float moveSpeed;
  public int currentPoint;
  public Transform platform;

  [Header("Button")]
  public GameObject buttonTarget;

  private SpriteRenderer theSR;
  public Sprite downButton;
  private bool isPressed;

  void Start()
  {
    theSR = GetComponent<SpriteRenderer>();
  }

  void Update()
  {
    MovingPlatform();
  }

  private void MovingPlatform()
  {
    /* this moves the platform to the first position (starting the loop) */
    platform.position = Vector3.MoveTowards(platform.position, points[currentPoint].position, moveSpeed * Time.deltaTime); 

    /* this checks if the platform has has reached the destination */
    if (Vector3.Distance(platform.position, points[currentPoint].position) < .05f)
    {
      currentPoint ++; /* this increments currentPoint */
      if (currentPoint >= points.Length) /* this checks if the platform has reached the end of the array of points */
      {
        currentPoint = 0; /* this resets the index of points, restarting the loop */
      }
    }

  }


  private void MovePlatformWithButton(Collider2D other)
  {
    if (other.tag == "Player" && !hasSwitched)
    {
      objectToSwitch.SetActive(false);
      theSR.sprite = downButton;
      isPressed = true;
    }

  }





}
