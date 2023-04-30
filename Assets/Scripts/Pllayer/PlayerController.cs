using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    private PlayerInput playerInput;
    private PlayerAnimation playerAnimation;
    private PlayerMovement playerMovement;
    private Vector2 playerInputDir;


    private void Awake()
    {
        playerAnimation = GetComponent<PlayerAnimation>();
        playerInput = GetComponent<PlayerInput>();
        playerMovement = GetComponent<PlayerMovement>();
    }//Awake

    private void Update()
    {
       playerInputDir = playerInput.InputDirection();
       playerAnimation.SetSpriteDirection(playerInputDir);
    }//Update

    private void FixedUpdate()
    {
        playerMovement.MovePlayer(playerInputDir);
    }//FixedUpdate

    

   
}
