using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
  // this is the door or whatever you want the button to affect.
  public GameObject objectToSwitch; 
  
  // self-explanatory, but this is the class that actually renders the sprite in the environment. This will be re-assigned to the "pressed button" sprite when the player interacts with the button.
  private SpriteRenderer spriteRenderer; 
  public Sprite downButton; 
  private bool hasSwitched; 

  void Start() 
  {
    spriteRenderer = GetComponent<SpriteRenderer>();
  }

  void Update()
  {
    
  }

// in this instance, "other" is the player or whatever we want to assign as interacting with the button.
  private void OnTriggerEnter2D(Collider2D other) 
  {
    if (other.tag == "Player" && !hasSwitched)
    {
      objectToSwitch.SetActive(false); 
      spriteRenderer.sprite = downButton;
      hasSwitched = true;
    }
  }
}
