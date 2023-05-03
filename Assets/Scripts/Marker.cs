using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Marker : MonoBehaviour
{
    
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private Sprite triggeredBoxSprite;
    [SerializeField]
    private Sprite unTriggeredBoxSprite;
    private bool triggered;
    
    private void Awake()
    {
    
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Start()
    {
      triggered = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Box")
        {
            triggered = true;
            Debug.Log("Trigger success");
            SpriteRenderer boxSprite = other.GetComponent<SpriteRenderer>();
            boxSprite.sprite = triggeredBoxSprite;
            GameManager.Instance.currentMarkerCount++;
            Debug.Log("Current Markers Triggered =: " + GameManager.Instance.currentMarkerCount);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Box")
        {
            Debug.Log("Testing remove trigger");
            SpriteRenderer boxSprite = other.GetComponent<SpriteRenderer>();
            triggered = false;
            boxSprite.sprite = unTriggeredBoxSprite;
            GameManager.Instance.currentMarkerCount--;
            Debug.Log("Current Markers Triggered =: " + GameManager.Instance.currentMarkerCount);
        }
        

    }
}
