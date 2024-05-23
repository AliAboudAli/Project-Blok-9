using UnityEngine;

public class Movements : MonoBehaviour
{
    [Header("Movement")]
    public float movementSpeed;
    public float jumpForce = 1;
     
    public Vector2 maxJump = new Vector2(0,10);
    public bool IsJumping;
    Rigidbody2D rb;
    
    
    [Header("Health")]
    public float health;
    public float maxHealth = 100;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            IsJumping = true;
        }

        if (IsJumping)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            maxJump = new Vector2(0, 0);
        }
    }
}
