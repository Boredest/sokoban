using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public new Rigidbody2D rigidbody { get; private set; }
    public float speed = 5f;
    private PlayerInput playerInput;
    private Vector2 playerDirection;


    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
    }//Awake

    private void Update()
    {
       playerDirection = playerInput.SetDirection();
    }//Update

    private void FixedUpdate()
    {
        MovePlayer(playerDirection);

    }//FixedUpdate

    private void MovePlayer(Vector2 direction)
    {
        Vector2 position = rigidbody.position;
        Vector2 translation = direction * speed * Time.fixedDeltaTime;
        rigidbody.MovePosition(position + translation);
    }

   
}
