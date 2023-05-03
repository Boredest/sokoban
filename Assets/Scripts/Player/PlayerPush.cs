using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPush : MonoBehaviour
{
    [SerializeField]
    private float pushSpeed = 5f;
    private void Awake()
    {
       
    }//Awake

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Box")
        {
            Debug.Log("Collision detected.");
            Rigidbody2D boxRB = collision.gameObject.GetComponent<Rigidbody2D>();
            Vector2 pushDirection = (collision.transform.position - transform.position).normalized;
            Vector2 pushForce = GetPushDirection(pushDirection) * pushSpeed;
            boxRB.AddForce(pushForce, ForceMode2D.Force);

        }
    }

    

    private Vector2 GetPushDirection(Vector2 pushDirection)
    {
        Vector2 pushDirAbs = new Vector2(Mathf.Abs(pushDirection.x), Mathf.Abs(pushDirection.y));
        if (pushDirAbs.x > pushDirAbs.y)
        {
            return new Vector2(pushDirection.x, 0f).normalized;
        }
        else
        {
            return new Vector2(0f, pushDirection.y).normalized;
        }
    }
}

