using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private int currentFrame;
    private float frameRate = 3f;
    private SpriteRenderer spriteRenderer;

    public Sprite[] upFrames;
    public Sprite[] downFrames;
    public Sprite[] leftFrames;
    public Sprite[] rightFrames;

    
    private void Awake()
    {   
        spriteRenderer = GetComponent<SpriteRenderer>();
    }//Awake

    public void SetSpriteDirection(Vector2 direction)
    {
        Sprite[] frames = null;
        if (direction.y > 0) // up
        {
            frames = upFrames;
        }
        else if (direction.y < 0) // down
        {
            frames = downFrames;
        }
        else if (direction.x < 0) // left
        {
            frames = leftFrames;
        }
        else if (direction.x > 0) // right
        {
            frames = rightFrames;
        }

       
        if (frames != null)
        {
            currentFrame = Mathf.FloorToInt(Time.time * frameRate) % frames.Length;
            spriteRenderer.sprite = frames[currentFrame];
            currentFrame++;
            if (currentFrame >= frames.Length)
            {
                currentFrame = 0;
            }
            

        }
    }//SetSpriteDirection
}
