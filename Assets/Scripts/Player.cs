using UnityEngine;
using Vector2 = UnityEngine.Vector2;

public class Player : MonoBehaviour
{
    [Header("Movement")] 
    public float movementSpeed;
    public float jumpForce = 1f;
    public float maxJump = 10f;
    public bool isGrounded;
    private Rigidbody2D _rb;
    
    // Start is called before the first frame update
    void Start()
    {
        movementSpeed = 5;
        _rb = GetComponent<Rigidbody2D>(); 
    }

    // Update is called once per frame
    void Update()
    {
        
        float horizontal = Input.GetAxis("Horizontal");

        Vector2 move = new Vector2(horizontal,0);
        transform.Translate(move * Time.deltaTime * movementSpeed, Space.Self);
    }

    public void FixedUpdate()
    {
        if (Input.GetButton("Jump") && isGrounded & _rb.velocity.magnitude > 0)
        {
            if (isGrounded)
            {
                _rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                isGrounded = false;

                if (_rb.velocity.y > maxJump)
                {
                    _rb.velocity = new Vector2(_rb.velocity.x, maxJump);
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}