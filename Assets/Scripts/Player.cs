using UnityEngine;
using Vector2 = UnityEngine.Vector2;

public class Player : MonoBehaviour
{
    [Header("Movement")] 
    [Range(0f, 100f)]public float movementSpeed;
    [Range(0f, 100f)]public float jumpForce = 1f;
    [Range(0f, 100f)]public float maxJump = 10f;
    public bool isGrounded;
    private Rigidbody2D _rb;

    [Header("Aiming with mouse position Y")]
    [Range(0f, 100f)] float mouseSens = 100f;
    public Transform playerBody;
    public Weapon weapon;
    [Range(0f, 100f)] [SerializeField] private float XRotate = 0f;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        movementSpeed = 5;
        _rb = GetComponent<Rigidbody2D>(); 
        //Voorzorgt dat die niet omvalt bij een collider (alleen Rigidbody2D)
        _rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSens * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSens * Time.deltaTime;
        
        float horizontal = Input.GetAxis("Horizontal");

        Vector2 move = new Vector2(horizontal,0);
        transform.Translate(move * Time.deltaTime * movementSpeed, Space.Self);

        XRotate -= mouseY;
        XRotate = Mathf.Clamp(XRotate, -90f, 90f);
        
        playerBody.Rotate(Vector2.up * mouseX);
        Camera.main.transform.localRotation = Quaternion.Euler(XRotate, 0f,0f );
        if (Input.GetButton("left shift"))
        {
            movementSpeed = 10f;
        }
        else
        {
            movementSpeed = 5f;
        }

        if (Input.GetButton("Jump") && isGrounded && _rb.velocity.magnitude > 0)
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