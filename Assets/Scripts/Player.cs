using UnityEngine;
using UnityEngine.XR;
using Vector2 = UnityEngine.Vector2;

public class Player : MonoBehaviour
{
    [Header("Movement")] [Range(0f, 100f)] public float movementSpeed;
    [Range(0f, 100f)] public float jumpForce = 1f;
    [Range(0f, 100f)] public float maxJump = 10f;
    public bool isGrounded;
    private Rigidbody2D _rb;

    [Header("Aiming with mouse position Y")] [Range(0f, 100f)]
    float mouseSens = 100f;
    public Transform Cam;

    public Weapon weapon;
    [Range(0f, 100f)] [SerializeField] private float XRotate = 0f;
    public Vector3 direction;


    // Start is called before the first frame update
    void Start()
    {
       Cursor.visible = false;
        movementSpeed = 5;
        _rb = GetComponent<Rigidbody2D>();
        //Voorzorgt dat die niet omvalt bij een collider (alleen Rigidbody2D)
        _rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");

        Vector2 move = new Vector2(horizontal, 0);
        transform.Translate(move * Time.deltaTime * movementSpeed);

        if (Input.GetButton("left shift"))
        {
            movementSpeed = 10f;
        }
        else
        {
            movementSpeed = 5f;
        }

        //controleert de input van jump en moet kijken om de speler al grounded is of niet
        if (Input.GetButton("Jump") && isGrounded)
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

void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}