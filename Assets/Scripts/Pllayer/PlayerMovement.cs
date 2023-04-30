using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public new Rigidbody2D rigidbody { get; private set; }
    public float speed = 5f;
   
    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
       
    }//Awake

    public void MovePlayer(Vector2 movementVector)
    {
        Vector2 position = rigidbody.position;
        Vector2 translation = movementVector * speed * Time.fixedDeltaTime;
        rigidbody.MovePosition(position + translation);
    }
}
